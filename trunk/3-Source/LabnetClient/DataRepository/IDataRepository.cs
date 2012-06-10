using System;
using System.Collections.Generic;
using DomainModel;
using DomainModel.Report;

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

        List<Partner> GetPartnerByName(string name, bool isActive);

        bool IsValidPartner(string name);

        List<Partner> GetPartners();

        object GetPartnerNameByName(string name, string searchType);

        Partner GetPartnerByLabExamination(int examinationNumber);

        string CreateLabConnectionCode(int clientLabId,int labId);

        void RemoveLabConnection(int clientLabId, int labId);
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
        List<Test> GetTestsHaveRealCost();
        Test GetTest(int testId);
        void TestInsert(Test test);
        void TestUpdate(int id, Test test);
        void TestDelete(int testId);
        List<Test> GetTestByTestSectionId(int testSectionId);
        bool IsValidTest(string testName);
        List<SearchTest_Result> TestSearch(string testName, string testSectionName, string panelName, bool isActive);
        object GetTestByName(string name, string searchType);
        object GetTestByNameForPanel(string name, string searchType);
        object GetTestHaveCostNotDependenceTestSection();
        #endregion

        #region Panel
        Panel GetPanel(int panelId);
        void PanelInsert(Panel panel);
        void PanelUpdate(int id, Panel panel);
        void PanelDelete(int panelId);
        List<Panel> GetAllPanel();
        bool IsValidPanel(string name);
        List<Panel> GetPanelByName(string name, bool isActive);
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
        List<Doctor> GetDoctorByNameForSearch(string name, bool IsActive);
        object GetDoctorNameByName(string name, string searchType);
        string CreateConnectionCode(int doctorId,int labId);
        void RemoveConnection(int doctorId,int labId);
        #endregion

        #region TestSection
        object GetTestSectionByName(string name, string searchType);
        TestSection GetTestSection(int testSectionId);
        List<TestSection> GetTestSections();
        List<TestSection> GetAllTestSections();
        void TestSectionInsert(TestSection ts);
        void TestSectionDelete(int testSectionId);
        bool IsValidTestSection(string tsName,int? testSectionId);
        List<VMTestListItem> GetTestsOfTestSection(int id);
        void TestSectionUpdate(TestSection testSection);
        object GetTestSectionByNameForPanel(string name, string searchType);
        List<TestSection> GetTestSectionByNameAndStatus(string name, bool isActive);
        #endregion

        #region Patient
            int PatientInsert(Patient patient);
            int PatientItemInsert(PatientItem patient);
            List<PatientsGets_Result> GetPatients(int? patientId, string firstName, string phone, string email, string indentifierNumber, string address, int? partnerId, int? orderNumber, DateTime? receivedDate);
            Patient GetPatientByLabExaminationId(int labExaminationId);
            Patient GetPatientByExaminationDateAndOrderNumber(DateTime receivedDate, int orderNumber);
            //Patient GetPatientByLabExaminationId(int id);
            Patient GetPatientNumber(int id);
            List<VMPatientTest> GetPatientTests(int patientId, int labExaminationId);
            void PatientUpdate(int patientId, Patient patient);
            PatientItem PatientItemUpdate(int patientId, PatientItem patientItem);
            List<VMTestResult> GetPatientTestResults(int orderNumber, DateTime receivedDate);
            List<VMTestResult> GetPatientTestResults(int labExaminationId);
            Patient GetPatientById(int patientId);
        #endregion

        #region Analysis
            void AnalysisInsert(Analysis analysis);
            void AnalysisApproved(int analysisId, int staffId); 
            void AnalysisDelete(int analysisId);
        #endregion

        #region LabExamination
            int LabExaminationInsert(LabExamination labExamination);
            int GetLabExaminationOrderNumber();
            VMLabExamination GetLabExamination(int labExaminationId);
            VMLabExamination GetLabExaminationById(int id);
            VMLabExamination GetLabExamination(int orderNumber, DateTime receivedDate);
            VMLabExamination GetLabExamination(string examinationNumber);
            void LabExaminationUpdate(int labId,int patientId, DateTime receivedDate, int orderNumber, LabExamination labExamination);
            string GetUniqueExaminationNumber(int length,int labId);
            string GetUniquePatientNumber(int length, int labId);
            void LabExaminationUpdateStatus(int labExaminationId,int status);
        #endregion

        #region Result
            void ResultInsert(int analysisId, string result, int staffId);

            void ResultUpdate(int analysisId,int resultId, string result, int staffId);
        #endregion

        #region Report
            List<Report_PatientResult> ReportDataPatientResult(int labExaminationId);
            List<Report_TestResultNoteBook> ReportDataTestResultNoteBook(DateTime startDate, DateTime endDate);
            List<Report_BaoCaoTaiChinh> ReportDataBaoCaoTaiChinh(DateTime startDate, DateTime endDate, int partnerId,string partnerType);
        #endregion

        #region LabUser
            LabUser GetLabUser(string userName, string password);
        #endregion

        #region TestSectionCommission
        TestSectionCommission GetTestSectionCommission(int id);
        void TestSectionCommissionInsert(TestSectionCommission test);
        void TestSectionCommissionUpdate(int id, TestSectionCommission test);
        void TestSectionCommissionDelete(int id);
        #endregion

        #region Instrument
        List<SearchInstrumentResult_Result> InstrumentResultSearch(DateTime? receivedDate, string orderNumber, int? instrumentId);
        List<InstrumentResult> GetAllValidInstrumentResult();
        List<InstrumentResult> GetAllValidInstrumentResultByCondition(DateTime? receivedDate, string orderNumber, int? instrumentId);
        List<InstrumentResult> GetAllInstrumentResultByCondition(DateTime? receivedDate, string orderNumber, int? instrumentId);
        List<Instrument> GetInstruments();
        void InsertToResult(int? orderNumber, DateTime? receivedDate, int? testId, string value, int? instrumentResultId);
        void UpdateSID(DateTime? receivedDate, string oldOrderNumber, int newOrderNumber, int? instrumentId);
        #endregion

        #region Service
        string SetupDoctorConnection(string connectionCode, int serverDoctorId, int clientDoctorId, string doctorConnectName);
        string SetupLabConnection(string connectionCode, int serverLabId, int clientLabId, string labConnectName);
        #endregion

    }
}
