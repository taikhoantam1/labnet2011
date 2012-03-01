using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataRepository
{
   public class Repository : IRepository
    {
        private LabnetServerEntities myDb;
        public LabnetServerEntities Context { get { return myDb; } }

        /// <summary>
        /// 
        /// </summary>
        public Repository()
        {
            myDb = new LabnetServerEntities();
        }

        public LabnetAccount GetAccount(string UserName)
        {
            LabnetAccount account = Context.LabnetAccounts.Where(p => p.UserName == UserName).FirstOrDefault();
            return account;
        }
    }
}
