using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;

namespace LabnetClient.Models
{
    public class PartnerSearchViewModel : BaseModel
    {
        public string PartnerName { get; set; }

        public AutocompleteModel Autocomplete { get; set; }

        public bool IsActive { get; set; }

        public PartnerSearchViewModel()
        {
            Autocomplete = new AutocompleteModel("Partner.Name");
            IsActive = true;
        }
    }
}