using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLTabulka1.API
{
    public static class Apid
    {
        private const string Chars = "0987654321qwertyupasdfghjkzxcvbnm";

        /// <summary>
        /// generuje sadu znaku z vyhrané sady
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Create(int length = 9)
        {
            int max = Chars.Length;
            var random = Random.Shared;
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(Chars[random.Next(0, max)]);
            }
            return stringBuilder.ToString();
        }
    }
}
