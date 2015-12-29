using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassUse;
using System.Data;

namespace SqlHelper
{
    public class SqlPutPlan
    {
        public static PutPlan getSname(String moive,String time)
        {
            PutPlan putPlan = new PutPlan();
            putPlan.setScreenName((int)DbHelperSQL.GetSingle(
                "select SName from Put_Plan where MName='" + moive + "' and time='" + time + "'"));
            putPlan.setTime(Convert.ToDateTime(time));
            putPlan.setMoiveName(moive);
            return putPlan;

        }
        public static void putPlanAdd(PutPlan putPlan)
        {

            string insert = "insert into Put_Plan(MName,SName,time,PlanDay,timefinish) values ('" 
                + putPlan.getMoiveName() + "'," + putPlan.getScreenName() + ",'" + putPlan.getTime() + "','"+putPlan.getPlanDay()+"','"+putPlan.getFinishTime()+"')";
            DbHelperSQL.ExecuteSql(insert);
        }
        public static void deletePutPlan(PutPlan putPlan)
        {
            string insert = "delete from Put_plan where MName='" 
                + putPlan.getMoiveName()+ "' and SName='" + putPlan.getScreenName() + 
                "' and time = '" + putPlan.getTime() + "' and PlanDay = '"+putPlan.getPlanDay()+"'";
            DbHelperSQL.ExecuteSql(insert);
        }
        public static DataSet getAllMoiveName(String planDay)
        {
            string sql = "select distinct MName from Put_Plan where PlanDay = '"+planDay+"'";
            return 
                DbHelperSQL.Query(sql,"MName");
        }
        public static DataSet getAllScreensName(String planDay)
        {
            String sql = "select  distinct SName from Put_Plan where PlanDay = '" + planDay + "'";
            return
                DbHelperSQL.Query(sql, "SName");
        }
        public static DataSet getTimeByMoiveName(String moiveName,String planDay)
        {
            String sql = "select  distinct time from Put_Plan where MName='" + moiveName + "' and PlanDay='" + planDay + "'";
            return
                DbHelperSQL.Query(sql, "time");
        }
        public static DataSet getMoiveNameByScreenName(String screenName,String planDay)
        {
            string sql = "select  distinct MName from Put_Plan where SName='" + screenName + "' and PlanDay='"+planDay+"'";
            return
                DbHelperSQL.Query(sql, "MName");
        }
        public static DataSet getTimeByMoiveScressName(String moiveName, String screenName,String planDay)
        {
            string sql = "select  distinct time from Put_Plan where MName='" + moiveName + "'and SName='" + screenName + "' and PlanDay='" + planDay + "'";
            return
                DbHelperSQL.Query(sql, "time");
        }
        public static DataSet getbeginTimeByPlandayScressNamey(String planday, string screen)
        {
            string sql = "select * from Put_Plan where PlanDay='" + planday + "'and SName='" + screen + "'order by time";
            return
                DbHelperSQL.Query(sql, "time");
        }
        public static DataSet getbeginTimeByPlandayScressName(String planday, string screen)
        {
            string sql = "select * from Put_Plan where PlanDay='" + planday + "'and SName='" + screen + "' order by time desc";
            return
                DbHelperSQL.Query(sql, "time11");
        }

        //把连个time相加变成time
        public static String add(String time1, String time2)
        {
            int allminute = changeToMinute(time1) + changeToMinute(time2);
            return changeTime(allminute);
        }
        //把分钟数转化成time
        public static String changeTime(int lengh)
        {
            int h, m, s;
            int allsencend = lengh * 60;
            h = allsencend / 3600;
            allsencend -= h * 3600;
            m = allsencend / 60;
            s = allsencend -= m * 60;

            if (h >= 10 && m >= 10) return h + ":" + m + ":" + "0" + s;
            else if (h < 10 && m > 10) return "0" + h + ":" + m + ":" + "0" + s;
            else if (h > 10 && m < 10) return h + ":" + "0" + m + ":" + "0" + s;
            else return "0" + h + ":" + "0" + m + ":" + "0" + s;
        }

        //把字符转化为分钟数
        public static int changeToMinute(String time)
        {
            String h1, m1, s1;
            int h, m, s;
            h1 = time.Substring(0, 2);
            m1 = time.Substring(3, 2);
            s1 = time.Substring(6, 2);

            h = Convert.ToInt16(h1);
            m = Convert.ToInt16(m1);
            s = Convert.ToInt16(s1);

            return h * 60 + m;
        }
       
    }
}
