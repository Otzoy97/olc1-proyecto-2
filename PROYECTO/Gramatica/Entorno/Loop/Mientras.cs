using Irony.Parsing;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Loop
{
    class Mientras: IEntorno
    {
        /// <summary>
        /// Diccionario de simbolos locales
        /// </summary>
        public Dictionary<string, Simbolo> WhileSym { get; }
        /// <summary>
        /// Nodo de acciones
        /// </summary>
        public ParseTreeNode WhileTree { get; }
        /// <summary>
        /// Nodo de condicion
        /// </summary>
        public ParseTreeNode WhileCond { get; }
        /// <summary>
        /// Entorno inmediato superior
        /// </summary>
        private IEntorno EntornoPadre { get; set; }


        public Mientras(ParseTreeNode acciones, ParseTreeNode condicion, IEntorno entornoPadre)
        {
            this.WhileSym = new Dictionary<string, Simbolo>();
            this.WhileTree = acciones;
            this.WhileCond = condicion;
            this.EntornoPadre = entornoPadre;
        }
        /// <summary>
        /// Busca un simbolo en el diccionario local o en el diccionario del entorno padre
        /// </summary>
        /// <param name="nombreVar"></param>
        /// <returns></returns>
        public Simbolo BuscarSimbolo(string nombreVar)
        {
            return this.WhileSym.ContainsKey(nombreVar) ? this.WhileSym[nombreVar] : EntornoPadre.BuscarSimbolo(nombreVar);
        }

        public void Ejecutar()
        {

        }
    }
}
