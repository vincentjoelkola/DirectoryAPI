using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BrethrenModels;


namespace BrethrenRepository
{
    public class BrethrenDB
    {
        string connectionString = ConfigurationManager.ConnectionStrings["BookServiceContext"].ConnectionString;

        public async Task<GetAssembliesDTO> GetChristianAssemblyDetails(string keyword, string country, string state, string district,
            string city, int pageno, int pageSize)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                GetAssembliesDTO getAssembliesDTO = new GetAssembliesDTO();
                getAssembliesDTO.ChristianAssemblies = new List<ChristianAssemblyDTO>();
                getAssembliesDTO.Countries = new List<string>();
                getAssembliesDTO.States = new List<string>();
                getAssembliesDTO.Districts = new List<string>();
                getAssembliesDTO.Cities = new List<string>();

                int offset = (pageno - 1) * pageSize;

                SqlCommand cmd = new SqlCommand("GetAllAssemblies", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@keyword";
                parameter1.SqlDbType = SqlDbType.NVarChar;
                parameter1.Direction = ParameterDirection.Input;
                parameter1.Value = ConvertNullToEmpty(keyword);
                cmd.Parameters.Add(parameter1);

                SqlParameter parameter2 = new SqlParameter();
                parameter2.ParameterName = "@city";
                parameter2.SqlDbType = SqlDbType.NVarChar;
                parameter2.Direction = ParameterDirection.Input;
                parameter2.Value = ConvertNullToEmpty(city);
                cmd.Parameters.Add(parameter2);

                SqlParameter parameter3 = new SqlParameter();
                parameter3.ParameterName = "@district";
                parameter3.SqlDbType = SqlDbType.NVarChar;
                parameter3.Direction = ParameterDirection.Input;
                parameter3.Value = ConvertNullToEmpty(district);
                cmd.Parameters.Add(parameter3);

                SqlParameter parameter4 = new SqlParameter();
                parameter4.ParameterName = "@state";
                parameter4.SqlDbType = SqlDbType.NVarChar;
                parameter4.Direction = ParameterDirection.Input;
                parameter4.Value = ConvertNullToEmpty(state);
                cmd.Parameters.Add(parameter4);

                SqlParameter parameter5 = new SqlParameter();
                parameter5.ParameterName = "@country";
                parameter5.SqlDbType = SqlDbType.NVarChar;
                parameter5.Direction = ParameterDirection.Input;
                parameter5.Value = ConvertNullToEmpty(country);
                cmd.Parameters.Add(parameter5);

                SqlParameter parameter6 = new SqlParameter();
                parameter6.ParameterName = "@pageno";
                parameter6.SqlDbType = SqlDbType.Int;
                parameter6.Direction = ParameterDirection.Input;
                parameter6.Value = pageno;
                cmd.Parameters.Add(parameter6);

                SqlParameter parameter7 = new SqlParameter();
                parameter7.ParameterName = "@pagesize";
                parameter7.SqlDbType = SqlDbType.Int;
                parameter7.Direction = ParameterDirection.Input;
                parameter7.Value = pageSize;
                cmd.Parameters.Add(parameter7);

                SqlParameter parameter8 = new SqlParameter();
                parameter8.ParameterName = "@total";
                parameter8.SqlDbType = SqlDbType.Int;
                parameter8.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parameter8);



                /*string command = "select AssemblyId, AssemblyName, Address1, city, district, state, country from ChristianAssembly";

                if (!string.IsNullOrWhiteSpace(keyword) && keyword.Length > 2)
                {
                    keyword = keyword.ToLower();
                    command = @"select AssemblyId, AssemblyName, Address1, city, district, state, country from ChristianAssembly";
                    command += " where lower(AssemblyName) like '%" + keyword + "%' or ";
                    command += "lower(Address1) like '%" + keyword + "%' or ";
                    command += "lower(LandMark) like '%" + keyword + "%' or ";
                    command += "lower(City) like '%" + keyword + "%' or ";
                    command += "lower(district) like '%" + keyword + "%' or ";
                    command += "lower(State) like '%" + keyword + "%' or ";
                    command += "lower(Country) like '%" + keyword + "%' or ";
                    command += "lower(PinCode) like '%" + keyword + "%'";
                }*/


                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                await Task.Run(() => sqlDataAdapter.Fill(ds));
                getAssembliesDTO.Total = int.Parse(parameter8.Value.ToString());
                getAssembliesDTO.ChristianAssemblies.AddRange(ds.Tables[0].AsEnumerable()
                .Select(dataRow => new ChristianAssemblyDTO
                {
                    AssemblyName = dataRow.Field<string>("AssemblyName"),
                    AssemblyId = dataRow.Field<int>("AssemblyId"),
                    Address1 = dataRow.Field<string>("Address1"),
                    City = dataRow.Field<string>("city"),
                    District = dataRow.Field<string>("district"),
                    State = dataRow.Field<string>("state"),
                    Country = dataRow.Field<string>("country"),
                }).ToList());

                getAssembliesDTO.Countries.AddRange(ds.Tables[1].AsEnumerable()
               .Select(dataRow => dataRow.Field<string>("country")).ToList());

                getAssembliesDTO.States.AddRange(ds.Tables[2].AsEnumerable()
               .Select(dataRow => dataRow.Field<string>("state")).ToList());

                getAssembliesDTO.Districts.AddRange(ds.Tables[3].AsEnumerable()
               .Select(dataRow => dataRow.Field<string>("district")).ToList());

                getAssembliesDTO.Cities.AddRange(ds.Tables[4].AsEnumerable()
               .Select(dataRow => dataRow.Field<string>("city")).ToList());


