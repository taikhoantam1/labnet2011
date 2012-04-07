using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;
using DomainModel.Constant;
using System.Web.Mvc;

namespace LabnetClient.Models
{
    public class PatientTestViewModel
    {
        public PatientTestViewModel(VMPatient patient, VMLabExamination labExamination, List<VMTestResult> tests, List<VMPartner> listPartner, List<VMDoctor> listBacSi)
        {
            Patient = patient;
            LabExamination = labExamination;
            JQGrid = new JQGridModel(typeof(VMTestResult), true, tests, "/BenhNhan/SaveTestResults");
            //Create ComboBox nơi gửi mẫu
           ComboBoxNoiGuiMauModel = CreateNoiGuiMauComboBox(labExamination,listPartner, listBacSi);
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

            if (labExamination.PartnerId != null)
            {
                comboBoxNoiGuiMau.SelectedText = LabExamination.PartnerName;
                comboBoxNoiGuiMau.SelectedValue = labExamination.PartnerId.ToString();
            }
            else if (labExamination.DoctorId != null)
            {
                comboBoxNoiGuiMau.SelectedText = LabExamination.DoctorName;
                comboBoxNoiGuiMau.SelectedValue = labExamination.DoctorId.ToString();
            }
            comboBoxNoiGuiMau.IsEnabled = false;
            return comboBoxNoiGuiMau;
        }
        public VMPatient Patient { get; set; }

        public VMLabExamination LabExamination { get; set; }

        public JQGridModel JQGrid { get; set; }

        //public SelectList SelectListPartner { get; set; }

        public ComboBoxModel ComboBoxNoiGuiMauModel { get; set; }
    }
}