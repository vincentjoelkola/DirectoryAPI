using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrethrenModels
{
    public class GetEvangelistDTO
    {
        public List<EvangelistDTO> Evangelists { get; set; }
        public List<string> Countries { get; set; }
        public List<string> States { get; set; }
        public List<string> Districts { get; set; }
        public List<string> Cities { get; set; }
        public int Total { get; set; }
        public int CurrentPageNo { get; set; }
        public int PageSize { get; set; }
        public int NoofPages { get; set; }
    }

    public class EvangelistDTO
    {
        public int EvangelistId { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }
        public int? Age { get; set; }
        public int AssemblyId { get; set; }
        public string AssemblyName { get; set; }
        public string Address1 { get; set; }
        public string LandMark { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }
        public string PhotoUrl { get; set; }
        public string PermanentPhoneNo { get; set; }
        public string WhatsAppNo { get; set; }
    }

    public class ReqEvangelistDTO
    {
        public int RequestId { get; set; }
        public int Status { get; set; }
        public string CreateDate { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }
        public int? Age { get; set; }
        public int AssemblyId { get; set; }
        public string AssemblyName { get; set; }
        public string Address1 { get; set; }
        public string LandMark { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }
    }
}
