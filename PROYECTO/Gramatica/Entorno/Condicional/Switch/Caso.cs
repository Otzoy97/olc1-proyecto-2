using PROYECTO.Gramatica.Simbolo;
using System.Collections.Generic;
using Irony.Parsing;

namespace PROYECTO.Gramatica.Entorno.Condicional.Switch
{
    class Caso : Comprobar
    {
        public new LinkedList<IEntorno> SubEntornos { get; }
        public new Dictionary<string,Symbol> Simbolos { get; }
        public new ParseTreeNode SubArbol { get; }

        public Dictionary<string, Symbol> Condicion;

        public Caso(LinkedList<IEntorno> subentornos, Dictionary<string,Symbol> simbolos, ParseTreeNode subarbol, Dictionary<string, Symbol> variable)
        {
            this.SubEntornos = subentornos;
            this.Simbolos = simbolos;
            this.SubArbol = subarbol;
            this.Condicion = variable;
        }
    }
}
