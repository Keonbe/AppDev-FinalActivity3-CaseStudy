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
    /// <summary>
    /// Provides data access methods for user management, authentication, and shopping cart operations.
    /// </summary>
    public class ClassMethods
    {
        //Connection String
        //Kean ConnStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\c#\appdev\FINALS\CaseStudy\FinalActivity3\FinalActivity3_CaseStudy\FinalActivity3_CaseStudy\App_Data\Sales&InvSystemDB.mdf;Integrated Security=True";
        //SqlConnection sqlConn = new SqlConnection(ConnStr); //Conncetion Object

        /// <summary>
        /// Connection string retrieved from the application's configuration.
        /// </summary>
        string ConnStr = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;


        /// <summary>
        /// Registers a new user in the system after verifying the email doesn't already exist.
        /// </summary>
        /// <param name="name">The user's full name.</param>
        /// <param name="emailAddress">The user's email address (must be unique).</param>
        /// <param name="passWord">The user's password.</param>
        /// <param name="membershipType">The type of membership for the user.</param>
        /// <exception cref="InvalidOperationException">Thrown when the email address already exists in the database.</exception>
        /// <remarks>
        /// This method performs two operations:
        /// 1. Checks if the email already exists to prevent duplicate registrations
        /// 2. Saves the user information using the SaveUserRegisration stored procedure
        /// </remarks>
        public void SaveRecordRegistration(string name, string emailAddress, string passWord, string membershipType) //Saves the user registration details from Regisration.aspx(USER)
        {
            using (var conn = new SqlConnection(ConnStr))
            {
                conn.Open();

                // 1) Check for existing email
                using (var check = new SqlCommand(
                    "SELECT COUNT(1) FROM UserInfoTable WHERE EmailAddress = @Email", conn))
                {
                    check.Parameters.AddWithValue("@Email", emailAddress);
                    var exists = (int)check.ExecuteScalar() > 0;
                    if (exists)
                        throw new InvalidOperationException(
                            $"A user with email '{emailAddress}' already exists.");
                }

                // 2) If not exists, call your stored procedure
                using (var saveRecord = new SqlCommand("SaveUserRegisration", conn))
                {
                    saveRecord.CommandType = CommandType.StoredProcedure;
                    saveRecord.Parameters.Add("@Name", SqlDbType.NVarChar, 150).Value = name;
                    saveRecord.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 150).Value = emailAddress;
                    saveRecord.Parameters.Add("@Password", SqlDbType.NVarChar, 50).Value = passWord;
                    saveRecord.Parameters.Add("@MembershipType", SqlDbType.NVarChar, 10).Value = membershipType;
                    saveRecord.Parameters.Add("@IsAdmin", SqlDbType.Bit).Value = 0;

                    saveRecord.ExecuteNonQuery();
                } // Connection automatically closed here
            }
        } //'SqlDbType.Bit' Instead of 'SqlDbType.NVarChar' - Error "false" if SqlDbType.NVarChar

        /// <summary>
        /// Verifies user credentials and retrieves user information.
        /// </summary>
        /// <param name="email">The email address of the user attempting to log in.</param>
        /// <param name="password">The password associated with the user account.</param>
        /// <param name="userId">Output parameter that will contain the user ID if login is successful.</param>
        /// <param name="memType">Output parameter that will contain the membership type if login is successful.</param>
        /// <returns>
        /// Returns <c>true</c> if the login credentials are valid; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method uses the LoginAccountCheck stored procedure to verify user credentials.
        /// </remarks>
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
        /// <summary>
        /// Private field to store user email address.
        /// </summary>
        string EmailAddress, Password; //CTRL R + E

        /// <summary>
        /// Gets or sets the email address of the current user.
        /// </summary>
        public string EmailAddressClass { get => EmailAddress; set => EmailAddress = value; }

        /// <summary>
        /// Gets or sets the password of the current user.
        /// </summary>
        public string PasswordClass { get => Password; set => Password = value; }

        /// <summary>
        /// Private field to store admin email address.
        /// </summary>
        string AdminEmailAddress, AdminPassword;

        /// <summary>
        /// Gets or sets the email address of the current admin.
        /// </summary>
        public string AdminEmailAddressClass { get => AdminEmailAddress; set => AdminEmailAddress = value; }

        /// <summary>
        /// Gets or sets the password of the current admin.
        /// </summary>
        public string AdminPasswordClass { get => AdminPassword; set => AdminPassword = value; }
        #endregion

        /// <summary>
        /// Updates a user's password after verifying current credentials.
        /// </summary>
        /// <param name="emailAddress">The email address of the user.</param>
        /// <param name="passWord">The current password of the user.</param>
        /// <param name="newPassword">The new password to set for the user.</param>
        /// <remarks>
        /// This method calls the UpdateUserPassword stored procedure to update the user's password.
        /// </remarks>
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
        /// Adds a product to the user's shopping cart.
        /// </summary>
        /// <param name="userId">The ID of the user adding the product to their cart.</param>
        /// <param name="productID">The ID of the product being added to the cart.</param>
        /// <param name="quantity">The quantity of the product to add.</param>
        /// <exception cref="Exception">Thrown when the product cannot be added to the cart.</exception>
        /// <remarks>
        /// This method calls the AddProductToCart stored procedure to add the product to the user's cart.
        /// </remarks>
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
        /// Retrieves a list of all available products from the database.
        /// </summary>
        /// <returns>A DataTable containing all products with their details.</returns>
        /// <remarks>
        /// This method calls the GetAllProducts stored procedure to retrieve the product data.
        /// </remarks>
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
        /// Retrieves all items currently in the user's shopping cart.
        /// </summary>
        /// <param name="userId">The ID of the user whose cart items are being retrieved.</param>
        /// <returns>A DataTable containing all items in the user's cart with their details.</returns>
        /// <remarks>
        /// This method calls the SC_GetCartItems stored procedure to retrieve cart data.
        /// </remarks>
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
        /// Removes a specific item from the user's shopping cart.
        /// </summary>
        /// <param name="userId">The ID of the user who owns the cart.</param>
        /// <param name="cartId">The ID of the cart item to remove.</param>
        /// <remarks>
        /// This method calls the SC_RemoveCartItem stored procedure to remove the item from the cart.
        /// </remarks>
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
        /// Processes the checkout operation for all items in the user's cart.
        /// </summary>
        /// <param name="userId">The ID of the user checking out.</param>
        /// <param name="subTotal">The subtotal amount before applying discounts.</param>
        /// <param name="memType">The membership type of the user, which determines the discount rate.</param>
        /// <param name="discountRate">The discount rate to apply to the subtotal.</param>
        /// <param name="finalTotal">The final total amount after applying the discount.</param>
        /// <remarks>
        /// This method calls the SC_ProcessCheckout stored procedure to process the checkout in one atomic operation.
        /// The stored procedure handles:
        /// - Creating an order record
        /// - Transferring cart items to order details
        /// - Updating inventory quantities
        /// - Clearing the user's cart
        /// </remarks>
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
        public DataSet DisplaySalesSummary()
        { 
            SqlDataAdapter da = new SqlDataAdapter("rdlc_GetSalesSummaryByProduct", ConnStr);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            da.Fill(ds, "MyTable");
            return ds;
        }

    }
}
