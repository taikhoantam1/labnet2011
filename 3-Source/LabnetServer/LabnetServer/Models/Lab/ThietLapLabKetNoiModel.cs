using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataRepository;

namespace LabnetServer.Models
{
    public class ThietLapLabKetNoiModel
    {
        public LabnetAccount LabAccount { get; set; }

        public JQGridModel DanhSachKetNoiModel { get; set; }
    }
}