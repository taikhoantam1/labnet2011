using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.JQGrid;

namespace DomainModel
{
    public class VMLabExamination
    {
        [JQColumnAttribute("", true, true, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public int ExaminationId { get; set; }

        [JQColumnAttribute("VMExmaination_ExaminationNumber", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string ExaminationNumber { get; set; }

        [JQColumnAttribute("VMExmaination_CreatedDate", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string CreatedDate { get; set; }

        [JQColumnAttribute("VMExmaination_PatientName", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string PatientName { get; set; }

        [JQColumnAttribute("VMExmaination_Phone", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string Phone { get; set; }

        [JQColumnAttribute("VMExmaination_BirthDay", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string BirthDay { get; set; }

        [JQColumnAttribute("VMExmaination_Status", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string Status { get; set; }

        [JQColumnAttribute("VMExmaination_ViewResult", true, false, false, EditTypeEnum.Link, FormatterEnum.EditLink)]
        public string ViewResultLink
        {
            get
            {
                return "/KetQuaXetNghiem/LabViewResultFromList?ExaminationNumber=" + ExaminationNumber;
            }
        }
    }
}
