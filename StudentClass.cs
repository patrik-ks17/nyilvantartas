using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace nyilvantartas
{
    internal class StudentClass
    {
        private DBConnect connect = new DBConnect();

        public bool insertStudent(
            string lname, string fname, string phone, 
            DateTime bdate, int gender, string address,
            byte[] avatar)
        {
            MySqlCommand command = new MySqlCommand(
                "INSERT INTO `tablestudent` (`id`, `lastName`, `firstName`, `phone`, `birthDate`, `gender`, `adress`, `avatar`) VALUES (NULL, @lname, @fname, @phone, @date, @gen, @adr, @ava);"
                , connect.getConnection);

            //  Paraméterek átadása az SQL részére
            command.Parameters.Add("@lname", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@fname", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@date", MySqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gen", MySqlDbType.Int32).Value = gender;
            command.Parameters.Add("@adr", MySqlDbType.Text).Value = address;
            command.Parameters.Add("@ava", MySqlDbType.Blob).Value = avatar;

            connect.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnection();
                return true;
            }
            else
            {
                connect.closeConnection();
                return false;
            }
        }
        public bool deleteStudent(int id)
        {
            MySqlCommand command = new MySqlCommand(
                "DELETE FROM `tablestudent` WHERE `id` = @id",
                connect.getConnection);

            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            connect.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnection();
                return true;
            }
            else
            {
                connect.closeConnection();
                return false;
            }

        }
        public DataTable getStudentList()
        {
            MySqlCommand command =
                new MySqlCommand("SELECT * FROM `tablestudent`",
                connect.getConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            DataTable table = new DataTable();
            adapter.Fill(table);    
            return table;
        }

        public string exeCount(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connect.getConnection);
            connect.openConnection();
            string count = command.ExecuteScalar().ToString();
            connect.closeConnection();
            return count;
        }

        public string totalStudents()
        {
            return exeCount("SELECT COUNT(*) FROM `tablestudent`");
        }

        public string maleStudents()
        {
            return exeCount("SELECT COUNT(*) FROM `tablestudent` WHERE `gender` = 1");
        }

        public string femaleStudents()
        {
            return exeCount("SELECT COUNT(*) FROM `tablestudent` WHERE `gender` = 2");
        }
        public bool updateStudent(int id,
             string lname, string fname, string phone,
             DateTime bdate, int gender, string address,
             byte[] avatar)
        {
            MySqlCommand command = new MySqlCommand(
                "UPDATE `tablestudent` SET `lastName` = @lname, `firstName` = @fname, `phone` = @phone, `birthDate` = @date, `gender` = @gen, `adress` = @adr, `avatar` = @ava WHERE `id` = @id;"
                , connect.getConnection);

            //  Paraméterek átadása az SQL részére
            command.Parameters.Add("@id",
                MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@lname", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@fname", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@date", MySqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gen", MySqlDbType.Int32).Value = gender;
            command.Parameters.Add("@adr", MySqlDbType.Text).Value = address;
            command.Parameters.Add("@ava", MySqlDbType.Blob).Value = avatar;

            connect.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnection();
                return true;
            }
            else
            {
                connect.closeConnection();
                return false;
            }
        }
    }
}
