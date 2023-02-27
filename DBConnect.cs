using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nyilvantartas
{
    internal class DBConnect
    {
        private MySqlConnection connect = new MySqlConnection(
            "datasource=localhost;port=3306;username=root;" +
            "password=Passw123;database=petistudent");

        public MySqlConnection getConnection
        {
            get
            {
                return connect;
            }
        }

        public void openConnection()
        {
            if (connect.State != System.Data.ConnectionState.Open)
                connect.Open();
        }

        public void closeConnection()
        {
            if (connect.State != System.Data.ConnectionState.Closed)
                connect.Close();    
        }
    }


}
