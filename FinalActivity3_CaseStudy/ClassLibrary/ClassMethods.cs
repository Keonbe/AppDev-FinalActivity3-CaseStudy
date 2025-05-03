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
        static string ConnStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\Documents\c#\appdev\FINALS\Lec_FinalActivity1\Lec_FinalActivity1\App_Data\Database1.mdf;Integrated Security=True";

        //Conncetion Object
        SqlConnection sqlConn = new SqlConnection(ConnStr);
    }
}
