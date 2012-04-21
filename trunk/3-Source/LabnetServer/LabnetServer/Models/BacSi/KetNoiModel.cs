using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;
using DataRepository;

namespace LabnetServer.Models
{
    public class ThietLapKetNoiModel
    {
        public Doctor Doctor { get; set; }

        public JQGridModel DanhSachKetNoiModel { get; set; }


    }
}