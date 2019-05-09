﻿using PROYECTO.Gramatica.Entorno;
using System.Collections.Generic;
using Irony.Parsing;

namespace PROYECTO.Gramatica.Entorno.Condicional.Switch
{
    class Caso : Comprobar
    {
        public LinkedList<IEntorno> CaseEnt { get; }
        public Dictionary<string, Simbolo> CaseSym { get; }
        public ParseTreeNode CaseTree { get; }

        public ParseTreeNode Condicion;

        public Caso(ParseTreeNode acciones, ParseTreeNode variable)
        {
            this.CaseEnt = new LinkedList<IEntorno>();
            this.CaseSym = new Dictionary<string, Simbolo>();
            this.CaseTree = acciones;
            this.Condicion = variable;
        }
    }
}
