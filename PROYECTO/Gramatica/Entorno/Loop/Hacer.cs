using Irony.Parsing;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Loop
{
    class Hacer : IEntorno
    {
        /// <summary>
        /// Diccionario de simbolos locales
        /// </summary>
        public Dictionary<string, Simbolo> DoSym { get; }
        /// <summary>
        /// Nodo de acciones
        /// </summary>
        public ParseTreeNode DoTree { get; }
        /// <summary>
        /// Nodo condición del Loop
        /// </summary>
        public ParseTreeNode DoCond { get; }
        /// <summary>
        /// Entorno inmediato superior
        /// </summary>
        private IEntorno EntornoPadre { get; set; }

        public Hacer(ParseTreeNode acciones, ParseTreeNode condicion, IEntorno entornoPadre)
        {
            this.DoSym = new Dictionary<string, Simbolo>();
            this.DoTree = acciones;
            this.DoCond = condicion;
            this.EntornoPadre = entornoPadre;
        }
        /// <summary>
        /// Busca un simbolo en el diccionario local o en el diccionario del entorno padre
        /// </summary>
        /// <param name="nombreVar"></param>
        /// <returns></returns>
        public Simbolo BuscarSimbolo(string nombreVar)
        {
            return this.DoSym.ContainsKey(nombreVar) ? this.DoSym[nombreVar] : EntornoPadre.BuscarSimbolo(nombreVar);
        }

        public bool Ejecutar()
        {
            return false;
        }
        /// <summary>
        /// Busca la clase padre
        /// </summary>
        /// <returns></returns>
        public Clase BuscarClasePadre()
        {
            return EntornoPadre.BuscarClasePadre();
        }
        /// <summary>
        /// Busca la función padre
        /// </summary>
        /// <returns></returns>
        public Funcion BuscarFuncionPadre()
        {
            return EntornoPadre.BuscarFuncionPadre();
        }

        /// <summary>
        /// Devuelve si algun padre superior es un loop
        /// </summary>
        /// <returns></returns>
        public bool EsLoop()
        {
            return true;
        }
    }
}
