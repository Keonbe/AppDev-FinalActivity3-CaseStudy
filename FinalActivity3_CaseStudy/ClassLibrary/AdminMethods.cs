using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ClassLibrary
{
    /// <summary>
    /// Provides data access methods for administrator operations in the system,
    /// including admin authentication, product management, and admin account creation.
    /// </summary>
    public class AdminMethods
    {
        //Connection String
        //Kean static string ConnStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\c#\appdev\FINALS\CaseStudy\FinalActivity3\FinalActivity3_CaseStudy\FinalActivity3_CaseStudy\App_Data\Sales&InvSystemDB.mdf;Integrated Security=True";
        //SqlConnection sqlConn = new SqlConnection(ConnStr); //Conncetion Object

        /// <summary>
        /// Connection string retrieved from the application's configuration.
        /// </summary>
        string ConnStr = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;


        /// <summary>
        /// Verifies if the provided email and password match an existing admin account.
        /// </summary>
        /// <param name="emailAddress">The email address of the admin attempting to log in.</param>
        /// <param name="passWord">The password associated with the admin account.</param>
        /// <returns>
        /// Returns <c>true</c> if the admin exists and credentials match; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="Exception">
        /// Thrown when database access fails or an unexpected error occurs.
        /// </exception>
        /// <remarks>
        /// This method executes the AdminLoginAccountCheck stored procedure to verify admin credentials.
        /// If an exception occurs during the database operation, the method wraps it in a more descriptive
        /// exception before rethrowing.
        /// </remarks>
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
        /// Adds a new product to the product catalog in the database.
        /// </summary>
        /// <param name="productID">The unique identifier for the product (max 10 characters).</param>
        /// <param name="productName">The name of the product (max 100 characters).</param>
        /// <param name="basePrice">The base price of the product.</param>
        /// <param name="stockAvailable">The initial quantity of stock available for the product.</param>
        /// <exception cref="Exception">
        /// Thrown when the product cannot be added to the database due to SQL errors.
        /// </exception>
        /// <remarks>
        /// This method calls the AddNewProducts stored procedure to insert the product data.
        /// </remarks>
        public void AddNewProducts(string productID, string productName, double basePrice, int stockAvailable)
        {
            var conn = new SqlConnection(ConnStr);
            conn.Open();

            // 1) Check for existing product
            using (var check = new SqlCommand(
                "SELECT COUNT(1) FROM ProductInventoryTable WHERE ProductID = @PID", conn))
            {
                check.Parameters.AddWithValue("@PID", productID);
                bool exists = (int)check.ExecuteScalar() > 0;
                if (exists)
                    throw new InvalidOperationException(
                        $"Product with ID '{productID}' already exists.");
            }

            // 2) Insert via stored procedure or inline SQL
            try
            {
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


        /// <summary>
        /// Creates a new administrator account in the system.
        /// </summary>
        /// <param name="name">The full name of the administrator.</param>
        /// <param name="emailAddress">The email address for the administrator account (must be unique).</param>
        /// <param name="passWord">The password for the administrator account.</param>
        /// <param name="membershipType">The type of membership to assign to the administrator.</param>
        /// <exception cref="Exception">
        /// Thrown when the administrator account cannot be created due to SQL errors.
        /// </exception>
        /// <remarks>
        /// This method calls the SaveUserRegisration stored procedure with IsAdmin set to 1 to indicate
        /// that the created account has administrator privileges. Unlike regular user registration,
        /// this method does not check for existing email addresses before attempting to save.
        /// </remarks>
        public void SaveRegisrationAdmin(string name, string emailAddress, string passWord, string membershipType)
        {

            var conn = new SqlConnection(ConnStr);
            conn.Open();

            // 1) Check for existing email
            using (var check = new SqlCommand(
                "SELECT COUNT(1) FROM UserInfoTable WHERE EmailAddress = @Email", conn))
            {
                check.Parameters.AddWithValue("@Email", emailAddress);
                bool exists = (int)check.ExecuteScalar() > 0;
                if (exists)
                    throw new InvalidOperationException(
                        $"A user with email '{emailAddress}' already exists.");
            }

            // 2) Insert via stored procedure or inline SQL
            try
            {
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
