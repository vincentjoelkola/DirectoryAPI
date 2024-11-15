using System;
using System.Collections.Generic;

namespace BookService.Models
{
    public class Child
    {
        public int Id { get; set; }
        public String ChildName { get; set; }
        public int Gender { get; set; }
        public int Age { get; set; }
        public int FatherId { get; set; }
        public Evangelist Father { get; set; }
    }

    public class Evangelist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public String EduQualification { get; set; }
        public string WifesName { get; set; }
        public DateTime WifesDOB { get; set; }
        public ICollection<Child> Children { get; set; }
        public DateTime DateOfCommMinistry { get; set; }
        //public int CommdAssemblyId { get; set; }
        public int AssemblyId { get; set; }
        //public ChristianAssembly CommdAssembly { get; set; }
        public ChristianAssembly Assembly { get; set; }
        public string Address1 { get; set; }
        public string LandMark { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }
        public string PermanantPhoneNo { get; set; }
        public string WhatsAppNo { get; set; }
        public string EmailAddress { get; set; }
    }
}