using entidade;
using Negocio;
using System;
using System.Windows.Forms;

namespace Presentacion
{
    /// <summary>
    /// Formulario de login (Vista del MVP). No tiene lógica de negocio.
    /// </summary>
    public partial class frmLogin : Form, ILoginVista
    {
        private readonly Loginpresen _presentador;

        public event EventHandler IniciarSesion;

        public frmLogin()
        {
            InitializeComponent();
            txtPassword.UseSystemPasswordChar = true;  // oculta la contraseña
            AcceptButton = btnIniciarSesion;           // Enter = iniciar sesión
            _presentador = new Loginpresen(this, new N_usuarios());
        }

        public string Usuario => txtusuarios.Text.Trim();

        public string Password => txtPassword.Text;

        /// <summary>Avisa al presentador que se presionó el botón.</summary>
        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            IniciarSesion?.Invoke(this, EventArgs.Empty);
        }

        public void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void AbrirPrincipal(E_usuarios usuario)
        {
            frmPrincipal principal = new frmPrincipal(usuario);

            principal.FormClosed += (s, args) =>
            {
                if (principal.CerroSesion)
                {
                    txtusuarios.Clear();
                    txtPassword.Clear();
                    Show();
                }
                else
                {
                    Close();
                }
            };

            Hide();
            principal.Show();
        }
    }
}