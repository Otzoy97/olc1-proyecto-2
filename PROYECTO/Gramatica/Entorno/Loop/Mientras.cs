using Irony.Parsing;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Loop
{
    class Mientras: Funcion, IEntorno
    {
        public new LinkedList<IEntorno> SubEntornos { get; }
        public new Dictionary<string, Simbolo> Simbolos { get; }
        public new ParseTreeNode SubArbol { get; }

        public ParseTreeNode Condicion { get; }

        public Mientras(LinkedList<IEntorno> subentornos, Dictionary<string, Simbolo> simbolos, ParseTreeNode subArbol, ParseTreeNode condicion)
        {
            this.SubEntornos = subentornos;
            this.Simbolos = simbolos;
            this.SubArbol = subArbol;
            this.Condicion = condicion;
        }
    }
}
