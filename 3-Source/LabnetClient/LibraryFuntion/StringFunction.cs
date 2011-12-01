using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Web.Script.Serialization;

namespace LibraryFuntion
{
    public static class StringFunction
    {
        public static String StripDiacritics(String accented)
        {
            if (accented == null)
                return "";

            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            String strFormD = accented.Normalize(System.Text.NormalizationForm.FormD);
            String result = regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            return Regex.Replace(result, "[^a-zA-Z0-9_]+", "-", RegexOptions.Compiled);
        }
        public static string ConvertDateToString(DateTime _Date)
        {
            string strDate = _Date.Date.Year.ToString() + "-"
                            + (_Date.Month.ToString().Length == 2 ? _Date.Month.ToString() : "0" + _Date.Month.ToString()) + "-"
                            + (_Date.Day.ToString().Length == 2 ? _Date.Day.ToString() : "0" + _Date.Day.ToString()) + " "
                            + (_Date.Hour.ToString().Length == 2 ? _Date.Hour.ToString() : "0" + _Date.Hour.ToString()) + ":"
                            + (_Date.Minute.ToString().Length == 2 ? _Date.Minute.ToString() : "0" + _Date.Minute.ToString()) + ":"
                            + (_Date.Second.ToString().Length == 2 ? _Date.Second.ToString() : "0" + _Date.Second.ToString());
            return strDate;
        }
        public static DateTime StringToDate(string _Date)
        {
            //string dateString = "Sun 15 Jun 2008 8:30 AM -06:00";
           DateTime dt1= Convert.ToDateTime(_Date);
            return dt1;
        }

        /// <summary>
        /// Chuyển tiếng việt có dấu thành không dấu
        /// </summary>
        /// <param name="text">Chuỗi tiếng việt có dấu</param>
        /// <returns>Chuỗi tiếng việt không dấu</returns>
        public  unsafe static string RejectMarks(this string source)
        {

            String[] pattern = new String[7];
            Char[] replaceChar = new Char[14];
            replaceChar[0] = 'a';
            replaceChar[1] = 'd';
            replaceChar[2] = 'e';
            replaceChar[3] = 'i';
            replaceChar[4] = 'o';
            replaceChar[5] = 'u';
            replaceChar[6] = 'y';
            replaceChar[7] = 'A';
            replaceChar[8] = 'D';
            replaceChar[9] = 'E';
            replaceChar[10] = 'I';
            replaceChar[11] = 'O';
            replaceChar[12] = 'U';
            replaceChar[13] = 'Y';
            //Mẫu cần thay thế tương ứng
            pattern[0] = "(á|à|ả|ã|ạ|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ)"; //letter a
            pattern[1] = "đ";   //letter d
            pattern[2] = "(é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ)"; //letter e
            pattern[3] = "(í|ì|ỉ|ĩ|ị)"; //letter i
            pattern[4] = "(ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ)"; //letter o
            pattern[5] = "(ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự)"; //letter u
            pattern[6] = "(ý|ỳ|ỷ|ỹ|ỵ)"; //letter y
            fixed (Char* ptrChar = replaceChar)
            {
                for (int i = 0; i < pattern.Length; i++)
                {
                    MatchCollection matchs = Regex.Matches(source, pattern[i], RegexOptions.IgnoreCase);
                    foreach (Match m in matchs)
                    {
                        Char ch = Char.IsLower(m.Value[0]) ? *(ptrChar + i) : *(ptrChar + i + 7);
                        source = source.Replace(m.Value[0], ch);
                    }
                }
            }
            return source;
        }

    }
}
