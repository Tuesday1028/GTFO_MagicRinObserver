﻿namespace Hikaria.MagicRinObserver.Ext
{
    internal static class StringExt
    {
        public static string[] SplitByLength(this string str, int length)
        {
            List<string> result = new();

            for (int i = 0; i < str.Length; i += length)
            {
                int substringLength = Math.Min(length, str.Length - i);
                string substring = str.Substring(i, substringLength);
                result.Add(substring);
            }

            return result.ToArray();
        }
    }
}
