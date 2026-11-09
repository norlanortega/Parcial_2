using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using entidade;
using Negocio;

namespace Presentacion
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            string strUsuario = txtusuarios.Text.Trim();
            string strPassword = txtPassword.Text.Trim();
            E_usuarios usuario = N_usuarios.Login(strUsuario, strPassword);

            if (usuario != null && usuario.id > 0)
            {
                MessageBox.Show("¡Bienvenido, " + usuario.dsUsuario + "!", "Login Exitoso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                new frmPrincipal().Show();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error de Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
