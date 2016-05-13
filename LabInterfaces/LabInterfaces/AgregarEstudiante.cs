using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace LabInterfaces
{
    /*Notas 
     Explicar el tabIndex, name de los controles*/

    public partial class AgregarEstudiante : Form
    {
        Estudiante estudiante;

        public AgregarEstudiante()
        {
            InitializeComponent();
            estudiante = new Estudiante();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ListaEstudiantes lista = new ListaEstudiantes();
            lista.Show();
            this.Hide();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            char genero = ' ';

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

            int resultado = estudiante.agregarEstudiante(txtCedula.Text, txtCarne.Text,  txtNombre.Text,  txtApe1.Text, txtApe2.Text, txtEmail.Text, genero, dtpFecha.Value.ToString("yyyy-MM-dd"), txtDireccion.Text ,txtTelefono.Text , 1);

            if(resultado == 0)
            {
                MessageBox.Show("El estudiante ha sido agregado exitosamente", "Resultados", MessageBoxButtons.OK, MessageBoxIcon.None);
                txtCarne.Clear();
                txtCedula.Clear();
                txtNombre.Clear();
                txtApe1.Clear();
                txtApe2.Clear();
                txtEmail.Clear();
                txtDireccion.Clear();
                txtTelefono.Clear();

            }
            else if (resultado == 2627)
            {
                MessageBox.Show("Ya existe un estudiante asociado a este numero de cedula en el sistema", "Resultados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
