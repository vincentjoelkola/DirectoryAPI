using BrethrenModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrethrenRepository
{
    public class UserMasterRepository
    {
        public async Task<UserMaster> ValidateUser(string username, string password)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["BookServiceContext"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                UserMaster userMaster = null;
                SqlCommand cmd = new SqlCommand("GetUserDetails", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@UserName";
                parameter.SqlDbType = SqlDbType.NVarChar;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = username;
                cmd.Parameters.Add(parameter);

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@UserPassword";
                parameter1.SqlDbType = SqlDbType.NVarChar;
                parameter1.Direction = ParameterDirection.Input;
                parameter1.Value = password;
                cmd.Parameters.Add(parameter1);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                await Task.Run(() => sqlDataAdapter.Fill(ds));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    userMaster = new UserMaster();
                    userMaster.UserId = ds.Tables[0].Rows[0].Field<int>("UserId");
                    userMaster.UserName = ds.Tables[0].Rows[0].Field<string>("UserName");
                    userMaster.UserPassword = ds.Tables[0].Rows[0].Field<string>("UserPassword");
                    userMaster.UserDisplayName = ds.Tables[0].Rows[0].Field<string>("UserDisplayName");
                    userMaster.UserRoles = ds.Tables[0].Rows[0].Field<string>("UserRoles");
                    userMaster.UserEmailId = ds.Tables[0].Rows[0].Field<string>("UserEmail");
                }
                return userMaster;
            }
        }
    }
}
