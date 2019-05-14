using System;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno
{
    class Clase : IEntorno
    { 
        /// <summary>
        /// Almacenará todo los subentorno (funciones/metodos) de la clase
        /// </summary>
        public Dictionary<string, Funcion> ClaseEnt { get; set; }
        /// <summary>
        /// Almacenará todo tipo de dato que se declare (int,string,bool,char,double,clases)
        /// </summary>
        public Dictionary<string, Simbolo> ClaseSym { get; set; }
        /// <summary>
        /// Almacenará cualquier otras clases que se hayan importado
        /// -Esto se especificará en la segunda pasada-
        /// </summary>
        public Dictionary<string, Clase> ClaseImp { get; set; }
        /// <summary>
        /// Lista con los nombre de las clases que se deberán buscar en la
        /// lista de clases
        /// </summary>
        public LinkedList<String> ClaseImpNames { get; set; }
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Clase()
        {
            ClaseEnt = new Dictionary<string, Funcion>();
            ClaseSym = new Dictionary<string, Simbolo>();
            ClaseImp = new Dictionary<string, Clase>();
            ClaseImpNames = new LinkedList<string>();
        }
        public void Ejecutar()
        {

        }
        /// <summary>
        /// Busca en el diccionario de Funciones de la clase y en las funciones de las clases importadas
        /// </summary>
        /// <param name="nombreFuncion">Nombre de la Función buscada</param>
        /// <returns></returns>
        public Funcion BuscarFuncion(string nombreFuncion)
        {
            ///Busca en el diccionario local
            if (ClaseEnt.ContainsKey(nombreFuncion))
            {
                return ClaseEnt[nombreFuncion];
            }
            //Recorre el diccionario de clases importadas
            //en busca de una coincidencia
            foreach (var flagClass in ClaseImp)
            {
                //Utiliza recursion para buscar la función
                Funcion ret = flagClass.Value.BuscarFuncion(nombreFuncion);
                //Verifica que no sea nulo lo que se devolvió
                if (ret != null)
                {
                    //Si no es nulo retorna el valor
                    return ret;
                }
            }
            //No encontró ninguna coincidencia, retorna un null
            return null;
        }
        /// <summary>
        /// Busca entre el diccionario de simbolos de la clase y en los simbolos de las clases importadas
        /// Si no encuentra nada devuelve un Simbolo tipo VOID
        /// </summary>
        /// <param name="nombreVar">Nombre del Simbolo buscado</param>
        /// <returns></returns>
        public Simbolo BuscarSimbolo(string nombreVar)
        {
            //Primero buscará en sus propios simbolos
            if (ClaseSym.ContainsKey(nombreVar))
            {
                return ClaseSym[nombreVar];
            }
            //Si llegó acá, quiere decir que no encontró nada
            //Buscará en los simbolo de las clases importadas
            foreach (var flagClass in ClaseImp)
            {
                if (flagClass.Value.ClaseSym.ContainsKey(nombreVar))
                {
                    return flagClass.Value.ClaseSym[nombreVar];
                }
            }
            //No encontrón nada, devuelve un simbolo vacío
            return new Simbolo();
        }
        /// <summary>
        /// Realiza una copia de la clase específicada
        /// </summary>
        /// <param name="original">Clase a copiar</param>
        /// <returns></returns>
        public static Clase Copiar(Clase original)
        {
            Clase prueba = new Clase();
            ///Copia los simbolos
            foreach (var lst01 in original.ClaseSym)
            {
                Simbolo symPrueba = new Simbolo
                {
                    Arr = lst01.Value.Arr,
                    Dato = lst01.Value.Dato,
                    EsPrivado = lst01.Value.EsPrivado,
                    Oper = lst01.Value.Oper,
                    TipoDato = lst01.Value.TipoDato
                };

                if (lst01.Value.Arreglo != null)
                {
                    Arreglo arr = new Arreglo
                    {
                        Dimension = lst01.Value.Arreglo.Dimension,
                        SizeBi = lst01.Value.Arreglo.SizeBi,
                        SizeUni = lst01.Value.Arreglo.SizeUni,
                        SizeTri = lst01.Value.Arreglo.SizeTri
                    };
                    symPrueba.Arreglo = arr;
                }
                if (lst01.Value.Posicion != null)
                {
                    Posicion pos = new Posicion(lst01.Value.Posicion.Fila, lst01.Value.Posicion.Columna);
                    symPrueba.Posicion = pos;
                }
                prueba.ClaseSym.Add(lst01.Key.ToString(), symPrueba);
            }
            ///Copia los nombre de las clases importadas
            foreach (var lst01 in original.ClaseImpNames)
            {
                prueba.ClaseImpNames.AddLast(lst01.ToString());
            }
            ///Copia las funciones de la clase
            foreach (var lst01 in original.ClaseEnt)
            {
                Funcion funcion = Funcion.Copiar(lst01.Value);
                funcion.ClaseEnt = prueba.ClaseEnt;
                funcion.ClaseImp = prueba.ClaseImp;
                funcion.ClaseImpNames = prueba.ClaseImpNames;
                funcion.ClaseSym = prueba.ClaseSym;
                prueba.ClaseEnt.Add(lst01.Key, funcion);
            }
            ///Copia las clases importadas
            foreach (var lst01 in original.ClaseImp)
            {
                prueba.ClaseImp.Add(lst01.Key.ToString(), Copiar(lst01.Value));
            }
            return prueba;
        }
    }
}
