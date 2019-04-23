﻿using Irony.Parsing;

namespace PROYECTO.Gramatica
{
    class Sintactico : Grammar
    {
        public Sintactico() : base(caseSensitive: false)
        {
            #region Regex
            #endregion

            #region Terminales
            /*-------- COMENTARIOS EN LÍNEA --------*/
            var LineComment = new CommentTerminal("LineComment", ">>", "\n", "\r\n");
            /*-------- COMENTARIOS EN BLOQUE --------*/
            var BlockComment = new CommentTerminal("BlockComment", "<-", "->");
            /*-------- NUMERO --------*/
            var Real = new NumberLiteral("tkREAL", NumberOptions.AllowSign);
            /*-------- CADENA --------*/
            var Cadena = new StringLiteral("tkSTR", "\"");
            /*-------- CARACTER --------*/
            var Caracter = new StringLiteral("tkCHAR", "'", StringOptions.IsChar);
            /*-------- PUNTUACION --------*/
            var SEMICOLON = ToTerm(";", "SEMICOLON");
            var COMMA = ToTerm(",", "COMMA");
            var DOT = ToTerm(".", "DOT");
            var COLON = ToTerm(":", "COLON");
            /*-------- AGRUPACION --------*/
            var PARIZQ = ToTerm("(", "PARIZQ");
            var PARDER = ToTerm(")", "PARDER");
            var LLVIZQ = ToTerm("{", "LLVIZQ");
            var LLVDER = ToTerm("}", "LLVDER");
            var CORIZQ = ToTerm("[", "CORIZQ");
            var CORDER = ToTerm("]", "CORDER");
            /*-------- BINARIOS --------*/
            var MAS = ToTerm("+", "MAS");
            var MENOS = ToTerm("-", "MENOS");
            var POR = ToTerm("*", "POR");
            var DIVISION = ToTerm("/", "DIVISION");
            var POTENCIA = ToTerm("^", "POTENCIA");
            var AND = ToTerm("&&", "AND");
            var OR = ToTerm("||", "OR");
            var IGUAL = ToTerm("==", "IGUAL");
            var DIFERENTE = ToTerm("!=", "DIFERENTE");
            var MAYOR_IGUAL = ToTerm(">=", "MAYOR_IGUAL");
            var MENOR_IGUAL = ToTerm("<=", "MENOR_IGUAL");
            var MAYOR = ToTerm(">", "MAYOR");
            var MENOR = ToTerm("<", "MENOR");
            var ASIGNACION = ToTerm("=", "ASIGNACION");
            /*-------- UNARIOS --------*/
            var INCREMENTO = ToTerm("++", "INCREMENTO");
            var DECREMENTO = ToTerm("--", "DECREMENTO");
            var NOT = ToTerm("!", "NOT");
            /*-------- RESERVADAS --------*/
            /*-------- DEFINICION DE TIPOS --------*/
            var INT = ToTerm("int", "INT");
            var CHAR = ToTerm("char", "CHAR");
            var STRING = ToTerm("string", "STRING");
            var DOUBLE = ToTerm("double", "DOUBLE");
            var BOOL = ToTerm("bool", "BOOL");
            var VOID = ToTerm("void", "VOID");
            var ARRAY = ToTerm("array", "ARRAY");
            /*-------- CLASES --------*/
            var PUBLICO = ToTerm("publico", "PUBLICO");
            var PRIVADO = ToTerm("privado", "PRIVADO");
            var OVERRIDE = ToTerm("override", "OVERRIDE");
            var IMPORTAR = ToTerm("importar", "IMPORTAR");
            var NEW = ToTerm("new", "NEW");
            var MAIN = ToTerm("main", "MAIN");
            var RETURN = ToTerm("return", "RETURN");
            var CLASE = ToTerm("clase", "CLASE");
            /*-------- NATIVA --------*/
            var PRINT = ToTerm("print", "PRINT");
            var SHOW = ToTerm("show", "SHOW");
            /*-------- CONDICIONALES Y LOOPS --------*/
            var IF = ToTerm("if", "IF");
            var ELSE = ToTerm("else", "ELSE");
            var FOR = ToTerm("for", "FOR");
            var REPEAT = ToTerm("repeat", "REPEAT");
            var WHILE = ToTerm("while", "WHILE");
            var COMPROBAR = ToTerm("comprobar", "COMPROBAR");
            var CASO = ToTerm("caso", "CASO");
            var DEFECTO = ToTerm("defecto", "DEFECTO");
            var SALIR = ToTerm("salir", "SALIR");
            var HACER = ToTerm("hacer", "HACER");
            var MIENTRAS = ToTerm("mientras", "MIENTRAS");
            var CONTINUAR = ToTerm("continuar", "CONTINUAR");
            /*-------- IDENTIFICADORES --------*/
            var Identificador = new IdentifierTerminal("tkID");
            var Variable = new IdentifierTerminal("tkVAR");
            #endregion

            #region No terminales
            /*PRODUCCIONES INICIALES*/
            NonTerminal INICIO = new NonTerminal("INICIO"),
            CLASE_STA = new NonTerminal("CLASE_STA"),
            CLASE_STA_LIST = new NonTerminal("CLASE_STA_LIST"),
            CLASE_STA_BODY = new NonTerminal("CLASE_STA_BODY"),
            IMPORTAR_STA = new NonTerminal("IMPORTAR_STA"),
            IMPORTAR_STA_LIST = new NonTerminal("IMPORTAR_STA_LIST"),
            FUNCION = new NonTerminal("FUNCION"),
            METODO = new NonTerminal("METODO"),
            INSTRUCCION = new NonTerminal("INSTRUCCION"),
            INSTRUCCION_LIST = new NonTerminal("INSTRUCCION"),
            VISIBILIDAD = new NonTerminal("VISIBILIDAD"),
            MAIN_STA = new NonTerminal("MAIN_STA"),
            OVER_STA = new NonTerminal("OVER_STA"),
            /*Contiene los tipos de datos utilizados*/
            DATATYPE = new NonTerminal("DATATYPE"),
            /*Contiene una lista de variables*/
            VARLIST = new NonTerminal("VARLIST"),
            /*Declara las variables normales*/
            DECLARACION = new NonTerminal("DECLARACION"),
            /*Asigna variables normales*/
            ASSIGNMENT = new NonTerminal("ASSIGNMENT"),
            /*Establece la dimensión de los arreglos*/
            DIMENSION = new NonTerminal("DIMENSION"),
            DIMENSION_LIST = new NonTerminal("DIMENSION_LIST"),
            /*Establece el contenido de los arreglos al declararse*/
            ARRCONTENT = new NonTerminal("ARRCONTENT"),
            ARRCONTENT_LIST = new NonTerminal("ARRCONTENT_LIST"),
            /*Específica la forma de las operaciones*/
            OPER = new NonTerminal("OPER"),
            /*Lista de operaciones :v*/
            OPERLIST = new NonTerminal("OPERLIST"),
            /*Producciones para if*/
            IF_STA = new NonTerminal("IF_STA"),
            ELSE_STA = new NonTerminal("ELSE_STA"),
            /*Producciones para un FOR*/
            FOR_STA = new NonTerminal("FOR_STA"),
            FOR_CND = new NonTerminal("FOR_CND"),
            /*Producciones para un REPEAT*/
            REPEAT_STA = new NonTerminal("REPEAT_STA"),
            WHILE_STA = new NonTerminal("WHILE_STA"),
            /*Producciones para un SWITCH*/
            SWITCH = new NonTerminal("SWITCH"),
            CASE_LST = new NonTerminal("CASE_LST"),
            CASE = new NonTerminal("CASE"),
            /*Producciones para un DO-WHILE*/
            DO = new NonTerminal("DO"),
            /*Produccion para un RETURN STATEMENT*/
            RTN_STA = new NonTerminal("RTN_STA");
            #endregion

            #region Producciones
            INICIO.Rule = MakePlusRule(INICIO, CLASE_STA);

            #region CLASE
            //ESTRUCTURA BASICA DE UN CLASE
            CLASE_STA.Rule = CLASE + Identificador + IMPORTAR_STA + LLVIZQ + CLASE_STA_LIST + LLVDER;
            //IMPORTACIONES
            IMPORTAR_STA_LIST.Rule = MakeListRule(IMPORTAR_STA_LIST, COMMA, Identificador);
            IMPORTAR_STA.Rule = IMPORTAR + IMPORTAR_STA_LIST | Empty;
            //CUERPO DE UNA CLASE, VARIABLES GLOBALES, FUNCIONES Y METODOS
            CLASE_STA_LIST.Rule = MakePlusRule(CLASE_STA_LIST, CLASE_STA_BODY);
            CLASE_STA_BODY.Rule = MAIN_STA | FUNCION | METODO | DECLARACION + SEMICOLON;
            //VISIBILIDAD DE UNA FUNCION, METODO O VARIABLE GLOBAL
            VISIBILIDAD.Rule = PUBLICO | PRIVADO | Empty;
            //SOBRECARGA DE MÉTODOS
            OVER_STA.Rule = OVERRIDE | Empty;
            //LISTA DE INSTRUCCIONES VÁLIDAS DENTRO DE FUNCIONES Y MÉTODOS
            INSTRUCCION_LIST.Rule = MakePlusRule(INSTRUCCION_LIST, INSTRUCCION);
            INSTRUCCION.Rule = DECLARACION + SEMICOLON | ASSIGNMENT + SEMICOLON | IF_STA | FOR_STA | REPEAT_STA | WHILE_STA | SWITCH | DO | CONTINUAR + SEMICOLON | SALIR + SEMICOLON | RTN_STA + SEMICOLON| Empty;
            #endregion

            #region FUNCION
            FUNCION.Rule = VISIBILIDAD + Identificador + DATATYPE + OVER_STA + PARIZQ + /*PARAMETRO_LST + */PARDER + LLVIZQ + INSTRUCCION_LIST + LLVDER
                | VISIBILIDAD + Identificador + ARRAY + DATATYPE + DIMENSION + OVER_STA + PARIZQ +/*+ PARAMETRO_LST +*/ PARDER + LLVIZQ + INSTRUCCION_LIST + LLVDER;
            #endregion
            
            #region METODO
            METODO.Rule = VISIBILIDAD + Identificador + VOID + OVER_STA + PARIZQ + /*PARAMETRO_LST +*/ PARDER + LLVIZQ + INSTRUCCION_LIST + LLVDER;
            #endregion

            #region RETURN  STATEMENT
            RTN_STA.Rule = RETURN + ASSIGNMENT;
            #endregion

            #region MAIN
            MAIN_STA.Rule = MAIN + PARIZQ + PARDER + LLVIZQ + INSTRUCCION_LIST + LLVDER;
            #endregion

            #region DECLARACION, ASIGNACION Y GET DE VARIABLES Y ARREGLOS
            DATATYPE.Rule = INT | CHAR | STRING | DOUBLE | BOOL | Identificador;
            //var, var , var
            VARLIST.Rule = MakeListRule(VARLIST, COMMA, Variable);
            //[oper][oper][oper]
            DIMENSION_LIST.Rule = MakePlusRule(DIMENSION_LIST, DIMENSION);
            DIMENSION.Rule = CORIZQ + OPER + CORDER;
            //{3,3} {{3,3},{3,3}} {{{3,3},{3,3}},{{3,3},{3,3}}}
            ARRCONTENT_LIST.Rule = MakeListRule(ARRCONTENT_LIST, COMMA, ARRCONTENT);
            ARRCONTENT.Rule = LLVIZQ + ARRCONTENT_LIST + LLVDER | LLVIZQ + OPERLIST + LLVDER;
            //int var, var, var;
            //int var, var  = oper;
            //int array var, var, var[oper][oper];
            //int array var, var, var[oper][oper] = {{3,3},{3,3}};
            DECLARACION.Rule = VISIBILIDAD + DATATYPE + VARLIST
                | VISIBILIDAD + DATATYPE + VARLIST + ASIGNACION + OPER
                | VISIBILIDAD + DATATYPE + ARRAY + VARLIST + DIMENSION_LIST
                | VISIBILIDAD + DATATYPE + ARRAY + VARLIST + DIMENSION_LIST + ASIGNACION + ARRCONTENT;
            //var = oper;
            //var[oper] = oper;
            ASSIGNMENT.Rule = Variable + ASIGNACION + OPER | Variable + DIMENSION_LIST + ASIGNACION + OPER | OPER;
            #endregion

            #region OPERACIONES
            OPER.Rule = OPER + MAS + OPER
                | OPER + MENOS + OPER
                | OPER + POR + OPER
                | OPER + DIVISION + OPER
                | OPER + POTENCIA + OPER
                | OPER + IGUAL + OPER
                | OPER + DIFERENTE + OPER
                | OPER + MAYOR + OPER
                | OPER + MENOR + OPER
                | OPER + MAYOR_IGUAL + OPER
                | OPER + MENOR_IGUAL + OPER
                | OPER + OR + OPER
                | OPER + AND + OPER
                | NOT + OPER
                | OPER + INCREMENTO
                | OPER + DECREMENTO
                | PARIZQ + OPER + PARDER
                | Real
                | Caracter
                | Cadena
                | "false"
                | "true"
                | "verdadero"
                | "falso"
                | Variable
                | Variable + DIMENSION_LIST
                /*Llamadas a función*/
                | NEW + Identificador + PARIZQ + PARDER;

            OPERLIST.Rule = MakeListRule(OPERLIST, COMMA, OPER);
            #endregion

            #region IF
            IF_STA.Rule = IF + PARIZQ + OPER + PARDER + LLVIZQ + INSTRUCCION_LIST + LLVDER + ELSE_STA;
            ELSE_STA.Rule = ELSE + LLVIZQ + INSTRUCCION_LIST + LLVDER | ELSE + IF_STA | Empty;
            #endregion

            #region FOR
            FOR_STA.Rule = FOR + PARIZQ + FOR_CND + PARDER + LLVIZQ + INSTRUCCION_LIST + LLVDER;
            FOR_CND.Rule = DECLARACION + SEMICOLON + OPER + SEMICOLON + ASSIGNMENT
                | ASSIGNMENT + SEMICOLON + OPER + SEMICOLON + ASSIGNMENT;
            #endregion

            #region REPEAT
            REPEAT_STA.Rule = REPEAT + PARIZQ + OPER + PARDER + LLVIZQ + INSTRUCCION_LIST + LLVDER;
            #endregion

            #region WHILE
            WHILE_STA.Rule = WHILE + PARIZQ + OPER + PARDER + LLVIZQ + INSTRUCCION_LIST + LLVDER;
            #endregion

            #region COMPROBAR
            SWITCH.Rule = COMPROBAR + PARIZQ + OPER + PARDER + LLVIZQ + CASE_LST + LLVDER;
            CASE_LST.Rule = MakePlusRule(CASE_LST, CASE);
            CASE.Rule = CASO + OPER + COLON + INSTRUCCION_LIST
                | DEFECTO + COLON + INSTRUCCION_LIST;
            #endregion

            #region DO-WHILE
            DO.Rule = HACER + LLVIZQ + INSTRUCCION_LIST + LLVDER + MIENTRAS + PARIZQ + OPER + PARDER + SEMICOLON;
            #endregion

            #endregion

            #region Preferencias
            /*-------- INICIO --------*/
            this.Root = INICIO;
            /*-------- COMENTARIOS IGNORADOS --------*/
            NonGrammarTerminals.Add(LineComment);
            NonGrammarTerminals.Add(BlockComment);
            /*-------- PUNTUACIÓN Y AGRUPACIÓN --------*/
            MarkPunctuation(SEMICOLON, COLON, DOT, COMMA, PARIZQ, PARDER, LLVIZQ, LLVDER, CORIZQ, CORDER);
            MarkPunctuation(IMPORTAR, CLASE, MAIN, IF, ELSE, FOR, COMPROBAR, CASO, DEFECTO, WHILE, REPEAT);
            MarkPunctuation(HACER, MIENTRAS, ARRAY);
            /*-------- ASOCIATIVIDAD --------*/
            RegisterOperators(0, Associativity.Left, OR);
            RegisterOperators(1, Associativity.Left, AND);
            RegisterOperators(2, Associativity.Left, IGUAL, DIFERENTE, MAYOR, MAYOR_IGUAL, MENOR, MENOR_IGUAL);
            RegisterOperators(3, Associativity.Right, NOT);
            RegisterOperators(4, Associativity.Left, MAS, MENOS);
            RegisterOperators(5, Associativity.Left, POR, DIVISION);
            RegisterOperators(6, Associativity.Left, POTENCIA);
            RegisterOperators(7, Associativity.Left, PARIZQ, PARDER);
            /*-------- PALABRAS RESERVADAS --------*/
            MarkReservedWords(INT.Text, CHAR.Text, STRING.Text, DOUBLE.Text,
                BOOL.Text, VOID.Text, ARRAY.Text, PUBLICO.Text, PRIVADO.Text,
                OVERRIDE.Text, IMPORTAR.Text, NEW.Text, MAIN.Text, RETURN.Text,
                PRINT.Text, SHOW.Text, IF.Text, ELSE.Text, FOR.Text, REPEAT.Text,
                WHILE.Text, COMPROBAR.Text, CASO.Text, DEFECTO.Text, SALIR.Text,
                HACER.Text, MIENTRAS.Text, CONTINUAR.Text, CLASE.Text, "false", "true", "verdadero", "falso");
            /*-------- NOTERMINAL TRANSIENT --------*/
            MarkTransient(DATATYPE, ARRCONTENT, DIMENSION, INSTRUCCION, IMPORTAR_STA, VISIBILIDAD, OVER_STA, CLASE_STA_BODY);
            MarkTransient(ELSE_STA);
            #endregion
        }
    }
}
