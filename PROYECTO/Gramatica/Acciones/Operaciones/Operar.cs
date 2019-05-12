using Irony.Parsing;
using PROYECTO.Gramatica.Entorno;
using System;

namespace PROYECTO.Gramatica.Acciones.Operaciones
{
    class Operar 
    {
        private IEntorno Entorno;
        public Operar(IEntorno entorno)
        {
            this.Entorno = entorno;
        }
        public Simbolo Interpretar(ParseTreeNode operRaiz)
        {
            Simbolo retorno = new Simbolo();
            switch (operRaiz.Term.Name)
            {
                case "DIMENSION_LIST":
                    switch (operRaiz.ChildNodes.Count)
                    {
                        case 1:
                            //Es un arreglo unidimensional
                            retorno.Arreglo.Dimension = TipoArreglo.UNI;
                            break;
                        case 2:
                            //Es un arreglo bidimensional
                            retorno.Arreglo.Dimension = TipoArreglo.BI;
                            break;
                        case 3:
                            //Es un arreglo tridimensional
                            retorno.Arreglo.Dimension = TipoArreglo.TRI;
                            break;
                        default:
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
                                    return this.Entorno.BuscarSimbolo(operRaiz.ChildNodes[0].Token.Text);
                                case "CALL":
                                    break;
                            }
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
                                //Clona la clase en cuestión y la asigna a retorno.Dato
                            }
                            //Variables
                            if (operRaiz.ChildNodes[0].Term.Name.Equals("tkVAR"))
                            {
                                string nombreVariable = operRaiz.ChildNodes[1].Token.Text.ToLower();
                                //Busca en la lista de simbolos, verifica que exista el nombre de la variable
                                var varArr = this.Entorno.BuscarSimbolo(operRaiz.ChildNodes[0].Token.Text);
                                //Determina si es un array
                                if (varArr.TipoDato >= Tipo.INTARR)
                                {/*
                            switch ()
                            {

                            }*/
                                }
                                //Determina las dimensiones del array
                                //Devuelve el simbolo
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
