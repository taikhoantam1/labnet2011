using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryFuntion
{
    public class FlexiQuery
    {
        private int page;
        private int rp;
        private string qtype;
        private string sortName;
        private string sortOrder;
        private string query;

        public string Query
        {
            get { return query; }
            set { query = value; }
        }
        public int Page
        {
            get { return page; }
            set { page = value; }
        }


        public int Rp
        {
            get { return rp; }
            set { rp = value; }
        }


        public string Qtype
        {
            get { return qtype; }
            set { qtype = value; }
        }

        public string SortName
        {
            get { return sortName; }
            set { sortName = value; }
        }

        public string SortOrder
        {
            get { return sortOrder; }
            set { sortOrder = value; }
        }
    }
}
