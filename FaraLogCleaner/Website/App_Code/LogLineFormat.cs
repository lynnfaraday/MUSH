// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogLineFormat.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Defines the LogLineFormat type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace App_Code
{

    public class LogLineFormat
    {
        public enum ActionType
        {
            Include,
            Omit,
            Replace
        }

        public enum MatchType
        {
            Regex,
            StartsWith,
            EndsWith,
            Contains
        }

        public string SpecifierString { get; set; }
        public ActionType Action { get; set; }
        public string ReplaceText { get; set; }
        public MatchType Match { get; set; }
        public string RegexString
        {
            get
            {
                switch (Match)
                {
                    case MatchType.EndsWith:
                        return SpecifierString + "$";
                    case MatchType.StartsWith:
                        return "^" + SpecifierString;
                    default:
                        return SpecifierString;
                }
            }
        }

        public LogLineFormat()
        {
        }

        public LogLineFormat(string regex, ActionType action, MatchType match)
            : this(regex, action, match, string.Empty)
        {
        }

        public LogLineFormat(string regex, ActionType action, MatchType match, string replaceText)
        {
            SpecifierString = regex;
            Action = action;
            Match = match;
            ReplaceText = replaceText;
        }
    }
}