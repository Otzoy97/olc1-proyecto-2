using Irony.Parsing;
using PROYECTO.Gramatica.Entorno;
using System;

namespace PROYECTO.Gramatica.Acciones.Operaciones
{
    class Potencia
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
                Console.WriteLine("No se puede potenciar cadenas");
            }
            //ENTERO y CHAR
            if (symizq.TipoDato == Tipo.INT || symizq.TipoDato == Tipo.CHAR)
            {
                return PotenciarInt((int)symizq.Dato, symder);
            }
            if (symder.TipoDato == Tipo.INT || symder.TipoDato == Tipo.CHAR)
            {
                return PotenciarInt((int)symder.Dato, symizq);
            }
            //DOUBLE
            if (symizq.TipoDato == Tipo.DOUBLE)
            {
                return PotenciarDouble((double)symizq.Dato, symder);
            }
            if (symder.TipoDato == Tipo.DOUBLE)
            {
                return PotenciarDouble((double)symder.Dato, symizq);
            }
            //BOOLEANO
            if (symizq.TipoDato == Tipo.BOOLEAN)
            {
                return PotenciarBool((bool)symizq.Dato, symder);
            }
            if (symder.TipoDato == Tipo.BOOLEAN)
            {
                return PotenciarBool((bool)symder.Dato, symizq);
            }
            return new Simbolo();
        }
        /// <summary>
        /// Maneja la forma de sumar entre un entero y el resto de tipos primitivos
        /// </summary>
        /// <param name="intVar"></param>
        /// <param name="sym"></param>
        /// <returns></returns>
        private static Simbolo PotenciarInt(int intVar, Simbolo sym)
        {
            Simbolo retorno = new Simbolo();
            switch (sym.TipoDato)
            {
                case Tipo.INT:
                case Tipo.CHAR:
                    retorno.Dato = intVar ^ ((int)sym.Dato);
                    retorno.TipoDato = Tipo.INT;
                    break;
                case Tipo.STRING:
                    Console.WriteLine("No se puede potenciar cadenas");
                    break;
                case Tipo.DOUBLE:
                    retorno.Dato = Math.Pow((double)intVar, (double)sym.Dato);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;
                case Tipo.BOOLEAN:
                    retorno.Dato = ((int)((bool)sym.Dato ? intVar : 1));
                    retorno.TipoDato = Tipo.INT;
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
        private static Simbolo PotenciarDouble(double intVar, Simbolo sym)
        {
            Simbolo retorno = new Simbolo();
            switch (sym.TipoDato)
            {
                case Tipo.INT:
                case Tipo.CHAR:
                case Tipo.DOUBLE:
                    retorno.Dato = Math.Pow(intVar, (double)sym.Dato);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;
                case Tipo.STRING:
                    Console.WriteLine("No se puede potenciar cadenas");
                    break;
                case Tipo.BOOLEAN:
                    retorno.Dato = ((double) ((bool)sym.Dato ? intVar : 1));
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
        private static Simbolo PotenciarBool(bool boolVar, Simbolo sym)
        {
            Simbolo retorno = new Simbolo();
            switch (sym.TipoDato)
            {
                case Tipo.INT:
                case Tipo.CHAR:
                    retorno.Dato = (int)(boolVar ? 1 : 0);
                    retorno.TipoDato = Tipo.INT;
                    break;
                case Tipo.STRING:
                    Console.WriteLine("No se puede potenciar cadenas");
                    break;
                case Tipo.DOUBLE:
                    retorno.Dato = (double)(boolVar ? 1 : 0);
                    retorno.TipoDato = Tipo.DOUBLE;
                    break;
                case Tipo.BOOLEAN:
                    Console.WriteLine("No se puede potenciar entre booleanos");
                    break;
            }
            return retorno;
        }
    }
}
