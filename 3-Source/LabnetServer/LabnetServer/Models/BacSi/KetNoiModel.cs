using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;

namespace LabnetServer.Models
{
    public class ThietLapKetNoiModel
    {
        public VMDoctor Doctor { get; set; }

        public JQGridModel DanhSachKetNoiModel { get; set; }


    }
}