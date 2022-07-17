using System;
using System.Collections.Generic;
using RLRMWF;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RLRMWF
{
    public partial class MaterialForm : MainForm
    {
        Materials material = new Materials();
        VendorBatch batch = new VendorBatch();

        public MaterialForm()
        {
            InitializeComponent();
        }
        public void GetMaterial(int input)
        {

            var result = material.getMaterial(input);
            number.Text = result.number.ToString();
            name.Text = result.nameAbreviation;
            permitNumber.Text = result.permitNumber;
            description.Text = result.name;

            if (result.isRawMaterial == true)
            {
                isRawMaterial.Checked = true;
                RawMaterialTab.Enabled = false;
                code.Text = result.rawMaterialCode;
            }
            else
                code.Text = result.productCode;
            ui.Text = result.unitOfIssue;
            idStart.Text = result.sequenceIdStart.ToString();
            idEnd.Text = result.sequenceIdEnd.ToString();
            idCurrent.Text = result.currentId.ToString();
            if (result.processOrderRequired == true)
                requiresPO.Checked = true;
            if (result.batchManaged == true)
                batchManaged.Checked = true;
            if (result.carbonDrumRequired == true)
            {
                cdRequired.Checked = true;

                if (result.carbonDrumWeightAllowed != null)
                    changeOut.Text = result.carbonDrumWeightAllowed.ToString() + result.unitOfIssue;  //Not displaying weight

                else if (result.carbonDrumDaysAllowed != null)
                    changeOut.Text = result.carbonDrumDaysAllowed.ToString() + " Days";

            }
            if (result.isRawMaterial == false)
                GetRawMaterialTab(result.nameId);
            else
                RawMaterialTab.Hide();


        }
        public void GetRawMaterialTab(int id)
        {
            var result = material.getRawMaterialFromDatabase(id);
            List<TextBox> box = new List<TextBox>();

            box.Add(number1);
            box.Add(vendor1);
            box.Add(start1);
            box.Add(end1);
            box.Add(current1);
            box.Add(mc1);

            box.Add(number2);
            box.Add(vendor2);
            box.Add(start2);
            box.Add(end2);
            box.Add(current2);
            box.Add(mc2);

            box.Add(number3);
            box.Add(vendor3);
            box.Add(start3);
            box.Add(end3);
            box.Add(current3);
            box.Add(mc3);

            box.Add(number4);
            box.Add(vendor4);
            box.Add(start4);
            box.Add(end4);
            box.Add(current4);
            box.Add(mc4);

            box.Add(number5);
            box.Add(vendor5);
            box.Add(start5);
            box.Add(end5);
            box.Add(current5);
            box.Add(mc5);

            var i = 0;
            foreach (var output in result)
            {
                box[i].Text = output.number.ToString();
                box[i].Visible = true;
                i++;
                box[i].Text = output.vendor.ToString();
                box[i].Visible = true;
                i++;
                box[i].Text = output.sequenceIdStart.ToString();
                box[i].Visible = true;
                i++;
                box[i].Text = output.sequenceIdEnd.ToString();
                box[i].Visible = true;
                i++;
                box[i].Text = output.currentId.ToString();
                box[i].Visible = true;
                i++;
                box[i].Text = output.rawMaterialCode.ToString();
                box[i].Visible = true;
                i++;
            }
            GetVendorBatchTab(result);

        }

        public void GetVendorBatchTab(List<Materials> input) // rework vendor tab. dif output should desplay for dif material depending on number of vendors
        {
            vendorBatch batch = new vendorBatch();
            Label[] vendorLabel = { VendorLabel1, VendorLabel2, VendorLabel3 };
            ListView[] batchlist = { BatchList1, BatchList2, BatchList3 };
            List<vendorBatch> batches = new List<vendorBatch>();
            int i = 0;
            if(input != null) {
                foreach (var output in input)
                {
                    vendorLabel[i].Text = output.vendor;
                    vendorLabel[i].Visible = true;
                    batchlist[i].View = View.Details;
                    batchlist[i].Columns.Add("Batch Number", -2);
                    batchlist[i].Columns.Add("Quantity", -2);

                    batches = batch.getVendorBatch(output.number, output.vendor);
                    foreach (var result in batches)
                    {
                        ListViewItem qty = new ListViewItem();
                        qty.Text = result.vendorBatchNumber;
                        qty.SubItems.Add(result.quantity.ToString());
                        batchlist[i].Items.Add(qty);
                        batchlist[i].Visible = true;
                        if (batchlist.Count() < 0)
                        {
                            batchlist[i].Visible = true;
                        }
                    }
                    i++;
                }
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            
            searchOptions search = new searchOptions("material");
            this.Close();
            MainForm.ActiveForm.Show();
            search.Show();
        }
    }
}
