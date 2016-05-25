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
            llenarTabla(dgvEstudiantes);
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

        private void llenarTabla(DataGridView dataGridView)
        {
            DataTable tabla = estudiante.obtenerEstudiantes(null, null);

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = tabla;
            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            dataGridView.DataSource = bindingSource;
            for (int i = 0; i < dgvEstudiantes.ColumnCount; i++)
            {
                dataGridView.Columns[i].Width = 100;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*Llamar al procedimiento almacenado, cambiar el procedimiento para que use nombre y no cedula*/
            estudiante.eliminarEstudiante(cmbNombre.Text);
            llenarCombobox(cmbNombre);
            llenarTabla(dgvEstudiantes);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AgregarEstudiante agregar = new AgregarEstudiante();
            agregar.Show();
            this.Hide();
        }

    }
}
