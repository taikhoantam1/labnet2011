﻿using System;
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

        //[JQColumnAttribute("VMPartnerSearch_Address", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        //public string Address {get;set;}
        
        [JQColumnAttribute("VMPartnerSearch_Email", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string RealEmail { get { return string.IsNullOrWhiteSpace(Email) ? "" : Email; } }

        public string Email { get; set; }

        [JQColumnAttribute("VMPartnerSearch_ConnectionCode", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string ConnectionCode { get; set; }

        [JQColumnAttribute("VMPartnerSearch_IsConnected", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string IsConnected { get; set; }

        [JQColumnAttribute("VMPartnerSearch_ConnectedLab", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string ConnectedLab { get; set; }

        [JQColumnAttribute("VMPartnerSearch_Phone", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string RealPhone { get { return string.IsNullOrWhiteSpace(Phone) ? "" : Phone; } }

        public string Phone { get; set; }

        [JQColumnAttribute("VMPartnerSearch_IsActive", true, false, false, EditTypeEnum.Text, FormatterEnum.Text)]
        public string IsActive { get; set; }
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
