using Irony.Parsing;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Loop
{
    class Para : Funcion, IEntorno
    {
        public LinkedList<IEntorno> ForEnt { get; }
        public Dictionary<string, Simbolo> ForSym { get; }
        public ParseTreeNodeList ForTree { get; }

        public Dictionary<string, Simbolo> ForVar { get; }
        public ParseTreeNodeList ForCond { get; }

        public Para(Dictionary<string, Simbolo> variable, ParseTreeNodeList acciones, ParseTreeNodeList condicion)
        {
            this.ForEnt = new LinkedList<IEntorno>();
            this.ForSym = new Dictionary<string, Simbolo>();
            this.ForTree = acciones;
            this.ForCond = condicion;
            this.ForVar = variable;
        }
    }
}
