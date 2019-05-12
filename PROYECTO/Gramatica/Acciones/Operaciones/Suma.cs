using Irony.Parsing;
using PROYECTO.Gramatica.Entorno;
using System;

namespace PROYECTO.Gramatica.Acciones.Operaciones
{
    class Suma //: IOperacion
    {
        /// <summary>
        /// Devuelve un simbolo
        /// </summary>
        /// <param name="raiz"></param>
        /// <param name="operClass"></param>
        /// <returns></returns>
        public static Simbolo Interpretar(ParseTreeNode raiz, Operar operClass)
        {
            Simbolo symizq = operClass.Interpretar(raiz.ChildNodes[0]);
            Simbolo symder = operClass.Interpretar(raiz.ChildNodes[2]);
            //ENTERO
            if (symizq.TipoDato == Tipo.INT)
            {
                return SumarInt((int)symizq.Dato, symder);
            }
            if (symder.TipoDato == Tipo.INT)
            {
                return SumarInt((int)symder.Dato, symizq);
            }
            //STRING
            if (symizq.TipoDato == Tipo.STRING)
            {
                return SumarString(symizq.Dato.ToString(), symder);
            }
            if (symder.TipoDato == Tipo.STRING)
            {
                return SumarString(symder.Dato.ToString(), symizq);
            }
            //DOUBLE
            if (symizq.TipoDato == Tipo.DOUBLE)
            {
                return SumarDouble((double)symizq.Dato, symder);
            }
            if (symder.TipoDato == Tipo.DOUBLE)
            {
                return SumarDouble((double)symder.Dato, symizq);
            }
            //CHAR
            if (symizq.TipoDato == Tipo.CHAR)
            {
                return SumarChar((char)symizq.Dato, symder);
            }
            if (symder.TipoDato == Tipo.CHAR)
            {
                return SumarChar((char)symder.Dato, symizq);
            }
            //BOOLEANO
            if (symizq.TipoDato == Tipo.BOOLEAN)
            {
                return SumarBool((bool)symizq.Dato, symder);
            }
            if (symder.TipoDato == Tipo.BOOLEAN)
            {
                return SumarBool((bool)symder.Dato, symizq);
            }
            return new Simbolo();
        }
        /// <summary>
        /// Maneja la forma de sumar entre un entero y el resto de tipos primitivos
        /// </summary>
        /// <param name="intVar"></param>
        /// <param name="sym"></param>
        /// <returns></returns>
        private static Simbolo SumarInt(int intVar, Simbolo sym)
        {
            Simbolo retorno = new Simbolo();
            switch (sym.TipoDato)
            {
                case Tipo.INT:
                    retorno.Dato = intVar + ((int)sym.Dato);
                    retorno.TipoDato = Tipo.INT;
                    break;
                case Tipo.STRING:
                    retorno.Dato = intVar.ToString() + sym.Dato.ToString();
                    retorno.TipoDato = Tipo.STRING;
                    break;
                case Tipo.CHAR:
                    retorno.Dato = intVar + ((char)sym.Dato);
                    retorno.TipoDato = Tipo.INT;
                    break;
                case Tipo.DOUBLE:
                    retorno.Dato = intVar + ((double)sym.Dato);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;
                case Tipo.BOOLEAN:
                    retorno.Dato = intVar + ((bool)sym.Dato ? 1 : 0);
                    retorno.TipoDato = Tipo.INT;
                    break;
            }
            return retorno;
        }
        /// <summary>
        /// Maneja la forma de sumar entre un char y el resto de tipos primitivos
        /// </summary>
        /// <param name="intVar"></param>
        /// <param name="sym"></param>
        /// <returns></returns>
        private static Simbolo SumarChar(char intVar, Simbolo sym)
        {
            Simbolo retorno = new Simbolo();
            switch (sym.TipoDato)
            {
                case Tipo.INT:
                    retorno.Dato = intVar + ((int)sym.Dato);
                    retorno.TipoDato = Tipo.INT;
                    break;
                case Tipo.STRING:
                    retorno.Dato = intVar.ToString() + sym.Dato.ToString();
                    retorno.TipoDato = Tipo.STRING;
                    break;
                case Tipo.CHAR:
                    retorno.Dato = intVar + ((char)sym.Dato);
                    retorno.TipoDato = Tipo.INT;
                    break;
                case Tipo.DOUBLE:
                    retorno.Dato = intVar + ((double)sym.Dato);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;
                case Tipo.BOOLEAN:
                    retorno.Dato = intVar + ((bool)sym.Dato ? 1 : 0);
                    retorno.TipoDato = Tipo.INT;
                    break;
            }
            return retorno;
        }
        /// <summary>
        /// Maneja la forma de sumar entre un string y el resto de tipos primitivos
        /// </summary>
        /// <param name="strVar"></param>
        /// <param name="sym"></param>
        /// <returns></returns>
        private static Simbolo SumarString(String strVar, Simbolo sym)
        {
            Simbolo retorno = new Simbolo();
            switch (sym.TipoDato)
            {
                case Tipo.INT:
                case Tipo.STRING:
                case Tipo.DOUBLE:
                case Tipo.CHAR:
                    retorno.Dato = strVar + sym.Dato.ToString();
                    retorno.TipoDato = Tipo.STRING;
                    break;
                case Tipo.BOOLEAN:
                    Console.WriteLine("No se puede sumar una cadena y un booleano");
                    break;
            }
            return retorno;
        }
        /// <summary>
        /// Maneja la forma de sumar entre un double y el resto de tipos primitivos
        /// </summary>
        /// <param name="intVar"></param>
        /// <param name="sym"></param>
        /// <returns></returns>
        private static Simbolo SumarDouble(double intVar, Simbolo sym)
        {
            Simbolo retorno = new Simbolo();
            switch (sym.TipoDato)
            {
                case Tipo.INT:
                    retorno.Dato = intVar + ((int)sym.Dato);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;

                case Tipo.STRING:
                    retorno.Dato = intVar.ToString() + sym.Dato.ToString();
                    retorno.TipoDato = Tipo.STRING;
                    break;
                case Tipo.DOUBLE:
                    retorno.Dato = intVar + ((double)sym.Dato);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;
                case Tipo.CHAR:
                    retorno.Dato = intVar + ((char)sym.Dato);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;
                case Tipo.BOOLEAN:
                    retorno.Dato = intVar + ((bool)sym.Dato ? 1 : 0);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;
            }
            return retorno;
        }
        /// <summary>
        /// Maneja la forma de sumar entre un booleano y el resto de tipos primitivos
        /// </summary>
        /// <param name="boolVar"></param>
        /// <param name="sym"></param>
        /// <returns></returns>
        private static Simbolo SumarBool(bool boolVar, Simbolo sym)
        {
            Simbolo retorno = new Simbolo();
            switch (sym.TipoDato)
            {
                case Tipo.INT:
                    retorno.Dato = (boolVar ? 1 : 0) + ((int)sym.Dato);
                    retorno.TipoDato = Tipo.INT;
                    break;
                case Tipo.STRING:
                    Console.WriteLine("No se puede sumar una cadena y un booleano");
                    break;
                case Tipo.DOUBLE:
                    retorno.Dato = (boolVar ? 1 : 0) + ((double)sym.Dato);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;
                case Tipo.CHAR:
                    retorno.Dato = (boolVar ? 1 : 0) + ((char)sym.Dato);
                    retorno.TipoDato = Tipo.INT;
                    break;
                case Tipo.BOOLEAN:
                    retorno.Dato = boolVar || (bool)sym.Dato;
                    retorno.TipoDato = Tipo.BOOLEAN;
                    break;
            }
            return retorno;
        }
    }
}
