using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.JQGrid;

namespace DomainModel
{
    public class VMPartnerSearch
    {
        public int Id { get; set; }

        [JQColumnAttribute("VMPartnerSearch_PartnerName", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string PartnerName { get; set; }

        [JQColumnAttribute("VMPartnerSearch_Address", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string Address {get;set;}

        [JQColumnAttribute("VMPartnerSearch_Email", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string Email { get; set; }

        [JQColumnAttribute("VMPartnerSearch_Phone", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string Phone { get; set; }
        
        /// <summary>
        /// Link edit partner use in search panel
        /// </summary>
        [JQColumnAttribute("", true, false, false, EditTypeEnum.Link, FormatterEnum.EditLink)]
        public string PartnerEditLink
        {
            get
            {
                return "/DoiTac/Edit/" + Id;
            }
        }
    }
}
