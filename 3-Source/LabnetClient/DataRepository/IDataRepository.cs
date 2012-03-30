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

        List<VMTestSectionListItem> GetPartnerTestSection(int id);

        List<Partner> GetPartnerByName(string name);

        bool IsValidPartner(string name);

        List<Partner> GetPartners();

        object GetPartnerNameByName(string name, string searchType);
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
        List<Test> GetTests();
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
        object GetPanelNameByName(string name, string searchType);
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
        object GetDoctorNameByName(string name, string searchType);
        #endregion

        #region TestSection
        object GetTestSectionByName(string name, string searchType);
        TestSection GetTestSection(int testSectionId);
        List<TestSection> GetTestSections();
        void TestSectionInsert(TestSection ts);
        void TestSectionDelete(int testSectionId);
        bool IsValidTestSection(string tsName,int? testSectionId);
        List<VMTestListItem> GetTestsOfTestSection(int Id);
        void TestSectionUpdate(TestSection testSection);
        object GetTestSectionByNameForPanel(string name, string searchType);
        #endregion

        #region Patient
            string GetPatientNumber();
            int PatientInsert(Patient patient);
            int PatientItemInsert(PatientItem patient);
            List<PatientsGets_Result> GetPatients(int? PatientId, string FirstName, string Phone, string Email, string IndentifierNumber, string Address, int? PartnerId, int? OrderNumber, DateTime? ReceivedDate);
            Patient GetPatient(int labExaminationId);
            Patient GetPatient(DateTime ReceivedDate, int OrderNumber);
            //Patient GetPatient(int id);
            Patient GetPatientNumber(int Id);
            List<VMPatientTest> GetPatientTests(int patientId, int labExaminationId);
            void PatientUpdate(int patientId, Patient patient);
            PatientItem PatientItemUpdate(int patientId, PatientItem patientItem);
            List<VMTestResult> GetPatientTestResults(int orderNumber, DateTime receivedDate);
            List<VMTestResult> GetPatientTestResults(int labExaminationId);
        #endregion

        #region Analysis
            void AnalysisInsert(Analysis analysis);
            void AnalysisApproved(int analysisId, int staffId); 
            void AnalysisDelete(int analysisId);
        #endregion

        #region LabExamination
            string GetExaminationNumber();
            int LabExaminationInsert(LabExamination labExamination);
            int GetLabExaminationOrderNumber();
            VMLabExamination GetLabExamination(int LabExaminationId);
            VMLabExamination GetLabExamination(int OrderNumber, DateTime ReceivedDate);
            VMLabExamination GetLabExamination(string examinationNumber);
            void LabExaminationUpdate(int patientId,DateTime receivedDate, int orderNumber, LabExamination labExamination);
        #endregion

        #region Result
            void ResultInsert(int analysisId, string result, int staffId);

            void ResultUpdate(int analysisId,int resultId, string result, int staffId);
        #endregion

        #region Report
            List<Report_PatientResult> ReportData_PatientResult(int labExaminationId);
            List<report_TestResultNoteBook> ReportData_TestResultNoteBook(DateTime startDate, DateTime endDate);
        #endregion

        #region LabUser
            LabUser GetLabUser(string UserName, string Password);
        #endregion

        #region TestSectionCommission
        TestSectionCommission GetTestSectionCommission(int id);
        void TestSectionCommissionInsert(TestSectionCommission test);
        void TestSectionCommissionUpdate(int id, TestSectionCommission test);
        void TestSectionCommissionDelete(int id);
        #endregion


    }
}
