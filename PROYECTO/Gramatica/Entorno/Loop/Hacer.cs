using Irony.Parsing;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Loop
{
    class Hacer : Funcion, IEntorno
    {
        public LinkedList<IEntorno> DoEnt { get; }
        public Dictionary<string, Simbolo> DoSym { get; }
        public ParseTreeNodeList DoTree { get; }

        public ParseTreeNodeList DoCond { get; }


        public Hacer(ParseTreeNodeList acciones, ParseTreeNodeList condicion)
        {
            this.DoEnt = new LinkedList<IEntorno>();
            this.DoSym = new Dictionary<string, Simbolo>();
            this.DoTree = acciones;
            this.DoCond = condicion;
        }
    }
}
