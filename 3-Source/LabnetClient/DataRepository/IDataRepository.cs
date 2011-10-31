using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataRepository
{
    public interface IDataRepository
    {
        #region Partner
        /// <summary>
        /// Insert partner
        /// </summary>
        /// <param name="partner">partner info</param>
        void PartnerInsert(Partner partner);

        /// <summary>
        /// Get partner info by partner id
        /// </summary>
        /// <param name="id">partner id</param>
        /// <returns></returns>
        Partner GetPartnerById(int id);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="partner"></param>
        void PartnerUpdate(int id, Partner partner);
        #endregion

        #region Test
        Test GetTest(int testId);
        void InsertTest(Test test);
        void UpdateTest(int id, Test test);
        void DeleteTest(int testId);
        List<Test> GetTestByTestSectionId(int testSectionId);
        #endregion

        #region Panel
        Panel GetPanel(int panelId);
        void InsertPanel(Panel panel);
        void UpdatePanel(int id, Panel panel);
        void DeletePanel(int panelId);
        List<Panel> GetAllPanel();
        #endregion

        #region PanelItem
        PanelItem GetPanelItem(int panelItemId);
        void InsertPanelItem(PanelItem panelItem);
        void UpdatePanelItem(int id, PanelItem panelItem);
        void DeletePanelItem(int panelItemId);
        List<PanelItem> GetPanelItemByPanelId(int panelId);
        List<PanelItem> GetPanelItemByTestId(int testId);
        #endregion

        #region Doctor
        Doctor GetDoctor(int doctorId);
        void InsertDoctor(Doctor doctor);
        void UpdateDoctor(int id, Doctor doctor);
        void DeleteDoctor(int doctorId);
        #endregion
    }
}
