using System;
using System.Data;
using System.Data.SqlClient;

namespace ClassLibrary
{
    public class AdminMethods
    {
        //Connection String
        static string ConnStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\c#\appdev\FINALS\CaseStudy\FinalActivity3\FinalActivity3_CaseStudy\FinalActivity3_CaseStudy\App_Data\Sales&InvSystemDB.mdf;Integrated Security=True";

        SqlConnection sqlConn = new SqlConnection(ConnStr); //Conncetion Object
 
        public bool CheckAdmin(string emailAddress, string passWord)
        {
            const string procedureName = "AdminLoginAccountCheck";
            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(procedureName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userName", SqlDbType.NVarChar, 150).Value = emailAddress;
                cmd.Parameters.Add("@passWord", SqlDbType.NVarChar, 50).Value = passWord;

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.HasRows)
                        return false;

                    reader.Read();
                    var isAdminDb = reader["IsAdmin"].ToString().Trim();
                    return isAdminDb.Equals("true", StringComparison.OrdinalIgnoreCase);
                }
            }
        }




    }
}
