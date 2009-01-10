using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace FaraLJBackup
{
    public partial class Form1 : Form
    {
        bool m_backupInProgress = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void BackupButton_Click(object sender, EventArgs e)
        {
            if (Username.Text == String.Empty)
            {
                MessageBox.Show("You did not enter a username.");
            }

            if (Password.Text == String.Empty)
            {
                MessageBox.Show("You did not enter a password.");
            }
          
            if (m_backupInProgress)
            {
                MessageBox.Show("A backup is already in progress.");
            }

            m_backupInProgress = true;

            Thread thread = new Thread(new ThreadStart(BackupThread));
            thread.IsBackground = true;
            thread.Start();
        }

        void BackupThread()
        {
            try
            {
                string filePath = String.Empty;
                if (InvokeRequired)
                {
                    Invoke((MethodInvoker)delegate()
                         {
                             filePath = PromptForFile();
                         });
                }
                else
                {
                    filePath = PromptForFile();
                }
                if (filePath == String.Empty)
                {
                    DisplayMessage("No backup file selected.  Backup aborted.");
                    m_backupInProgress = false;
                    return;
                }

                List<Entry> entries = new List<Entry>();
                DateTime date = StartCalendar.SelectionStart;
                while (date <= DateTime.Now)
                {
                    DisplayMessage("Getting events for " + date.ToString());
                    GetEntries(date, entries);
                    date = date.AddDays(1);
                }

                if (entries.Count == 0)
                {
                    DisplayMessage("No entries found.  Backup aborted.");
                    m_backupInProgress = false;
                    return;
                }

               

                StreamWriter writer = null;

                try
                {

                    writer = new StreamWriter(filePath);
                    writer.WriteLine("<ul>\r\n");
                    foreach (Entry entry in entries)
                    {
                        writer.WriteLine("<li><a href=\"#" + entry.ID + "\">" + entry.Title + "</a>\r\n");
                    }
                    writer.WriteLine("</ul>");

                    foreach (Entry entry in entries)
                    {
                        writer.WriteLine(entry.ToString());
                    }
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }

                DisplayMessage("Backup complete.");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error performing backup: " + ex.Message);
            }
            finally
            {
                m_backupInProgress = false;
            }
        }

        // NOTE: Not thread-safe.  Must be called from invoke if necessary
        private string PromptForFile()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.OverwritePrompt = true;
            dialog.CreatePrompt = true;           
            dialog.ShowDialog();
            return dialog.FileName;
        }
        delegate void LogDelegate();
        private void DisplayMessage(string message)
        {

            LogDelegate TempDelegate = delegate()
            {
               if (MessageLog.TextLength > 60000)
                  {
                  MessageLog.Select(0, MessageLog.TextLength / 2);
                  MessageLog.SelectedText = "***BUFFER CLEARED***\n";
                  }

               MessageLog.AppendText(message + "\n");
              MessageLog.ScrollToCaret();

            };

            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate()
                     {
                         TempDelegate();
                     });
            }
            else
            {
                TempDelegate();
            }
        }

        private void GetEntries(DateTime date, List<Entry> entries)
        {
            Dictionary<string, string> postParams = new Dictionary<string, string>();
            string postString = String.Empty;
            string responseString;

            // Hash the password using MD5 and convert the byte array into a
            // simple hex string (e.g., 0x01 0x02 becomes "0102")

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] passwordBytes = Encoding.ASCII.GetBytes(Password.Text);
            byte[] hashBytes = md5.ComputeHash(passwordBytes);
            StringBuilder builder = new StringBuilder(hashBytes.Length * 2);
            foreach (byte b in hashBytes)
            {
                builder.AppendFormat("{0:x2}", b);
            }
            string hashedPassword = builder.ToString();

            postParams["mode"] = "getevents";
            postParams["user"] = Username.Text;
            postParams["hpassword"] = hashedPassword;
            postParams["prefersubject"] = "0";
            postParams["noprops"] = "0";
            postParams["selecttype"] = "day";
            postParams["year"] = date.Year.ToString();
            postParams["month"] = date.Month.ToString();
            postParams["day"] = date.Day.ToString();
            postParams["lineendings"] = "pc";

            foreach (string key in postParams.Keys)
            {
                if (postString != String.Empty)
                {
                    postString += "&";
                }
                postString += key + "=" + postParams[key];
            }

            responseString = HTTPPost(postString);

            // The response contains a list of key/value pairs, with the key and value
            // on separate lines.  For example:
            // LINE1: eventCount
            // LINE2: 1
            // LINE3: events_1_subject
            // LINE4: My Title
            // etc.

            Dictionary<string, string> responseParams = new Dictionary<string, string>();
            Queue<string> lines = new Queue<string>();

            foreach (string line in responseString.Split(new Char[] { '\n' }))
            {
                lines.Enqueue(line);
            }


            // Loop through the key/value pairs and convert them into a nice
            // parameter dictionary.

            while (lines.Count >= 2)
            {
                string key = lines.Dequeue();
                string value = lines.Dequeue();
                responseParams[key] = value;
            }

            if (!responseParams.ContainsKey("success") ||
                responseParams["success"] != "OK")
            {
                throw new Exception("Error reading events from server: " +
                    responseString);
            }

            int eventsCount = int.Parse(responseParams["events_count"]);
            for (int eventIndex = 1; eventIndex <= eventsCount; eventIndex++)
            {
                Entry entry = new Entry();
                entry.DecodeEntry(responseParams, eventIndex);
                entries.Add(entry);
            }
        }

        private string HTTPPost(string postData)
        {
            WebRequest request = null;
            HttpWebResponse response = null;
            Stream dataStream = null;
            StreamReader reader = null;

            byte[] byteArray;
            string responseFromServer = null;

            try
            {
                // Create a request for the URL.         

                request = WebRequest.Create("http://www.livejournal.com/interface/flat");

                // Set the Method property of the request to POST and the content type to a form.
                // Also set our pre-authenticate flag because the router expects authentication headers
                // on every request.
                // NOTE: Without the authentication flag, it will work intermittently.

                request.Method = "POST";
                //request.ContentType = "application/x-www-form-urlencoded";
                //request.PreAuthenticate = true;


                // Create POST data and convert it to a byte array.  Set our content length based
                // on the length of the array.

                byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = byteArray.Length;

                // Use a data stream to write out the request.  This just places
                // the byte array into the request's outgoing data stream.

                dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                // Read the data from the response's data stream.

                response = (HttpWebResponse)request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
            }

            catch (Exception e)
            {
                responseFromServer = null;

                MessageBox.Show("Error communicating with server: " + e.Message);
            }

            finally
            {

                // Clean up the streams and responses.

                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }

                if (dataStream != null)
                {
                    dataStream.Close();
                    dataStream.Dispose();
                }

                if (response != null)
                {
                    response.Close();
                }
            }

            return responseFromServer;
        }



        class Entry : IComparable
        {
            string m_id;
            string m_title;
            string m_body;
            string m_url;
            string m_tags;
            DateTime m_timestamp;

            public string ID
            {
                get { return m_id; }
            }

            public string Title
            {
                get { return m_title; }
            }

            public string Body
            {
                get { return m_body; }
            }

            public string Url
            {
                get { return m_url; }
            }

            public DateTime Timestamp
            {
                get { return m_timestamp; }
            }

            public string Tags
            {
                get { return m_tags; }
            }

            public Entry()
            {
            }

            public void DecodeEntry(Dictionary<string, string> responseParams, int eventIndex)
            {
                // Look up useful events parameters
                string evtPrefix = "events_" + eventIndex + "_";

                m_url = responseParams[evtPrefix + "url"];
                m_id = responseParams[evtPrefix + "itemid"];

                // The body has fancy formatting that needs to be decoded.
                m_body = DecodeBody(responseParams[evtPrefix + "event"]);

                m_timestamp = DateTime.Parse(responseParams[evtPrefix + "eventtime"]);

                // Subjects are optional, so use date if they're not there.
                if (responseParams.ContainsKey(evtPrefix + "subject"))
                {
                    m_title = responseParams[evtPrefix + "subject"];
                }
                else
                {
                    m_title = m_timestamp.ToShortDateString();
                }

                // Try to find the list of tags.  This is in a property with 
                // the name "taglist" and an itemid matching our ID.

                m_tags= String.Empty;
                int propCount = int.Parse(responseParams["prop_count"]);
                for (int propIndex = 1; propIndex <= propCount; propIndex++)
                {
                    string propPrefix = "prop_" + propIndex + "_";

                    if ((responseParams[propPrefix + "itemid"] == m_id) &&
                        (responseParams[propPrefix + "name"] == "taglist"))
                    {
                        m_tags = responseParams[propPrefix + "value"];
                    }
                }

            }

            public override string ToString()
            {
                return "<a name=\"" + m_id + "\"/>" + 
                    "<a href=\"" + m_url + "\">" +
                     "<h1>" + m_title + "</h1></a>\r\n" +
                     "<p><b>" + m_timestamp.ToString() + "</b></p>\r\n" +
                     m_body + "\r\n" +
                     "<p><i>Tags:</i> " + m_tags + "</p>";
            }

            public int CompareTo(object obj)
            {
                Entry otherEntry = obj as Entry;
                if (otherEntry == null)
                {
                    return 0;
                }
                return DateTime.Compare(this.Timestamp, otherEntry.Timestamp);
            }

            private string DecodeBody(string orig)
            {
                // LJ encodes spaces as "+"
                orig = orig.Replace("+", " ");

                // Special chars are encoded like %22, where 22 is a hex value
                // We have a match evaluator function to do the conversion back.
                Regex regex = new Regex("%([a-fA-F0-9]{2,2})");
                orig = regex.Replace(orig, new MatchEvaluator(ToHTML));

                // Trim garbage comments.
                regex = new Regex("<!--(.|\n)*-->");
                orig = regex.Replace(orig, "");

                // Replace linebreaks with BR
                orig = orig.Replace("\r\n", "\r\n<br/>");

                return orig;
            }

            private string ToHTML(Match match)
            {
                int charValue = Convert.ToInt32(match.Groups[1].ToString(), 16);
                char c = Convert.ToChar(charValue);
                return c.ToString();
            }
        }
    }
}