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


        List<Partner> GetPartnerByName(string name);

        bool IsValidPartner(string name);
        #endregion

        #region PartnerCost
        PartnerCost GetPartnerCost(int partnerCostId);
        void PartnerCostInsert(PartnerCost partnerCost);
        void PartnerCostUpdate(int id, PartnerCost partnerCost);
        void PartnerCostDelete(int id);
        void PartnerCostDeleteByPartnerId(int id);
        List<PartnerCost> GetPartnerCostByPartnerId(int id);
        PartnerCost GetPartnerCostByTestId(int testId, int partnerId);
        bool IsPartnerCostExist(int testId, int partnerId);
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
        object GetTestByNameForPanel(string name, string searchType);
        #endregion

        #region Panel
        Panel GetPanel(int panelId);
        void PanelInsert(Panel panel);
        void PanelUpdate(int id, Panel panel);
        void PanelDelete(int panelId);
        List<Panel> GetAllPanel();
        bool IsValidPanel(string name);
        List<Panel> GetPanelByName(string name);
        #endregion

        #region PanelItem
        PanelItem GetPanelItem(int panelItemId);
        void PanelItemInsert(PanelItem panelItem);
        void PanelItemUpdate(int id, PanelItem panelItem);
        void PanelItemDelete(int panelItemId);
        List<PanelItem> GetPanelItemByPanelId(int panelId);
        List<PanelItem> GetPanelItemByTestId(int testId);
        PanelItem GetPanelItemByTestIdAndPanelId(int testId, int panelId);
        bool IsPanelItemExist(int testId);
        List<VMTestListItem> GetPanelTest(int id);
        #endregion

        #region Doctor
        Doctor GetDoctor(int doctorId);
        void DoctorInsert(Doctor doctor);
        void DoctorUpdate(int id, Doctor doctor);
        void DoctorDelete(int doctorId);
        bool IsValidDoctor(string name);
        List<Doctor> GetDoctorByName(string name);
        #endregion

        #region TestSection
        object GetTestSectionByName(string name, string searchType);
        TestSection GetTestSection(int testSectionId);
        #endregion
    }
}
