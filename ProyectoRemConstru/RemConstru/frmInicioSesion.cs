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
    public partial class frmInicioSesion : Form
    {
        public frmInicioSesion()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            DataTable dtsRetorno = new DataTable();
            dtsRetorno = BCP.usuarioBCP.loginUsuario(txtUsuario.Text, txtContrasenia.Text);
            if (dtsRetorno.Rows.Count > 0)
            {
                frmMenu frmInicio = new frmMenu();
                frmInicio.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario y/o Contraseña Incorrecta");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
