using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ClassMethods
    {
        //Connection String
        //Kean ConnStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\c#\appdev\FINALS\CaseStudy\FinalActivity3\FinalActivity3_CaseStudy\FinalActivity3_CaseStudy\App_Data\Sales&InvSystemDB.mdf;Integrated Security=True";
        static string ConnStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\c#\appdev\FINALS\CaseStudy\FinalActivity3\FinalActivity3_CaseStudy\FinalActivity3_CaseStudy\App_Data\Sales&InvSystemDB.mdf;Integrated Security=True";

        SqlConnection sqlConn = new SqlConnection(ConnStr); //Conncetion Object

        /// <summary>
        /// Saves user input registered by calling the SaveUserRegisration stored procedure.
        /// </summary>
        public void SaveRecordRegisration(string name, string emailAddress, string passWord, string membershipType) //Saves the user registration details from Regisration.aspx(USER)
        {
            sqlConn.Open();
            SqlCommand saveRecord = new SqlCommand("SaveUserRegisration", sqlConn);
            saveRecord.CommandType = CommandType.StoredProcedure;

            //@Name, @EmailAddress, @Password, @MembershipType, 'false'(IsAdmin)
            saveRecord.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
            saveRecord.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = emailAddress;
            saveRecord.Parameters.Add("@Password", SqlDbType.NVarChar).Value = passWord;
            saveRecord.Parameters.Add("@MembershipType", SqlDbType.NVarChar).Value = membershipType;
            saveRecord.Parameters.Add("@IsAdmin", SqlDbType.Bit).Value = 0; //BIT accepts 0 or 1
            saveRecord.ExecuteNonQuery();
            sqlConn.Close();
        }
        //'SqlDbType.Bit' Instead of 'SqlDbType.NVarChar' - Error "false" if SqlDbType.NVarChar

        /// <summary>
        /// Check if account exist in database by calling the LoginAccountCheck stored procedure.
        /// </summary>
        public bool CheckLogin(string email, string password, out int userId) //Checks if user exists in the database
        {
            userId = 0;
            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand("LoginAccountCheck", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailAddress", email);
                cmd.Parameters.AddWithValue("@Password", password);

                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    userId = Convert.ToInt32(reader["UserID"]);
                    return true;
                }
            }
            return false;
        }

        #region Stores EmailAddress and Password in the class
        string EmailAddress, Password; //CTRL R + E
        public string EmailAddressClass { get => EmailAddress; set => EmailAddress = value; }
        public string PasswordClass { get => Password; set => Password = value; }
        string AdminEmailAddress, AdminPassword;
        public string AdminEmailAddressClass { get => AdminEmailAddress; set => AdminEmailAddress = value; }
        public string AdminPasswordClass { get => AdminPassword; set => AdminPassword = value; }
        #endregion

        /// <summary>
        /// Update user password to a new one by calling the UpdateUserPassword stored procedure.
        /// </summary>
        public void UpdateProfileManager(string emailAddress, string passWord, string newPassword) //Updates Password @ ProfileManager.aspx
        {
            sqlConn.Open();
            SqlCommand updateRecord = new SqlCommand("UpdateUserPassword", sqlConn);
            updateRecord.CommandType = CommandType.StoredProcedure;
            updateRecord.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = emailAddress;
            updateRecord.Parameters.Add("@Password", SqlDbType.NVarChar).Value = passWord;
            updateRecord.Parameters.Add("@NewPassword", SqlDbType.NVarChar).Value = newPassword;
            updateRecord.ExecuteNonQuery();
            sqlConn.Close();
        }

        /// <summary>
        /// Add products to carttable by calling the AddProductToCart stored procedure.
        /// </summary>
        public void AddToCartMethod(int userId, string productID, int quantity)
        {   //stored procedure to add to cart
            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand("AddProductToCart", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Match the stored-proc parameters:
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@ProductID", productID);
                cmd.Parameters.AddWithValue("@Quantity", quantity);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Returns a DataTable of all products by calling the GetAllProducts stored procedure.
        /// </summary>
        public DataTable GetAllProducts()
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand("GetAllProducts", conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                da.Fill(dt);
            }
            return dt;
        }

    }
}
