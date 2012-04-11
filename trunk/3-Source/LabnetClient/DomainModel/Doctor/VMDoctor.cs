using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DomainModel.Properties;

namespace DomainModel
{
    public class VMDoctor
    {
        public VMDoctor()
        {
            Lastupdated = DateTime.Now;
            BirthDate = DateTime.Now;
            IsActive = true;
        }

        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "VMDoctor_NameRequired", ErrorMessageResourceType = typeof(Resources))]
        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Hospital { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsActive { get; set; }

        public string Degree { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Ward { get; set; }

        public string Other { get; set; }

        public DateTime Lastupdated { get; set; }

        public double? Commission { get; set; }

        public string BankAccountNumber { get; set; }

        public string ConnectionCode { get; set; }

        public bool IsConnected { get; set; }
        
    }
}
