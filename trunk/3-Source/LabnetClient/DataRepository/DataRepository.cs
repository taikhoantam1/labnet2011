using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LibraryFuntion;
using System.Data.Objects.SqlClient;
using DomainModel;
namespace DataRepository
{
    public class Repository : IDataRepository
    {
        private LabManager_ClientEntities myDb;
        public LabManager_ClientEntities Context { get { return myDb; } }

        /// <summary>
        /// 
        /// </summary>
        public Repository()
        {
            myDb = new LabManager_ClientEntities();
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
                TestName = p.Test.Name,
                TesstId = p.TestId,
                Cost = p.Cost
            }).ToList();

            return listTest;
        }

        public List<Partner> GetPartnerByName(string name)
        {
            List<Partner> lstPartner = (from _partner in myDb.Partners where _partner.Name.ToUpper().Contains(name.ToUpper()) select _partner).ToList();
            return lstPartner;
        }

        public bool IsValidPartner(string name)
        {
            bool isValid = true;
            Partner partner = myDb.Partners.SingleOrDefault(u => u.Name.ToUpper() == name.ToUpper());
            if (partner != null)
            {
                isValid = false;
            }
            return isValid;
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
            Test testWithSameName = myDb.Tests.SingleOrDefault(u => u.Name.ToUpper() == testName.ToUpper());
            if (testWithSameName != null)
            {
                isValid = false;
            }
            return isValid;
        }

        public List<SearchTest_Result> TestSearch(string testName, string testSectionName, string panelName)
        {
            List<SearchTest_Result> lstTestSearch = new List<SearchTest_Result>();
            if (!string.IsNullOrEmpty(testSectionName) && string.IsNullOrEmpty(panelName))
            {
                lstTestSearch = myDb.SearchTest(testName.ToUpper(), testSectionName.ToUpper(), "").ToList();
            }
            if (string.IsNullOrEmpty(testSectionName) && string.IsNullOrEmpty(panelName))
            {
                lstTestSearch = myDb.SearchTest(testName.ToUpper(), "", "").ToList();
            }

            return lstTestSearch;
        }

        public object GetTestByName(string name, string searchType)
        {
            List<SearchTestByName_Result> lstTest = myDb.SearchTestByName(name, searchType.ToUpper()).ToList();
            return lstTest.Select(p => new { Label = p.Name, Value = p.Id, Tag = p.Cost });
		}

        public object GetTestByNameForPanel(string name, string searchType)
        {
            List<SearchTestByNameForPanel_Result> lstTest = myDb.SearchTestByNameForPanel(name, searchType.ToUpper()).ToList();
            return lstTest.Select(p => new { Label = p.Name, Value = p.Id, Tag = p.TestSectionName });
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
            Panel panel = myDb.Panels.SingleOrDefault(u => u.Name.ToUpper() == name.ToUpper());
            if (panel != null)
            {
                isValid = false;
            }
            return isValid;
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
            List<Panel> lstPanel = (from _panel in myDb.Panels where _panel.Name.ToUpper().Contains(name.ToUpper()) select _panel).ToList();
            return lstPanel;
        }

        public PanelItem GetPanelItemByTestIdAndPanelId(int testId, int panelId)
        {
            PanelItem panelItem = (from _item in myDb.PanelItems 
                                   where _item.PanelId == panelId && _item.TestId == testId && _item.IsActive == true 
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
                TesstId = p.TestId,
                TestSectionName = p.Test.TestSection.Name
            }).ToList();

            return listTest;
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
            Doctor doctor = myDb.Doctors.SingleOrDefault(u => u.Name.ToUpper() == name.ToUpper());
            if (doctor != null)
            {
                isValid = false;
            }
            return isValid;
        }
        #endregion

        #region TestSection


        public object GetTestSectionByName(string name, string searchType)
        {
            name = name.ToUpper();
            List<SearchTestSection_Result> lstTestSection = myDb.SearchTestSection(name, searchType.ToUpper()).ToList();
            return lstTestSection.Select(p => new { Label = p.Name, Value = p.Id });
        }

        public TestSection GetTestSection(int testSectionId)
        {
            TestSection testSection = (from _testSection in myDb.TestSections where _testSection.Id == testSectionId select _testSection).First();
            myDb.SaveChanges();
            return testSection;
        }
        #endregion
    }
}
