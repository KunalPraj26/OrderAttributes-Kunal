using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace OrderValuesInAttributes
{
    public class Connection
    {

        public Connection(string connection)
        {
            string connetionString;
            SqlConnection cnn;
            //connetionString = @"Data Source=BRZBEL000599L;Initial Catalog=IAM;Integrated Security=True";
            connetionString = @"Data Source="+ System.Environment.MachineName+"; Initial Catalog=IAM;Integrated Security=True";
            cnn = new SqlConnection(connetionString);
            cnn.Open();


        }

        public static SqlConnection DBConnection (string connectionString)
        {
   
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            return cnn;

        }


    }
}
