using System.Windows.Forms;

namespace PROYECTO.Gramatica.Entorno
{
    public enum Tipo
    {
        INT, STRING, DOUBLE, CHAR, BOOLEAN, CLASE, 
        INTARR, STRINGARR, DOUBLEARR, CHARARR, BOOLEANARR, CLASEARR
    }

    public class Posicion
    {
        public int Fila { get; set; }
        public int Columna{get;set;}
        //public TabPage Pestania { get; set; }

        public Posicion(int fila, int columna/*, TabPage pestania*/)
        {
            this.Fila = fila;
            this.Columna = columna;
            //this.Pestania = pestania;
        }

    }

    public class Simbolo
    {
        public object dat;
        public Posicion Posicion { get; set; }
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="dato"></param>
        /// <param name="tipoDato"></param>
        public Simbolo(Posicion pos, object dato, bool esPrivado, Tipo tipoDato)
        {
            this.Posicion = pos;
            this.Dato = dato;
            this.TipoDato = tipoDato;
            this.EsPrivado = esPrivado;
        }
    }
}
