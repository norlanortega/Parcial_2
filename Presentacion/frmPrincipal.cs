using entidade;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion
{
    /// <summary>
    /// Formulario principal (Vista del MVP): menú a la izquierda,
    /// hora arriba y usuario en sesión abajo.
    /// </summary>
    public partial class frmPrincipal : Form, IPrincipalVista
    {
        private readonly PrincipalPresentador _presentador;

        private Panel pnlMenu, pnlHeader, pnlContenido;
        private Label lblLogo, lblTitulo, lblHora;
        private Button btnInicio, btnUsuarios, btnReportes, btnConfiguracion, btnCerrarSesion;
        private StatusStrip ssEstado;
        private ToolStripStatusLabel lblUsuarioSesion;
        private System.Windows.Forms.Timer tmrHora;

        private static readonly Color ColorMenu = Color.FromArgb(33, 37, 54);
        private static readonly Color ColorMenuHover = Color.FromArgb(52, 58, 82);

        /// <summary>true si presionó "Cerrar sesión"; false si cerró con la X.</summary>
        public bool CerroSesion { get; private set; }

        public frmPrincipal(E_usuarios usuario)
        {
            InitializeComponent();
            ConstruirDiseno();

            _presentador = new PrincipalPresentador(this, usuario);
            _presentador.Iniciar();
            tmrHora.Start();
        }

        public void MostrarUsuario(string texto)
        {
            lblUsuarioSesion.Text = texto;
        }

        public void MostrarHora(string hora)
        {
            lblHora.Text = hora;
        }

        /// <summary>Crea el menú lateral, la cabecera, el contenido y la barra inferior.</summary>
        private void ConstruirDiseno()
        {
            Text = "Sistema - Menú principal";
            Size = new Size(1000, 600);
            MinimumSize = new Size(800, 500);
            StartPosition = FormStartPosition.CenterScreen;

            // Contenido (centro)
            pnlContenido = new Panel { Dock = DockStyle.Fill, BackColor = Color.WhiteSmoke };
            lblTitulo = new Label
            {
                Text = "Inicio",
                AutoSize = true,
                Location = new Point(30, 30),
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = ColorMenu
            };
            pnlContenido.Controls.Add(lblTitulo);

            // Cabecera con la hora (arriba)
            pnlHeader = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.White };
            lblHora = new Label
            {
                Dock = DockStyle.Right,
                Width = 320,
                TextAlign = ContentAlignment.MiddleRight,
                Padding = new Padding(0, 0, 20, 0),
                Font = new Font("Segoe UI", 12),
                ForeColor = ColorMenu
            };
            pnlHeader.Controls.Add(lblHora);

            // Menú lateral (izquierda)
            pnlMenu = new Panel { Dock = DockStyle.Left, Width = 200, BackColor = ColorMenu };
            lblLogo = new Label
            {
                Text = "MI SISTEMA",
                Dock = DockStyle.Top,
                Height = 70,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White
            };

            btnInicio = CrearBotonMenu("Inicio");
            btnUsuarios = CrearBotonMenu("Usuarios");
            btnReportes = CrearBotonMenu("Reportes");
            btnConfiguracion = CrearBotonMenu("Configuración");
            btnCerrarSesion = CrearBotonMenu("Cerrar sesión");
            btnCerrarSesion.Dock = DockStyle.Bottom;

            // Con Dock=Top el último agregado queda arriba, por eso van al revés
            Button[] opciones = { btnInicio, btnUsuarios, btnReportes, btnConfiguracion };
            for (int i = opciones.Length - 1; i >= 0; i--)
            {
                opciones[i].Click += OpcionMenu_Click;
                pnlMenu.Controls.Add(opciones[i]);
            }
            pnlMenu.Controls.Add(btnCerrarSesion);
            pnlMenu.Controls.Add(lblLogo);
            btnCerrarSesion.Click += BtnCerrarSesion_Click;

            // Barra inferior con el usuario en sesión
            ssEstado = new StatusStrip();
            lblUsuarioSesion = new ToolStripStatusLabel();
            ssEstado.Items.Add(lblUsuarioSesion);

            Controls.Add(pnlContenido);
            Controls.Add(pnlHeader);
            Controls.Add(pnlMenu);
            Controls.Add(ssEstado);

            // Reloj: se actualiza cada segundo
            tmrHora = new System.Windows.Forms.Timer { Interval = 1000 };
            tmrHora.Tick += (s, e) => _presentador.ActualizarHora();
            FormClosed += (s, e) => tmrHora.Stop();
        }

        /// <summary>Crea un botón con el estilo del menú.</summary>
        private Button CrearBotonMenu(string texto)
        {
            Button btn = new Button
            {
                Text = texto,
                Dock = DockStyle.Top,
                Height = 48,
                FlatStyle = FlatStyle.Flat,
                BackColor = ColorMenu,
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                Font = new Font("Segoe UI", 10),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = ColorMenuHover;
            return btn;
        }

        /// <summary>Muestra en el título la opción elegida.</summary>
        private void OpcionMenu_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = ((Button)sender).Text;
        }

        /// <summary>Cierra la sesión y vuelve al login.</summary>
        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            CerroSesion = true;
            Close();
        }
    }
}