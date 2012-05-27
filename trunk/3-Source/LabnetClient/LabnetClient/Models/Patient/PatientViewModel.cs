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
        }

        public PatientViewModel(VMPatient patient,
                                VMLabExamination labExamination,
                                List<VMPatientTest> patientTest,
                                List<VMPartner> listPartner,
                                List<VMDoctor> listBacSi,
                                List<VMPanel> lstPanel,
                                List<VMTest> lstTest,
                                List<VMTestSection> lstTestSection)
        {
            Repository Repository = new DataRepository.Repository();
            Patient = patient;
            LabExamination = labExamination;
            if (LabExamination.OrderNumber == null)
                LabExamination.OrderNumber = Repository.GetLabExaminationOrderNumber();

            JQGrid = new JQGridModel(typeof(VMPatientTest), true, patientTest, "/BenhNhan/SavePatientTest");
            JQGrid.Height = 220;

            //Create ComboBox nơi gửi mẫu
            ComboBoxNoiGuiMauModel = CreateNoiGuiMauComboBox(labExamination, listPartner, listBacSi);
            //Create ComboBox Panel 
            List<OptionItem> panelData = lstPanel.Select(p => new OptionItem
                                                        {
                                                            Value = p.Id.ToString(),
                                                            Label = p.Name,
                                                            Tag = ""
                                                        }).ToList();
            ComboBoxPanelModel = new ComboBoxModel("", panelData);

            //Create ComboBox Test
            List<OptionItem> testData = lstTest.Select(p => new OptionItem
            {
                Value = p.Id.ToString(),
                Label = p.Name,
                Tag = ""
            }).ToList();
            ComboBoxTestModel = new ComboBoxModel("", testData);

            //Create ComboBox TestSection
            List<OptionItem> testSectionData = lstTestSection.Select(p => new OptionItem
            {
                Value = p.Id.ToString(),
                Label = p.Name,
                Tag = ""
            }).ToList();
            ComboBoxTestSectionModel = new ComboBoxModel("", testSectionData);

            //VMPartner partner = new VMPartner();
            //partner.Id = -1;
            //partner.Name = " ";
            //listPartner.Insert(0, partner);
            //SelectListPartner = new SelectList(listPartner, "Id", "Name");
        }

        private ComboBoxModel CreateNoiGuiMauComboBox(VMLabExamination labExamination, List<VMPartner> listPartner, List<VMDoctor> listBacSi)
        {

            List<OptionItem> noiGuiMauData = listPartner.Select(p => new OptionItem
            {
                Value = p.Id.ToString(),
                Label = p.Name,
                Tag = "Lab"
            }).ToList();

            noiGuiMauData.AddRange(
                listBacSi.Select(p => new OptionItem
                {
                    Value = p.Id.ToString(),
                    Label = p.Name,
                    Tag = "BacSi"
                }).ToList()
            );

            ComboBoxModel comboBoxNoiGuiMau = new ComboBoxModel("", noiGuiMauData);

            if (labExamination.PartnerId != null && labExamination.PartnerId != -1)
            {
                comboBoxNoiGuiMau.SelectedText = LabExamination.PartnerName;
                comboBoxNoiGuiMau.SelectedValue = labExamination.PartnerId.ToString();
            }
            else if (labExamination.DoctorId != null && labExamination.DoctorId != -1)
            {
                comboBoxNoiGuiMau.SelectedText = LabExamination.DoctorName;
                comboBoxNoiGuiMau.SelectedValue = labExamination.DoctorId.ToString();
            }
            return comboBoxNoiGuiMau;
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

        public ComboBoxModel ComboBoxNoiGuiMauModel { get; set; }

        /// <summary>
        /// Nơi gui mau la bas si (1) hay la lab gui mau (2)
        /// </summary>
        public int LoaiNoiGuiMau { get; set; }

        public int NoiGioMauId { get; set; }

        public ComboBoxModel ComboBoxPanelModel { get; set; }

        public ComboBoxModel ComboBoxTestModel { get; set; }

        public ComboBoxModel ComboBoxTestSectionModel { get; set; }

        public ViewMode ViewMode { get; set; }

        public SelectList SelectListPartner { get; set; }
    }
}