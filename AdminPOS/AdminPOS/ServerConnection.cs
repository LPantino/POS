using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdminPOS.Properties;
using MySql.Data.MySqlClient;

namespace AdminPOS
{
    class ServerConnection
    {
        public MySqlConnection con = null;
        public string Server = Settings.Default["Server"].ToString();
        public string Port = Settings.Default["Port"].ToString();
        public string Uid = Settings.Default["User"].ToString();
        public string Pwd = Settings.Default["Password"].ToString();

        public void Connection()
        {
            string strConnection = "Server=" + Server + ";Port=" + Port + ";Database=postech;Uid=" + Uid + ";Pwd=" + Pwd + "";
            con = new MySqlConnection(strConnection);

        }

        public void OpenConnection()
        {
            try
            {
                con.Open();

            }
            catch (System.TimeoutException)
            {

            }
        }

        public void CloseConnection()
        {
            try
            {
                con.Close();

            }
            catch (System.TimeoutException)
            {

            }
        }
    }
}
