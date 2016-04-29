using System;
using System.IO;

namespace AutomataLexicoS {
    class Lex_auto {
        //Atributos
        private string codigoF, token = "";
        private enum e: byte { INICIO, NUMERO, LETRA, OTRO };//[ESTADOS]
        private enum t: byte { ALFA, NUM, SUB, PYC, OTRO }//[Tipo de CHAR]
        private byte e_Act = (byte)e.INICIO;//estado actual
        private StreamWriter f_tokens;
        //Constructor
        public Lex_auto(string fuente) {
            codigoF = fuente;//codigo fuente
            f_tokens = File.CreateText("tokens.txt");//Crea archivo para guardar tokens
        }
        //Metodos
        //////////////////////////////////////////////////////////////////////////////////////////////////
        /// [ MAIN ]
        static int Main(string[] args) {
            Console.WriteLine("Compilando");
            if (args.Length > 0) {//Al menos 1 argumento
                new Lex_auto(args[0]).compilar();
                Console.WriteLine("Compilado completado!");
                return 0; //Sin error
            }
            return -1;//Error (No argumentos)
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////
        /// COMPILAR
        private void compilar() {
            char c = ' ';
            using (f_tokens)
            using (StreamReader sr = new StreamReader(codigoF)) {//ruta del codigo
                while (sr.Peek() >= 0) {
                    c = (char)sr.Read();//lee char
                    switch (e_Act) {
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[INICIO:0]
                        case (byte)e.INICIO:
                            switch (tipo(c)) {
                                case (byte)t.NUM:
                                    e_Act = (byte)e.NUMERO;
                                    token += c;//completar token
                                    break;
                                case (byte)t.ALFA:
                                case (byte)t.SUB:
                                    e_Act = (byte)e.LETRA;
                                    token += c;//completar token
                                    break;
                                case (byte)t.PYC:
                                    f_tokens.WriteLine(";");//   <--
                                    token = "";
                                    break;
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[NUMERO:1]
                        case (byte)e.NUMERO:
                            switch (tipo(c)) {
                                case (byte)t.NUM:
                                    token += c;//completar token
                                    break;
                                case (byte)t.ALFA:
                                case (byte)t.SUB:
                                    e_Act = (byte)e.LETRA;
                                    f_tokens.WriteLine(token);//   <--
                                    token = char.ToString(c);//limpia el token y agrega nuevo char
                                    break;
                                case (byte)t.PYC:
                                    e_Act = (byte)e.INICIO;
                                    f_tokens.WriteLine(token + "\n;\n");//   <-- 2
                                    token = "";
                                    break;
                                case (byte)t.OTRO:
                                    e_Act = (byte)e.INICIO;//estado otro
                                    f_tokens.WriteLine(token + "\n");//   <--
                                    token = "";//limpia el token
                                    break;
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[Letra:1]
                        case (byte)e.LETRA:
                            switch (tipo(c)) {
                                case (byte)t.ALFA:
                                case (byte)t.SUB:
                                case (byte)t.NUM:
                                    token += c;//completar token
                                    break;
                                case (byte)t.PYC:
                                    e_Act = (byte)e.INICIO;
                                    f_tokens.WriteLine(token + "\n;\n");//   <-- 2
                                    token = "";
                                    break;
                                case (byte)t.OTRO:
                                    e_Act = (byte)e.INICIO;//estado otro
                                    f_tokens.WriteLine(token + "\n");//   <--
                                    token = "";//limpia el token
                                    break;
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[OTRO:*]
                        default:
                            Console.WriteLine("Otro");
                            break;
                    }
                }
                if (token != "")//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~(agrega ultimo token: de ser posible)
                    f_tokens.WriteLine(token + "\n");//   <--
                sr.Close();
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////
        /// Herramientas
        private byte tipo(char c) {
            if (char.IsDigit(c)) return (byte)t.NUM;        // Numero
            else if (Char.IsLetter(c)) return (byte)t.ALFA;  // Letra
            else if (c == ';') return (byte)t.PYC;          // ;
            else if (c == '_') return (byte)t.SUB;          // _
            else return (byte)t.OTRO;                       // Otro
        }
        //GET SET
    }
}
