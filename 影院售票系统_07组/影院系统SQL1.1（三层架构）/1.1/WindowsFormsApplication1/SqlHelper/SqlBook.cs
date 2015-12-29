using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ClassUse;

namespace SqlHelper
{
    public class SqlBook
    {
        public static void buyTicket(Book book)
        {
            string SQLStr = "Insert into Book_record Values('" 
                + book.getMoiveName() + "','" + book.getTime() + "'," + book.getScreenName() + ",'" 
                + book.getSeat() + "','" + book.getConsumerType() + "'," + book.getDiscount() + "," 
                + book.getAfterDiscount() + ",'" + book.getSaleTime() + "')";
            DbHelperSQL.ExecuteSql(SQLStr);
        }
        public static void refund(Book book)
        {
            string SQLStr = "delete from Book_record where MName='" 
                + book.getMoiveName() + "' and SName=" + book.getScreenName() + " and time='" 
                + book.getTime() + "' and seat='" + book.getSeat() + "'";
            DbHelperSQL.ExecuteSql(SQLStr);
        }
        public static DataSet getSeatByKye(int sName,String sMane,String time)
        {
            string sql = "select seat from Book_record where SName='" 
                + sName + "' and MName='" + sMane + "'and time='" + time + "'";
            return
                DbHelperSQL.Query(sql, "seat");
        }
       
    }
}
