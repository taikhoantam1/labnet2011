using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Xml.Linq;
using System.Data;
using System.Xml;
using System.Reflection;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Linq.Expressions;
using System.Web.UI.HtmlControls;
using System.Data.Linq.SqlClient;

namespace LibraryFuntion
{
    public static class ExtensionMethods
    {
        public static IQueryable<T> OrderBy<T>(
               this IQueryable<T> source, string propertyName, bool asc)
        {
            var type = typeof(T);
            string methodName = asc ? "OrderBy" : "OrderByDescending";
            var property = type.GetProperty(propertyName);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName,
                              new Type[] { type, property.PropertyType },
                              source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }

        public static IQueryable<T> Like<T>(this IQueryable<T> source,
                      string propertyName, string keyword)
        {
            var type = typeof(T);
            var property = type.GetProperty(propertyName);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var constant = Expression.Constant("%" + keyword + "%");
            var like = typeof(SqlMethods).GetMethod("Like",
                       new Type[] { typeof(string), typeof(string) });
            MethodCallExpression methodExp =
                  Expression.Call(null, like, propertyAccess, constant);
            Expression<Func<T, bool>> lambda =
                  Expression.Lambda<Func<T, bool>>(methodExp, parameter);
            return source.Where(lambda);
        }
        public static IQueryable<T> MakeFlexQuery<T>(this IQueryable<T> source,FlexiQuery flexQuery, FlexigridViewData flexigridObject)
        {
            if (!string.IsNullOrEmpty(flexQuery.Qtype) && !string.IsNullOrEmpty(flexQuery.Query))
            {
                source = source.Like(flexQuery.Qtype, flexQuery.Query);
            }
            flexigridObject.page = flexQuery.Page;
            flexigridObject.total = source.Count();
            source = source.Skip((flexQuery.Page - 1) * flexQuery.Rp).Take(flexQuery.Rp);

            if (!string.IsNullOrEmpty(flexQuery.SortName) && !string.IsNullOrEmpty(flexQuery.SortOrder))
            {
                source = source.OrderBy(flexQuery.SortName, (flexQuery.SortOrder == "asc"));
            }
            return source;
        }
    }
}
