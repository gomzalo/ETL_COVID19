// ************************************************* EJEMPLO PARA CARGA DE UN SOLO UNICO ARCHIVO  ********************************************
//Importamos las librerias necesarias
using System;
using System.Data;
using Microsoft.SqlServer.Dts.Runtime;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

//Apartado para utilizar las variables "Globales" creadas anteriormente
//Este tipo de variables permiten al proyecto tener acceso y permisos a las carpetas de nuestra computadora
//y facilitar posibles cambios.
//Se crean otras locales con la intención de no usar todo el nombre en donde se quiera implementar
string Delimitador = Dts.Variables["User::Delimitador"].Value.ToString();
string CarpetaOrigen = Dts.Variables["User::CarpetaOrigen"].Value.ToString();
string CarpetaErrores = Dts.Variables["User::CarpetaErrores"].Value.ToString();
string CarpetaExitos = Dts.Variables["User::CarpetaExitos"].Value.ToString();


//Creamos la variable de tipo SQL que nos permita una nueva conexión
SqlConnection my_adonet_connection = new SqlConnection();
// Indicamos el servidor y la base de datos a la que nos conectaremos
my_adonet_connection = (SqlConnection)(Dts.Connections["Servidor.OrigenData"].AcquireConnection(Dts.Transaction) as SqlConnection);

//Variable para verificar que el linea tenga contenido
int counter = 0;
string linea;

//Creamos la variable de tipo StreamReader para realizar la lectura del contenido del archivo
//StreamReader archivo_lectura = new StreamReader("C:\Usuarios\Escritorio\Caso.csv");
StreamReader archivo_lectura = new StreamReader(archivoactual);


while ((linea = archivo_lectura.ReadLine()) != null)
{
    if (counter > 0)
    {
        //Variable de tipo vector para almacenar el contenido cada columna del archivo
        string[] campos = linea.Split(Delimitador.ToCharArray()[0]);

        //Creación del codigo SQL para ejecutar el Insert
        string query = "INSERT INTO tempventa (linea,archivo,cod_cliente,nombre_cliente,tipo_cliente," +
        "direccion_cliente,correo_cliente,cod_articulo,color,descripcion,departamento," +
        "cod_sucursal,nombre_sucursal,direccion_sucursal,region_sucursal,departamento_sucursal," +
        "zona_sucursal,cod_vendedor,nombre_vendedor,sucursal_vendedor,fecha,unidades,preciounitario)" +
        "Values('" + counter + "', '" + "ventas.txt" + "', '" + campos[0] + "', '" + campos[1] + "', '" + campos[2] + "', '" + campos[3] + "', '" + campos[4] + "', '" + campos[5]
        + "', '" + campos[6] + "', '" + campos[7] + "', '" + campos[8] + "', '" + campos[9] + "', '" + campos[10]
        + "', '" + campos[11] + "', '" + campos[12] + "', '" + campos[13] + "', '" + campos[14] + "', '" + campos[15]
        + "', '" + campos[16] + "', '" + campos[17] + "', '" + campos[18] + "', '" + campos[19] + "', '" + campos[20] + "')";
                                            
        //Variable SQL para ejecutar el comando del query creado anteriormente (querycreado,conexión la base de datos)
        SqlCommand comandosql = new SqlCommand(query, my_adonet_connection);
        comandosql.ExecuteNonQuery();


        string query2 = "INSERT INTO tempfecha (linea,archivo,fecha) Values('" + counter + "', '" + "ventas.txt" + "', '" + campos[18] + "')";
        SqlCommand comandosql2 = new SqlCommand(query2, my_adonet_connection);
        comandosql2.ExecuteNonQuery();

    }
    counter++;
}
//Cerramos el archivo utilizado para evitar problemas de permisos
archivo_lectura.Close();
MessageBox.Show("Archivo ventas.txt fue analizado correctamente");



// ************************************************* EJEMPLO PARA CARGA DE UNO O MAS ARCHIVOS  ********************************************
//Se utiliza para validar que no hayan errores que con nuestras variables que interrumpan la ejecucion
//Importamos las librerias necesarias
using System;
using System.Data;
using Microsoft.SqlServer.Dts.Runtime;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

