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

            string insert = "insert into Put_Plan(MName,SName,time) values ('" 
                + putPlan.getMoiveName() + "'," + putPlan.getScreenName() + ",'" + putPlan.getTime() + "')";
            DbHelperSQL.ExecuteSql(insert);
        }
        public static void deletePutPlan(PutPlan putPlan)
        {
            string insert = "delete from Put_plan where MName='" 
                + putPlan.getMoiveName()+ "' and SName='" + putPlan.getScreenName() + "' and time = '" + putPlan.getTime() + "'";
            DbHelperSQL.ExecuteSql(insert);
        }
        public static DataSet getAllMoiveName()
        {
            string sql = "select distinct MName from Put_Plan";
            return 
                DbHelperSQL.Query(sql,"MName");
        }
        public static DataSet getAllScreensName()
        {
            String sql = "select  distinct SName from Put_Plan";
            return
                DbHelperSQL.Query(sql, "SName");
        }
        public static DataSet getTimeByMoiveName(String moiveName)
        {
            String sql = "select  distinct time from Put_Plan where MName='" + moiveName + "'";
            return
                DbHelperSQL.Query(sql, "time");
        }
        public static DataSet getMoiveNameByScreenName(String screenName)
        {
            string sql = "select  distinct MName from Put_Plan where SName='" + screenName + "'";
            return
                DbHelperSQL.Query(sql, "MName");
        }
        public static DataSet getTimeByMoiveScressName(String moiveName, String screenName)
        {
            string sql = "select  distinct time from Put_Plan where MName='" + moiveName + "'and SName='" + screenName + "'";
            return
                DbHelperSQL.Query(sql, "time");
        }
    }
}
