using Irony.Parsing;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Loop
{
    class Para :  IEntorno
    {
        /// <summary>
        /// Diccionario de simbolos
        /// </summary>
        public Dictionary<string, Simbolo> ForSym { get; }
        /// <summary>
        /// Nodo de acciones
        /// </summary>
        public ParseTreeNode ForTree { get; }
        /// <summary>
        ///
        /// </summary>
        public Dictionary<string, Simbolo> ForVar { get; }
        /// <summary>
        /// 
        /// </summary>
        public ParseTreeNode ForCond { get; }
        /// <summary>
        /// Entorno inmediato superior
        /// </summary>
        private IEntorno EntornoPadre { get; set; }


        public Para(Dictionary<string, Simbolo> variable, ParseTreeNode acciones, ParseTreeNode condicion, IEntorno entornoPadre)
        {
            this.ForSym = new Dictionary<string, Simbolo>();
            this.ForTree = acciones;
            this.ForCond = condicion;
            this.ForVar = variable;
            this.EntornoPadre = entornoPadre;
        }
        /// <summary>
        /// Busca un simbolo en el diccionario local o en el diccionario del entorno padre
        /// </summary>
        /// <param name="nombreVar"></param>
        /// <returns></returns>
        public Simbolo BuscarSimbolo(string nombreVar)
        {
            return this.ForSym.ContainsKey(nombreVar) ? this.ForSym[nombreVar] : EntornoPadre.BuscarSimbolo(nombreVar);
        }

        public void Ejecutar()
        {

        }
    }
}
