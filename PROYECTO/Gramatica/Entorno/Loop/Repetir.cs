using Irony.Parsing;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Loop
{
    class Repetir : IEntorno
    {
        /// <summary>
        /// Diccionario de simbolos local
        /// </summary>
        public Dictionary<string, Simbolo> RepeatSym { get; }
        /// <summary>
        /// Nodo de acciones
        /// </summary>
        public ParseTreeNode RepeatTree { get; }
        /// <summary>
        /// Nodo que especifica las iteraciones
        /// </summary>
        public ParseTreeNode Iteracion { get; }
        /// <summary>
        /// Entorno inmediato superior
        /// </summary>
        private IEntorno EntornoPadre { get; set; }


        public Repetir(ParseTreeNode subArbol, ParseTreeNode iteracion, IEntorno entornoPadre)
        {
            this.RepeatSym = new Dictionary<string, Simbolo>();
            this.RepeatTree = subArbol;
            this.Iteracion = iteracion;
            this.EntornoPadre = entornoPadre;
        }
        /// <summary>
        /// Busca un simbolo en el diccionario local o en el diccionario del entorno padre
        /// </summary>
        /// <param name="nombreVar"></param>
        /// <returns></returns>
        public Simbolo BuscarSimbolo(string nombreVar)
        {
            return this.RepeatSym.ContainsKey(nombreVar) ? this.RepeatSym[nombreVar] : EntornoPadre.BuscarSimbolo(nombreVar);
        }
        public void Ejecutar()
        {

        }
    }
}
