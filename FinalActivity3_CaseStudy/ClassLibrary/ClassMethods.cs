using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ClassLibrary
{
    public class ClassMethods
    {
        //Connection String
        //Kean ConnStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\c#\appdev\FINALS\CaseStudy\FinalActivity3\FinalActivity3_CaseStudy\FinalActivity3_CaseStudy\App_Data\Sales&InvSystemDB.mdf;Integrated Security=True";
        //SqlConnection sqlConn = new SqlConnection(ConnStr); //Conncetion Object

        // Use ConfigurationManager to get the connection string frtom webconfig
        string ConnStr = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;


        /// <summary>
        /// Saves user input registered by calling the SaveUserRegisration stored procedure.
        /// </summary>
        public void SaveRecordRegisration(string name, string emailAddress, string passWord, string membershipType) //Saves the user registration details from Regisration.aspx(USER)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            using (SqlCommand saveRecord = new SqlCommand("SaveUserRegisration", conn))
            {
                saveRecord.CommandType = CommandType.StoredProcedure;
                saveRecord.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
                saveRecord.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = emailAddress;
                saveRecord.Parameters.Add("@Password", SqlDbType.NVarChar).Value = passWord;
                saveRecord.Parameters.Add("@MembershipType", SqlDbType.NVarChar).Value = membershipType;
                saveRecord.Parameters.Add("@IsAdmin", SqlDbType.Bit).Value = 0;

                conn.Open();
                saveRecord.ExecuteNonQuery();
            } // Connection automatically closed here
        }
        //'SqlDbType.Bit' Instead of 'SqlDbType.NVarChar' - Error "false" if SqlDbType.NVarChar

        /// <summary>
        /// Check if account exist in database by calling the LoginAccountCheck stored procedure.
        /// </summary>
        public bool CheckLogin(string email, string password, out int userId, out string memType)
        {
            userId = 0;
            memType = string.Empty;
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
                    memType = reader["MembershipType"].ToString(); // ✅ Read from result set
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

            using (SqlConnection conn = new SqlConnection(ConnStr))
            using (SqlCommand updateRecord = new SqlCommand("UpdateUserPassword", conn))
            {
                updateRecord.CommandType = CommandType.StoredProcedure;
                updateRecord.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = emailAddress;
                updateRecord.Parameters.Add("@Password", SqlDbType.NVarChar).Value = passWord;
                updateRecord.Parameters.Add("@NewPassword", SqlDbType.NVarChar).Value = newPassword;

                conn.Open();
                updateRecord.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Add products to carttable by calling the AddProductToCart stored procedure.
        /// </summary>
        public void AddToCartMethod(int userId, string productID, int quantity)
        {
            try
            {
                using (var conn = new SqlConnection(ConnStr))
                using (var cmd = new SqlCommand("AddProductToCart", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Explicitly define parameters
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;
                    cmd.Parameters.Add("@ProductID", SqlDbType.NChar, 10).Value = productID.PadRight(10).Substring(0, 10); // Ensure 10 chars
                    cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Log error or rethrow
                throw new Exception("AddToCart failed: " + ex.Message, ex);
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

        //-------------------------------------------------------------------// Main: Shopping Cart

        /// <summary>
        /// Retrieves all cart items for the given user.
        /// </summary>
        public DataTable GetCartItems(int userId)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand("SC_GetCartItems", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                conn.Open();
                using (var da = new SqlDataAdapter(cmd))
                    da.Fill(dt);
            }
            return dt;
        }

        /// <summary>
        /// Removes one item from the user's cart.
        /// </summary>
        public void RemoveCartItem(int userId, int cartId)
        {
            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand("SC_RemoveCartItem", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@CartId", cartId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Processes the user's entire cart checkout in one atomic operation.
        /// </summary>
        public void ProcessCartCheckout(int userId, double subTotal, string memType, double discountRate, double finalTotal)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SC_ProcessCheckout", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@SubTotal", subTotal);
                    cmd.Parameters.AddWithValue("@MemType", memType);
                    cmd.Parameters.AddWithValue("@DiscountRate", discountRate);
                    cmd.Parameters.AddWithValue("@FinalTotal", finalTotal);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
