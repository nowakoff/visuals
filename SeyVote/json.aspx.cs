using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;


namespace SeyVote
{
    public partial class json : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SQLAzureConnection"].ConnectionString;
            string queryString = "SELECT * FROM Partners";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {   //format should look like this: {“SourceDigital”:[

                    Response.Write(String.Format("{2}{0}{1}{0}: {3}", "\"", "Partners", "{", "["));
                    int x = 0;

                    while (reader.Read())
                    {
                        if (x == 0)
                        {
                            Response.Write(String.Format("{2}{3}{0}{3}:{1},{3}{5}{3}:{3}{6}{3}{4}", reader.GetName(0), reader[0], "{", "\"", "}", reader.GetName(1), reader[1]));
                        }
                        else
                        {
                            Response.Write(String.Format(",{2}{3}{0}{3}:{1},{3}{5}{3}:{3}{6}{3}{4}", reader.GetName(0), reader[0], "{", "\"", "}", reader.GetName(1), reader[1]));
                        }
                        x = x + 1;

                    }

                    //format should look like this: ]}   

                    Response.Write(String.Format("{0} {1}", "]", "}"));



                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                    // Response.Redirect("helloworldnowak.html");
                }
            }

        }
    }
}