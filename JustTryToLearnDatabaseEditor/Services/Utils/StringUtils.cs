﻿using System;

namespace JustTryToLearnDatabaseEditor.Services.Utils
{
    public static class StringUtils
    {
        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input[0].ToString().ToUpper() + input.Substring(1);
            }
        }

        public static string NormalizeString(this string input)
        {
            input = input.ToLower();
            input = input.Trim();
            input = input.FirstCharToUpper();
            
            return input;
        }
    }
}
