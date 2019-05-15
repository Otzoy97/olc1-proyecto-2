using PROYECTO.Gramatica.Acciones;
using PROYECTO.Gramatica.Acciones.Operaciones;
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
        public bool Ejecutar()
        {
            Operar operar = new Operar(this, this);
            //Ejecutará las OPER y ARR de los simbolos
            foreach (var symClass in ClaseSym)
            {
                //Se recupera el tipo de simbolo
                var symClasType = symClass.Value.TipoDato;
                //Si no es null opera el DIMENSIONLST
                if (symClass.Value.Arr != null)
                {
                    var symClassArr = operar.Interpretar(symClass.Value.Arr);
                    symClass.Value.Arreglo = symClassArr.Arreglo;
                }
                //Si no es null opera el OPER
                if (symClass.Value.Oper != null)
                {
                    var symClassOper = operar.Interpretar(symClass.Value.Oper);
                    //Determina si los tipos son los mismo
                    if (symClassOper.TipoDato != symClass.Value.TipoDato)
                    {
                        Main.Imprimir(String.Format("Los tipos no coinciden {0} -> {1} : ({2},{3})", symClassOper.TipoDato , symClass.Value.TipoDato, symClass.Value.Posicion.Fila, symClass.Value.Posicion.Columna));
                        return true;
                    }
                    //Los tipos coinciden
                    //Se debe determinar si es un arreglo
                    if (symClassOper.TipoDato > Tipo.CLASE)
                    {
                        if (symClassOper.Arreglo.Dimension != symClass.Value.Arreglo.Dimension)
                        {
                            Main.Imprimir(String.Format("Las dimensiones de {0} no coincide con {1}  : ({2},{3})", symClassOper.TipoDato, symClass.Value.TipoDato, symClassOper.Posicion != null ? symClassOper.Posicion.Fila : 0, symClassOper.Posicion != null ? symClassOper.Posicion.Columna : 0));
                            return true;
                        }
                        //Es un arreglo, debe asegurarse que las dimensiones coincidan
                        if (symClassOper.Arreglo.SizeUni != symClass.Value.Arreglo.SizeUni)
                        {
                            Main.Imprimir(String.Format("El arreglo unidemensional de {0} no coincide con {1}  : ({2},{3})", symClassOper.TipoDato, symClass.Value.TipoDato, symClassOper.Posicion != null ? symClassOper.Posicion.Fila : 0, symClassOper.Posicion != null ? symClassOper.Posicion.Columna : 0));
                            return true;
                        }
                        if (symClassOper.Arreglo.SizeBi != symClass.Value.Arreglo.SizeBi)
                        {
                            Main.Imprimir(String.Format("El arreglo bidemensional de {0} no coincide con {1}  : ({2},{3})", symClassOper.TipoDato, symClass.Value.TipoDato, symClassOper.Posicion != null ? symClassOper.Posicion.Fila : 0, symClassOper.Posicion != null ? symClassOper.Posicion.Columna : 0));
                            return true;
                        }
                        if (symClassOper.Arreglo.SizeTri != symClass.Value.Arreglo.SizeTri)
                        {
                            Main.Imprimir(String.Format("El arreglo tridemensional de {0} no coincide con {1}  : ({2},{3})", symClassOper.TipoDato, symClass.Value.TipoDato, symClassOper.Posicion != null ? symClassOper.Posicion.Fila : 0, symClassOper.Posicion != null ? symClassOper.Posicion.Columna : 0));
                            return true;
                        }
                    }
                    //Asigna el valor de symClassOpera al dato del diccionar de clases actual
                    symClass.Value.Dato = symClassOper.Dato;
                }
                
            }
            //Importar clases
            foreach (var impNameClass in ClaseImpNames)
            {
                //Buscará coincidencias en el diccionario de clases
                if (Recorrido.Clases.ContainsKey(impNameClass))
                {
                    ClaseImp.Add(impNameClass, Clase.Copiar(Recorrido.Clases[impNameClass]));
                }
                else
                {
                    Main.Imprimir(String.Format("No se encontró la importación : {0}", impNameClass));
                    return true;
                }
            }
            //Verificará que las funciones marcadas como override existan en alguna clase importada
            bool flagOverride = false;
            foreach (var funClass in ClaseEnt)
            {
                if (funClass.Value.Override)
                {
                    //Es una sobrecarga, lo que obliga que exista una función con el mismo nombre en 
                    //el listado de entornos de clases importadas
                    foreach (var impClass in ClaseImp)
                    {
                        //Buscará en sus entornos un nombre igual
                        if (impClass.Value.ClaseEnt.ContainsKey(funClass.Key))
                        {
                            flagOverride = true;
                            break;
                        }
                    }
                    //No encontró ninguna coinciden, se termina la operación
                    if (!flagOverride)
                    {
                        Main.Imprimir(String.Format("La función {0} no permite sobrecarga", funClass.Key));
                        return true;
                    }
                }
            }
            //Buscará una ocurrencia del main
            if (ClaseEnt.ContainsKey("main"))
            {
                //Recupera dicha ocurrencia
                Funcion func = ClaseEnt["main"];
                //Ejecuta la ocurrencia
                Nativa.Print(String.Format("El main terminó en : {0}",func.Ejecutar()));
            }
            return false;
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
        /// <summary>
        /// Devuelve esta clase
        /// </summary>
        /// <returns></returns>
        public Clase BuscarClasePadre()
        {
            return this;
        }

        public Funcion BuscarFuncionPadre()
        {
            return null;
        }

        /// <summary>
        /// Devuelve si algun padre superior es un loop
        /// </summary>
        /// <returns></returns>
        public bool EsLoop()
        {
            return true;
        }
    }
}
