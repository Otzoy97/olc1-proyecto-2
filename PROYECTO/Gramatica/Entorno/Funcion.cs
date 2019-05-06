using Irony.Parsing;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno
{
    class Funcion : Clase, IEntorno
    {
        public new LinkedList<IEntorno> SubEntornos { get; }
        public new Dictionary<string, Simbolo> Simbolos { get; }
        public ParseTreeNode SubArbol { get; }
        public bool EsPrivado { get; set; }
        public Funcion()
        {
            SubEntornos = null;
            Simbolos = null;
            SubArbol = null;
        }

        public Funcion(LinkedList<IEntorno> subentornos, Dictionary<string, Simbolo> simbolos, ParseTreeNode subArbol)
        {
            this.SubEntornos = subentornos;
            this.Simbolos = simbolos;
            this.SubArbol = subArbol;
        }
    }
}
