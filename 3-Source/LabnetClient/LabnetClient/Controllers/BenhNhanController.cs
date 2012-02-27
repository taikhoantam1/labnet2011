using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using DataRepository;
using DomainModel;
using LabnetClient.Models;
using LibraryFuntion;
using DomainModel.Constant;
using LabnetClient.App_Code;
using System;
using System.Transactions;

namespace LabnetClient.Controllers
{
    public class BenhNhanController : BaseController
    {
        public ActionResult Index()
        {

            List<VMPartner> lstPartner = Mapper.Map<List<Partner>, List<VMPartner>>(Repository.GetPartners());
            List<VMPanel> lstPanel = Mapper.Map<List<Panel>, List<VMPanel>>(Repository.GetPanels());
            List<VMTest> lstTest = Mapper.Map<List<Test>, List<VMTest>>(Repository.GetTests());
            PatientViewModel Model = new PatientViewModel(new VMPatient(), new VMLabExamination(), null, lstPartner, lstPanel,lstTest);
            Model.ViewMode = ViewMode.Create;
            return View(Model);
        }

        public ActionResult Create()
        {
            List<VMPartner> lstPartner = Mapper.Map<List<Partner>, List<VMPartner>>(Repository.GetPartners());
            List<VMPanel> lstPanel = Mapper.Map<List<Panel>, List<VMPanel>>(Repository.GetPanels());
            List<VMTest> lstTest = Mapper.Map<List<Test>, List<VMTest>>(Repository.GetTests());
            PatientViewModel Model = new PatientViewModel(new VMPatient(), new VMLabExamination(), null, lstPartner, lstPanel, lstTest);
            Model.ViewMode = ViewMode.Create;
            return View(Model);
        }

