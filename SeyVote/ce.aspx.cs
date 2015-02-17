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
    public partial class ce : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //test the function with hardcoded value
            //AddVoteToDatabase("vote ques", "answer Yes");
            AddVoteToDatabase(Request.Form["vote_name"], Request.Form["vote_question"], Request.Form["quorum"], Request.Form["type"]);



            AddVoteToDatabase2(Request.Form["vote_name"]);

        }
        private void AddVoteToDatabase(string vote_name, string vote_question, string quorum, string type)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SQLAzureConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            //create the sql statement for the insert - use @ variables as placeholders for parameter values 
            SqlCommand cmd = new SqlCommand(
            "INSERT INTO Votes (Vote_Name, Vote_Question, Quorum, Type) VALUES ( @vote_name, @vote_question, @quorum, @type)", connection);



            //define the parameter getting sent into the query 
            SqlParameter param = new SqlParameter();


            param = new SqlParameter();
            param.ParameterName = "@vote_name";
            param.Value = vote_name;

            cmd.Parameters.Add(param);


            param = new SqlParameter();
            param.ParameterName = "@vote_question";
            param.Value = vote_question;

            cmd.Parameters.Add(param);


            param = new SqlParameter();
            param.ParameterName = "@quorum";
            param.Value = quorum;

            cmd.Parameters.Add(param);

            param = new SqlParameter();
            param.ParameterName = "@type";
            param.Value = type;

            cmd.Parameters.Add(param);


            //open the database
            connection.Open();
            //run the insert statement query on the database
            cmd.ExecuteNonQuery();

            //close the database
            connection.Close();


        }
        private void AddVoteToDatabase2(string vote_name)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SQLAzureConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);


            //create the sql statement for the insert - use @ variables as placeholders for parameter values 
            SqlCommand cmd = new SqlCommand(
            "INSERT INTO Choices (Vote_Name) VALUES ( @vote_name)", connection);

            



            //define the parameter getting sent into the query 
            SqlParameter param = new SqlParameter();


            param = new SqlParameter();
            param.ParameterName = "@vote_name";
            param.Value = vote_name;

            cmd.Parameters.Add(param);


          

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