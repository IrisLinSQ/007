using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Xml;

namespace SqlHelper
{
    public class DbHelperSQL
    {
        static string connectionString = null;
            //"Data Source=.;Initial Catalog=CinemaData;Integrated Security=True";
        
         public  DbHelperSQL()
        {
            
        }
         public static void constr()
         {
             XmlDocument xmlDoc = new XmlDocument();
             xmlDoc.Load(@"resource\conf.xml");
             XmlElement xmlRoot = xmlDoc.DocumentElement;
             connectionString = xmlRoot.ChildNodes.Item(0).InnerText;
         }


         //执行查询语句，返回DataSet
         public static DataSet Query(string SQLString,string dataSetName)
         {
             constr();
             SqlConnection connection = null;
             try
             {
                 using (connection = new SqlConnection(connectionString))
                 {
                     DataSet ds = new DataSet();
                     connection.Open();
                     SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                     command.Fill(ds, dataSetName);
                     return ds;
                 }
             }
             catch (System.Exception)
             {
                 throw;
                 //return null;
             }
             finally
             {
                 connection.Close();
             }

         }

         //执行查询语句，返回SqlDataReader
         public static SqlDataReader ExecuteReader(string strSQL)
         {
             constr();
             SqlConnection connection = new SqlConnection(connectionString);
             SqlCommand cmd = new SqlCommand(strSQL, connection);
             try
             {
                 connection.Open();
                 SqlDataReader myReader = cmd.ExecuteReader();
                 return myReader;
             }
             catch (System.Data.SqlClient.SqlException e)
             {
                 connection.Close();
                 throw new Exception(e.Message);
                 
             }

         }

        //执行操作并返回受影响行数
         public static int ExecuteSql(string SQLString)
         {
             constr();
             using (SqlConnection connection = new SqlConnection(connectionString))
             {
                 using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                 {
                     try
                     {
                         connection.Open();
                         int rows = cmd.ExecuteNonQuery();
                         return rows;
                     }
                     catch (System.Data.SqlClient.SqlException)
                     {

                         connection.Close();
                         throw;
                         //    throw new Exception(E.Message);
                     }
                 }
             }
         }
        //查询取得单个值
        public static object GetSingle(string SQLString)
        {
            constr();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw new Exception(e.Message);
                    }
                }
            }
        }
    }
}
