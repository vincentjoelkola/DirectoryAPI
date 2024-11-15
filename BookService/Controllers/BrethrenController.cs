using BookService.Models;
using BrethrenModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BrethrenRepository;

namespace BookService.Controllers
{
    public class BrethrenController : ApiController
    {
        BrethrenDB _brethrenDB;
        public BrethrenController()
        {
            _brethrenDB = new BrethrenDB();
        }

        [Route("Assemblies")]
        [HttpGet]
        [ResponseType(typeof(GetAssembliesDTO))]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IHttpActionResult> GetAllAssemblies(string keyword="",string country="", string state="", string district = "", string city="", int pageno=1,int pagesize=50)
        {
            GetAssembliesDTO res = await _brethrenDB.GetChristianAssemblyDetails(keyword, country, state, district, city, pageno, pagesize);
            return Ok(res);
        }

        [Route("Assemblies")]
        [HttpGet]
        [ResponseType(typeof(ChristianAssemblyFullDTO))]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IHttpActionResult> GetAssemblyDetails(int id)
        {
            ChristianAssemblyFullDTO res = await _brethrenDB.GetChristianAssemblyFullDetails(id);
            return Ok(res);
        }

        [Route("Evangelists")]
        [HttpGet]
        [ResponseType(typeof(GetEvangelistDTO))]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IHttpActionResult> GetAllEvangelists(string keyword = "", string country = "", string state = "", string district = "", string city = "", int pageno = 1, int pagesize = 50)
        {
            GetEvangelistDTO res = await _brethrenDB.GetAllEvangelistsDetails(keyword, country, state, district, city, pageno, pagesize);
            return Ok(res);
        }

        [Route("Evangelists")]
        [HttpGet]
        [ResponseType(typeof(EvangelistFullDTO))]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IHttpActionResult> GetEvangelistDetails(int id)
        {
            EvangelistFullDTO res = await _brethrenDB.GetEvangelistDetails(id);
            return Ok(res); 
        }

        [Route("AvailableServices")]
        [HttpGet]
        [ResponseType(typeof(List<ServiceLanguagesDTO>))]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IHttpActionResult> GetAllAvailableServices()
        {
            List<ServiceLanguagesDTO> res = await _brethrenDB.GetAllAvailableServices();
            return Ok(res);
        }

        [Route("Assemblies")]
        [HttpPost]
        [ResponseType(typeof(ChristianAssemblyFullDTO))]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IHttpActionResult> PostAssembly(ChristianAssemblyFullDTO cassembly)
        {
            var assemblyid = await _brethrenDB.SaveAssembly(cassembly);
            return Ok(assemblyid);
        }

        [Route("Evangelists")]
        [HttpPost]
        [ResponseType(typeof(EvangelistFullDTO))]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IHttpActionResult> PostEvangelist(EvangelistFullDTO evangelist)
        {
            _brethrenDB.SaveEvangelist(evangelist);
            return Ok(evangelist);
        }

        [Route("Assemblies")]
        [HttpPut]
        [ResponseType(typeof(ChristianAssemblyDTO))]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IHttpActionResult> DeleteAssembly(int id)
        {
            _brethrenDB.DeleteAssembly(id);
            return Ok(new ChristianAssemblyDTO());
        }

        [Route("Evangelists")]
        [HttpPut]
        [ResponseType(typeof(EvangelistDTO))]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IHttpActionResult> DeleteEvangelist(int id)
        {
            _brethrenDB.DeleteEvangelist(id);
            return Ok(new EvangelistDTO());
        }

        [Route("AssemblyRequest")]
        [HttpPost]
        [ResponseType(typeof(ChristianAssemblyFullDTO))]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IHttpActionResult> PostAssemblyRequest(ChristianAssemblyFullDTO cassembly)
        {
            _brethrenDB.SaveAssemblyReq(cassembly);
            return Ok(cassembly);
        }

