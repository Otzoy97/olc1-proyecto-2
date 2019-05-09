using Irony.Parsing;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Condicional
{
    class Si : Funcion, IEntorno
    {
        public LinkedList<IEntorno> IfEnt { get; }
        public Dictionary<string, Simbolo> IfSym { get; }
        public ParseTreeNode IfTree { get; }

        public LinkedList<IEntorno> ElseEnt { get; }
        public Dictionary<string, Simbolo> ElseSym { get; }
        public ParseTreeNode ElseTree { get; }

        public ParseTreeNode Condicion { get; }

        public Si(ParseTreeNode condicion, ParseTreeNode accionesTrue)
        {
            this.IfEnt = new LinkedList<IEntorno>();
            this.IfSym = new Dictionary<string, Simbolo>();
            this.IfTree = accionesTrue;
            this.Condicion = condicion;
        }
        public Si(ParseTreeNode condicion, ParseTreeNode accionesTrue, ParseTreeNode accionesFalse)
        {
            this.IfEnt = new LinkedList<IEntorno>();
            this.IfSym = new Dictionary<string, Simbolo>();
            this.IfTree = accionesTrue;
            this.Condicion = condicion;
            this.ElseEnt = new LinkedList<IEntorno>();
            this.ElseSym = new Dictionary<string, Simbolo>();
            this.ElseTree = accionesFalse;
        }
    }
}
