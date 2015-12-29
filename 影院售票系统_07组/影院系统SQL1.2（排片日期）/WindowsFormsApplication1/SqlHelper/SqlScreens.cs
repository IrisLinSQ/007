using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassUse;
using System.Data;

namespace SqlHelper
{
    public class SqlScreens
    {
        public static DataSet getAllScreensName()
        {
            String sql = "select SName from Screens";
            return
                 DbHelperSQL.Query(sql, "screenName");
        }
        public static Screens getScreenBySName(String sName)
        {
            Screens screen = new Screens();
            screen.setName(Convert.ToInt16(sName));
            screen.setRow((int)DbHelperSQL.GetSingle("select Row from Screens where SName='" + sName + "'"));
            screen.setCol((int)DbHelperSQL.GetSingle("select col from Screens where SName='" + sName + "'"));
            screen.setImage((string)DbHelperSQL.GetSingle("select SImage from Screens where SName='" + sName + "'"));
            return screen;
        }
        public static void scressAdd(Screens screen)
        {
            string insert = "insert into Screens(SName,Row,col,SImage) values(" 
                + screen.getName() + "," + screen.getRow() + "," + screen.getCol() + ",'" + screen.getImage() + "')";
            DbHelperSQL.ExecuteSql(insert);
        }
        public static void deleteScress(String scressName)
        {
            string insert = "delete from Screens where SName='" + scressName.Trim() + "'";
            DbHelperSQL.ExecuteSql(insert);
        }
        public static int allScreenNumber(int screen)
        {
            return
                (int)DbHelperSQL.GetSingle("select count(*) from Book_record where SName =" + screen + "");
        }
        public static int singleScreenNumber(int screen)
        {
            DateTime begin = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            DateTime finish = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));

            return 
                (int)DbHelperSQL.GetSingle("select count(*) from Book_record where Saletime >= '" + begin + "' and time <='" + finish + "'and SName =" + screen + "");
        }
    }
}
