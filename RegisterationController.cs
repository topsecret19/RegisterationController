using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    [RoutePrefix("api/Registeration")]
    public class RegisterationController : ApiController
    {
        [Route("register")]
        [HttpPost]
        public HttpResponseMessage Registeration(User user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    // Create the connectionString
                    // Trusted_Connection is used to denote the connection uses Windows Authentication
                    conn.ConnectionString = "Server=tcp:marwan.database.windows.net,1433;Initial Catalog=EmployeeDB;Persist Security Info=False;User ID=myid;Password=@myid;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                    conn.Open();

                    SqlCommand insertCommand = new SqlCommand("INSERT INTO Employees VALUES (@1, @2)", conn);

                    // In the command, there are some parameters denoted by @, you can 
                    // change their value on a condition, in my code they're hardcoded.

                    insertCommand.Parameters.Add(new SqlParameter("1", user.Email));
                    insertCommand.Parameters.Add(new SqlParameter("2", user.Password));

                    var message = Request.CreateResponse(HttpStatusCode.Created, user);
                    message.Headers.Location = new Uri(Request.RequestUri + user.ID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
