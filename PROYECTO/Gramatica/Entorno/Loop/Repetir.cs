using Irony.Parsing;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Loop
{
    class Repetir : Funcion, IEntorno
    {
        public LinkedList<IEntorno> RepeatEnt { get; }
        public Dictionary<string, Simbolo> RepeatSym { get; }
        public ParseTreeNode RepeatTree { get; }

        public ParseTreeNode Iteracion { get; }

        public Repetir(ParseTreeNode subArbol, ParseTreeNode iteracion)
        {
            this.RepeatEnt = new LinkedList<IEntorno>();
            this.RepeatSym = new Dictionary<string, Simbolo>();
            this.RepeatTree = subArbol;
            this.Iteracion = iteracion;
        }
    }
}
