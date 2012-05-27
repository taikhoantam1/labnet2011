using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DomainModel.Properties;

namespace DomainModel
{
    public class VMPartner
    {

        public VMPartner()
        {
            IsActive = true;
        }
        /// <summary>
        /// Sets or gets name of lab partner
        /// </summary>
        [Required(ErrorMessageResourceName = "VMPartner_NameRequired", ErrorMessageResourceType = typeof(Resources))]
        public string Name { get; set; }

        /// <summary>
        /// Sets or gets owner of lab partner
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Sets or gets address of lab partner
        /// </summary>
        [Required(ErrorMessageResourceName = "VMPartner_AddressRequired", ErrorMessageResourceType = typeof(Resources))]
        public string Address { get; set; }

        /// <summary>
        /// Sets or gets email which will use to sent test result to lab partner
        /// </summary>
        [Required(ErrorMessageResourceName = "VMPartner_EmailRequired", ErrorMessageResourceType = typeof(Resources))]
        public string Email { get; set; }

        /// <summary>
        /// Sets or gets phone number to contact to lab partner
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Sets or gets logo will use in report hearder of test result
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// Sets or gets fax number to contact to lab partner
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// Sets or gets note to lab partner
        /// </summary>
        public string Note { get; set; }


        /// <summary>
        /// Sets or gets the value indicate the lab is use or not
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Partner id
        /// </summary>
        public int Id { get; set; }

        public string ConnectionCode { get; set; }

        public bool IsConnected { get; set; }

        //public List<VMPartnerCost> PartnerCostDetails { get; set; }
    }
}
