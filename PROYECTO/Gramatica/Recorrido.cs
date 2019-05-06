using Irony.Parsing;
using PROYECTO.Gramatica.Entorno;
using System;
using System.Collections.Generic;

namespace PROYECTO.Gramatica
{
    class Recorrido
    {
        public Dictionary<string, Clase> Clases { get; }
        private Clase punteroClase;

        public Recorrido()
        {
            Clases = new Dictionary<string, Clase>();
        }

        /*public Recorrido(ParseTreeNode Raiz)
        {

        }*/
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
                            punteroClase = new Clase();
                            
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
        public void CrearEntorno(ParseTreeNode Raiz)
        {
            foreach (var rama in Raiz.ChildNodes)
            {
                //Apuntador al nodo hijo de esta rama
                var ramaAux = rama.ChildNodes;
                switch (rama.Token.KeyTerm.Name)
                {
                    case "DECLARACION":
                        //Obtiene la posición de la variable
                        Posicion pos = new Posicion(ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1);
                        //Variable booleana que servirá para determinar 
                        //la visibilidad del simbolo reconocido
                        bool esPrivado = false;
                        //El primer token es tkVISIBLE? entonces verifica que el contenido
                        if (ramaAux[0].Term.Name.ToString().Equals("tkVISIBLE"))
                        {
                            esPrivado = ramaAux[0].Token.Text.Equals("privado");
                            //Elimna el nodo 
                            ramaAux.RemoveAt(0);
                        }
                        //Recupera el tipo de dato que se debe guardar
                        var dataType = this.GetTipoDato(ramaAux[0].Token.Text, ramaAux[1].Token.KeyTerm.Name.Equals("tkARR"));
                        //Elimina el nodo del tipo de dato
                        ramaAux.RemoveAt(0);
                        //Contando el número de nodos que posee el Nodo padre
                        //Ejecuta diferente acciones
                        switch (ramaAux.Count)
                        {
                            case 1:
                                //Declaración simple sin asignación
                                //Recorre la lista de nodos hijos y crea los simbolos 
                                foreach (var varlst in ramaAux[0].ChildNodes)
                                {
                                    punteroClase.Simbolos.Add(varlst.Token.Text, new Simbolo(pos, null, esPrivado, dataType));
                                }
                                break;
                            case 2:
                                //Declaración array sin asignación
                                if (ramaAux[1].Token.KeyTerm.Name.Equals("DIMENSION_LIST"))
                                {

                                } else
                                //Declaración simple con asignación
                                if (ramaAux[1].Token.KeyTerm.Name.Equals("OPER"))
                                {

                                }
                                break;
                            case 3:
                                
                                break;
                            case 4:
                                //Declaración array con asignación
                                break;
                        }
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
        /// <summary>
        /// Determina el tipo de dato que se utilizará dada la cadena de entrada
        /// </summary>
        /// <param name="tipoDato">cadena que especifica el tipo de dato</param>
        /// <param name="esArray">determina si el tipo de dato es un array o no</param>
        /// <returns></returns>
        private Tipo GetTipoDato(string tipoDato, bool esArray)
        {
            Tipo tipo = esArray ? Tipo.CLASEARR : Tipo.CLASE;
            switch (tipoDato)
            {
                case "int":
                    tipo = esArray ? Tipo.INTARR : Tipo.INT;
                    break;
                case "string":
                    tipo = esArray ? Tipo.STRINGARR : Tipo.STRING;
                    break;
                case "bool":
                    tipo = esArray ? Tipo.BOOLEANARR : Tipo.BOOLEAN;
                    break;
                case "char":
                    tipo = esArray ? Tipo.CHARARR : Tipo.CHAR;
                    break;
                case "double":
                    tipo = esArray ? Tipo.DOUBLEARR : Tipo.DOUBLE;
                    break;
            }
            return tipo;
        }



        public void RecorrerVarList(ParseTreeNodeList Root, Posicion Location)
        {
            foreach (var Nodo in Root) { 
                switch ()
                {   
                    case "int":
                        break;
                    case "string":
                        break;
                    case "bool":
                        break;
                    case "char":
                        break;
                    case "double":
                        break;
                    default:
                        break;
                }
            }
        }
    
}