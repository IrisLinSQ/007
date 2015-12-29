using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassUse;
using System.Xml;
using System.Data;

namespace SqlHelper
{
    public class SqlAdmin
    {
        Admin admin = new Admin();

        public static void addAdmin(Admin admin)
        {
            string insert = "insert into Administrator(ID,Password) values ('" 
                + admin.getID() + "','" + admin.getPassword() + "')";
            DbHelperSQL.ExecuteSql(insert);
        }
        public static Boolean exitById(String id)
        {

            string check = "select ID from Administrator";
            DataSet ds = DbHelperSQL.Query(check, "ID");
            foreach (DataRow row in ds.Tables["ID"].Rows)
            {
                if (row["ID"].ToString().Trim() == id.Trim())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
