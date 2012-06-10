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
        private readonly LabManager_ClientEntities _myDb;
        private IServerConnector _connector;

        public LabManager_ClientEntities Context
        {
            get { return _myDb; }
        }

        public IServerConnector Connector
        {
            get { return _connector; }
            set { _connector = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Repository()
        {
            _myDb = new LabManager_ClientEntities();
            _connector = new ServerConnector();
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
            oldPartner.Logo = updateVaule.Logo;
            _myDb.SaveChanges();

        }

        public void PartnerInsert(Partner partner)
        {
            _myDb.Partners.AddObject(partner);
            _myDb.SaveChanges();
        }
        public Partner GetPartnerById(int id)
        {
            Partner partner = _myDb.Partners.FirstOrDefault(p => p.Id == id);
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
            List<VMTestSectionListItem> listTest = partner.TestSectionCommissions.Where(p => p.IsActive).Select(p => new VMTestSectionListItem
            {
                TestSectionName = p.TestSection.Name,
                TestSectionId = p.TestSectionId,
                TestSectionCost = p.Cost,
                IsEnableTestSection = true,
            }).ToList();

            return listTest;
        }

        public List<Partner> GetPartnerByName(string name, bool isActive)
        {
            List<Partner> lstPartner = (from _partner in _myDb.Partners where (string.IsNullOrEmpty(name) || _partner.Name.ToUpper().Contains(name.ToUpper()))
                                        && _partner.IsActive == isActive
                                        select _partner).ToList();
            return lstPartner;
        }
        public List<Partner> GetPartners()
        {
            List<Partner> lstPartner = (from _partner in _myDb.Partners where _partner.IsActive select _partner).ToList();
            return lstPartner;
        }
        public bool IsValidPartner(string name)
        {
            bool isValid = true;
            Partner partner = _myDb.Partners.SingleOrDefault(u => u.Name == name);
            if (partner != null)
            {
                isValid = false;
            }
            return isValid;
        }

        public object GetPartnerNameByName(string name, string searchType)
        {
            List<SearchPartnerByName_Result> lstPartner = _myDb.SearchPartnerByName(name, searchType).ToList();
            return lstPartner.Select(p => new { Label = p.Name, Value = p.Name });
        }

        public Partner GetPartnerByLabExamination(int examinationNumber)
        {
            Partner partner = (from _partner in _myDb.Partners
                               join
                                   _labExamination in _myDb.LabExaminations on _partner.Id equals _labExamination.PartnerId
                               where _labExamination.Id == examinationNumber
                               select _partner).FirstOrDefault();
            return partner;
        }

        public string CreateLabConnectionCode(int clientLabId, int labId)
        {
            string connectionCode = _myDb.GenerateConnectionCode((int)ConstantNumber.ConnetionCodeLength).FirstOrDefault();
            connectionCode = AddLabIdToUniqueString(connectionCode, labId);
            if (clientLabId != 0)
            {
                Partner currentPartner = (from partner in _myDb.Partners where partner.Id == clientLabId select partner).First();
                currentPartner.ConnectionCode = connectionCode;
                try
                {
                    _connector.SubmitLabConnectionCodeToServer(clientLabId, labId, connectionCode);
                    currentPartner.UpdatedOnServer = true;

                }
                catch (Exception ex)
                {
                    currentPartner.UpdatedOnServer = false;
                }
                _myDb.SaveChanges();
            }
            return connectionCode;
        }

        public void RemoveLabConnection(int clientLabId, int labId)
        {
            Partner currentPartner = (from _partner in _myDb.Partners where _partner.Id == clientLabId select _partner).First();
            if (currentPartner.IsConnected == true)
            {
                currentPartner.IsConnected = false;
                currentPartner.ConnectionCode = "";
                //currentPartner.ServerLabId = null;
                currentPartner.UpdatedOnServer = false;
                try
                {
                    bool isSuccess = Connector.RemoveLabConnect(currentPartner.ServerLabId.Value, clientLabId, labId, currentPartner.ConnectionCode);
                    if (isSuccess)
                    {
                        currentPartner.UpdatedOnServer = true;
                        currentPartner.ServerLabId = null;
                    }
                }
                catch (Exception ex)
                {
                }
                _myDb.SaveChanges();
            }
        }
        #endregion

        #region PartnerCost
        public PartnerCost GetPartnerCost(int partnerCostId)
        {
            PartnerCost partnerCost = _myDb.PartnerCosts.FirstOrDefault(_partnerCost => _partnerCost.Id == partnerCostId);
            _myDb.SaveChanges();
            return partnerCost;
        }

        public void PartnerCostInsert(PartnerCost partnerCost)
        {
            _myDb.PartnerCosts.AddObject(partnerCost);
            _myDb.SaveChanges();
        }

        public void PartnerCostUpdate(int id, PartnerCost partnerCost)
        {
            PartnerCost currentPartnerCost = (from _partnerCost in _myDb.PartnerCosts where _partnerCost.Id == id select _partnerCost).First();
            currentPartnerCost.TestId = partnerCost.TestId;
            currentPartnerCost.PartnerId = partnerCost.PartnerId;
            currentPartnerCost.Cost = partnerCost.Cost;
            currentPartnerCost.Description = partnerCost.Description;
            currentPartnerCost.IsActive = partnerCost.IsActive;
            currentPartnerCost.LastUpdated = partnerCost.LastUpdated;

            _myDb.SaveChanges();
        }

        public void PartnerCostDelete(int id)
        {
            PartnerCost partnerCost = GetPartnerCost(id);
            partnerCost.IsActive = false;

            _myDb.SaveChanges();
        }

        public void PartnerCostDeleteByPartnerId(int id)
        {
            List<PartnerCost> lstPartnerCost = (from _partnerCost in _myDb.PartnerCosts where _partnerCost.PartnerId == id select _partnerCost).ToList();
            foreach (PartnerCost p in lstPartnerCost)
            {
                p.IsActive = false;
            }
            _myDb.SaveChanges();
        }

        public List<PartnerCost> GetPartnerCostByPartnerId(int id)
        {
            List<PartnerCost> lstPartnerCost = (from _partnerCost in _myDb.PartnerCosts where _partnerCost.PartnerId == id select _partnerCost).ToList();

            return lstPartnerCost;
        }

        public PartnerCost GetPartnerCostByTestId(int testId, int partnerId)
        {
            PartnerCost partnerCost = (from _partnerCost in _myDb.PartnerCosts
                                       where _partnerCost.TestId == testId && _partnerCost.PartnerId == partnerId && _partnerCost.IsActive == true
                                       select _partnerCost).First();
            return partnerCost;
        }

        public bool IsPartnerCostExist(int testId, int partnerId)
        {
            bool isExist = false;
            PartnerCost partnerCost = new PartnerCost();
            partnerCost = (from _partnerCost in _myDb.PartnerCosts
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
            List<Test> lstTests = (from _test in _myDb.Tests
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
            List<Test> lstTests = (from _test in _myDb.Tests
                                   where _test.IsActive == true && (_test.TestSection == null || !_test.TestSection.UseCostForAssociateTest)
                                   select _test).ToList();
            return lstTests;
        }


        public Test GetTest(int testId)
        {
            try
            {
                Test test = (from _test in _myDb.Tests where _test.Id == testId select _test).First();
                _myDb.SaveChanges();
                return test;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void TestInsert(Test test)
        {
            _myDb.Tests.AddObject(test);
            _myDb.SaveChanges();
        }

        public void TestUpdate(int id, Test test)
        {
            Test currentTest = (from _test in _myDb.Tests where _test.Id == id select _test).First();
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

            _myDb.SaveChanges();
        }

        public void TestDelete(int testId)
        {
            Test test = GetTest(testId);
            test.IsActive = false;

            _myDb.SaveChanges();
        }

        public List<Test> GetTestByTestSectionId(int testSectionId)
        {
            List<Test> lstTests = (from _test in _myDb.Tests
                                   where (_test.IsActive == true && _test.TestSectionId == testSectionId)
                                   select _test).ToList();
            return lstTests;
        }

        public bool IsValidTest(string testName)
        {
            bool isValid = true;
            Test testWithSameName = _myDb.Tests.SingleOrDefault(u => u.Name == testName);
            if (testWithSameName != null)
            {
                isValid = false;
            }
            return isValid;
        }

        public List<SearchTest_Result> TestSearch(string testName, string testSectionName, string panelName, bool isActive)
        {
            List<SearchTest_Result> lstTestSearch = _myDb.SearchTest(testName, testSectionName, panelName, isActive).ToList();
            return lstTestSearch;
        }

        public object GetTestByName(string name, string searchType)
        {
            List<SearchTestByName_Result> lstTest = _myDb.SearchTestByName(name, searchType).ToList();
            return lstTest.Select(p => new { Label = p.Name, Value = p.Id, Tag = p.Cost });
        }

        public object GetTestByNameForPanel(string name, string searchType)
        {
            List<SearchTestByNameForPanel_Result> lstTest = _myDb.SearchTestByNameForPanel(name, searchType).ToList();
            return lstTest.Select(p => new { Label = p.Name, Value = p.Id, Tag = p.TestSectionName + "," + p.Cost });
        }

        public object GetTestHaveCostNotDependenceTestSection()
        {
            var lstTest = _myDb.Tests.Where(p => (p.TestSection == null || !p.TestSection.UseCostForAssociateTest) && p.IsActive)
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
                Panel panel = (from _panel in _myDb.Panels where (_panel.Id == panelId) select _panel).First();
                _myDb.SaveChanges();
                return panel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PanelInsert(Panel panel)
        {
            _myDb.Panels.AddObject(panel);
            _myDb.SaveChanges();
        }

        public void PanelUpdate(int id, Panel panel)
        {
            Panel currentPanel = (from _panel in _myDb.Panels where _panel.Id == id select _panel).First();

            currentPanel.Name = panel.Name;
            currentPanel.Description = panel.Description;
            currentPanel.IsActive = panel.IsActive;
            currentPanel.LastUpdated = panel.LastUpdated;

            _myDb.SaveChanges();
        }

        public void PanelDelete(int panelId)
        {
            Panel panel = GetPanel(panelId);
            panel.IsActive = false;

            _myDb.SaveChanges();
        }

        public List<Panel> GetAllPanel()
        {
            List<Panel> lstPanels = (from _panel in _myDb.Panels where _panel.IsActive == true select _panel).ToList();
            return lstPanels;
        }

        public bool IsValidPanel(string name)
        {
            bool isValid = true;
            Panel panel = _myDb.Panels.SingleOrDefault(u => u.Name == name);
            if (panel != null)
            {
                isValid = false;
            }
            return isValid;
        }
        public object GetPanelNameByName(string name, string searchType)
        {
            List<SearchPanelByName_Result> lstPanel = _myDb.SearchPanelByName(name, searchType).ToList();
            return lstPanel.Select(p => new { Label = p.Name, Value = p.Name });
        }
        #endregion Panel

        #region PanelItem
        public PanelItem GetPanelItem(int panelItemId)
        {
            try
            {
                PanelItem panelItem = (from _panelItem in _myDb.PanelItems where _panelItem.Id == panelItemId select _panelItem).First();
                _myDb.SaveChanges();
                return panelItem;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PanelItemInsert(PanelItem panelItem)
        {
            _myDb.PanelItems.AddObject(panelItem);
            _myDb.SaveChanges();
        }

        public void PanelItemUpdate(int id, PanelItem panelItem)
        {
            PanelItem currentPanelItem = (from _panelItem in _myDb.PanelItems where _panelItem.Id == id select _panelItem).First();

            currentPanelItem.PanelId = panelItem.PanelId;
            currentPanelItem.TestId = panelItem.TestId;
            currentPanelItem.TestName = panelItem.TestName;
            currentPanelItem.SortOrder = panelItem.SortOrder;
            currentPanelItem.IsActive = panelItem.IsActive;
            currentPanelItem.LastUpdated = panelItem.LastUpdated;

            _myDb.SaveChanges();
        }

        public void PanelItemDelete(int panelItemId)
        {
            PanelItem panelItem = GetPanelItem(panelItemId);
            panelItem.IsActive = false;

            _myDb.SaveChanges();
        }

        public List<PanelItem> GetPanelItemByPanelId(int panelId)
        {
            List<PanelItem> lstPanelItems = (from _panelItem in _myDb.PanelItems where _panelItem.PanelId == panelId select _panelItem).ToList();

            return lstPanelItems;
        }

        public List<PanelItem> GetPanelItemByTestId(int testId)
        {
            List<PanelItem> lstPanelItems = (from _panelItem in _myDb.PanelItems where _panelItem.TestId == testId select _panelItem).ToList();

            return lstPanelItems;
        }

        public List<Panel> GetPanelByName(string name, bool isActive)
        {
            List<Panel> lstPanel = (from _panel in _myDb.Panels where (string.IsNullOrEmpty(name) || _panel.Name.Contains(name)) && _panel.IsActive == isActive 
                                    select _panel).ToList();
            return lstPanel;
        }

        public PanelItem GetPanelItemByTestIdAndPanelId(int testId, int panelId)
        {
            PanelItem panelItem = (from _item in _myDb.PanelItems
                                   where _item.PanelId == panelId && _item.TestId == testId
                                   select _item).FirstOrDefault();

            return panelItem;
        }

        public bool IsPanelItemExist(int testId)
        {
            bool isExist = false;
            PanelItem item = new PanelItem();
            item = (from _panelItem in _myDb.PanelItems
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
            return (from _panel in _myDb.Panels where _panel.IsActive select _panel).ToList();
        }
        #endregion PanelItem

        #region Doctor
        public Doctor GetDoctor(int doctorId)
        {
            try
            {
                Doctor doctor = (from _doctor in _myDb.Doctors where _doctor.Id == doctorId select _doctor).First();
                _myDb.SaveChanges();
                return doctor;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DoctorInsert(Doctor doctor)
        {
            _myDb.Doctors.AddObject(doctor);
            _myDb.SaveChanges();
        }

        public void DoctorUpdate(int id, Doctor doctor)
        {
            Doctor currentDoctor = (from _doctor in _myDb.Doctors where _doctor.Id == id select _doctor).First();
            currentDoctor.Name = doctor.Name;
            currentDoctor.Address = doctor.Address;
            currentDoctor.Phone = doctor.Phone;
            currentDoctor.IsActive = doctor.IsActive;
            currentDoctor.Email = doctor.Email;
            currentDoctor.Commission = doctor.Commission;
            currentDoctor.BankAccountNumber = doctor.BankAccountNumber;
            currentDoctor.Other = doctor.Other;
            currentDoctor.ConnectionCode = doctor.ConnectionCode;
            _myDb.SaveChanges();
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
            string connectionCode = _myDb.GenerateConnectionCode((int)ConstantNumber.ConnetionCodeLength).FirstOrDefault();
            connectionCode = AddLabIdToUniqueString(connectionCode, labId);
            if (doctorId != 0)
            {
                Doctor currentDoctor = (from _doctor in _myDb.Doctors where _doctor.Id == doctorId select _doctor).First();
                currentDoctor.ConnectionCode = connectionCode;
                try
                {
                    _connector.SubmitConnectionCodeToServer(doctorId, labId, connectionCode);
                    currentDoctor.UpdatedOnServer = true;
                    
                }
                catch (Exception ex)
                {
                    currentDoctor.UpdatedOnServer = false;
                }
                _myDb.SaveChanges();
            }
            return connectionCode;
        }

        public void RemoveConnection(int doctorId,int labId)
        {
            Doctor currentDoctor = (from _doctor in _myDb.Doctors where _doctor.Id == doctorId select _doctor).First();
            if (currentDoctor.IsConnected == true)
            {
                currentDoctor.ConnectionCode = "";
                currentDoctor.IsConnected = false;
                currentDoctor.UpdatedOnServer = false;
                try
                {
                    bool isSuccess = Connector.RemoveDoctorConnect(currentDoctor.ServerDoctorId.Value, doctorId, labId, currentDoctor.ConnectionCode);
                    if (isSuccess)
                    {
                        currentDoctor.ServerDoctorId = null;
                        currentDoctor.UpdatedOnServer = true;
                    }
                }
                catch (Exception ex)
                { 
                }
                _myDb.SaveChanges();
            }
        }
        public void DoctorDelete(int doctorId)
        {
            Doctor doctor = GetDoctor(doctorId);
            doctor.IsActive = false;

            _myDb.SaveChanges();
        }

        public bool IsValidDoctor(string name)
        {
            bool isValid = true;
            Doctor doctor = _myDb.Doctors.SingleOrDefault(u => u.Name == name);
            if (doctor != null)
            {
                isValid = false;
            }
            return isValid;
        }

        public List<Doctor> GetDoctorByName(string name)
        {
            List<Doctor> lstDoctor = (from _doctor in _myDb.Doctors where _doctor.IsActive && (string.IsNullOrEmpty(name) || _doctor.Name.Contains(name))
                                      select _doctor).ToList();
            return lstDoctor;
        }

        public List<Doctor> GetDoctorByNameForSearch(string name, bool IsActive)
        {
            List<Doctor> lstDoctor = (from _doctor in _myDb.Doctors
                                      where (string.IsNullOrEmpty(name) || _doctor.Name.Contains(name))
                                        && _doctor.IsActive == IsActive
                                      select _doctor).ToList();
            return lstDoctor;
        }

        public object GetDoctorNameByName(string name, string searchType)
        {
            List<SearchDoctorByName_Result> lstDoctor = _myDb.SearchDoctorByName(name, searchType).ToList();
            return lstDoctor.Select(p => new { Label = p.Name, Value = p.Name });
        }

        public string GetUniqueExaminationNumber(int length,int labId)
        {
            string examinationNumber = _myDb.GenerateLabExaminationNumber(5).FirstOrDefault();
            examinationNumber = AddLabIdToUniqueString(examinationNumber, labId);
            return examinationNumber;
        }
        public string GetUniquePatientNumber(int length, int labId)
        {
            string examinationNumber = _myDb.GenerateLabExaminationNumber(5).FirstOrDefault();
            examinationNumber = AddLabIdToUniqueString(examinationNumber, labId);
            return examinationNumber;
        }


        #endregion

        #region TestSection


        public object GetTestSectionByName(string name, string searchType)
        {
            List<SearchTestSection_Result> lstTestSection = _myDb.SearchTestSection(name, searchType).ToList();
            return lstTestSection.Select(p => new { Label = p.Name, Value = p.Id, Tag = p.UseCostForAssociateTest });
        }

        public TestSection GetTestSection(int testSectionId)
        {
            TestSection testSection = (from _testSection in _myDb.TestSections where _testSection.Id == testSectionId select _testSection).First();
            _myDb.SaveChanges();
            return testSection;
        }

        public List<TestSection> GetTestSections()
        {
            List<TestSection> lstTestSections = (from _testSection in _myDb.TestSections
                                                 where _testSection.IsActive == true
                                                 select _testSection).ToList();
            return lstTestSections;
        }

        public List<TestSection> GetAllTestSections()
        {
            List<TestSection> lstTestSections = (from _testSection in _myDb.TestSections
                                                 select _testSection).ToList();
            return lstTestSections;
        }

        public void TestSectionInsert(TestSection ts)
        {
            _myDb.TestSections.AddObject(ts);
            _myDb.SaveChanges();
        }


        public void TestSectionDelete(int testSectionId)
        {
            TestSection ts = GetTestSection(testSectionId);
            ts.IsActive = false;

            _myDb.SaveChanges();
        }

        public bool IsValidTestSection(string tsName, int? testSectionId)
        {
            bool isValid = true;
            TestSection tsWithSameName = _myDb.TestSections.SingleOrDefault(u => u.Name == tsName);
            if (tsWithSameName != null)
            {
                if ((testSectionId == null)
                || (testSectionId != null && testSectionId != tsWithSameName.Id))
                    isValid = false;
            }
            return isValid;
        }

        public List<VMTestListItem> GetTestsOfTestSection(int id)
        {
            TestSection testSection = GetTestSection(id);
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
            TestSection testSection = (from _testSection in _myDb.TestSections
                                       where _testSection.Id == testSectionToUpdate.Id
                                       select _testSection).First();
            testSection.IsActive = testSectionToUpdate.IsActive;
            testSection.Name = testSectionToUpdate.Name;
            testSection.SortOrder = testSectionToUpdate.SortOrder;
            testSection.Cost = testSectionToUpdate.Cost;
            testSection.Description = testSection.Description;
            testSection.UseCostForAssociateTest = testSectionToUpdate.UseCostForAssociateTest;
            _myDb.SaveChanges();
        }

        public object GetTestSectionByNameForPanel(string name, string searchType)
        {
            List<SearchTestSectionByNameForPanel_Result> lstTestSection = _myDb.SearchTestSectionByNameForPanel(name, searchType).ToList();
            return lstTestSection.Select(p => new { Label = p.Name, Value = p.Id, Tag = p.Cost });
        }

        public List<TestSection> GetTestSectionByNameAndStatus(string name, bool isActive)
        {
            List<TestSection> lstTestSection = (from _testSection in _myDb.TestSections
                                                where (string.IsNullOrEmpty(name) || _testSection.Name.Contains(name)) && _testSection.IsActive == isActive
                                                select _testSection).ToList();
            return lstTestSection;
        }
        #endregion

        #region Patient

        public int PatientInsert(Patient patient)
        {
            _myDb.Patients.AddObject(patient);
            _myDb.SaveChanges();
            return patient.Id;
        }

        public int PatientItemInsert(PatientItem patientItem)
        {
            _myDb.PatientItems.AddObject(patientItem);
            _myDb.SaveChanges();
            return patientItem.Id;
        }

        public List<PatientsGets_Result> GetPatients(int? patientId, string firstName, string phone, string email, string indentifierNumber, string address, int? partnerId, int? orderNumber, DateTime? receivedDate)
        {
            List<PatientsGets_Result> listPatient = _myDb.PatientsGets(patientId, firstName, phone, email, indentifierNumber, address, partnerId, orderNumber, receivedDate).ToList();
            return listPatient;
        }

        public Patient GetPatientNumber(int id)
        {
            Patient patient = _myDb.Patients.FirstOrDefault(p => p.Id == id);
            return patient;
        }

        public List<VMPatientTest> GetPatientTests(int Id, int labExaminationId)
        {
            Patient patient = GetPatientByLabExaminationId(labExaminationId);
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

        public Patient GetPatientByLabExaminationId(int labExaminationId)
        {

            Patient patient = (from _patient in _myDb.Patients
                               join
                                   _labExamination in _myDb.LabExaminations on _patient.Id equals _labExamination.PatientId
                               where _labExamination.Id == labExaminationId
                               select _patient).FirstOrDefault();
            return patient;
        }

        public Patient GetPatientByExaminationDateAndOrderNumber(DateTime receivedDate, int orderNumber)
        {
            Patient patient = (from _patient in _myDb.Patients
                               join
                                   _labExamination in _myDb.LabExaminations on _patient.Id equals _labExamination.PatientId
                               where EntityFunctions.TruncateTime(_labExamination.ReceivedDate) == receivedDate && _labExamination.OrderNumber == orderNumber
                               select _patient).FirstOrDefault();
            return patient;
        }

        public void PatientUpdate(int patientId, Patient patient)
        {

            Patient patientOld = (from _patient in _myDb.Patients
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
            _myDb.SaveChanges();
        }

        public PatientItem PatientItemUpdate(int patientId, PatientItem patientItem)
        {
            PatientItem patientItemOld = _myDb.PatientItems.FirstOrDefault(p => p.PatientId == patientId);
            patientItemOld.LastUpdated = patientItem.LastUpdated;
            _myDb.SaveChanges();
            return patientItemOld;
        }

        public List<VMPatientTest> GetPatientTests(int Id)
        {
            return new List<VMPatientTest>();
        }

        public Patient GetPatientById(int patientId)
        {
            return _myDb.Patients.FirstOrDefault(p => p.Id == patientId);
        }

        #endregion

        #region Analysis
        public void AnalysisInsert(Analysis analysis)
        {
            _myDb.Analyses.AddObject(analysis);
            _myDb.SaveChanges();
        }
        public void AnalysisApproved(int analysisId, int staffId)
        {
            Analysis analysis = _myDb.Analyses.FirstOrDefault(p => p.Id == analysisId);
            if (analysis != null)
            {
                analysis.Status = (int)AnalysisStatusEnum.Approved;
            }
            _myDb.SaveChanges();
        }
        public void AnalysisDelete(int analysisId)
        {
            Analysis analysis = _myDb.Analyses.FirstOrDefault(p => p.Id == analysisId);
            if (analysis != null)
            {
                // Delete result 
                var resultOfAnalysis = _myDb.Results.Where(p=>p.AnalysisId == analysisId);
                foreach (var result in resultOfAnalysis)
                {
                    _myDb.Results.DeleteObject(result);
                }
                _myDb.Analyses.DeleteObject(analysis);
                _myDb.SaveChanges();
            }
        }

        #endregion

        #region LabExamination
        public int LabExaminationInsert(LabExamination labExamination)
        {
            _myDb.LabExaminations.AddObject(labExamination);
            _myDb.SaveChanges();
            return labExamination.Id;

        }
        public int GetLabExaminationOrderNumber()
        {
            DateTime today = DateTime.Now.Date;
            List<LabExamination> listLabExToday = _myDb.LabExaminations.Where(p => EntityFunctions.TruncateTime(p.ReceivedDate) == EntityFunctions.TruncateTime(today)).ToList();
            if (listLabExToday.Count == 0)
                return 1;
            return _myDb.LabExaminations.Where(p => EntityFunctions.TruncateTime(p.ReceivedDate) == EntityFunctions.TruncateTime(today)).Max(p => p.OrderNumber) + 1;

        }
        public VMLabExamination GetLabExamination(int labExaminationId)
        {
            VMLabExamination labExamination = _myDb.LabExaminations.Where(p => p.PatientId == labExaminationId)
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
        public VMLabExamination GetLabExamination(int orderNumber, DateTime receivedDate)
        {
            VMLabExamination labExamination = _myDb.LabExaminations.Where(p => p.OrderNumber == orderNumber && EntityFunctions.TruncateTime(p.ReceivedDate) == receivedDate)
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
        public void LabExaminationUpdate(int labId,int patientId, DateTime receivedDate, int orderNumber, LabExamination labExamination)
        {
            LabExamination labExamOld = _myDb.LabExaminations.FirstOrDefault(p => EntityFunctions.TruncateTime(p.ReceivedDate) == receivedDate && p.OrderNumber == orderNumber && p.PatientId == patientId);
            if (labExamOld != null)
            {
                labExamOld.CreatedBy = labExamination.CreatedBy;
                labExamOld.PartnerId = labExamination.PartnerId;
                labExamOld.Diagnosis = labExamination.Diagnosis;
                labExamOld.Status = labExamination.Status;
                labExamOld.DoctorId = labExamination.DoctorId;
                labExamOld.SpecifiedDoctor = labExamination.SpecifiedDoctor;
                //Update to server
                var patient = GetPatientById(patientId);
                
                try
                {
                    Connector.UpdateExamination(labId,
                                                labExamination.ExaminationNumber,
                                                labExamination.Status,
                                                patient.FirstName,
                                                patient.Phone,
                                                patient.Age,
                                                labExamination.PartnerId,
                                                labExamination.DoctorId);
                    labExamOld.UpdatedOnServer = true;
                }
                catch (Exception)
                {
                    labExamOld.UpdatedOnServer = false;
                }
                
            }
            _myDb.SaveChanges();
        }

        public VMLabExamination GetLabExamination(string examinationNumber)
        {
            VMLabExamination labExamination = _myDb.LabExaminations.Where(p => p.ExaminationNumber == examinationNumber)
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
        public VMLabExamination GetLabExaminationById(int id)
        {
            VMLabExamination labExamination = _myDb.LabExaminations.Where(p => p.Id == id)
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
        public void LabExaminationUpdateStatus(int labExaminationId, int status)
        {
            LabExamination labExamination = _myDb.LabExaminations.FirstOrDefault(p => p.Id == labExaminationId);
            if (labExamination!= null)
            {
                labExamination.Status = status;
                try
                {
                    Connector.UpdateExaminationStatus(labExamination.ExaminationNumber, status);
                    labExamination.UpdatedOnServer = true;
                }
                catch (Exception)
                {
                    labExamination.UpdatedOnServer = false;
                }
                _myDb.SaveChanges();
            }
        }

        #endregion

        #region Result
        public void ResultInsert(int analysisId, string result, int staffId)
        {
            Result testResult = _myDb.Results.FirstOrDefault(p => p.AnalysisId == analysisId);
            if (testResult == null)
            {
                //Add result
                testResult = new Result();
                testResult.AnalysisId = analysisId;
                testResult.LastUpdated = DateTime.Now;
                testResult.LastModifiedStaffId = staffId;
                testResult.IsReportable = true;
                testResult.Value = result;
                _myDb.Results.AddObject(testResult);

                // update analysis
                Analysis analysis = _myDb.Analyses.FirstOrDefault(p => p.Id == analysisId);
                if (analysis != null)
                {
                    analysis.Status = (int)AnalysisStatusEnum.HaveResult;
                }
                _myDb.SaveChanges();
            }
            else
            {
                ResultUpdate(analysisId, testResult.Id, result, staffId);
            }
        }

        public void ResultUpdate(int analysisId, int resultId, string result, int staffId)
        {

            //Add result
            Result testResult = _myDb.Results.FirstOrDefault(p => p.Id == resultId);
            if (testResult != null)
            {
                testResult.LastUpdated = DateTime.Now;
                testResult.LastModifiedStaffId = staffId;
                testResult.IsReportable = true;
                testResult.Value = result;
            }
            // update analysis
            Analysis analysis = _myDb.Analyses.FirstOrDefault(p => p.Id == analysisId);
            if (analysis != null)
            {
                if (string.IsNullOrEmpty(result))
                    analysis.Status = (int)AnalysisStatusEnum.New;
                else
                {
                    analysis.Status = (int)AnalysisStatusEnum.HaveResult;
                }
            }
            _myDb.SaveChanges();
        }

        public List<VMTestResult> GetPatientTestResults(int orderNumber, DateTime receivedDate)
        {
            VMLabExamination labExamination = GetLabExamination(orderNumber, receivedDate);
            return GetPatientTestResults(labExamination.Id);
        }

        public List<VMTestResult> GetPatientTestResults(int labExaminationId)
        {
            List<TestResultsGet_Result> tests = _myDb.PatientTestResultsGet(labExaminationId).ToList();
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
        public List<Report_PatientResult> ReportDataPatientResult(int labExaminationId)
        {
            List<Report_PatientResult> list = _myDb.Report_PatientResult(labExaminationId).ToList();
            return list;
        }
        public List<Report_TestResultNoteBook> ReportDataTestResultNoteBook(DateTime startDate, DateTime endDate)
        {
            List<report_TestResultNoteBook_Result> list = _myDb.report_TestResultNoteBook(startDate, endDate).ToList();
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
        public List<Report_BaoCaoTaiChinh> ReportDataBaoCaoTaiChinh(DateTime startDate, DateTime endDate, int partnerId,string partnerType)
        {
            List<report_ThongKeTaiChinh_Result> list = null;
            if (partnerType == "Doctor")
            {
                list = _myDb.report_ThongKeTaiChinh_Dortor(startDate, endDate, partnerId).ToList();
            }
            else
            {
                list = _myDb.report_ThongKeTaiChinh(startDate, endDate, partnerId).ToList();
            }
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
        public LabUser GetLabUser(string userName, string password)
        {
            LabUser account = _myDb.LabUsers.FirstOrDefault(p => p.UserName == userName && p.Password == password);
            return account;
        }
        #endregion

        #region TestSectionCommission
        public TestSectionCommission GetTestSectionCommission(int id)
        {
            try
            {
                TestSectionCommission test = (from _test in _myDb.TestSectionCommissions where _test.Id == id select _test).First();
                _myDb.SaveChanges();
                return test;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void TestSectionCommissionInsert(TestSectionCommission test)
        {
            _myDb.TestSectionCommissions.AddObject(test);
            _myDb.SaveChanges();
        }

        public void TestSectionCommissionUpdate(int id, TestSectionCommission test)
        {
            TestSectionCommission currentTest = (from _test in _myDb.TestSectionCommissions where _test.Id == id select _test).First();

            currentTest.IsActive = test.IsActive;
            currentTest.Cost = test.Cost;

            _myDb.SaveChanges();
        }

        public void TestSectionCommissionDelete(int id)
        {
            TestSectionCommission test = GetTestSectionCommission(id);
            test.IsActive = false;

            _myDb.SaveChanges();
        }
        #endregion

        #region Instrument
        public List<SearchInstrumentResult_Result> InstrumentResultSearch(DateTime? receivedDate, string orderNumber, int? instrumentId)
        {
            List<SearchInstrumentResult_Result> lstInstrumentResult = _myDb.SearchInstrumentResult(receivedDate, orderNumber, instrumentId).ToList();
            return lstInstrumentResult;
        }

        public List<Instrument> GetInstruments()
        {
            List<Instrument> lstInstruments = (from _instrument in _myDb.Instruments
                                               where _instrument.IsActive == true
                                               select _instrument).ToList();
            return lstInstruments;
        }

        public List<InstrumentResult> GetAllValidInstrumentResult()
        {
            List<InstrumentResult> lstInstrumentResults = (from _insResult in _myDb.InstrumentResults
                                                           where _insResult.Flag == false
                                                           select _insResult).ToList();
            return lstInstrumentResults;
        }

        public List<InstrumentResult> GetAllValidInstrumentResultByCondition(DateTime? receivedDate, string orderNumber, int? instrumentId)
        {
            List<InstrumentResult> lstInstrumentResults = (from _insResult in _myDb.InstrumentResults
                                                           where _insResult.Flag == false
                                                             && _insResult.ReceivedDate == receivedDate
                                                             && (orderNumber == null || _insResult.OrderNumber == orderNumber)
                                                             && (instrumentId == null || _insResult.InstrumentId == instrumentId)
                                                           select _insResult).ToList();
            return lstInstrumentResults;
        }

        public List<InstrumentResult> GetAllInstrumentResultByCondition(DateTime? receivedDate, string orderNumber, int? instrumentId)
        {
            List<InstrumentResult> lstInstrumentResult = (from _insResult in _myDb.InstrumentResults
                                                           where _insResult.ReceivedDate == receivedDate
                                                             && (orderNumber == null ||_insResult.OrderNumber == orderNumber)
                                                             && (instrumentId == null || _insResult.InstrumentId == instrumentId)
                                                           select _insResult).ToList();
            return lstInstrumentResult;
        }

        public void InsertToResult(int? orderNumber, DateTime? receivedDate, int? testId, string value, int? instrumentResultId)
        {
            _myDb.Result(orderNumber, receivedDate, testId, value, instrumentResultId);
        }

        public void UpdateSID(DateTime? receivedDate, string oldOrderNumber, int newOrderNumber, int? instrumentId)
        {
            int success = _myDb.UpdateSID(oldOrderNumber, newOrderNumber, receivedDate, instrumentId);
        }
        #endregion

        #region Service
        public string SetupDoctorConnection(string connectionCode, int serverDoctorId, int clientDoctorId, string doctorConnectName)
        {

            // Kiem tra connection code có tồn tại?
            Doctor doctor = _myDb.Doctors.FirstOrDefault(p => p.ConnectionCode == connectionCode);
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
            bool isDoctorWasConnectWithLab = _myDb.Doctors.Any(p => p.ServerDoctorId.HasValue
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
            _myDb.SaveChanges();
            return "Success";
        }

        public string SetupLabConnection(string connectionCode, int serverLabId, int clientLabId, string labConnectName)
        {

            // Kiem tra connection code có tồn tại?
            Partner partner = _myDb.Partners.FirstOrDefault(p => p.ConnectionCode == connectionCode);
            if (partner == null)
            {
                //Connection error 1 = Connection code not exist
                return "Doctor_Connection_Error1";
            }

            // Kiểm tra connection code đã được dùng chưa
            if (partner.IsConnected)
            {
                // Connection error 2 = Connection code was used
                return "Doctor_Connection_Error2";
            }

            // Kiểm tra lab đã liên kết với lab chưa
            bool isLabWasConnectWithLab = _myDb.Partners.Any(p => p.ServerLabId.HasValue
                                                            && p.ServerLabId.Value == serverLabId
                                                            && p.ConnectionCode != connectionCode && p.IsConnected);
            if (isLabWasConnectWithLab)
            {
                // Connection error 3 = Doctor current has a connectiton with lab
                return "Doctor_Connection_Error3";
            }

            // Cập nhật liên kết
            partner.ServerLabId = serverLabId;
            partner.LabConnectName = labConnectName;
            partner.IsConnected = true;
            _myDb.SaveChanges();
            return "Success";
        }
        #endregion
    }
}
