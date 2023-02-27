using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nyilvantartas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //  Panelek elrejtése
            panelStudent.Visible = false;
            panelCourse.Visible = false;
            panelScore.Visible = false;
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        #region SlideMenu
        private void hidePanels()
        {
            //  Panelek elrejtése
            if (panelStudent.Visible == true)
                     panelStudent.Visible = false;
            if (panelCourse.Visible == true)
                    panelCourse.Visible = false;
            if (panelScore.Visible == true)
                     panelScore.Visible = false;
        }

        private void showPanel(Panel panel)
        {
            if (panel.Visible == false)
            {
                hidePanels();
                panel.Visible = true;
            }
            else
                panel.Visible = false;  
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            showPanel(panelStudent);
        }

        private void btnCourse_Click(object sender, EventArgs e)
        {
            showPanel(panelCourse);
        }

        private void btnScore_Click(object sender, EventArgs e)
        {
            showPanel(panelScore);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (MessageBox.Show(
                "Valóban ki szeretnél lépni?",
                "Kilépés",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) ==
                DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        #endregion SlideMenu

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = childForm;

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;    
            panelFormSub.Controls.Add(childForm);   
            panelFormSub.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void btnStudentCreate_Click(object sender, EventArgs e)
        {
            openChildForm(new FormRegistration());
        }

        private void btnStudentEdit_Click(object sender, EventArgs e)
        {
            openChildForm(new FormStudentManagement());
        }
    }
}

