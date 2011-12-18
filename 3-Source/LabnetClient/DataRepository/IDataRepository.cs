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

        List<Partner> GetPartners();
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
        List<Panel> GetPanels();
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

        #region Patient
            string GetPatientNumber();
            int PatientInsert(Patient patient);
            int PatientItemInsert(PatientItem patient);
            List<PatientsGets_Result> GetPatients(int? PatientId, string FirstName, string Phone, string Email, string IndentifierNumber, string Address, int? PartnerId, int? OrderNumber, DateTime? ReceivedDate);
            Patient GetPatient(int Id, DateTime receivedDate,int orderNumber);
            Patient GetPatientNumber(int Id);
            List<VMPatientTest> GetPatientTests(int Id, DateTime receivedDate, int orderNumber);
            void PatientUpdate(int patientId, Patient patient);
            void PatientItemUpdate(int patientId, PatientItem patientItem);
        #endregion
        #region Analysis
            void AnalysisInsert(Analysis analysis);
        #endregion

        #region LabExamination
            string GetExaminationNumber();
            int LabExaminationInsert(LabExamination labExamination);
            int GetLabExaminationOrderNumber();
            VMLabExamination GetLabExamination(int Id);
            void LabExaminationUpdate(int patientId,DateTime receivedDate, int orderNumber, LabExamination labExamination);
        #endregion
    }
}
