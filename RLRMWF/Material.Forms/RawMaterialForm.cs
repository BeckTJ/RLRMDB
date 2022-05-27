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
    public partial class RawMaterialForm : MainForm
    {
        RMLog rawMaterialLog;
        List<int> rmNumber = new List<int>();

        public RawMaterialForm(int number)
        {
            InitializeComponent();
            rawMaterialLog = new RMLog(number);
        }
        internal void InputRawMaterialInformation()
        {
            var vendorNames = rawMaterialLog.getVendorList();
            vendorBatchBox.Items.Clear();

            foreach (var vendorName in vendorNames)
            {
                vendorBox.Items.Add(vendorName.vendorName);
                rmNumber.Add((int)vendorName.materialNumber);
            }
        }
        private void VendorBox_selectedIndexChange(object sender, EventArgs e)
        {
            if (vendorBox.SelectedItem != null)
            {
                var selectedVendor = vendorBox.SelectedItem.ToString();
                var selectedIndex = vendorBox.SelectedIndex;

                rawMaterialLog.vendor = selectedVendor;


                if (rawMaterialLog.getIsMpps() == true)
                {
                    lotNumberBox.ReadOnly = false;
                    SAPBatchBox2.ReadOnly = false;
                    containerNumberBox.ReadOnly = false;
                    netWeightBox.ReadOnly = false;
                    vendorBatchBox.ResetText();
                    vendorBatchBox.Enabled = false;
                }
                else
                {
                    lotNumberBox.ReadOnly = true;
                    SAPBatchBox2.ReadOnly = true;
                    containerNumberBox.ReadOnly = true;
                    netWeightBox.ReadOnly = true;
                    vendorBatchBox.Enabled = true;
                }

                materialNumberBox.Text = rmNumber[selectedIndex].ToString();

                var vendorBatchList = rawMaterialLog.getVendorBatchList(rmNumber[selectedIndex],selectedVendor);

                foreach (var batch in vendorBatchList)
                {
                    vendorBatchBox.Items.Add(batch);
                }
            }
        }
        private void batchBox_selectedIndexChange(object sendor, EventArgs e)
        {
            
            if (vendorBatchBox.SelectedItem != null)
            {
                var vendorBatch = vendorBatchBox.SelectedItem.ToString();
                rawMaterialLog.vendorBatch = vendorBatch;
            }
        }
        private void Submit_Click(object sender, EventArgs e)
        {
            rawMaterialLog.checkSampleSubmit(sampleNumberBox.Text);

            if (rawMaterialLog.getIsMpps() == false)
            {
                DrumId.Text =  rawMaterialLog.SubmitRawMaterial();
                approvalBox.Text = rawMaterialLog.approvalDate.ToShortDateString();
                experationBox.Text = rawMaterialLog.experationDate.ToShortDateString();
                qcSignOffBox.Text = rawMaterialLog.qcOperator;

            }
            else
            {
                rawMaterialLog.inspectionLot = long.Parse(lotNumberBox.Text);
                rawMaterialLog.sapBatch = int.Parse(SAPBatchBox2.Text);
                rawMaterialLog.ctn = containerNumberBox.Text;
                rawMaterialLog.netWeight = int.Parse(netWeightBox.Text);
                approvalBox.Text = rawMaterialLog.approvalDate.ToShortDateString();
                experationBox.Text = rawMaterialLog.experationDate.ToShortDateString();
                qcSignOffBox.Text = rawMaterialLog.qcOperator;

                rawMaterialLog.SubmitMPPSMaterial();
            }
            ;
            operatorBox.Text = rawMaterialLog.getUser();
            dateBox.Text = DateTime.Today.ToShortDateString();
            Submit.Enabled = false;   
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            this.Close();
            mainForm.Show();
        }

        
    }
}
