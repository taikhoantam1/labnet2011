using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataRepository;

namespace LabnetServer.Models
{
    public class DanhSachBenhNhanModel 
    {
        public DanhSachBenhNhanModel()
        { }
        public DanhSachBenhNhanModel(List<LabClient> labs)
        {
             List<OptionItem> labsOptions = labs.Select(p => new OptionItem
            {
                Value = p.LabId.ToString(),
                Label = p.Name,
                Tag = "Lab"
            }).ToList();

             LabComboBox = new ComboBoxModel("LabId", labsOptions);
             SentDate = DateTime.Now;
        }

        public int? LabId { get; set; }

        public DateTime SentDate { get; set; }

        public ComboBoxModel LabComboBox { get; set; }

        public JQGridModel DanhSachBenhNhanDataTableModel { get; set; }

    }
}
