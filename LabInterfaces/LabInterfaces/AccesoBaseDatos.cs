using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Configuration;
// Namespace de acceso a base de datos
using System.Data;
using System.Data.SqlClient;

/*Cambiar el namespace para que funcione!!*/
namespace LabInterfaces
{
    class AccesoBaseDatos
    {
        /*En Initial Catalog se agrega la base de datos propia. Intregated Security es para utilizar Windows Authentication*/
        String conexion = "Data Source=10.1.4.55; Initial Catalog=gaudyblanco; Integrated Security=SSPI";
        
        /**
         * Constructor de la clase
         */
        public AccesoBaseDatos(){
        }

        /**
         * Permite ejecutar una consulta SQL, los datos son devueltos en un SqlDataReader
         * Recibe: La consulta sql a ejecutar
         * Modifica: Obtiene las tuplas que corresponden a la consulta recibida
         * Retorna: El datareader con los resultados de la ejecución de la consulta
         */
        public SqlDataReader ejecutarConsulta(String consulta)
        {
            //Prepara una nueva conexión a la bd y la abre
            SqlConnection sqlConnection = new SqlConnection(conexion);
            sqlConnection.Open();

            SqlDataReader datos = null;
            SqlCommand comando = null;

            try
            {
                //Ejecuta la consulta sql recibida por parámetro y la carga en un datareader
                comando = new SqlCommand(consulta, sqlConnection);
                datos = comando.ExecuteReader();
            }
            catch (SqlException ex)
            {

            }
            return datos;
        }

        /**
         * Permite ejecutar una consulta SQL, los datos son devueltos en un DataTable
         * * Recibe: La consulta sql a ejecutar
         * Modifica: Obtiene las tuplas que corresponden a la consulta recibida
         * Retorna: El datatable con los resultados de la ejecución de la consulta
         */
        public DataTable ejecutarConsultaTabla(String consulta)
        {
            //Prepara una nueva conexión a la bd y la abre
            SqlConnection sqlConnection = new SqlConnection(conexion);
            sqlConnection.Open();

            SqlCommand comando = new SqlCommand(consulta, sqlConnection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataTable table = new DataTable();

            dataAdapter.Fill(table);
			
			return table;
        }

        /*Método para ejecutar un insert, update o delete 
         Recibe: la sentencia sql a ejecutar
         Modifica: realiza el cambio en la base de datos de acuerdo al tipo de sentencia sql
         Retorna: el tipo de error que generó la consulta o cero si la ejecución fue exitosa*/
        public int actualizarDatos(String consulta)
        {
            int error = 0;

            //Prepara una nueva conexión a la bd y la abre
            SqlConnection sqlConnection = new SqlConnection(conexion);
            sqlConnection.Open();

            SqlCommand cons = new SqlCommand(consulta, sqlConnection);

            try
            {
                //Ejecuta la consulta sql recibida por parámetro
                cons.ExecuteNonQuery();
            }
            catch(SqlException e)
            {
                error = e.Number;
                Debug.WriteLine("Error: " + error);
            }

            finally
            {
                sqlConnection.Close();
            }

            return error;
        }

        /*Método para llamar al procedimiento almacenado de eliminarEstudiante 
         Recibe: el nombre del o los estudiantes que se va a eliminar
         Modifica: Elimina las tuplas que coincidan con el nombre recibido por parámetro
         Retorna: el tipo de error que generó la consulta o cero si la ejecución fue exitosa*/
        public int eliminarEstudiante(string nombre)
        {
            int error = 0;
            using (SqlConnection con = new SqlConnection(conexion))
            {
                /*El sqlCommand recibe como primer parámetro el nombre del procedimiento almacenado, 
                 * de segundo parámetro recibe el sqlConnection
                */
                using (SqlCommand cmd = new SqlCommand("eliminarEstudiante", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        //Se preparan los parámetros que recibe el procedimiento almacenado
                        cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre;

                        con.Open();

                        //Se ejecuta el procedimiento almacenado
                        cmd.ExecuteNonQuery();
                        return error;
                    }
                    catch (SqlException ex)
                    {

                        error = ex.Number;
                        return error;
                    }
                }
            }

        }

    }
}
