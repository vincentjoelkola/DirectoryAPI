using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookService.Models
{
    public class ServiceLanguage
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public ICollection<ChristianAssembly> ChristianAssemblies { get; set; }
    }

    public class ChristianAssembly
    {
        public int Id { get; set; }
        [Required]
        public string AssemblyName { get; set; }
        public int NoOfPersons { get; set; }
        public ICollection<ServiceLanguage> ServiceLanguages { get; set; }
        public string WorshipTime { get; set; }
        public string Address1 { get; set; }
        public string LandMark { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }
        public string PermanantPhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public ICollection<Evangelist> Evangelists { get; set; }
    }

    
}