using System;
using System.IO;

namespace PROYECTO.Archivo
{
    class ES
    {
        /// <summary>
        /// Obtiene el texto de un archivo de texto plano
        /// </summary>
        /// <param name="URL">El AbsolutPath del archivo que se desea leer</param>
        /// <returns></returns>
        public String Leer(String URL)
        {
            //Prepara el archivo para su lectura
            StreamReader reader = null;
            //Se almacenar temporalmente una linea
            String texto = "";
            //Aquí se almacenara TODAS las lineas leídas :v
            String retorno = "";
            //El sistema intentará escribir un string
            try
            {
                //Se instancia el archivo para su lectura
                reader = new StreamReader(URL);
                //Mientras el texto no sea nulo
                while ((texto = reader.ReadLine()) != null)
                {
                    //Agrega texto a la cadena a retornar
                    retorno += texto + Environment.NewLine;
                }
            }
            catch (Exception)
            {
                //Ocurre un error al leer el archivo
            }
            finally
            {
                //Intentará cerrar la lectura del archivo
                try
                {
                    //Verifica que se haya instanciado el StreamReader
                    if (reader != null)
                    {
                        //Cierra la lectura del archivo
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                    //Ocurre un error al cerrar el archivo
                }
            }
            //Retorna la cadena de texto, con o sin errores en ella
            return retorno;
        }
    }
}
