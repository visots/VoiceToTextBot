﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceToTextBot.Extensions
{
    internal class StringExtensions
    {
        /// <summary>
        /// Преобразование строки, чтобы она начиналась с заглавной буквы
        /// </summary>
        public static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}
