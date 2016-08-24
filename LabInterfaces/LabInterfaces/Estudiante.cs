using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LabInterfaces
{
    class Estudiante
    {
        AccesoBaseDatos bd;

        /*Constructor de la clase estudiante*/
        public Estudiante()
        {
            //Se inicializa el objeto que realiza la conexión con la base de datos
            bd = new AccesoBaseDatos();
        }

        /*Método para agregar un nuevo estudiante a la base de datos
         Recibe: Los datos del nuevo estudiante
         Modifica: inserta en la base de datos el nuevo estudiante
         Retorna: el tipo de error que generó la inserción o cero si la inserción fue exitosa*/
        public int agregarEstudiante(string cedula, string carne, string nombre, string ape1, string ape2, string email, char genero, string fechaNac, string direccion, string telefono, int estado)
        {
            String insertar = "INSERT INTO Estudiante (Cedula, carne, nombre, apellido1, apellido2, email, sexo, fechaNac, direccion, telefono, estado ) VALUES (" + cedula + ",'" + carne + "', '" + nombre + "', '" + ape1 + "','" + ape2 + "', '" + email + "','" + genero + "','" + fechaNac + "','" + direccion + "','" + telefono + "', '" + 1 + "'  )";
            return bd.actualizarDatos(insertar);
        }

        /*Método para obtener los nombres de estudiantes de la base de datos
         Recibe: Nada
         Modifica: Realiza la selección de los nombres de estudiantes y lo carga en un dataReader
         Retorna: el dataReader con los datos*/
        public SqlDataReader obtenerListaNombresEstudiantes()
        {
            SqlDataReader datos = null;
            try
            {
                datos = bd.ejecutarConsulta("Select distinct nombre from Estudiante");
            }
            catch (SqlException ex)
            {
            
            }

            return datos;  
        }


        /*Método para obtener los estudiantes de la base de datos
         Recibe: dos tipos de filtros por los cuales se pueden filtrar las tuplas
         Modifica: Realiza la selección de los estudiantes y los carga en un dataTable
         Retorna: el dataTable con los datos*/
        public DataTable obtenerEstudiantes(string filtroNombre, string filtroGeneral)
        {
            DataTable tabla = null;
            try
            {
                //Si los filtros son nulos se cargan todos los estudiantes de la base de datos
                if (filtroGeneral == null && filtroNombre == null)
                { 
                    tabla = bd.ejecutarConsultaTabla("Select * from estudiante"); 
                }
                //Si el filtro de nombre no es nulo carga los estudiantes cuyo nombre sea el que tiene el filtro
                else if(filtroNombre != null)
                {
                    tabla = bd.ejecutarConsultaTabla("Select * from estudiante where nombre ='" + filtroNombre+ "'"); 
                }
                //Si el filtro general no es nulo cargan los estudiantes con atributos que contengan ese filtro como parte del atributo (like)
                else if(filtroGeneral != null)
                {
                    tabla = bd.ejecutarConsultaTabla("Select * from estudiante where nombre like '%" + filtroGeneral + "%' OR apellido1 like '%" + filtroGeneral + "%' OR apellido2 like '%" + filtroGeneral + "%' OR cedula like '%" + filtroGeneral + "%' OR carne like '%" + filtroGeneral + "%'"); 
                }
                //Si ninguno de los filtros es nulo carga los estudiantes que coincidan con ambos filtros
                else if(filtroGeneral != null && filtroNombre != null)
                {
                    tabla = bd.ejecutarConsultaTabla("Select * from estudiante where nombre ='" + filtroNombre + "' &&  nombre like '%" + filtroGeneral + "%' OR apellido1 like '%" + filtroGeneral + "%' OR apellido2 like '%" + filtroGeneral + "%' OR cedula like '%" + filtroGeneral + "%' OR carne like '%" + filtroGeneral + "%'"); 
                }
                
            }
            catch (SqlException ex)
            {
                
            }

            return tabla;
        }

        /*Método para eliminar un estudiante mediante el procedimiento almacenado
         Recibe: El nombre de los estudiantes o estudiante a eliminar
         Modifica: Llama al método que elimina el estudiante mediante el nombre
         Retorna: el tipo de error que generó el eliminar o cero si el eliminar fue exitoso*/
        public int eliminarEstudiante(string nombre)
        {
            return bd.eliminarEstudiante(nombre);
        }

        /*Método para agregar un usuario mediante el procedimiento almacenado
         Recibe: 
         Modifica: 
         Retorna: */
        public bool agregarUsuario(string nombre, string password, string cedula)
        {

            int resultado = bd.agregarUsuario(nombre, password, cedula);

            //si el procedimiento almacenado devuelve un 1 es porque agregó un nuevo usuario, por lo que se convierte en true
            if (resultado == 1)
            {
                return true;
            }

             //Cualquier valor distinto a 1 que el procedimiento almacenado significa que no agregó un nuevo usuario
            else
            {
                return false;
            }
        }

    }
}
