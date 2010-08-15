// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Summary description for User
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace App_Code
{
    public class WebUser
    {
        public string Username { get; private set; }
        private readonly string _dataDir;

        public static List<string> GetUsernames(string dataDir)
        {
            var directories = Directory.GetDirectories(dataDir);
            var dirInfo = directories.Select(dir => new DirectoryInfo(dir));
            var users = dirInfo.Select(dir => dir.Name).ToList();
            return users;
        }

        public WebUser(string username, string dataDir)
        {
            Username = username;
            _dataDir = dataDir;
        }

        public string UserDir { get { return Path.Combine(_dataDir, Username); } }
        private const string EmailFileName = "email.txt";

        public List<FileInfo> GetFiles()
        {
            var files = Directory.GetFiles(UserDir);

            // Be sure to omit the email file.
            return (from file in files
                    where file != EmailFileName
                    select new FileInfo(file)).ToList();
        }

        public string Email
        {
            get
            {
                StreamReader reader = new StreamReader(Path.Combine(UserDir, EmailFileName));
                var email = reader.ReadLine();
                return email;
            }
        }
    }
}
