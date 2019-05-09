﻿using Irony.Parsing;
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
                            this.Clases.Add(rama.ChildNodes[0].Token.Value.ToString(), punteroClase);
                            this.CrearEntorno(rama.ChildNodes[1]);
                        }
                        else
                        {
                            //No puede haber dos clases con el mismo nombre
                        }
                        break;
                    case 3:
                        //Primero verifica que no exista esa clase ya en el Diccionario
                        if (!Clases.ContainsKey(rama.ChildNodes[0].Token.Value.ToString()))
                        {
                            punteroClase = new Clase
                            {
                                ClaseImpTree = rama.ChildNodes[1]
                            };
                            this.Clases.Add(rama.ChildNodes[0].Token.Value.ToString(), punteroClase);
                            this.CrearEntorno(rama.ChildNodes[2]);
                        }
                        else
                        {
                            //No puede haber dos clases con el mismo nombre
                        }
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
                switch (rama.Term.Name)
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
                        //Obtiene el tipo de dato
                        Tipo dataType = GetTipoDato(ramaAux);
                        //Contando el número de nodos que posee el Nodo padre
                        //ejecuta diferente acciones
                        switch (dataType)
                        {
                            case Tipo.INT:
                            case Tipo.STRING:
                            case Tipo.DOUBLE:
                            case Tipo.CHAR:
                            case Tipo.BOOLEAN:
                            case Tipo.CLASE:
                                foreach (var varlst in ramaAux[0].ChildNodes)
                                {
                                    if (!punteroClase.ClaseSym.ContainsKey(varlst.Token.Text))
                                    {
                                        punteroClase.ClaseSym.Add(varlst.Token.Text, new Simbolo(pos, esPrivado, ramaAux.Count != 2 ? null : ramaAux[1], dataType));
                                    }
                                    else
                                    {
                                        //Error variable duplicada
                                    }
                                }
                                break;
                            case Tipo.INTARR:
                            case Tipo.STRINGARR:
                            case Tipo.DOUBLEARR:
                            case Tipo.CHARARR:
                            case Tipo.BOOLEANARR:
                            case Tipo.CLASEARR:
                                if (ramaAux[1].ChildNodes.Count > 3)
                                {
                                    //Marcar error de dimensiones máximas 
                                }
                                else
                                {
                                    //Guarda el array
                                    foreach (var varlst in ramaAux[0].ChildNodes)
                                    {
                                        if (!punteroClase.ClaseSym.ContainsKey(varlst.Token.Text))
                                        {
                                            punteroClase.ClaseSym.Add(varlst.Token.Text, new Simbolo(pos, esPrivado, ramaAux.Count != 3 ? null : ramaAux[2], ramaAux[1], dataType));
                                        }
                                        else
                                        {
                                            //Error variable duplicada
                                        }
                                    }
                                }
                                break;
                        }
                        break;
                    case "MAIN_STA":
                        //Verifica que no exista ya un main
                        if (!punteroClase.ClaseEnt.ContainsKey("main"))
                        {
                            //Crea el subentorno main
                            Funcion main = new Funcion
                            {
                                //Enlaza el hijo con el padre
                                ClaseSym = punteroClase.ClaseSym,
                                ClaseImp = punteroClase.ClaseImp,
                                ClaseEnt = punteroClase.ClaseEnt,
                                ClaseImpTree = punteroClase.ClaseImpTree
                            };
                            //Establece los atributos de la funcion
                            punteroClase.ClaseEnt.Add("main",main);
                        }
                        else
                        {
                            //Arroja error de dos main en la misma clase
                            Console.WriteLine("Main duplicado. No pueden existir dos main en la misma clase");
                        }
                        break;
                    case "FUNCION":
                        break;
                    case "METODO":
                        break;
                }
            }
        }
        /// <summary>
        /// Determina el tipo de dato que se utilizará según la lista de nodo dada
        /// </summary>
        /// <param name="tipoDato">Lista de nodos que contiene la info de una declaración de variable</param>
        /// <returns></returns>
        private Tipo GetTipoDato(ParseTreeNodeList tipoDato)
        {
            //Servirá para determinar si la variable es o no un array
            bool esArray = false;
            switch (tipoDato.Count)
            {
                case 2:
                    esArray = false;
                    break;
                case 3:
                    esArray = tipoDato[2].Term.Name.Equals("DIMENSION_LIST");
                    break;
                case 4:
                    esArray = true;
                    break;
            }
            //Establece el tipo de dato
            Tipo tipo = esArray ? Tipo.CLASEARR : Tipo.CLASE;
            switch (tipoDato[0].Token.Text)
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
            //Elimina el nodo, pues ya se recuperó la info deseada
            tipoDato.RemoveAt(0);
            return tipo;
        }
    }
}