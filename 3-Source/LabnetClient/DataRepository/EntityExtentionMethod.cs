using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq.Expressions;
using System.Reflection;

namespace DataRepository
{
    partial class LabManager_ClientEntities : ObjectContext
    {
        // Method definition 
        [EdmFunction("LabManager_ClientEntities", "fuChuyenCoDauThanhKhongDau")]
        public string ChuyenCoDauThanhKhongDau(string value)
        {
            return this.QueryProvider.Execute<string>(Expression.Call(
                Expression.Constant(this),
                (MethodInfo)MethodInfo.GetCurrentMethod(),
                Expression.Constant(value, typeof(double?))));
        }
        
    }
}
