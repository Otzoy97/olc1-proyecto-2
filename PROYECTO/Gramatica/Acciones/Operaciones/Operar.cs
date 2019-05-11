using Irony.Parsing;
using PROYECTO.Gramatica.Entorno;
using System;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Acciones.Operaciones
{
    class Operar 
    {
        public ParseTreeNode OperRaiz { get; }

        public Simbolo Interpretar(ParseTreeNode operRaiz)
        {
            Simbolo retorno = new Simbolo();
            switch (operRaiz.ChildNodes.Count)
            {
                case 1:
                    switch (operRaiz.ChildNodes[0].Term.Name)
                    {
                        case "OPER":
                            retorno = Interpretar(operRaiz.ChildNodes[0]);
                            break;
                        case "tkREAL":
                            //Determina el tipo  (double o int)
                            if (operRaiz.ChildNodes[0].Token.Value.GetType().Equals(typeof(int)))
                            {
                                retorno.TipoDato = Tipo.INT;
                                retorno.Dato = operRaiz.ChildNodes[0].Token.Value;
                            }
                            if (operRaiz.ChildNodes[0].Token.Value.GetType().Equals(typeof(double)))
                            {
                                retorno.TipoDato = Tipo.DOUBLE;
                                retorno.Dato = operRaiz.ChildNodes[0].Token.Value;
                            }
                            break;
                        case "tkCHAR":
                            retorno.TipoDato = Tipo.CHAR;
                            retorno.Dato = operRaiz.ChildNodes[0].Token.Value;
                            break;
                        case "tkSTR":
                            retorno.TipoDato = Tipo.STRING;
                            retorno.Dato = operRaiz.ChildNodes[0].Token.Value;
                            break;
                        case "CSTBOOL":
                            retorno.TipoDato = Tipo.BOOLEAN;
                            retorno.Dato = operRaiz.ChildNodes[0].Token.Value;
                            break;
                        case "tkVAR":
                            break;
                        case "CALL":
                            break;
                    }
                    break;
                case 2:
                    //Operación unaria
                    //NEGACION
                    if (operRaiz.ChildNodes[0].Term.Name.Equals("NOT"))
                    {
                        retorno = Interpretar(operRaiz.ChildNodes[1]);
                        if (retorno.TipoDato == Tipo.BOOLEAN)
                        {
                            retorno.Dato = !(bool)retorno.Dato;
                        }
                        else
                        {
                            Console.WriteLine("La operación 'negación' solo es posible con valores booleanos");
                        }
                    }
                    //INCREMENTO, DECREMENTO
                    if (operRaiz.ChildNodes[0].Term.Name.Equals("OPER"))
                    {
                        retorno = Interpretar(operRaiz.ChildNodes[0]);
                        //Determina si el tipo de dato es el correcto
                        if (retorno.TipoDato == Tipo.INT || retorno.TipoDato == Tipo.DOUBLE || retorno.TipoDato == Tipo.CHAR)
                        {
                            switch (operRaiz.ChildNodes[0].Term.Name)
                            {
                                case "INCREMENTO":
                                    retorno.Dato = retorno.TipoDato == Tipo.INT || retorno.TipoDato == Tipo.CHAR ? ((int)retorno.Dato) + 1 : ((double)retorno.Dato) + 1;
                                    retorno.TipoDato = retorno.TipoDato == Tipo.INT || retorno.TipoDato == Tipo.CHAR ? Tipo.INT : Tipo.DOUBLE;
                                    break;
                                case "DECREMENTO":
                                    retorno.Dato = retorno.TipoDato == Tipo.INT || retorno.TipoDato == Tipo.CHAR ? ((int)retorno.Dato) - 1 : ((double)retorno.Dato) - 1;
                                    retorno.TipoDato = retorno.TipoDato == Tipo.INT || retorno.TipoDato == Tipo.CHAR ? Tipo.INT : Tipo.DOUBLE;
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("La operación 'incremento/decremento' solo es posible con int, double o char");
                        }

                    }
                    //DECLARACION DE CLASE
                    if (operRaiz.ChildNodes[0].Term.Name.Equals("NEW"))
                    {
                        retorno.TipoDato = Tipo.CLASE;
                        string nombreClase = operRaiz.ChildNodes[1].Token.Text.ToLower();
                        //Busca en la lista de clases si existe una clases con el nombre anterior
                        //Clona la clase en cuestión y la asigna a retorno.Dato
                    }
                    //Variables
                    if (operRaiz.ChildNodes[0].Term.Name.Equals("tkVAR"))
                    {
                        string nombreVariable = operRaiz.ChildNodes[1].Token.Text.ToLower();
                        //Busca en la lista de simbolos, verifica que exista el nombre de la variable
                        //Determina si es un array
                        //Determina las dimensiones del array
                        //Devuelve el simbolo
                    }
                    break;
                case 3:
                    //Operación binaria
                    switch (operRaiz.ChildNodes[1].Term.Name)
                    {
                        case "MAS":

                            break;
                        case "MENOS":
                            break;
                        case "POR":
                            break;
                        case "DIVISION":
                            break;
                        case "POTENCIA":
                            break;
                        case "IGUAL":
                            break;
                        case "DIFERENTE":
                            break;
                        case "MAYOR":
                            break;
                        case "MENOR":
                            break;
                        case "MAYOR_IGUAL":
                            break;
                        case "MENOR_IGUAL":
                            break;
                        case "DOT":
                            break;
                        case "OR":
                            break;
                        case "AND":
                            break;
                    }
                    break;
            }
            return retorno;
        }
    }
}
