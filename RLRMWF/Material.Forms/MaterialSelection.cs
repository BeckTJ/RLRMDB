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
        MaterialForm materialForm;
        RawMaterialForm rawMaterialForm;
        List<Materials> sel;
        string formChoice;
        int result;

        public searchOptions(string formToOpen)
        {
            InitializeComponent();
            formChoice = formToOpen;

            Selection selection = new Selection();
            sel = new List<Materials>();
            sel = selection.getMaterialName();
            foreach (var s in sel)
            {
                materialSelectionBox.Items.Add(s.name);
            } 
        }
        private void materialSelectionBox_selectedIndexChange(object sendor, EventArgs e)
        {
            if (materialSelectionBox != null)
            {
                result = Convert.ToInt32(sel[materialSelectionBox.SelectedIndex].number);
            }
        }
        public void getMaterialForm(int input)
        {
            materialForm = new MaterialForm();
            materialForm.GetMaterial(input);
            this.Close();
            materialForm.Show();
        }
        public void getRawMaterialForm(int input)
        {
            rawMaterialForm = new RawMaterialForm(input);
            this.Close();
            rawMaterialForm.Show();
            rawMaterialForm.InputRawMaterialInformation();
        }
        private void SearchButton_Click(object sender, EventArgs e)
        {
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
            //MainForm.ActiveForm.Close();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
