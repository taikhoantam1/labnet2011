using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace LibraryFuntion
{
    public class StringFunction
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
    }
}
