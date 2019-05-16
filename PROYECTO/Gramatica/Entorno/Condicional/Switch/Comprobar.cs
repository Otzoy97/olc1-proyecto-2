using Irony.Parsing;
using PROYECTO.Gramatica.Acciones.Operaciones;
using System;
using System.Collections.Generic;

namespace PROYECTO.Gramatica.Entorno.Condicional.Switch
{
    class Comprobar: IEntorno
    {
        /// <summary>
        /// Especifica la variable a comprobar
        /// </summary>
        public ParseTreeNode Variable { get; }
        /// <summary>
        /// Determina el entornoPadre superior
        /// </summary>
        public IEntorno EntornoPadre { get; set; }
        /// <summary>
        /// Especifica la colección de casos
        /// </summary>
        public ParseTreeNode CaseNode { get; }
        
        private ParseTreeNode AuxDefault { get; set; }
        
        public Comprobar(ParseTreeNode variable, ParseTreeNode casos, IEntorno padre)
        {
            this.CaseNode = casos;
            this.Variable = variable;
            this.EntornoPadre = padre;
        }

        public bool Ejecutar()
        {
            bool retorno = false;
            //Servirá para operar todo
            Operar operar = new Operar(this, BuscarClasePadre());
            //Operara el caseNodo{
            var RefComprobar = operar.Interpretar(CaseNode);
            //Verifica que no sea nulo, y que sea un int , char, bool, double, string
            if (RefComprobar.TipoDato == Tipo.VOID || RefComprobar.Dato == null || RefComprobar.TipoDato >= Tipo.CLASE)
            {
                Main.Imprimir(
                    String.Format(
                        "Se esperaba un INT, CHAR, BOOL, DOUBLE o STRING, se recibió {0}, ({1},{2})",
                        RefComprobar.TipoDato, 
                        RefComprobar.Posicion != null ? RefComprobar.Posicion.Fila : 0,
                        RefComprobar.Posicion != null ? RefComprobar.Posicion.Columna : 0
                    ));
            }
            //Es un dato válido
            //Recorre el nodo de acciones
            try
            {
                //Decide si se debe operar el nodo de default
                bool OperarDefault = true;
                foreach (var nodeTree in CaseNode.ChildNodes)
                {
                    var ramaAux = nodeTree.ChildNodes;
                    //Pasa por "primera vez"
                    switch (ramaAux.Count)
                    {
                        case 1:
                            //Es un defecto
                            //Agrega la referncia en auxDefault
                            auxDefault = ramaAux[0];
                            break;
                        case 2:
                            //Opera el nodo 0
                            var SimCase = operar.Interpretar(ramaAux[0]);
                            //Verifica que el nodo interpretado sea del mismo tipo que el de la referencia
                            if (RefComprobar.TipoDato != SimCase.TipoDato || SimCase.Dato == null)
                            {
                                Main.Imprimir(
                                    String.Format(
                                        "Se esperaba un {0}, se recibió {3}, ({1},{2})",
                                        RefComprobar.TipoDato,
                                        SimCase.Posicion != null ? SimCase.Posicion.Fila : 0,
                                        SimCase.Posicion != null ? SimCase.Posicion.Columna : 0,
                                        SimCase.TipoDato
                                ));
                            }
                            //Decide si los datos coinciden
                            if (RefComprobar.Dato.Equals(SimCase.Dato))
                            {
                                //Crea un nuevo "caso" y lo ejecuta
                                OperarDefault = !new Caso(ramaAux[1], this).Ejecutar();
                            }
                            break;
                    }
                    if (!OperarDefault)
                    {
                        break;
                    }
                }
                if (OperarDefault)
                {
                    //Crea un nuevo "caso" y lo ejecuta
                    new Caso(auxDefault, this).Ejecutar();
                }
            }
            catch (Exception e)
            {
                Main.Imprimir(String.Format("Excepción en tiempo de ejecución: {0} -> {1}", e.Message, e.Source));
                retorno = true;
            }
            return retorno;
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
        /// <summary>
        /// Busca en los simbolos del padre superior
        /// </summary>
        /// <param name="nombreVar"></param>
        /// <returns></returns>
        public Simbolo BuscarSimbolo(string nombreVar)
        {
            return EntornoPadre.BuscarSimbolo(nombreVar);
        }

    }
}
