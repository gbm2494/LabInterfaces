using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabInterfaces
{
    public partial class Login : Form
    {
        Estudiante estudiante;

        public Login()
        {
            InitializeComponent();
            estudiante = new Estudiante();
        }

        /*Método que se activa al dar click en el botón aceptar y valida el usuario que quiere iniciar sesión*/
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Si los dos campos tienen datos se valida si no muestra un mensaje de error
            if (txtUsuario.Text != "" && txtPassword.Text != "")
            {
                //si el usuario si existe pasa a la pantalla de Agregar usuario
                if (estudiante.login(txtUsuario.Text, txtPassword.Text) == true)
                {
                    //Crea la interfaz AgregarEstudiante y la muestra, desaparece la interfaz actual
                    AgregarEstudiante agregar = new AgregarEstudiante();
                    agregar.Show();
                    this.Hide();
                }
                    //Si el usuario no existe muestra un mensaje de error
                else
                {
                    MessageBox.Show("Usuario y/o incorrecto, por favor intente de nuevo", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Por favor introduzca todos los datos para el inicio de sesión", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

    }
}
