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
    public partial class postvote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //test the function with hardcoded value
            //AddVoteToDatabase("1", "100", "Yes");
            AddVoteToDatabase(Request.Form["voteId"], Request.Form["partnerId"], Request.Form["choice"]);

        }
        private void AddVoteToDatabase(string voteId, string partnerId, string choice)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SQLAzureConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            //create the sql statement for the insert - use @ variables as placeholders for parameter values 
            SqlCommand cmd = new SqlCommand(
            "INSERT INTO Results (VoteId, PartnerId, Choice) VALUES (@voteId, @partnerId, @choice)", connection);

            //define the parameter getting sent into the query 
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@voteId";
            //use the method argument passed in as the value for the parameter
            param.Value = voteId;
            //add the parameter to the .net command object's builtin parameters list
            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@partnerId";
            param.Value = partnerId;

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
