using System.Collections.Generic;
using System.Web.Mvc;
using DataRepository;
using DomainModel;
using DomainModel.Constant;
using System.Collections.Generic;
using System.Linq;
using LibraryFuntion;

namespace LabnetClient.Models
{
    public class PatientViewModel : BaseModel
    {
        public PatientViewModel()
        {
            LabExamination = new VMLabExamination();
        }

        public PatientViewModel(VMPatient patient, VMLabExamination labExamination, List<VMPatientTest> patientTest, List<VMPartner> listPartner, List<VMPanel> lstPanel,List<VMTest> lstTest)
        {
            Repository Repository = new DataRepository.Repository();
            Patient =patient;
            LabExamination = labExamination;
            if(    LabExamination.OrderNumber==null)
            LabExamination.OrderNumber = Repository.GetLabExaminationOrderNumber();
            
            JQGrid = new JQGridModel(typeof(VMPatientTest), true, patientTest, "/BenhNhan/SavePatientTest");
            
            //Create Panel model
            List<OptionItem> panelData = lstPanel.Select(p => new OptionItem
                                                        {
                                                            Value = p.Id.ToString(),
                                                            Label=p.Name,
                                                            Tag = ""}).ToList();
            ComboBoxPanelModel = new ComboBoxModel("", panelData);

            //Create Test model
            List<OptionItem> testData = lstTest.Select(p => new OptionItem
            {
                Value = p.Id.ToString(),
                Label = p.Name,
                Tag = ""
            }).ToList();
            ComboBoxTestModel = new ComboBoxModel("", testData);

            VMPartner partner = new VMPartner();
            partner.Id = -1;
            partner.Name = " ";
            listPartner.Insert(0, partner);
            SelectListPartner = new SelectList(listPartner, "Id", "Name");
        }

        /// <summary>
        /// Gets or set partner info
        /// </summary>
        public VMPatient Patient { get; set; }

        /// <summary>
        /// Autocomplete model
        /// </summary>
        public VMLabExamination LabExamination { get; set; }

        /// <summary>
        /// Gets or sets list test assigned to partner
        /// </summary>
        public List<VMTestListItem> PartnerTestList { get; set; }

        public JQGridModel JQGrid { get; set; }

        public ComboBoxModel ComboBoxPanelModel { get; set; }

        public ComboBoxModel ComboBoxTestModel { get; set; }

        public ViewMode ViewMode { get; set; }

        public SelectList SelectListPartner { get; set; }
    }
}