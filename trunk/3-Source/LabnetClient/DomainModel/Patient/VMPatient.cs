using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DomainModel.Properties;
using DomainModel.JQGrid;

namespace DomainModel
{
    public class VMPatient
    {
        public VMPatient()
        {
            LabExamination = new VMLabExamination();
        }
        [JQColumnAttribute("", true, true,false, EditTypeEnum.Text, FormatterEnum.Text)]
        public int? Id { get; set; }

        [Required(ErrorMessageResourceName = "VMPatient_Name_RequireMessage", ErrorMessageResourceType = typeof(Resources))]
        [JQColumnAttribute("VMPatient_Name", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string FirstName { get; set; }

        [JQColumnAttribute("VMPatient_Age", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string Age { get; set; }

        public string PatientNumber { get; set; }


        [Required(ErrorMessageResourceName = "VMPatient_Address_RequireMessage", ErrorMessageResourceType = typeof(Resources))]
        [JQColumnAttribute("VMPatient_Address", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string Address { get; set; }

        public bool Gender { get; set; }

        [Required(ErrorMessageResourceName = "VMPatient_Birthday_RequireMessage", ErrorMessageResourceType = typeof(Resources))]
        public string BirthDate { get; set; }

        public int Status { get; set; }

        public string IndentifierNumber { get; set; }

        [JQColumnAttribute("VMPatient_Phone", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string Phone { get; set; }

        public string Email { get; set; }

        public VMLabExamination LabExamination { get; set; }
        /// <summary>
        /// Link edit panel use in search panel
        /// </summary>
        [JQColumnAttribute("", true, false, false, EditTypeEnum.Link, FormatterEnum.EditLink)]
        public string PatientEditLink
        {
            get
            {
                return string.Format("/BenhNhan/Edit/{0}?OrderNumber={1}&ReceivedDate={2}", Id, LabExamination.OrderNumber, LabExamination.ReceivedDate.Value.ToString("d"));
            }
        }
    }
}
