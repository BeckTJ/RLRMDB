using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ULRMDB
{
    public partial class loginForm : Form
    {
        material m = new material();
        string user;
        string pw;
        connection con = new connection();

        public loginForm()
        {
            InitializeComponent();             
        }

        private void login_Click(object sender, EventArgs e)
        {
            con.openConnection();
            m.Show();
            this.Visible = false;
        }
    }
}
