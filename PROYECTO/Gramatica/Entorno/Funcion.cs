using Irony.Parsing;
using PROYECTO.Gramatica.Simbolo;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno
{
    class Funcion : Clase, IEntorno
    {

        public new LinkedList<IEntorno> SubEntornos { get; }
        public new Dictionary<string, Symbol> Simbolos { get; }
        public ParseTreeNode SubArbol { get; }

        public Funcion()
        {
            SubEntornos = null;
            Simbolos = null;
            SubArbol = null;
        }

        public Funcion(LinkedList<IEntorno> subentornos, Dictionary<string, Symbol> simbolos, ParseTreeNode subArbol)
        {
            this.SubEntornos = subentornos;
            this.Simbolos = simbolos;
            this.SubArbol = subArbol;
        }
    }
}
