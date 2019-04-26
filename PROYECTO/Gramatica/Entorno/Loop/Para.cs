using Irony.Parsing;
using PROYECTO.Gramatica.Simbolo;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Loop
{
    class Para : Funcion, IEntorno
    {
        public new LinkedList<IEntorno> SubEntornos { get; }
        public new Dictionary<string, Symbol> Simbolos { get; }
        public new ParseTreeNode SubArbol { get; }

        public Para(LinkedList<IEntorno> subentornos, Dictionary<string, Symbol> simbolos, ParseTreeNode subArbol)
        {
            this.SubEntornos = subentornos;
            this.Simbolos = simbolos;
            this.SubArbol = subArbol;
        }
    }
}
