using Irony.Parsing;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Loop
{
    class Para : Funcion, IEntorno
    {
        public LinkedList<IEntorno> ForEnt { get; }
        public Dictionary<string, Simbolo> ForSym { get; }
        public ParseTreeNode ForTree { get; }

        public Dictionary<string, Simbolo> ForVar { get; }
        public ParseTreeNode ForCond { get; }

        public Para(Dictionary<string, Simbolo> variable, ParseTreeNode acciones, ParseTreeNode condicion)
        {
            this.ForEnt = new LinkedList<IEntorno>();
            this.ForSym = new Dictionary<string, Simbolo>();
            this.ForTree = acciones;
            this.ForCond = condicion;
            this.ForVar = variable;
        }
    }
}
