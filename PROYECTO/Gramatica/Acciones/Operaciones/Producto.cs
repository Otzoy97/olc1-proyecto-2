using Irony.Parsing;
using PROYECTO.Gramatica.Entorno;
using System;

namespace PROYECTO.Gramatica.Acciones.Operaciones
{
    class Producto
    {
        /// <summary>
        /// Devuelve un simbolo
        /// </summary>
        /// <param name="raiz"></param>
        /// <param name="operClass"></param>
        /// <returns></returns>
        public Simbolo Interpretar(ParseTreeNode raiz, Operar operClass)
        {
            Simbolo symizq = operClass.Interpretar(raiz.ChildNodes[0]);
            Simbolo symder = operClass.Interpretar(raiz.ChildNodes[2]);
            //STRING
            if (symizq.TipoDato == Tipo.STRING || symder.TipoDato == Tipo.STRING)
            {
                Console.WriteLine("No se pueden multiplicar cadenas");
            }
            //ENTERO
            if (symizq.TipoDato == Tipo.INT)
            {
                return ProductoInt((int)symizq.Dato, symder);
            }
            if (symder.TipoDato == Tipo.INT)
            {
                return ProductoInt((int)symder.Dato, symizq);
            }
            //DOUBLE
            if (symizq.TipoDato == Tipo.DOUBLE)
            {
                return ProductoDouble((double)symizq.Dato, symder);
            }
            if (symder.TipoDato == Tipo.DOUBLE)
            {
                return ProductoDouble((double)symder.Dato, symizq);
            }
            //CHAR
            if (symizq.TipoDato == Tipo.CHAR)
            {
                return ProductoChar((char)symizq.Dato, symder);
            }
            if (symder.TipoDato == Tipo.CHAR)
            {
                return ProductoChar((char)symder.Dato, symizq);
            }
            //BOOLEANO
            if (symizq.TipoDato == Tipo.BOOLEAN)
            {
                return ProductoBool((bool)symizq.Dato, symder);
            }
            if (symder.TipoDato == Tipo.BOOLEAN)
            {
                return ProductoBool((bool)symder.Dato, symizq);
            }
            return new Simbolo();
        }
        /// <summary>
        /// Maneja la forma de sumar entre un entero y el resto de tipos primitivos
        /// </summary>
        /// <param name="intVar"></param>
        /// <param name="sym"></param>
        /// <returns></returns>
        private Simbolo ProductoInt(int intVar, Simbolo sym)
        {
            Simbolo retorno = new Simbolo();
            switch (sym.TipoDato)
            {
                case Tipo.INT:
                    retorno.Dato = intVar * ((int)sym.Dato);
                    retorno.TipoDato = Tipo.INT;
                    break;
                case Tipo.STRING:
                    Console.WriteLine("No se puede multiplicar cadenas");
                    break;
                case Tipo.CHAR:
                    retorno.Dato = intVar * ((char)sym.Dato);
                    retorno.TipoDato = Tipo.INT;
                    break;
                case Tipo.DOUBLE:
                    retorno.Dato = intVar * ((double)sym.Dato);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;
                case Tipo.BOOLEAN:
                    retorno.Dato = intVar * ((bool)sym.Dato ? 1 : 0);
                    retorno.TipoDato = Tipo.INT;
                    break;
            }
            return retorno;
        }
        /// <summary>
        /// Maneja la forma de sumar entre un char y el resto de tipos primitivos
        /// </summary>
        /// <param name="charVar"></param>
        /// <param name="sym"></param>
        /// <returns></returns>
        private Simbolo ProductoChar(char charVar, Simbolo sym)
        {
            Simbolo retorno = new Simbolo();
            switch (sym.TipoDato)
            {
                case Tipo.INT:
                    retorno.Dato = charVar * ((int)sym.Dato);
                    retorno.TipoDato = Tipo.INT;
                    break;
                case Tipo.STRING:
                    Console.WriteLine("No se puede multiplicar cadenas");
                    break;
                case Tipo.CHAR:
                    retorno.Dato = charVar * ((char)sym.Dato);
                    retorno.TipoDato = Tipo.INT;
                    break;
                case Tipo.DOUBLE:
                    retorno.Dato = charVar * ((double)sym.Dato);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;
                case Tipo.BOOLEAN:
                    retorno.Dato = charVar * ((bool)sym.Dato ? 1 : 0);
                    retorno.TipoDato = Tipo.INT;
                    break;
            }
            return retorno;
        }
        /// <summary>
        /// Maneja la forma de sumar entre un double y el resto de tipos primitivos
        /// </summary>
        /// <param name="doubleVar"></param>
        /// <param name="sym"></param>
        /// <returns></returns>
        private Simbolo ProductoDouble(double doubleVar, Simbolo sym)
        {
            Simbolo retorno = new Simbolo();
            switch (sym.TipoDato)
            {
                case Tipo.INT:
                    retorno.Dato = doubleVar * ((int)sym.Dato);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;
                case Tipo.STRING:
                    Console.WriteLine("No se puede multiplicar cadenas");
                    break;
                case Tipo.DOUBLE:
                    retorno.Dato = doubleVar * ((double)sym.Dato);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;
                case Tipo.CHAR:
                    retorno.Dato = doubleVar * ((char)sym.Dato);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;
                case Tipo.BOOLEAN:
                    retorno.Dato = doubleVar * ((bool)sym.Dato ? 1 : 0);
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
        private Simbolo ProductoBool(bool boolVar, Simbolo sym)
        {
            Simbolo retorno = new Simbolo();
            switch (sym.TipoDato)
            {
                case Tipo.INT:
                    retorno.Dato = (boolVar ? 1 : 0) * ((int)sym.Dato);
                    retorno.TipoDato = Tipo.INT;
                    break;
                case Tipo.STRING:
                    Console.WriteLine("No se puede multiplicar cadenas");
                    break;
                case Tipo.DOUBLE:
                    retorno.Dato = (boolVar ? 1 : 0) * ((double)sym.Dato);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;
                case Tipo.CHAR:
                    retorno.Dato = (boolVar ? 1 : 0) * ((char)sym.Dato);
                    retorno.TipoDato = Tipo.INT;
                    break;
                case Tipo.BOOLEAN:
                    retorno.Dato = boolVar && (bool)sym.Dato;
                    retorno.TipoDato = Tipo.BOOLEAN;
                    break;
            }
            return retorno;
        }
    }
}
