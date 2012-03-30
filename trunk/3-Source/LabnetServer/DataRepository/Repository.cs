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
        public Examination GetExamination(string examinatioNumber)
        {
            return myDb.Examinations.Where(p => p.ExaminationNumber == examinatioNumber).FirstOrDefault();
        }

        public void ExaminationInsert(string examinationNumber, int labId, int status)
        {
            Examination ex=new Examination{LabId = labId , ExaminationNumber =  examinationNumber , Status = status, CreatedDate = DateTime.Now};
            myDb.Examinations.AddObject(ex);
            myDb.SaveChanges();
        }

        public void UpdateLabAmount(int labId)
        {
            LabClient lab = myDb.LabClients.Where(p => p.LabId == labId).FirstOrDefault();
            if (lab != null)
            {
                lab.Amount--;
            }
            myDb.SaveChanges();
        }
    }
}
