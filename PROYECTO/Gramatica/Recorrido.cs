using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROYECTO.Gramatica.Entorno;
using Irony.Parsing;
using PROYECTO.Gramatica.Simbolo;

namespace PROYECTO.Gramatica
{
    class Recorrido
    {
        public Dictionary<string, Clase> Clases { get; }

        public Recorrido(ParseTreeNode Raiz)
        {

        }
        /// <summary>
        /// Recorrerá de forma parcial el árbol creado por Irony
        /// Creará la clases
        /// </summary>
        /// <param name="Raiz"></param>
        public void CrearClase(ParseTreeNode Raiz)
        {
            //Determinará si tiene una lista de imports
            foreach (var rama in Raiz.ChildNodes)
            {
                switch (rama.ChildNodes.Count)
                {
                    case 2:
                        //Recupera el nombre de la clase dada
                        String nombreClase = rama.ChildNodes[0].Token.Value.ToString();
                        //Primero verifica que no exista esa clase ya en el Diccionario
                        if (!Clases.ContainsKey(nombreClase))
                        {
                            Clase clase01 = new Clase();
                            Clases.Add(nombreClase, clase01);
                        }
                        else
                        {
                            //No puede haber dos clases con el mismo nombre
                        }
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("No sé we, algo petó :'v");
                        break;
                }
            }
        }
        public void CrearEntorno(Clase clase, ParseTreeNode Raiz)
        {
            foreach (var rama in Raiz.ChildNodes)
            {
                //Apuntador al nodo hijo de esta rama
                var ramaAux = rama.ChildNodes;
                switch (rama.Token.KeyTerm.Name)
                {
                    case "DECLARACION":
                        switch (ramaAux.Count)
                        {
                            //DECLARACION
                            case 2:

                                break;
                            //ASIGNACION
                            case 3:
                                break;
                        }
                        //Symbol simbolo01 = new Symbol(new Posicion(fila,columna), ramaAux[0] );
                        break;
                    case "MAIN_STA":
                        break;
                    case "FUNCION":
                        break;
                    case "METODO":
                        break;
                }
            }
        }
    }
}