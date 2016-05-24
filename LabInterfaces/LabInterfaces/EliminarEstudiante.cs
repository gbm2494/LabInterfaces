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
using System.Diagnostics;


namespace LabInterfaces
{
    public partial class EliminarEstudiante : Form
    {
        Estudiante estudiante;

        public EliminarEstudiante()
        {
            InitializeComponent();
            estudiante = new Estudiante();
        }

        private void EliminarEstudiante_Load(object sender, EventArgs e)
        {
            llenarCombobox(cmbNombre);
        }

        private void llenarCombobox(ComboBox combobox)
        {
            SqlDataReader datos = estudiante.obtenerListaEstudiantes();

            if (datos != null)
            {
                combobox.Items.Add("Seleccione");
                while (datos.Read())
                {
                    combobox.Items.Add(datos.GetValue(0));
                }
            }
            else
            {
                combobox.Items.Clear();
                combobox.Items.Add("Seleccione");
            }

            combobox.SelectedIndex = 0;

        }
    }
}
