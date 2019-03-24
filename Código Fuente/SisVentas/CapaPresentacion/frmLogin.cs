using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;




namespace CapaPresentacion
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            LblHora.Text = DateTime.Now.ToString();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);



        private void timer1_Tick(object sender, EventArgs e)
        {
            LblHora.Text = DateTime.Now.ToString();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            DataTable Datos = CapaNegocio.NTrabajador.Login(this.TxtUsuario.Text,this.TxtPassword.Text);
            //Evaluar si existe el Usuario
            if (Datos.Rows.Count==0)
            {
                MessageBox.Show("NO Tiene Acceso al Sistema","Sistema de Ventas DP",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                frmMenu frm  = new frmMenu();
                //frm.Idtrabajador = Datos.Rows[0][0].ToString();
                //frm.Apellidos = Datos.Rows[0][1].ToString();
                //frm.Nombre = Datos.Rows[0][2].ToString();
                //frm.Acceso = Datos.Rows[0][3].ToString();

                frm.Show();
                this.Hide();

            }
        }

        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                //Es una propiedad que indica que se ha controlado o no el evento para que los eventos propios de .net
                //sigan en ejecucion o no
                e.Handled = true;
                DataTable Datos = CapaNegocio.NTrabajador.Login(this.TxtUsuario.Text, this.TxtPassword.Text);
                //Evaluar si existe el Usuario
                if (Datos.Rows.Count == 0)
                {
                    MessageBox.Show("NO Tiene Acceso al Sistema", "Sistema de Ventas DP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    frmPrincipal frm = new frmPrincipal();
                    frm.Idtrabajador = Datos.Rows[0][0].ToString();
                    frm.Apellidos = Datos.Rows[0][1].ToString();
                    frm.Nombre = Datos.Rows[0][2].ToString();
                    frm.Acceso = Datos.Rows[0][3].ToString();

                    frm.Show();
                    this.Hide();

                }

            }
        }

        private void TxtUsuario_Enter(object sender, EventArgs e)
        {
            if (TxtUsuario.Text == "USUARIO")
            {
                TxtUsuario.Text = "";
                TxtUsuario.ForeColor = Color.LightGray;
            }

        }

        private void TxtUsuario_Leave(object sender, EventArgs e)
        {
            if(TxtUsuario.Text == "")
            {
                TxtUsuario.Text = "USUARIO";
                TxtUsuario.ForeColor = Color.Gray;

            }
        }

        private void TxtPassword_Enter(object sender, EventArgs e)
        {
            if (TxtPassword.Text == "CONTRASEÑA")
            {
                TxtPassword.Text = "";
                TxtPassword.ForeColor = Color.LightGray;
                TxtPassword.UseSystemPasswordChar = true;

            }
        }

        private void TxtPassword_Leave(object sender, EventArgs e)
        {
            if (TxtPassword.Text == "")
            {
                TxtPassword.Text = "CONTRASEÑA";
                TxtPassword.ForeColor = Color.Gray;
                TxtPassword.UseSystemPasswordChar = false;

            }
        }

        private void frmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
