using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrethrenModels
{
    public class GetAssembliesDTO
    {
      public List<ChristianAssemblyDTO> ChristianAssemblies { get; set; }
      public List<string> Countries { get; set; }
      public List<string> States { get; set; }
      public List<string> Districts { get; set; }
      public List<string> Cities { get; set; }
      public int Total { get; set; }
      public int CurrentPageNo { get; set; }
      public int PageSize { get; set; }
      public int NoofPages { get; set; }
    }

    public class ChristianAssemblyDTO
    {
        public int AssemblyId { get; set; }
        public string AssemblyName { get; set; }
        public string Address1 { get; set; }
        public string LandMark { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }

    public class ReqChristianAssemblyDTO
    {
        public int RequestId { get; set; }
        public int Status { get; set; }
        public string CreateDate { get; set; }
        public string AssemblyName { get; set; }
        public string Address1 { get; set; }
        public string LandMark { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }

}
