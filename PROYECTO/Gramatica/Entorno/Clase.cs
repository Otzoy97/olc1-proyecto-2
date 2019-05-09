using System.Collections.Generic;
using Irony.Parsing;

namespace PROYECTO.Gramatica.Entorno
{
    class Clase
    { 
        /// <summary>
        /// Almacenará todo los subentorno (funciones/metodos) de la clase
        /// </summary>
        public Dictionary<string, Funcion> ClaseEnt { get; set; }
        /// <summary>
        /// Almacenará todo tipo de dato que se declare (int,string,bool,char,double,clases)
        /// </summary>
        public Dictionary<string, Simbolo> ClaseSym { get; set; }
        /// <summary>
        /// Almacenará cualquier otras clases que se hayan importado
        /// -Esto se especificará en la segunda pasada-
        /// </summary>
        public Dictionary<string, Clase> ClaseImp { get; set; }
        /// <summary>
        /// Servirá de auxilio para determinar cuales son las clases que se hayan importado
        /// -Servirá en la primera pasada-
        /// </summary>
        public ParseTreeNode ClaseImpTree { get; set; }
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Clase()
        {
            ClaseEnt = new Dictionary<string, Funcion>();
            ClaseSym = new Dictionary<string, Simbolo>();
            ClaseImp = null;
            ClaseImpTree = null;
        }
        /// <summary>
        /// Constructor que acepta un diccionario de fnciones y simbolos
        /// </summary>
        /// <param name="subentornos"></param>
        /// <param name="simbolos"></param>
        /*public Clase(Dictionary<string, Funcion> subentornos, Dictionary<string, Symbol> simbolos)
        {
            this.SubEntornos = subentornos;
            this.Simbolos = simbolos;
            this.Imports = null;
            this.SubArbolImports = null;
        }*/
        /// <summary>
        /// Construcor que acepta un diccionario de funciones y simbolos
        /// Además guarda el arbol que especifica los imports para esta clase en particular
        /// </summary>
        /// <param name="subentornos"></param>
        /// <param name="simbolos"></param>
        /// <param name="subArbol"></param>
        /*public Clase(Dictionary<string, Funcion> subentornos, Dictionary<string, Symbol> simbolos, ParseTreeNode subArbol )
        {
            this.SubEntornos = subentornos;
            this.Simbolos = simbolos;
            this.Imports = null;
            this.SubArbolImports = subArbol;
        }*/
    }
}
