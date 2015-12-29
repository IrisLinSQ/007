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
        public static int getLengthByMoive(String moive)
        {
            String sql = "select MLength from Movie where MName = '" + moive + "'";
            return (int)DbHelperSQL.GetSingle(sql);
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
        public static int allMoiveNumber(int screen)
        {
            return
                (int)DbHelperSQL.GetSingle("select count(*) from Book_record where SName =" + screen + "");
        }
        public static int singleMoiveNumber(String moive)
        {
            DateTime begin = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            DateTime finish = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));

            return 
                (int)DbHelperSQL.GetSingle("select count(*) from Book_record where Saletime >= '" + begin + "' and time <='" + finish + "'and MName ='" + moive + "'");
        }
        public static int singleMoiveNumber(int screen)
        {
            DateTime begin = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            DateTime finish = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));

            return
                (int)DbHelperSQL.GetSingle("select count(*) from Book_record where Saletime >= '" + begin + "' and time <='" + finish + "'and SName =" + screen + "");
        }
        public static int allNormalNumber(String moive)
        {
            return
                (int)DbHelperSQL.GetSingle("select count(*) from Book_record where MName ='" + moive + "'and ConsumerType = 'normal'");
        }
        public static int allNormalNumber(int screen)
        {
            return
                (int)DbHelperSQL.GetSingle("select count(*) from Book_record where SName =" + screen + " and ConsumerType = 'normal'");
        }
        public static int todayNormalNumbee(String moive)
        {
            DateTime begin = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            DateTime finish = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));

            return
                (int)DbHelperSQL.GetSingle("select count(*) from Book_record where Saletime >= '"
                + begin + "' and time <='" + finish + "'and MName ='" + moive + "'and ConsumerType = 'normal'");
        }
        public static int todayNormalNumbee(int screen)
        {
            DateTime begin = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            DateTime finish = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));

            return
                (int)DbHelperSQL.GetSingle("select count(*) from Book_record where Saletime >= '"
                + begin + "' and time <='" + finish + "'and SName =" + screen + " and ConsumerType = 'normal'");
        }
        public static int allFreeNumber(String moive)
        {
            return
                (int)DbHelperSQL.GetSingle("select count(*) from Book_record where MName ='" + moive + "'and ConsumerType = 'free'");
        }
        public static int allFreeNumber(int screen)
        {
            return
                (int)DbHelperSQL.GetSingle("select count(*) from Book_record where SName =" + screen + " and ConsumerType = 'free'");
        }
        public static int todayFreeNumber(String moive)
        {
            DateTime begin = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            DateTime finish = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));

            return
                (int)DbHelperSQL.GetSingle("select count(*) from Book_record where Saletime >= '"
                + begin + "' and time <='" + finish + "'and MName ='" + moive + "'and ConsumerType = 'free'");
        }
        public static int todayFreeNumber(int screen)
        {
            DateTime begin = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            DateTime finish = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));

            return
                (int)DbHelperSQL.GetSingle("select count(*) from Book_record where Saletime >= '"
                + begin + "' and time <='" + finish + "'and SName =" + screen + " and ConsumerType = 'free'");
        }
        public static int allStuNumber(String moive)
        {
            return
                (int)DbHelperSQL.GetSingle("select count(*) from Book_record where MName ='" + moive + "'and ConsumerType = 'student'");
        }
        public static int allStuNumber(int screen)
        {
            return
                (int)DbHelperSQL.GetSingle("select count(*) from Book_record where SName = " + screen + " and ConsumerType = 'student'");
        }
        public static int todayStuNumber(String moive)
        {
            DateTime begin = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            DateTime finish = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));

            return
                (int)DbHelperSQL.GetSingle("select count(*) from Book_record where Saletime >= '"
                + begin + "' and time <='" + finish + "'and MName ='" + moive + "'and ConsumerType = 'student'");
        }
        public static int todayStuNumber(int screen)
        {
            DateTime begin = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            DateTime finish = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));

            return
                (int)DbHelperSQL.GetSingle("select count(*) from Book_record where Saletime >= '"
                + begin + "' and time <='" + finish + "'and SName =" + screen + " and ConsumerType = 'student'");
        }
        public static float getPrice(String moive)
        {
            return
                (float)Convert.ToDouble(DbHelperSQL.GetSingle("select MFare from Movie where MName ='" + moive + "'"));
        }
        public static float allMoneyNumber(String moive)
        {
            float money = SqlMoive.getPrice(moive);
            int number = SqlMoive.allMoiveNumber(moive);
            return money * (float)number;
        }
        //public static float allMoneyNumber(int screen)
        //{
        //    float money ;
        //    int number;
        //    float allMoney = 0;
        //    DataSet ds = SqlMoive.getAllMoiveName();
        //    foreach (DataRow row1 in ds.Tables[0].Rows)
        //    {
        //        money = SqlMoive.getPrice(row1[0].ToString().Trim());
        //        number = SqlMoive.allMoiveNumber(row1[0].ToString().Trim());
        //        allMoney += (money * (float)number);
        //    }
        //    return allMoney;
        //}
        public static float todayMoneyNumber(String moive)
        {
            float money = SqlMoive.getPrice(moive);
            int number = SqlMoive.singleMoiveNumber(moive);
            return money * (float)number;
        }
        public static float allSaleMoneyNumber(String moive)
        {
            float money=0;
            DataSet ds = SqlBook.getAfterDiscountByMoive(moive);
            foreach (DataRow row1 in ds.Tables[0].Rows)
            {
                money+=(float)Convert.ToDouble(row1[0].ToString().Trim());
            }
            return money;
        }
        public static float allSaleMoneyNumber(int screen)
        {
            float money = 0;
            DataSet ds = SqlBook.getAfterDiscountByMoive(screen);
            foreach (DataRow row1 in ds.Tables[0].Rows)
            {
                money += (float)Convert.ToDouble(row1[0].ToString().Trim());
            }
            return money;
        }
        public static float todaySaleMoneyNumber(String moive)
        {
            float money = 0;
            DataSet ds = SqlBook.getTodayAfterDiscountByMoive(moive);
            foreach (DataRow row1 in ds.Tables[0].Rows)
            {
                money += (float)Convert.ToDouble(row1[0].ToString().Trim());
            }
            return money;
        }
        public static float todaySaleMoneyNumber(int screen)
        {
            float money = 0;
            DataSet ds = SqlBook.getTodayAfterDiscountByMoive(screen);
            foreach (DataRow row1 in ds.Tables[0].Rows)
            {
                money += (float)Convert.ToDouble(row1[0].ToString().Trim());
            }
            return money;
        }
        public static DataSet getAllMoiveType()
        {
            String sql = "select Type from MoiveType";
            return
                DbHelperSQL.Query(sql, "type");
        }
    }
}
