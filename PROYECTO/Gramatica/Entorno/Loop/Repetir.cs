using Irony.Parsing;
using System.Collections.Generic;
using System;
using PROYECTO.Gramatica.Acciones.Operaciones;
using PROYECTO.Gramatica.Acciones;
using PROYECTO.Gramatica.Entorno.Condicional;

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
        /// <summary>
        /// Ejecuta las acciones específicadas en el Nodo de acciones
        /// </summary>
        public bool Ejecutar()
        {
            bool retorno = false;
            //Servirá para operar todo
            Operar operar = new Operar(this,BuscarClasePadre());
            //Opera el nodo de iteracion
            Simbolo RepeatIteracion = operar.Interpretar(this.Iteracion);
            //Se asegura que sea un entero
            if (RepeatIteracion.TipoDato != Tipo.INT)
            {
                Main.Imprimir(String.Format("Repetir necesita un entero: ({0},{1})",
                    RepeatIteracion.Posicion != null ? RepeatIteracion.Posicion.Fila : 0,
                    RepeatIteracion.Posicion != null ? RepeatIteracion.Posicion.Columna : 0));
                return true;
            }
            //Procede a ejecutar el nodo las n veces que se determinó anteriormente
            try
            {
                for (int K = 0; K < ((int)RepeatIteracion.Dato); K++)
                {
                    //Recorre el nodo de acciones
                    foreach (var nodeTree in RepeatTree.ChildNodes)
                    {
                        var ramaAux = nodeTree.ChildNodes;
                        //Según el nombre del nodo así serán las acciones
                        switch (nodeTree.Term.Name)
                        {
                            case "DECLARACION":
                                #region DECLARACION
                                //Obtiene la posición de la variable
                                Posicion pos = new Posicion(ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1);
                                //El primer token es tkVISIBLE? entonces verifica que el contenido
                                if (ramaAux[0].Term.Name.ToString().Equals("tkVISIBLE"))
                                {
                                    //Elimina el nodo , pues no es de utilidad
                                    ramaAux.RemoveAt(0);
                                    //Realiza una advertencia
                                    Main.Imprimir(String.Format("**Advertencia** Sentencia fuera de entorno -{0}- ; ({1},{2})", ramaAux[0].Token.Text.ToLower(), ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                }
                                //Obtiene el tipo de dato
                                Tipo dataType = Funcion.GetTipoDato(ramaAux);
                                //Es un dato plano
                                if (dataType >= Tipo.INT && dataType <= Tipo.CLASE)
                                {
                                    foreach (var varlst in ramaAux[1].ChildNodes)
                                    {
                                        //Verifica que la variable no exista
                                        if (RepeatSym.ContainsKey(varlst.Token.Text.ToLower()))
                                        {
                                            Main.Imprimir(String.Format("La variable {0} ya ha sido declarada en este entorno: ({1},{2})", varlst.Token.Text.ToLower(), varlst.Token.Location.Line + 1, varlst.Token.Location.Column + 1));
                                            return false;
                                        }
                                        //Determina si hay algo que operar
                                        if (ramaAux.Count != 2)
                                        {
                                            //Simplemente crea un simbolo y lo agrega al diccionario
                                            this.RepeatSym.Add(varlst.Token.Text.ToLower(), new Simbolo(pos, false, null, dataType));
                                        }
                                        else
                                        {
                                            //Opera el oper
                                            Simbolo operSym = operar.Interpretar(ramaAux[2]);
                                            //Verifica que los tipos coincidan
                                            if (operSym.TipoDato != dataType)
                                            {
                                                Main.Imprimir(String.Format("Los tipos no coinciden {0}. No se puede asignar un {3} a un {4} : ({1},{2})", varlst.Token.Text.ToLower(), varlst.Token.Location.Line + 1, varlst.Token.Location.Column + 1, operSym.TipoDato, dataType));
                                                return false;
                                            }
                                            //Asigna todos los atributos al simbolo que se agregará al diccionario
                                            Simbolo ArrSym = new Simbolo
                                            {
                                                Arreglo = null,
                                                Dato = operSym.Dato,
                                                TipoDato = dataType,
                                                Posicion = pos,
                                                EsPrivado = false,
                                                Arr = null,
                                                Oper = ramaAux[2]
                                            };
                                            //Añade el simbolo al entorno local
                                            RepeatSym.Add(varlst.Token.Text.ToLower(), ArrSym); Main.Imprimir(String.Format("La variable {0} ya ha sido declarada en este entorno: ({1},{2})", varlst.Token.Text.ToLower(), varlst.Token.Location.Line + 1, varlst.Token.Location.Column + 1));
                                        }
                                    }
                                }
                                //Es un ARRAY
                                if (dataType >= Tipo.INTARR && dataType <= Tipo.CLASEARR)
                                {
                                    if (ramaAux[2].ChildNodes.Count > 3)
                                    {
                                        Main.Imprimir(String.Format("Esta versión no soporta arreglos de más de tres dimensiones ({0},{1})", pos.Fila, pos.Columna));
                                        return false;
                                    }
                                    //Guarda el array
                                    foreach (var varlst in ramaAux[1].ChildNodes)
                                    {
                                        //Verifica que la variable no exista
                                        if (RepeatSym.ContainsKey(varlst.Token.Text.ToLower()))
                                        {
                                            Main.Imprimir(String.Format("La variable {0} ya ha sido declarada en este entorno: ({1},{2})", varlst.Token.Text.ToLower(), varlst.Token.Location.Line + 1, varlst.Token.Location.Column + 1));
                                            return false;
                                        }
                                        //Opera las dimensiones
                                        Simbolo dimArrSym = operar.Interpretar(ramaAux[2]);
                                        //Determina si se opera o no el OPER
                                        if (ramaAux.Count != 3)
                                        {
                                            //Inicializa el dato
                                            object arrDato = null;
                                            //Según las dimensiones
                                            switch (dimArrSym.Arreglo.Dimension)
                                            {
                                                case TipoArreglo.UNI:
                                                    switch (dataType)
                                                    {
                                                        case Tipo.INTARR:
                                                            arrDato = new int[dimArrSym.Arreglo.SizeUni];
                                                            break;
                                                        case Tipo.STRINGARR:
                                                            arrDato = new string[dimArrSym.Arreglo.SizeUni];
                                                            break;
                                                        case Tipo.DOUBLEARR:
                                                            arrDato = new double[dimArrSym.Arreglo.SizeUni];
                                                            break;
                                                        case Tipo.CHARARR:
                                                            arrDato = new char[dimArrSym.Arreglo.SizeUni];
                                                            break;
                                                        case Tipo.BOOLEANARR:
                                                            arrDato = new bool[dimArrSym.Arreglo.SizeUni];
                                                            break;
                                                        case Tipo.CLASEARR:
                                                            arrDato = new Clase[dimArrSym.Arreglo.SizeUni];
                                                            break;
                                                    }
                                                    break;
                                                case TipoArreglo.BI:
                                                    switch (dataType)
                                                    {
                                                        case Tipo.INTARR:
                                                            arrDato = new int[dimArrSym.Arreglo.SizeBi][];
                                                            //recorre el arreglo inicializando el arreglo
                                                            for (int i = 0; i < (arrDato as int[][]).Length; i++)
                                                            {
                                                                (arrDato as int[][])[i] = new int[dimArrSym.Arreglo.SizeUni];
                                                            }
                                                            break;
                                                        case Tipo.STRINGARR:
                                                            arrDato = new string[dimArrSym.Arreglo.SizeBi][];
                                                            for (int i = 0; i < (arrDato as string[][]).Length; i++)
                                                            {
                                                                (arrDato as string[][])[i] = new string[dimArrSym.Arreglo.SizeUni];
                                                            }
                                                            break;
                                                        case Tipo.DOUBLEARR:
                                                            arrDato = new double[dimArrSym.Arreglo.SizeBi][];
                                                            for (int i = 0; i < (arrDato as double[][]).Length; i++)
                                                            {
                                                                (arrDato as double[][])[i] = new double[dimArrSym.Arreglo.SizeUni];
                                                            }
                                                            break;
                                                        case Tipo.CHARARR:
                                                            arrDato = new char[dimArrSym.Arreglo.SizeBi][];
                                                            for (int i = 0; i < (arrDato as char[][]).Length; i++)
                                                            {
                                                                (arrDato as char[][])[i] = new char[dimArrSym.Arreglo.SizeUni];
                                                            }
                                                            break;
                                                        case Tipo.BOOLEANARR:
                                                            arrDato = new bool[dimArrSym.Arreglo.SizeBi][];
                                                            for (int i = 0; i < (arrDato as bool[][]).Length; i++)
                                                            {
                                                                (arrDato as bool[][])[i] = new bool[dimArrSym.Arreglo.SizeUni];
                                                            }
                                                            break;
                                                        case Tipo.CLASEARR:
                                                            arrDato = new Clase[dimArrSym.Arreglo.SizeBi][];
                                                            for (int i = 0; i < (arrDato as Clase[][]).Length; i++)
                                                            {
                                                                (arrDato as Clase[][])[i] = new Clase[dimArrSym.Arreglo.SizeUni];
                                                            }
                                                            break;
                                                    }
                                                    break;
                                                case TipoArreglo.TRI:
                                                    switch (dataType)
                                                    {
                                                        case Tipo.INTARR:
                                                            arrDato = new int[dimArrSym.Arreglo.SizeTri][][];
                                                            for (int i = 0; i < dimArrSym.Arreglo.SizeTri; i++)
                                                            {
                                                                (arrDato as int[][][])[i] = new int[dimArrSym.Arreglo.SizeBi][];
                                                                for (int j = 0; j < dimArrSym.Arreglo.SizeBi; i++)
                                                                {
                                                                    (arrDato as int[][][])[i][j] = new int[dimArrSym.Arreglo.SizeUni];
                                                                }
                                                            }
                                                            break;
                                                        case Tipo.STRINGARR:
                                                            arrDato = new string[dimArrSym.Arreglo.SizeTri][][];
                                                            for (int i = 0; i < dimArrSym.Arreglo.SizeTri; i++)
                                                            {
                                                                (arrDato as string[][][])[i] = new string[dimArrSym.Arreglo.SizeBi][];
                                                                for (int j = 0; j < dimArrSym.Arreglo.SizeBi; i++)
                                                                {
                                                                    (arrDato as string[][][])[i][j] = new string[dimArrSym.Arreglo.SizeUni];
                                                                }
                                                            }
                                                            break;
                                                        case Tipo.DOUBLEARR:
                                                            arrDato = new double[dimArrSym.Arreglo.SizeTri][][];
                                                            for (int i = 0; i < dimArrSym.Arreglo.SizeTri; i++)
                                                            {
                                                                (arrDato as double[][][])[i] = new double[dimArrSym.Arreglo.SizeBi][];
                                                                for (int j = 0; j < dimArrSym.Arreglo.SizeBi; i++)
                                                                {
                                                                    (arrDato as double[][][])[i][j] = new double[dimArrSym.Arreglo.SizeUni];
                                                                }
                                                            }
                                                            break;
                                                        case Tipo.CHARARR:
                                                            arrDato = new char[dimArrSym.Arreglo.SizeTri][][];
                                                            for (int i = 0; i < dimArrSym.Arreglo.SizeTri; i++)
                                                            {
                                                                (arrDato as char[][][])[i] = new char[dimArrSym.Arreglo.SizeBi][];
                                                                for (int j = 0; j < dimArrSym.Arreglo.SizeBi; i++)
                                                                {
                                                                    (arrDato as char[][][])[i][j] = new char[dimArrSym.Arreglo.SizeUni];
                                                                }
                                                            }
                                                            break;
                                                        case Tipo.BOOLEANARR:
                                                            arrDato = new bool[dimArrSym.Arreglo.SizeTri][][];
                                                            for (int i = 0; i < dimArrSym.Arreglo.SizeTri; i++)
                                                            {
                                                                (arrDato as bool[][][])[i] = new bool[dimArrSym.Arreglo.SizeBi][];
                                                                for (int j = 0; j < dimArrSym.Arreglo.SizeBi; i++)
                                                                {
                                                                    (arrDato as bool[][][])[i][j] = new bool[dimArrSym.Arreglo.SizeUni];
                                                                }
                                                            }
                                                            break;
                                                        case Tipo.CLASEARR:
                                                            arrDato = new Clase[dimArrSym.Arreglo.SizeTri][][];
                                                            for (int i = 0; i < dimArrSym.Arreglo.SizeTri; i++)
                                                            {
                                                                (arrDato as Clase[][][])[i] = new Clase[dimArrSym.Arreglo.SizeBi][];
                                                                for (int j = 0; j < dimArrSym.Arreglo.SizeBi; i++)
                                                                {
                                                                    (arrDato as Clase[][][])[i][j] = new Clase[dimArrSym.Arreglo.SizeUni];
                                                                }
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                            //Asigna todos los atributos al simbolo que se agregará al diccionario
                                            Simbolo ArrSym = new Simbolo
                                            {
                                                Arreglo = dimArrSym.Arreglo,
                                                Dato = arrDato,
                                                TipoDato = dataType,
                                                Posicion = pos,
                                                EsPrivado = false,
                                                Arr = ramaAux[2],
                                                Oper = null
                                            };
                                            //Añade el simbolo al entorno local
                                            RepeatSym.Add(varlst.Token.Text.ToLower(), ArrSym);
                                        }
                                        else
                                        {
                                            //Opera el OPER
                                            Simbolo operArrSym = operar.Interpretar(ramaAux[3]);
                                            //Verifica que el tipo de dato sea el mismo
                                            if (operArrSym.TipoDato != dataType)
                                            {
                                                Main.Imprimir(String.Format("Los tipos no coinciden {0}. No se puede asignar un {3} a un {4} : ({1},{2})", varlst.Token.Text.ToLower(), varlst.Token.Location.Line + 1, varlst.Token.Location.Column + 1, operArrSym.TipoDato, dataType));
                                                return false;
                                            }
                                            //Verifica que posea dimensiones
                                            if (dimArrSym.Arreglo == null || operArrSym.Arreglo == null)
                                            {
                                                Main.Imprimir(String.Format("Esto no es un arreglo {0}: ({1},{2})", varlst.Token.Text.ToLower(), varlst.Token.Location.Line + 1, varlst.Token.Location.Column + 1));
                                                return false;
                                            }
                                            //Verifica que las dimensiones sean las mismas
                                            if (dimArrSym.Arreglo.Dimension != operArrSym.Arreglo.Dimension)
                                            {
                                                Main.Imprimir(String.Format("Las dimensiones no coinciden {0}: ({1},{2})", varlst.Token.Text.ToLower(), varlst.Token.Location.Line + 1, varlst.Token.Location.Column + 1));
                                                return false;
                                            }
                                            //Verifica que todas las dimensiones sean iguales
                                            if (dimArrSym.Arreglo.SizeUni != operArrSym.Arreglo.SizeUni && dimArrSym.Arreglo.SizeBi != operArrSym.Arreglo.SizeBi && dimArrSym.Arreglo.SizeTri != operArrSym.Arreglo.SizeTri)
                                            {
                                                Main.Imprimir(String.Format("Las dimensiones no coinciden {0}: ({1},{2})", varlst.Token.Text.ToLower(), varlst.Token.Location.Line + 1, varlst.Token.Location.Column + 1));
                                                return false;
                                            }
                                            //Asigna todos los atributos al simbolo que se agregará al diccionario
                                            Simbolo ArrSym = new Simbolo
                                            {
                                                Arreglo = dimArrSym.Arreglo,
                                                Dato = operArrSym.Dato,
                                                TipoDato = dataType,
                                                Posicion = pos,
                                                EsPrivado = false,
                                                Arr = ramaAux[2],
                                                Oper = ramaAux[3]
                                            };
                                            //Añade el simbolo al entorno local
                                            RepeatSym.Add(varlst.Token.Text.ToLower(), ArrSym);
                                        }
                                    }
                                }
                                break;
                            #endregion
                            case "ASSIGNMENT":
                                #region ASIGNACION
                                //Determina que realizar contando el número de nodos
                                switch (ramaAux.Count)
                                {
                                    case 1:
                                        #region OPER
                                        //Opera el nodo
                                        operar.Interpretar(ramaAux[0]);
                                        #endregion
                                        break;
                                    case 2:
                                        #region VARIABLE SIMPLE
                                        //Opera el nodo 1
                                        Simbolo symOper = operar.Interpretar(ramaAux[1]);
                                        //Busca la variable del nodo 0
                                        Simbolo symRef = BuscarSimbolo(ramaAux[0].Token.Text.ToLower());
                                        //Se asegura que ambos simbolos sean del mismo tipo de datos
                                        //obviamente no pueden ser arrgelos
                                        if (symOper.TipoDato != symRef.TipoDato)
                                        {
                                            Main.Imprimir(String.Format("No se puede asignar un {0} a un {1} : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                            return true;
                                        }
                                        //Los simbolos son del mismo tipo por lo el simbolo Ref toma el dato de symOper
                                        symOper.Dato = symRef.Dato;
                                        #endregion
                                        break;
                                    case 3:
                                        #region VARIABLE ARREGLO
                                        //Opera el nodo OPER
                                        symOper = operar.Interpretar(ramaAux[2]);
                                        //Opera el nodo DIMENSIONLST
                                        //Devolverá la dimension que se quiere asignar
                                        var symDimLst = operar.Interpretar(ramaAux[1]);
                                        //Recupera la referencia del nodo0;
                                        symRef = BuscarSimbolo(ramaAux[0].Token.Text.ToLower());
                                        //Verifica que la dimension de la referencia ya haya sido trabajada
                                        if (symRef.Arreglo == null)
                                        {
                                            //Si es nulo, trabaja las dimensiones
                                            var symRefArrFirst = operar.Interpretar(symRef.Arr);
                                            //Asigna los datos recuperados  a la variables de referencia
                                            symRef.Arreglo.Dimension = symRefArrFirst.Arreglo.Dimension;
                                            symRef.Arreglo.SizeUni = symRefArrFirst.Arreglo.SizeUni;
                                            symRef.Arreglo.SizeBi = symRefArrFirst.Arreglo.SizeBi;
                                            symRef.Arreglo.SizeTri = symRefArrFirst.Arreglo.SizeTri;
                                        }
                                        //Se asegura que ambos simbolos sean del mismo tipo de dato
                                        if ((symOper.TipoDato + 6) != symRef.TipoDato || symOper.TipoDato != (symRef.TipoDato - 6))
                                        {
                                            Main.Imprimir(String.Format("No se puede asignar un {0} a un {1} : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                            return true;
                                        }
                                        //Verifica que la dimension sea la misma
                                        if (symRef.Arreglo.Dimension != symDimLst.Arreglo.Dimension)
                                        {
                                            Main.Imprimir(String.Format("Las dimensiones de {0} y {1} no coinciden : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                            return true;
                                        }
                                        //Si es un arreglo, va a asignar completamente el arreglo a la referencia
                                        if (symOper.TipoDato > Tipo.CLASE)
                                        {
                                            if (symRef.Arreglo.SizeUni != symDimLst.Arreglo.SizeUni)
                                            {
                                                Main.Imprimir(String.Format("El arreglo unidemensional de {0} no coincide con {1}  : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                                return true;
                                            }
                                            if (symRef.Arreglo.SizeBi != symDimLst.Arreglo.SizeBi)
                                            {
                                                Main.Imprimir(String.Format("El arreglo bidemensional de {0} no coincide con {1}  : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                                return true;
                                            }
                                            if (symRef.Arreglo.SizeTri != symDimLst.Arreglo.SizeTri)
                                            {
                                                Main.Imprimir(String.Format("El arreglo tridemensional de {0} no coincide con {1}  : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                                return true;
                                            }
                                            symRef.Dato = symOper.Dato;
                                        }
                                        //No es un arreglo, debe verificar que todas las dimensiones coincidan
                                        else
                                        {
                                            //Verifica que las dimensiones no se desborden
                                            //Según la dimensión así asignará los datos de OPER
                                            switch (symRef.Arreglo.Dimension)
                                            {
                                                case TipoArreglo.UNI:
                                                    if (symDimLst.Arreglo.SizeUni >= symRef.Arreglo.SizeUni)
                                                    {
                                                        Main.Imprimir(String.Format("El arreglo unidemensional de {0} desborda al arreglo {1}  : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                                        return true;
                                                    }
                                                    switch (symRef.TipoDato)
                                                    {
                                                        case Tipo.INTARR:
                                                            (symRef.Dato as int[])[symDimLst.Arreglo.SizeUni] = (int)symOper.Dato;
                                                            break;
                                                        case Tipo.STRINGARR:
                                                            (symRef.Dato as string[])[symDimLst.Arreglo.SizeUni] = (string)symOper.Dato;
                                                            break;
                                                        case Tipo.DOUBLEARR:
                                                            (symRef.Dato as double[])[symDimLst.Arreglo.SizeUni] = (double)symOper.Dato;
                                                            break;
                                                        case Tipo.CHARARR:
                                                            (symRef.Dato as char[])[symDimLst.Arreglo.SizeUni] = (char)symOper.Dato;
                                                            break;
                                                        case Tipo.BOOLEANARR:
                                                            (symRef.Dato as bool[])[symDimLst.Arreglo.SizeUni] = (bool)symOper.Dato;
                                                            break;
                                                        case Tipo.CLASEARR:
                                                            (symRef.Dato as Clase[])[symDimLst.Arreglo.SizeUni] = (Clase)symOper.Dato;
                                                            break;
                                                    }
                                                    break;
                                                case TipoArreglo.BI:
                                                    if (symDimLst.Arreglo.SizeUni >= symRef.Arreglo.SizeUni)
                                                    {
                                                        Main.Imprimir(String.Format("El arreglo unidemensional de {0} desborda al arreglo {1}  : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                                        return true;
                                                    }
                                                    if (symDimLst.Arreglo.SizeBi >= symRef.Arreglo.SizeBi)
                                                    {
                                                        Main.Imprimir(String.Format("El arreglo bidemensional de {0} desborda al arreglo {1}  : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                                        return true;
                                                    }
                                                    switch (symRef.TipoDato)
                                                    {
                                                        case Tipo.INTARR:
                                                            (symRef.Dato as int[][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi] = (int)symOper.Dato;
                                                            break;
                                                        case Tipo.STRINGARR:
                                                            (symRef.Dato as string[][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi] = (string)symOper.Dato;
                                                            break;
                                                        case Tipo.DOUBLEARR:
                                                            (symRef.Dato as double[][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi] = (double)symOper.Dato;
                                                            break;
                                                        case Tipo.CHARARR:
                                                            (symRef.Dato as char[][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi] = (char)symOper.Dato;
                                                            break;
                                                        case Tipo.BOOLEANARR:
                                                            (symRef.Dato as bool[][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi] = (bool)symOper.Dato;
                                                            break;
                                                        case Tipo.CLASEARR:
                                                            (symRef.Dato as Clase[][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi] = (Clase)symOper.Dato;
                                                            break;
                                                    }
                                                    break;
                                                case TipoArreglo.TRI:
                                                    if (symDimLst.Arreglo.SizeUni >= symRef.Arreglo.SizeUni)
                                                    {
                                                        Main.Imprimir(String.Format("El arreglo unidemensional de {0} desborda al arreglo {1}  : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                                        return true;
                                                    }
                                                    if (symDimLst.Arreglo.SizeBi >= symRef.Arreglo.SizeBi)
                                                    {
                                                        Main.Imprimir(String.Format("El arreglo bidemensional de {0} desborda al arreglo {1}  : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                                        return true;
                                                    }
                                                    if (symDimLst.Arreglo.SizeTri >= symRef.Arreglo.SizeTri)
                                                    {
                                                        Main.Imprimir(String.Format("El arreglo tridemensional de {0} desborda al arreglo {1}  : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                                        return true;
                                                    }
                                                    switch (symRef.TipoDato)
                                                    {
                                                        case Tipo.INTARR:
                                                            (symRef.Dato as int[][][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi][symDimLst.Arreglo.SizeTri] = (int)symOper.Dato;
                                                            break;
                                                        case Tipo.STRINGARR:
                                                            (symRef.Dato as string[][][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi][symDimLst.Arreglo.SizeTri] = (string)symOper.Dato;
                                                            break;
                                                        case Tipo.DOUBLEARR:
                                                            (symRef.Dato as double[][][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi][symDimLst.Arreglo.SizeTri] = (double)symOper.Dato;
                                                            break;
                                                        case Tipo.CHARARR:
                                                            (symRef.Dato as char[][][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi][symDimLst.Arreglo.SizeTri] = (char)symOper.Dato;
                                                            break;
                                                        case Tipo.BOOLEANARR:
                                                            (symRef.Dato as bool[][][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi][symDimLst.Arreglo.SizeTri] = (bool)symOper.Dato;
                                                            break;
                                                        case Tipo.CLASEARR:
                                                            (symRef.Dato as Clase[][][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi][symDimLst.Arreglo.SizeTri] = (Clase)symOper.Dato;
                                                            break;
                                                    }
                                                    break;
                                            }
                                        }
                                        #endregion
                                        break;
                                }
                                #endregion
                                break;
                            case "IF_STA":
                                switch (ramaAux.Count)
                                {
                                    case 2:
                                        //No tiene else
                                        if (new Si(ramaAux[0], ramaAux[1], this).Ejecutar())
                                        {
                                            return true;
                                        }
                                        break;
                                    case 3:
                                        if (new Si(ramaAux[0], ramaAux[1], ramaAux[2], this).Ejecutar())
                                        {
                                            return true;
                                        }
                                        //Tiene un else
                                        break;
                                }
                                break;
                            case "FOR_STA":
                                break;
                            case "REPEAT_STA":
                                break;
                            case "WHILE_STA":
                                if (new Mientras(nodeTree.ChildNodes[1], nodeTree.ChildNodes[0], this).Ejecutar())
                                {
                                    return true;
                                }
                                break;
                            case "SWITCH":
                                break;
                            case "DO":
                                break;
                            case "CONTINUAR":
                                #region CONTINUAR STATEMENT
                                if (EsLoop())
                                {
                                    continue;
                                }
                                else
                                {
                                    Main.Imprimir(String.Format("La sentencia continuar solo se permite en loops: ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                    return true;
                                }
                            #endregion
                            case "SALIR":
                                #region SALIR STATEMENT
                                if (EsLoop())
                                {
                                    break;
                                }
                                else
                                {
                                    Main.Imprimir(String.Format("La sentencia salir solo se permite en loops: ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                    return true;
                                }
                            #endregion
                            case "RTN_STA":
                                #region RETURN STATEMENT
                                //Crea un simboloq que servirá de referencia
                                var SymRefRtn = new Simbolo();
                                //Asigna una subrama , para facilidad de programación
                                var SubRamaAux = ramaAux[1].ChildNodes;
                                //Operar el assignment del lado izquierdo
                                switch (SubRamaAux.Count)
                                {
                                    case 1:
                                        #region OPER
                                        //Opera el nodo
                                        var SymRefOperSingle = operar.Interpretar(SubRamaAux[0]);
                                        //Asigna los nuevos valores al simbolo en cuestión
                                        SymRefRtn.TipoDato = SymRefOperSingle.TipoDato;
                                        SymRefRtn.Dato = SymRefOperSingle.Dato;
                                        #endregion
                                        break;
                                    case 2:
                                        #region VARIABLE SIMPLE
                                        //Opera el nodo 1
                                        Simbolo symOper = operar.Interpretar(SubRamaAux[1]);
                                        //Busca la variable del nodo 0
                                        Simbolo symRef = BuscarSimbolo(SubRamaAux[0].Token.Text.ToLower());
                                        //Se asegura que ambos simbolos sean del mismo tipo de datos
                                        //obviamente no pueden ser arrgelos
                                        if (symOper.TipoDato != symRef.TipoDato)
                                        {
                                            Main.Imprimir(String.Format("No se puede asignar un {0} a un {1} : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                            return true;
                                        }
                                        //Los simbolos son del mismo tipo por lo que simbolo Ref toma el dato de symOper
                                        symOper.Dato = symRef.Dato;
                                        SymRefRtn.Dato = symOper.Dato;
                                        SymRefRtn.TipoDato = symOper.TipoDato;
                                        #endregion
                                        break;
                                    case 3:
                                        #region VARIABLE ARREGLO
                                        //Opera el nodo OPER
                                        symOper = operar.Interpretar(SubRamaAux[2]);
                                        //Opera el nodo DIMENSIONLST
                                        //Devolverá la dimension que se quiere asignar
                                        var symDimLst = operar.Interpretar(SubRamaAux[1]);
                                        //Recupera la referencia del nodo0;
                                        symRef = BuscarSimbolo(SubRamaAux[0].Token.Text.ToLower());
                                        //Verifica que la dimension de la referencia ya haya sido trabajada
                                        if (symRef.Arreglo == null)
                                        {
                                            //Si es nulo, trabaja las dimensiones
                                            var symRefArrFirst = operar.Interpretar(symRef.Arr);
                                            //Asigna los datos recuperados  a la variables de referencia
                                            symRef.Arreglo.Dimension = symRefArrFirst.Arreglo.Dimension;
                                            symRef.Arreglo.SizeUni = symRefArrFirst.Arreglo.SizeUni;
                                            symRef.Arreglo.SizeBi = symRefArrFirst.Arreglo.SizeBi;
                                            symRef.Arreglo.SizeTri = symRefArrFirst.Arreglo.SizeTri;
                                        }
                                        //Se asegura que ambos simbolos sean del mismo tipo de dato
                                        if ((symOper.TipoDato + 6) != symRef.TipoDato || symOper.TipoDato != symRef.TipoDato)
                                        {
                                            Main.Imprimir(String.Format("No se puede asignar un {0} a un {1} : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                            return true;
                                        }
                                        //Verifica que la dimension sea la misma
                                        if (symRef.Arreglo.Dimension != symDimLst.Arreglo.Dimension)
                                        {
                                            Main.Imprimir(String.Format("Las dimensiones de {0} y {1} no coinciden : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                            return true;
                                        }
                                        //Si es un arreglo, va a asignar completamente el arreglo a la referencia
                                        if (symOper.TipoDato > Tipo.CLASE)
                                        {
                                            if (symRef.Arreglo.SizeUni != symDimLst.Arreglo.SizeUni)
                                            {
                                                Main.Imprimir(String.Format("El arreglo unidemensional de {0} no coincide con {1}  : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                                return true;
                                            }
                                            if (symRef.Arreglo.SizeBi != symDimLst.Arreglo.SizeBi)
                                            {
                                                Main.Imprimir(String.Format("El arreglo bidemensional de {0} no coincide con {1}  : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                                return true;
                                            }
                                            if (symRef.Arreglo.SizeTri != symDimLst.Arreglo.SizeTri)
                                            {
                                                Main.Imprimir(String.Format("El arreglo tridemensional de {0} no coincide con {1}  : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                                return true;
                                            }
                                            symRef.Dato = symOper.Dato;
                                        }
                                        else
                                        {
                                            switch (symRef.Arreglo.Dimension)
                                            {
                                                case TipoArreglo.UNI:
                                                    if (symRef.Arreglo.SizeUni >= symDimLst.Arreglo.SizeUni)
                                                    {
                                                        Main.Imprimir(String.Format("El arreglo unidemensional de {0} desborda al arreglo {1}  : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                                        return true;
                                                    }
                                                    switch (symRef.TipoDato)
                                                    {
                                                        case Tipo.INTARR:
                                                            (symRef.Dato as int[])[symDimLst.Arreglo.SizeUni] = (int)symOper.Dato;
                                                            break;
                                                        case Tipo.STRINGARR:
                                                            (symRef.Dato as string[])[symDimLst.Arreglo.SizeUni] = (string)symOper.Dato;
                                                            break;
                                                        case Tipo.DOUBLEARR:
                                                            (symRef.Dato as double[])[symDimLst.Arreglo.SizeUni] = (double)symOper.Dato;
                                                            break;
                                                        case Tipo.CHARARR:
                                                            (symRef.Dato as char[])[symDimLst.Arreglo.SizeUni] = (char)symOper.Dato;
                                                            break;
                                                        case Tipo.BOOLEANARR:
                                                            (symRef.Dato as bool[])[symDimLst.Arreglo.SizeUni] = (bool)symOper.Dato;
                                                            break;
                                                        case Tipo.CLASEARR:
                                                            (symRef.Dato as Clase[])[symDimLst.Arreglo.SizeUni] = (Clase)symOper.Dato;
                                                            break;
                                                    }
                                                    break;
                                                case TipoArreglo.BI:
                                                    if (symRef.Arreglo.SizeUni >= symDimLst.Arreglo.SizeUni)
                                                    {
                                                        Main.Imprimir(String.Format("El arreglo unidemensional de {0} desborda al arreglo {1}  : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                                        return true;
                                                    }
                                                    if (symRef.Arreglo.SizeBi >= symDimLst.Arreglo.SizeBi)
                                                    {
                                                        Main.Imprimir(String.Format("El arreglo bidemensional de {0} desborda al arreglo {1}  : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                                        return true;
                                                    }
                                                    switch (symRef.TipoDato)
                                                    {
                                                        case Tipo.INTARR:
                                                            (symRef.Dato as int[][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi] = (int)symOper.Dato;
                                                            break;
                                                        case Tipo.STRINGARR:
                                                            (symRef.Dato as string[][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi] = (string)symOper.Dato;
                                                            break;
                                                        case Tipo.DOUBLEARR:
                                                            (symRef.Dato as double[][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi] = (double)symOper.Dato;
                                                            break;
                                                        case Tipo.CHARARR:
                                                            (symRef.Dato as char[][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi] = (char)symOper.Dato;
                                                            break;
                                                        case Tipo.BOOLEANARR:
                                                            (symRef.Dato as bool[][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi] = (bool)symOper.Dato;
                                                            break;
                                                        case Tipo.CLASEARR:
                                                            (symRef.Dato as Clase[][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi] = (Clase)symOper.Dato;
                                                            break;
                                                    }
                                                    break;
                                                case TipoArreglo.TRI:
                                                    if (symRef.Arreglo.SizeUni >= symDimLst.Arreglo.SizeUni)
                                                    {
                                                        Main.Imprimir(String.Format("El arreglo unidemensional de {0} desborda al arreglo {1}  : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                                        return true;
                                                    }
                                                    if (symRef.Arreglo.SizeBi >= symDimLst.Arreglo.SizeBi)
                                                    {
                                                        Main.Imprimir(String.Format("El arreglo bidemensional de {0} desborda al arreglo {1}  : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                                        return true;
                                                    }
                                                    if (symRef.Arreglo.SizeTri >= symDimLst.Arreglo.SizeTri)
                                                    {
                                                        Main.Imprimir(String.Format("El arreglo tridemensional de {0} desborda al arreglo {1}  : ({2},{3})", symOper.TipoDato, symRef.TipoDato, symOper.Posicion != null ? symOper.Posicion.Fila : 0, symOper.Posicion != null ? symOper.Posicion.Columna : 0));
                                                        return true;
                                                    }
                                                    switch (symRef.TipoDato)
                                                    {
                                                        case Tipo.INTARR:
                                                            (symRef.Dato as int[][][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi][symDimLst.Arreglo.SizeTri] = (int)symOper.Dato;
                                                            break;
                                                        case Tipo.STRINGARR:
                                                            (symRef.Dato as string[][][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi][symDimLst.Arreglo.SizeTri] = (string)symOper.Dato;
                                                            break;
                                                        case Tipo.DOUBLEARR:
                                                            (symRef.Dato as double[][][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi][symDimLst.Arreglo.SizeTri] = (double)symOper.Dato;
                                                            break;
                                                        case Tipo.CHARARR:
                                                            (symRef.Dato as char[][][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi][symDimLst.Arreglo.SizeTri] = (char)symOper.Dato;
                                                            break;
                                                        case Tipo.BOOLEANARR:
                                                            (symRef.Dato as bool[][][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi][symDimLst.Arreglo.SizeTri] = (bool)symOper.Dato;
                                                            break;
                                                        case Tipo.CLASEARR:
                                                            (symRef.Dato as Clase[][][])[symDimLst.Arreglo.SizeUni][symDimLst.Arreglo.SizeBi][symDimLst.Arreglo.SizeTri] = (Clase)symOper.Dato;
                                                            break;
                                                    }
                                                    break;
                                            }
                                        }
                                        //Asigna por valor los datos al SymRefRtn
                                        SymRefRtn.Dato = symRef.Dato;
                                        SymRefRtn.Arreglo.Dimension = symRef.Arreglo.Dimension;
                                        SymRefRtn.Arreglo.SizeUni = symRef.Arreglo.SizeUni;
                                        SymRefRtn.Arreglo.SizeBi = symRef.Arreglo.SizeBi;
                                        SymRefRtn.Arreglo.SizeTri = symRef.Arreglo.SizeTri;
                                        SymRefRtn.TipoDato = symRef.TipoDato;
                                        #endregion
                                        break;
                                }
                                SymRefRtn.Posicion = new Posicion(ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1);
                                //Recupera la función inmediata superior
                                Funcion funcionPadre = BuscarFuncionPadre();
                                if (funcionPadre == null)
                                {
                                    Main.Imprimir(String.Format("Ocurrio un error en el entorno {0}: no se pudo recuperar la Función padre", this.GetType()));
                                    return true;
                                }
                                //Se asegura que el simbolo generado y el simbolo de retorno de la funciónPadre
                                //sean iguales
                                if (funcionPadre.ReturnData.TipoDato != SymRefRtn.TipoDato)
                                {
                                    Main.Imprimir(String.Format("El tipo de dato retornado no coincide con el tipo de la función : ({0},{1})", SymRefRtn.Posicion.Fila, SymRefRtn.Posicion.Columna));
                                    return true;
                                }
                                //Determina si el ReturnData de la función padres es un arreglo
                                if (funcionPadre.ReturnData.Arr != null)
                                {
                                    //Opera la dimension_lst de la función, si no han sido operada antes ya
                                    if (funcionPadre.ReturnData.Arreglo == null)
                                    {
                                        var simAuxArr = operar.Interpretar(funcionPadre.ReturnData.Arr);
                                        //Asigna los datos recuperados  a la variables de referencia
                                        funcionPadre.ReturnData.Arreglo.Dimension = simAuxArr.Arreglo.Dimension;
                                        funcionPadre.ReturnData.Arreglo.SizeUni = simAuxArr.Arreglo.SizeUni;
                                        funcionPadre.ReturnData.Arreglo.SizeBi = simAuxArr.Arreglo.SizeBi;
                                        funcionPadre.ReturnData.Arreglo.SizeTri = simAuxArr.Arreglo.SizeTri;
                                    }
                                    //Se asegura que la dimensión sea la misma
                                    if (SymRefRtn.Arreglo.Dimension != funcionPadre.ReturnData.Arreglo.Dimension)
                                    {
                                        Main.Imprimir(String.Format("Las dimensiones de {0} y {1} no coinciden : ({2},{3})", SymRefRtn.TipoDato, funcionPadre.ReturnData.TipoDato, SymRefRtn.Posicion != null ? SymRefRtn.Posicion.Fila : 0, SymRefRtn.Posicion != null ? SymRefRtn.Posicion.Columna : 0));
                                        return true;
                                    }
                                    //Se asegura que todas sus dimensiones sean iguales
                                    if (SymRefRtn.Arreglo.SizeUni != funcionPadre.ReturnData.Arreglo.SizeUni)
                                    {
                                        Main.Imprimir(String.Format("El arreglo unidemensional de {0} no coincide con {1}  : ({2},{3})", SymRefRtn.TipoDato, funcionPadre.ReturnData.TipoDato, SymRefRtn.Posicion != null ? SymRefRtn.Posicion.Fila : 0, SymRefRtn.Posicion != null ? SymRefRtn.Posicion.Columna : 0));
                                        return true;
                                    }
                                    if (SymRefRtn.Arreglo.SizeBi != funcionPadre.ReturnData.Arreglo.SizeBi)
                                    {
                                        Main.Imprimir(String.Format("El arreglo bidemensional de {0} no coincide con {1}  : ({2},{3})", SymRefRtn.TipoDato, funcionPadre.ReturnData.TipoDato, SymRefRtn.Posicion != null ? SymRefRtn.Posicion.Fila : 0, SymRefRtn.Posicion != null ? SymRefRtn.Posicion.Columna : 0));
                                        return true;
                                    }
                                    if (SymRefRtn.Arreglo.SizeTri != funcionPadre.ReturnData.Arreglo.SizeTri)
                                    {
                                        Main.Imprimir(String.Format("El arreglo tridemensional de {0} no coincide con {1}  : ({2},{3})", SymRefRtn.TipoDato, funcionPadre.ReturnData.TipoDato, SymRefRtn.Posicion != null ? SymRefRtn.Posicion.Fila : 0, SymRefRtn.Posicion != null ? SymRefRtn.Posicion.Columna : 0));
                                        return true;
                                    }
                                }
                                funcionPadre.ReturnData.Dato = SymRefRtn.Dato;
                                funcionPadre.ReturnData.Arreglo = SymRefRtn.Arreglo;
                                #endregion
                                return true;
                            case "NATIVA":
                                #region NATIVAS
                                if (ramaAux[0].Term.Name.Equals("PRINT"))
                                {
                                    //Opera el nodo OPER
                                    var symOperPrint = operar.Interpretar(ramaAux[1]);
                                    //Se asegura que el contenido no sea nulo
                                    if (symOperPrint.TipoDato == Tipo.VOID || symOperPrint.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("La función PRINT no acepta VOID: ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    Nativa.Print(symOperPrint.Dato.ToString());
                                }
                                if (ramaAux[0].Term.Name.Equals("SHOW"))
                                {
                                    //Se asegura que los nodos hijos del nodo hijo derecho solo sean dos
                                    if (ramaAux[1].ChildNodes.Count != 2)
                                    {
                                        Main.Imprimir(String.Format("La función SHOW esperaba dos parametros : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    //Opera los DOS nodos hijos derechos
                                    var symOperShowIzq = operar.Interpretar(ramaAux[1].ChildNodes[0]);
                                    var symOperShowDer = operar.Interpretar(ramaAux[1].ChildNodes[1]);
                                    //Se asegura que el contenido no sea nulo
                                    if ((symOperShowIzq.TipoDato == Tipo.VOID && symOperShowDer.TipoDato == Tipo.VOID) ||
                                        (symOperShowIzq.Dato == null && symOperShowDer.Dato == null))
                                    {
                                        Main.Imprimir(String.Format("La función SHOW no acepta VOID: ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    Nativa.Show(symOperShowIzq.Dato, symOperShowDer);
                                }
                                #endregion
                                break;
                            case "FIGURES":
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Main.Imprimir(String.Format("Excepción en tiempo de ejecución: {0} -> {1}", e.Message, e.Source));
                retorno = true;
            }
            finally
            {
                //Al finalizar copia las variables que se encuentran en el diccionario de simbolos local
                foreach (var locSym in RepeatSym)
                {
                    Main.AgregarSimbolo(locSym.Key, locSym.Value, this.ToString());
                }
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

        public override string ToString()
        {
            return "Repetir";
        }
    }
}
