using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataRepository
{
    public interface IRepository
    {
        #region LabAccount
            LabnetAccount GetAccount(string UserName);
        #endregion
        #region Examination
            void ExaminationInsert(string examinationNumber, int labId, int status);
            Examination GetExamination(string examinatioNumber);
        #endregion
        #region LabClient
            void UpdateLabAmount(int labId);
        #endregion

            
    }
}
