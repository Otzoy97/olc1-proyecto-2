using Irony.Parsing;
using PROYECTO.Gramatica.Acciones;
using PROYECTO.Gramatica.Acciones.Operaciones;
using PROYECTO.Gramatica.Entorno.Condicional;
using PROYECTO.Gramatica.Entorno.Condicional.Switch;
using PROYECTO.Gramatica.Entorno.Loop;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PROYECTO.Gramatica.Entorno
{
    class Funcion : Clase, IEntorno
    {
        //Simbolos almacendos en tiempo de ejecución
        public Dictionary<string, Simbolo> FuncSym { get; set; }
        //Subarbol de acciones
        public ParseTreeNode FuncTree { get; set; }
        //Determina si la función es privada
        public bool EsPrivado { get; set; }
        //Determina el tipo de dato que debe devolver
        //public Tipo DataType { get; set; }
        //Especifica el simbolo a retornar
        public Simbolo ReturnData { get; set; }
        //Es sobrecarga
        public bool Override { get; set; }
        //Simbolo parametros de la función
        public Dictionary<string, Simbolo> FuncParSym { get; set; }

        public Funcion()
        {
            FuncSym = new Dictionary<string, Simbolo>();
            FuncParSym = new Dictionary<string, Simbolo>();
            ReturnData = new Simbolo();
            EsPrivado = false;            
            FuncTree = null;   
            Override = false;
        }

        public new Simbolo BuscarSimbolo(string nombreVar)
        {
            return this.FuncSym.ContainsKey(nombreVar) ? this.FuncSym[nombreVar] : base.BuscarSimbolo(nombreVar);
        }

        

        public new bool Ejecutar()
        {
            bool retorno = false;
            //Servirá para operar todo
            Operar operar = new Operar(this, BuscarClasePadre());
            //Recorre el nodo de acciones
            try
            {
                foreach (var nodeTree in FuncTree.ChildNodes)
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
                            Tipo dataType = GetTipoDato(ramaAux);
                            //Es un dato plano
                            if (dataType >= Tipo.INT && dataType <= Tipo.CLASE)
                            {
                                foreach (var varlst in ramaAux[1].ChildNodes)
                                {
                                    //Verifica que la variable no exista
                                    if (FuncSym.ContainsKey(varlst.Token.Text.ToLower()))
                                    {
                                        Main.Imprimir(String.Format("La variable {0} ya ha sido declarada en este entorno: ({1},{2})", varlst.Token.Text.ToLower(), varlst.Token.Location.Line + 1, varlst.Token.Location.Column + 1));
                                        return false;
                                    }
                                    //Determina si hay algo que operar
                                    if (!ramaAux[ramaAux.Count - 1].Term.Name.Equals("OPER"))
                                    {
                                        //Simplemente crea un simbolo y lo agrega al diccionario
                                        this.FuncSym.Add(varlst.Token.Text.ToLower(), new Simbolo(pos, false, null, dataType));
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
                                        FuncSym.Add(varlst.Token.Text.ToLower(), ArrSym); Main.Imprimir(String.Format("La variable {0} ya ha sido declarada en este entorno: ({1},{2})", varlst.Token.Text.ToLower(), varlst.Token.Location.Line + 1, varlst.Token.Location.Column + 1));
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
                                    if (FuncSym.ContainsKey(varlst.Token.Text.ToLower()))
                                    {
                                        Main.Imprimir(String.Format("La variable {0} ya ha sido declarada en este entorno: ({1},{2})", varlst.Token.Text.ToLower(), varlst.Token.Location.Line + 1, varlst.Token.Location.Column + 1));
                                        return false;
                                    }
                                    //Opera las dimensiones
                                    Simbolo dimArrSym = operar.Interpretar(ramaAux[2]);
                                    //Determina si se opera o no el OPER
                                    if (ramaAux.Count == 3)
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
                                        FuncSym.Add(varlst.Token.Text.ToLower(), ArrSym);
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
                                        FuncSym.Add(varlst.Token.Text.ToLower(), ArrSym);
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
                                    symRef.Dato = symOper.Dato;
                                    #endregion
                                    break;
                                case 3:
                                    #region VARIABLE ARREGLO
                                    //Opera el nodo OPER
                                    //Que debería de ser una tipo 'plano' 
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
                                    //No es un arreglo, debe verificar que todas las dimensiones coincidan
                                    else
                                    {
                                        //Verifica que las dimensiones no se desborden
                                        //Según la dimensión así asignará los datos de OPER
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
                            if (new Para(nodeTree.ChildNodes[0].ChildNodes[0], nodeTree.ChildNodes[0].ChildNodes[1], nodeTree.ChildNodes[0].ChildNodes[2], nodeTree.ChildNodes[1], this).Ejecutar())
                            {
                                return true;
                            }
                            break;
                        case "REPEAT_STA":
                            //Crea un nuevo entorno de Repeat y lo ejecuta
                            if (new Repetir(nodeTree.ChildNodes[1], nodeTree.ChildNodes[0], this).Ejecutar())
                            {
                                return true;
                            }
                            break;
                        case "WHILE_STA":
                            if (new Mientras(nodeTree.ChildNodes[1], nodeTree.ChildNodes[0], this).Ejecutar())
                            {
                                return true;
                            }
                            break;
                        case "SWITCH":
                            if (new Comprobar(nodeTree.ChildNodes[0], nodeTree.ChildNodes[1], this).Ejecutar())
                            {
                                return true;
                            }
                            break;
                        case "DO":
                            if (new Hacer(nodeTree.ChildNodes[0], nodeTree.ChildNodes[1], this).Ejecutar())
                            {
                                return true;
                            }
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
                            //Al finalizar copia las variables que se encuentran en el diccionario de simbolos local
                            foreach (var locSym in FuncSym)
                            {
                                Main.AgregarSimbolo(locSym.Key, locSym.Value, this.ToString());
                            }
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
                            Simbolo Par1, Par2, Par3, Par4, Par5, Par6, Par7, Par8;
                            switch (ramaAux[0].Term.Name)
                            {
                                case "CIRCLE":
                                    //Cuenta el numero de nodos hijo de lado izquierdo, si 
                                    //son diferente a los esperado , error
                                    if (ramaAux[1].ChildNodes.Count != 5)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban 5 parametros, parametros recibidos {0} : ({1},{2})", ramaAux[1].ChildNodes.Count, ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    #region COMPROBACION ERR CIRCLE
                                    //Recupera cada uno de los 5 simbolos y se asegura que sean del tipo esperado
                                    Par1 = operar.Interpretar(ramaAux[1].ChildNodes[0]);
                                    if (Par1.TipoDato != Tipo.STRING || Par1.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un STRING : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    Par2 = operar.Interpretar(ramaAux[1].ChildNodes[1]);
                                    if (Par2.TipoDato != Tipo.INT || Par2.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un INT : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    Par3 = operar.Interpretar(ramaAux[1].ChildNodes[2]);
                                    if (Par3.TipoDato != Tipo.BOOLEAN || Par3.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un BOOLEAN : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    Par4 = operar.Interpretar(ramaAux[1].ChildNodes[3]);
                                    if (Par4.TipoDato != Tipo.INT || Par4.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un INT : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    Par5 = operar.Interpretar(ramaAux[1].ChildNodes[4]);
                                    if (Par5.TipoDato != Tipo.INT || Par5.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un INT : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    #endregion
                                    Nativa.AgregarFigura(new Circulo(Par1.Dato.ToString(), (int)Par2.Dato, (bool)Par3.Dato, (int)Par4.Dato, (int)Par5.Dato));
                                    break;
                                case "TRIAN":
                                    //Cuenta el numero de nodos hijo de lado izquierdo, si 
                                    //son diferente a los esperado , error
                                    if (ramaAux[1].ChildNodes.Count != 8)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban 8 parametros, parametros recibidos {0} : ({1},{2})", ramaAux[1].ChildNodes.Count, ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                    }
                                    #region COMPROBACION ERRORES TRINAGULO
                                    //Recupera cada uno de los 8 simbolos y se asegura que sean del tipo esperado
                                    Par1 = operar.Interpretar(ramaAux[1].ChildNodes[0]);
                                    if (Par1.TipoDato != Tipo.STRING || Par1.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un STRING : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    Par2 = operar.Interpretar(ramaAux[1].ChildNodes[1]);
                                    if (Par2.TipoDato != Tipo.BOOLEAN || Par2.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un BOOLEAN : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    Par3 = operar.Interpretar(ramaAux[1].ChildNodes[2]);
                                    if (Par3.TipoDato != Tipo.INT || Par3.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un INT : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    Par4 = operar.Interpretar(ramaAux[1].ChildNodes[3]);
                                    if (Par4.TipoDato != Tipo.INT || Par4.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un INT : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    Par5 = operar.Interpretar(ramaAux[1].ChildNodes[4]);
                                    if (Par5.TipoDato != Tipo.INT || Par5.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un INT : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    Par6 = operar.Interpretar(ramaAux[1].ChildNodes[5]);
                                    if (Par6.TipoDato != Tipo.INT || Par6.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un INT : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    Par7 = operar.Interpretar(ramaAux[1].ChildNodes[6]);
                                    if (Par7.TipoDato != Tipo.INT || Par7.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un INT : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    Par8 = operar.Interpretar(ramaAux[1].ChildNodes[7]);
                                    if (Par8.TipoDato != Tipo.INT || Par8.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un INT : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    #endregion
                                    Nativa.AgregarFigura(new Triangulo(Par1.Dato.ToString(), (bool)Par2.Dato, (int)Par3.Dato, (int)Par4.Dato, (int)Par5.Dato, (int)Par6.Dato, (int)Par7.Dato, (int)Par8.Dato));
                                    break;
                                case "SQA":
                                    //Cuenta el numero de nodos hijo de lado izquierdo, si 
                                    //son diferente a los esperado , error
                                    if (ramaAux[1].ChildNodes.Count != 6)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban 6 parametros, parametros recibidos {0} : ({1},{2})", ramaAux[1].ChildNodes.Count, ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                    }
                                    #region COMPROBACION ERRORES SQUARE
                                    //Recupera cada uno de los 6 simbolos y se asegura que sean del tipo esperado
                                    Par1 = operar.Interpretar(ramaAux[1].ChildNodes[0]);
                                    if (Par1.TipoDato != Tipo.STRING || Par1.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un STRING : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    Par2 = operar.Interpretar(ramaAux[1].ChildNodes[1]);
                                    if (Par2.TipoDato != Tipo.BOOLEAN || Par2.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un BOOLEAN : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    Par3 = operar.Interpretar(ramaAux[1].ChildNodes[2]);
                                    if (Par3.TipoDato != Tipo.INT || Par3.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un INT : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    Par4 = operar.Interpretar(ramaAux[1].ChildNodes[3]);
                                    if (Par4.TipoDato != Tipo.INT || Par4.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un INT : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    Par5 = operar.Interpretar(ramaAux[1].ChildNodes[4]);
                                    if (Par5.TipoDato != Tipo.INT || Par5.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un INT : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    Par6 = operar.Interpretar(ramaAux[1].ChildNodes[5]);
                                    if (Par6.TipoDato != Tipo.INT || Par6.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un INT : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    #endregion
                                    Nativa.AgregarFigura(new Rectangulo(Par1.Dato.ToString(), (bool)Par2.Dato, (int)Par3.Dato, (int)Par4.Dato, (int)Par5.Dato, (int)Par6.Dato));
                                    break;
                                case "LINE":
                                    //Cuenta el numero de nodos hijo de lado izquierdo, si 
                                    //son diferente a los esperado , error
                                    if (ramaAux[1].ChildNodes.Count != 6)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban 6 parametros, parametros recibidos {0} : ({1},{2})", ramaAux[1].ChildNodes.Count, ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                    }
                                    //Recupera cada uno de los 6 simbolos y se asegura que sean del tipo esperado
                                    #region COMPROBACION ERRORES LINE
                                    Par1 = operar.Interpretar(ramaAux[1].ChildNodes[0]);
                                    if (Par1.TipoDato != Tipo.STRING || Par1.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un STRING : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    Par2 = operar.Interpretar(ramaAux[1].ChildNodes[1]);
                                    if (Par2.TipoDato != Tipo.INT || Par2.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un INT : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    Par3 = operar.Interpretar(ramaAux[1].ChildNodes[2]);
                                    if (Par3.TipoDato != Tipo.INT || Par3.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un INT : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    Par4 = operar.Interpretar(ramaAux[1].ChildNodes[3]);
                                    if (Par4.TipoDato != Tipo.INT || Par4.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un INT : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    Par5 = operar.Interpretar(ramaAux[1].ChildNodes[4]);
                                    if (Par5.TipoDato != Tipo.INT || Par5.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un INT : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    Par6 = operar.Interpretar(ramaAux[1].ChildNodes[5]);
                                    if (Par6.TipoDato != Tipo.INT || Par6.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("Se esperaban un INT : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1));
                                        return true;
                                    }
                                    #endregion
                                    Nativa.AgregarFigura(new Linea(Par1.Dato.ToString(), (int)Par2.Dato, (int)Par3.Dato, (int)Par4.Dato, (int)Par5.Dato, (int)Par6.Dato));
                                    break;
                                case "FIGURE":
                                    //Opera el lado derecho del nodo
                                    var OperSymFigure = operar.Interpretar(ramaAux[1]);
                                    //Se asegura que no sea nulo y que sea un tipo de dato válido
                                    if (OperSymFigure.TipoDato == Tipo.VOID || OperSymFigure.TipoDato >= Tipo.CLASE || OperSymFigure.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("No se puede asignar un {2} a esta sentencia : ({0},{1})", ramaAux[0].Token.Location.Line + 1, ramaAux[0].Token.Location.Column + 1, OperSymFigure.TipoDato));
                                        return true;
                                    }
                                    //Opera la sentencia
                                    Nativa.PintarFigura(OperSymFigure.Dato.ToString());
                                    break;
                            }
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Main.Imprimir(String.Format("Excepción en tiempo de ejecución: {0} -> {1}", e.Message, e.Source));
                //Al finalizar copia las variables que se encuentran en el diccionario de simbolos local
                foreach (var locSym in FuncSym)
                {
                    Main.AgregarSimbolo(locSym.Key, locSym.Value, this.ToString());
                }
                retorno = true;
            }
            finally
            {
               //Al finalizar copia las variables que se encuentran en el diccionario de simbolos local
               foreach (var locSym in FuncSym)
               {
                   Main.AgregarSimbolo(locSym.Key, locSym.Value, this.ToString());
               }
            }            
            return retorno;
        }

        /// <summary>
        /// Determina el tipo de dato que se utilizará según la lista de nodo dada
        /// </summary>
        /// <param name="tipoDato">Lista de nodos que contiene la info de una declaración de variable</param>
        /// <returns></returns>
        public static Tipo GetTipoDato(ParseTreeNodeList tipoDato)
        {
            //Servirá para determinar si la variable es o no un array
            bool esArray = false;
            switch (tipoDato.Count)
            {
                case 2:
                    esArray = false;
                    break;
                case 3:
                    esArray = tipoDato[2].Term.Name.Equals("DIMENSION_LIST");
                    break;
                case 4:
                    esArray = true;
                    break;
            }
            //Establece el tipo de dato
            Tipo tipo = esArray ? Tipo.CLASEARR : Tipo.CLASE;
            switch (tipoDato[0].Token.Text.ToLower())
            {
                case "int":
                    tipo = esArray ? Tipo.INTARR : Tipo.INT;
                    break;
                case "string":
                    tipo = esArray ? Tipo.STRINGARR : Tipo.STRING;
                    break;
                case "bool":
                    tipo = esArray ? Tipo.BOOLEANARR : Tipo.BOOLEAN;
                    break;
                case "char":
                    tipo = esArray ? Tipo.CHARARR : Tipo.CHAR;
                    break;
                case "double":
                    tipo = esArray ? Tipo.DOUBLEARR : Tipo.DOUBLE;
                    break;
            };
            return tipo;
        }
        /// <summary>
        /// Devuelve esta función
        /// </summary>
        /// <returns></returns>
        public new Funcion BuscarFuncionPadre()
        {
            return this;
        }
        /// <summary>
        /// Realiza una copia de la función 
        /// </summary>
        /// <param name="original">Función a copiar</param>
        /// <returns></returns>
        public static Funcion Copiar(Funcion original)
        {
            Funcion funcion = new Funcion()
            {
                FuncTree = original.FuncTree,
                EsPrivado = original.EsPrivado,
                Override = original.Override
            };
            //Copia el simbolo de retorno
            funcion.ReturnData.Arr = original.ReturnData.Arr;
            funcion.ReturnData.Dato = original.ReturnData.Dato;
            funcion.ReturnData.EsPrivado = original.ReturnData.EsPrivado;
            funcion.ReturnData.Oper = original.ReturnData.Oper;
            funcion.ReturnData.TipoDato = original.ReturnData.TipoDato;
            if (original.ReturnData.Arreglo != null)
            {
                Arreglo arr = new Arreglo
                {
                    Dimension = original.ReturnData.Arreglo.Dimension,
                    SizeBi = original.ReturnData.Arreglo.SizeBi,
                    SizeUni = original.ReturnData.Arreglo.SizeUni,
                    SizeTri = original.ReturnData.Arreglo.SizeTri
                };
                funcion.ReturnData.Arreglo = arr;
            }
            if (original.ReturnData.Posicion != null)
            {
                Posicion pos = new Posicion(original.ReturnData.Posicion.Fila, original.ReturnData.Posicion.Columna);
                funcion.ReturnData.Posicion = pos;
            }
            ///Copia los simbolos-parametros de la función
            foreach (var lst01 in original.FuncParSym)
            {
                Simbolo symPrueba = new Simbolo
                {
                    Arr = lst01.Value.Arr,
                    Dato = lst01.Value.Dato,
                    EsPrivado = lst01.Value.EsPrivado,
                    Oper = lst01.Value.Oper,
                    TipoDato = lst01.Value.TipoDato
                };

                if (lst01.Value.Arreglo != null)
                {
                    Arreglo arr = new Arreglo
                    {
                        Dimension = lst01.Value.Arreglo.Dimension,
                        SizeBi = lst01.Value.Arreglo.SizeBi,
                        SizeUni = lst01.Value.Arreglo.SizeUni,
                        SizeTri = lst01.Value.Arreglo.SizeTri
                    };
                    symPrueba.Arreglo = arr;
                }
                if (lst01.Value.Posicion != null)
                {
                    Posicion pos = new Posicion(lst01.Value.Posicion.Fila, lst01.Value.Posicion.Columna);
                    symPrueba.Posicion = pos;
                }
                funcion.FuncParSym.Add(lst01.Key.ToString(), symPrueba);
            }
            return funcion;
        }
        /// <summary>
        /// Muestra el entorno en el que está
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Funcion";
        }
    }
}
