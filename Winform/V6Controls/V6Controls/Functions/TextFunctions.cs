using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using V6Controls.Functions.Enums;

namespace V6Controls.Functions
{
    public static class TextFunctions
    {
        public static string ChangeCase(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            var result = text;
            var state = GetTextState(text);
            switch (state)
            {
                case TextState.AllLower:
                    result = text.ToUpper();
                    break;
                case TextState.AllUpper:
                    result = ToFirstUpper(text);
                    break;
                case TextState.FirstUpper:
                    result = ToWordUpper(text);
                    break;
                case TextState.WordUpper:
                    result = text.ToLower();
                    break;
                case TextState.Unknow:
                    result = text.ToLower();
                    break;
                default:
                    result = text.ToLower();
                    break;
                //throw new ArgumentOutOfRangeException();
            }
            return result;
        }

        public static string ChangeToUnicode(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            var result = text;

            var code = V6Tools.ChuyenMaTiengViet.NhanDangMaTiengViet(text);
            if (code == "TCVN")
            {
                result = V6Tools.ChuyenMaTiengViet.TCVNtoUNICODE(text);
            }
            else if (code == "VNI")
            {
                result = V6Tools.ChuyenMaTiengViet.VNItoUNICODE(text);
            }
            
            return result;
        }

        private static string ToWordUpper(string text)
        {
            //var s = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());

            var result = "";
            var last_char = char.MinValue;
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsSeparator(last_char) || char.IsWhiteSpace(last_char) || last_char == char.MinValue)
                {
                    result += c.ToString().ToUpper();
                }
                else if (!char.IsSeparator(c) && !char.IsWhiteSpace(c))
                {
                    result += c;
                }
                else
                {
                    result += c.ToString().ToUpper();
                }
                last_char = c;
            }
            return result;
        }

        private static string ToFirstUpper(string text)
        {
            return text[0].ToString().ToUpper() + text.Substring(1).ToLower();
        }

        private static TextState GetTextState(string text)
        {
            var all_lower = true;
            var all_upper = true;
            var word_upper = true;
            var first_upper = char.IsUpper(text[0]);
            var last_char = char.MinValue;
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsUpper(c))
                {
                    all_lower = false;
                    if (i > 0) first_upper = false;
                }
                if (char.IsLower(c)) all_upper = false;
                if (char.IsSeparator(last_char) || char.IsWhiteSpace(last_char) || last_char == char.MinValue)
                {
                    if (char.IsLower(c)) word_upper = false;
                }
                else if (!char.IsSeparator(c) && !char.IsWhiteSpace(c))
                {
                    if (char.IsUpper(c)) word_upper = false;
                }
                last_char = c;
            }
            //foreach (char c in text)
            //{
            //    if (char.IsUpper(c)) all_lower = false;
            //    if (char.IsLower(c)) all_upper = false;
            //    if (char.IsSeparator(last_char) || char.IsWhiteSpace(last_char) || last_char == char.MinValue)
            //    {
            //        if (char.IsLower(c)) word_upper = false;
            //    }
            //    last_char = c;
            //}
            if (all_lower) return TextState.AllLower;
            if (all_upper) return TextState.AllUpper;
            if (word_upper) return TextState.WordUpper;
            if (first_upper) return TextState.FirstUpper;
            return TextState.Unknow;
        }
    }
}
