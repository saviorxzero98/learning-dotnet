using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EvaluateExpressionSample.Scripts
{
    public static class StringEx
    {
        /*public static string[] Split(this string value, string split)
        {
            return value.Split(split);
        }*/

        /// <summary>
        /// To Pascal Case
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToPascalCase(this string value)
        {
            return value.ToCamelCase(false);
        }
        /// <summary>
        /// To Pascal Case
        /// </summary>
        /// <param name="value"></param>
        /// <param name="isKeepSpaceOrPunctuation"></param>
        /// <returns></returns>
        public static string ToPascalCase(this string value, bool isKeepSpaceOrPunctuation)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("Error String!");

            string newValue = value;
            if (!isKeepSpaceOrPunctuation)
            {
                newValue = Regex.Replace(newValue, @"_|-", " ").ToFirstLetterUpper();
                newValue = Regex.Replace(newValue, @"_|-| ", string.Empty);
            }
            return newValue.FirstOrDefault().ToString().ToUpper() + newValue.Substring(1);
        }

        /// <summary>
        /// To Camel Case
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string value)
        {
            return value.ToCamelCase(false);
        }
        /// <summary>
        /// To Camel Case
        /// </summary>
        /// <param name="value"></param>
        /// <param name="isKeepSpaceOrPunctuation"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string value, bool isKeepSpaceOrPunctuation)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("Error String!");

            string newValue = value;
            if (!isKeepSpaceOrPunctuation)
            {
                newValue = Regex.Replace(newValue, @"_|-", " ").ToFirstLetterUpper();
                newValue = Regex.Replace(newValue, @"_|-| ", string.Empty);
            }

            return newValue.FirstOrDefault().ToString().ToLower() + newValue.Substring(1);
        }


        /// <summary>
        /// To First Letter Upper
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToFirstLetterUpper(this string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("Error String!");

            string[] words = value.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].FirstOrDefault().ToString().ToUpper() + words[i].Substring(1);
            }

            return string.Join(" ", words);
        }

        /// <summary>
        /// Truncate Word
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string TruncateWord(this string value, int length)
        {
            return value.TruncateWord(length, string.Empty);
        }
        /// <summary>
        /// Truncate Word
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <param name="trailingString"></param>
        /// <returns></returns>
        public static string TruncateWord(this string value, int length, string trailingString)
        {
            if (value == null || value.Length < length)
            {
                return value;
            }

            if (!string.IsNullOrEmpty(trailingString))
            {
                length -= trailingString.Length;
            }

            Regex regex = new Regex("[-’'.a-zA-Z0-9]+|[ ]+|[^ ^-’'.a-zA-Z0-9]");
            var tokens = regex.Matches(value);

            StringBuilder builder = new StringBuilder();
            foreach (var token in tokens)
            {
                if (builder.Length + token.ToString().Length <= length)
                {
                    builder.Append(token);
                }
                else
                {
                    break;
                }
            }

            if (!string.IsNullOrEmpty(trailingString))
            {
                builder.Append(" ");
                builder.Append(trailingString);
            }

            return builder.ToString();
        }
    }
}
