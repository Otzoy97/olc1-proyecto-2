using Irony.Parsing;
using PROYECTO.Gramatica.Simbolo;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Loop
{
    class Repetir : Funcion, IEntorno
    {
        public new LinkedList<IEntorno> SubEntornos { get; }
        public new Dictionary<string, Symbol> Simbolos { get; }
        public new ParseTreeNode SubArbol { get; }

        public Dictionary<string, Symbol> Iteracion { get; }

        public Repetir(LinkedList<IEntorno> subentornos, Dictionary<string, Symbol> simbolos, ParseTreeNode subArbol, Dictionary<string, Symbol> iteracion)
        {
            this.SubEntornos = subentornos;
            this.Simbolos = simbolos;
            this.SubArbol = subArbol;
            this.Iteracion = iteracion;
        }
    }
}
