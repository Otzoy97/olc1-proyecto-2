using Irony.Parsing;
using PROYECTO.Gramatica.Entorno;
using System;

namespace PROYECTO.Gramatica.Acciones.Operaciones
{
    class Division
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
                Console.WriteLine("No se pueden dividir cadenas");
            }
            //ENTERO, DOUBLE, CHAR
            if (symizq.TipoDato == Tipo.INT || symizq.TipoDato == Tipo.DOUBLE || symizq.TipoDato == Tipo.CHAR)
            {
                return DividirDouble((double)symizq.Dato, symder);
            }
            if (symder.TipoDato == Tipo.INT || symder.TipoDato == Tipo.DOUBLE || symder.TipoDato == Tipo.CHAR)
            {
                return DividirDouble((double)symder.Dato, symizq);
            }
            //BOOLEANO
            if (symizq.TipoDato == Tipo.BOOLEAN)
            {
                return DividirBool((bool)symizq.Dato, symder);
            }
            if (symder.TipoDato == Tipo.BOOLEAN)
            {
                return DividirBool((bool)symder.Dato, symizq);
            }
            return new Simbolo();
        }
        /// <summary>
        /// Maneja la forma de sumar entre un double y el resto de tipos primitivos
        /// </summary>
        /// <param name="doubleVar"></param>
        /// <param name="sym"></param>
        /// <returns></returns>
        private static Simbolo DividirDouble(double doubleVar, Simbolo sym)
        {
            Simbolo retorno = new Simbolo();
            switch (sym.TipoDato)
            {
                case Tipo.INT:
                case Tipo.DOUBLE:
                case Tipo.CHAR:
                    retorno.Dato = doubleVar / ((double)sym.Dato);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;
                case Tipo.STRING:
                    Console.WriteLine("No se pueden dividir cadenas");
                    break;
                case Tipo.BOOLEAN:
                    retorno.Dato = doubleVar / ((bool)sym.Dato ? 1 : 0);
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
        private static Simbolo DividirBool(bool boolVar, Simbolo sym)
        {
            Simbolo retorno = new Simbolo();
            switch (sym.TipoDato)
            {
                case Tipo.INT:
                case Tipo.CHAR:
                    retorno.Dato = (boolVar ? 1 : 0) / ((int)sym.Dato);
                    retorno.TipoDato = Tipo.INT;
                    break;
                case Tipo.STRING:
                    Console.WriteLine("No se pueden dividir cadenas");
                    break;
                case Tipo.DOUBLE:
                    retorno.Dato = (boolVar ? 1 : 0) / ((double)sym.Dato);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;
                case Tipo.BOOLEAN:
                    Console.WriteLine("No se puede dividir entre booleanos");
                    break;
            }
            return retorno;
        }
    }
}