        [Route("AllAssemblyRequests")]
        [HttpGet]
        [ResponseType(typeof(List<ReqChristianAssemblyDTO>))]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IHttpActionResult> GetAllAssemblyRequests(string keyword = "")
        {
            List<ReqChristianAssemblyDTO> res = await _brethrenDB.GetAllAssemblyRequests(keyword);
            return Ok(res);
        }

        [Route("AssemblyRequest")]
        [HttpGet]
        [ResponseType(typeof(ChristianAssemblyFullDTO))]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IHttpActionResult> GetAssemblyRequestDetails(int id)
        {
            ChristianAssemblyFullDTO res = await _brethrenDB.GetAssemblyRequestFullDetails(id);
            return Ok(res);
        }

        [Route("ApproveAssemblyRequest")]
        [HttpGet]
        [ResponseType(typeof(ChristianAssemblyFullDTO))]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IHttpActionResult> ApproveAssemblyRequest(int id)
        {
            _brethrenDB.UpdateAssembyRequestStatus(id, 1);
            return Ok();
        }

        [Route("RejectAssemblyRequest")]
        [HttpGet]
        [ResponseType(typeof(ChristianAssemblyFullDTO))]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IHttpActionResult> RejectAssemblyRequest(int id)
        {
            _brethrenDB.UpdateAssembyRequestStatus(id, 2);
            return Ok();
        }

        [Route("SaveEditedAssemblyRequest")]
        [HttpPost]
        [ResponseType(typeof(ChristianAssemblyFullDTO))]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IHttpActionResult> SaveEditedAssemblyRequest(ChristianAssemblyFullDTO cassembly)
        {
            var assemblyid = await _brethrenDB.SaveAssembly(cassembly);
            _brethrenDB.UpdateAssembyRequestStatusOnly(cassembly.RequestId, assemblyid);
            return Ok(assemblyid);
        }

        [Route("EvangelistRequest")]
        [HttpPost]
        [ResponseType(typeof(EvangelistFullDTO))]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IHttpActionResult> PostEvagelistRequest(EvangelistFullDTO cassembly)
        {
            _brethrenDB.SaveEvangelistReq(cassembly);
            return Ok();
        }

        [Route("AllEvangelistRequests")]
        [HttpGet]
        [ResponseType(typeof(List<ReqEvangelistDTO>))]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IHttpActionResult> GetAllEvangelistRequests(string keyword = "")
        {
            List<ReqEvangelistDTO> res = await _brethrenDB.GetAllEvangelistRequests(keyword);
            return Ok(res);
        }

        [Route("EvangelistRequest")]
        [HttpGet]
        [ResponseType(typeof(EvangelistFullDTO))]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IHttpActionResult> GetEvangelistRequestDetails(int id)
        {
            EvangelistFullDTO res = await _brethrenDB.GetEvangelistRequestFullDetails(id);
            return Ok(res);
        }

        [Route("ApproveEvangelistRequest")]
        [HttpGet]
        [ResponseType(typeof(ChristianAssemblyFullDTO))]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IHttpActionResult> ApproveEvangelistRequest(int id)
        {
            _brethrenDB.UpdateEvangelistRequestStatus(id, 1);
            return Ok();
        }

        [Route("RejectEvangelistRequest")]
        [HttpGet]
        [ResponseType(typeof(ChristianAssemblyFullDTO))]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IHttpActionResult> RejectEvangelistRequest(int id)
        {
            _brethrenDB.UpdateEvangelistRequestStatus(id, 2);
            return Ok();
        }

        [Route("SaveEditedEvangelistRequest")]
        [HttpPost]
        [ResponseType(typeof(ChristianAssemblyFullDTO))]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        public async Task<IHttpActionResult> SaveEditedEvangelistRequest(EvangelistFullDTO cassembly)
        {
            var evangelistId = await _brethrenDB.SaveEvangelist(cassembly);
            _brethrenDB.UpdateEvangelistRequestStatusOnly(cassembly.RequestId, evangelistId);
            return Ok(evangelistId);
        }
    }
}
