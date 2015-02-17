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
    public partial class choicevote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //test the function with hardcoded value
            //AddVoteToDatabase("1", "fgg", "Yes");
            AddVoteToDatabase(Request.Form["vote_name"], Request.Form["choice[]"]);

        }
        private void AddVoteToDatabase( string vote_name, string choice)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SQLAzureConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            //create the sql statement for the insert - use @ variables as placeholders for parameter values 
            SqlCommand cmd = new SqlCommand(
            "INSERT INTO Choices ( Vote_Name, Choice) VALUES (@vote_name, @choice)", connection);

            //define the parameter getting sent into the query 
            SqlParameter param = new SqlParameter();
         

            param = new SqlParameter();
            param.ParameterName = "@vote_name";
            param.Value = vote_name;

            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@choice";
            param.Value = choice;

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
