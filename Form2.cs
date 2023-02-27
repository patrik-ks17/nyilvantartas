using nyilvantartas.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nyilvantartas
{
    public partial class FormRegistration : Form
    {
        //  Összekapcsoljuk a StudentClass és FormRegistration osztályokat
        private StudentClass student = new StudentClass();
        public FormRegistration()
        {
            InitializeComponent();
        }

        private void FormRegistration_Load(object sender, EventArgs e)
        {
            StudentHelper.showTable(gridStudents);
            refreshFields();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            StudentHelper.uploadAvatar(pbAvatar);
        }

        private bool verifyStudent()
        {
            if ((textLastName.Text == "") || (textFirstName.Text == "") ||
                (textPhone.Text == "") || (textAddress.Text == ""))
                return false;
            else
                return true;

            //  TODO: Komolyabb ellenőrzés, tájékoztatás
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string lname = textLastName.Text;
            string fname = textFirstName.Text;
            string phone = textPhone.Text;
            DateTime bdate = dateBirth.Value;
            string address = textAddress.Text;
            int gender = radioMale.Checked ? 1 : 2;

            //  Avatar kép konvertálása Image -> byte[]

             MemoryStream ms = new MemoryStream();
            if (pbAvatar.Image == null)
                pbAvatar.Image = Properties.Resources.ures;

            pbAvatar.Image.Save(ms, pbAvatar.Image.RawFormat);    
            byte[] avatar = ms.ToArray();

            
            //  Életkor ellenőrzése
            int bornYear = dateBirth.Value.Year;
            int currentYear = DateTime.Now.Year;
            int age = currentYear - bornYear;
            if ((age < 10) || (age > 100))
            {
                MessageBox.Show(
                    "Az életkor 10 és 100 év között legyen!",
                    "Hiba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else if (verifyStudent())
            {
                try
                {
                    if (student.insertStudent(lname, fname, phone, bdate, gender,
                        address, avatar))
                    {
                        MessageBox.Show(
                             "A tanuló hozzáadása megtörtént",
                            "Sikeres művelet",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Hiba",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(
                    "Minden mezőt kötelező kitölteni!",
                    "Hiba",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            StudentHelper.showTable(gridStudents);
        }
    
        private void btnClear_Click(object sender, EventArgs e)
        {
            textFirstName.Clear();
            textLastName.Clear();
            textPhone.Clear();
            textAddress.Clear();

            dateBirth.Value = DateTime.Now;
            radioMale.Checked = true;
        }

        private void gridStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            refreshFields();
        }
        private void refreshFields()
        {
            textLastName.Text =
                gridStudents.CurrentRow.Cells[1].Value.ToString();
            textFirstName.Text =
                gridStudents.CurrentRow.Cells[2].Value.ToString();
            textPhone.Text =
                gridStudents.CurrentRow.Cells[3].Value.ToString();
            textAddress.Text =
                gridStudents.CurrentRow.Cells[6].Value.ToString();

            dateBirth.Value =
                (DateTime)gridStudents.CurrentRow.Cells[4].Value;

            if ((int)gridStudents.CurrentRow.Cells[5].Value == 1)
                radioMale.Checked = true;
            else
                radioFemale.Checked = true;

            MemoryStream ms = new MemoryStream((byte[])gridStudents.CurrentRow.Cells[7].Value);

            pbAvatar.Image = Image.FromStream(ms);
            pbAvatar.SizeMode = PictureBoxSizeMode.Zoom;
        }
    }
}
