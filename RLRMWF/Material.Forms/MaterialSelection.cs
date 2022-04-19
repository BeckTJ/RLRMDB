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
        RawMaterialForm rawMaterialForm = new RawMaterialForm();
        string formChoice;

        public searchOptions(string formToOpen)
        {
            InitializeComponent();
            formChoice = formToOpen;
        }
        public void getMaterialForm(int input)
        {
            materialForm.GetMaterial(input);
            this.Close();
            materialForm.Show();
        }
        public void getRawMaterialForm(int input)
        {
            rawMaterialForm.RawMaterialDrum(input);
            this.Close();
            rawMaterialForm.Show();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {

            var result = Convert.ToInt32(Input.Text);

            switch (formChoice)
            {
                case "material":
                    getMaterialForm(result);
                    break;
                case "sample":
                    getRawMaterialForm(result);
                    break;
                default:throw new Exception();
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
