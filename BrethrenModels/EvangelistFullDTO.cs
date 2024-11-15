using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrethrenModels
{
    public class EvangelistFullDTO
    {
        public int EvangelistId { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }
        public string EduQualification { get; set; }
        public string WifesName { get; set; }
        public string WifesAge { get; set; }
        public int NoofChildren { get; set; }
        public ICollection<ChildDTO> Children { get; set; }
        public string DateOfCommMinistry { get; set; }
        public int CommdAssemblyId { get; set; }
        public ChristianAssemblyDTO CommdAssembly { get; set; }
        public int AssemblyId { get; set; }
        public ChristianAssemblyDTO Assembly { get; set; }
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
        public string Photo { get; set; }
        public string PhotoUrl { get; set; }
        public int RequestId { get; set; }
        public int Status { get; set; }
    }
}
