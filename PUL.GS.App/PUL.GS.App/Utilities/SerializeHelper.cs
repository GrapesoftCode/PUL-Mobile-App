using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PUL.GS.App.Utilities
{
    internal class SerializeHelper
    {
        public static string SerializeObject(object item)
        {
            return JsonConvert.SerializeObject(item, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
        public static T DeserializeObject<T>(string args)
        {
            if (!string.IsNullOrEmpty(args))
            {
                return JsonConvert.DeserializeObject<T>(args, new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            }
            return default(T);
        }
        public static string Encrypt(string str)
        {
            string txt = str;
            byte[] bs = null;

            bs = Encoding.UTF8.GetBytes(txt);
            int[] ns = new int[bs.Length];

            for (int i = 0; i <= bs.Length - 1; i++)
            {
                ns[i] = Convert.ToInt32(bs[i]);
            }
            StringBuilder s = new StringBuilder();

            foreach (int n in ns)
                s.Append(n.ToString("x2").ToLower());

            return s.ToString();
        }
        public static string Decrypt(string str)
        {
            string txt = str;
            string[] ArrayHex = new string[txt.Length / 2];
            byte[] ArrayDec = new byte[txt.Length / 2];
            int hex = 0;

            for (int i = 0; i < ArrayHex.Length; i++)
            {
                ArrayHex[i] = txt[hex].ToString() + txt[hex + 1].ToString();
                hex = hex + 2;
            }

            for (int j = 0; j < ArrayDec.Length; j++)
            {
                ArrayDec[j] = byte.Parse(ArrayHex[j], NumberStyles.HexNumber);
                hex = hex + 2;
            }

            Encoding utf8 = Encoding.UTF8;
            string deCodstr = utf8.GetString(ArrayDec, 0, ArrayDec.Length);

            return deCodstr;
        }
    }
}
