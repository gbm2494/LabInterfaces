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

        public Estudiante()
        {
            bd = new AccesoBaseDatos();
        }

        public int agregarEstudiante(string cedula, string carne, string nombre, string ape1, string ape2, string email, char genero, string fechaNac, string direccion, string telefono, int estado)
        {
            String insertar = "INSERT INTO Estudiante (Cedula, carne, nombre, apellido1, apellido2, email, sexo, fechaNac, direccion, telefono, estado ) VALUES (" + cedula + ",'" + carne + "', '" + nombre + "', '" + ape1 + "','" + ape2 + "', '" + email + "','" + genero + "','" + fechaNac + "','" + direccion + "','" + telefono + "', '" + 1 + "'  )";
            return  bd.insertarDatos(insertar);
        }

        public SqlDataReader obtenerListaEstudiantes()
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


        public DataTable obtenerEstudiantes(string filtroNombre, string filtroGeneral)
        {
            DataTable tabla = null;
            try
            {
                if (filtroGeneral == null && filtroNombre == null)
                { 
                    tabla = bd.ejecutarConsultaTabla("Select * from estudiante"); 
                }
                else if(filtroNombre != null)
                {
                    tabla = bd.ejecutarConsultaTabla("Select * from estudiante where nombre ='" + filtroNombre+ "'"); 
                }
                else if(filtroGeneral != null)
                {
                    tabla = bd.ejecutarConsultaTabla("Select * from estudiante where nombre like '%" + filtroGeneral + "%' OR apellido1 like '%" + filtroGeneral + "%' OR apellido2 like '%" + filtroGeneral + "%' OR cedula like '%" + filtroGeneral + "%' OR carne like '%" + filtroGeneral + "%'"); 
                }
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

        public int eliminarEstudiante(string nombre)
        {
            return bd.eliminarEstudiante(nombre);
        }

    }
}
