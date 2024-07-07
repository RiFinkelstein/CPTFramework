using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
            return dt;
            //- take sql statemnt and return a data table 
        }
    }
}
//note