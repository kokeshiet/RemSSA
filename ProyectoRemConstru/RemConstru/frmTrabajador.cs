using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemConstru
{
    public partial class frmTrabajador : Form
    {
        public frmTrabajador()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            txtDV.Text = BCP.Util.calculaDV(Convert.ToInt32(txtRut.Text));
        }
    }
}
