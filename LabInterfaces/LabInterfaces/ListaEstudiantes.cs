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

        /*Constructor de la clase*/
        public ListaEstudiantes()
        {
            InitializeComponent();
            estudiante = new Estudiante();
        }

        /*Método load, este método es llamado cuando se carga la pantalla */
        private void ListaEstudiantes_Load(object sender, EventArgs e)
        {
            //Llena el combobox de nombres de estudiante
            llenarCombobox(cmbNombre);
            //Llena el datagridview de estudiantes con todas las tuplas de estudiante de la interfaz
            llenarTabla(dgvEstudiantes, null, null);
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
         Recibe: Un control datagridview que va a cargar con datos, una string en caso que se quiera filtrar por el valor
         * del combobox y un string de filtroGeneral en caso que se quiera filtrar por el texto introducido por el usuario
         Modifica: carga los datos en el datagridview
         Retorna: ningún valor*/
        private void llenarTabla(DataGridView dataGridView, string filtroCombobox, string filtroGeneral)
        {
          
            /*obtiene un dataTable con todos los estudiantes que se encuentran en la base de datos que cumplan las condiciones
             de los dos filtros que el método recibe por parámetro*/
            DataTable tabla = estudiante.obtenerEstudiantes(filtroCombobox, filtroGeneral);

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


        /*Método para el evento de cambio de la selección del combobox nombre
         Cuando en la interfaz el usuario cambia la selección este metodo se activa*/
        private void cmbNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Se carga el datagridview con estudiantes que tengan como nombre el seleccionado en el combobox nombre
            llenarTabla(dgvEstudiantes, cmbNombre.Text, null);
        }

        /*Método que se activa al dar click en el link de agregar estudiante*/
        private void lkAgregar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Crea la interfaz AgregarEstudiante y la muestra, desaparece la interfaz actual
            AgregarEstudiante agregar = new AgregarEstudiante();
            agregar.Show();
            this.Hide();
        }

        /*Método que se activa al dar click en el botón Buscar*/
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Llena el datagridview con los estudiantes que contengan en alguno de sus campos el texto del textbox txtBuscar
            llenarTabla(dgvEstudiantes, null, txtBuscar.Text);

        }

    }
}
