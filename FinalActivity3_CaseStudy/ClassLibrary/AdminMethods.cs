using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ClassLibrary
{
    public class AdminMethods
    {
        //Connection String
        //Kean static string ConnStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\c#\appdev\FINALS\CaseStudy\FinalActivity3\FinalActivity3_CaseStudy\FinalActivity3_CaseStudy\App_Data\Sales&InvSystemDB.mdf;Integrated Security=True";
        //SqlConnection sqlConn = new SqlConnection(ConnStr); //Conncetion Object

        string ConnStr = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;


        /// <summary>
        /// Checks if user is admin by calling the AdminLoginAccountCheck stored procedure.
        /// </summary>
        public bool CheckAdmin(string emailAddress, string passWord)
        {
            const string procedureName = "AdminLoginAccountCheck";

            try
            {
                using (var conn = new SqlConnection(ConnStr))
                using (var cmd = new SqlCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userName", SqlDbType.NVarChar, 150).Value = emailAddress;
                    cmd.Parameters.Add("@passWord", SqlDbType.NVarChar, 50).Value = passWord;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log the specific SQL error (you could add logging here)
                throw new Exception("Admin verification failed due to database error", ex);
            }
            catch (Exception ex)
            {
                // Catch any other unexpected errors
                throw new Exception("Admin verification failed", ex);
            }
        }

        /// <summary>
        /// Add new products to products table by calling the AddNewProducts stored procedure.
        /// </summary>
        public void AddNewProducts(string productID, string productName, double basePrice, int stockAvailable)
        {
            try
            {
                using (var conn = new SqlConnection(ConnStr))
                using (var saveRecord = new SqlCommand("AddNewProducts", conn))
                {
                    saveRecord.CommandType = CommandType.StoredProcedure;
                    saveRecord.Parameters.Add("@productID", SqlDbType.NVarChar, 10).Value = productID;
                    saveRecord.Parameters.Add("@ProductName", SqlDbType.NVarChar, 100).Value = productName;
                    saveRecord.Parameters.Add("@Price", SqlDbType.Float).Value = basePrice;
                    saveRecord.Parameters.Add("@Stocks", SqlDbType.Int).Value = stockAvailable;

                    conn.Open();
                    saveRecord.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Product addition failed: " + ex.Message, ex);
            }
        }

        public static class DBHelper //static helper class for global/shared access to the connection string
        {
            public static string ConnectionString => @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\YourPath\Sales&InvSystemDB.mdf;Integrated Security=True";
        }

        /// <summary>
        /// Add a admin account to userTable by calling the SaveUserRegisration stored procedure(IsAdmin = 1).
        /// </summary>
        public void SaveRegisrationAdmin(string name, string emailAddress, string passWord, string membershipType)
        {
            try
            {
                using (var conn = new SqlConnection(ConnStr))
                using (var saveRecord = new SqlCommand("SaveUserRegisration", conn))
                {
                    saveRecord.CommandType = CommandType.StoredProcedure;
                    saveRecord.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
                    saveRecord.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = emailAddress;
                    saveRecord.Parameters.Add("@Password", SqlDbType.NVarChar).Value = passWord;
                    saveRecord.Parameters.Add("@MembershipType", SqlDbType.NVarChar).Value = membershipType;
                    saveRecord.Parameters.Add("@IsAdmin", SqlDbType.Bit).Value = 1;

                    conn.Open();
                    saveRecord.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Admin registration failed: " + ex.Message, ex);
            }
        }

    }
}
