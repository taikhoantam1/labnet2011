using DomainModel;
using System.Collections.Generic;

namespace LabnetClient.Models
{
    public class PartnerViewModel : BaseModel
    {
        public PartnerViewModel()
        {}
        public PartnerViewModel(VMPartner partner , List<VMTestListItem> partnerTestList)
        {
            Partner = partner;
            Autocomplete = new AutocompleteModel("Partner.TestName");
            JQGrid = new JQGridModel(typeof(VMTestListItem), true, partnerTestList, "/Partner/SavePartnerTest");
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
        /// 
        /// </summary>
        public JQGridModel JQGrid { get; set; }



    }
}