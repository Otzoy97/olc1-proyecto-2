using Irony.Parsing;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Loop
{
    class Para : Funcion, IEntorno
    {
        public new LinkedList<IEntorno> SubEntornos { get; }
        public new Dictionary<string, Simbolo> Simbolos { get; }
        public new ParseTreeNode SubArbol { get; }

        public Dictionary<string, Simbolo> Variable { get; }
        public ParseTreeNode Condicion { get; }
        
        public Para(LinkedList<IEntorno> subentornos, Dictionary<string, Simbolo> simbolos, ParseTreeNode subArbol, Dictionary<string, Simbolo> variable, ParseTreeNode condicion)
        {
            this.SubEntornos = subentornos;
            this.Simbolos = simbolos;
            this.SubArbol = subArbol;
            this.Condicion = condicion;
            this.Variable = variable;
        }
    }
}
