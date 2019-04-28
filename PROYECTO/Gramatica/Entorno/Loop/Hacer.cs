using Irony.Parsing;
using PROYECTO.Gramatica.Simbolo;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Loop
{
    class Hacer : Funcion, IEntorno
    {
        public new LinkedList<IEntorno> SubEntornos { get; }
        public new Dictionary<string, Symbol> Simbolos { get; }
        public new ParseTreeNode SubArbol { get; }

        public Dictionary<string, Symbol> Condicion { get; }


        public Hacer(LinkedList<IEntorno> subentornos, Dictionary<string, Symbol> simbolos, ParseTreeNode subArbol, Dictionary<string, Symbol> condicion)
        {
            this.SubEntornos = subentornos;
            this.Simbolos = simbolos;
            this.SubArbol = subArbol;
            this.Condicion = condicion;
        }
    }
}
