using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkMate.Feature
{
    internal class DB
    {
        public static DataTable SelectSingle(string query)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(App.IConfDB))
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand(query.ToString(), con))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        cmd.CommandTimeout = 180;

                        using (DataSet ds = new DataSet())
                        {
                            sda.Fill(ds);
                            dt = ds.Tables[0];
                        }
                    }
                }
            }
            return dt;
        }

        public static void Execute(string query)
        {
            if (string.IsNullOrEmpty(query)) return;

            try
            {
                using (MySqlConnection con = new MySqlConnection(App.IConfDB))
                {
                    con.Open();
                    new MySqlCommand(query, con).ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                App.ILog.Error(ex.ToString());
            }
        }

        public static int ExecuteWithLastIdx(string query)
        {
            int idx = -1;
            if (string.IsNullOrEmpty(query)) return idx;

            try
            {
                using (MySqlConnection con = new MySqlConnection(App.IConfDB))
                {
                    con.Open();
                    new MySqlCommand(query, con).ExecuteNonQuery();

                    // 새로 삽입된 행의 idx 값을 가져오기 위한 쿼리
                    string selectQuery = "SELECT LAST_INSERT_ID();";

                    // LAST_INSERT_ID 실행
                    using (var cmd = new MySqlCommand(selectQuery, con))
                    {
                        // 삽입된 행의 idx 값을 가져오기
                        idx = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                App.ILog.Error(ex.ToString());
            }
            return idx;
        }
    }
}