        [HttpPost]
        public ActionResult Create(PatientViewModel model)
        {
            model.LabExamination.ReceivedDate = DateTime.Now;
            List<VMPatientTest> patientTests = new List<VMPatientTest>();
            if (Session[SessionProperties.SessionPatientTestList] != null)
            {
                patientTests = (List<VMPatientTest>)Session[SessionProperties.SessionPatientTestList];
                if (!patientTests.Any(p => p.IsEnable))
                {
                    ModelState.AddModelError("TestRequired", Resources.PatientStrings.PatientCreate_TestRequired);
                }
            }
            else
            {
                ModelState.AddModelError("TestRequired", Resources.PatientStrings.PatientCreate_TestRequired);
            }

            if (ModelState.IsValid)
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    //Insert patient
                    Patient patient = new Patient();
                    patient.Address = model.Patient.Address;
                    patient.BirthDate = model.Patient.BirthDate;
                    patient.Gender = model.Patient.Gender;
                    patient.FirstName = model.Patient.FirstName;
                    patient.PatientNumber = Repository.GetPatientNumber();
                    patient.IndentifierNumber = model.Patient.IndentifierNumber ?? patient.PatientNumber;
                    if (patient.BirthDate.Length <= 4)
                        patient.Age = patient.BirthDate;
                    else
                        patient.Age = patient.BirthDate.Substring(6);
                    patient.Email = model.Patient.Email;
                    int patientId = Repository.PatientInsert(patient);

                    //Insert labmanagerment
                    LabExamination labExamination = new LabExamination();
                    labExamination.CreatedBy = AppHelper.GetLoginUserId();
                    labExamination.PatientId = patientId;
                    labExamination.ExaminationNumber = Repository.GetExaminationNumber();
                    labExamination.OrderNumber = model.LabExamination.OrderNumber.Value;
                    labExamination.PartnerId = model.LabExamination.PartnerId == -1 ? null : model.LabExamination.PartnerId;
                    labExamination.ReceivedDate = model.LabExamination.ReceivedDate ?? DateTime.Now;
                    labExamination.Status = (int)LabExaminationStatusEnum.New;
                    labExamination.Diagnosis = model.LabExamination.Diagnosis;
                    int LabExaminationId = Repository.LabExaminationInsert(labExamination);

                    //Insert PatientItem
                    PatientItem patientItem = new PatientItem();
                    patientItem.PatientId = patientId;
                    patientItem.LabExaminationId = LabExaminationId;
                    patientItem.LastUpdated = DateTime.Now.Date;
                    int patientItemId = Repository.PatientItemInsert(patientItem);
                    //Insert Analysis
                    foreach (var test in patientTests)
                    {
                        Analysis analysis = new Analysis();
                        analysis.PatientItemId = patientItemId;
                        analysis.TestId = test.TestId;
                        analysis.Status = (int)AnalysisStatusEnum.New;
                        Repository.AnalysisInsert(analysis);
                    }
                    tran.Complete();
                }
                return RedirectToAction("Create");
            }
            else
            {
                model.LabExamination.ReceivedDate = DateTime.Now.Date;
                List<VMPartner> lstPartner = Mapper.Map<List<Partner>, List<VMPartner>>(Repository.GetPartners());
                List<VMPanel> lstPanel = Mapper.Map<List<Panel>, List<VMPanel>>(Repository.GetPanels());
                List<VMTest> lstTest = Mapper.Map<List<Test>, List<VMTest>>(Repository.GetTests());
                PatientViewModel Model = new PatientViewModel(model.Patient, model.LabExamination, patientTests, lstPartner, lstPanel, lstTest);
                return View(Model);
            }
        }

        public string SavePatientTest(List<VMPatientTest> Rows)
        {
            Session[SessionProperties.SessionPatientTestList] = Rows;
            return "Success";
        }

        [HttpPost]
        public string GetPanelTests(int Id, PatientViewModel Model)
        {
            List<VMPatientTest> tests = Repository.GetPanelTest(Id)
                                                .Select(p => new VMPatientTest
                                                {
                                                    IsEnable = true,
                                                    Cost = p.Cost,
                                                    TestName = p.TestName,
                                                    Section = p.TestSectionName,
                                                    TestId = p.TestId
                                                }).ToList();
            return tests.ToJson();
        }
        [HttpPost]
        public string GetTests(int Id, PatientViewModel Model)
        {
            Test test= Repository.GetTest(Id);
            VMPatientTest tests = new VMPatientTest
                                                {
                                                    IsEnable = true,
                                                    Cost = test.Cost,
                                                    TestName = test.Name,
                                                    Section = test.TestSection.Name,
                                                    TestId = test.Id
                                                };
            return tests.ToJson();
        }
        

        [HttpPost]
        public ActionResult Search(PatientViewModel model)
        {
            List<PatientsGets_Result> patientSearchResult = Repository.GetPatients(model.Patient.Id,
                                                                                    model.Patient.FirstName,
                                                                                    model.Patient.Phone,
                                                                                    model.Patient.Email,
                                                                                    model.Patient.IndentifierNumber,
                                                                                    model.Patient.Address,
                                                                                    model.LabExamination.PartnerId == -1 ? null : model.LabExamination.PartnerId,
                                                                                    model.LabExamination.OrderNumber,
                                                                                    model.LabExamination.ReceivedDate);
            List<VMPatient> patients = Mapper.Map<List<PatientsGets_Result>, List<VMPatient>>(patientSearchResult);
            JQGridModel grid = new JQGridModel(typeof(VMPatient), false, patients, "");
            return View("DataTable", grid);
        }

        public string SearchByOrderNumber(int OrderNumber, DateTime ReceivedDate)
        {
            Patient patinent = Repository.GetPatient(ReceivedDate, OrderNumber);
            if (patinent != null)
                return patinent.Id.ToString();
            return "false";
        }

        public ActionResult Edit(int Id, int OrderNumber, string ReceivedDate)
        {
            DateTime receivedDate = Convert.ToDateTime(ReceivedDate);
            List<VMPanel> lstPanel = Mapper.Map<List<Panel>, List<VMPanel>>(Repository.GetPanels());
            VMPatient patient = Mapper.Map<Patient, VMPatient>(Repository.GetPatient(receivedDate, OrderNumber));
            VMLabExamination labExamination = Repository.GetLabExamination(OrderNumber, receivedDate);
            List<VMPatientTest> tests = Repository.GetPatientTests(Id, labExamination.Id);
            List<VMPartner> lstPartner = Mapper.Map<List<Partner>, List<VMPartner>>(Repository.GetPartners());

            List<VMTest> lstTest = Mapper.Map<List<Test>, List<VMTest>>(Repository.GetTests());
            PatientViewModel Model = new PatientViewModel(patient, labExamination, tests, lstPartner, lstPanel,lstTest);
            Model.ViewMode = ViewMode.Edit;
            return View("Create", Model);
        }

        [HttpPost]
        public ActionResult Edit(int Id, PatientViewModel model)
        {
            List<VMPatientTest> patientTests = new List<VMPatientTest>();
            if (Session[SessionProperties.SessionPatientTestList] != null)
            {
                patientTests = (List<VMPatientTest>)Session[SessionProperties.SessionPatientTestList];
                if (!patientTests.Any(p => p.IsEnable))
                {
                    ModelState.AddModelError("TestRequired", Resources.PatientStrings.PatientCreate_TestRequired);
                }
            }
            else
            {
                ModelState.AddModelError("TestRequired", Resources.PatientStrings.PatientCreate_TestRequired);
            }

            if (ModelState.IsValid)
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    //Update patient
                    Patient patient = new Patient();
                    patient.Address = model.Patient.Address;
                    patient.BirthDate = model.Patient.BirthDate;
                    patient.Gender = model.Patient.Gender;
                    patient.FirstName = model.Patient.FirstName;
                    patient.PatientNumber = Repository.GetPatientNumber();
                    patient.IndentifierNumber = model.Patient.IndentifierNumber ?? patient.PatientNumber;
                    patient.Age = patient.BirthDate;
                    patient.Email = model.Patient.Email;
                    Repository.PatientUpdate(Id, patient);

                    //Update labmanagerment
                    LabExamination labExamination = new LabExamination();
                    labExamination.CreatedBy = AppHelper.GetLoginUserId();
                    labExamination.PartnerId = model.LabExamination.PartnerId == -1 ? null : model.LabExamination.PartnerId;
                    labExamination.Status = (int)LabExaminationStatusEnum.New;
                    labExamination.Diagnosis = model.LabExamination.Diagnosis;
                    Repository.LabExaminationUpdate(Id, model.LabExamination.ReceivedDate.Value, model.LabExamination.OrderNumber.Value, labExamination);

                    //Insert PatientItem
                    PatientItem patientItem = new PatientItem();
                    patientItem.LastUpdated = DateTime.Now;
                    patientItem = Repository.PatientItemUpdate(Id, patientItem);

                    //Update Analysis

                    List<VMPatientTest> existTests = Repository.GetPatientTests(Id, model.LabExamination.Id);
                    foreach (var test in patientTests)
                    {
                        if (!existTests.Any(p => p.TestId == test.TestId))
                        {
                            Analysis analysis = new Analysis();
                            analysis.PatientItemId = patientItem.Id;
                            analysis.TestId = test.TestId;
                            analysis.Status = (int)AnalysisStatusEnum.New;
                            Repository.AnalysisInsert(analysis);
                        }
                    }
                    tran.Complete();
                }
                return RedirectToAction("Index");
            }

            List<VMPartner> lstPartner = Mapper.Map<List<Partner>, List<VMPartner>>(Repository.GetPartners());
            List<VMPanel> lstPanel = Mapper.Map<List<Panel>, List<VMPanel>>(Repository.GetPanels());
            List<VMTest> lstTest = Mapper.Map<List<Test>, List<VMTest>>(Repository.GetTests());
            PatientViewModel Model = new PatientViewModel(model.Patient, model.LabExamination, patientTests, lstPartner, lstPanel,lstTest);
            Model.ViewMode = ViewMode.Edit;
            return View("Create", Model);
        }

        public ActionResult PatientTestResult(int? OrderNumber, string ReceivedDate)
        {

            DateTime? receivedDate = null;
            if (!string.IsNullOrEmpty(ReceivedDate))
                receivedDate = Convert.ToDateTime(ReceivedDate);
            VMLabExamination labExamination = new VMLabExamination();
            VMPatient patient = new VMPatient();
            List<VMTestResult> tests = new List<VMTestResult>();
            if ((receivedDate != null && OrderNumber != null))
            {
                patient = Mapper.Map<Patient, VMPatient>(Repository.GetPatient(receivedDate.Value, OrderNumber.Value));
                labExamination = Repository.GetLabExamination(OrderNumber.Value, receivedDate.Value);
                tests = Repository.GetPatientTestResults(OrderNumber.Value, receivedDate.Value);
            }
            else
            {
            }
            List<VMPartner> lstPartner = Mapper.Map<List<Partner>, List<VMPartner>>(Repository.GetPartners());
            PatientTestViewModel model = new PatientTestViewModel(patient, labExamination, tests, lstPartner);
            return View(model);
        }

        public string SaveTestResults(List<VMTestResult> Rows)
        {
            using (TransactionScope tran = new TransactionScope())
            {
                foreach (var result in Rows)
                {
                    if (result.Status == (int)AnalysisStatusEnum.New && !String.IsNullOrEmpty(result.Result))
                    {
                        //Add new result and update analysis status
                        Repository.ResultInsert(result.AnalysisId, result.Result, AppHelper.GetLoginUserId());

                    }
                    else if (result.Status == (int)AnalysisStatusEnum.HaveResult )
                    {
                        //Update result
                        Repository.ResultUpdate(result.AnalysisId, result.ResultId.Value, result.Result, AppHelper.GetLoginUserId());

                    }
                }
                tran.Complete();
            }
            return "Success";
        }
        [HttpPost]
        public string TestResultApproved(int LabExaminationId)
        {

            using (TransactionScope tran = new TransactionScope())
            {
                List<VMTestResult> tests=Repository.GetPatientTestResults(LabExaminationId);
                foreach (var result in tests)
                {
                    if (result.Status == (int)AnalysisStatusEnum.HaveResult)
                    {
                        //update analysis status
                        Repository.AnalysisApproved(result.AnalysisId, AppHelper.GetLoginUserId());
                    }
                }
                tran.Complete();
            }
            return "Success";
        }
    }
}
