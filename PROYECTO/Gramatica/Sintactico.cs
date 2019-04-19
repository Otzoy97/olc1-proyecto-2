using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

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
            var DEFECTO = ToTerm("default", "DEFECTO");
            var SALIR = ToTerm("salir", "SALIR");
            var HACER = ToTerm("hacer", "HACER");
            var MIENTRAS = ToTerm("mientras", "MIENTRAS");
            var CONTINUAR = ToTerm("continuar", "CONTINUAR");
            /*-------- IDENTIFICADORES --------*/
            var Identificador = new IdentifierTerminal("tkID");
            var Variable = new IdentifierTerminal("tkVAR");
            #endregion

            #region No terminales
            var INICIO = new NonTerminal("INICIO");
            /*Contiene los tipos de datos utilizados*/
            var DATATYPE = new NonTerminal("DATATYPE");
            /*Contiene una lista de variables*/
            var VARLIST = new NonTerminal("VARLIST");
            /*Declara las variables normales*/
            var DECLARACION_SIMPLE = new NonTerminal("DECLARACION_SIMPLE");
            /*Declara arreglos*/
            var DECLARACION_ARRAY = new NonTerminal("DECLARACION_ARRAY");
            /*Asigna variables normales*/
            var ASIGNACION_SIMPLE = new NonTerminal("ASIGNACION_SIMPLE");
            /*Asigna arreglos*/
            var ASIGNACION_ARRAY = new NonTerminal("ASIGNACION_SIMPLE");
            /*Establece la dimensión de los arreglos*/
            var DIMENSION = new NonTerminal("SET_DIMENSION");
            var DIMENSION_1 = new NonTerminal("SET_DIMENSION");
            var DIMENSION_2 = new NonTerminal("SET_DIMENSION");
            var DIMENSION_3 = new NonTerminal("SET_DIMENSION");
            /*Establece el contenido de los arreglos al declararse*/
            var SET_CONTENT = new NonTerminal("SET_CONTENT");
            /*Específica la forma de las operaciones*/
            var OPER = new NonTerminal("OPER");
            #endregion

            #region Producciones
            INICIO.Rule = OPER;

            #region DECLARACION, ASIGNACION Y GET DE VARIABLES Y ARREGLOS

            DATATYPE.Rule = INT | CHAR | STRING | DOUBLE | BOOL;
            VARLIST.Rule = VARLIST + COMMA + Variable | Variable;


            DIMENSION.Rule = 

            DECLARACION_SIMPLE.Rule = DATATYPE + VARLIST + SEMICOLON
                | DATATYPE + VARLIST + ASIGNACION + OPER + SEMICOLON;

            DECLARACION_ARRAY.Rule = DATATYPE + ARRAY + VARLIST + DIMENSION + SEMICOLON
                | DATATYPE + ARRAY + VARLIST + DIMENSION + ASIGNACION + SET_CONTENT + SEMICOLON;

            ASIGNACION_SIMPLE.Rule = Variable + ASIGNACION + OPER + SEMICOLON;
            ASIGNACION_ARRAY.Rule = Variable + DIMENSION + ASIGNACION + OPER + SEMICOLON;
            
            

            #endregion
            



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
                | Variable + DIMENSION;
            #endregion

            #region Preferencias
            /*-------- INICIO --------*/
            this.Root = INICIO;
            /*-------- COMENTARIOS IGNORADOS --------*/
            NonGrammarTerminals.Add(LineComment);
            NonGrammarTerminals.Add(BlockComment);
            /*-------- PUNTUACIÓN Y AGRUPACIÓN --------*/
            MarkPunctuation(SEMICOLON, COLON, DOT, COMMA, PARIZQ, PARDER, LLVIZQ, LLVDER, CORIZQ, CORDER);
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
                HACER.Text, MIENTRAS.Text, CONTINUAR.Text, "false", "true","verdadero", "falso");
            #endregion
        }
    }
}
