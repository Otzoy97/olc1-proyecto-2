using Irony.Parsing;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Loop
{
    class Mientras: Funcion, IEntorno
    {
        public LinkedList<IEntorno> WhileEnt { get; }
        public Dictionary<string, Simbolo> WhileSym { get; }
        public ParseTreeNode WhileTree { get; }

        public ParseTreeNode WhileCond { get; }

        public Mientras(ParseTreeNode acciones, ParseTreeNode condicion)
        {
            this.WhileEnt = new LinkedList<IEntorno>();
            this.WhileSym = new Dictionary<string, Simbolo>();
            this.WhileTree = acciones;
            this.WhileCond = condicion;
        }
    }
}
