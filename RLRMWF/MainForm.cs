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
    public partial class MainFrom : Form
    {
        
        public MainFrom()
        {
            InitializeComponent();
            mainMenuStrip();
        }

        public void mainMenuStrip()
        {
            searchToolStripMenuItem.Click += new EventHandler(menuStrip1_ItemClicked);
            updateToolStripMenuItem.Click += new EventHandler(menuStrip1_ItemClicked);
            newMaterialToolStripMenuItem.Click += new EventHandler(menuStrip1_ItemClicked);
            vendorBatchToolStripMenuItem.Click += new EventHandler(menuStrip1_ItemClicked);
            vendorUpdateToolStripMenuItem1.Click += new EventHandler(menuStrip1_ItemClicked);
            newToolStripMenuItem1.Click += new EventHandler(menuStrip1_ItemClicked);
            exitToolStripMenuItem.Click += new EventHandler(menuStrip1_ItemClicked);
        }

        private void menuStrip1_ItemClicked(object sender, EventArgs e)
        {
            MaterialForm newMaterial = new MaterialForm();
            searchOptions search = new searchOptions();
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem == searchToolStripMenuItem || menuItem == updateToolStripMenuItem || menuItem == newMaterialToolStripMenuItem)
            {
                search.Show();
            }    
            else if(menuItem == exitToolStripMenuItem)
            {
                Application.Exit();
            }
        }
    }
}
