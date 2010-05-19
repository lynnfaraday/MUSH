// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogFormat.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Summary description for LogFormat
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace App_Code
{
    public class LogFormat
    {
        private bool _lastLineBlank;
        public List<LogLineFormat> LineFormats { get; set; }

        public LogFormat(List<LogLineFormat> formats)
        {
            LineFormats = formats;
        }

        public LogFormat()
        {
            LineFormats = new List<LogLineFormat>();
        }

        public string Parse(StreamReader reader)
        {
            StringBuilder builder = new StringBuilder();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Parse(line, builder);
            }
            return builder.ToString();
        }

        public void Parse(string line, StringBuilder builder)
        {
            string modifiedLine;
            if (IncludeLine(line, out modifiedLine))
            {
                // Don't have lots of blank lines in a row.
                bool currentLineBlank = string.IsNullOrEmpty(modifiedLine);
                if (currentLineBlank && _lastLineBlank)
                {
                    return;
                }

                builder.AppendLine(modifiedLine);
                _lastLineBlank = currentLineBlank;

                if (!currentLineBlank)
                {
                    builder.AppendLine();
                    _lastLineBlank = true;
                }
            }
        }

        private bool IncludeLine(string line, out string modifiedLine)
        {
            line = line.Trim();
            modifiedLine = line;
            foreach (var lineFormat in LineFormats)
            {
                Regex regex = new Regex(lineFormat.RegexString);
                if (regex.IsMatch(line))
                {
                    switch (lineFormat.Action)
                    {
                        case LogLineFormat.ActionType.Include:
                            modifiedLine = line;
                            return true;
                        case LogLineFormat.ActionType.Omit:
                            return false;
                        case LogLineFormat.ActionType.Replace:
                            modifiedLine = Regex.Replace(line, lineFormat.RegexString, lineFormat.ReplaceText);
                            return true;
                    }
                }
            }
                       
            return true;
        }

        public static List<LogLineFormat> Read(string formatString)
        {
            List<LogLineFormat> lineFormats = new List<LogLineFormat>();
            char[] delimiters = new char[] {'\n', '\r'};
            foreach (string lineFormat in formatString.Split(delimiters, StringSplitOptions.RemoveEmptyEntries))
            {
                var format = ParseLogLineFormat(lineFormat);
                if (format != null)
                {
                    lineFormats.Add(format);
                }
            }
            return lineFormats;
        }

        public static LogLineFormat ParseLogLineFormat(string lineFormat)
        {
            // Skip comments or empty lines.
            if (String.IsNullOrEmpty(lineFormat) || lineFormat.StartsWith("#"))
            {
                return null;
            }

            char[] seps = {'|'};
            string[] split = lineFormat.Split(seps);
            int argCount = split.GetUpperBound(0) + 1;

            string specifier;
            string replaceText;
            LogLineFormat.MatchType matchType = LogLineFormat.MatchType.Regex;
            LogLineFormat.ActionType actionType;

            switch (argCount)
            {
                    // Assume default action of omit.
                case 1:
                    actionType = LogLineFormat.ActionType.Omit;
                    specifier = split[0];
                    replaceText = String.Empty;
                    break;

                    // Try to parse the action.
                case 2:
                    actionType = (LogLineFormat.ActionType) Enum.Parse(typeof (LogLineFormat.ActionType), split[0]);
                    specifier = split[1];
                    replaceText = string.Empty;
                    break;

                case 3:
                    actionType = (LogLineFormat.ActionType) Enum.Parse(typeof (LogLineFormat.ActionType), split[0]);
                    specifier = split[1];
                    replaceText = split[2];
                    break;

                    // Invalid # of args.
                default:
                    return null;
            }
            return new LogLineFormat(specifier, actionType, matchType, replaceText);
        }
    }
}