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

        /*Constructor de la clase*/
        public EliminarEstudiante()
        {
            InitializeComponent();
            estudiante = new Estudiante();
        }

        /*Método load, este método es llamado cuando se carga la pantalla */
        private void EliminarEstudiante_Load(object sender, EventArgs e)
        {
            //Llena el combobox de nombres de estudiante
            llenarCombobox(cmbNombre);
            //Llena el datagridview de estudiantes con todas las tuplas de estudiante de la interfaz
            llenarTabla(dgvEstudiantes);
        }

        /*Método para llenar un combobox con datos específicos
         Recibe: Un objeto combobox que va a llenar con una consulta específica
         Modifica: Llena el combobox que recibe por parámetro con el nombre de todos los estudiantes que se encuentran en la bd
         Retorna: Ningún valor*/
        private void llenarCombobox(ComboBox combobox)
        {
            //Se obtiene un dataReader con todos los nombres de los estudiantes de la base de datos
            SqlDataReader datos = estudiante.obtenerListaNombresEstudiantes();

            /*Si existen datos en la base de datos se carga como primer elemento del combobox un dato "Seleccione"
            y luego se cargan todos los datos de la base de datos*/
            if (datos != null)
            {
                combobox.Items.Add("Seleccione");
                while (datos.Read())
                {
                    combobox.Items.Add(datos.GetValue(0));
                }
            }
            /*Si no hay tuplas en la base de datos se limpia el combobox y se carga unicamente el valor "Seleccione"*/
            else
            {
                combobox.Items.Clear();
                combobox.Items.Add("Seleccione");
            }

            //Se pone por defecto la primera entrada del combobox seleccionada
            combobox.SelectedIndex = 0;

        }

        /*Método para llenar un datagridview con datos específicos
         Recibe: Un control datagridview que va a cargar con datos
         Modifica: carga los datos en el datagridview
         Retorna: ningún valor*/
        private void llenarTabla(DataGridView dataGridView)
        {
            /*obtiene un dataTable con todos los estudiantes que se encuentran en la base de datos (null, null) es para
            vengan todas las tuplas sin ningún filtro*/
            DataTable tabla = estudiante.obtenerEstudiantes(null, null);

            //Se inicializa el source para cargar el datagridview y se le asigna el dataTable obtenido
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = tabla;
            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            dataGridView.DataSource = bindingSource;

            //Ciclo para darle un ancho a cada columna del datagridview proporcionado
            for (int i = 0; i < dgvEstudiantes.ColumnCount; i++)
            {
                dataGridView.Columns[i].Width = 100;
            }
        }

        /*Método que se activa al dar click en el botón Buscar*/
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            /*Llama al procedimiento almacenado mediante el método de la clase estudiante, elimina por nombre*/
            estudiante.eliminarEstudiante(cmbNombre.Text);
            //Vuelvo a llenar el combobox y el datagridview con los datos actualizados
            llenarCombobox(cmbNombre);
            llenarTabla(dgvEstudiantes);
        }

        /*Método que se activa al dar click en el link de agregar estudiante*/
        private void lkAgregar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Crea la interfaz AgregarEstudiante y la muestra, desaparece la interfaz actual
            AgregarEstudiante agregar = new AgregarEstudiante();
            agregar.Show();
            this.Hide();
        }

    }
}
