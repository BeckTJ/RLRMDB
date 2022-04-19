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
        private int materialNumber;
        private string drumId;
        private string vendor1;
        private string vendorBatch;
        private string distilOperator;
        private long inspectionLotNumber;
        private int sapBatch;
        private string containerNumber;
        private int netWeight;
        private string sampleNumber;

        public RawMaterialForm()
        {
            InitializeComponent();
        }
        public void RawMaterialDrum(int number)
        {
            RawMaterialLog rm = new RawMaterialLog();
            Materials materials = new Materials();
            Vendors vendorName = new Vendors();
            VendorBatch batch = new VendorBatch();
            ListViewItem vendors = new ListViewItem();
           
            dateBox.Text = DateTime.Today.ToString("d");
            operatorBox.Text = "DT"; // Set from user (not created yet)
            distilOperator = "DT";

            

                vendors.Text = item;
                vendorBox.Items.Add(vendors);
            
            vendor1 = vendorBox.SelectedText;



            vendorBatchBox.Items.Add(batch.getVendorBatch(number,vendor1));
            containerNumber = containerNumberBox.Text;
            sampleNumber = sampleNumberBox.Text;

            if (lotNumberBox.Text == "")
                lotNumberBox.Text = "";
            else
                inspectionLotNumber = long.Parse(lotNumberBox.Text);
            if (SAPBatchBox2.Text == "")
                SAPBatchBox2.Text = "";
            else
                sapBatch = int.Parse(SAPBatchBox2.Text);
            if (netWeightBox.Text == "")
                netWeightBox.Text = "";
            else
                netWeight = int.Parse(netWeightBox.Text);
            
            rm.getDrumId(number, vendor1);
            var result = rm.drumId;
            DrumId.Text = result;


        }
        public void setRawMaterialDrum()
        {
            RawMaterialLog material = new RawMaterialLog();
            material.setRawMaterialDrum(materialNumber, vendor1, vendorBatch, distilOperator, inspectionLotNumber, sapBatch,
                containerNumber, netWeight, sampleNumber);
            material.addDrumToDatabase();


        }
        private void Submit_Click(object sender, EventArgs e)
        {
            setRawMaterialDrum();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
         
            this.Close();
            MainForm.ActiveForm.Show();
        }

        
    }
}
