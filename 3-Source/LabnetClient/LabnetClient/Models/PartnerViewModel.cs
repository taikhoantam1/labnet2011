using DomainModel;
using System.Collections.Generic;

namespace LabnetClient.Models
{
    public class PartnerViewModel : BaseModel
    {
        public PartnerViewModel()
        {
            Partner = new VMPartner();
            Autocomplete = new AutocompleteModel("Partner.TestName");
        }

        /// <summary>
        /// Gets or set partner info
        /// </summary>
        public VMPartner Partner { get; set; }

        /// <summary>
        /// Autocomplete model
        /// </summary>
        public AutocompleteModel Autocomplete { get; set; }

        /// <summary>
        /// Gets or sets list test assigned to partner
        /// </summary>
        public List<VMTestListItem> PartnerTestList { get; set; }
    }
}