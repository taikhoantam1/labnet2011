using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryFuntion
{
    public class HTML_Render
    {
        public static string GetHTMLComboBox(Dictionary<int, string> cmb_value, string name, string id,bool haveAll)
        {
            string code = "<select  style='width:150px;'";
            if(name!="")
                code+=" name='" + name + "'";
            if(id!="")
                code+=" id='" + id + "'";
            code += ">";

            foreach (int key in cmb_value.Keys)
            {
                code += "<option value='" + key.ToString() + "'>" + cmb_value[key] + "</option>";
            }
            if(haveAll)
                code += "<option value='-1'>View All</option>";
            code+="</select>";
            return code;
        }
    }
}
