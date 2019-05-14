using Irony.Parsing;
using System;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno
{
    class Funcion : Clase, IEntorno
    {
        //Simbolos almacendos en tiempo de ejecución
        public Dictionary<string, Simbolo> FuncSym { get; }
        //Subarbol de acciones
        public ParseTreeNode FuncTree { get; set; }
        //Determina si la función es privada
        public bool EsPrivado { get; set; }
        //Determina el tipo de dato que debe devolver
        //public Tipo DataType { get; set; }
        //Especifica el simbolo a retornar
        public Simbolo ReturnData { get; set; }
        //Es sobrecarga
        public bool Override { get; set; }
        //Simbolo parametros de la función
        public Dictionary<string, Simbolo> FuncParSym { get; set; }

        public Funcion()
        {
            FuncSym = new Dictionary<string, Simbolo>();
            ReturnData = new Simbolo();
            EsPrivado = false;            
            FuncTree = null;   
            Override = false;
        }

        public new Simbolo BuscarSimbolo(string nombreVar)
        {
            return this.FuncSym.ContainsKey(nombreVar) ? this.FuncSym[nombreVar] : base.BuscarSimbolo(nombreVar);
        }

        

        public new void Ejecutar()
        {

        }

        /// <summary>
        /// Realiza una copia de la función 
        /// </summary>
        /// <param name="original">Función a copiar</param>
        /// <returns></returns>
        public static Funcion Copiar(Funcion original)
        {
            Funcion funcion = new Funcion()
            {
                FuncTree = original.FuncTree,
                EsPrivado = original.EsPrivado,
                Override = original.Override
            };
            //Copia el simbolo de retorno
            funcion.ReturnData.Arr = original.ReturnData.Arr;
            funcion.ReturnData.Dato = original.ReturnData.Dato;
            funcion.ReturnData.EsPrivado = original.ReturnData.EsPrivado;
            funcion.ReturnData.Oper = original.ReturnData.Oper;
            funcion.ReturnData.TipoDato = original.ReturnData.TipoDato;
            if (original.ReturnData.Arreglo != null)
            {
                Arreglo arr = new Arreglo
                {
                    Dimension = original.ReturnData.Arreglo.Dimension,
                    SizeBi = original.ReturnData.Arreglo.SizeBi,
                    SizeUni = original.ReturnData.Arreglo.SizeUni,
                    SizeTri = original.ReturnData.Arreglo.SizeTri
                };
                funcion.ReturnData.Arreglo = arr;
            }
            if (original.ReturnData.Posicion != null)
            {
                Posicion pos = new Posicion(original.ReturnData.Posicion.Fila, original.ReturnData.Posicion.Columna);
                funcion.ReturnData.Posicion = pos;
            }
            ///Copia los simbolos-parametros de la función
            foreach (var lst01 in original.FuncParSym)
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
                funcion.FuncParSym.Add(lst01.Key.ToString(), symPrueba);
            }
            return funcion;
        }
    }
}
