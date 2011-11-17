using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel
{
    public class VMPartnerSearch
    {
        public string Name { get; set; }

        public List<PartnerSearchObject> ListSearchResult { get; set; }
    }

    public class PartnerSearchObject
    {
        public int Id { get; set; }

        public string PartnerName { get; set; }
    }
}
