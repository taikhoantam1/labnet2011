using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryFuntion
{
    public class FlexigridViewData

    {

        public int page;

        public int total;

        public List<FlexigridRow> rows = new List<FlexigridRow>();

    }

    

    public class FlexigridRow
   {

       public long id;
       public List<string> cell=new List<string>();

   }
}
