using System.Windows.Forms;

namespace PROYECTO.Gramatica.Simbolo
{
    public enum Tipo
    {
        INT, STRING, DOUBLE, CHAR, BOOLEAN, CLASE
    }

    public class Posicion
    {
        public int Fila { get; set; }
        public int Columna{get;set;}
        public TabPage Pestania { get; set; }

        public Posicion(int fila, int columna, TabPage pestania)
        {
            this.Fila = fila;
            this.Columna = columna;
            this.Pestania = pestania;
        }

    }

    public class Symbol
    {
        public Posicion Posicion { get; set; }
        public object Dato { get; set; }
        public Tipo TipoDato { get; set; }

        Symbol(Posicion pos, object dato, Tipo tipoDato)
        {
            this.Posicion = pos;
            this.Dato = dato;
            this.TipoDato = tipoDato;
        }
    }
}
