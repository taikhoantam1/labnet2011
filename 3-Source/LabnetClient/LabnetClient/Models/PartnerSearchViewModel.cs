using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;

namespace LabnetClient.Models
{
    public class PartnerSearchViewModel : BaseModel
    {
        public VMPartnerSearch PartnerSearch { get; set; }

        public PartnerSearchViewModel()
        {
            PartnerSearch = new VMPartnerSearch();
        }
    }
}