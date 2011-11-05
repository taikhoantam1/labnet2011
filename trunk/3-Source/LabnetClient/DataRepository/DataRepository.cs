using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LibraryFuntion;
using System.Data.Objects.SqlClient;
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

        public void PartnerUpdate(int id, Partner updateVaule) {
            
            Partner oldPartner=GetPartnerById( id);
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

        public void InsertPanel(Panel panel)
        {
            myDb.Panels.AddObject(panel);
            myDb.SaveChanges();
        }

        public void UpdatePanel(int id, Panel panel)
        {
            Panel currentPanel = (from _panel in myDb.Panels where _panel.Id == id select _panel).First();

            currentPanel.Name = panel.Name;
            currentPanel.Description = panel.Description;
            currentPanel.IsActive = panel.IsActive;
            currentPanel.LastUpdated = panel.LastUpdated;
        }

        public void DeletePanel(int panelId)
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

        public void InsertPanelItem(PanelItem panelItem)
        {
            myDb.PanelItems.AddObject(panelItem);
            myDb.SaveChanges();
        }

        public void UpdatePanelItem(int id, PanelItem panelItem)
        {
            PanelItem currentPanelItem = (from _panelItem in myDb.PanelItems where _panelItem.Id == id select _panelItem).First();

            currentPanelItem.PanelId = panelItem.PanelId;
            currentPanelItem.TestId = panelItem.TestId;
            currentPanelItem.TestName = panelItem.TestName;
            currentPanelItem.SortOrder = panelItem.SortOrder;
            currentPanelItem.IsActive = panelItem.IsActive;
            currentPanelItem.LastUpdated = panelItem.LastUpdated;
        }

        public void DeletePanelItem(int panelItemId)
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

        public void InsertDoctor(Doctor doctor)
        {
            myDb.Doctors.AddObject(doctor);
            myDb.SaveChanges();
        }

        public void UpdateDoctor(int id, Doctor doctor)
        {
        }

        public void DeleteDoctor(int doctorId)
        {
            Doctor doctor = GetDoctor(doctorId);
            doctor.IsActive = false;
           
            myDb.SaveChanges();
        }
        #endregion

        #region TestSection


        public List<SearchTestSection_Result> GetTestSectionByName(string name)
        {
            name = name.ToUpper();
            List<SearchTestSection_Result> lstTestSection = myDb.SearchTestSection(name).ToList();

            return lstTestSection;
        }
        #endregion
    }
}