try
{
    //Apartado para utilizar las variables "Globales" creadas anteriormente
    //Este tipo de variables permiten al proyecto tener acceso y permisos a las carpetas de nuestra computadora
    //y facilitar posibles cambiso.
    //Se crean otras locales con la intenci�n de no usar todo el nombre en donde se quiera implementar
    string Delimitador = Dts.Variables["User::Delimitador"].Value.ToString();
    string CarpetaOrigen = Dts.Variables["User::CarpetaOrigen"].Value.ToString();
    string CarpetaErrores = Dts.Variables["User::CarpetaErrores"].Value.ToString();
    string CarpetaExitos = Dts.Variables["User::CarpetaExitos"].Value.ToString();

    //Variable de tipo array que almacenar� todos los archivos con extension .txt que se encuentran
    //en la carpeta Origen establecida.
    string[] ArchivosDisponibles = Directory.GetFiles(CarpetaOrigen, "*" + ".txt");

    //Se verifica que existan archivos
    if (ArchivosDisponibles.Length > 0)
    {
        //Recorremos el vector de archivos
        foreach (string archivoactual in ArchivosDisponibles)
        {
            //Se utiliza para validar que no existan errores que interrumpan la ejecuci�n
            //al intentar conectarnos a la DBOrigen
            try
            {
                //Creamos la variable de tipo SQL que nos permita una nueva conexi�n
                SqlConnection my_adonet_connection = new SqlConnection();
                // Indicamos el servidor y la base de datos a la que nos conectaremos
                my_adonet_connection = (SqlConnection)(Dts.Connections["CARLOST.origendata"].AcquireConnection(Dts.Transaction) as SqlConnection);

                //Variable para verificar que el linea tenga contenido
                int counter = 0;
                string linea;

                //Creamos la variable de tipo StreamReader para realizar la lectura del contenido del archivo
                StreamReader archivo_lectura = new StreamReader(archivoactual);

                //Se aplica un split para eliminar de la ruta del archivo el simbolo \ y as� obtener
                //el nombre del archivo para compararlo
                string[] nombrearchivo = archivoactual.Split("\\".ToCharArray());
                switch (nombrearchivo[3])
                {
                    case "articulo.txt":
                        while ((linea = archivo_lectura.ReadLine()) != null)
                        {
                            if (counter > 0)
                            {
                                string[] campos = linea.Split(Delimitador.ToCharArray()[0]);
                                string query = "INSERT INTO articulo (linea,archivo,cod_articulo,color,descripcion,departamento) Values('" + counter + "', '" + "articulo.txt" + "', '" + campos[0] + "', '" + campos[1] + "', '" + campos[2] + "', '" + campos[3] + "')";
                                SqlCommand comandosql = new SqlCommand(query, my_adonet_connection);
                                comandosql.ExecuteNonQuery();
                            }
                            counter++;
                        }
                        archivo_lectura.Close();
                        MessageBox.Show("Archivo articulo.txt fue analizado correctamente");
                        break;
                    case "cliente.txt":
                        while ((linea = archivo_lectura.ReadLine()) != null)
                        {
                            if (counter > 0)
                            {
                                string[] campos = linea.Split(Delimitador.ToCharArray()[0]);
                                string query = "INSERT INTO cliente (linea,archivo,cod_cliente,nombre,tipocliente,direccion,email) Values('" + counter + "', '" + "cliente.txt" + "', '" + campos[0] + "', '" + campos[1] + "', '" + campos[2] + "', '" + campos[3] + "', '" + campos[4] + "')";
                                SqlCommand comandosql = new SqlCommand(query, my_adonet_connection);
                                comandosql.ExecuteNonQuery();
                            }
                            counter++;
                        }
                        archivo_lectura.Close();
                        MessageBox.Show("Archivo cliente.txt fue analizado correctamente");
                        break;
                    case "sucursal.txt":
                        while ((linea = archivo_lectura.ReadLine()) != null)
                        {
                            if (counter > 0)
                            {
                                string[] campos = linea.Split(Delimitador.ToCharArray()[0]);
                                string query = "INSERT INTO sucursal (linea,archivo,cod_sucursal,nombre,direccion) Values('" + counter + "', '" + "sucursal" + "', '" + campos[0] + "', '" + campos[1] + "', '" + campos[2] + "')";
                                SqlCommand comandosql = new SqlCommand(query, my_adonet_connection);
                                comandosql.ExecuteNonQuery();

                                string query2 = "INSERT INTO region (linea,archivo,region,departamento,zona) Values('" + counter + "', '" + "sucursal.txt" + "', '" + campos[3] + "', '" + campos[4] + "', '" + campos[5] + "')";
                                SqlCommand comandosql2 = new SqlCommand(query2, my_adonet_connection);
                                comandosql2.ExecuteNonQuery();
                            }
                            counter++;
                        }
                        archivo_lectura.Close();
                        MessageBox.Show("Archivo sucursal.txt fue analizado correctamente");
                        break;
                    case "vendedor.txt":
                        while ((linea = archivo_lectura.ReadLine()) != null)
                        {
                            if (counter > 0)
                            {
                                string[] campos = linea.Split(Delimitador.ToCharArray()[0]);
                                string query = "INSERT INTO vendedor (linea,archivo,cod_vendedor,nombre,sucursal) Values('" + counter + "', '" + "vendedor.txt" + "', '" + campos[0] + "', '" + campos[1] + "', '" + campos[2] + "')";
                                SqlCommand comandosql = new SqlCommand(query, my_adonet_connection);
                                comandosql.ExecuteNonQuery();
                            }
                            counter++;
                        }
                        archivo_lectura.Close();
                        MessageBox.Show("Archivo vendedor.txt fue analizado correctamente");
                        break;
                    case "ventas.txt":
                        while ((linea = archivo_lectura.ReadLine()) != null)
                        {
                            if (counter > 0)
                            {
                                //Variable de tipo vector para almacenar el contenido cada columna del archivo
                                string[] campos = linea.Split(Delimitador.ToCharArray()[0]);

                                //Creacion del codigo SQL para ejecutar el Insert
                                string query = "INSERT INTO venta (linea,archivo,cod_cliente,nombre_cliente,tipo_cliente," +
                                "direccion_cliente,correo_cliente,cod_articulo,color,descripcion,departamento," +
                                "cod_sucursal,nombre_sucursal,direccion_sucursal,region_sucursal,departamento_sucursal," +
                                "zona_sucursal,cod_vendedor,nombre_vendedor,sucursal_vendedor,fecha,unidades,preciounitario)" +
                                "Values('" + counter + "', '" + "ventas.txt" + "', '" + campos[0] + "', '" + campos[1] + "', '" + campos[2] + "', '" + campos[3] + "', '" + campos[4] + "', '" + campos[5]
                                + "', '" + campos[6] + "', '" + campos[7] + "', '" + campos[8] + "', '" + campos[9] + "', '" + campos[10]
                                + "', '" + campos[11] + "', '" + campos[12] + "', '" + campos[13] + "', '" + campos[14] + "', '" + campos[15]
                                + "', '" + campos[16] + "', '" + campos[17] + "', '" + campos[18] + "', '" + campos[19] + "', '" + campos[20] + "')";

                                //Variable SQL para ejecutar el comando del query creado anteriormente (querycreado,conexi�n la base de datos)
                                SqlCommand comandosql = new SqlCommand(query, my_adonet_connection);
                                comandosql.ExecuteNonQuery();

                                string query2 = "INSERT INTO fecha (linea,archivo,fecha) Values('" + counter + "', '" + "ventas.txt" + "', '" + campos[18] + "')";
                                SqlCommand comandosql2 = new SqlCommand(query2, my_adonet_connection);
                                comandosql2.ExecuteNonQuery();
                            }
                            counter++;
                        }
                        archivo_lectura.Close();
                        MessageBox.Show("Archivo ventas.txt fue analizado correctamente");
                        break;

                        default:
                            MessageBox.Show("Error, el archivo: " + archivoactual + " no puede cargarse a la base de datos por falta de su tabla destino");
                            break;
                }
                //Se procede a Cerrar la conexi�n de la base de datos utilizada
                my_adonet_connection.Close();
                //Se genera un resultado de exito para continuar con la siguiente secuencia
                Dts.TaskResult = (int)ScriptResults.Success;
            }
            catch (Exception excepcion)
            {
                MessageBox.Show("Error: " + excepcion.Message.ToString());
                using (StreamWriter sw = File.CreateText(CarpetaErrores + "\\" + "ErrorReadFiles.log"))
                {
                    sw.WriteLine(excepcion.Message.ToString());
                    Dts.TaskResult = (int)ScriptResults.Failure;
                }
            }
        }
    }
    else
    {
        MessageBox.Show("Ha ocurrido un error: El directorio analizado no posee archivos para realizar la carga de la informaci�n");
        using (StreamWriter sw = File.CreateText(CarpetaErrores + "\\" + "ErrorLog.log"))
        {
            sw.WriteLine("Ha ocurrido un error: El directorio analizado no posee archivos para realizar la carga de la informaci�n");
            Dts.TaskResult = (int)ScriptResults.Failure;
        }
    }
}
catch (Exception excepcion)
{
    MessageBox.Show("Ha ocurrido un error: " + excepcion.Message.ToString());
    using (StreamWriter sw = File.CreateText(Dts.Variables["User::CarpetaErrores"].Value.ToString() + "\\" + "ErrorLog.log"))
    {
        sw.WriteLine(excepcion.Message.ToString());
        Dts.TaskResult = (int)ScriptResults.Failure;
    }
}
