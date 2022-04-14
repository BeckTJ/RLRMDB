using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RLRMWF
{
    public partial class searchOptions : Form
    {
        MaterialForm materialForm = new MaterialForm();

        public searchOptions()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            var input = Convert.ToInt32(Input.Text);
            materialForm.GetMaterial(input);

            this.Close();
            MainFrom.ActiveForm.Close();
            materialForm.Show();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
