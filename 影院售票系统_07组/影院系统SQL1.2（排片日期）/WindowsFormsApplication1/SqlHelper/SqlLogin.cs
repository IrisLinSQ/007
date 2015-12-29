using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassUse;
using System.Data;

namespace SqlHelper
{
    public class SqlLogin
    {
        public static Admin findAdminById(String id)
        {
            Admin admin = new Admin();
            if (SqlAdmin.exitById(id)) admin.setID(id);
            else return null;
            String sql = "select Password from Administrator where ID ='" + id + "'";
            string password = (String)DbHelperSQL.GetSingle(sql);
            admin.setPassword(password);
            return admin;
            
        }
        public static void addLogin(Login login)
        {
            String loginsql = "insert  into Login_record values('" + login.getID().Trim() + "','" + login.getTime()+ "')";
            DbHelperSQL.ExecuteSql(loginsql);
        }
    }
}
