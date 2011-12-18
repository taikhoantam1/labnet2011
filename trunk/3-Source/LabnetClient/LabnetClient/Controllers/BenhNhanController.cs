using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using DataRepository;
using DomainModel;
using LabnetClient.Models;
using LibraryFuntion;
using LabnetClient.Constant;
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
            PatientViewModel Model = new PatientViewModel(new VMPatient(), new VMLabExamination(), null, lstPartner, lstPanel);
            Model.ViewMode = ViewMode.Create;
            return View(Model);
        }
        public ActionResult Create()
        {
            List<VMPartner> lstPartner = Mapper.Map<List<Partner>, List<VMPartner>>(Repository.GetPartners());
            List<VMPanel> lstPanel = Mapper.Map<List<Panel>, List<VMPanel>>(Repository.GetPanels());
            PatientViewModel Model = new PatientViewModel(new VMPatient(), new VMLabExamination(), null, lstPartner, lstPanel);
            Model.ViewMode = ViewMode.Create;
            return View(Model);
        }

        public string SavePatientTest(List<VMPatientTest> Rows)
        {
            Session[SessionProperties.SessionPatientTestList] = Rows;
            return "Success";
        }

        [HttpPost]
        public ActionResult Create(PatientViewModel model)
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
                    //Insert patient
                    Patient patient = new Patient();
                    patient.Address = model.Patient.Address;
                    patient.BirthDate = model.Patient.BirthDate;
                    patient.Gender = model.Patient.Gender;
                    patient.FirstName = model.Patient.FirstName;
                    patient.PatientNumber = Repository.GetPatientNumber();
                    patient.IndentifierNumber = model.Patient.IndentifierNumber ?? patient.PatientNumber;
                    patient.Age = patient.BirthDate;
                    patient.Email = model.Patient.Email;
                    int patientId = Repository.PatientInsert(patient);

                    //Insert labmanagerment
                    LabExamination labExamination = new LabExamination();
                    labExamination.CreatedBy = AppHelper.GetLoginUserId();
                    labExamination.PatientId = patientId;
                    labExamination.ExaminationNumber = Repository.GetExaminationNumber();
                    labExamination.OrderNumber = model.LabExamination.OrderNumber.Value;
                    labExamination.PartnerId = model.LabExamination.PartnerId == -1 ? null : model.LabExamination.PartnerId;
                    labExamination.ReceivedDate = DateTime.Now;
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


                return View("Index");
            }

            List<VMPartner> lstPartner = Mapper.Map<List<Partner>, List<VMPartner>>(Repository.GetPartners());
            List<VMPanel> lstPanel = Mapper.Map<List<Panel>, List<VMPanel>>(Repository.GetPanels());
            PatientViewModel Model = new PatientViewModel(model.Patient, model.LabExamination, patientTests, lstPartner, lstPanel);
            return View(Model);
        }

        [HttpPost]
        public string GetPanelTests(int Id)
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
        public ActionResult Search(PatientViewModel model)
        {
            List<PatientsGets_Result> patientSearchResult= Repository.GetPatients(  model.Patient.Id,
                                                                                    model.Patient.FirstName,
                                                                                    model.Patient.Phone,
                                                                                    model.Patient.Email,
                                                                                    model.Patient.IndentifierNumber,
                                                                                    model.Patient.Address,
                                                                                    model.LabExamination.PartnerId == -1 ? null : model.LabExamination.PartnerId,
                                                                                    model.LabExamination.OrderNumber,
                                                                                    model.LabExamination.ReceivedDate);
            List<VMPatient> patients=Mapper.Map<List<PatientsGets_Result>,List<VMPatient>>(patientSearchResult);
            JQGridModel grid = new JQGridModel(typeof(VMPatient), false, patients, "");
            return View("DataTable", grid);
        }

        public ActionResult Edit(int Id, string ReceivedDate, int? OrderNumber)
        {
            DateTime receivedDate = Convert.ToDateTime(ReceivedDate);
            List<VMPartner> lstPartner = Mapper.Map<List<Partner>, List<VMPartner>>(Repository.GetPartners());
            List<VMPanel> lstPanel = Mapper.Map<List<Panel>, List<VMPanel>>(Repository.GetPanels());
            VMPatient patient = Mapper.Map<Patient, VMPatient>(Repository.GetPatient(Id,receivedDate,OrderNumber.Value));
            VMLabExamination labExamination =Repository.GetLabExamination(Id);
            List<VMPatientTest> tests = Repository.GetPatientTests(Id, receivedDate, OrderNumber.Value);
            PatientViewModel Model = new PatientViewModel(patient, labExamination, tests, lstPartner, lstPanel);
            Model.ViewMode = ViewMode.Edit;
            return View("Create",Model);
        }

        [HttpPost]
        public ActionResult  Edit(int Id ,DateTime ReceivedDate,int OrdeNumber, PatientViewModel model)
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
                    Repository.PatientUpdate(Id,patient);

                    //Update labmanagerment
                    LabExamination labExamination = new LabExamination();
                    labExamination.CreatedBy = AppHelper.GetLoginUserId();
                    labExamination.PartnerId = model.LabExamination.PartnerId == -1 ? null : model.LabExamination.PartnerId;
                    labExamination.Status = (int)LabExaminationStatusEnum.New;
                    labExamination.Diagnosis = model.LabExamination.Diagnosis;
                    Repository.LabExaminationUpdate(Id,ReceivedDate,OrdeNumber,labExamination);

                    //Insert PatientItem
                    PatientItem patientItem = new PatientItem();
                    patientItem.LastUpdated = DateTime.Now;
                    Repository.PatientItemUpdate(Id,patientItem);
                    //Update Analysis

                    //foreach (var test in patientTests)
                    //{
                    //    Analysis analysis = new Analysis();
                    //    analysis.PatientItemId = Id;
                    //    analysis.TestId = test.TestId;
                    //    analysis.Status = (int)AnalysisStatusEnum.New;
                    //    Repository.AnalysisInsert(analysis);
                    //}
                    tran.Complete();
                }


                return View("Index");
            }

            List<VMPartner> lstPartner = Mapper.Map<List<Partner>, List<VMPartner>>(Repository.GetPartners());
            List<VMPanel> lstPanel = Mapper.Map<List<Panel>, List<VMPanel>>(Repository.GetPanels());
            PatientViewModel Model = new PatientViewModel(model.Patient, model.LabExamination, patientTests, lstPartner, lstPanel);
            return View(Model);
        }
    }
}
