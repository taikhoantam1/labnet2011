using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel;

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

        /// <summary>
        /// Gets list test of partner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<VMTestListItem> GetPartnerTest(int id);
        #endregion

        #region Test
        Test GetTest(int testId);
        void TestInsert(Test test);
        void TestUpdate(int id, Test test);
        void TestDelete(int testId);
        List<Test> GetTestByTestSectionId(int testSectionId);
        bool IsValidTest(string testName);
        List<SearchTest_Result> TestSearch(string testName, string testSectionName, string panelName);
        object GetTestByName(string name, string searchType);
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

        #region TestSection
        object GetTestSectionByName(string name, string searchType);
        TestSection GetTestSection(int testSectionId);
        #endregion
    }
}
