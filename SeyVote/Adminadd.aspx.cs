using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;


namespace SeyVote
{
    public partial class Adminadd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //test the function with hardcoded value
            //AddVoteToDatabase("1", "100", "Yes");
            AddVoteToDatabase(Request.Form["amdinname"], Request.Form["adminemail"], Request.Form["adminpassword"], Request.Form["admindisable"]);

        }
        private void AddVoteToDatabase(string adminname, string email, string password, string admindisable)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SQLAzureConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            //create the sql statement for the insert - use @ variables as placeholders for parameter values 
            SqlCommand cmd = new SqlCommand(
            "INSERT INTO Admins (Admin_Name, Email, Password, Admin_Disable) VALUES (@admin_name, @password, @email, @admindisable)", connection);

            //define the parameter getting sent into the query 
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@admin_name";
            //use the method argument passed in as the value for the parameter
            param.Value = adminname;
            //add the parameter to the .net command object's builtin parameters list
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@password";
            param.Value = password;

            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@email";
            param.Value = email;

            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@admindiasble";
            param.Value = admindisable;

            cmd.Parameters.Add(param);

            //open the database
            connection.Open();
            //run the insert statement query on the database
            cmd.ExecuteNonQuery();
            //close the database
            connection.Close();


        }
    }
}
