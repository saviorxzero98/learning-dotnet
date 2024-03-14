using System.Text;

namespace CryptoSample.Encoders
{
    public class UnicodeEncoder
    {
        public static UnicodeEncoder Instance
        {
            get
            {
                return new UnicodeEncoder();
            }
        }

        /// <summary>
        /// To Unicode
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Encode(string text)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in text)
            {
                sb.Append("\\u");
                sb.Append(string.Format("{0:x4}", (int)c));
            }
            return sb.ToString();
        }

        /// <summary>
        /// From Unicode
        /// </summary>
        /// <param name="unicode"></param>
        /// <returns></returns>
        public string Decode(string unicode)
        {
            string[] words = unicode.Split(new string[] { "\\u" }, StringSplitOptions.None);

            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < words.Length; i++)
            {
                string word = words[i];

                if (word.Length != 4)
                {
                    continue;
                }

                byte[] bytes = new byte[2];
                bytes[1] = Convert.ToByte(word.Substring(0, 2), 16);
                bytes[0] = Convert.ToByte(word.Substring(2, 2), 16);
                string text = Encoding.Unicode.GetString(bytes);
                sb.Append(text);
            }
            return sb.ToString();
        }
    }
}
