using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataRepository;
using DomainModel;

namespace LabnetClient.Models
{
    public class QuanLyTaiChinhViewModel
    {

        public QuanLyTaiChinhViewModel()
        {
        }
        public QuanLyTaiChinhViewModel(    List<VMPartner> lstPartner , List<VMDoctor> lstBacSi )
        {

            //Create ComboBox nơi gửi mẫu
            ComboBoxNoiGuiMauModel = CreateNoiGuiMauComboBox(lstPartner, lstBacSi);
        }

        private ComboBoxModel CreateNoiGuiMauComboBox(List<VMPartner> listPartner, List<VMDoctor> listBacSi)
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
            return comboBoxNoiGuiMau;
        }

        /// <summary>
        /// Nơi gui mau la bas si (1) hay la lab gui mau (2)
        /// </summary>
        public int LoaiNoiGuiMau { get; set; }
        public int PartnerId { get; set; }
        public int DoctorId { get; set; }
        public int NoiGioMauId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public ReporViewModel ReportModel { get; set; }
        public SelectList DropDownListPartner { get; set; }
        public ComboBoxModel ComboBoxNoiGuiMauModel { get; set; }
    }
}