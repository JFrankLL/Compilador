
using System.Drawing;

namespace Sintactico {
    public class Token {
        //  [Atributos]
        public string lexema { get; set; }
        public tk_t tipo { get; set; }
        public int Linea { get; set; }
        public int Columna { get; set; }

        //  [Constructores]
        public Token(string tipo, string lexema = "INESPERADO") {//Regular
            this.tipo = getTipo(lexema, tipo);
            this.lexema = lexema;
        }
        public Token(int l, string tipo, string lexema = "INESPERADO") {//Regular
            this.tipo = getTipo(lexema, tipo);
            this.lexema = lexema;
            this.Linea = l;//gerardo vergas
        }
        //  [Metodos]
        public override string ToString() {
            return lexema;//tipo+"("+lexema+")";
        }
        public static tk_t getTipo(string lex, string tipo) {
            switch (lex.ToLower()) {
                case "programa": return tk_t.PROGRAMA;
                case "if": return tk_t.P_IF;
                case "then": return tk_t.P_THEN;
                case "else": return tk_t.P_ELSE;
                case "repeat": return tk_t.P_REPEAT;
                case "until": return tk_t.P_UNTIL;
                case "while": return tk_t.P_WHILE;
                case "end": return tk_t.P_END;
                case "main": return tk_t.P_MAIN;
                case "float": return tk_t.P_FLOAT;
                case "int": return tk_t.P_INT;
                case "boolean": return tk_t.P_BOOLEAN;
                case "true": return tk_t.P_TRUE;
                case "false": return tk_t.P_FALSE;
                case ":=": return tk_t.O_ASIGNACION;
                case "==": return tk_t.O_IGUALDAD;
                case ">": return tk_t.O_MAYOR;
                case "<": return tk_t.O_MENOR;
                case ">=": return tk_t.O_MAY_I;
                case "<=": return tk_t.O_MEN_I;
                case "!=": return tk_t.O_DIFERENTE;
                case "+": return tk_t.O_MAS;
                case "-": return tk_t.O_MENOS;
                case "*": return tk_t.O_MUL;
                case "%": return tk_t.O_MODULO;
                case "++": return tk_t.O_INC;
                case "--": return tk_t.O_DEC;
                case "/": return tk_t.O_DIV;
                case "(": return tk_t.B_PA;
                case ")": return tk_t.B_PC;
                case "{": return tk_t.B_CA;
                case "}": return tk_t.B_CC;
                case ",": return tk_t.B_COMA;
                case ";": return tk_t.B_PYC;
                case "cin": return tk_t.O_CIN;
                case "cout": return tk_t.O_COUT;
                //case "//": return tk_t.CMT;
                //case "/**/": return tk_t.CMTS;
                default:
                    switch (tipo.ToLower()) {
                        case "num": return tk_t.N_INT;
                        case "nof": return tk_t.N_FLOAT;
                        case "error": return tk_t.ERROR;
                        case "prg": return tk_t.PROGRAMA;
                        case "null": return tk_t.NULL;
                        default: return tk_t.IDE;
                    }
            }
        }
        public Color getTipoColor() {
            switch (tipo) {
                //Operadores
                case tk_t.O_ASIGNACION: case tk_t.O_DIFERENTE:  case tk_t.O_DIV:
                case tk_t.O_IGUALDAD:   case tk_t.O_INC:        case tk_t.O_MAS:
                case tk_t.O_MAYOR:      case tk_t.O_MAY_I:      case tk_t.O_MENOR:
                case tk_t.O_MENOS:      case tk_t.O_MEN_I:      case tk_t.O_MODULO:
                case tk_t.O_MUL:
                    return Color.Violet;
                //Bloque
                case tk_t.B_CA: case tk_t.B_CC: case tk_t.B_COMA:
                case tk_t.B_PA: case tk_t.B_PC: case tk_t.B_PYC:
                    return Color.DodgerBlue;
                //Digitos
                case tk_t.N_FLOAT: case tk_t.N_INT:
                case tk_t.P_FALSE: case tk_t.P_TRUE:
                    return Color.Tomato;
                //Palabras Reservadas
                case tk_t.P_BOOLEAN: case tk_t.P_ELSE:  case tk_t.P_END:
                case tk_t.P_FLOAT:   case tk_t.P_IF:    case tk_t.P_INT:
                case tk_t.P_MAIN:    case tk_t.P_REPEAT:case tk_t.P_THEN:
                case tk_t.P_UNTIL:   case tk_t.P_WHILE: case tk_t.O_CIN:
                case tk_t.O_COUT:
                    return Color.YellowGreen;
                //main
                case tk_t.PROGRAMA:
                    return Color.Orange;
                //ERROR
                case tk_t.ERROR:
                    return Color.Red;
                //Identificadores
                default:
                    return Color.White;
            }
        }
    }
}
