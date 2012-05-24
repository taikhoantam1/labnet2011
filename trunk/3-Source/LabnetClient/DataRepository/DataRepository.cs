using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LibraryFuntion;
using System.Data.Objects.SqlClient;
using DomainModel;
using System.Data.Objects;
using DomainModel.Constant;
using DomainModel.Report;
namespace DataRepository
{
    public class Repository : IDataRepository
    {
        private LabManager_ClientEntities myDb;
        private IServerConnector connector;

        public LabManager_ClientEntities Context
        {
            get { return myDb; }
        }

        public IServerConnector Connector
        {
            get { return connector; }
            set { connector = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Repository()
        {
            myDb = new LabManager_ClientEntities();
            connector = new ServerConnector();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserName(int userId)
        {
            return "";
        }
        #region Partner
        public void PartnerUpdate(int id, Partner updateVaule)
        {

            Partner oldPartner = GetPartnerById(id);
            oldPartner.Address = updateVaule.Address;
            oldPartner.Email = updateVaule.Address;
            oldPartner.IsActive = updateVaule.IsActive;
            oldPartner.LabId = updateVaule.LabId;
            oldPartner.Name = updateVaule.Name;
            oldPartner.Owner = updateVaule.Owner;
            oldPartner.Phone = updateVaule.Phone;
            oldPartner.Fax = updateVaule.Fax;
            myDb.SaveChanges();

        }

        public void PartnerInsert(Partner partner)
        {
            myDb.Partners.AddObject(partner);
            myDb.SaveChanges();
        }
        public Partner GetPartnerById(int id)
        {
            Partner partner = myDb.Partners.Where(p => p.Id == id).FirstOrDefault();
            return partner;
        }
        /// <summary>
        /// Gets list test of partner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<VMTestListItem> GetPartnerTest(int id)
        {
            Partner partner = GetPartnerById(id);
            List<VMTestListItem> listTest = partner.PartnerCosts.Where(p => p.IsActive == true).Select(p => new VMTestListItem
            {
                TestName = p.Test.Name + "-" + p.Test.Description,
                TestId = p.TestId,
                Cost = p.Cost,
                TestSectionName = p.Test.TestSection.Name,
                IsEnable = true,
            }).ToList();

            return listTest;
        }

        public List<VMTestSectionListItem> GetPartnerTestSection(int id)
        {
            Partner partner = GetPartnerById(id);
            List<VMTestSectionListItem> listTest = partner.TestSectionCommissions.Where(p => p.IsActive == true).Select(p => new VMTestSectionListItem
            {
                TestSectionName = p.TestSection.Name,
                TestSectionId = p.TestSectionId,
                TestSectionCost = p.Cost,
                IsEnableTestSection = true,
            }).ToList();

            return listTest;
        }

        public List<Partner> GetPartnerByName(string name)
        {
            List<Partner> lstPartner = (from _partner in myDb.Partners where _partner.IsActive && (string.IsNullOrEmpty(name) || _partner.Name.ToUpper().Contains(name.ToUpper())) select _partner).ToList();
            return lstPartner;
        }
        public List<Partner> GetPartners()
        {
            List<Partner> lstPartner = (from _partner in myDb.Partners where _partner.IsActive select _partner).ToList();
            return lstPartner;
        }
        public bool IsValidPartner(string name)
        {
            bool isValid = true;
            Partner partner = myDb.Partners.SingleOrDefault(u => u.Name == name);
            if (partner != null)
            {
                isValid = false;
            }
            return isValid;
        }

        public object GetPartnerNameByName(string name, string searchType)
        {
            List<SearchPartnerByName_Result> lstPartner = myDb.SearchPartnerByName(name, searchType).ToList();
            return lstPartner.Select(p => new { Label = p.Name, Value = p.Name });
        }

        public Partner GetPartnerByLabExamination(int examinationNumber)
        {
            Partner partner = (from _partner in myDb.Partners
                               join
                                   _labExamination in myDb.LabExaminations on _partner.Id equals _labExamination.PartnerId
                               where _labExamination.Id == examinationNumber
                               select _partner).FirstOrDefault();
            return partner;
        }
        #endregion

        #region PartnerCost
        public PartnerCost GetPartnerCost(int partnerCostId)
        {
            PartnerCost partnerCost = (from _partnerCost in myDb.PartnerCosts where _partnerCost.Id == partnerCostId select _partnerCost).First();
            myDb.SaveChanges();
            return partnerCost;
        }

        public void PartnerCostInsert(PartnerCost partnerCost)
        {
            myDb.PartnerCosts.AddObject(partnerCost);
            myDb.SaveChanges();
        }

        public void PartnerCostUpdate(int id, PartnerCost partnerCost)
        {
            PartnerCost currentPartnerCost = (from _partnerCost in myDb.PartnerCosts where _partnerCost.Id == id select _partnerCost).First();
            currentPartnerCost.TestId = partnerCost.TestId;
            currentPartnerCost.PartnerId = partnerCost.PartnerId;
            currentPartnerCost.Cost = partnerCost.Cost;
            currentPartnerCost.Description = partnerCost.Description;
            currentPartnerCost.IsActive = partnerCost.IsActive;
            currentPartnerCost.LastUpdated = partnerCost.LastUpdated;

            myDb.SaveChanges();
        }

        public void PartnerCostDelete(int id)
        {
            PartnerCost partnerCost = GetPartnerCost(id);
            partnerCost.IsActive = false;

            myDb.SaveChanges();
        }

        public void PartnerCostDeleteByPartnerId(int id)
        {
            List<PartnerCost> lstPartnerCost = (from _partnerCost in myDb.PartnerCosts where _partnerCost.PartnerId == id select _partnerCost).ToList();
            foreach (PartnerCost p in lstPartnerCost)
            {
                p.IsActive = false;
            }
            myDb.SaveChanges();
        }

        public List<PartnerCost> GetPartnerCostByPartnerId(int id)
        {
            List<PartnerCost> lstPartnerCost = (from _partnerCost in myDb.PartnerCosts where _partnerCost.PartnerId == id select _partnerCost).ToList();

            return lstPartnerCost;
        }

        public PartnerCost GetPartnerCostByTestId(int testId, int partnerId)
        {
            PartnerCost partnerCost = (from _partnerCost in myDb.PartnerCosts
                                       where _partnerCost.TestId == testId && _partnerCost.PartnerId == partnerId && _partnerCost.IsActive == true
                                       select _partnerCost).First();
            return partnerCost;
        }

        public bool IsPartnerCostExist(int testId, int partnerId)
        {
            bool isExist = false;
            PartnerCost partnerCost = new PartnerCost();
            partnerCost = (from _partnerCost in myDb.PartnerCosts
                           where _partnerCost.TestId == testId && _partnerCost.PartnerId == partnerId && _partnerCost.IsActive == true
                           select _partnerCost).FirstOrDefault();
            if (partnerCost != null)
            {
                isExist = true;
            }

            return isExist;
        }
        #endregion

        #region Test
        public List<Test> GetTests()
        {
            List<Test> lstTests = (from _test in myDb.Tests
                                   where _test.IsActive == true
                                   select _test).ToList();
            return lstTests;
        }
        /// <summary>
        /// Những test không thuộc TestSection hoặc thuộc test section nhưng TestSection đó có UseCostForAssociateTest = false
        /// </summary>
        /// <returns></returns>
        public List<Test> GetTestsHaveRealCost()
        {
            List<Test> lstTests = (from _test in myDb.Tests
                                   where _test.IsActive == true && (_test.TestSection == null || !_test.TestSection.UseCostForAssociateTest)
                                   select _test).ToList();
            return lstTests;
        }


        public Test GetTest(int testId)
        {
            try
            {
                Test test = (from _test in myDb.Tests where _test.Id == testId select _test).First();
                myDb.SaveChanges();
                return test;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void TestInsert(Test test)
        {
            myDb.Tests.AddObject(test);
            myDb.SaveChanges();
        }

        public void TestUpdate(int id, Test test)
        {
            Test currentTest = (from _test in myDb.Tests where _test.Id == id select _test).First();
            currentTest.Name = test.Name;
            currentTest.Description = test.Description;
            currentTest.LowIndex = test.LowIndex;
            currentTest.HighIndex = test.HighIndex;
            currentTest.Unit = test.Unit;
            currentTest.Range = test.Range;
            currentTest.DepartmentId = test.DepartmentId;
            currentTest.CreatedBy = test.CreatedBy;
            currentTest.SortOrder = test.SortOrder;
            currentTest.TestSectionId = test.TestSectionId;
            currentTest.IsActive = test.IsActive;
            currentTest.LastUpdated = test.LastUpdated;
            currentTest.Cost = test.Cost;
            currentTest.IsBold = test.IsBold;

            myDb.SaveChanges();
        }

        public void TestDelete(int testId)
        {
            Test test = GetTest(testId);
            test.IsActive = false;

            myDb.SaveChanges();
        }

        public List<Test> GetTestByTestSectionId(int testSectionId)
        {
            List<Test> lstTests = (from _test in myDb.Tests
                                   where (_test.IsActive == true && _test.TestSectionId == testSectionId)
                                   select _test).ToList();
            return lstTests;
        }

        public bool IsValidTest(string testName)
        {
            bool isValid = true;
            Test testWithSameName = myDb.Tests.SingleOrDefault(u => u.Name == testName);
            if (testWithSameName != null)
            {
                isValid = false;
            }
            return isValid;
        }

        public List<SearchTest_Result> TestSearch(string testName, string testSectionName, string panelName)
        {
            List<SearchTest_Result> lstTestSearch = new List<SearchTest_Result>();
            lstTestSearch = myDb.SearchTest(testName, testSectionName, panelName).ToList();
            return lstTestSearch;
        }

        public object GetTestByName(string name, string searchType)
        {
            List<SearchTestByName_Result> lstTest = myDb.SearchTestByName(name, searchType).ToList();
            return lstTest.Select(p => new { Label = p.Name, Value = p.Id, Tag = p.Cost });
        }

        public object GetTestByNameForPanel(string name, string searchType)
        {
            List<SearchTestByNameForPanel_Result> lstTest = myDb.SearchTestByNameForPanel(name, searchType).ToList();
            return lstTest.Select(p => new { Label = p.Name, Value = p.Id, Tag = p.TestSectionName + "," + p.Cost });
        }

        public object GetTestHaveCostNotDependenceTestSection()
        {
            var lstTest = myDb.Tests.Where(p => (p.TestSection == null || !p.TestSection.UseCostForAssociateTest))
                                    .ToList()
                                    .Select(p => new
                                    {
                                        Label = p.Name,
                                        Value = p.Id,
                                        Tag = (p.TestSection != null ? p.TestSection.Name : "") + "," + p.Cost.ToString()
                                    });
            return lstTest;
        }
        #endregion Test

        #region Panel
        public Panel GetPanel(int panelId)
        {
            try
            {
                Panel panel = (from _panel in myDb.Panels where (_panel.Id == panelId) select _panel).First();
                myDb.SaveChanges();
                return panel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PanelInsert(Panel panel)
        {
            myDb.Panels.AddObject(panel);
            myDb.SaveChanges();
        }

        public void PanelUpdate(int id, Panel panel)
        {
            Panel currentPanel = (from _panel in myDb.Panels where _panel.Id == id select _panel).First();

            currentPanel.Name = panel.Name;
            currentPanel.Description = panel.Description;
            currentPanel.IsActive = panel.IsActive;
            currentPanel.LastUpdated = panel.LastUpdated;

            myDb.SaveChanges();
        }

        public void PanelDelete(int panelId)
        {
            Panel panel = GetPanel(panelId);
            panel.IsActive = false;

            myDb.SaveChanges();
        }

        public List<Panel> GetAllPanel()
        {
            List<Panel> lstPanels = (from _panel in myDb.Panels where _panel.IsActive == true select _panel).ToList();
            return lstPanels;
        }

        public bool IsValidPanel(string name)
        {
            bool isValid = true;
            Panel panel = myDb.Panels.SingleOrDefault(u => u.Name == name);
            if (panel != null)
            {
                isValid = false;
            }
            return isValid;
        }
        public object GetPanelNameByName(string name, string searchType)
        {
            List<SearchPanelByName_Result> lstPanel = myDb.SearchPanelByName(name, searchType).ToList();
            return lstPanel.Select(p => new { Label = p.Name, Value = p.Name });
        }
        #endregion Panel

        #region PanelItem
        public PanelItem GetPanelItem(int panelItemId)
        {
            try
            {
                PanelItem panelItem = (from _panelItem in myDb.PanelItems where _panelItem.Id == panelItemId select _panelItem).First();
                myDb.SaveChanges();
                return panelItem;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PanelItemInsert(PanelItem panelItem)
        {
            myDb.PanelItems.AddObject(panelItem);
            myDb.SaveChanges();
        }

        public void PanelItemUpdate(int id, PanelItem panelItem)
        {
            PanelItem currentPanelItem = (from _panelItem in myDb.PanelItems where _panelItem.Id == id select _panelItem).First();

            currentPanelItem.PanelId = panelItem.PanelId;
            currentPanelItem.TestId = panelItem.TestId;
            currentPanelItem.TestName = panelItem.TestName;
            currentPanelItem.SortOrder = panelItem.SortOrder;
            currentPanelItem.IsActive = panelItem.IsActive;
            currentPanelItem.LastUpdated = panelItem.LastUpdated;

            myDb.SaveChanges();
        }

        public void PanelItemDelete(int panelItemId)
        {
            PanelItem panelItem = GetPanelItem(panelItemId);
            panelItem.IsActive = false;

            myDb.SaveChanges();
        }

        public List<PanelItem> GetPanelItemByPanelId(int panelId)
        {
            List<PanelItem> lstPanelItems = (from _panelItem in myDb.PanelItems where _panelItem.PanelId == panelId select _panelItem).ToList();

            return lstPanelItems;
        }

        public List<PanelItem> GetPanelItemByTestId(int testId)
        {
            List<PanelItem> lstPanelItems = (from _panelItem in myDb.PanelItems where _panelItem.TestId == testId select _panelItem).ToList();

            return lstPanelItems;
        }

        public List<Panel> GetPanelByName(string name)
        {
            List<Panel> lstPanel = (from _panel in myDb.Panels where string.IsNullOrEmpty(name) || _panel.Name.Contains(name) select _panel).ToList();
            return lstPanel;
        }

        public PanelItem GetPanelItemByTestIdAndPanelId(int testId, int panelId)
        {
            PanelItem panelItem = (from _item in myDb.PanelItems
                                   where _item.PanelId == panelId && _item.TestId == testId
                                   select _item).FirstOrDefault();

            return panelItem;
        }

        public bool IsPanelItemExist(int testId)
        {
            bool isExist = false;
            PanelItem item = new PanelItem();
            item = (from _panelItem in myDb.PanelItems
                    where _panelItem.TestId == testId && _panelItem.IsActive == true
                    select _panelItem).FirstOrDefault();
            if (item != null)
            {
                isExist = true;
            }

            return isExist;
        }

        public List<VMTestListItem> GetPanelTest(int id)
        {
            Panel panel = GetPanel(id);
            List<VMTestListItem> listTest = panel.PanelItems.Where(p => p.IsActive == true).Select(p => new VMTestListItem
            {
                TestName = p.Test.Name,
                TestId = p.TestId,
                TestSectionName = p.Test.TestSection.Name,
                IsEnable = p.IsActive,
                Cost = p.Test.Cost,

            }).ToList();
            return listTest;
        }
        public List<Panel> GetPanels()
        {
            return (from _panel in myDb.Panels where _panel.IsActive select _panel).ToList();
        }
        #endregion PanelItem

        #region Doctor
        public Doctor GetDoctor(int doctorId)
        {
            try
            {
                Doctor doctor = (from _doctor in myDb.Doctors where _doctor.Id == doctorId select _doctor).First();
                myDb.SaveChanges();
                return doctor;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DoctorInsert(Doctor doctor)
        {
            myDb.Doctors.AddObject(doctor);
            myDb.SaveChanges();
        }

        public void DoctorUpdate(int id, Doctor doctor)
        {
            Doctor currentDoctor = (from _doctor in myDb.Doctors where _doctor.Id == id select _doctor).First();
            currentDoctor.Name = doctor.Name;
            currentDoctor.Address = doctor.Address;
            currentDoctor.Phone = doctor.Phone;
            currentDoctor.IsActive = doctor.IsActive;
            currentDoctor.Email = doctor.Email;
            currentDoctor.Commission = doctor.Commission;
            currentDoctor.BankAccountNumber = doctor.BankAccountNumber;
            currentDoctor.Other = doctor.Other;
            currentDoctor.ConnectionCode = doctor.ConnectionCode;
            myDb.SaveChanges();
        }
        public string AddLabIdToUniqueString(string code,int labId)
        {
            if (labId > 9)
            {
                code = labId.ToString() + code;
            }
            else
            {
                code = "0" + labId.ToString() + code;
            }
            return code;
        }
        public string CreateConnectionCode(int doctorId,int labId)
        {
            string connectionCode = myDb.GenerateConnectionCode((int)ConstantNumber.ConnetionCodeLength).FirstOrDefault();
            connectionCode = AddLabIdToUniqueString(connectionCode, labId);
            if (doctorId != 0)
            {
                Doctor currentDoctor = (from _doctor in myDb.Doctors where _doctor.Id == doctorId select _doctor).First();
                currentDoctor.ConnectionCode = connectionCode;
                try
                {
                    connector.SubmitConnectionCodeToServer(doctorId, labId, connectionCode);
                    currentDoctor.UpdatedOnServer = true;
                    
                }
                catch (Exception ex)
                {
                    currentDoctor.UpdatedOnServer = false;
                }
                myDb.SaveChanges();
            }
            return connectionCode;
        }

        public void RemoveConnection(int doctorId,int labId)
        {
            Doctor currentDoctor = (from _doctor in myDb.Doctors where _doctor.Id == doctorId select _doctor).First();
            if (currentDoctor.IsConnected == true)
            {
                currentDoctor.IsConnected = false;
                currentDoctor.ConnectionCode = "";
                currentDoctor.ServerDoctorId = null;
                currentDoctor.UpdatedOnServer = false;
                try
                {
                    bool isSuccess = Connector.RemoveDoctorConnect(currentDoctor.ServerDoctorId.Value, doctorId, labId, currentDoctor.ConnectionCode);
                    if (isSuccess)
                        currentDoctor.UpdatedOnServer = true;
                }
                catch (Exception ex)
                { 
                }
                myDb.SaveChanges();
            }
        }
        public void DoctorDelete(int doctorId)
        {
            Doctor doctor = GetDoctor(doctorId);
            doctor.IsActive = false;

            myDb.SaveChanges();
        }

        public bool IsValidDoctor(string name)
        {
            bool isValid = true;
            Doctor doctor = myDb.Doctors.SingleOrDefault(u => u.Name == name);
            if (doctor != null)
            {
                isValid = false;
            }
            return isValid;
        }

        public List<Doctor> GetDoctorByName(string name)
        {
            List<Doctor> lstDoctor = (from _doctor in myDb.Doctors where string.IsNullOrEmpty(name) || _doctor.Name.Contains(name) select _doctor).ToList();
            return lstDoctor;
        }

        public object GetDoctorNameByName(string name, string searchType)
        {
            List<SearchDoctorByName_Result> lstDoctor = myDb.SearchDoctorByName(name, searchType).ToList();
            return lstDoctor.Select(p => new { Label = p.Name, Value = p.Name });
        }

        public string GetUniqueExaminationNumber(int length,int labId)
        {
            string examinationNumber = myDb.GenerateLabExaminationNumber(5).FirstOrDefault();
            examinationNumber = AddLabIdToUniqueString(examinationNumber, labId);
            return examinationNumber;
        }
        public string GetUniquePatientNumber(int length, int labId)
        {
            string examinationNumber = myDb.GenerateLabExaminationNumber(5).FirstOrDefault();
            examinationNumber = AddLabIdToUniqueString(examinationNumber, labId);
            return examinationNumber;
        }
        #endregion

        #region TestSection


        public object GetTestSectionByName(string name, string searchType)
        {
            List<SearchTestSection_Result> lstTestSection = myDb.SearchTestSection(name, searchType).ToList();
            return lstTestSection.Select(p => new { Label = p.Name, Value = p.Id, Tag = p.UseCostForAssociateTest });
        }

        public TestSection GetTestSection(int testSectionId)
        {
            TestSection testSection = (from _testSection in myDb.TestSections where _testSection.Id == testSectionId select _testSection).First();
            myDb.SaveChanges();
            return testSection;
        }

        public List<TestSection> GetTestSections()
        {
            List<TestSection> lstTestSections = (from _testSection in myDb.TestSections
                                                 where _testSection.IsActive == true
                                                 select _testSection).ToList();
            return lstTestSections;
        }

        public void TestSectionInsert(TestSection ts)
        {
            myDb.TestSections.AddObject(ts);
            myDb.SaveChanges();
        }


        public void TestSectionDelete(int testSectionId)
        {
            TestSection ts = GetTestSection(testSectionId);
            ts.IsActive = false;

            myDb.SaveChanges();
        }

        public bool IsValidTestSection(string tsName, int? testSectionId)
        {
            bool isValid = true;
            TestSection tsWithSameName = myDb.TestSections.SingleOrDefault(u => u.Name == tsName);
            if (tsWithSameName != null)
            {
                if ((testSectionId == null)
                || (testSectionId != null && testSectionId != tsWithSameName.Id))
                    isValid = false;
            }
            return isValid;
        }

        public List<VMTestListItem> GetTestsOfTestSection(int Id)
        {
            TestSection testSection = GetTestSection(Id);
            List<VMTestListItem> listTest = testSection.Tests.Where(p => p.IsActive == true).Select(p => new VMTestListItem
            {
                TestName = p.Name,
                TestId = p.Id,
                TestSectionName = p.TestSection.Name,
                IsEnable = p.IsActive,
                Cost = p.Cost,
            }).ToList();
            return listTest;
        }
        public void TestSectionUpdate(TestSection testSectionToUpdate)
        {
            TestSection testSection = (from _testSection in myDb.TestSections
                                       where _testSection.Id == testSectionToUpdate.Id
                                       select _testSection).First();
            testSection.IsActive = testSectionToUpdate.IsActive;
            testSection.Name = testSectionToUpdate.Name;
            testSection.SortOrder = testSectionToUpdate.SortOrder;
            testSection.Cost = testSectionToUpdate.Cost;
            testSection.Description = testSection.Description;
            testSection.UseCostForAssociateTest = testSectionToUpdate.UseCostForAssociateTest;
            myDb.SaveChanges();
        }

        public object GetTestSectionByNameForPanel(string name, string searchType)
        {
            List<SearchTestSectionByNameForPanel_Result> lstTestSection = myDb.SearchTestSectionByNameForPanel(name, searchType).ToList();
            return lstTestSection.Select(p => new { Label = p.Name, Value = p.Id, Tag = p.Cost });
        }
        #endregion

        #region Patient

        public int PatientInsert(Patient patient)
        {
            myDb.Patients.AddObject(patient);
            myDb.SaveChanges();
            return patient.Id;
        }

        public int PatientItemInsert(PatientItem patientItem)
        {
            myDb.PatientItems.AddObject(patientItem);
            myDb.SaveChanges();
            return patientItem.Id;
        }

        public List<PatientsGets_Result> GetPatients(int? PatientId, string FirstName, string Phone, string Email, string IndentifierNumber, string Address, int? PartnerId, int? OrderNumber, DateTime? ReceivedDate)
        {
            List<PatientsGets_Result> listPatient = myDb.PatientsGets(PatientId, FirstName, Phone, Email, IndentifierNumber, Address, PartnerId, OrderNumber, ReceivedDate).ToList();
            return listPatient;
        }

        public Patient GetPatientNumber(int Id)
        {
            Patient patient = myDb.Patients.Where(p => p.Id == Id).FirstOrDefault();
            return patient;
        }
        public List<VMPatientTest> GetPatientTests(int Id, int labExaminationId)
        {
            Patient patient = GetPatient(labExaminationId);
            List<VMPatientTest> listTest = new List<VMPatientTest>();
            foreach (var patientItem in patient.PatientItems)
            {
                foreach (var analysis in patientItem.Analyses)
                {
                    VMPatientTest patientTest = new VMPatientTest
                    {
                        TestName = analysis.Test.Name,
                        TestId = analysis.Test.Id,
                        Section = analysis.Test.TestSection.Name,
                        IsEnable = analysis.Test.IsActive,
                        Cost = analysis.Test.Cost,
                        AnalysisId = analysis.Id


                    };
                    listTest.Add(patientTest);
                }
            }
            return listTest;
        }

        public Patient GetPatient(int labExaminationId)
        {

            Patient patient = (from _patient in myDb.Patients
                               join
                                   _labExamination in myDb.LabExaminations on _patient.Id equals _labExamination.PatientId
                               where _labExamination.Id == labExaminationId
                               select _patient).FirstOrDefault();
            return patient;
        }

        public Patient GetPatient(DateTime ReceivedDate, int OrderNumber)
        {
            Patient patient = (from _patient in myDb.Patients
                               join
                                   _labExamination in myDb.LabExaminations on _patient.Id equals _labExamination.PatientId
                               where EntityFunctions.TruncateTime(_labExamination.ReceivedDate) == ReceivedDate && _labExamination.OrderNumber == OrderNumber
                               select _patient).FirstOrDefault();
            return patient;
        }

        public void PatientUpdate(int patientId, Patient patient)
        {

            Patient patientOld = (from _patient in myDb.Patients
                                  where _patient.Id == patientId
                                  select _patient).FirstOrDefault();
            patientOld.Address = patient.Address;
            patientOld.BirthDate = patient.BirthDate;
            patientOld.Gender = patient.Gender;
            patientOld.FirstName = patient.FirstName;
            patientOld.Age = patient.Age;
            patientOld.Email = patient.Email;
            patientOld.Phone = patient.Phone;
            patientOld.IndentifierNumber = patient.IndentifierNumber;
            myDb.SaveChanges();
        }

        public PatientItem PatientItemUpdate(int patientId, PatientItem patientItem)
        {
            PatientItem patientItemOld = myDb.PatientItems.Where(p => p.PatientId == patientId).FirstOrDefault();
            patientItemOld.LastUpdated = patientItem.LastUpdated;
            myDb.SaveChanges();
            return patientItemOld;
        }

        public List<VMPatientTest> GetPatientTests(int Id)
        {
            return new List<VMPatientTest>();
        }


        #endregion

        #region Analysis
        public void AnalysisInsert(Analysis analysis)
        {
            myDb.Analyses.AddObject(analysis);
            myDb.SaveChanges();
        }
        public void AnalysisApproved(int analysisId, int staffId)
        {
            Analysis analysis = myDb.Analyses.Where(p => p.Id == analysisId).FirstOrDefault();
            if (analysis != null)
            {
                analysis.Status = (int)AnalysisStatusEnum.Approved;
            }
            myDb.SaveChanges();
        }
        public void AnalysisDelete(int analysisId)
        {
            Analysis analysis = myDb.Analyses.Where(p => p.Id == analysisId).FirstOrDefault();
            if (analysis != null)
            {
                myDb.Analyses.DeleteObject(analysis);
                myDb.SaveChanges();
            }
        }

        #endregion

        #region LabExamination
        public int LabExaminationInsert(LabExamination labExamination)
        {
            myDb.LabExaminations.AddObject(labExamination);
            myDb.SaveChanges();
            return labExamination.Id;

        }
        public int GetLabExaminationOrderNumber()
        {
            DateTime today = DateTime.Now.Date;
            List<LabExamination> listLabExToday = myDb.LabExaminations.Where(p => EntityFunctions.TruncateTime(p.ReceivedDate) == EntityFunctions.TruncateTime(today)).ToList();
            if (listLabExToday.Count == 0)
                return 1;
            return myDb.LabExaminations.Where(p => EntityFunctions.TruncateTime(p.ReceivedDate) == EntityFunctions.TruncateTime(today)).Max(p => p.OrderNumber) + 1;

        }
        public VMLabExamination GetLabExamination(int Id)
        {
            VMLabExamination labExamination = myDb.LabExaminations.Where(p => p.PatientId == Id)
                                                                .Select(p => new VMLabExamination
                                                                {
                                                                    CreatedBy = p.CreatedBy,
                                                                    Diagnosis = p.Diagnosis,
                                                                    Id = p.Id,
                                                                    OrderNumber = p.OrderNumber,
                                                                    PartnerId = p.PartnerId,
                                                                    PatientId = p.PatientId,
                                                                    PartnerName = p.Partner.Name,
                                                                    ReceivedDate = p.ReceivedDate,
                                                                    Status = p.Status,
                                                                    ExaminationNumber = p.ExaminationNumber,
                                                                    DoctorId = p.DoctorId,
                                                                    DoctorName = p.Doctor.Name

                                                                }).FirstOrDefault();
            return labExamination;
        }
        public VMLabExamination GetLabExamination(int OrderNumber, DateTime ReceivedDate)
        {
            VMLabExamination labExamination = myDb.LabExaminations.Where(p => p.OrderNumber == OrderNumber && EntityFunctions.TruncateTime(p.ReceivedDate) == ReceivedDate)
                                                               .Select(p => new VMLabExamination
                                                               {
                                                                   CreatedBy = p.CreatedBy,
                                                                   Diagnosis = p.Diagnosis,
                                                                   Id = p.Id,
                                                                   OrderNumber = p.OrderNumber,
                                                                   PartnerId = p.PartnerId,
                                                                   PartnerName = p.Partner.Name,
                                                                   PatientId = p.PatientId,
                                                                   ReceivedDate = p.ReceivedDate,
                                                                   Status = p.Status,
                                                                   ExaminationNumber = p.ExaminationNumber,
                                                                   DoctorId = p.DoctorId,
                                                                   DoctorName = p.Doctor.Name,
                                                                   SpecifiedDoctor = p.SpecifiedDoctor
                                                               }).FirstOrDefault();
            return labExamination;
        }
        public void LabExaminationUpdate(int patientId, DateTime receivedDate, int orderNumber, LabExamination labExamination)
        {
            LabExamination labExamOld = myDb.LabExaminations.Where(p => EntityFunctions.TruncateTime(p.ReceivedDate) == receivedDate && p.OrderNumber == orderNumber && p.PatientId == patientId).FirstOrDefault();
            labExamOld.CreatedBy = labExamination.CreatedBy;
            labExamOld.PartnerId = labExamination.PartnerId;
            labExamOld.Diagnosis = labExamination.Diagnosis;
            labExamOld.Status = labExamination.Status;
            labExamOld.DoctorId = labExamination.DoctorId;
            labExamOld.SpecifiedDoctor = labExamination.SpecifiedDoctor;
            myDb.SaveChanges();
        }
        public VMLabExamination GetLabExamination(string examinationNumber)
        {
            VMLabExamination labExamination = myDb.LabExaminations.Where(p => p.ExaminationNumber == examinationNumber)
                                                                 .Select(p => new VMLabExamination
                                                                 {
                                                                     CreatedBy = p.CreatedBy,
                                                                     Diagnosis = p.Diagnosis,
                                                                     Id = p.Id,
                                                                     OrderNumber = p.OrderNumber,
                                                                     PartnerId = p.PartnerId,
                                                                     PartnerName = p.Partner.Name,
                                                                     PatientId = p.PatientId,
                                                                     ReceivedDate = p.ReceivedDate,
                                                                     Status = p.Status,
                                                                     ExaminationNumber = p.ExaminationNumber,
                                                                     DoctorId = p.DoctorId,
                                                                     DoctorName = p.Doctor.Name
                                                                 }).FirstOrDefault();
            return labExamination;
        }

        public VMLabExamination GetLabExaminationById(int Id)
        {
            VMLabExamination labExamination = myDb.LabExaminations.Where(p => p.Id == Id)
                                                                .Select(p => new VMLabExamination
                                                                {
                                                                    CreatedBy = p.CreatedBy,
                                                                    Diagnosis = p.Diagnosis,
                                                                    Id = p.Id,
                                                                    OrderNumber = p.OrderNumber,
                                                                    PartnerId = p.PartnerId,
                                                                    PatientId = p.PatientId,
                                                                    PartnerName = p.Partner.Name,
                                                                    ReceivedDate = p.ReceivedDate,
                                                                    Status = p.Status,
                                                                    ExaminationNumber = p.ExaminationNumber,
                                                                    DoctorId = p.DoctorId,
                                                                    DoctorName = p.Doctor.Name

                                                                }).FirstOrDefault();
            return labExamination;
        }

        #endregion

        #region Result
        public void ResultInsert(int analysisId, string result, int staffId)
        {
            Result testResult = myDb.Results.Where(p => p.AnalysisId == analysisId).FirstOrDefault();
            if (testResult == null)
            {
                //Add result
                testResult = new Result();
                testResult.AnalysisId = analysisId;
                testResult.LastUpdated = DateTime.Now;
                testResult.LastModifiedStaffId = staffId;
                testResult.IsReportable = true;
                testResult.Value = result;
                myDb.Results.AddObject(testResult);

                // update analysis
                Analysis analysis = myDb.Analyses.Where(p => p.Id == analysisId).FirstOrDefault();
                if (analysis != null)
                {
                    analysis.Status = (int)AnalysisStatusEnum.HaveResult;
                }
                myDb.SaveChanges();
            }
            else
            {
                ResultUpdate(analysisId, testResult.Id, result, staffId);
            }
        }

        public void ResultUpdate(int analysisId, int resultId, string result, int staffId)
        {

            //Add result
            Result testResult = myDb.Results.Where(p => p.Id == resultId).FirstOrDefault();
            if (testResult != null)
            {
                testResult.LastUpdated = DateTime.Now;
                testResult.LastModifiedStaffId = staffId;
                testResult.IsReportable = true;
                testResult.Value = result;
            }
            // update analysis
            Analysis analysis = myDb.Analyses.Where(p => p.Id == analysisId).FirstOrDefault();
            if (analysis != null)
            {
                if (string.IsNullOrEmpty(result))
                    analysis.Status = (int)AnalysisStatusEnum.New;
                else
                {
                    analysis.Status = (int)AnalysisStatusEnum.HaveResult;
                }
            }
            myDb.SaveChanges();
        }

        public List<VMTestResult> GetPatientTestResults(int orderNumber, DateTime receivedDate)
        {
            VMLabExamination labExamination = GetLabExamination(orderNumber, receivedDate);
            return GetPatientTestResults(labExamination.Id);
        }

        public List<VMTestResult> GetPatientTestResults(int labExaminationId)
        {
            List<TestResultsGet_Result> tests = myDb.PatientTestResultsGet(labExaminationId).ToList();
            List<VMTestResult> testResults = new List<VMTestResult>();
            if (tests != null && tests.Count != 0)
            {
                foreach (var p in tests)
                {
                    string tinhtrang = RepositoryStrings.AnalysisStatus_New;
                    if (p.Status == (int)AnalysisStatusEnum.HaveResult)
                    {
                        tinhtrang = RepositoryStrings.AnalysisStatus_HaveResult;
                    }
                    else if (p.Status == (int)AnalysisStatusEnum.Approved)
                    {
                        tinhtrang = RepositoryStrings.AnalysisStatus_Approved;
                    }

                    testResults.Add(new VMTestResult
                    {
                        Index = (int)p.STT,
                        Name = p.Name,
                        Range = p.Range,
                        Result = p.Result ?? "",
                        TestId = p.TestId,
                        Unit = p.Unit,
                        StatusString = tinhtrang,
                        Status = p.Status,
                        AnalysisId = p.AnalysisId,
                        ResultId = p.ResultId,
                        MoTa = p.Description


                    });
                }
            }
            return testResults;
        }
        #endregion

        #region Report
        public List<Report_PatientResult> ReportData_PatientResult(int labExaminationId)
        {
            List<Report_PatientResult> list = myDb.Report_PatientResult(labExaminationId).ToList();
            return list;
        }
        public List<Report_TestResultNoteBook> ReportData_TestResultNoteBook(DateTime startDate, DateTime endDate)
        {
            List<report_TestResultNoteBook_Result> list = myDb.report_TestResultNoteBook(startDate, endDate).ToList();
            Dictionary<string, Report_TestResultNoteBook> result = new Dictionary<string, Report_TestResultNoteBook>();
            foreach (var item in list)
            {
                if (!result.Keys.Contains(item.ExaminationNumber))
                {
                    Report_TestResultNoteBook testResult = new Report_TestResultNoteBook
                    {
                        Age = item.Age,
                        Male = item.Gender,
                        PartnerName = item.LabName,
                        PatientName = item.FirstName,
                        Phone = item.Phone,
                        ReceiveDate = item.ReceivedDate.ToString("d"),
                        ReturnDate = item.ReceivedDate.ToString("d"),
                        Result = string.Format("{0}:{1}", item.Name, item.Value)
                    };
                    result.Add(item.ExaminationNumber, testResult);
                }
                else
                {
                    result[item.ExaminationNumber].Result += string.Format(" , {0}:{1}", item.Name, item.Value);
                }
            }
            return result.Values.ToList();
        }
        public List<Report_BaoCaoTaiChinh> ReportData_BaoCaoTaiChinh(DateTime startDate, DateTime endDate, int partnerId)
        {
            List<report_ThongKeTaiChinh_Result> list = myDb.report_ThongKeTaiChinh(startDate, endDate, partnerId).ToList();
            Dictionary<string, Report_BaoCaoTaiChinh> result = new Dictionary<string, Report_BaoCaoTaiChinh>();
            foreach (var item in list)
            {
                if (!result.Keys.Contains(item.ExaminationNumber))
                {
                    Report_BaoCaoTaiChinh testResult = new Report_BaoCaoTaiChinh
                    {
                        Age = item.Age,
                        Male = item.Gender,
                        PartnerName = item.LabName,
                        PatientName = item.FirstName,
                        Phone = item.Phone,
                        ReceiveDate = item.ReceivedDate.ToString("d"),
                        ReturnDate = item.ReceivedDate.ToString("d"),
                        Result = string.Format("{0}", item.Name),
                    };
                    result.Add(item.ExaminationNumber, testResult);
                }
                else
                {
                    result[item.ExaminationNumber].Result += string.Format(" , {0}", item.Name);
                }
            }

            // Tinh giá của xét nghiệm
            foreach (var item in result.Keys)
            {
                List<report_ThongKeTaiChinh_Result> listTestResultOfAPatient = list.Where(p => p.ExaminationNumber == item).ToList();
                decimal cost = 0;
                //Tinh giá của các test đơn và những test thuộc test section không sử dụng giá chung UseTestSectionCode=false
                cost = listTestResultOfAPatient.Where(p => (p.TestSectionId == null || !p.UseTestSectionCode.Value))
                                                .Select(p => p.Cost.Value).Sum();
                // Lấy danh sách test section và giá của những test section có sử dụng giá chung UseTestSectionCode=true
                var TestSectionUseTestSectionCost = listTestResultOfAPatient.Where(p => (p.TestSectionId != null && p.UseTestSectionCode.Value)).Select(p => new { TestSectionId = p.TestSectionId, Cost = p.TestSectionCost }).Distinct();
                cost += TestSectionUseTestSectionCost.Select(p => p.Cost.Value).Sum();
                result[item].Cost = cost;
            }
            return result.Values.ToList();
        }
        #endregion

        #region LabUser
        public LabUser GetLabUser(string UserName, string Password)
        {
            LabUser account = myDb.LabUsers.Where(p => p.UserName == UserName && p.Password == Password).FirstOrDefault();
            return account;
        }
        #endregion

        #region TestSectionCommission
        public TestSectionCommission GetTestSectionCommission(int id)
        {
            try
            {
                TestSectionCommission test = (from _test in myDb.TestSectionCommissions where _test.Id == id select _test).First();
                myDb.SaveChanges();
                return test;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void TestSectionCommissionInsert(TestSectionCommission test)
        {
            myDb.TestSectionCommissions.AddObject(test);
            myDb.SaveChanges();
        }

        public void TestSectionCommissionUpdate(int id, TestSectionCommission test)
        {
            TestSectionCommission currentTest = (from _test in myDb.TestSectionCommissions where _test.Id == id select _test).First();

            currentTest.IsActive = test.IsActive;
            currentTest.Cost = test.Cost;

            myDb.SaveChanges();
        }

        public void TestSectionCommissionDelete(int id)
        {
            TestSectionCommission test = GetTestSectionCommission(id);
            test.IsActive = false;

            myDb.SaveChanges();
        }
        #endregion

        #region Instrument
        public List<SearchInstrumentResult_Result> InstrumentResultSearch(DateTime? receivedDate, string orderNumber, int? instrumentId)
        {
            List<SearchInstrumentResult_Result> lstInstrumentResult = myDb.SearchInstrumentResult(receivedDate, orderNumber, instrumentId).ToList();
            return lstInstrumentResult;
        }

        public List<Instrument> GetInstruments()
        {
            List<Instrument> lstInstruments = (from _instrument in myDb.Instruments
                                               where _instrument.IsActive == true
                                               select _instrument).ToList();
            return lstInstruments;
        }

        public List<InstrumentResult> GetAllValidInstrumentResult()
        {
            List<InstrumentResult> lstInstrumentResults = (from _insResult in myDb.InstrumentResults
                                                           where _insResult.Flag == false
                                                           select _insResult).ToList();
            return lstInstrumentResults;
        }

        public List<InstrumentResult> GetAllValidInstrumentResultByCondition(DateTime? receivedDate, string orderNumber, int? instrumentId)
        {
            List<InstrumentResult> lstInstrumentResults = (from _insResult in myDb.InstrumentResults
                                                           where _insResult.Flag == false
                                                             && _insResult.ReceivedDate == receivedDate
                                                             && (orderNumber == null || _insResult.OrderNumber == orderNumber)
                                                             && (instrumentId == null || _insResult.InstrumentId == instrumentId)
                                                           select _insResult).ToList();
            return lstInstrumentResults;
        }

        public List<InstrumentResult> GetAllInstrumentResultByCondition(DateTime? receivedDate, string orderNumber, int? instrumentId)
        {
            List<InstrumentResult> lstInstrumentResult = (from _insResult in myDb.InstrumentResults
                                                           where _insResult.ReceivedDate == receivedDate
                                                             && (orderNumber == null ||_insResult.OrderNumber == orderNumber)
                                                             && (instrumentId == null || _insResult.InstrumentId == instrumentId)
                                                           select _insResult).ToList();
            return lstInstrumentResult;
        }

        public void InsertToResult(int? orderNumber, DateTime? receivedDate, int? testId, string value, int? instrumentResultId)
        {
            myDb.Result(orderNumber, receivedDate, testId, value, instrumentResultId);
        }

        public void UpdateSID(DateTime? receivedDate, string oldOrderNumber, int newOrderNumber, int? instrumentId)
        {
            int success = myDb.UpdateSID(oldOrderNumber, newOrderNumber, receivedDate, instrumentId);
        }
        #endregion

        #region Service
        public string SetupDoctorConnection(string connectionCode, int serverDoctorId, int clientDoctorId, string doctorConnectName)
        {

            // Kiem tra connection code có tồn tại?
            Doctor doctor = myDb.Doctors.Where(p => p.ConnectionCode == connectionCode).FirstOrDefault();
            if (doctor == null)
            {
                //Connection error 1 = Connection code not exist
                return "Doctor_Connection_Error1";
            }

            // Kiểm tra connection code đã được dùng chưa
            if (doctor.IsConnected)
            {
                // Connection error 2 = Connection code was used
                return "Doctor_Connection_Error2";
            }

            // Kiểm tra bác sĩ đã liên kết với lab chưa
            bool isDoctorWasConnectWithLab = myDb.Doctors.Any(p => p.ServerDoctorId.HasValue
                                                            && p.ServerDoctorId.Value == serverDoctorId
                                                            && p.ConnectionCode != connectionCode && p.IsConnected);
            if (isDoctorWasConnectWithLab)
            {
                // Connection error 3 = Doctor current has a connectiton with lab
                return "Doctor_Connection_Error3";
            }

            // Cập nhật liên kết
            doctor.ServerDoctorId = serverDoctorId;
            doctor.DoctorConnectName = doctorConnectName;
            doctor.IsConnected = true;
            myDb.SaveChanges();
            return "Success";
        }
        #endregion
    }
}
