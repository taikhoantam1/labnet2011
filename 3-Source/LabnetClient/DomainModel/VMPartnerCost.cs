using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel
{
    public class VMPartnerCost
    {
        public int Id { get; set; }

        public int TestId { get; set; }

        public int PartnerId { get; set; }

        public decimal Cost { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime LastUpdated { get; set; }

        public VMPartnerCost()
        {
            LastUpdated = DateTime.Now;
            IsActive = true;
        }
    }
}
