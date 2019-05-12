using Irony.Parsing;
using PROYECTO.Gramatica.Entorno;
using System;

namespace PROYECTO.Gramatica.Acciones.Operaciones
{
    enum TipoRel
    {
        IGUAL, DIFERENTE, MAYOR, MENOR, MENOR_IGUAL, MAYOR_IGUAL
    }
    class Relacionar
    {
        private static TipoRel tipo;
        /// <summary>
        /// Devuelve un simbolo
        /// </summary>
        /// <param name="raiz"></param>
        /// <param name="operClass"></param>
        /// <returns></returns>
        public static Simbolo Interpretar(ParseTreeNode raiz, Operar operClass, TipoRel tipo)
        {
            //Asigna la variable global
            Relacionar.tipo = tipo;
            
            Simbolo symizq = operClass.Interpretar(raiz.ChildNodes[0]);
            Simbolo symder = operClass.Interpretar(raiz.ChildNodes[2]);
            //ENTERO DOUBLE
            if (symizq.TipoDato == Tipo.INT || symizq.TipoDato == Tipo.DOUBLE)
            {
                return RelDouble(symizq.TipoDato == Tipo.INT ? (int)symizq.Dato : (double)symizq.Dato, symder);
            }
            if (symder.TipoDato == Tipo.INT || symder.TipoDato == Tipo.DOUBLE)
            {
                return RelDouble(symder.TipoDato == Tipo.INT ? (int) symder.Dato : (double)symder.Dato, symizq);
            }
            //STRING
            if (symizq.TipoDato == Tipo.STRING)
            {
                return RelString(symizq.Dato.ToString(), symder);
            }
            if (symder.TipoDato == Tipo.STRING)
            {
                return RelString(symder.Dato.ToString(), symizq);
            }
            //CHAR
            if (symizq.TipoDato == Tipo.CHAR)
            {
                return RelChar((char)symizq.Dato, symder);
            }
            if (symder.TipoDato == Tipo.CHAR)
            {
                return RelChar((char)symder.Dato, symizq);
            }
            //BOOLEANO
            if (symizq.TipoDato == Tipo.BOOLEAN)
            {
                return RelBool((bool)symizq.Dato, symder);
            }
            if (symder.TipoDato == Tipo.BOOLEAN)
            {
                return RelBool((bool)symder.Dato, symizq);
            }
            return new Simbolo();
        }
        /// <summary>
        /// Maneja la forma de comparar un entero u double con otro tipo de dato
        /// </summary>
        /// <param name="doubleVar"></param>
        /// <param name="sym"></param>
        /// <returns></returns>
        private static Simbolo RelDouble(double doubleVar, Simbolo sym)
        {
            Simbolo retorno = new Simbolo();
            switch (sym.TipoDato)
            {
                case Tipo.INT:
                case Tipo.CHAR:
                case Tipo.DOUBLE:
                    var auxTemp = (sym.TipoDato == Tipo.INT ? (int)sym.Dato : sym.TipoDato == Tipo.CHAR ? (char)sym.Dato : (double)sym.Dato);
                    switch (tipo)
                    {
                        case TipoRel.IGUAL:
                            retorno.Dato = doubleVar == auxTemp;
                            break;
                        case TipoRel.DIFERENTE:
                            retorno.Dato = doubleVar != auxTemp;
                            break;
                        case TipoRel.MAYOR:
                            retorno.Dato = doubleVar > auxTemp;
                            break;
                        case TipoRel.MENOR:
                            retorno.Dato = doubleVar < auxTemp;
                            break;
                        case TipoRel.MENOR_IGUAL:
                            retorno.Dato = doubleVar <= auxTemp;
                            break;
                        case TipoRel.MAYOR_IGUAL:
                            retorno.Dato = doubleVar >= auxTemp;
                            break;
                    }
                    retorno.TipoDato = Tipo.BOOLEAN;
                    break;
                case Tipo.STRING:
                    Console.WriteLine("No se puede comparar int y string");
                    break;
                case Tipo.BOOLEAN:
                    switch (tipo)
                    {
                        case TipoRel.IGUAL:
                            retorno.Dato = doubleVar == ((bool)sym.Dato ? 1 : 0);
                            break;
                        case TipoRel.DIFERENTE:
                            retorno.Dato = doubleVar != ((bool)sym.Dato ? 1 : 0);
                            break;
                        case TipoRel.MAYOR:
                            retorno.Dato = doubleVar > ((bool)sym.Dato ? 1 : 0);
                            break;
                        case TipoRel.MENOR:
                            retorno.Dato = doubleVar < ((bool)sym.Dato ? 1 : 0);
                            break;
                        case TipoRel.MENOR_IGUAL:
                            retorno.Dato = doubleVar <= ((bool)sym.Dato ? 1 : 0);
                            break;
                        case TipoRel.MAYOR_IGUAL:
                            retorno.Dato = doubleVar >= ((bool)sym.Dato ? 1 : 0);
                            break;
                    }
                    retorno.TipoDato = Tipo.BOOLEAN;
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
        private static Simbolo RelChar(char charVar, Simbolo sym)
        {
            Simbolo retorno = new Simbolo();
            switch (sym.TipoDato)
            {
                case Tipo.INT:
                case Tipo.DOUBLE:
                case Tipo.CHAR:
                    var auxTemp = (sym.TipoDato == Tipo.INT ? (int)sym.Dato : sym.TipoDato == Tipo.CHAR ? (char)sym.Dato : (double)sym.Dato);
                    switch (tipo)
                    {
                        case TipoRel.IGUAL:
                            retorno.Dato = charVar == auxTemp;
                            break;
                        case TipoRel.DIFERENTE:
                            retorno.Dato = charVar != auxTemp;
                            break;
                        case TipoRel.MAYOR:
                            retorno.Dato = charVar > auxTemp;
                            break;
                        case TipoRel.MENOR:
                            retorno.Dato = charVar < auxTemp;
                            break;
                        case TipoRel.MENOR_IGUAL:
                            retorno.Dato = charVar <= auxTemp;
                            break;
                        case TipoRel.MAYOR_IGUAL:
                            retorno.Dato = charVar >= auxTemp;
                            break;
                    }
                    retorno.TipoDato = Tipo.BOOLEAN;
                    break;
                case Tipo.STRING:
                    Console.WriteLine("Solo se aceptan compraciones entre strings");
                    break;
                case Tipo.BOOLEAN:
                    Console.WriteLine("No se puede comparar un char y un booleano");
                    break;
            }
            return retorno;
        }
        /// <summary>
        /// Maneja la forma de comparar un string con otro tipo de dato
        /// </summary>
        /// <param name="strVar"></param>
        /// <param name="sym"></param>
        /// <returns></returns>
        private static Simbolo RelString(String strVar, Simbolo sym)
        {
            Simbolo retorno = new Simbolo();
            switch (sym.TipoDato)
            {
                case Tipo.INT:
                case Tipo.BOOLEAN:
                case Tipo.DOUBLE:
                case Tipo.CHAR:
                    Console.WriteLine("Solo se aceptan compraciones entre strings");
                    break;
                case Tipo.STRING:
                    switch (tipo)
                    {
                        case TipoRel.IGUAL:
                            retorno.Dato = strVar.Equals(sym.Dato.ToString());
                            break;
                        case TipoRel.DIFERENTE:
                            retorno.Dato = !strVar.Equals(sym.Dato.ToString());
                            break;
                        case TipoRel.MAYOR:
                            retorno.Dato = strVar.Length > sym.Dato.ToString().Length;
                            break;
                        case TipoRel.MENOR:
                            retorno.Dato = strVar.Length < sym.Dato.ToString().Length;
                            break;
                        case TipoRel.MENOR_IGUAL:
                            retorno.Dato = strVar.Length <= sym.Dato.ToString().Length;
                            break;
                        case TipoRel.MAYOR_IGUAL:
                            retorno.Dato = strVar.Length >= sym.Dato.ToString().Length;
                            break;
                    }
                    retorno.TipoDato = Tipo.BOOLEAN;
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
        private static Simbolo RelBool(bool boolVar, Simbolo sym)
        {
            Simbolo retorno = new Simbolo();
            switch (sym.TipoDato)
            {
                case Tipo.INT:
                case Tipo.DOUBLE:
                    switch (tipo)
                    {
                        case TipoRel.IGUAL:
                            retorno.Dato = (boolVar ? 1 : 0) == ((double)sym.Dato);
                            break;
                        case TipoRel.DIFERENTE:
                            retorno.Dato = (boolVar ? 1 : 0) != ((double)sym.Dato);
                            break;
                        case TipoRel.MAYOR:
                            retorno.Dato = (boolVar ? 1 : 0) > ((double)sym.Dato);
                            break;
                        case TipoRel.MENOR:
                            retorno.Dato = (boolVar ? 1 : 0) < ((double)sym.Dato);
                            break;
                        case TipoRel.MENOR_IGUAL:
                            retorno.Dato = (boolVar ? 1 : 0) <= ((double)sym.Dato);
                            break;
                        case TipoRel.MAYOR_IGUAL:
                            retorno.Dato = (boolVar ? 1 : 0) >= ((double)sym.Dato);
                            break;
                    }
                    retorno.TipoDato = Tipo.BOOLEAN;
                    break;
                case Tipo.CHAR:
                    Console.WriteLine("No se puede comparar un bool y un char");
                    break;
                case Tipo.STRING:
                    Console.WriteLine("Solo se aceptan compraciones entre strings");
                    break;
                case Tipo.BOOLEAN:
                    switch (tipo)
                    {
                        case TipoRel.IGUAL:
                            retorno.Dato = boolVar == (bool)sym.Dato;
                            break;
                        case TipoRel.DIFERENTE:
                            retorno.Dato = boolVar != (bool)sym.Dato;
                            break;
                        case TipoRel.MAYOR:
                            retorno.Dato = boolVar && !(bool)sym.Dato; 
                            break;
                        case TipoRel.MENOR:
                            retorno.Dato = !boolVar && (bool)sym.Dato;
                            break;
                        case TipoRel.MENOR_IGUAL:
                            retorno.Dato = !boolVar || (bool)sym.Dato;
                            break;
                        case TipoRel.MAYOR_IGUAL:
                            retorno.Dato = boolVar || !(bool)sym.Dato;
                            break;
                    }
                    retorno.TipoDato = Tipo.BOOLEAN;
                    break;
            }
            return retorno;
        }
    }
}
