using Irony.Parsing;
using PROYECTO.Gramatica.Entorno;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PROYECTO.Gramatica.Acciones.Operaciones
{
    class Operar 
    {
        private IEntorno Entorno;
        private Dictionary<string, Clase> Clases;
        private Clase ClaseLocal;

        public Operar(IEntorno entorno, Clase clase, Dictionary<string,Clase> clases)
        {
            this.Entorno = entorno;
            this.Clases = clases;
            this.ClaseLocal = clase;
        }
        public Simbolo Interpretar(ParseTreeNode operRaiz)
        {
            Simbolo retorno = new Simbolo();
            if (operRaiz == null)
            {
                Main.Imprimir("La variable no ha sido inicializada");
                return new Simbolo();
            }
            switch (operRaiz.Term.Name)
            {
                case "DIMENSION_LIST":
                    switch (operRaiz.ChildNodes.Count)
                    {
                        case 1:
                            //Es un arreglo unidimensional
                            retorno.Arreglo.Dimension = TipoArreglo.UNI;
                            //Opera el primer nodo
                            var sizeUni = Interpretar(operRaiz.ChildNodes[0]);
                            //Verifica que sea un entero
                            if (sizeUni.TipoDato == Tipo.INT)
                            {
                                retorno.Arreglo.SizeUni = (int) sizeUni.Dato;
                                retorno.Arreglo.Dimension = TipoArreglo.UNI;
                            }
                            else
                            {
                                Main.Imprimir(String.Format("Las dimensiones del arreglo deben especificarse como enteros. ({0},{1})",sizeUni.Posicion.Fila,sizeUni.Posicion.Columna));
                            }
                            break;
                        case 2:
                            //Es un arreglo bidimensional
                            retorno.Arreglo.Dimension = TipoArreglo.BI;
                            //Opera los dos nodos
                            sizeUni = Interpretar(operRaiz.ChildNodes[0]);
                            var sizeBi = Interpretar(operRaiz.ChildNodes[1]);
                            //Verifica que todos sean un entero
                            if (sizeUni.TipoDato == Tipo.INT && sizeBi.TipoDato == Tipo.INT)
                            {
                                retorno.Arreglo.SizeUni = (int)sizeUni.Dato;
                                retorno.Arreglo.SizeBi = (int)sizeBi.Dato;
                                retorno.Arreglo.Dimension = TipoArreglo.BI;
                            }
                            else
                            {
                                Main.Imprimir(String.Format("Las dimensiones del arreglo deben especificarse como enteros. ({0},{1})", sizeUni.Posicion.Fila, sizeUni.Posicion.Columna));
                            }
                            break;
                        case 3:
                            //Es un arreglo tridimensional
                            retorno.Arreglo.Dimension = TipoArreglo.TRI;
                            //Opera los tres nodos
                            sizeUni = Interpretar(operRaiz.ChildNodes[0]);
                            sizeBi = Interpretar(operRaiz.ChildNodes[1]);
                            var sizeTri = Interpretar(operRaiz.ChildNodes[2]);
                            //Verifica que todos sean un entero
                            if (sizeUni.TipoDato == Tipo.INT && sizeBi.TipoDato == Tipo.INT && sizeTri.TipoDato == Tipo.INT)
                            {
                                retorno.Arreglo.SizeUni = (int)sizeUni.Dato;
                                retorno.Arreglo.SizeBi = (int)sizeBi.Dato;
                                retorno.Arreglo.SizeTri = (int)sizeTri.Dato;
                                retorno.Arreglo.Dimension = TipoArreglo.TRI;
                            }
                            else
                            {
                                Main.Imprimir(String.Format("Las dimensiones del arreglo deben especificarse como enteros. ({0},{1})", sizeUni.Posicion.Fila, sizeUni.Posicion.Columna));
                            }
                            break;
                        default:
                            Main.Imprimir(String.Format("Unexpected error. Recorrido: {0}", "DIMENSION_LIST"));
                            return new Simbolo();
                    }
                    break;
                case "OPER":
                    switch (operRaiz.ChildNodes.Count)
                    {
                        case 1:
                            switch (operRaiz.ChildNodes[0].Term.Name)
                            {
                                case "OPER":
                                    retorno = Interpretar(operRaiz.ChildNodes[0]);
                                    break;
                                case "tkREAL":
                                    //Determina el tipo  (double o int)
                                    if (operRaiz.ChildNodes[0].Token.Value.GetType().Equals(typeof(int)))
                                    {
                                        retorno.TipoDato = Tipo.INT;
                                        retorno.Dato = operRaiz.ChildNodes[0].Token.Value;
                                    }
                                    if (operRaiz.ChildNodes[0].Token.Value.GetType().Equals(typeof(double)))
                                    {
                                        retorno.TipoDato = Tipo.DOUBLE;
                                        retorno.Dato = operRaiz.ChildNodes[0].Token.Value;
                                    }
                                    break;
                                case "tkCHAR":
                                    retorno.TipoDato = Tipo.CHAR;
                                    retorno.Dato = (char)operRaiz.ChildNodes[0].Token.Value;
                                    break;
                                case "tkSTR":
                                    retorno.TipoDato = Tipo.STRING;
                                    retorno.Dato = operRaiz.ChildNodes[0].Token.Value.ToString();
                                    break;
                                case "CSTBOOL":
                                    retorno.TipoDato = Tipo.BOOLEAN;
                                    retorno.Dato = operRaiz.ChildNodes[0].Token.Value;
                                    break;
                                case "tkVAR":
                                    return Entorno.BuscarSimbolo(operRaiz.ChildNodes[0].Token.Text);
                                case "CALL":
                                    //Esta es una llamada en la misma clase
                                    //Se debe verificar que el nombre de la función exista 
                                    //en la coleccion de entornos de la ClaseLocal

                                    //Esta función puede devolver un nulo, por lo que se debe verificar que no sea nulo
                                    Funcion func = ClaseLocal.BuscarFuncion(operRaiz.ChildNodes[0].ChildNodes[0].Token.Text.ToLower());
                                    if (func==null)
                                    {
                                        Main.Imprimir(String.Format("No existe el método {0} : ({1}, {2})", operRaiz.ChildNodes[0].ChildNodes[0].Token.Text.ToLower(), operRaiz.ChildNodes[0].ChildNodes[0].Token.Location.Line + 1, operRaiz.ChildNodes[0].ChildNodes[0].Token.Location.Column + 1));
                                        return new Simbolo();
                                    }
                                    //Cuenta el numero de nodos para determinar si es una llamada con parametros o no
                                    switch (operRaiz.ChildNodes[0].ChildNodes.Count)
                                    {
                                        //Busca en los métodoos
                                        case 1:
                                            //No tiene parametros
                                            //Verifica que la función recuperada no tenga parametros tampoco
                                            if (func.FuncParSym.Count > 0)
                                            {
                                                Main.Imprimir(String.Format("Se esperaba una lista de parametros {0} : ({1}, {2})", operRaiz.ChildNodes[0].ChildNodes[0].Token.Text.ToLower(), operRaiz.ChildNodes[0].ChildNodes[0].Token.Location.Line + 1, operRaiz.ChildNodes[0].ChildNodes[0].Token.Location.Column + 1));
                                                return new Simbolo();
                                            }
                                            //Ejecuta la funcion
                                            func.Ejecutar();
                                            break;

                                        case 2:
                                            //Tiene parametros
                                            //Verifica que la función recuperada tenga parametros
                                            if (func.FuncParSym.Count == 0)
                                            {
                                                Main.Imprimir(String.Format("No se esperaba una lista de parametros {0} : ({1}, {2})", operRaiz.ChildNodes[0].ChildNodes[0].Token.Text.ToLower(), operRaiz.ChildNodes[0].ChildNodes[0].Token.Location.Line + 1, operRaiz.ChildNodes[0].ChildNodes[0].Token.Location.Column + 1));
                                                return new Simbolo();
                                            }
                                            //Recorre el listado de parametros, recuperando los simbolos
                                            //Genera una lista de simbolos
                                            LinkedList<Simbolo> parSym = new LinkedList<Simbolo>();                                            //OPER, OPER, OPER
                                            foreach (var oper in operRaiz.ChildNodes[0].ChildNodes[1].ChildNodes)
                                            {
                                                parSym.AddLast(Interpretar(oper));
                                            }
                                            //Verifica que posea la misma cantidad de parametros
                                            if (parSym.Count != func.FuncParSym.Count)
                                            {
                                                Main.Imprimir(String.Format("Se esperaban más parametros {0} : ({1}, {2})", operRaiz.ChildNodes[0].ChildNodes[0].Token.Text.ToLower(), operRaiz.ChildNodes[0].ChildNodes[0].Token.Location.Line + 1, operRaiz.ChildNodes[0].ChildNodes[0].Token.Location.Column + 1));
                                                return new Simbolo();
                                            }
                                            //Compara el tipo de datos recuperados en parSym con el 
                                            //diccionario de Simbolo-Parametros que posee la función
                                            //Si todo es correcto agregará la lista de simbolo de la función 
                                            //los simbolo recuperados
                                            for (int i = 0;i<parSym.Count;i++)
                                            {
                                                Simbolo symPar = parSym.ElementAt(i);
                                                var dicSymPar = func.FuncParSym.ElementAt(i);
                                                //Verifica que el tipo sea el mismo
                                                if (symPar.TipoDato != dicSymPar.Value.TipoDato)
                                                {
                                                    Main.Imprimir(String.Format("No coinciden los tipos {0} : ({1}, {2})", dicSymPar.Key.ToString(), symPar.Posicion != null ? symPar.Posicion.Fila : 0, symPar.Posicion != null ? symPar.Posicion.Columna : 0));
                                                    return new Simbolo();
                                                }
                                                //Si es un array deberá de verificar que TODAS las dimensiones sean las mismas
                                                if (symPar.TipoDato > Tipo.CLASE)
                                                {
                                                    //Opera las dimensiones del simbolo del diccionario
                                                    Simbolo dimensionesDic = Interpretar(dicSymPar.Value.Arr);
                                                    //Se asegura que las dimensiones no sean nulas
                                                    if (symPar.Arreglo == null)
                                                    {
                                                        Main.Imprimir(String.Format("No se ha inicializado los tipos {0} : ({1}, {2})", dicSymPar.Key.ToString(), symPar.Posicion != null ? symPar.Posicion.Fila : 0, symPar.Posicion != null ? symPar.Posicion.Columna : 0));
                                                        return new Simbolo();
                                                    }
                                                    //Verifica que las dimensiones sean las mimsas
                                                    if (symPar.Arreglo.Dimension != dimensionesDic.Arreglo.Dimension)
                                                    {
                                                        Main.Imprimir(String.Format("Las dimensiones no coinciden {0} : ({1}, {2})", dicSymPar.Key.ToString(), symPar.Posicion != null ? symPar.Posicion.Fila : 0, symPar.Posicion != null ? symPar.Posicion.Columna : 0));
                                                        return new Simbolo();
                                                    }
                                                    if (symPar.Arreglo.SizeUni != dimensionesDic.Arreglo.SizeUni)
                                                    {
                                                        Main.Imprimir(String.Format("El largo unidemensional no coincide  {0} : ({1}, {2})", dicSymPar.Key.ToString(), symPar.Posicion != null ? symPar.Posicion.Fila : 0, symPar.Posicion != null ? symPar.Posicion.Columna : 0));
                                                        return new Simbolo();
                                                    }
                                                    if (symPar.Arreglo.SizeBi != dimensionesDic.Arreglo.SizeBi)
                                                    {
                                                        Main.Imprimir(String.Format("El largo bidemensional no coincide  {0} : ({1}, {2})", dicSymPar.Key.ToString(), symPar.Posicion != null ? symPar.Posicion.Fila : 0, symPar.Posicion != null ? symPar.Posicion.Columna : 0));
                                                        return new Simbolo();
                                                    }
                                                    if (symPar.Arreglo.SizeTri != dimensionesDic.Arreglo.SizeTri)
                                                    {
                                                        Main.Imprimir(String.Format("El largo tridmensional no coincide  {0} : ({1}, {2})", dicSymPar.Key.ToString(), symPar.Posicion != null ? symPar.Posicion.Fila : 0, symPar.Posicion != null ? symPar.Posicion.Columna : 0));
                                                        return new Simbolo();
                                                    }
                                                }
                                                //Llegado este punto ya se verificó que los tipos son iguales
                                                //y que si es un array las dimensiones coinciden 
                                                //Se procederá a agregar el simbolo symPar al diccionario de Simbolos de la función
                                                func.FuncSym.Add(dicSymPar.Key, symPar);
                                            }
                                            //Ya se especificaron los  parametros a la función, se procederá a ejecutar la función
                                            func.Ejecutar();
                                            break;
                                    }
                                    //Llegado a este punto todo debió haber salido bien, por lo tanto
                                    //se realiza una copia del simbolo de retorno de la función
                                    Simbolo returnDataSymFunc = new Simbolo()
                                    {
                                        Arr = func.ReturnData.Arr,
                                        Dato = func.ReturnData.Dato,
                                        TipoDato = func.ReturnData.TipoDato,
                                        Oper = func.ReturnData.Oper,
                                        EsPrivado = func.EsPrivado
                                    };
                                    //Realiza una copia del arrreglo si no es nulo
                                    if (func.ReturnData.Arreglo != null)
                                    {
                                        returnDataSymFunc.Arreglo.Dimension = func.ReturnData.Arreglo.Dimension;
                                        returnDataSymFunc.Arreglo.SizeUni = func.ReturnData.Arreglo.SizeUni;
                                        returnDataSymFunc.Arreglo.SizeBi = func.ReturnData.Arreglo.SizeBi;
                                        returnDataSymFunc.Arreglo.SizeTri = func.ReturnData.Arreglo.SizeTri;
                                    }
                                    //Asigna el simbolo copiado al retorno
                                    retorno = returnDataSymFunc;
                                    break;
                            }
                            retorno.Posicion = new Posicion(operRaiz.ChildNodes[0].Token.Location.Line, operRaiz.ChildNodes[0].Token.Location.Column);
                            break;
                        case 2:
                            //Operación unaria
                            //NEGACION
                            if (operRaiz.ChildNodes[0].Term.Name.Equals("NOT"))
                            {
                                retorno = Interpretar(operRaiz.ChildNodes[1]);
                                if (retorno.TipoDato == Tipo.BOOLEAN)
                                {
                                    retorno.Dato = !(bool)retorno.Dato;
                                }
                                else
                                {
                                    Console.WriteLine("La operación 'negación' solo es posible con valores booleanos");
                                }
                            }
                            //INCREMENTO, DECREMENTO
                            if (operRaiz.ChildNodes[0].Term.Name.Equals("OPER"))
                            {
                                retorno = Interpretar(operRaiz.ChildNodes[0]);
                                //Determina si el tipo de dato es el correcto
                                if (retorno.TipoDato == Tipo.INT || retorno.TipoDato == Tipo.DOUBLE || retorno.TipoDato == Tipo.CHAR)
                                {
                                    switch (operRaiz.ChildNodes[0].Term.Name)
                                    {
                                        case "INCREMENTO":
                                            retorno.Dato = retorno.TipoDato == Tipo.INT || retorno.TipoDato == Tipo.CHAR ? ((int)retorno.Dato) + 1 : ((double)retorno.Dato) + 1;
                                            retorno.TipoDato = retorno.TipoDato == Tipo.INT || retorno.TipoDato == Tipo.CHAR ? Tipo.INT : Tipo.DOUBLE;
                                            break;
                                        case "DECREMENTO":
                                            retorno.Dato = retorno.TipoDato == Tipo.INT || retorno.TipoDato == Tipo.CHAR ? ((int)retorno.Dato) - 1 : ((double)retorno.Dato) - 1;
                                            retorno.TipoDato = retorno.TipoDato == Tipo.INT || retorno.TipoDato == Tipo.CHAR ? Tipo.INT : Tipo.DOUBLE;
                                            break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("La operación 'incremento/decremento' solo es posible con int, double o char");
                                }

                            }
                            //DECLARACION DE CLASE
                            if (operRaiz.ChildNodes[0].Term.Name.Equals("NEW"))
                            {
                                retorno.TipoDato = Tipo.CLASE;
                                string nombreClase = operRaiz.ChildNodes[1].Token.Text.ToLower();
                                //Busca en la lista de clases si existe una clases con el nombre anterior
                                //Clona la clase en cuestión y la asigna a retorno.Dato (No se puede clonar :'v)
                                //Va a buscar entre la lista de clases si existe una clase con el nombre que se especificó en el segundo nodo
                                if (Clases.ContainsKey(operRaiz.ChildNodes[0].ChildNodes[1].Token.Text.ToLower()))
                                {
                                    //especifica el tipo de dato del retorno
                                    retorno.TipoDato = Tipo.CLASE;
                                    //Copia la clase que tiene el nombre de la instancia
                                    retorno.Dato = Clase.Copiar(Clases[operRaiz.ChildNodes[0].ChildNodes[1].Token.Text.ToLower()]);
                                }
                                //Aviso que no funciona bien esta mierda alv
                                Main.Imprimir("Las instancias no funcionan correctamente en esta versión.");
                            }
                            //Variables
                            if (operRaiz.ChildNodes[0].Term.Name.Equals("tkVAR"))
                            {
                                string nombreVariable = operRaiz.ChildNodes[1].Token.Text.ToLower();
                                //Busca en la lista de simbolos, verifica que exista el nombre de la variable
                                var varArr = this.Entorno.BuscarSimbolo(operRaiz.ChildNodes[0].Token.Text);
                                //Determina si es un array
                                if (varArr.TipoDato >= Tipo.INTARR)
                                {
                                    var DimensionSymbol = Interpretar(operRaiz.ChildNodes[1]);
                                    //Se asegura que sean de la misma dimensiones y que el tamaño recuperado sea menor al de la variable
                                    if (varArr.Arreglo.Dimension == DimensionSymbol.Arreglo.Dimension)
                                    {
                                        switch (varArr.Arreglo.Dimension)
                                        {
                                            case TipoArreglo.UNI:
                                                if (DimensionSymbol.Arreglo.SizeUni >= varArr.Arreglo.SizeUni)
                                                {
                                                    Main.Imprimir(String.Format("El indice específicado excede el tamaño del arreglo: {0}, ({1},{2})", nombreVariable, operRaiz.ChildNodes[0].Token.Location.Line + 1, operRaiz.ChildNodes[0].Token.Location.Column + 1));
                                                    return new Simbolo();
                                                }
                                                switch (retorno.TipoDato)
                                                {
                                                    case Tipo.INTARR:
                                                        retorno.Dato = (varArr.Dato as int[])[DimensionSymbol.Arreglo.SizeUni];
                                                        break;
                                                    case Tipo.STRINGARR:
                                                        retorno.Dato = (varArr.Dato as string[])[DimensionSymbol.Arreglo.SizeUni];
                                                        break;
                                                    case Tipo.DOUBLEARR:
                                                        retorno.Dato = (varArr.Dato as double[])[DimensionSymbol.Arreglo.SizeUni];
                                                        break;
                                                    case Tipo.CHARARR:
                                                        retorno.Dato = (varArr.Dato as char[])[DimensionSymbol.Arreglo.SizeUni];
                                                        break;
                                                    case Tipo.BOOLEANARR:
                                                        retorno.Dato = (varArr.Dato as bool[])[DimensionSymbol.Arreglo.SizeUni];
                                                        break;
                                                    case Tipo.CLASEARR:
                                                        retorno.Dato = (varArr.Dato as Clase[])[DimensionSymbol.Arreglo.SizeUni];
                                                        break;
                                                    default:
                                                        Main.Imprimir(String.Format("No sé we , algo pasó cuando se buscaban los arreglos: {0}, ({1},{2})", nombreVariable, operRaiz.ChildNodes[0].Token.Location.Line + 1, operRaiz.ChildNodes[0].Token.Location.Column + 1));
                                                        return new Simbolo();
                                                }
                                                break;
                                            case TipoArreglo.BI:
                                                if (DimensionSymbol.Arreglo.SizeUni >= varArr.Arreglo.SizeUni && DimensionSymbol.Arreglo.SizeBi >= varArr.Arreglo.SizeBi)
                                                {
                                                    Main.Imprimir(String.Format("El indice específicado excede el tamaño del arreglo: {0}, ({1},{2})", nombreVariable, operRaiz.ChildNodes[0].Token.Location.Line + 1, operRaiz.ChildNodes[0].Token.Location.Column + 1));
                                                    return new Simbolo();
                                                }
                                                switch (retorno.TipoDato)
                                                {
                                                    case Tipo.INTARR:
                                                        retorno.Dato = (varArr.Dato as int[][])[DimensionSymbol.Arreglo.SizeUni][DimensionSymbol.Arreglo.SizeBi];
                                                        break;
                                                    case Tipo.STRINGARR:
                                                        retorno.Dato = (varArr.Dato as string[][])[DimensionSymbol.Arreglo.SizeUni][DimensionSymbol.Arreglo.SizeBi];
                                                        break;
                                                    case Tipo.DOUBLEARR:
                                                        retorno.Dato = (varArr.Dato as double[][])[DimensionSymbol.Arreglo.SizeUni][DimensionSymbol.Arreglo.SizeBi];
                                                        break;
                                                    case Tipo.CHARARR:
                                                        retorno.Dato = (varArr.Dato as char[][])[DimensionSymbol.Arreglo.SizeUni][DimensionSymbol.Arreglo.SizeBi];
                                                        break;
                                                    case Tipo.BOOLEANARR:
                                                        retorno.Dato = (varArr.Dato as bool[][])[DimensionSymbol.Arreglo.SizeUni][DimensionSymbol.Arreglo.SizeBi];
                                                        break;
                                                    case Tipo.CLASEARR:
                                                        retorno.Dato = (varArr.Dato as Clase[][])[DimensionSymbol.Arreglo.SizeUni][DimensionSymbol.Arreglo.SizeBi];
                                                        break;
                                                    default:
                                                        Main.Imprimir(String.Format("No sé we , algo pasó cuando se buscaban los arreglos: {0}, ({1},{2})", nombreVariable, operRaiz.ChildNodes[0].Token.Location.Line + 1, operRaiz.ChildNodes[0].Token.Location.Column + 1));
                                                        return new Simbolo();
                                                }
                                                break;
                                            case TipoArreglo.TRI:
                                                if (DimensionSymbol.Arreglo.SizeUni >= varArr.Arreglo.SizeUni && DimensionSymbol.Arreglo.SizeBi >= varArr.Arreglo.SizeBi && DimensionSymbol.Arreglo.SizeTri >= varArr.Arreglo.SizeTri)
                                                {
                                                    Main.Imprimir(String.Format("El indice específicado excede el tamaño del arreglo: {0}, ({1},{2})", nombreVariable, operRaiz.ChildNodes[0].Token.Location.Line + 1, operRaiz.ChildNodes[0].Token.Location.Column + 1));
                                                    return new Simbolo();
                                                }
                                                switch (retorno.TipoDato)
                                                {
                                                    case Tipo.INTARR:
                                                        retorno.Dato = (varArr.Dato as int[][][])[DimensionSymbol.Arreglo.SizeUni][DimensionSymbol.Arreglo.SizeBi][DimensionSymbol.Arreglo.SizeTri];
                                                        break;
                                                    case Tipo.STRINGARR:
                                                        retorno.Dato = (varArr.Dato as string[][][])[DimensionSymbol.Arreglo.SizeUni][DimensionSymbol.Arreglo.SizeBi][DimensionSymbol.Arreglo.SizeTri];
                                                        break;
                                                    case Tipo.DOUBLEARR:
                                                        retorno.Dato = (varArr.Dato as double[][][])[DimensionSymbol.Arreglo.SizeUni][DimensionSymbol.Arreglo.SizeBi][DimensionSymbol.Arreglo.SizeTri];
                                                        break;
                                                    case Tipo.CHARARR:
                                                        retorno.Dato = (varArr.Dato as char[][][])[DimensionSymbol.Arreglo.SizeUni][DimensionSymbol.Arreglo.SizeBi][DimensionSymbol.Arreglo.SizeTri];
                                                        break;
                                                    case Tipo.BOOLEANARR:
                                                        retorno.Dato = (varArr.Dato as bool[][][])[DimensionSymbol.Arreglo.SizeUni][DimensionSymbol.Arreglo.SizeBi][DimensionSymbol.Arreglo.SizeTri];
                                                        break;
                                                    case Tipo.CLASEARR:
                                                        retorno.Dato = (varArr.Dato as Clase[][][])[DimensionSymbol.Arreglo.SizeUni][DimensionSymbol.Arreglo.SizeBi][DimensionSymbol.Arreglo.SizeTri];
                                                        break;
                                                    default:
                                                        Main.Imprimir(String.Format("No sé we , algo pasó cuando se buscaban los arreglos: {0}, ({1},{2})", nombreVariable, operRaiz.ChildNodes[0].Token.Location.Line + 1, operRaiz.ChildNodes[0].Token.Location.Column + 1));
                                                        return new Simbolo();
                                                }
                                                break;
                                        }
                                        retorno.TipoDato = varArr.TipoDato - 6;
                                    }
                                    else
                                    {
                                        Main.Imprimir(String.Format("Las dimensiones no coinciden: {0}, ({1},{2})", nombreVariable, operRaiz.ChildNodes[0].Token.Location.Line + 1, operRaiz.ChildNodes[0].Token.Location.Column + 1));
                                    }
                                }
                                else
                                {
                                    Main.Imprimir(String.Format("No se encontró la variable: {0}, ({1},{2})", nombreVariable, operRaiz.ChildNodes[0].Token.Location.Line+1,operRaiz.ChildNodes[0].Token.Location.Column+1));
                                }
                            }
                            break;
                        case 3:
                            //Operación binaria
                            switch (operRaiz.ChildNodes[1].Term.Name)
                            {
                                case "MAS":
                                    return Suma.Interpretar(operRaiz, this);
                                case "MENOS":
                                    return Resta.Interpretar(operRaiz, this);
                                case "POR":
                                    return Producto.Interpretar(operRaiz, this);
                                case "DIVISION":
                                    return Division.Interpretar(operRaiz, this);
                                case "POTENCIA":
                                    return Potencia.Interpretar(operRaiz, this);
                                case "IGUAL":
                                    return Relacionar.Interpretar(operRaiz, this, TipoRel.IGUAL);
                                case "DIFERENTE":
                                    return Relacionar.Interpretar(operRaiz, this, TipoRel.DIFERENTE);
                                case "MAYOR":
                                    return Relacionar.Interpretar(operRaiz, this, TipoRel.MAYOR);
                                case "MENOR":
                                    return Relacionar.Interpretar(operRaiz, this, TipoRel.MENOR);
                                case "MAYOR_IGUAL":
                                    return Relacionar.Interpretar(operRaiz, this, TipoRel.MAYOR_IGUAL);
                                case "MENOR_IGUAL":
                                    return Relacionar.Interpretar(operRaiz, this, TipoRel.MENOR_IGUAL);
                                case "DOT":
                                    //Esto solo es posible en clases, por lo tanto, se debe determinar que el nodo
                                    //izquierdo sea de tipo Clase
                                    //Luego se deber determinar que el hijo del nodo derecho es un CALL o no
                                    //Si es un CALL se deberá buscar en la lista de funciones
                                    //Si no lo es , se deberá buscar en la lista de variables

                                    //Interpreta el lado izquierdo
                                    Simbolo symizqClass = Interpretar(operRaiz.ChildNodes[0]);
                                    //Verifica que lo se haya interpertado sea una clase
                                    if (symizqClass.TipoDato != Tipo.CLASE)
                                    {
                                        Main.Imprimir(String.Format("Esta operación solo se acepta en tipo de datos Clases: ({0}, {1})", operRaiz.ChildNodes[0].Token.Location.Line + 1, operRaiz.ChildNodes[0].Token.Location.Column + 1));
                                        return new Simbolo();
                                    }
                                    //Verifica que el dato no sea nulo
                                    if (symizqClass.Dato == null)
                                    {
                                        Main.Imprimir(String.Format("La variable no se ha inicializado: ({0}, {1})", operRaiz.ChildNodes[0].Token.Location.Line + 1, operRaiz.ChildNodes[0].Token.Location.Column + 1));
                                        return new Simbolo();
                                    }
                                    //Determina si es una llamada o una búsqueda de atributo
                                    if (operRaiz.ChildNodes[0].Term.Name.Equals("CALL"))
                                    {
                                        //Se debe determinar cuál 
                                    }
                                    else
                                    {
                                        //Verifica que el nodo derecho sea una hoja
                                        //if (operRaiz.ChildNodes[1].ChildNodes.Count != 0)
                                        //{
                                        //    Main.Imprimir(String.Format("Se esperaba un identificador:  ({0}, {1})", operRaiz.ChildNodes[0].Token.Location.Line + 1, operRaiz.ChildNodes[0].Token.Location.Column + 1));
                                        //    return new Simbolo();
                                        //}
                                        //Verifica que el nodo derecho sea una variable
                                        if (!operRaiz.ChildNodes[1].ChildNodes[0].Term.Name.Equals("tkVAR"))
                                        {
                                            Main.Imprimir(String.Format("Se esperaba un identificador:  ({0}, {1})", operRaiz.ChildNodes[0].Token.Location.Line + 1, operRaiz.ChildNodes[0].Token.Location.Column + 1));
                                            return new Simbolo();
                                        }
                                        //Hay al menos una clase ahí dentro
                                        Clase classIzq = symizqClass.Dato as Clase;
                                        //Busca la variable
                                        if (!classIzq.ClaseSym.ContainsKey(operRaiz.ChildNodes[1].Token.Text.ToLower()))
                                        {
                                            Main.Imprimir(String.Format("No existe el atributo {2} : ({0}, {1})", operRaiz.ChildNodes[0].Token.Location.Line + 1, operRaiz.ChildNodes[0].Token.Location.Column + 1, operRaiz.ChildNodes[1].Token.Text.ToLower()));
                                            return new Simbolo();
                                        }
                                        //Operara el simbolo de clase y retornará un símbolo
                                        Simbolo AuxSimbolo = classIzq.ClaseSym[operRaiz.ChildNodes[1].Token.Text.ToLower()];
                                        //Determina si el simbolo es visible al exterior
                                        if (classIzq.ClaseSym[operRaiz.ChildNodes[1].Token.Text.ToLower()].EsPrivado)
                                        {
                                            Main.Imprimir(String.Format("El atributo no es visible {2} : ({0}, {1})", operRaiz.ChildNodes[0].Token.Location.Line + 1, operRaiz.ChildNodes[0].Token.Location.Column + 1, operRaiz.ChildNodes[1].Token.Text.ToLower()));
                                            return new Simbolo();
                                        }
                                        retorno = AuxSimbolo;
                                    }
                                    break;
                                case "OR":
                                    Simbolo izqoper = Interpretar(operRaiz.ChildNodes[0]);
                                    Simbolo deroper = Interpretar(operRaiz.ChildNodes[2]);
                                    if (izqoper.TipoDato == Tipo.BOOLEAN && deroper.TipoDato == Tipo.BOOLEAN)
                                    {
                                        retorno.TipoDato = Tipo.BOOLEAN;
                                        retorno.Dato = ((bool)izqoper.Dato) || ((bool)deroper.Dato);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Solo se puede utilizar el operador OR con booleanos");
                                    }
                                    break;
                                case "AND":
                                    izqoper = Interpretar(operRaiz.ChildNodes[0]);
                                    deroper = Interpretar(operRaiz.ChildNodes[2]);
                                    if (izqoper.TipoDato == Tipo.BOOLEAN && deroper.TipoDato == Tipo.BOOLEAN)
                                    {
                                        retorno.TipoDato = Tipo.BOOLEAN;
                                        retorno.Dato = ((bool)izqoper.Dato) && ((bool)deroper.Dato);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Solo se puede utilizar el operador AND con booleanos");
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
                case "OPERLIST":
                    //Verifica que el arreglo tenga
                    if (operRaiz.ChildNodes.Count > 0)
                    {
                        //Especifica la dimension del arreglo
                        retorno.Arreglo.Dimension = TipoArreglo.UNI;
                        //Especifica el largo del arreglo
                        retorno.Arreglo.SizeUni = operRaiz.ChildNodes.Count;
                        //Contador que servirá para guardar los datos en los arreglos
                        int contador = 0;
                        //Largo del arreglo
                        int largo = operRaiz.ChildNodes.Count;
                        //Bandera que servirá para verificar el resto de tipo
                        //de datos de los nodos
                        Simbolo flag = Interpretar(operRaiz.ChildNodes[0]);
                        //Dependiendo de la bandera
                        //el retorno cambiará de tipo de arr
                        //se inicializa el dato
                        switch (flag.TipoDato)
                        {
                            case Tipo.INT:
                                retorno.TipoDato += 6;
                                retorno.Dato = new int[largo];
                                break;
                            case Tipo.STRING:
                                retorno.TipoDato += 6;
                                retorno.Dato = new string[largo];
                                break;
                            case Tipo.DOUBLE:
                                retorno.TipoDato += 6;
                                retorno.Dato = new double[largo];
                                break;
                            case Tipo.CHAR:
                                retorno.TipoDato += 6;
                                retorno.Dato = new char[largo];
                                break;
                            case Tipo.BOOLEAN:
                                retorno.TipoDato += 6;
                                retorno.Dato = new bool[largo];
                                break;
                            case Tipo.CLASE:
                                retorno.TipoDato += 6;
                                retorno.Dato = new Clase[largo];
                                break;
                            default:
                                Console.WriteLine("RuntimeError al crear arreglo, nivel 1 : el tipo de dato no es el correcto");
                                return new Simbolo();
                        }
                        //Recorre los nodos de operlist, recuperando los datos
                        //Verifica que sean del mismo tipo
                        //Y luego añade los datos recuperados al arreglo
                        foreach (var lst in operRaiz.ChildNodes)
                        {
                            //Recupera el operador del nodo
                            var symlst = Interpretar(lst);
                            //Verifica que el nodo recuperado sea el
                            //del mismo tipo que la bandera
                            if (symlst.TipoDato == flag.TipoDato)
                            {
                                //Verifica que no se exceda el tamaño
                                if (contador < largo)
                                { 
                                    switch (retorno.TipoDato)
                                    {
                                        case Tipo.INTARR:
                                            (retorno.Dato as int[])[contador++] = (int)symlst.Dato;
                                            break;
                                        case Tipo.STRINGARR:
                                            (retorno.Dato as string[])[contador++] = (string)symlst.Dato;
                                            break;
                                        case Tipo.DOUBLEARR:
                                            (retorno.Dato as double[])[contador++] = (double)symlst.Dato;
                                            break;
                                        case Tipo.CHARARR:
                                            (retorno.Dato as char[])[contador++] = (char)symlst.Dato;
                                            break;
                                        case Tipo.BOOLEANARR:
                                            (retorno.Dato as bool[])[contador++] = (bool)symlst.Dato;
                                            break;
                                        case Tipo.CLASEARR:
                                            (retorno.Dato as Clase[])[contador++] = (Clase)symlst.Dato;
                                            break;
                                        default:
                                            Console.WriteLine("RuntimeError al crear arreglo, nivel 2: No se puede guardar el dato en el arreglo");
                                            return new Simbolo();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Indice fuera de borde");
                                    return new Simbolo();
                                }
                            }
                            else
                            {
                                Console.WriteLine("El arreglo debe ser del mismo tipo de dato");
                                return new Simbolo();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("El arreglo no puede estar vacío");
                        return new Simbolo();
                    }
                    break;
                case "ARRCONTENT_LIST":
                    //Recupera el primero nodo el cual servirá como bandera
                    //para homogeneizar el resto de nodos
                    string strflag = operRaiz.ChildNodes[0].Term.Name;
                    switch (strflag)
                    {
                        case "ARRCONTENT_LIST":
                            //Contador tercera dimension
                            int contador3 = 0;
                            //Largo tercera dimension
                            int largo3 = 0;
                            //Es de tres dimenstiones
                            //Especifica el tamaño del retorno
                            retorno.Arreglo.Dimension = TipoArreglo.TRI;
                            //Especifica el largo de la tercera dimension
                            retorno.Arreglo.SizeTri = operRaiz.ChildNodes.Count;
                            //Recupera el primer simbolo que servirá como flag
                            Simbolo flagArr = Interpretar(operRaiz.ChildNodes[0]);
                            //Se asegura que lo recuperado sea un array bidimensional
                            if (flagArr.TipoDato >= Tipo.INTARR && flagArr.Arreglo.Dimension == TipoArreglo.BI)
                            {
                                //Especifica el tipo que se retornará
                                retorno.TipoDato = flagArr.TipoDato;
                                //Especifica el tamaño del arreglo unidemensional
                                retorno.Arreglo.SizeUni = flagArr.Arreglo.SizeUni;
                                //Especifica el tamaño del arreglo bidimensional
                                retorno.Arreglo.SizeBi = flagArr.Arreglo.SizeBi;
                                //Inicializa el dato
                                switch (retorno.TipoDato)
                                {
                                    case Tipo.INTARR:
                                        retorno.Dato = new int[largo3][][];
                                        break;
                                    case Tipo.STRINGARR:
                                        retorno.Dato = new string[largo3][][];
                                        break;
                                    case Tipo.DOUBLEARR:
                                        retorno.Dato = new double[largo3][][];
                                        break;
                                    case Tipo.CHARARR:
                                        retorno.Dato = new char[largo3][][];
                                        break;
                                    case Tipo.BOOLEANARR:
                                        retorno.Dato = new bool[largo3][][];
                                        break;
                                    case Tipo.CLASEARR:
                                        retorno.Dato = new Clase[largo3][][];
                                        break;
                                    default:
                                        Console.WriteLine("RuntimeError al crear arreglo, nivel 1: No se puede crear el dato en el arreglo tridimensional");
                                        return new Simbolo();
                                }
                                //Guarda los arreglos bidimensionales en el arreglo tridimensional
                                foreach (var lst in operRaiz.ChildNodes)
                                {
                                    //Recupera el simbolo unidemensional
                                    Simbolo symUni = Interpretar(lst);
                                    //Se asegura que el simbolo recuperado se igual que el flag
                                    if (symUni.TipoDato == flagArr.TipoDato && symUni.Arreglo.Dimension == flagArr.Arreglo.Dimension && symUni.Arreglo.SizeUni == flagArr.Arreglo.SizeUni)
                                    {
                                        switch (symUni.TipoDato)
                                        {
                                            case Tipo.INTARR:
                                                (retorno.Dato as int[][][])[contador3++] = symUni.Dato as int[][];
                                                break;
                                            case Tipo.STRINGARR:
                                                (retorno.Dato as string[][][])[contador3++] = symUni.Dato as string[][];
                                                break;
                                            case Tipo.DOUBLEARR:
                                                (retorno.Dato as double[][][])[contador3++] = symUni.Dato as double[][];
                                                break;
                                            case Tipo.CHARARR:
                                                (retorno.Dato as char[][][])[contador3++] = symUni.Dato as char[][];
                                                break;
                                            case Tipo.BOOLEANARR:
                                                (retorno.Dato as bool[][][])[contador3++] = symUni.Dato as bool[][];
                                                break;
                                            case Tipo.CLASEARR:
                                                (retorno.Dato as Clase[][][])[contador3++] = symUni.Dato as Clase[][];
                                                break;
                                            default:
                                                Console.WriteLine("RuntimeError al crear arreglo, nivel 2: No se puede guardar el dato en el arreglo bidemensional");
                                                return new Simbolo();
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("RuntimeError al crear arreglo: los tipos del arreglo unidimensional no coinciden entre sí");
                                        return new Simbolo();
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("RuntimeError al crear arreglo: los tipos del arreglo bidimensional no son unidemensionales");
                                return new Simbolo();
                            }
                            break;
                        case "OPERLIST":
                            //Contador segunda dimension
                            int contador2 = 0;
                            //Largo segunda dimension
                            int largo2 = operRaiz.ChildNodes.Count;
                            //Es de dos dimensiones
                            //Especifica el tamaño del retorno
                            retorno.Arreglo.Dimension = TipoArreglo.BI;
                            //Especifica el largo de la segunda dimension
                            retorno.Arreglo.SizeBi = operRaiz.ChildNodes.Count;
                            //Recupera el primer simbolo que servirá como flag
                            Simbolo flagSymbol = Interpretar(operRaiz.ChildNodes[0]);
                            //Se asegura que lo recuperado sea un array unidimensional
                            if (flagSymbol.TipoDato >= Tipo.INTARR && flagSymbol.Arreglo.Dimension == TipoArreglo.UNI)
                            {
                                //Especifica el tipo que se retornará
                                retorno.TipoDato = flagSymbol.TipoDato;
                                //Especifica el tamaño del arreglo unidimensional
                                retorno.Arreglo.SizeUni = flagSymbol.Arreglo.SizeUni;
                                //Inicializa el dato
                                switch (retorno.TipoDato)
                                {
                                    case Tipo.INTARR:
                                        retorno.Dato = new int[largo2][];
                                        break;
                                    case Tipo.STRINGARR:
                                        retorno.Dato = new string[largo2][];
                                        break;
                                    case Tipo.DOUBLEARR:
                                        retorno.Dato = new double[largo2][];
                                        break;
                                    case Tipo.CHARARR:
                                        retorno.Dato = new char[largo2][];
                                        break;
                                    case Tipo.BOOLEANARR:
                                        retorno.Dato = new bool[largo2][];
                                        break;
                                    case Tipo.CLASEARR:
                                        retorno.Dato = new Clase[largo2][];
                                        break;
                                    default:
                                        Console.WriteLine("RuntimeError al crear arreglo, nivel 1: No se puede crear el dato en el arreglo bidemensional");
                                        return new Simbolo();
                                }
                                //Guarda los arreglos unidimensionales en el arreglo bidimensional
                                foreach (var lst in operRaiz.ChildNodes)
                                {
                                    //Recupera el simbolo unidemensional
                                    Simbolo symUni = Interpretar(lst);
                                    //Se asegura que el simbolo recuperado se igual que el flag
                                    if (symUni.TipoDato == flagSymbol.TipoDato && symUni.Arreglo.Dimension == flagSymbol.Arreglo.Dimension && symUni.Arreglo.SizeUni == flagSymbol.Arreglo.SizeUni)
                                    {
                                        switch (symUni.TipoDato)
                                        {                                            
                                            case Tipo.INTARR:
                                                (retorno.Dato as int[][])[contador2++] = symUni.Dato as int[];
                                                break;
                                            case Tipo.STRINGARR:
                                                (retorno.Dato as string[][])[contador2++] = symUni.Dato as string[];
                                                break;
                                            case Tipo.DOUBLEARR:
                                                (retorno.Dato as double[][])[contador2++] = symUni.Dato as double[];
                                                break;
                                            case Tipo.CHARARR:
                                                (retorno.Dato as char[][])[contador2++] = symUni.Dato as char[];
                                                break;
                                            case Tipo.BOOLEANARR:
                                                (retorno.Dato as bool[][])[contador2++] = symUni.Dato as bool[];
                                                break;
                                            case Tipo.CLASEARR:
                                                (retorno.Dato as Clase[][])[contador2++] = symUni.Dato as Clase[];
                                                break;
                                            default:
                                                Console.WriteLine("RuntimeError al crear arreglo, nivel 2: No se puede guardar el dato en el arreglo bidemensional");
                                                return new Simbolo();
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("RuntimeError al crear arreglo: los tipos del arreglo unidimensional no coinciden entre sí");
                                        return new Simbolo();
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("RuntimeError al crear arreglo: los tipos del arreglo bidimensional no son unidemensionales");
                                return new Simbolo();
                            }
                            break;
                        default:
                            Console.WriteLine("Error al crear arrglos multidmensionales");
                            return new Simbolo();
                    }
                    break;
            }
            return retorno;
        }
    }
}

