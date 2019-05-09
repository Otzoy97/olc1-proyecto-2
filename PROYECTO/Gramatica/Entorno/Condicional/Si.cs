using Irony.Parsing;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Condicional
{
    class Si : Funcion, IEntorno
    {
        public LinkedList<IEntorno> IfEnt { get; }
        public Dictionary<string, Simbolo> IfSym { get; }
        public ParseTreeNodeList IfTree { get; }

        public LinkedList<IEntorno> ElseEnt { get; }
        public Dictionary<string, Simbolo> ElseSym { get; }
        public ParseTreeNodeList ElseTree { get; }

        public ParseTreeNodeList Condicion { get; }

        public Si(ParseTreeNodeList condicion, ParseTreeNodeList accionesTrue)
        {
            this.IfEnt = new LinkedList<IEntorno>();
            this.IfSym = new Dictionary<string, Simbolo>();
            this.IfTree = subarboltrue;
            this.Condicion = condicion;
        }
        public Si(ParseTreeNodeList condicion, ParseTreeNodeList accionesTrue, ParseTreeNodeList accionesFalse)
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
