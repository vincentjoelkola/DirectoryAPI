using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrethrenModels
{
    public class ChristianAssemblyFullDTO
    {
        public int AssemblyId { get; set; }
        public string AssemblyName { get; set; }
        public int NoOfPersons { get; set; }
        public ICollection<int> ServiceLanguages { get; set; }
        public ICollection<AssemblyElderDTO> AssemblyElders { get; set; }
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
        public ICollection<EvangelistDTO> Evangelists { get; set; }
        public int RequestId { get; set; }
        public int Status { get; set; }
    }

}
