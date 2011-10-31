using DomainModel;

namespace LabnetClient.Models
{
    public class PartnerViewModel:BaseModel
    {
        public PartnerViewModel()
        {
            Partner = new VMPartner();   
        }

        /// <summary>
        /// Gets or set partner info
        /// </summary>
        public VMPartner Partner { get; set; }

    }
}