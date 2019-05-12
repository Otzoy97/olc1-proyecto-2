using Irony.Parsing;
using PROYECTO.Gramatica.Entorno;
using System;

namespace PROYECTO.Gramatica.Acciones.Operaciones
{
    class Resta
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
            //STRING
            if (symizq.TipoDato == Tipo.STRING || symder.TipoDato == Tipo.STRING)
            {
                Console.WriteLine("No se pueden restar cadenas");
            }
            //ENTERO
            if (symizq.TipoDato == Tipo.INT)
            {
                return RestarInt((int)symizq.Dato, symder);
            }
            if (symder.TipoDato == Tipo.INT)
            {
                return RestarInt((int)symder.Dato, symizq);
            }
            //DOUBLE
            if (symizq.TipoDato == Tipo.DOUBLE)
            {
                return RestarDouble((double)symizq.Dato, symder);
            }
            if (symder.TipoDato == Tipo.DOUBLE)
            {
                return RestarDouble((double)symder.Dato, symizq);
            }
            //CHAR
            if (symizq.TipoDato == Tipo.CHAR)
            {
                return RestarChar((char)symizq.Dato, symder);
            }
            if (symder.TipoDato == Tipo.CHAR)
            {
                return RestarChar((char)symder.Dato, symizq);
            }
            //BOOLEANO
            if (symizq.TipoDato == Tipo.BOOLEAN)
            {
                return RestarBool((bool)symizq.Dato, symder);
            }
            if (symder.TipoDato == Tipo.BOOLEAN)
            {
                return RestarBool((bool)symder.Dato, symizq);
            }
            return new Simbolo();
        }
        /// <summary>
        /// Maneja la forma de sumar entre un entero y el resto de tipos primitivos
        /// </summary>
        /// <param name="intVar"></param>
        /// <param name="sym"></param>
        /// <returns></returns>
        private static Simbolo RestarInt(int intVar, Simbolo sym)
        {
            Simbolo retorno = new Simbolo();
            switch (sym.TipoDato)
            {
                case Tipo.INT:
                    retorno.Dato = intVar - ((int)sym.Dato);
                    retorno.TipoDato = Tipo.INT;
                    break;
                case Tipo.STRING:
                    Console.WriteLine("No se pueden restar cadenas");
                    break;
                case Tipo.CHAR:
                    retorno.Dato = intVar - ((char)sym.Dato);
                    retorno.TipoDato = Tipo.INT;
                    break;
                case Tipo.DOUBLE:
                    retorno.Dato = intVar - ((double)sym.Dato);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;
                case Tipo.BOOLEAN:
                    retorno.Dato = intVar - ((bool)sym.Dato ? 1 : 0);
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
        private static Simbolo RestarChar(char charVar, Simbolo sym)
        {
            Simbolo retorno = new Simbolo();
            switch (sym.TipoDato)
            {
                case Tipo.INT:
                    retorno.Dato = charVar - ((int)sym.Dato);
                    retorno.TipoDato = Tipo.INT;
                    break;
                case Tipo.STRING:
                    Console.WriteLine("No se pueden restar cadenas");
                    break;
                case Tipo.CHAR:
                    retorno.Dato = charVar - ((char)sym.Dato);
                    retorno.TipoDato = Tipo.INT;
                    break;
                case Tipo.DOUBLE:
                    retorno.Dato = charVar - ((double)sym.Dato);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;
                case Tipo.BOOLEAN:
                    Console.WriteLine("No se puede realizar una resta entre un char y un boolean");
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
        private static Simbolo RestarDouble(double doubleVar, Simbolo sym)
        {
            Simbolo retorno = new Simbolo();
            switch (sym.TipoDato)
            {
                case Tipo.INT:
                    retorno.Dato = doubleVar - ((int)sym.Dato);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;

                case Tipo.STRING:
                    Console.WriteLine("No se pueden restar cadenas"); ;
                    break;
                case Tipo.DOUBLE:
                    retorno.Dato = doubleVar - ((double)sym.Dato);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;
                case Tipo.CHAR:
                    retorno.Dato = doubleVar - ((char)sym.Dato);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;
                case Tipo.BOOLEAN:
                    retorno.Dato = doubleVar - ((bool)sym.Dato ? 1 : 0);
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
        private static Simbolo RestarBool(bool boolVar, Simbolo sym)
        {
            Simbolo retorno = new Simbolo();
            switch (sym.TipoDato)
            {
                case Tipo.INT:
                    retorno.Dato = (boolVar ? 1 : 0) + ((int)sym.Dato);
                    retorno.TipoDato = Tipo.INT;
                    break;
                case Tipo.STRING:
                    Console.WriteLine("No se puede sumar un booleano y una cadena");
                    break;
                case Tipo.DOUBLE:
                    retorno.Dato = (boolVar ? 1 : 0) + ((double)sym.Dato);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;
                case Tipo.CHAR:
                    Console.WriteLine("No se puede sumar un booleano y un caracter");
                    break;
                case Tipo.BOOLEAN:
                    Console.WriteLine("No se puede sumar entre booleanos");
                    break;
            }
            return retorno;
        }
    }
}
