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
        static string ConnStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\c#\appdev\FINALS\CaseStudy\FinalActivity3\FinalActivity3_CaseStudy\FinalActivity3_CaseStudy\App_Data\Sales&InvSystemDB.mdf;Integrated Security=True";

        //Conncetion Object
        SqlConnection sqlConn = new SqlConnection(ConnStr);

        public void SaveRecordRegisration(string name, string emailAddress, string passWord, string membershipType)
        {
            sqlConn.Open();
            SqlCommand saveRecord = new SqlCommand("SaveUserRegisration", sqlConn);
            saveRecord.CommandType = CommandType.StoredProcedure;

            //@Name, @EmailAddress, @Password, @MembershipType, 'false'(IsAdmin)
            saveRecord.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
            saveRecord.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = emailAddress;
            saveRecord.Parameters.Add("@Password", SqlDbType.NVarChar).Value = passWord;
            saveRecord.Parameters.Add("@MembershipType", SqlDbType.NVarChar).Value = membershipType;
            saveRecord.Parameters.Add("@IsAdmin", SqlDbType.NVarChar).Value = "false";
            saveRecord.ExecuteNonQuery();
            sqlConn.Close();
        }

        public bool CheckLogin(string emailAddress, string passWord)
        {
            bool isValid = false;
            sqlConn.Open();
            SqlCommand checkLogin = new SqlCommand("LoginAccountCheck", sqlConn);
            checkLogin.CommandType = CommandType.StoredProcedure;
            //@EmailAddress, @Password
            checkLogin.Parameters.Add("@userName", SqlDbType.NVarChar).Value = emailAddress;
            checkLogin.Parameters.Add("@passWord", SqlDbType.NVarChar).Value = passWord;
            SqlDataReader reader = checkLogin.ExecuteReader();
            while (reader.Read())
            {
                emailAddress = reader["EmailAddress"].ToString();
                isValid = true;
                break;
            }
            sqlConn.Close();
            return isValid;
        }


        #region
        string EmailAddress, Password; //CTRL R + E
        public string EmailAddressClass { get => EmailAddress; set => EmailAddress = value; }
        public string PasswordClass { get => Password; set => Password = value; }
        #endregion

    }
}
