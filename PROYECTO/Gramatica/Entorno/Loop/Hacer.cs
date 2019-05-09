using Irony.Parsing;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Loop
{
    class Hacer : Funcion, IEntorno
    {
        public LinkedList<IEntorno> DoEnt { get; }
        public Dictionary<string, Simbolo> DoSym { get; }
        public ParseTreeNode DoTree { get; }

        public ParseTreeNode DoCond { get; }


        public Hacer(ParseTreeNode acciones, ParseTreeNode condicion)
        {
            this.DoEnt = new LinkedList<IEntorno>();
            this.DoSym = new Dictionary<string, Simbolo>();
            this.DoTree = acciones;
            this.DoCond = condicion;
        }
    }
}
