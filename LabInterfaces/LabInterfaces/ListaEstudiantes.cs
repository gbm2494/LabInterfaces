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
    public partial class ListaEstudiantes : Form
    {
        Estudiante estudiante;

        public ListaEstudiantes()
        {
            InitializeComponent();
            estudiante = new Estudiante();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AgregarEstudiante agregar = new AgregarEstudiante();
            agregar.Show();
            this.Hide();
        }

        private void ListaEstudiantes_Load(object sender, EventArgs e)
        {
            llenarCombobox(cmbNombre);
            llenarTabla(dgvEstudiantes, null, null);
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

        private void llenarTabla(DataGridView dataGridView, string filtroCombobox, string filtroGeneral)
        {
            DataTable tabla = estudiante.obtenerEstudiantes(filtroCombobox, filtroGeneral);

            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = tabla;
            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            dataGridView.DataSource = bindingSource;
            for (int i = 0; i < dgvEstudiantes.ColumnCount; i++)
            {
                dataGridView.Columns[i].Width = 100;
            }
        }


        private void cmbNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarTabla(dgvEstudiantes, cmbNombre.Text, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            llenarTabla(dgvEstudiantes, null, txtBuscar.Text);
        }
    }
}
