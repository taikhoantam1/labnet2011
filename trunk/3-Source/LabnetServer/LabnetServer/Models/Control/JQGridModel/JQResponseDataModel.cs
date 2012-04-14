using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace JQGrid.Models
{
    public class JQResponseDataModel
    {
        public JQResponseDataModel()
        {
            rows = new List<JQRow>();
        }
        public List<JQRow> rows { get; set; }

        public int CurrentPage { get; set; }

        public int Pages { get; set; }

        public int PageSize { get; set; }

        public int NumberRecords { get; set; }

    }

    public class JQRow
    {
        public string id { get; set; }

        public List<string> cell { get; set; }
    }
    public static class JsonHelper
    {

        public static string ToJson(this object obj)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static string ToJson(this object obj, int recursionDepth)
        {

            var serializer = new JavaScriptSerializer();



            serializer.RecursionLimit = recursionDepth;



            return serializer.Serialize(obj);

        }

    }
}