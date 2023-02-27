using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nyilvantartas
{
    internal static class StudentHelper
    {
        private static StudentClass student = new StudentClass();
        public static void showTable(DataGridView grid)
        {
            grid.DataSource = student.getStudentList();
            DataGridViewImageColumn avatarColumn = new DataGridViewImageColumn();
            avatarColumn = (DataGridViewImageColumn)grid.Columns[7];
            avatarColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;

            grid.Columns[0].Width = 20;
            grid.Columns[5].Width = 20;
            grid.Columns[7].Width = 64;
        }

        public static void uploadAvatar(PictureBox box)
        {
            //  Avatar képének feltöltése
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Képkiválasztás (*.jpg;*.png;*.gif;)|*.jpg;*.png;*.gif";

            if (ofd.ShowDialog() == DialogResult.OK)
                box.Image = Image.FromFile(ofd.FileName);
            box.SizeMode = PictureBoxSizeMode.Zoom;
            // TODO: Képméret ellenőrzés
        }

    }
}
