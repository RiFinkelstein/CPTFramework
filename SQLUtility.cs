using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CPUFramework
{
    public class SQLUtility
    {
        public static string ConnectionString = "";

        public static DataTable GetDataTable(string sqlstatement)
        {
            DataTable dt = new();
            SqlConnection conn = new();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            //DisplayMessage("Connection Status", conn.State.ToString());
            var cmd = new SqlCommand();
            cmd.CommandText = sqlstatement;
            cmd.Connection = conn;
            var dr = cmd.ExecuteReader();
            dt.Load(dr);

            SetAllColumnsAllowNull(dt);

            return dt;
            //- take sql statemnt and return a data table 
        }

        public static void ExecuteSQL(string sqlstatemnt)
        {
            GetDataTable(sqlstatemnt);
        }

        private static void SetAllColumnsAllowNull(DataTable dt)
        {
            foreach(DataColumn c in dt.Columns)
            {
                c.AllowDBNull = true;
            }
        }

        public static void DebugPringDataTable(DataTable dt)
        {
           foreach(DataRow r in dt.Rows) { 
                foreach(DataColumn c in dt.Columns)
                {
                    Debug.Print(c.ColumnName + " = "+ r[c.ColumnName].ToString());
                }
            }
        }

        public static int GetFirstColumnFirstRowValue(string sql)
        {
            int n = 0;
            DataTable dt = GetDataTable(sql);
            if (dt.Rows.Count > 0 && dt.Columns.Count > 0)
            {
                if (dt.Rows[0][0] != DBNull.Value)
                {
                    int.TryParse(dt.Rows[0][0].ToString(), out n);
                }
            }
            return n;
        }

        public static DateTime GetFirstColumnFirstRowValueDate(string sql) { 
            DateTime result = DateTime.Now; 
            DataTable dt = GetDataTable(sql); 
            if (dt.Rows.Count > 0 && dt.Columns.Count > 0) 
            { 
                if (dt.Rows[0][0] != DBNull.Value) 
                { 
                    DateTime parsedDate; 
                    if (DateTime.TryParse(dt.Rows[0][0].ToString(), out parsedDate)) 
                    { 
                        result = parsedDate; 
                    } 
                } 
            }
            return result; 
        }
    }
}
