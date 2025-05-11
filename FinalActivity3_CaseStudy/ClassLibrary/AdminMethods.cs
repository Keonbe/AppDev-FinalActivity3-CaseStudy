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

        /// <summary>
        /// Checks if user is admin by calling the AdminLoginAccountCheck stored procedure.
        /// </summary>
        public bool CheckAdmin(string emailAddress, string passWord) //Check if user is an admin
        {
            const string procedureName = "AdminLoginAccountCheck";
            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(procedureName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure; //Parameter: @userName @passWord
                cmd.Parameters.Add("@userName", SqlDbType.NVarChar, 150).Value = emailAddress; 
                cmd.Parameters.Add("@passWord", SqlDbType.NVarChar, 50).Value = passWord;

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    return reader.HasRows; // if row exists, it means admin login matched
                }
            }
        }

        /// <summary>
        /// Add new products to products table by calling the AddNewProducts stored procedure.
        /// </summary>
        public void AddNewProducts(string productID, string productName, double basePrice, int stockAvailable) //Add new products to AddProducts.aspx
        {
            sqlConn.Open();
            SqlCommand saveRecord = new SqlCommand("AddNewProducts", sqlConn); //Stored Procedure name
            saveRecord.CommandType = CommandType.StoredProcedure;
            //Parameter: @productID, @ProductName, @Price, @Stocks
            saveRecord.Parameters.Add("@productID", SqlDbType.NVarChar).Value = productID;
            saveRecord.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = productName;
            saveRecord.Parameters.Add("@Price", SqlDbType.NVarChar).Value = basePrice;
            saveRecord.Parameters.Add("@Stocks", SqlDbType.NVarChar).Value = stockAvailable;
            saveRecord.ExecuteNonQuery(); //Execute Stored Procedure
            sqlConn.Close();
        }

        public static class DBHelper //static helper class for global/shared access to the connection string
        {
            public static string ConnectionString => @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\YourPath\Sales&InvSystemDB.mdf;Integrated Security=True";
        }

        /// <summary>
        /// Add a admin account to userTable by calling the SaveUserRegisration stored procedure(IsAdmin = 1).
        /// </summary>
        public void SaveRegisrationAdmin(string name, string emailAddress, string passWord, string membershipType) //Saves the user registration details from RegisterAdmin(USER)
        {
            sqlConn.Open();
            SqlCommand saveRecord = new SqlCommand("SaveUserRegisration", sqlConn); //Stored Procedure name
            saveRecord.CommandType = CommandType.StoredProcedure;
            //Parameter: @Name, @EmailAddress, @Password, @MembershipType, 'false'(IsAdmin)
            saveRecord.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
            saveRecord.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = emailAddress;
            saveRecord.Parameters.Add("@Password", SqlDbType.NVarChar).Value = passWord;
            saveRecord.Parameters.Add("@MembershipType", SqlDbType.NVarChar).Value = membershipType;
            saveRecord.Parameters.Add("@IsAdmin", SqlDbType.Bit).Value = 1; //BIT accepts 0 or 1 - True
            saveRecord.ExecuteNonQuery();
            sqlConn.Close();
        }

    }
}
