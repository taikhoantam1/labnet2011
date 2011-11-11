using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DomainModel.Properties;

namespace DomainModel
{
    public class VMPanel
    {
        public VMPanel()
        {
            Lastupdated = DateTime.Now;
        }

        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "VMPanel_NameRequired", ErrorMessageResourceType = typeof(Resources))]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime Lastupdated { get; set; }
    }
}
