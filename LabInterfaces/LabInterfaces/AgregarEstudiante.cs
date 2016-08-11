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
    /*Notas 
     Explicar el tabIndex, name de los controles*/

    public partial class AgregarEstudiante : Form
    {
        Estudiante estudiante;

        /*Constructor de la clase*/
        public AgregarEstudiante()
        {
            InitializeComponent();
            estudiante = new Estudiante();
        }

        /*Método que se activa al dar click en el botón guardar para guardar un nuevo estudiante en la base de datod*/
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //obtengo el genero seleccionado en la pantalla
            char genero = ' ';

            //El control radiobutton tiene la propiedad Checked en true si este se encuentra seleccionado
            if(rbFem.Checked)
            {
                genero = 'F';
            }
            else if(rbMasc.Checked)
            {
                genero = 'M';
            }
            else if (rbOtro.Checked)
            {
                genero = 'O';
            }

            /*Mediante el objeto de tipo Estudiante podemos agregar un nuevo estudiante con el método agregarEstudiante,
              el cual recibe por parámetro todos los valores de la tabla Estudiante*/
            int resultado = estudiante.agregarEstudiante(txtCedula.Text, txtCarne.Text,  txtNombre.Text,  txtApe1.Text, txtApe2.Text, txtEmail.Text, genero, dtpFecha.Value.ToString("yyyy-MM-dd"), txtDireccion.Text ,txtTelefono.Text , 1);

            //Si la inserción devuelve un 0 la inserción fue exitosa, por lo que se muestra un mensaje de éxito
            if(resultado == 0)
            {
                int resultado1 = estudiante.agregarUsuario(txtUsuario.Text, txtPassword.Text);
                if (resultado1 == 1)
                {
                    MessageBox.Show("El estudiante ha sido agregado exitosamente", "Resultados", MessageBoxButtons.OK, MessageBoxIcon.None);
                    //Se limpian las cajas de texto para permitir al usuario añadir un nuevo estudiante cuando lo desee
                    txtCarne.Clear();
                    txtCedula.Clear();
                    txtNombre.Clear();
                    txtApe1.Clear();
                    txtApe2.Clear();
                    txtEmail.Clear();
                    txtDireccion.Clear();
                    txtTelefono.Clear();
                }
                else
                {
                    MessageBox.Show("Ya existe un estudiante asociado a este usuario en el sistema", "Resultados", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            //si la inserción devuelve un código de error se puede validar con un mensaje de error personalizado
            else if (resultado == 2627)
            {
                MessageBox.Show("Ya existe un estudiante asociado a este numero de cedula en el sistema", "Resultados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*Método que se activa cuando se da click en el link de Lista de estudiantes*/
        private void lkLista_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Creación de la interfaz ListaEstudiantes y se muestra, desaparece la interfaz actual
            ListaEstudiantes lista = new ListaEstudiantes();
            lista.Show();
            this.Hide();
        }

        /*Método que se activa cuando se da click en el link de eliminar Estudiante*/
        private void lkEliminar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Creación de la interfaz EliminarEstudiante y se muestra, desaparece la interfaz actual
            EliminarEstudiante eliminar = new EliminarEstudiante();
            eliminar.Show();
            this.Hide();
        }
    }
}
