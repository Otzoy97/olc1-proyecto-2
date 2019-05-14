using Irony.Parsing;
using System;

namespace PROYECTO.Gramatica.Entorno
{
    [Serializable]
    public enum Tipo
    {
        VOID, INT, STRING, DOUBLE, CHAR, BOOLEAN, CLASE, 
        INTARR, STRINGARR, DOUBLEARR, CHARARR, BOOLEANARR, CLASEARR
    }
    [Serializable]
    public class Posicion
    {
        public int Fila { get; set; }
        public int Columna{get;set;}

        public Posicion(int fila, int columna)
        {
            this.Fila = fila;
            this.Columna = columna;
        }

    }
    [Serializable]
    public class Arreglo
    {
        public TipoArreglo Dimension { get; set; }
        public int SizeUni { get; set; }
        public int SizeBi { get; set; }
        public int SizeTri { get; set; }
    }
    [Serializable]
    public enum TipoArreglo
    {
        UNI, BI, TRI
    }
    [Serializable]
    public class Simbolo
    {
        /// <summary>
        /// Determina la posición del símbolo
        /// </summary>
        public Posicion Posicion { get; set; }
        /// <summary>
        /// Guarda el dato del símbolo
        /// </summary>
        private object dat;
        /// <summary>
        /// Si el simbolo es un arreglo, acá se guardan sus dimensiones y su tamaño
        /// </summary>
        public Arreglo Arreglo { get; set; }
        /// <summary>
        /// Encapsula el atributo dat
        /// </summary>
        public object Dato
        {
            get
            {
                if (!EsPrivado)
                {
                    return this.dat;
                }
                return null;
            }
            set
            {
                if (!EsPrivado)
                {
                    dat = value;
                }
            }
        }
        /// <summary>
        /// Determina el tipo de dato que guarda el símbolo
        /// </summary>
        public Tipo TipoDato { get; set; }
        /// <summary>
        /// Específica la visibilidad del símbolo
        /// </summary>
        public bool EsPrivado { get; set; }
        /// <summary>
        /// Almacena el AST de las dimensiones del arreglo
        /// </summary>
        public ParseTreeNode Arr { get; set; }
        /// <summary>
        /// Almacena el AST del contenido del Simbolo
        /// </summary>
        public ParseTreeNode Oper { get; set; }
        /// <summary>
        /// Declaracion/asignación simple
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="dato"></param>
        /// <param name="tipoDato"></param>
        public Simbolo(Posicion pos, bool esPrivado, ParseTreeNode oper, Tipo tipoDato)
        {
            this.Posicion = pos;
            this.TipoDato = tipoDato;
            this.EsPrivado = esPrivado;
            this.Oper = oper;
            this.Dato = null;
            this.Arr = null;
        }
        /// <summary>
        /// Declaración/asignación array
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="esPrivado"></param>
        /// <param name="oper"></param>
        /// <param name="arr"></param>
        /// <param name="tipoDato"></param>
        public Simbolo(Posicion pos, bool esPrivado, ParseTreeNode oper, ParseTreeNode arr, Tipo tipoDato)
        {
            this.Posicion = pos;
            this.TipoDato = tipoDato;
            this.EsPrivado = esPrivado;
            this.Oper = oper;
            this.Arr = arr;
            this.Dato = null;
        }
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Simbolo()
        {
            this.Posicion = null;
            this.TipoDato = Tipo.VOID;
            this.EsPrivado = false;
            this.Oper = null;
            this.Arr = null;
            this.Dato = null;
        }
    }
}
