using System.Windows.Forms;
using Irony.Parsing;

namespace PROYECTO.Gramatica.Entorno
{
    public enum Tipo
    {
        VOID, INT, STRING, DOUBLE, CHAR, BOOLEAN, CLASE, 
        INTARR, STRINGARR, DOUBLEARR, CHARARR, BOOLEANARR, CLASEARR
    }

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

    public class Simbolo
    {
        public Posicion Posicion { get; set; }
        private object dat;
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
        public Tipo TipoDato { get; set; }
        public bool EsPrivado { get; set; }

        public ParseTreeNode Arr { get; set; }
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
