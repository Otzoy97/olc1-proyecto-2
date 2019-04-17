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
            #endregion

            #region No terminales
            #endregion

            #region Producciones
            #endregion

            #region Preferencias
            /*-------- COMENTARIOS IGNORADOS --------*/
            NonGrammarTerminals.Add(LineComment);
            NonGrammarTerminals.Add(BlockComment);
            /*-------- PUNTUACIÓN Y AGRUPACIÓN --------*/
            MarkPunctuation(SEMICOLON, COLON, DOT, COMMA, PARIZQ, PARDER, LLVIZQ, LLVDER, CORIZQ, CORDER);
            #endregion
        }
    }
}
