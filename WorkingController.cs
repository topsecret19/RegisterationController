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
public HttpResponseMessage InsertUser([FromBody]User user)
{
    try
    {
        string connetionString = null;
        string sql = null;
        connetionString = "Server=tcp:marwan.database.windows.net,1433;Initial Catalog=EmployeeDB;Persist Security Info=False;User ID=myid;Password=mypassword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        using (SqlConnection cnn = new SqlConnection(connetionString))
        {
            sql = "insert into Users ([Email], [Password]) values(@email,@password)";
            cnn.Open();
            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.ExecuteNonQuery();
                var message = Request.CreateResponse(HttpStatusCode.Created, user);
                message.Headers.Location = new Uri(Request.RequestUri + user.ID.ToString());
                return message;
            }
        }
    }
    catch (Exception ex)
    {
        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
    }
}
    }
}