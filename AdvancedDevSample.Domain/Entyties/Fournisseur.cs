using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedDevSample.Domain.Entyties
{
    public class Fournisseur
    {
        public Guid Id { get; private set; }
        public string CompanyName { get; private set; }
        public string ContactEmail { get; private set; }

        public Fournisseur(Guid id, string companyName, string contactEmail)
        {
            Id = id;
            CompanyName = companyName;
            ContactEmail = contactEmail;
        }
    }
}
