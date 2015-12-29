using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassUse;
using System.Data;

namespace SqlHelper
{
    public class SqlMoive
    {
        public static Moive getMoive(String moiveName)
        {
            Moive moive = new Moive();

            String sql = "select * from Movie where MName='" + moiveName + "'";
            DataSet ds = DbHelperSQL.Query(sql, "Moive");

            moive.setName(ds.Tables["Moive"].Rows[0]["MName"].ToString().Trim());
            moive.setType(ds.Tables["Moive"].Rows[0]["MType"].ToString().Trim());
            moive.setDirector(ds.Tables["Moive"].Rows[0]["MDirector"].ToString().Trim());
            moive.setActor(ds.Tables["Moive"].Rows[0]["MActor"].ToString().Trim());
            moive.setImage(ds.Tables["Moive"].Rows[0]["MImage"].ToString().Trim());
            moive.setStory(ds.Tables["Moive"].Rows[0]["MStory"].ToString().Trim());
            moive.setLength(Convert.ToInt16(ds.Tables["Moive"].Rows[0]["MLength"].ToString().Trim()));
            moive.setPutDate(Convert.ToDateTime(ds.Tables["Moive"].Rows[0]["MPutDay"].ToString().Trim()));
            moive.setFare((float)Convert.ToDouble(ds.Tables["Moive"].Rows[0]["MFare"].ToString().Trim()));
            
            return moive;
        }
        public static DataSet getAllMoiveName()
        {
            String sql = "select MName from Movie";
            return
                DbHelperSQL.Query(sql,"moiveName");
        }

        public static void addMoive(Moive moive)
        {
            string insert = "insert into Movie(MName,MType,MDirector,MActor,MPutDay,MLength,MFare,MImage,MStory) values('" 
                + moive.getName() + "','" + moive.getType() + "','" + moive.getDirector() + "','" + moive.getActor() + "','" 
                + moive.getPutDate() + "'," + moive.getLength() + "," + moive.getFare() + ",'" + moive.getImage() + "','" 
                + moive.getStory() + "')";
            DbHelperSQL.ExecuteSql(insert);
        }
        public static void deleteMoive(String moiveName)
        {
            string insert = "delete from Movie where MName='" + moiveName.Trim() + "'";
            DbHelperSQL.ExecuteSql(insert);

        }
        public static int allMoiveNumber(String moive)
        {
            return 
                (int)DbHelperSQL.GetSingle("select count(*) from Book_record where MName ='" + moive + "'");
        }
        public static int singleMoiveNumber(String moive)
        {
            DateTime begin = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            DateTime finish = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));

            return 
                (int)DbHelperSQL.GetSingle("select count(*) from Book_record where Saletime >= '" + begin + "' and time <='" + finish + "'and MName ='" + moive + "'");
        }
    }
}
