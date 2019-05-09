using Irony.Parsing;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno
{
    class Funcion : Clase, IEntorno
    {
        public LinkedList<IEntorno> FuncEnt { get; }
        public Dictionary<string, Simbolo> FuncSym { get; }
        public ParseTreeNode FuncTree { get; }
        public bool EsPrivado { get; set; }

        public Funcion()
        {
            FuncEnt = new LinkedList<IEntorno>();
            FuncSym = new Dictionary<string, Simbolo>();
            EsPrivado = false;
            FuncTree = null;
        }
        /*
        public Funcion(bool esprivado, LinkedList<IEntorno> subentornos, ParseTreeNode subArbol)
        {
            this.FuncEnt = subentornos;
            this.FuncSym = simbolos;
            this.FuncTree = subArbol;
            this.EsPrivado = esprivado;
        }*/
    }
}
