using PROYECTO.Gramatica.Entorno;
using System.Collections.Generic;
using Irony.Parsing;

namespace PROYECTO.Gramatica.Entorno.Condicional.Switch
{
    class Caso : Comprobar
    {
        public new LinkedList<IEntorno> SubEntornos { get; }
        public new Dictionary<string, Simbolo> Simbolos { get; }
        public new ParseTreeNode SubArbol { get; }

        public ParseTreeNode Condicion;

        public Caso(LinkedList<IEntorno> subentornos, Dictionary<string, Simbolo> simbolos, ParseTreeNode subarbol, ParseTreeNode variable)
        {
            this.SubEntornos = subentornos;
            this.Simbolos = simbolos;
            this.SubArbol = subarbol;
            this.Condicion = variable;
        }
    }
}