                getAssembliesDTO.CurrentPageNo = pageno;
                getAssembliesDTO.PageSize = pageSize;
                double pagesizedbl = Convert.ToDouble(pageSize);
                var remainder = Math.Ceiling(getAssembliesDTO.Total / pagesizedbl);
                getAssembliesDTO.NoofPages = Convert.ToInt16(remainder);
                return getAssembliesDTO;
            }
        }

        public async Task<GetEvangelistDTO> GetAllEvangelistsDetails(string keyword, string country, string state, string district,
          string city, int pageno, int pageSize)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                GetEvangelistDTO getEvangelistsDTO = new GetEvangelistDTO();
                getEvangelistsDTO.Evangelists = new List<EvangelistDTO>();
                getEvangelistsDTO.Countries = new List<string>();
                getEvangelistsDTO.States = new List<string>();
                getEvangelistsDTO.Districts = new List<string>();
                getEvangelistsDTO.Cities = new List<string>();

                int offset = (pageno - 1) * pageSize;

                SqlCommand cmd = new SqlCommand("GetAllEvangelists", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@keyword";
                parameter1.SqlDbType = SqlDbType.NVarChar;
                parameter1.Direction = ParameterDirection.Input;
                parameter1.Value = ConvertNullToEmpty(keyword);
                cmd.Parameters.Add(parameter1);

                SqlParameter parameter2 = new SqlParameter();
                parameter2.ParameterName = "@city";
                parameter2.SqlDbType = SqlDbType.NVarChar;
                parameter2.Direction = ParameterDirection.Input;
                parameter2.Value = ConvertNullToEmpty(city);
                cmd.Parameters.Add(parameter2);

                SqlParameter parameter3 = new SqlParameter();
                parameter3.ParameterName = "@district";
                parameter3.SqlDbType = SqlDbType.NVarChar;
                parameter3.Direction = ParameterDirection.Input;
                parameter3.Value = ConvertNullToEmpty(district);
                cmd.Parameters.Add(parameter3);

                SqlParameter parameter4 = new SqlParameter();
                parameter4.ParameterName = "@state";
                parameter4.SqlDbType = SqlDbType.NVarChar;
                parameter4.Direction = ParameterDirection.Input;
                parameter4.Value = ConvertNullToEmpty(state);
                cmd.Parameters.Add(parameter4);

                SqlParameter parameter5 = new SqlParameter();
                parameter5.ParameterName = "@country";
                parameter5.SqlDbType = SqlDbType.NVarChar;
                parameter5.Direction = ParameterDirection.Input;
                parameter5.Value = ConvertNullToEmpty(country);
                cmd.Parameters.Add(parameter5);

                SqlParameter parameter6 = new SqlParameter();
                parameter6.ParameterName = "@pageno";
                parameter6.SqlDbType = SqlDbType.Int;
                parameter6.Direction = ParameterDirection.Input;
                parameter6.Value = pageno;
                cmd.Parameters.Add(parameter6);

                SqlParameter parameter7 = new SqlParameter();
                parameter7.ParameterName = "@pagesize";
                parameter7.SqlDbType = SqlDbType.Int;
                parameter7.Direction = ParameterDirection.Input;
                parameter7.Value = pageSize;
                cmd.Parameters.Add(parameter7);

                SqlParameter parameter8 = new SqlParameter();
                parameter8.ParameterName = "@total";
                parameter8.SqlDbType = SqlDbType.Int;
                parameter8.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parameter8);


                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                await Task.Run(() => sqlDataAdapter.Fill(ds));
                getEvangelistsDTO.Total = int.Parse(parameter8.Value.ToString());
                getEvangelistsDTO.Evangelists.AddRange(ds.Tables[0].AsEnumerable()
                .Select(dataRow => new EvangelistDTO
                {
                    EvangelistId = dataRow.Field<int>("EvangelistId"),
                    Name = dataRow.Field<string>("EvangelistName"),
                    DOB = dataRow.Field<string>("dob"),
                    Age = GetEvangelistAge(dataRow.Field<string>("dob")), //dataRow.Field<int>("Age"),
                    AssemblyId = dataRow.Field<int>("AssemblyId"),
                    AssemblyName = dataRow.Field<string>("AssemblyName"),
                    Address1 = dataRow.Field<string>("Address1"),
                    City = dataRow.Field<string>("city"),
                    District = dataRow.Field<string>("district"),
                    State = dataRow.Field<string>("state"),
                    Country = dataRow.Field<string>("country"),
                    PinCode = dataRow.Field<string>("PinCode"),
                    LandMark = dataRow.Field<string>("LandMark"),
                    PhotoUrl = dataRow.Field<string>("Photo"),
                }).ToList());

                getEvangelistsDTO.Countries.AddRange(ds.Tables[1].AsEnumerable()
               .Select(dataRow => dataRow.Field<string>("country")).ToList());

                getEvangelistsDTO.States.AddRange(ds.Tables[2].AsEnumerable()
               .Select(dataRow => dataRow.Field<string>("state")).ToList());

                getEvangelistsDTO.Districts.AddRange(ds.Tables[3].AsEnumerable()
               .Select(dataRow => dataRow.Field<string>("district")).ToList());

                getEvangelistsDTO.Cities.AddRange(ds.Tables[4].AsEnumerable()
               .Select(dataRow => dataRow.Field<string>("city")).ToList());


                getEvangelistsDTO.CurrentPageNo = pageno;
                getEvangelistsDTO.PageSize = pageSize;
                double pagesizedbl = Convert.ToDouble(pageSize);
                var remainder = Math.Ceiling(getEvangelistsDTO.Total / pagesizedbl);
                getEvangelistsDTO.NoofPages = Convert.ToInt16(remainder);
                return getEvangelistsDTO;
            }
        }

        public async Task<List<EvangelistDTO>> GetAllEvangelists(string keyword)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string command = @"SELECT e.EvangelistId,  e.EvangelistName,   e.DOB,
                                   e.AssemblyId,  c.AssemblyName,
                                   e.Address1 ,e.LandMark
                                  ,e.City ,e.District
                                  ,e.State
                                  ,e.Country
                                  ,e.PinCode
                              FROM Evangelists e left join ChristianAssembly c  on e.AssemblyId = c.AssemblyId";

                if (!string.IsNullOrWhiteSpace(keyword) && keyword.Length > 2)
                {
                    keyword = keyword.ToLower();
                    command = @"SELECT e.EvangelistId,  e.EvangelistName,   e.DOB,
                                   e.AssemblyId,  c.AssemblyName,
                                   e.Address1 ,e.LandMark
                                  ,e.City ,e.District
                                  ,e.State
                                  ,e.Country
                                  ,e.PinCode
                              FROM Evangelists e left join ChristianAssembly c  on e.AssemblyId = c.AssemblyId";
                    command += " where lower(e.EvangelistName) like '%" + keyword + "%' or ";
                    command += "lower(c.AssemblyName) like '%" + keyword + "%' or ";
                    command += "lower(e.Address1) like '%" + keyword + "%' or ";
                    command += "lower(e.LandMark) like '%" + keyword + "%' or ";
                    command += "lower(e.City) like '%" + keyword + "%' or ";
                    command += "lower(e.District) like '%" + keyword + "%' or ";
                    command += "lower(e.State) like '%" + keyword + "%' or ";
                    command += "lower(e.Country) like '%" + keyword + "%' or ";
                    command += "lower(e.PinCode) like '%" + keyword + "%'";
                }


                //CONVERT(int,ROUND(DATEDIFF(hour, e.DOB,GETDATE())/8766.0,0)) AS Age,

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command, connection);
                DataSet ds = new DataSet();
                await Task.Run(() => sqlDataAdapter.Fill(ds));
                var evangelistSimpleDTO = ds.Tables[0].AsEnumerable()
                .Select(dataRow => new EvangelistDTO
                {
                    EvangelistId = dataRow.Field<int>("EvangelistId"),
                    Name = dataRow.Field<string>("EvangelistName"),
                    DOB = dataRow.Field<string>("dob"),
                    Age = GetEvangelistAge(dataRow.Field<string>("dob")), //dataRow.Field<int>("Age"),
                    AssemblyId = dataRow.Field<int>("AssemblyId"),
                    AssemblyName = dataRow.Field<string>("AssemblyName"),
                    Address1 = dataRow.Field<string>("Address1"),
                    City = dataRow.Field<string>("city"),
                    District = dataRow.Field<string>("district"),
                    State = dataRow.Field<string>("state"),
                    Country = dataRow.Field<string>("country"),
                    PinCode = dataRow.Field<string>("PinCode"),
                    LandMark = dataRow.Field<string>("LandMark"),
                }).ToList();
                return evangelistSimpleDTO;
            }
        }

        public int? GetEvangelistAge(string dateStr)
        {
            int? age = null;
            if (!string.IsNullOrWhiteSpace(dateStr))
            {
                DateTime agedt = DateTime.ParseExact(dateStr, "dd/mm/yyyy", null);
                age = DateTime.Now.Year - agedt.Year;
                if (DateTime.Now.DayOfYear < agedt.DayOfYear)
                    age = age - 1;
            }
            return age;
        }

        public async Task<ChristianAssemblyFullDTO> GetChristianAssemblyFullDetails(int assemblyId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var christianAssemblyFullDTO = new ChristianAssemblyFullDTO();
                SqlCommand cmd = new SqlCommand("GetAssemblyFullDetails", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@AssemblyId";
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = assemblyId;
                cmd.Parameters.Add(parameter);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                await Task.Run(() => sqlDataAdapter.Fill(ds));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    christianAssemblyFullDTO.AssemblyId = ds.Tables[0].Rows[0].Field<int>("AssemblyId");
                    christianAssemblyFullDTO.AssemblyName = ds.Tables[0].Rows[0].Field<string>("AssemblyName");
                    christianAssemblyFullDTO.NoOfPersons = ds.Tables[0].Rows[0].Field<int>("NoOfPersons");
                    christianAssemblyFullDTO.WorshipTime = ds.Tables[0].Rows[0].Field<string>("WorshipTime");
                    christianAssemblyFullDTO.Address1 = ds.Tables[0].Rows[0].Field<string>("Address1");
                    christianAssemblyFullDTO.LandMark = ds.Tables[0].Rows[0].Field<string>("LandMark");
                    christianAssemblyFullDTO.City = ds.Tables[0].Rows[0].Field<string>("city");
                    christianAssemblyFullDTO.District = ds.Tables[0].Rows[0].Field<string>("District");
                    christianAssemblyFullDTO.State = ds.Tables[0].Rows[0].Field<string>("State");
                    christianAssemblyFullDTO.Country = ds.Tables[0].Rows[0].Field<string>("Country");
                    christianAssemblyFullDTO.PinCode = ds.Tables[0].Rows[0].Field<string>("PinCode");
                    christianAssemblyFullDTO.PermanantPhoneNo = ds.Tables[0].Rows[0].Field<string>("PermanantPhoneNo");
                    christianAssemblyFullDTO.EmailAddress = ds.Tables[0].Rows[0].Field<string>("EmailAddress");
                }

                christianAssemblyFullDTO.ServiceLanguages = ds.Tables[1].AsEnumerable()
                .Select(dataRow => dataRow.Field<int>("ServiceId")).ToList();


                christianAssemblyFullDTO.AssemblyElders = ds.Tables[2].AsEnumerable()
                .Select(dataRow => new AssemblyElderDTO
                {
                    AssemblyElderId = dataRow.Field<int>("AssemblyElderId"),
                    ElderName = dataRow.Field<string>("ElderName"),
                    ElderPhone = dataRow.Field<string>("PhoneNo"),
                }).ToList();

                christianAssemblyFullDTO.Evangelists = ds.Tables[3].AsEnumerable()
                .Select(dataRow => new EvangelistDTO
                {
                    EvangelistId = dataRow.Field<int>("EvangelistId"),
                    Name = dataRow.Field<string>("EvangelistName"),
                    DOB = dataRow.Field<string>("dob"),
                    AssemblyId = dataRow.Field<int>("AssemblyId"),
                    Address1 = dataRow.Field<string>("Address1"),
                    LandMark = dataRow.Field<string>("LandMark"),
                    City = dataRow.Field<string>("City"),
                    State = dataRow.Field<string>("State"),
                    District = dataRow.Field<string>("District"),
                    Country = dataRow.Field<string>("Country"),
                    PinCode = dataRow.Field<string>("PinCode"),
                    PermanentPhoneNo = dataRow.Field<string>("PermanentPhoneNo"),
                    WhatsAppNo = dataRow.Field<string>("WhatsAppNo"),
                }).ToList();

                return christianAssemblyFullDTO;
            }
        }

        public async Task<EvangelistFullDTO> GetEvangelistDetails(int evangelistId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var evangelistFullDTO = new EvangelistFullDTO();
                SqlCommand cmd = new SqlCommand("GetEvangelistFullDetails", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@evangelistId";
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = evangelistId;
                cmd.Parameters.Add(parameter);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                await Task.Run(() => sqlDataAdapter.Fill(ds));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    evangelistFullDTO.EvangelistId = ds.Tables[0].Rows[0].Field<int>("EvangelistId");
                    evangelistFullDTO.Name = ds.Tables[0].Rows[0].Field<string>("EvangelistName");
                    evangelistFullDTO.DOB = ds.Tables[0].Rows[0].Field<string>("DOB"); //.ToString("dd/MM/yyyy");
                    evangelistFullDTO.EduQualification = ds.Tables[0].Rows[0].Field<string>("EduQualification");
                    evangelistFullDTO.WifesName = ds.Tables[0].Rows[0].Field<string>("WifesName");
                    evangelistFullDTO.WifesAge = ds.Tables[0].Rows[0].Field<string>("WifesAge");
                    evangelistFullDTO.NoofChildren = ds.Tables[0].Rows[0].Field<int>("NoOfChildren");
                    evangelistFullDTO.AssemblyId = ds.Tables[0].Rows[0].Field<int>("AssemblyId");
                    evangelistFullDTO.CommdAssemblyId = ds.Tables[0].Rows[0].Field<int>("CommdAssemblyId");
                    evangelistFullDTO.DateOfCommMinistry = ds.Tables[0].Rows[0].Field<string>("DateOfCommMinistry"); //.ToString("dd/MM/yyyy");
                    evangelistFullDTO.Address1 = ds.Tables[0].Rows[0].Field<string>("Address1");
                    evangelistFullDTO.LandMark = ds.Tables[0].Rows[0].Field<string>("LandMark");
                    evangelistFullDTO.City = ds.Tables[0].Rows[0].Field<string>("city");
                    evangelistFullDTO.District = ds.Tables[0].Rows[0].Field<string>("District");
                    evangelistFullDTO.State = ds.Tables[0].Rows[0].Field<string>("State");
                    evangelistFullDTO.Country = ds.Tables[0].Rows[0].Field<string>("Country");
                    evangelistFullDTO.PinCode = ds.Tables[0].Rows[0].Field<string>("PinCode");
                    evangelistFullDTO.PermanantPhoneNo = ds.Tables[0].Rows[0].Field<string>("PermanentPhoneNo");
                    evangelistFullDTO.EmailAddress = ds.Tables[0].Rows[0].Field<string>("Email");
                    evangelistFullDTO.WhatsAppNo = ds.Tables[0].Rows[0].Field<string>("WhatsAppNo");
                    evangelistFullDTO.PhotoUrl = ds.Tables[0].Rows[0].Field<string>("Photo");
                }

                evangelistFullDTO.Children = ds.Tables[1].AsEnumerable()
                .Select(dataRow => new ChildDTO
                {
                    ChildId = dataRow.Field<int>("ChildId"),
                    ChildName = dataRow.Field<string>("ChildName"),
                    Age = dataRow.Field<string>("Age"),
                }).ToList();

                if (ds.Tables[2].Rows.Count > 0)
                {
                    ChristianAssemblyDTO christianAssemblyDTO = new ChristianAssemblyDTO();
                    christianAssemblyDTO.AssemblyId = ds.Tables[2].Rows[0].Field<int>("AssemblyId");
                    christianAssemblyDTO.AssemblyName = ds.Tables[2].Rows[0].Field<string>("AssemblyName");
                    christianAssemblyDTO.Address1 = ds.Tables[2].Rows[0].Field<string>("Address1");
                    christianAssemblyDTO.LandMark = ds.Tables[2].Rows[0].Field<string>("LandMark");
                    christianAssemblyDTO.City = ds.Tables[2].Rows[0].Field<string>("city");
                    christianAssemblyDTO.District = ds.Tables[2].Rows[0].Field<string>("District");
                    christianAssemblyDTO.State = ds.Tables[2].Rows[0].Field<string>("State");
                    christianAssemblyDTO.Country = ds.Tables[2].Rows[0].Field<string>("Country");
                    evangelistFullDTO.Assembly = christianAssemblyDTO;
                }

                if (ds.Tables[3].Rows.Count > 0)
                {
                    ChristianAssemblyDTO christianAssemblyDTO = new ChristianAssemblyDTO();
                    christianAssemblyDTO.AssemblyId = ds.Tables[3].Rows[0].Field<int>("AssemblyId");
                    christianAssemblyDTO.AssemblyName = ds.Tables[3].Rows[0].Field<string>("AssemblyName");
                    christianAssemblyDTO.Address1 = ds.Tables[3].Rows[0].Field<string>("Address1");
                    christianAssemblyDTO.LandMark = ds.Tables[3].Rows[0].Field<string>("LandMark");
                    christianAssemblyDTO.City = ds.Tables[3].Rows[0].Field<string>("city");
                    christianAssemblyDTO.District = ds.Tables[3].Rows[0].Field<string>("District");
                    christianAssemblyDTO.State = ds.Tables[3].Rows[0].Field<string>("State");
                    christianAssemblyDTO.Country = ds.Tables[3].Rows[0].Field<string>("Country");
                    evangelistFullDTO.CommdAssembly = christianAssemblyDTO;
                }

                return evangelistFullDTO;
            }
        }

        public async Task<List<ServiceLanguagesDTO>> GetAllAvailableServices()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string command = "select ServiceId, ServiceLanguage from ServiceLanguages;";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command, connection);
                DataSet ds = new DataSet();
                await Task.Run(() => sqlDataAdapter.Fill(ds));
                var christianAssemblyDTO = ds.Tables[0].AsEnumerable()
                .Select(dataRow => new ServiceLanguagesDTO
                {
                    ServiceId = dataRow.Field<int>("ServiceId"),
                    ServiceLanguage = dataRow.Field<string>("ServiceLanguage"),
                }).ToList();
                return christianAssemblyDTO;
            }
        }

        public async Task<int> SaveAssembly(ChristianAssemblyFullDTO assemblyFullDTO)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var evangelistFullDTO = new EvangelistFullDTO();
                SqlCommand cmd = new SqlCommand("SaveAssemblyDetails", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@AssemblyId";
                parameter1.SqlDbType = SqlDbType.Int;
                parameter1.Direction = ParameterDirection.Input;
                parameter1.Value = assemblyFullDTO.AssemblyId;
                cmd.Parameters.Add(parameter1);

                SqlParameter parameter2 = new SqlParameter();
                parameter2.ParameterName = "@AssemblyName";
                parameter2.SqlDbType = SqlDbType.NVarChar;
                parameter2.Direction = ParameterDirection.Input;
                parameter2.Value = ConvertNullToEmpty(assemblyFullDTO.AssemblyName);
                cmd.Parameters.Add(parameter2);

                SqlParameter parameter3 = new SqlParameter();
                parameter3.ParameterName = "@NoOfPersons";
                parameter3.SqlDbType = SqlDbType.Int;
                parameter3.Direction = ParameterDirection.Input;
                parameter3.Value = assemblyFullDTO.NoOfPersons;
                cmd.Parameters.Add(parameter3);

                SqlParameter parameter4 = new SqlParameter();
                parameter4.ParameterName = "@WorshipTime";
                parameter4.SqlDbType = SqlDbType.NVarChar;
                parameter4.Direction = ParameterDirection.Input;
                parameter4.Value = ConvertNullToEmpty(assemblyFullDTO.WorshipTime);
                cmd.Parameters.Add(parameter4);

                SqlParameter parameter5 = new SqlParameter();
                parameter5.ParameterName = "@Address1";
                parameter5.SqlDbType = SqlDbType.NVarChar;
                parameter5.Direction = ParameterDirection.Input;
                parameter5.Value = ConvertNullToEmpty(assemblyFullDTO.Address1);
                cmd.Parameters.Add(parameter5);

                SqlParameter parameter6 = new SqlParameter();
                parameter6.ParameterName = "@LandMark";
                parameter6.SqlDbType = SqlDbType.NVarChar;
                parameter6.Direction = ParameterDirection.Input;
                parameter6.Value = ConvertNullToEmpty(assemblyFullDTO.LandMark);
                cmd.Parameters.Add(parameter6);

                SqlParameter parameter7 = new SqlParameter();
                parameter7.ParameterName = "@City";
                parameter7.SqlDbType = SqlDbType.NVarChar;
                parameter7.Direction = ParameterDirection.Input;
                parameter7.Value = ConvertNullToEmpty(assemblyFullDTO.City);
                cmd.Parameters.Add(parameter7);

                SqlParameter parameter8 = new SqlParameter();
                parameter8.ParameterName = "@District";
                parameter8.SqlDbType = SqlDbType.NVarChar;
                parameter8.Direction = ParameterDirection.Input;
                parameter8.Value = ConvertNullToEmpty(assemblyFullDTO.District);
                cmd.Parameters.Add(parameter8);

                SqlParameter parameter9 = new SqlParameter();
                parameter9.ParameterName = "@State";
                parameter9.SqlDbType = SqlDbType.NVarChar;
                parameter9.Direction = ParameterDirection.Input;
                parameter9.Value = ConvertNullToEmpty(assemblyFullDTO.State);
                cmd.Parameters.Add(parameter9);

                SqlParameter parameter10 = new SqlParameter();
                parameter10.ParameterName = "@Country";
                parameter10.SqlDbType = SqlDbType.NVarChar;
                parameter10.Direction = ParameterDirection.Input;
                parameter10.Value = ConvertNullToEmpty(assemblyFullDTO.Country);
                cmd.Parameters.Add(parameter10);

                SqlParameter parameter11 = new SqlParameter();
                parameter11.ParameterName = "@PinCode";
                parameter11.SqlDbType = SqlDbType.NVarChar;
                parameter11.Direction = ParameterDirection.Input;
                parameter11.Value = ConvertNullToEmpty(assemblyFullDTO.PinCode);
                cmd.Parameters.Add(parameter11);

                SqlParameter paramete12 = new SqlParameter();
                paramete12.ParameterName = "@PermanantPhoneNo";
                paramete12.SqlDbType = SqlDbType.NVarChar;
                paramete12.Direction = ParameterDirection.Input;
                paramete12.Value = ConvertNullToEmpty(assemblyFullDTO.PermanantPhoneNo);
                cmd.Parameters.Add(paramete12);

                SqlParameter parameter13 = new SqlParameter();
                parameter13.ParameterName = "@EmailAddress";
                parameter13.SqlDbType = SqlDbType.NVarChar;
                parameter13.Direction = ParameterDirection.Input;
                parameter13.Value = ConvertNullToEmpty(assemblyFullDTO.EmailAddress);
                cmd.Parameters.Add(parameter13);

                SqlParameter parameter14 = new SqlParameter();
                parameter14.ParameterName = "@InsertedAssemId";
                parameter14.SqlDbType = SqlDbType.Int;
                parameter14.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parameter14);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                int insAssemblyId = int.Parse(parameter14.Value.ToString());
                UpdateAssemblyServices(insAssemblyId, assemblyFullDTO);
                UpdateAssemblyElders(insAssemblyId, assemblyFullDTO);
                return insAssemblyId;
            }
        }

        public async void UpdateAssemblyServices(int assemblyId, ChristianAssemblyFullDTO assemblyFullDTO)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                DataTable tvp = new DataTable();
                tvp.Columns.Add(new DataColumn("ID", typeof(int)));
                foreach (var id in assemblyFullDTO.ServiceLanguages)
                    tvp.Rows.Add(id);

                var evangelistFullDTO = new EvangelistFullDTO();
                SqlCommand cmd = new SqlCommand("UpdateAssemblyServices", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@AssemblyId";
                parameter1.SqlDbType = SqlDbType.Int;
                parameter1.Direction = ParameterDirection.Input;
                parameter1.Value = assemblyId;
                cmd.Parameters.Add(parameter1);

                SqlParameter parameter2 = cmd.Parameters.AddWithValue("@List", tvp);
                parameter2.SqlDbType = SqlDbType.Structured;
                parameter2.Direction = ParameterDirection.Input;
                parameter2.TypeName = "dbo.IDList";

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public async void UpdateAssemblyElders(int assemblyId, ChristianAssemblyFullDTO assemblyFullDTO)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                DataTable tvp = new DataTable();
                tvp.Columns.Add(new DataColumn("ElderName", typeof(string)));
                tvp.Columns.Add(new DataColumn("Phone", typeof(string)));
                foreach (var elder in assemblyFullDTO.AssemblyElders) {
                    List<string> elderVals = new List<string>();
                    DataRow dr = tvp.NewRow();
                    dr["ElderName"] = elder.ElderName;
                    dr["Phone"] = elder.ElderPhone;
                    tvp.Rows.Add(dr);
                }

                var evangelistFullDTO = new EvangelistFullDTO();
                SqlCommand cmd = new SqlCommand("UpdateAssemblyElders", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@AssemblyId";
                parameter1.SqlDbType = SqlDbType.Int;
                parameter1.Direction = ParameterDirection.Input;
                parameter1.Value = assemblyId;
                cmd.Parameters.Add(parameter1);

                SqlParameter parameter2 = cmd.Parameters.AddWithValue("@elderList", tvp);
                parameter2.SqlDbType = SqlDbType.Structured;
                parameter2.Direction = ParameterDirection.Input;
                parameter2.TypeName = "dbo.ElderList";

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public async Task<int> SaveEvangelist(EvangelistFullDTO evangelistFullDTO)
        {
            string photopath = "";
            if (!string.IsNullOrWhiteSpace(evangelistFullDTO.Photo))
            {
                var fullPath = HttpContext.Current.Server.MapPath("~/eimages/");
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }
                var filename = Guid.NewGuid().ToString() + ".jpg";
                var filePath = fullPath + filename;
                ProcessPhotoFile(evangelistFullDTO.Photo, filePath);
                photopath = filename;
            }
            else if (!string.IsNullOrWhiteSpace(evangelistFullDTO.PhotoUrl))
            {
                photopath = evangelistFullDTO.PhotoUrl;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SaveEvangelistDetails", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@EvangelistId";
                parameter1.SqlDbType = SqlDbType.Int;
                parameter1.Direction = ParameterDirection.Input;
                parameter1.Value = evangelistFullDTO.EvangelistId;
                cmd.Parameters.Add(parameter1);

                SqlParameter parameter2 = new SqlParameter();
                parameter2.ParameterName = "@EvangelistName";
                parameter2.SqlDbType = SqlDbType.NVarChar;
                parameter2.Direction = ParameterDirection.Input;
                parameter2.Value = ConvertNullToEmpty(evangelistFullDTO.Name);
                cmd.Parameters.Add(parameter2);

                string dob = "";
                if (!string.IsNullOrWhiteSpace(evangelistFullDTO.DOB))
                {
                    dob = evangelistFullDTO.DOB; // DateTime.ParseExact(evangelistFullDTO.DOB, "dd/MM/yyyy",null);
                }

                SqlParameter parameter3 = new SqlParameter();
                parameter3.ParameterName = "@DOB";
                parameter3.SqlDbType = SqlDbType.VarChar;
                parameter3.Direction = ParameterDirection.Input;
                parameter3.Value = dob;
                cmd.Parameters.Add(parameter3);

                SqlParameter parameter4 = new SqlParameter();
                parameter4.ParameterName = "@EduQualification";
                parameter4.SqlDbType = SqlDbType.NVarChar;
                parameter4.Direction = ParameterDirection.Input;
                parameter4.Value = ConvertNullToEmpty(evangelistFullDTO.EduQualification);
                cmd.Parameters.Add(parameter4);

                SqlParameter parameter5 = new SqlParameter();
                parameter5.ParameterName = "@WifesName";
                parameter5.SqlDbType = SqlDbType.NVarChar;
                parameter5.Direction = ParameterDirection.Input;
                parameter5.Value = ConvertNullToEmpty(evangelistFullDTO.WifesName);
                cmd.Parameters.Add(parameter5);

                SqlParameter parameter6 = new SqlParameter();
                parameter6.ParameterName = "@WifesAge";
                parameter6.SqlDbType = SqlDbType.VarChar;
                parameter6.Direction = ParameterDirection.Input;
                parameter6.Value = ConvertNullToEmpty(evangelistFullDTO.WifesAge);
                cmd.Parameters.Add(parameter6);

                SqlParameter parameter7 = new SqlParameter();
                parameter7.ParameterName = "@NoOfChildren";
                parameter7.SqlDbType = SqlDbType.Int;
                parameter7.Direction = ParameterDirection.Input;
                parameter7.Value = evangelistFullDTO.NoofChildren;
                cmd.Parameters.Add(parameter7);

                string dateofcomm = "";
                if (!string.IsNullOrWhiteSpace(evangelistFullDTO.DateOfCommMinistry))
                {
                    dateofcomm = evangelistFullDTO.DateOfCommMinistry; // DateTime.ParseExact(evangelistFullDTO.DateOfCommMinistry, "dd/MM/yyyy", null);
                }

                SqlParameter parameter8 = new SqlParameter();
                parameter8.ParameterName = "@DateOfCommMinistry";
                parameter8.SqlDbType = SqlDbType.VarChar;
                parameter8.Direction = ParameterDirection.Input;
                parameter8.Value = dateofcomm;
                cmd.Parameters.Add(parameter8);

                SqlParameter parameter9 = new SqlParameter();
                parameter9.ParameterName = "@CommdAssemblyId";
                parameter9.SqlDbType = SqlDbType.Int;
                parameter9.Direction = ParameterDirection.Input;
                parameter9.Value = evangelistFullDTO.CommdAssembly != null ? evangelistFullDTO.CommdAssembly.AssemblyId : 0;
                cmd.Parameters.Add(parameter9);

                SqlParameter parameter10 = new SqlParameter();
                parameter10.ParameterName = "@AssemblyId";
                parameter10.SqlDbType = SqlDbType.Int;
                parameter10.Direction = ParameterDirection.Input;
                parameter10.Value = evangelistFullDTO.Assembly != null ? evangelistFullDTO.Assembly.AssemblyId : 0;
                cmd.Parameters.Add(parameter10);

                SqlParameter parameter11 = new SqlParameter();
                parameter11.ParameterName = "@Address1";
                parameter11.SqlDbType = SqlDbType.NVarChar;
                parameter11.Direction = ParameterDirection.Input;
                parameter11.Value = ConvertNullToEmpty(evangelistFullDTO.Address1);
                cmd.Parameters.Add(parameter11);

                SqlParameter parameter12 = new SqlParameter();
                parameter12.ParameterName = "@LandMark";
                parameter12.SqlDbType = SqlDbType.NVarChar;
                parameter12.Direction = ParameterDirection.Input;
                parameter12.Value = ConvertNullToEmpty(evangelistFullDTO.LandMark);
                cmd.Parameters.Add(parameter12);

                SqlParameter parameter13 = new SqlParameter();
                parameter13.ParameterName = "@City";
                parameter13.SqlDbType = SqlDbType.NVarChar;
                parameter13.Direction = ParameterDirection.Input;
                parameter13.Value = ConvertNullToEmpty(evangelistFullDTO.City);
                cmd.Parameters.Add(parameter13);

                SqlParameter parameter14 = new SqlParameter();
                parameter14.ParameterName = "@District";
                parameter14.SqlDbType = SqlDbType.NVarChar;
                parameter14.Direction = ParameterDirection.Input;
                parameter14.Value = ConvertNullToEmpty(evangelistFullDTO.District);
                cmd.Parameters.Add(parameter14);

                SqlParameter parameter15 = new SqlParameter();
                parameter15.ParameterName = "@State";
                parameter15.SqlDbType = SqlDbType.NVarChar;
                parameter15.Direction = ParameterDirection.Input;
                parameter15.Value = ConvertNullToEmpty(evangelistFullDTO.State);
                cmd.Parameters.Add(parameter15);

                SqlParameter parameter16 = new SqlParameter();
                parameter16.ParameterName = "@Country";
                parameter16.SqlDbType = SqlDbType.NVarChar;
                parameter16.Direction = ParameterDirection.Input;
                parameter16.Value = ConvertNullToEmpty(evangelistFullDTO.Country);
                cmd.Parameters.Add(parameter16);

                SqlParameter parameter17 = new SqlParameter();
                parameter17.ParameterName = "@PinCode";
                parameter17.SqlDbType = SqlDbType.NVarChar;
                parameter17.Direction = ParameterDirection.Input;
                parameter17.Value = ConvertNullToEmpty(evangelistFullDTO.PinCode);
                cmd.Parameters.Add(parameter17);

                SqlParameter parameter18 = new SqlParameter();
                parameter18.ParameterName = "@PermanantPhoneNo";
                parameter18.SqlDbType = SqlDbType.NVarChar;
                parameter18.Direction = ParameterDirection.Input;
                parameter18.Value = ConvertNullToEmpty(evangelistFullDTO.PermanantPhoneNo);
                cmd.Parameters.Add(parameter18);

                SqlParameter parameter19 = new SqlParameter();
                parameter19.ParameterName = "@WhatsAppNo";
                parameter19.SqlDbType = SqlDbType.NVarChar;
                parameter19.Direction = ParameterDirection.Input;
                parameter19.Value = ConvertNullToEmpty(evangelistFullDTO.WhatsAppNo);
                cmd.Parameters.Add(parameter19);

                SqlParameter parameter20 = new SqlParameter();
                parameter20.ParameterName = "@Email";
                parameter20.SqlDbType = SqlDbType.NVarChar;
                parameter20.Direction = ParameterDirection.Input;
                parameter20.Value = ConvertNullToEmpty(evangelistFullDTO.EmailAddress);
                cmd.Parameters.Add(parameter20);

                SqlParameter parameter21 = new SqlParameter();
                parameter21.ParameterName = "@Photo";
                parameter21.SqlDbType = SqlDbType.NVarChar;
                parameter21.Direction = ParameterDirection.Input;
                parameter21.Value = photopath;
                cmd.Parameters.Add(parameter21);

                SqlParameter parameter22 = new SqlParameter();
                parameter22.ParameterName = "@InsertedEvgId";
                parameter22.SqlDbType = SqlDbType.Int;
                parameter22.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parameter22);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                int insEvangelistId = int.Parse(parameter22.Value.ToString());

                UpdateEvangelistChildren(insEvangelistId, evangelistFullDTO);
                return insEvangelistId;
            }
        }

        public async void UpdateEvangelistChildren(int evangelistId, EvangelistFullDTO evangelistFullDTO)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                DataTable tvp = new DataTable();
                tvp.Columns.Add(new DataColumn("ChildName", typeof(string)));
                tvp.Columns.Add(new DataColumn("Age", typeof(string)));

                if (evangelistFullDTO.Children != null)
                {
                    foreach (var child in evangelistFullDTO.Children)
                    {
                        List<string> elderVals = new List<string>();
                        DataRow dr = tvp.NewRow();
                        dr["ChildName"] = child.ChildName;
                        dr["Age"] = child.Age;
                        tvp.Rows.Add(dr);
                    }
                }

                SqlCommand cmd = new SqlCommand("UpdateEvangelistChildren", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@evangelistId";
                parameter1.SqlDbType = SqlDbType.Int;
                parameter1.Direction = ParameterDirection.Input;
                parameter1.Value = evangelistId;
                cmd.Parameters.Add(parameter1);

                SqlParameter parameter2 = cmd.Parameters.AddWithValue("@childrenList", tvp);
                parameter2.SqlDbType = SqlDbType.Structured;
                parameter2.Direction = ParameterDirection.Input;
                parameter2.TypeName = "dbo.ChildList";

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public async void DeleteAssembly(int assemblyId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("DeleteAssembly", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@AssemblyId";
                parameter1.SqlDbType = SqlDbType.Int;
                parameter1.Direction = ParameterDirection.Input;
                parameter1.Value = assemblyId;
                cmd.Parameters.Add(parameter1);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public async void DeleteEvangelist(int evangelistId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("DeleteEvangelist", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@EvangelistId";
                parameter1.SqlDbType = SqlDbType.Int;
                parameter1.Direction = ParameterDirection.Input;
                parameter1.Value = evangelistId;
                cmd.Parameters.Add(parameter1);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public async void SaveAssemblyReq(ChristianAssemblyFullDTO assemblyFullDTO)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var evangelistFullDTO = new EvangelistFullDTO();
                SqlCommand cmd = new SqlCommand("SaveAssemblyDetailsReq", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter2 = new SqlParameter();
                parameter2.ParameterName = "@AssemblyName";
                parameter2.SqlDbType = SqlDbType.NVarChar;
                parameter2.Direction = ParameterDirection.Input;
                parameter2.Value = ConvertNullToEmpty(assemblyFullDTO.AssemblyName);
                cmd.Parameters.Add(parameter2);

                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

                SqlParameter parameter15 = new SqlParameter();
                parameter15.ParameterName = "@CreatedDate";
                parameter15.SqlDbType = SqlDbType.NVarChar;
                parameter15.Direction = ParameterDirection.Input;
                parameter15.Value = indianTime;
                cmd.Parameters.Add(parameter15);

                SqlParameter parameter3 = new SqlParameter();
                parameter3.ParameterName = "@NoOfPersons";
                parameter3.SqlDbType = SqlDbType.Int;
                parameter3.Direction = ParameterDirection.Input;
                parameter3.Value = assemblyFullDTO.NoOfPersons;
                cmd.Parameters.Add(parameter3);

                SqlParameter parameter4 = new SqlParameter();
                parameter4.ParameterName = "@WorshipTime";
                parameter4.SqlDbType = SqlDbType.NVarChar;
                parameter4.Direction = ParameterDirection.Input;
                parameter4.Value = ConvertNullToEmpty(assemblyFullDTO.WorshipTime);
                cmd.Parameters.Add(parameter4);

                SqlParameter parameter5 = new SqlParameter();
                parameter5.ParameterName = "@Address1";
                parameter5.SqlDbType = SqlDbType.NVarChar;
                parameter5.Direction = ParameterDirection.Input;
                parameter5.Value = ConvertNullToEmpty(assemblyFullDTO.Address1);
                cmd.Parameters.Add(parameter5);

                SqlParameter parameter6 = new SqlParameter();
                parameter6.ParameterName = "@LandMark";
                parameter6.SqlDbType = SqlDbType.NVarChar;
                parameter6.Direction = ParameterDirection.Input;
                parameter6.Value = ConvertNullToEmpty(assemblyFullDTO.LandMark);
                cmd.Parameters.Add(parameter6);

                SqlParameter parameter7 = new SqlParameter();
                parameter7.ParameterName = "@City";
                parameter7.SqlDbType = SqlDbType.NVarChar;
                parameter7.Direction = ParameterDirection.Input;
                parameter7.Value = ConvertNullToEmpty(assemblyFullDTO.City);
                cmd.Parameters.Add(parameter7);

                SqlParameter parameter8 = new SqlParameter();
                parameter8.ParameterName = "@District";
                parameter8.SqlDbType = SqlDbType.NVarChar;
                parameter8.Direction = ParameterDirection.Input;
                parameter8.Value = ConvertNullToEmpty(assemblyFullDTO.District);
                cmd.Parameters.Add(parameter8);

                SqlParameter parameter9 = new SqlParameter();
                parameter9.ParameterName = "@State";
                parameter9.SqlDbType = SqlDbType.NVarChar;
                parameter9.Direction = ParameterDirection.Input;
                parameter9.Value = ConvertNullToEmpty(assemblyFullDTO.State);
                cmd.Parameters.Add(parameter9);

                SqlParameter parameter10 = new SqlParameter();
                parameter10.ParameterName = "@Country";
                parameter10.SqlDbType = SqlDbType.NVarChar;
                parameter10.Direction = ParameterDirection.Input;
                parameter10.Value = ConvertNullToEmpty(assemblyFullDTO.Country);
                cmd.Parameters.Add(parameter10);

                SqlParameter parameter11 = new SqlParameter();
                parameter11.ParameterName = "@PinCode";
                parameter11.SqlDbType = SqlDbType.NVarChar;
                parameter11.Direction = ParameterDirection.Input;
                parameter11.Value = ConvertNullToEmpty(assemblyFullDTO.PinCode);
                cmd.Parameters.Add(parameter11);

                SqlParameter paramete12 = new SqlParameter();
                paramete12.ParameterName = "@PermanantPhoneNo";
                paramete12.SqlDbType = SqlDbType.NVarChar;
                paramete12.Direction = ParameterDirection.Input;
                paramete12.Value = ConvertNullToEmpty(assemblyFullDTO.PermanantPhoneNo);
                cmd.Parameters.Add(paramete12);

                SqlParameter parameter13 = new SqlParameter();
                parameter13.ParameterName = "@EmailAddress";
                parameter13.SqlDbType = SqlDbType.NVarChar;
                parameter13.Direction = ParameterDirection.Input;
                parameter13.Value = ConvertNullToEmpty(assemblyFullDTO.EmailAddress);
                cmd.Parameters.Add(parameter13);

                SqlParameter parameter14 = new SqlParameter();
                parameter14.ParameterName = "@requestid";
                parameter14.SqlDbType = SqlDbType.Int;
                parameter14.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parameter14);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                int requestId = int.Parse(parameter14.Value.ToString());
                UpdateAssemblyServicesReq(requestId, assemblyFullDTO);
                UpdateAssemblyEldersReq(requestId, assemblyFullDTO);
            }
        }

        public async void UpdateAssemblyServicesReq(int requestId, ChristianAssemblyFullDTO assemblyFullDTO)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                DataTable tvp = new DataTable();
                tvp.Columns.Add(new DataColumn("ID", typeof(int)));
                foreach (var id in assemblyFullDTO.ServiceLanguages)
                    tvp.Rows.Add(id);

                var evangelistFullDTO = new EvangelistFullDTO();
                SqlCommand cmd = new SqlCommand("UpdateAssemblyServicesReq", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@requestid";
                parameter1.SqlDbType = SqlDbType.Int;
                parameter1.Direction = ParameterDirection.Input;
                parameter1.Value = requestId;
                cmd.Parameters.Add(parameter1);

                SqlParameter parameter2 = cmd.Parameters.AddWithValue("@List", tvp);
                parameter2.SqlDbType = SqlDbType.Structured;
                parameter2.Direction = ParameterDirection.Input;
                parameter2.TypeName = "dbo.IDList";

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public async void UpdateAssemblyEldersReq(int requestId, ChristianAssemblyFullDTO assemblyFullDTO)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                DataTable tvp = new DataTable();
                tvp.Columns.Add(new DataColumn("ElderName", typeof(string)));
                tvp.Columns.Add(new DataColumn("Phone", typeof(string)));
                if (assemblyFullDTO.AssemblyElders != null)
                {
                    foreach (var elder in assemblyFullDTO.AssemblyElders)
                    {
                        List<string> elderVals = new List<string>();
                        DataRow dr = tvp.NewRow();
                        dr["ElderName"] = elder.ElderName;
                        dr["Phone"] = elder.ElderPhone;
                        tvp.Rows.Add(dr);
                    }
                }

                var evangelistFullDTO = new EvangelistFullDTO();
                SqlCommand cmd = new SqlCommand("UpdateAssemblyEldersReq", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@requestid";
                parameter1.SqlDbType = SqlDbType.Int;
                parameter1.Direction = ParameterDirection.Input;
                parameter1.Value = requestId;
                cmd.Parameters.Add(parameter1);

                SqlParameter parameter2 = cmd.Parameters.AddWithValue("@elderList", tvp);
                parameter2.SqlDbType = SqlDbType.Structured;
                parameter2.Direction = ParameterDirection.Input;
                parameter2.TypeName = "dbo.ElderList";

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public async Task<List<ReqChristianAssemblyDTO>> GetAllAssemblyRequests(string keyword)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string command = "select RequestId, CreatedDate, Status, AssemblyName, Address1, LandMark, city, district, state, country from ReqChristianAssembly order by status asc, RequestId desc;";

                if (!string.IsNullOrWhiteSpace(keyword) && keyword.Length > 2)
                {
                    keyword = keyword.ToLower();
                    command = @"select RequestId, CreatedDate, Status, AssemblyName, Address1, city, district, state, country from ReqChristianAssembly";
                    command += " where lower(AssemblyName) like '%" + keyword + "%' or ";
                    command += "lower(Address1) like '%" + keyword + "%' or ";
                    command += "lower(LandMark) like '%" + keyword + "%' or ";
                    command += "lower(City) like '%" + keyword + "%' or ";
                    command += "lower(district) like '%" + keyword + "%' or ";
                    command += "lower(State) like '%" + keyword + "%' or ";
                    command += "lower(Country) like '%" + keyword + "%' or ";
                    command += "lower(PinCode) like '%" + keyword + "%'";
                }


                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command, connection);
                DataSet ds = new DataSet();
                await Task.Run(() => sqlDataAdapter.Fill(ds));
                var christianAssemblyDTO = ds.Tables[0].AsEnumerable()
                .Select(dataRow => new ReqChristianAssemblyDTO
                {
                    RequestId = dataRow.Field<int>("RequestId"),
                    Status = dataRow.Field<int>("Status"),
                    CreateDate = FormatDate(dataRow.Field<DateTime>("CreatedDate")),
                    AssemblyName = dataRow.Field<string>("AssemblyName"),
                    LandMark = dataRow.Field<string>("LandMark"),
                    Address1 = dataRow.Field<string>("Address1"),
                    City = dataRow.Field<string>("city"),
                    District = dataRow.Field<string>("district"),
                    State = dataRow.Field<string>("state"),
                    Country = dataRow.Field<string>("country"),
                }).ToList();
                return christianAssemblyDTO;
            }
        }

        public async void UpdateAssembyRequestStatus(int requestId, int status)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("UpdateAssemblyRequestStatus", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@RequestId";
                parameter1.SqlDbType = SqlDbType.Int;
                parameter1.Direction = ParameterDirection.Input;
                parameter1.Value = requestId;
                cmd.Parameters.Add(parameter1);

                SqlParameter parameter2 = new SqlParameter();
                parameter2.ParameterName = "@Status";
                parameter2.SqlDbType = SqlDbType.Int;
                parameter2.Direction = ParameterDirection.Input;
                parameter2.Value = status;
                cmd.Parameters.Add(parameter2);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public async void UpdateAssembyRequestStatusOnly(int requestId, int assemblyId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("UpdateAssemblyRequestStatusOnly", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@RequestId";
                parameter1.SqlDbType = SqlDbType.Int;
                parameter1.Direction = ParameterDirection.Input;
                parameter1.Value = requestId;
                cmd.Parameters.Add(parameter1);

                SqlParameter parameter2 = new SqlParameter();
                parameter2.ParameterName = "@AssemblyId";
                parameter2.SqlDbType = SqlDbType.Int;
                parameter2.Direction = ParameterDirection.Input;
                parameter2.Value = assemblyId;
                cmd.Parameters.Add(parameter2);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public async Task<ChristianAssemblyFullDTO> GetAssemblyRequestFullDetails(int requestId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var christianAssemblyFullDTO = new ChristianAssemblyFullDTO();
                SqlCommand cmd = new SqlCommand("GetAssemblyRequestFullDetails", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@RequestId";
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = requestId;
                cmd.Parameters.Add(parameter);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                await Task.Run(() => sqlDataAdapter.Fill(ds));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    christianAssemblyFullDTO.RequestId = ds.Tables[0].Rows[0].Field<int>("RequestId");
                    christianAssemblyFullDTO.AssemblyId = 0;
                    christianAssemblyFullDTO.AssemblyName = ds.Tables[0].Rows[0].Field<string>("AssemblyName");
                    christianAssemblyFullDTO.Status = ds.Tables[0].Rows[0].Field<int>("Status");
                    christianAssemblyFullDTO.NoOfPersons = ds.Tables[0].Rows[0].Field<int>("NoOfPersons");
                    christianAssemblyFullDTO.WorshipTime = ds.Tables[0].Rows[0].Field<string>("WorshipTime");
                    christianAssemblyFullDTO.Address1 = ds.Tables[0].Rows[0].Field<string>("Address1");
                    christianAssemblyFullDTO.LandMark = ds.Tables[0].Rows[0].Field<string>("LandMark");
                    christianAssemblyFullDTO.City = ds.Tables[0].Rows[0].Field<string>("city");
                    christianAssemblyFullDTO.District = ds.Tables[0].Rows[0].Field<string>("District");
                    christianAssemblyFullDTO.State = ds.Tables[0].Rows[0].Field<string>("State");
                    christianAssemblyFullDTO.Country = ds.Tables[0].Rows[0].Field<string>("Country");
                    christianAssemblyFullDTO.PinCode = ds.Tables[0].Rows[0].Field<string>("PinCode");
                    christianAssemblyFullDTO.PermanantPhoneNo = ds.Tables[0].Rows[0].Field<string>("PermanantPhoneNo");
                    christianAssemblyFullDTO.EmailAddress = ds.Tables[0].Rows[0].Field<string>("EmailAddress");
                }

                christianAssemblyFullDTO.ServiceLanguages = ds.Tables[1].AsEnumerable()
                .Select(dataRow => dataRow.Field<int>("ServiceId")).ToList();


                christianAssemblyFullDTO.AssemblyElders = ds.Tables[2].AsEnumerable()
                .Select(dataRow => new AssemblyElderDTO
                {
                    AssemblyElderId = 0,
                    ElderName = dataRow.Field<string>("ElderName"),
                    ElderPhone = dataRow.Field<string>("PhoneNo"),
                }).ToList();

                return christianAssemblyFullDTO;
            }
        }

        public async void SaveEvangelistReq(EvangelistFullDTO evangelistFullDTO)
        {
            string photopath = "";
            if (!string.IsNullOrWhiteSpace(evangelistFullDTO.Photo))
            {
                var fullPath = HttpContext.Current.Server.MapPath("~/eimages/");
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }
                var filename = Guid.NewGuid().ToString() + ".jpg";
                var filePath = fullPath + filename;
                ProcessRequestPhotoFile(evangelistFullDTO.Photo, filePath);
                photopath = filename;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("SaveEvangelistDetailsReq", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@EvangelistName";
                parameter1.SqlDbType = SqlDbType.NVarChar;
                parameter1.Direction = ParameterDirection.Input;
                parameter1.Value = ConvertNullToEmpty(evangelistFullDTO.Name);
                cmd.Parameters.Add(parameter1);

                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

                SqlParameter parameter2 = new SqlParameter();
                parameter2.ParameterName = "@CreatedDate";
                parameter2.SqlDbType = SqlDbType.NVarChar;
                parameter2.Direction = ParameterDirection.Input;
                parameter2.Value = indianTime;
                cmd.Parameters.Add(parameter2);

                string dob = "";
                if (!string.IsNullOrWhiteSpace(evangelistFullDTO.DOB))
                {
                    dob = evangelistFullDTO.DOB; // DateTime.ParseExact(evangelistFullDTO.DOB, "dd/MM/yyyy",null);
                }

                SqlParameter parameter3 = new SqlParameter();
                parameter3.ParameterName = "@DOB";
                parameter3.SqlDbType = SqlDbType.VarChar;
                parameter3.Direction = ParameterDirection.Input;
                parameter3.Value = dob;
                cmd.Parameters.Add(parameter3);

                SqlParameter parameter4 = new SqlParameter();
                parameter4.ParameterName = "@EduQualification";
                parameter4.SqlDbType = SqlDbType.NVarChar;
                parameter4.Direction = ParameterDirection.Input;
                parameter4.Value = ConvertNullToEmpty(evangelistFullDTO.EduQualification);
                cmd.Parameters.Add(parameter4);

                SqlParameter parameter5 = new SqlParameter();
                parameter5.ParameterName = "@WifesName";
                parameter5.SqlDbType = SqlDbType.NVarChar;
                parameter5.Direction = ParameterDirection.Input;
                parameter5.Value = ConvertNullToEmpty(evangelistFullDTO.WifesName);
                cmd.Parameters.Add(parameter5);

                SqlParameter parameter6 = new SqlParameter();
                parameter6.ParameterName = "@WifesAge";
                parameter6.SqlDbType = SqlDbType.VarChar;
                parameter6.Direction = ParameterDirection.Input;
                parameter6.Value = ConvertNullToEmpty(evangelistFullDTO.WifesAge);
                cmd.Parameters.Add(parameter6);

                SqlParameter parameter7 = new SqlParameter();
                parameter7.ParameterName = "@NoOfChildren";
                parameter7.SqlDbType = SqlDbType.Int;
                parameter7.Direction = ParameterDirection.Input;
                parameter7.Value = evangelistFullDTO.NoofChildren;
                cmd.Parameters.Add(parameter7);

                string dateofcomm = "";
                if (!string.IsNullOrWhiteSpace(evangelistFullDTO.DateOfCommMinistry))
                {
                    dateofcomm = evangelistFullDTO.DateOfCommMinistry; // DateTime.ParseExact(evangelistFullDTO.DateOfCommMinistry, "dd/MM/yyyy", null);
                }

                SqlParameter parameter8 = new SqlParameter();
                parameter8.ParameterName = "@DateOfCommMinistry";
                parameter8.SqlDbType = SqlDbType.VarChar;
                parameter8.Direction = ParameterDirection.Input;
                parameter8.Value = dateofcomm;
                cmd.Parameters.Add(parameter8);

                SqlParameter parameter9 = new SqlParameter();
                parameter9.ParameterName = "@CommdAssemblyId";
                parameter9.SqlDbType = SqlDbType.Int;
                parameter9.Direction = ParameterDirection.Input;
                parameter9.Value = evangelistFullDTO.CommdAssembly != null ? evangelistFullDTO.CommdAssembly.AssemblyId : 0;
                cmd.Parameters.Add(parameter9);

                SqlParameter parameter10 = new SqlParameter();
                parameter10.ParameterName = "@AssemblyId";
                parameter10.SqlDbType = SqlDbType.Int;
                parameter10.Direction = ParameterDirection.Input;
                parameter10.Value = evangelistFullDTO.Assembly != null ? evangelistFullDTO.Assembly.AssemblyId : 0;
                cmd.Parameters.Add(parameter10);

                SqlParameter parameter11 = new SqlParameter();
                parameter11.ParameterName = "@Address1";
                parameter11.SqlDbType = SqlDbType.NVarChar;
                parameter11.Direction = ParameterDirection.Input;
                parameter11.Value = ConvertNullToEmpty(evangelistFullDTO.Address1);
                cmd.Parameters.Add(parameter11);

                SqlParameter parameter12 = new SqlParameter();
                parameter12.ParameterName = "@LandMark";
                parameter12.SqlDbType = SqlDbType.NVarChar;
                parameter12.Direction = ParameterDirection.Input;
                parameter12.Value = ConvertNullToEmpty(evangelistFullDTO.LandMark);
                cmd.Parameters.Add(parameter12);

                SqlParameter parameter13 = new SqlParameter();
                parameter13.ParameterName = "@City";
                parameter13.SqlDbType = SqlDbType.NVarChar;
                parameter13.Direction = ParameterDirection.Input;
                parameter13.Value = ConvertNullToEmpty(evangelistFullDTO.City);
                cmd.Parameters.Add(parameter13);

                SqlParameter parameter14 = new SqlParameter();
                parameter14.ParameterName = "@District";
                parameter14.SqlDbType = SqlDbType.NVarChar;
                parameter14.Direction = ParameterDirection.Input;
                parameter14.Value = ConvertNullToEmpty(evangelistFullDTO.District);
                cmd.Parameters.Add(parameter14);

                SqlParameter parameter15 = new SqlParameter();
                parameter15.ParameterName = "@State";
                parameter15.SqlDbType = SqlDbType.NVarChar;
                parameter15.Direction = ParameterDirection.Input;
                parameter15.Value = ConvertNullToEmpty(evangelistFullDTO.State);
                cmd.Parameters.Add(parameter15);

                SqlParameter parameter16 = new SqlParameter();
                parameter16.ParameterName = "@Country";
                parameter16.SqlDbType = SqlDbType.NVarChar;
                parameter16.Direction = ParameterDirection.Input;
                parameter16.Value = ConvertNullToEmpty(evangelistFullDTO.Country);
                cmd.Parameters.Add(parameter16);

                SqlParameter parameter17 = new SqlParameter();
                parameter17.ParameterName = "@PinCode";
                parameter17.SqlDbType = SqlDbType.NVarChar;
                parameter17.Direction = ParameterDirection.Input;
                parameter17.Value = ConvertNullToEmpty(evangelistFullDTO.PinCode);
                cmd.Parameters.Add(parameter17);

                SqlParameter parameter18 = new SqlParameter();
                parameter18.ParameterName = "@PermanantPhoneNo";
                parameter18.SqlDbType = SqlDbType.NVarChar;
                parameter18.Direction = ParameterDirection.Input;
                parameter18.Value = ConvertNullToEmpty(evangelistFullDTO.PermanantPhoneNo);
                cmd.Parameters.Add(parameter18);

                SqlParameter parameter19 = new SqlParameter();
                parameter19.ParameterName = "@WhatsAppNo";
                parameter19.SqlDbType = SqlDbType.NVarChar;
                parameter19.Direction = ParameterDirection.Input;
                parameter19.Value = ConvertNullToEmpty(evangelistFullDTO.WhatsAppNo);
                cmd.Parameters.Add(parameter19);

                SqlParameter parameter20 = new SqlParameter();
                parameter20.ParameterName = "@Email";
                parameter20.SqlDbType = SqlDbType.NVarChar;
                parameter20.Direction = ParameterDirection.Input;
                parameter20.Value = ConvertNullToEmpty(evangelistFullDTO.EmailAddress);
                cmd.Parameters.Add(parameter20);

                SqlParameter parameter21 = new SqlParameter();
                parameter21.ParameterName = "@Photo";
                parameter21.SqlDbType = SqlDbType.NVarChar;
                parameter21.Direction = ParameterDirection.Input;
                parameter21.Value = photopath;
                cmd.Parameters.Add(parameter21);

                SqlParameter parameter22 = new SqlParameter();
                parameter22.ParameterName = "@RequestId";
                parameter22.SqlDbType = SqlDbType.Int;
                parameter22.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parameter22);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                int requestId = int.Parse(parameter22.Value.ToString());

                UpdateEvangelistChildrenReq(requestId, evangelistFullDTO);
            }
        }

        public async void UpdateEvangelistChildrenReq(int requestId, EvangelistFullDTO evangelistFullDTO)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                DataTable tvp = new DataTable();
                tvp.Columns.Add(new DataColumn("ChildName", typeof(string)));
                tvp.Columns.Add(new DataColumn("Age", typeof(string)));

                if (evangelistFullDTO.Children != null)
                {
                    foreach (var child in evangelistFullDTO.Children)
                    {
                        List<string> elderVals = new List<string>();
                        DataRow dr = tvp.NewRow();
                        dr["ChildName"] = child.ChildName;
                        dr["Age"] = child.Age;
                        tvp.Rows.Add(dr);
                    }
                }

                SqlCommand cmd = new SqlCommand("UpdateEvangelistChildrenReq", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@RequestId";
                parameter1.SqlDbType = SqlDbType.Int;
                parameter1.Direction = ParameterDirection.Input;
                parameter1.Value = requestId;
                cmd.Parameters.Add(parameter1);

                SqlParameter parameter2 = cmd.Parameters.AddWithValue("@childrenList", tvp);
                parameter2.SqlDbType = SqlDbType.Structured;
                parameter2.Direction = ParameterDirection.Input;
                parameter2.TypeName = "dbo.ChildList";

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public async Task<List<ReqEvangelistDTO>> GetAllEvangelistRequests(string keyword)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string command = @"SELECT e.RequestId, e.CreatedDate, e.Status, e.EvangelistName,   e.DOB,
                                   e.AssemblyId,  c.AssemblyName,
                                   e.Address1 ,e.LandMark
                                  ,e.City ,e.District
                                  ,e.State
                                  ,e.Country
                                  ,e.PinCode
                              FROM ReqEvangelists e left join ChristianAssembly c  on e.AssemblyId = c.AssemblyId order by e.status asc, e.RequestId desc";

                if (!string.IsNullOrWhiteSpace(keyword) && keyword.Length > 2)
                {
                    keyword = keyword.ToLower();
                    command = @"SELECT e.RequestId,e.CreatedDate, e.Status,  e.EvangelistName,   e.DOB,
                                   e.AssemblyId,  c.AssemblyName,
                                   e.Address1 ,e.LandMark
                                  ,e.City ,e.District
                                  ,e.State
                                  ,e.Country
                                  ,e.PinCode
                              FROM ReqEvangelists e left join ChristianAssembly c  on e.AssemblyId = c.AssemblyId";
                    command += " where lower(e.EvangelistName) like '%" + keyword + "%' or ";
                    command += "lower(c.AssemblyName) like '%" + keyword + "%' or ";
                    command += "lower(e.Address1) like '%" + keyword + "%' or ";
                    command += "lower(e.LandMark) like '%" + keyword + "%' or ";
                    command += "lower(e.City) like '%" + keyword + "%' or ";
                    command += "lower(e.District) like '%" + keyword + "%' or ";
                    command += "lower(e.State) like '%" + keyword + "%' or ";
                    command += "lower(e.Country) like '%" + keyword + "%' or ";
                    command += "lower(e.PinCode) like '%" + keyword + "%'";
                }


                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command, connection);
                DataSet ds = new DataSet();
                await Task.Run(() => sqlDataAdapter.Fill(ds));
                var evangelistSimpleDTO = ds.Tables[0].AsEnumerable()
                .Select(dataRow => new ReqEvangelistDTO
                {
                    RequestId = dataRow.Field<int>("RequestId"),
                    Status = dataRow.Field<int>("Status"),
                    CreateDate = FormatDate(dataRow.Field<DateTime>("CreatedDate")),
                    Name = dataRow.Field<string>("EvangelistName"),
                    DOB = dataRow.Field<string>("dob"),
                    Age = GetEvangelistAge(dataRow.Field<string>("dob")), //dataRow.Field<int>("Age"),
                    AssemblyId = dataRow.Field<int>("AssemblyId"),
                    AssemblyName = dataRow.Field<string>("AssemblyName"),
                    Address1 = dataRow.Field<string>("Address1"),
                    City = dataRow.Field<string>("city"),
                    District = dataRow.Field<string>("district"),
                    State = dataRow.Field<string>("state"),
                    Country = dataRow.Field<string>("country"),
                    PinCode = dataRow.Field<string>("PinCode"),
                    LandMark = dataRow.Field<string>("LandMark"),
                }).ToList();
                return evangelistSimpleDTO;

            }
        }

        public async Task<EvangelistFullDTO> GetEvangelistRequestFullDetails(int requestId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var evangelistFullDTO = new EvangelistFullDTO();
                SqlCommand cmd = new SqlCommand("GetEvangelistRequestFullDetails", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@RequestId";
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = requestId;
                cmd.Parameters.Add(parameter);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                await Task.Run(() => sqlDataAdapter.Fill(ds));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    evangelistFullDTO.RequestId = ds.Tables[0].Rows[0].Field<int>("RequestId");
                    evangelistFullDTO.Status = ds.Tables[0].Rows[0].Field<int>("Status");
                    evangelistFullDTO.Name = ds.Tables[0].Rows[0].Field<string>("EvangelistName");
                    evangelistFullDTO.DOB = ds.Tables[0].Rows[0].Field<string>("DOB"); //.ToString("dd/MM/yyyy");
                    evangelistFullDTO.EduQualification = ds.Tables[0].Rows[0].Field<string>("EduQualification");
                    evangelistFullDTO.WifesName = ds.Tables[0].Rows[0].Field<string>("WifesName");
                    evangelistFullDTO.WifesAge = ds.Tables[0].Rows[0].Field<string>("WifesAge");
                    evangelistFullDTO.NoofChildren = ds.Tables[0].Rows[0].Field<int>("NoOfChildren");
                    evangelistFullDTO.AssemblyId = ds.Tables[0].Rows[0].Field<int>("AssemblyId");
                    evangelistFullDTO.CommdAssemblyId = ds.Tables[0].Rows[0].Field<int>("CommdAssemblyId");
                    evangelistFullDTO.DateOfCommMinistry = ds.Tables[0].Rows[0].Field<string>("DateOfCommMinistry"); //.ToString("dd/MM/yyyy");
                    evangelistFullDTO.Address1 = ds.Tables[0].Rows[0].Field<string>("Address1");
                    evangelistFullDTO.LandMark = ds.Tables[0].Rows[0].Field<string>("LandMark");
                    evangelistFullDTO.City = ds.Tables[0].Rows[0].Field<string>("city");
                    evangelistFullDTO.District = ds.Tables[0].Rows[0].Field<string>("District");
                    evangelistFullDTO.State = ds.Tables[0].Rows[0].Field<string>("State");
                    evangelistFullDTO.Country = ds.Tables[0].Rows[0].Field<string>("Country");
                    evangelistFullDTO.PinCode = ds.Tables[0].Rows[0].Field<string>("PinCode");
                    evangelistFullDTO.PermanantPhoneNo = ds.Tables[0].Rows[0].Field<string>("PermanentPhoneNo");
                    evangelistFullDTO.EmailAddress = ds.Tables[0].Rows[0].Field<string>("Email");
                    evangelistFullDTO.WhatsAppNo = ds.Tables[0].Rows[0].Field<string>("WhatsAppNo");
                    evangelistFullDTO.PhotoUrl = ds.Tables[0].Rows[0].Field<string>("Photo");
                }

                evangelistFullDTO.Children = ds.Tables[1].AsEnumerable()
                .Select(dataRow => new ChildDTO
                {
                    ChildId = 0,
                    ChildName = dataRow.Field<string>("ChildName"),
                    Age = dataRow.Field<string>("Age"),
                }).ToList();

                if (ds.Tables[2].Rows.Count > 0)
                {
                    ChristianAssemblyDTO christianAssemblyDTO = new ChristianAssemblyDTO();
                    christianAssemblyDTO.AssemblyId = ds.Tables[2].Rows[0].Field<int>("AssemblyId");
                    christianAssemblyDTO.AssemblyName = ds.Tables[2].Rows[0].Field<string>("AssemblyName");
                    christianAssemblyDTO.Address1 = ds.Tables[2].Rows[0].Field<string>("Address1");
                    christianAssemblyDTO.LandMark = ds.Tables[2].Rows[0].Field<string>("LandMark");
                    christianAssemblyDTO.City = ds.Tables[2].Rows[0].Field<string>("city");
                    christianAssemblyDTO.District = ds.Tables[2].Rows[0].Field<string>("District");
                    christianAssemblyDTO.State = ds.Tables[2].Rows[0].Field<string>("State");
                    christianAssemblyDTO.Country = ds.Tables[2].Rows[0].Field<string>("Country");
                    evangelistFullDTO.Assembly = christianAssemblyDTO;
                }

                if (ds.Tables[3].Rows.Count > 0)
                {
                    ChristianAssemblyDTO christianAssemblyDTO = new ChristianAssemblyDTO();
                    christianAssemblyDTO.AssemblyId = ds.Tables[3].Rows[0].Field<int>("AssemblyId");
                    christianAssemblyDTO.AssemblyName = ds.Tables[3].Rows[0].Field<string>("AssemblyName");
                    christianAssemblyDTO.Address1 = ds.Tables[3].Rows[0].Field<string>("Address1");
                    christianAssemblyDTO.LandMark = ds.Tables[3].Rows[0].Field<string>("LandMark");
                    christianAssemblyDTO.City = ds.Tables[3].Rows[0].Field<string>("city");
                    christianAssemblyDTO.District = ds.Tables[3].Rows[0].Field<string>("District");
                    christianAssemblyDTO.State = ds.Tables[3].Rows[0].Field<string>("State");
                    christianAssemblyDTO.Country = ds.Tables[3].Rows[0].Field<string>("Country");
                    evangelistFullDTO.CommdAssembly = christianAssemblyDTO;
                }

                return evangelistFullDTO;
            }
        }

        public async void UpdateEvangelistRequestStatus(int requestId, int status)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("UpdateEvangelistRequestStatus", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@RequestId";
                parameter1.SqlDbType = SqlDbType.Int;
                parameter1.Direction = ParameterDirection.Input;
                parameter1.Value = requestId;
                cmd.Parameters.Add(parameter1);

                SqlParameter parameter2 = new SqlParameter();
                parameter2.ParameterName = "@Status";
                parameter2.SqlDbType = SqlDbType.Int;
                parameter2.Direction = ParameterDirection.Input;
                parameter2.Value = status;
                cmd.Parameters.Add(parameter2);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public async void UpdateEvangelistRequestStatusOnly(int requestId, int evangelistId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("UpdateEvangelistRequestStatusOnly", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@RequestId";
                parameter1.SqlDbType = SqlDbType.Int;
                parameter1.Direction = ParameterDirection.Input;
                parameter1.Value = requestId;
                cmd.Parameters.Add(parameter1);

                SqlParameter parameter2 = new SqlParameter();
                parameter2.ParameterName = "@EvangelistId";
                parameter2.SqlDbType = SqlDbType.Int;
                parameter2.Direction = ParameterDirection.Input;
                parameter2.Value = evangelistId;
                cmd.Parameters.Add(parameter2);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public async void ProcessPhotoFile(string base64Image, string filePath)
        {
            if (base64Image.IndexOf("base64,") > -1)
            {
                base64Image = base64Image.Substring(base64Image.IndexOf("base64,") + "base64,".Length);
            }
            

            byte[] bytes = Convert.FromBase64String(base64Image);
            File.WriteAllBytes(filePath, bytes);

            Image image = Image.FromFile(filePath);
            if (image.Height > 700 || image.Width > 700)
            {
                image.Dispose();
                ResizeFile(filePath, 700, false);
            }
            ResizeFile(filePath, 100, true);
        }

        public void ResizeFile(string filePath, int size, bool isThumbnail)
        {
            Bitmap res = new Bitmap(size, size);
            using (Bitmap bmp = new Bitmap(filePath))
            {
                Graphics g = Graphics.FromImage(res);
                g.FillRectangle(new SolidBrush(Color.White), 0, 0, size, size);
                int t = 0, l = 0;
                if (bmp.Height > bmp.Width)
                    t = (bmp.Height - bmp.Width) / 2;
                else
                    l = (bmp.Width - bmp.Height) / 2;
                g.DrawImage(bmp, new Rectangle(0, 0, size, size), new Rectangle(l, t, bmp.Width - l * 2, bmp.Height - t * 2), GraphicsUnit.Pixel);
            }
            if(isThumbnail)
                filePath = filePath.Replace(".jpg", "_small.jpg");
            File.Delete(filePath);
            res.Save(filePath);
        }

        public async void ProcessRequestPhotoFile(string base64Image, string filePath)
        {
            if (base64Image.IndexOf("base64,") > -1)
            {
                base64Image = base64Image.Substring(base64Image.IndexOf("base64,") + "base64,".Length);
            }


            byte[] bytes = Convert.FromBase64String(base64Image);
            File.WriteAllBytes(filePath, bytes);
        }

        public string ConvertNullToEmpty(string value)
        {
            if (value == null) return "";
            else return value;
        }

        public string FormatDate(DateTime dt)
        {
            return dt.ToString("dd-MM-yyyy hh:mm tt");
        }

    }
}
