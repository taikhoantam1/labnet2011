using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.JQGrid;

namespace DomainModel
{
    public class VMLabConnectMapping
    {
        [JQColumnAttribute("", true, true, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public int MappingId { get; set; }
        [JQColumnAttribute("", true, true, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public int LabId { get; set; }

        [JQColumnAttribute("VMDoctorConnectMapping_LabName", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string LabName { get; set; }
        [JQColumnAttribute("VMDoctorConnectMapping_PhoneNumber", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string PhoneNumber { get; set; }
        [JQColumnAttribute("VMDoctorConnectMapping_Email", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string Address { get; set; }
        [JQColumnAttribute("VMDoctorConnectMapping_DateConnected", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string StrDateConnected { get { return DateConnected.HasValue ? DateConnected.Value.ToString("d") : ""; } }

        [JQColumnAttribute("VMDoctorConnectMapping_PatientInDay", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public int NumberPatientInDay { get; set; }
        [JQColumnAttribute("VMDoctorConnectMapping_PatientInMonth", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public int NumberPatientInMonth { get; set; }

        public DateTime? DateConnected { get; set; }
    }
}
