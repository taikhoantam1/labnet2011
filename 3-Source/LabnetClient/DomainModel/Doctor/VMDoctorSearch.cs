using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.JQGrid;

namespace DomainModel
{
    public class VMDoctorSearch
    {
        public string Name { get; set; }

        public List<DoctorSearchObject> ListSearchResult { get; set; }
    }

    public class DoctorSearchObject
    {
        public int Id { get; set; }

        [JQColumnAttribute("VMDoctorSearch_DoctorName", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string DoctorName { get; set; }

        /// <summary>
        /// Sets or gets Email
        /// </summary>
        [JQColumnAttribute("VMDoctorSearch_Email", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string Email { get; set; }

        [JQColumnAttribute("VMDoctorSearch_ConnectionCode", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string ConnectionCode { get; set; }

        [JQColumnAttribute("VMDoctorSearch_IsConnected", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string IsConnected { get; set; }

        [JQColumnAttribute("VMDoctorSearch_ConnectDoctor", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string DoctorConnectName { get; set; }

        [JQColumnAttribute("VMDoctorSearch_IsActive", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string IsActive { get; set; }

        /// <summary>
        /// Link edit partner use in search doctor
        /// </summary>
        [JQColumnAttribute("", true, false, false, EditTypeEnum.Link, FormatterEnum.EditLink)]
        public string DoctorEditLink
        {
            get
            {
                return "/BacSi/Edit/" + Id;
            }
        }
    }
}
