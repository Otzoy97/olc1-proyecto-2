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
            #region REGEX
            #endregion

            #region Terminales
            /*-------- COMENTARIOS EN LÍNEA --------*/
            var LineComment = new CommentTerminal("LineComment", ">>", "\n", "\r\n");
            /*-------- COMENTARIOS EN BLOQUE --------*/
            var BlockComment = new CommentTerminal("BlockComment", "<-", "->");
            /*-------- IDENTIFICADORES --------*/
            var Identificador = new IdentifierTerminal("tkID");
            var Variable = new IdentifierTerminal("tkVAR");
            /*-------- ENTEROS --------*/
            var Entero = new NumberLiteral("tkINT", NumberOptions.AllowSign | NumberOptions.IntOnly | NumberOptions.NoDotAfterInt);
            /*-------- DECIMALES --------*/
            var Decimal = new NumberLiteral("tkREAL", NumberOptions.AllowSign);
            /*-------- BOOLEANOS --------*/
            var Booleano = new ConstantTerminal("tkBOOL");
            Booleano.Add("true", true);
            Booleano.Add("verdadero", true);
            Booleano.Add("falso", false);
            Booleano.Add("false", false);
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
            var ASIGNACION = ToTerm("=","ASIGNACION");
            /*-------- UNARIOS --------*/
            var INCREMENTO = ToTerm("++", "INCREMENTO");
            var DECREMENTO = ToTerm("--", "DECREMENTO");
            var NOT = ToTerm("!","NOT");
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
            #endregion

            #region No terminales
            var INICIO = new NonTerminal("INICIO");
            var OPER = new NonTerminal("OPER");
            #endregion

            #region Producciones

            #endregion

            #region Preferencias
            /*-------- INICIO --------*/
            this.Root = INICIO;
            /*-------- COMENTARIOS IGNORADOS --------*/
            NonGrammarTerminals.Add(LineComment);
            NonGrammarTerminals.Add(BlockComment);
            /*-------- PUNTUACIÓN Y AGRUPACIÓN --------*/
            MarkPunctuation(SEMICOLON, COLON, DOT, COMMA, PARIZQ, PARDER, LLVIZQ, LLVDER, CORIZQ, CORDER);
            /*-------- PUNTUACIÓN Y AGRUPACIÓN --------*/
            RegisterOperators(0, Associativity.Left, OR);
            RegisterOperators(1, Associativity.Left, AND);
            RegisterOperators(2, Associativity.Left, IGUAL, DIFERENTE, MAYOR, MAYOR_IGUAL, MENOR, MENOR_IGUAL);
            RegisterOperators(3, Associativity.Left, NOT);
            RegisterOperators(4, Associativity.Left, MAS, MENOS);
            RegisterOperators(5, Associativity.Left, POR, DIVISION);
            RegisterOperators(6, Associativity.Left, POTENCIA);
            RegisterOperators(7, Associativity.Left, PARIZQ, PARDER);
            #endregion
        }
    }
}
