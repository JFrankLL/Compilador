using System;
using System.IO;

namespace AL {
    class Lex_auto {
        //Atributos
        private string codigoF, token = "", t_O="";
        private string[] pR = { "if", "then", "else", "repeat", "until", "while", "end", "main", "float", "int", "boolean","true","false"};
        private enum e: byte { INICIAL, O_SUMA, NUMERO, PUNTO_DECIMAL, NUMERO_FLOAT, IDENTIFICADOR, O_DIVISION,
            COMENTARIO, CMTS_CONT, CMTS_AS, COMENTARIOS,  O_RESTA, O_INCREMENTO, O_DECREMENTO, O_ASIGNA1, O_PARTE2,
            O_COMPARA1, O_MAYOR, O_MAYOR_IGUAL, O_MENOR, O_MENOR_IGUAL, O_DIFERENTE1, O_DIFERENTE2, OPERA, OTRO, ERROR };//[ESTADOS]
        private enum t: byte { LETRA, DIGITO, SUBRAYADO, DOS_PTOS, O_IGUAL, O_MAYOR, O_MENOR, O_NEGAR, O_SUMA, O_RESTA,
            O_DIVISION, O_MULTIPLICACION, O_MODULO, PUNTO, ESPACIO, SALTO, TAB, CARRO, OTRO, BLOQUE}//[Tipo de CHAR]
        private e e_Act = (byte)e.INICIAL;//estado actual
        private StreamWriter f_tokens, f_errores;
        private int linea=1, col=0;
        //Constructor
        public Lex_auto(string fuente) {
            codigoF = fuente;//codigo fuente
            f_tokens = File.CreateText("tokens.txt");//Crea archivo para guardar tokens
            f_errores = File.CreateText("errores.txt");//Crea archivo para guardar errores
        }
        //Metodos
        //////////////////////////////////////////////////////////////////////////////////////////////////
        /// [ MAIN ]
        static int Main(string[] args) {
            //new Lex_auto("testLexico.txt").compilar();
            if (args.Length > 0) {//Al menos 1 argumento
                new Lex_auto(args[0]).run();
                return 0; //Sin error
            }
            return -1;//Error (No argumentos)
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////
        /// COMPILAR
        public void run() {
            char c = ' ', a = '=';
            using (f_tokens)
            using (StreamReader sr = new StreamReader(codigoF)) {//ruta del codigo
                while (sr.Peek() >= 0) {
                    c = (char)sr.Read();//lee char
                    if (c == '\n') col = linea++ - linea;
                    col++;
                    switch (e_Act) {
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[INICIO:0]
                        case e.INICIAL:
                            switch (tipo(c)) {
                                case t.O_SUMA:
                                    e_Act = e.O_SUMA;//mandar a +: 1
                                    token += c; break;//completar token
                                case t.DIGITO:
                                    e_Act = e.NUMERO;//mandar a num: 2
                                    token += c; break;//completar token
                                case t.O_RESTA:
                                    e_Act = e.O_RESTA;//mandar a -: 11
                                    token += c; break;//completar token
                                case t.LETRA:case t.SUBRAYADO:
                                    e_Act = e.IDENTIFICADOR;//mandar a identificaderes: 5
                                    token += c;break;//completar token
                                case t.O_DIVISION:
                                    e_Act = e.O_DIVISION;//mandar a dividir: 6
                                    token += c; break;//completar token
                                case t.O_MULTIPLICACION: case t.O_MODULO:
                                    //e_Act = e.OPERA;//manda a (% *)
                                    f_tokens.WriteLine(c + "\t"+ tipo_s(c)+"\t"+linea);
                                    token = ""; break;//nuevo token
                                case t.DOS_PTOS:
                                    e_Act = e.O_ASIGNA1;//mandar a asignacion (:): 18
                                    token += c; break;//completa token
                                case t.O_IGUAL:
                                    a = c;
                                    e_Act = e.O_COMPARA1;//mandar a comparacion.p1 (=): 20
                                    token += c; break;//completa token
                                case t.O_MAYOR:
                                    e_Act = e.O_MAYOR;//mandar a mayor: 22
                                    token += c; break;//completa token
                                case t.O_MENOR:
                                    e_Act = e.O_MENOR;//mandar a mayor: 24
                                    token += c ;break;//completa token
                                case t.O_NEGAR:
                                    e_Act = e.O_DIFERENTE1;//mandar a diferente: 26
                                    token += c; break;//completa token
                                case t.BLOQUE:
                                    f_tokens.WriteLine(c+"\t"+tipo_s(c) + "\t" + linea); token = ""; break;//   <--
                                case t.ESPACIO: case t.SALTO: case t.TAB: case t.CARRO: break;
                                default:
                                    e_Act = e.ERROR;
                                    error(0, c); break;//   <--
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[Operador +:1]
                        case e.O_SUMA:
                            switch (tipo(c)) {
                                case t.DIGITO:
                                    f_tokens.WriteLine(token + "\tmas" + "\tSigno mas" + "\t" + linea);//   <--
                                    token = "" + c;//completar token
                                    e_Act = e.NUMERO;//Mandar a num: 2
                                    token = "" + c; break;//comletar token
                                case t.O_SUMA:
                                    e_Act = e.O_INCREMENTO;//Mandar a ++: 12
                                    token += c; break;//completar token
                                case t.O_RESTA:
                                    e_Act = e.O_RESTA;//mandar a -: 11
                                    f_tokens.WriteLine(token + "\tmas" + "\tSigno mas" + "\t" + linea);//   <--
                                    token = "" + c; break;//completar token
                                case t.LETRA: case t.SUBRAYADO:
                                    e_Act = e.IDENTIFICADOR;//mandar a identificadores: 5
                                    f_tokens.WriteLine(token + "\tmas" + "\tSigno mas" + "\t" + linea);//   <--
                                    token = ""+c; break;//empieza nuevo token
                                case t.ESPACIO: case t.SALTO: case t.TAB: case t.CARRO:
                                    e_Act = e.INICIAL;//manda a estado inicial
                                    f_tokens.WriteLine(token + "\tmas" + "\tSigno mas" + "\t" + linea);//   <--
                                    token = ""; break;//limpia token
                                case t.BLOQUE:
                                    e_Act = e.INICIAL;//mandar a inicial: 0
                                    f_tokens.WriteLine(token + "\tmas" + "\tSigno mas" + "\t" + linea);//   <--
                                    f_tokens.WriteLine(c + "\tmas" + "\t" +tipo_s(c) + "\t" + linea);//   <--
                                    token = ""; break;//reset token
                                default:
                                    e_Act = e.ERROR;
                                    error(1, c); break;//   <--
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[NUMERO:2]
                        case e.NUMERO:
                            switch (tipo(c)) {
                                case t.DIGITO:
                                    token += c;break;//completar token
                                case t.ESPACIO: case t.SALTO: case t.TAB: case t.CARRO:
                                    e_Act = (byte)e.INICIAL;//manda a estado inicial
                                    f_tokens.WriteLine(token + "\tnum" + "\tNumero entero" + "\t" + linea);//   <--
                                    token = ""; break;//limpia token
                                case t.PUNTO:
                                    e_Act = e.PUNTO_DECIMAL;//Mandar a .
                                    token += c;break;
                                case t.O_SUMA:
                                    e_Act = e.O_SUMA;
                                    f_tokens.WriteLine(token + "\tnum" + "\tNumero entero" + "\t" + linea);//   <--
                                    token = "" + c; break;//limpia token
                                case t.O_RESTA:
                                    e_Act = e.O_RESTA;
                                    f_tokens.WriteLine(token + "\tnum" + "\tNumero entero" + "\t" + linea);//   <--
                                    token = "" + c; break;//
                                case t.O_MAYOR:
                                    e_Act = e.O_MAYOR;
                                    f_tokens.WriteLine(token + "\tnum" + "\tNumero entero" + "\t" + linea);//   <--
                                    token = "" + c; break;//limpia token
                                case t.O_MENOR:
                                    e_Act = e.O_MENOR;
                                    f_tokens.WriteLine(token + "\tnum" + "\tNumero entero" + "\t" + linea);//   <--
                                    token = "" + c; break;//limpia token
                                case t.BLOQUE:
                                    e_Act = e.INICIAL;//mandar a inicial: 0
                                    f_tokens.WriteLine(token + "\tnum" + "\tNumero entero" + "\t" + linea);//   <--
                                    f_tokens.WriteLine(c + "\tblq" + "\t"+tipo_s(c));//   <--
                                    token = ""; break;//reset token
                                case t.O_DIVISION:
                                    e_Act = e.O_DIVISION;//manda a div: 6
                                    f_tokens.WriteLine(token + "\tnum" + "\tNumero entero" + "\t" + linea);//   <--
                                    token = "" + c; break;//reset token
                                case t.O_MULTIPLICACION:
                                    e_Act = e.INICIAL;//manda a div: 6
                                    f_tokens.WriteLine(token + "\tnum" + "\tNumero entero" + "\t" + linea);//   <--
                                    f_tokens.WriteLine(c + "\tmul" + "\tSigno Multiplicacion" + "\t" + linea);//   <--
                                    token = ""; break;//reset token
                                case t.LETRA:
                                    e_Act = e.IDENTIFICADOR;
                                    f_tokens.WriteLine(token + "\tnum" + "\tNumero entero" + "\t" + linea);//   <--
                                    token = "" + c; break;
                                default:
                                    e_Act = e.ERROR;//Mandar a estado error
                                    error(2, c); break;//   <--
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[Punto decimal:3]
                        case e.PUNTO_DECIMAL:
                            switch (tipo(c)) {
                                case t.DIGITO:
                                    e_Act = e.NUMERO_FLOAT;//mandar a num. float
                                    token += c;break;//completa token
                                case t.ESPACIO: case t.SALTO: case t.TAB: case t.CARRO:
                                    error(3, c);
                                    e_Act = (byte)e.INICIAL;//manda a estado inicial
                                    token = ""; break;//limpia token
                                default:
                                    e_Act = e.ERROR;
                                    error(3, c); break;//   <--
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[Numero Flotante: 4]
                        case e.NUMERO_FLOAT:
                            switch (tipo(c)) {
                                case t.DIGITO:
                                    token += c; break;//completa token
                                case t.ESPACIO: case t.SALTO: case t.TAB: case t.CARRO:
                                    e_Act = e.INICIAL;//manda a estado inicial
                                    f_tokens.WriteLine(token + "\tnof" + "\tNumero flotante" + "\t" + linea);//   <--
                                    token = ""; break;//limpia token
                                case t.O_SUMA:
                                    e_Act = e.O_SUMA;
                                    f_tokens.WriteLine(token + "\tnof" + "\tNumero flotante" + "\t" + linea);//   <--
                                    token = "" + c; break;//limpia token
                                case t.O_RESTA:
                                    e_Act = e.O_RESTA;
                                    f_tokens.WriteLine(token + "\tnof" + "\tNumero flotante" + "\t" + linea);//   <--
                                    token = "" + c; break;//limpia token
                                case t.BLOQUE:
                                    e_Act = e.INICIAL;//mandar a inicial: 0
                                    f_tokens.WriteLine(token + "\tnof" + "\tNumero flotante" + "\t" + linea);//   <--
                                    f_tokens.WriteLine(c + "\tblq" + "\t" +tipo_s(c));//   <--
                                    token = ""; break;//reset token
                                case t.O_DIVISION:
                                    e_Act = e.O_DIVISION;//manda a div: 6
                                    f_tokens.WriteLine(token + "\tnof" + "\tNumero flotante" + "\t" + linea);//   <--
                                    token = ""+c; break;//reset token
                                case t.O_MULTIPLICACION:
                                    e_Act = e.INICIAL;//manda a div: 6
                                    f_tokens.WriteLine(token + "\tnof" + "\t Numero flotante" + "\t" + linea);//   <--
                                    f_tokens.WriteLine(c + "\tmul" + "\t Signo Multiplicacion" + "\t" + linea);//   <--
                                    token = ""; break;//reset token
                                case t.PUNTO:
                                    f_tokens.WriteLine(token + "\tnof" + "\t Numero flotante" + "\t" + linea);//   <--
                                    token = "";
                                    e_Act = e.ERROR;
                                    error(4, c); break;//   <--
                                default:
                                    e_Act = e.ERROR;
                                    error(4, c); break;//   <--
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[Identificadores:5]
                        case e.IDENTIFICADOR:
                            switch (tipo(c)) {
                                case t.LETRA:case t.SUBRAYADO:case t.DIGITO:
                                    token += c; break;//completar token
                                case t.O_SUMA:
                                    e_Act = e.O_SUMA;//mandar a +
                                    f_tokens.WriteLine(token+"\t"+palabraR(token) + "\t" + linea);//   <--
                                    token = "" + c; break;//completar nuevo token
                                case t.O_RESTA:
                                    e_Act = e.O_RESTA;//mandar a +
                                    f_tokens.WriteLine(token + "\t" + palabraR(token) + "\t" + linea);//   <--
                                    token = "" + c; break;//completar nuevo token
                                case t.O_DIVISION:
                                    e_Act = e.O_DIVISION;//mandar a +
                                    f_tokens.WriteLine(token + "\t" + palabraR(token) + "\t" + linea);//   <--
                                    token = ""+c; break;//completar nuevo token
                                case t.O_MULTIPLICACION: case t.O_MODULO:
                                    e_Act = e.OPERA;//mandar a operacion (* %)
                                    f_tokens.WriteLine(token + "\t" + palabraR(token) + "\t" + linea);//   <--
                                    token = "" + c; break;//completar nuevo token
                                case t.BLOQUE:
                                    e_Act = e.INICIAL;//mandar a inicial: 0
                                    f_tokens.WriteLine(token + "\t" + palabraR(token) + "\t" + linea);//   <--
                                    f_tokens.WriteLine(c + "\t" + tipo_s(c) + "\t" + linea);//   <--
                                    token = ""; break;//reset token
                                case t.O_IGUAL:
                                    a = c;
                                    e_Act = e.O_COMPARA1;//mandar a compara 1
                                    f_tokens.WriteLine(token + "\t" + palabraR(token) + "\t" + linea);//   <--
                                    token = ""+c; break;//hacer token
                                case t.ESPACIO: case t.SALTO: case t.TAB: case t.CARRO:
                                    e_Act = e.INICIAL;//manda a estado inicial
                                    f_tokens.WriteLine(token + "\t" + palabraR(token) + "\t" + linea);//   <--
                                    token = ""; break;//limpia token
                                case t.DOS_PTOS:
                                    e_Act = e.O_ASIGNA1;
                                    f_tokens.WriteLine(token + "\t" + palabraR(token) + "\t" + linea);//   <--
                                    token = ""+c; break;
                                case t.O_MENOR:
                                    e_Act = e.O_MENOR;
                                    f_tokens.WriteLine(token + "\t" + palabraR(token) + "\t" + linea);//   <--
                                    token = "" + c; break;
                                case t.O_MAYOR:
                                    e_Act = e.O_MAYOR;
                                    f_tokens.WriteLine(token + "\t" + palabraR(token) + "\t" + linea);//   <--
                                    token = "" + c; break;
                                default:
                                    f_tokens.WriteLine(token + "\t" + palabraR(token) + "\t" + linea);//   <--
                                    token = "";
                                    e_Act = e.ERROR;
                                    error(5, c); break;//   <--
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[Operador /: 6]
                        case e.O_DIVISION:
                            switch (tipo(c)) {
                                case t.DIGITO:
                                    e_Act = e.NUMERO;
                                    f_tokens.WriteLine(token + "\tdiv" + "\tDivision" + "\t" + linea);//   <--
                                    token = "" + c; break;
                                case t.LETRA:
                                    e_Act = e.IDENTIFICADOR;
                                    f_tokens.WriteLine(token + "\tdiv" + "\tDivision" + "\t" + linea);//   <--
                                    token = "" + c; break;
                                case t.O_DIVISION:
                                    e_Act = e.COMENTARIO;//Mandar a comentario simple: 7
                                    token = ""; break;//reset token
                                case t.O_MULTIPLICACION:
                                    e_Act = e.CMTS_CONT;//Mandar a comentarios multiples cont.: 8
                                    token = ""; break;//reset token
                                case t.ESPACIO: case t.TAB: break;
                                case t.SALTO: case t.CARRO:
                                    e_Act = e.INICIAL;//manda a estado inicial
                                    f_tokens.WriteLine(token + "\tdiv" + "\tDivision" + "\t" + linea);//   <--
                                    token = ""; //limpia token
                                    break;
                                case t.BLOQUE:
                                    e_Act = e.INICIAL;
                                    f_tokens.WriteLine(token + "\tdiv" + " \tDivision" + "\t" + linea);//   <--
                                    f_tokens.WriteLine(c + "\tblq" + "\t" +tipo_s(c) + "\t" + linea);//   <--
                                    token = ""; break;
                                case t.O_SUMA:
                                    e_Act = e.O_SUMA;
                                    f_tokens.WriteLine(token + "\tdiv" + "\tDivision" + "\t" + linea);//   <--
                                    token = "" + c;break;
                                case t.O_RESTA:
                                    e_Act = e.O_RESTA;
                                    f_tokens.WriteLine(token + "\tdiv" + "\tDivision" + "\t" + linea);//   <--
                                    token = "" + c; break;
                                default:
                                    e_Act = e.ERROR;
                                    error(6, c); break;
                            }
                            break;

                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[Comentario simple:7]
                        case e.COMENTARIO:
                            switch (tipo(c)) {
                                case t.SALTO:
                                    e_Act = (byte)e.INICIAL;//manda a inicial
                                    token = ""; break;//reinicia token
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[CMTS mult cont.:8]
                        case e.CMTS_CONT:
                            switch (tipo(c)) {
                                case t.O_MULTIPLICACION:
                                    e_Act = e.CMTS_AS; break;//manda a comentarios multiples casi acabado
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[Comenterios casi:9]
                        case e.CMTS_AS:
                            switch (tipo(c)) {
                                case t.O_MULTIPLICACION: case t.ESPACIO: case t.SALTO: case t.CARRO: case t.TAB: break;
                                case t.O_DIVISION:
                                    e_Act = e.COMENTARIOS; break;//manda a comentarios multiples terminado
                                default:
                                    e_Act = e.CMTS_CONT; break;//comentario sigue
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[Comentarios multiples:10]
                        case e.COMENTARIOS:
                            switch (tipo(c)) {
                                case t.ESPACIO: case t.SALTO: case t.TAB: case t.CARRO:
                                    e_Act = (byte)e.INICIAL;//manda a inicial
                                    token = ""; break;//reinicia token
                                case t.O_SUMA:
                                    e_Act = e.O_SUMA;//mandar a +: 1
                                    token += c; break;//completar token
                                case t.DIGITO:
                                    e_Act = e.NUMERO;//mandar a num: 2
                                    token += c; break;//completar token
                                case t.O_RESTA:
                                    e_Act = e.O_RESTA;//mandar a -: 11
                                    token += c; break;//completar token
                                case t.LETRA: case t.SUBRAYADO:
                                    e_Act = e.IDENTIFICADOR;//mandar a identificaderes: 5
                                    token += c; break;//completar token
                                case t.O_DIVISION:
                                    e_Act = e.O_DIVISION;//mandar a dividir: 6
                                    token += c; break;//completar token
                                case t.O_MULTIPLICACION: case t.O_MODULO:
                                    e_Act = e.OPERA;//manda a (% *)
                                    token = "" + c; break;//nuevo token
                                case t.DOS_PTOS:
                                    e_Act = e.O_ASIGNA1;//mandar a asignacion (:): 18
                                    token += c; break;//completa token
                                case t.O_IGUAL:
                                    a = c;
                                    e_Act = e.O_COMPARA1;//mandar a comparacion.p1 (=): 20
                                    token += c; break;//completa token
                                case t.O_MAYOR:
                                    e_Act = e.O_MAYOR;//mandar a mayor: 22
                                    token += c; break;//completa token
                                case t.O_MENOR:
                                    e_Act = e.O_MENOR;//mandar a mayor: 24
                                    token += c; break;//completa token
                                case t.O_NEGAR:
                                    e_Act = e.O_DIFERENTE1;//mandar a diferente: 26
                                    token += c; break;//completa token
                                case t.BLOQUE:
                                    f_tokens.WriteLine(c+"\tblq"+"\t"+tipo_s(c) + "\t" + linea); token = ""; break;//   <--
                                default:
                                    e_Act = e.ERROR;
                                    error(10, c); break;
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[Operador -:11]
                        case e.O_RESTA:
                            switch (tipo(c)) {
                                case t.DIGITO:
                                    f_tokens.WriteLine(token+"\tmen"+"\tSigno menos" + "\t" + linea);//   <--
                                    token = "" + c; //completar token
                                    e_Act = e.NUMERO;//Mandar a num: 2
                                    token = "" + c; break;//comletar token
                                case t.O_RESTA:
                                    e_Act = e.O_DECREMENTO;//Mandar a --: 13
                                    token += c; break;//completar token
                                case t.O_SUMA:
                                    e_Act = e.O_SUMA;//mandar a +: 1
                                    f_tokens.WriteLine(token+ "\tmen" + "\tSigno menos" + "\t" + linea);//   <--
                                    token = "" + c; break;//completar token
                                case t.LETRA: case t.SUBRAYADO:
                                    e_Act = e.IDENTIFICADOR;//mandar a identificadores: 5
                                    f_tokens.WriteLine(token + "\tmen" + "\tSigno menos" + "\t" + linea);//   <--
                                    token = ""+c; break;//empieza nuevo token
                                case t.ESPACIO: case t.SALTO: case t.TAB: case t.CARRO:
                                    e_Act = e.INICIAL;//manda a estado inicial
                                    f_tokens.WriteLine(token + "\tmen" + "\tSigno menos" + "\t" + linea);//   <--
                                    token = ""; break;//limpia token
                                case t.BLOQUE:
                                    e_Act = e.INICIAL;//mandar a inicial: 0
                                    f_tokens.WriteLine(token + "\tmen" + "\tSigno menos" + "\t" + linea);//   <--
                                    f_tokens.WriteLine(c+ "\tblq" + " \t" +tipo(c) + "\t" + linea);//   <--
                                    token = ""; break;//reset token
                                default:
                                    error(11, c);
                                    e_Act = (byte)e.INICIAL; break;
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[Operador ++:12]
                        case e.O_INCREMENTO:
                            switch (tipo(c)) {
                                case t.DIGITO:
                                    e_Act = e.NUMERO;//manda numero: 2
                                    f_tokens.WriteLine(token + "\tmas" + "\tIncremento" + "\t" + linea);//   <--
                                    token = "" + c; break;//nuevo token
                                case t.LETRA:
                                    e_Act = e.IDENTIFICADOR;//mandar a identificadores: 5
                                    f_tokens.WriteLine(token + "\tmas" + "\tIncremento" + "\t" + linea);//   <--
                                    token = "" + c; break;//reset token
                                case t.O_SUMA:
                                    e_Act = e.O_SUMA;
                                    f_tokens.WriteLine(token + "\tmas" + "\tIncremento" + "\t" + linea);
                                    token = "" + c; break;
                                case t.ESPACIO: case t.SALTO: case t.TAB: case t.CARRO:
                                    e_Act = e.INICIAL;//mandar a inicial: 0
                                    f_tokens.WriteLine(token + "\tmas" + "\tIncremento" + "\t" + linea);//   <--
                                    token = ""; break;//reset token
                                case t.BLOQUE:
                                    e_Act = e.INICIAL;//mandar a inicial: 0
                                    f_tokens.WriteLine(token + "\tmas" + "\tIncremento" + "\t" + linea);//   <--
                                    f_tokens.WriteLine(c +"\tblq" + "\t" + tipo_s(c) + "\t" + linea);//   <--
                                    token = ""; break;//reset token
                                case t.O_DIVISION: case t.O_IGUAL: case t.O_MAYOR: case t.O_MENOR:
                                case t.O_NEGAR: case t.O_RESTA:
                                    e_Act = e.ERROR;
                                    error(12, c); break;
                                default:
                                    error(12, c);
                                    e_Act = e.INICIAL;
                                    token = ""; break;
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[Operador --: 13]
                        case e.O_DECREMENTO:
                            switch (tipo(c)) {
                                case t.DIGITO:
                                    e_Act = e.NUMERO;//manda numero: 2
                                    f_tokens.WriteLine(token +"\tdec"+"\tDecremento" + "\t" + linea);//   <--
                                    token = "" + c; break;//nuevo token
                                case t.LETRA:
                                    e_Act = e.IDENTIFICADOR;//mandar a identificadores: 5
                                    f_tokens.WriteLine(token + "\tdec" + "\tDecremento" + "\t" + linea);//   <--
                                    token = "" + c; break;//reset token
                                case t.O_RESTA:
                                    e_Act = e.O_RESTA;
                                    f_tokens.WriteLine(token + "\tdec" + "\tDecremento" + "\t" + linea);
                                    token = "" + c; break;
                                case t.ESPACIO: case t.SALTO: case t.TAB: case t.CARRO:
                                    e_Act = e.INICIAL;//mandar a inicial: 0
                                    f_tokens.WriteLine(token + "\tdec" + "\tDecremento" + "\t" + linea);//   <--
                                    token = ""; break;//reset token
                                case t.BLOQUE:
                                    e_Act = e.INICIAL;//mandar a inicial: 0
                                    f_tokens.WriteLine(token +"\tdec"+ "\tDecremento" + "\t" + linea);//   <--
                                    f_tokens.WriteLine(c + "\tblq" + "\t" + tipo_s(c) + "\t" + linea);//   <--
                                    token = ""; break;//reset token
                                case t.O_DIVISION:case t.O_IGUAL:case t.O_MAYOR:case t.O_MENOR:case t.O_NEGAR:
                                    e_Act = e.ERROR;
                                    error(11, c); break;
                                default:
                                    error(11, c);
                                    e_Act = e.INICIAL;
                                    token = ""; break;
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[Asignacion 1:18]
                        case e.O_ASIGNA1:
                            switch (tipo(c)) {
                                case t.O_IGUAL://:=
                                    e_Act = e.O_PARTE2;
                                    t_O = "asi\tAsignacion";
                                    token += c;break;
                                case t.O_NEGAR:
                                    e_Act = e.O_DIFERENTE1;
                                    f_tokens.WriteLine(token + "\tasi" + "\t" + linea);
                                    token = "" + c; break;
                                default:
                                    error(18, c);
                                    e_Act = e.ERROR; break;
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[Parte 2:19]
                        case e.O_PARTE2:
                            switch (tipo(c)) {
                                case t.LETRA:
                                    e_Act = e.IDENTIFICADOR;
                                    f_tokens.WriteLine(token + "\t" + t_O + "\t" + linea);
                                    token = "" + c;break;
                                case t.DIGITO:
                                    e_Act = e.NUMERO;
                                    f_tokens.WriteLine(token + "\t" + t_O + "\t" + linea);
                                    token = "" + c; break;
                                case t.O_MAYOR:
                                    f_tokens.WriteLine(token + "\t" + t_O + "\t" + linea);
                                    e_Act = e.O_MAYOR;
                                    token = "" + c; break;
                                case t.O_MENOR:
                                    f_tokens.WriteLine(token + "\t" + t_O + "\t" + linea);
                                    e_Act = e.O_MENOR;
                                    token = "" + c; break;
                                case t.O_IGUAL:
                                    e_Act = e.O_COMPARA1;
                                    f_tokens.WriteLine(token + "\t" + t_O + "\t" + linea);
                                    token = "" + c; break;
                                case t.O_DIVISION:
                                    e_Act = e.O_DIVISION;
                                    f_tokens.WriteLine(token + "\t" + t_O + "\t" + linea);
                                    token = "" + c; break;
                                case t.BLOQUE:
                                    e_Act = e.INICIAL;
                                    f_tokens.WriteLine(token + "\t" + t_O + "\t" + linea);
                                    f_tokens.WriteLine(c + "\t" + tipo_s(c) + "\t" + linea);
                                    token = ""; break;
                                case t.O_SUMA:
                                    e_Act = e.O_SUMA;
                                    f_tokens.WriteLine(token + "\t" + t_O + "\t" + linea);
                                    token = "" + c; break;
                                case t.O_RESTA:
                                    e_Act = e.O_RESTA;
                                    f_tokens.WriteLine(token + "\t" + t_O + "\t" + linea);
                                    token = "" + c; break;
                                case t.O_NEGAR:
                                    e_Act = e.O_DIFERENTE1;
                                    f_tokens.WriteLine(token + "\t" + t_O + "\t" + linea);
                                    token = ""+c; break;
                                case t.ESPACIO: case t.SALTO: case t.TAB: case t.CARRO:
                                    e_Act = e.INICIAL;
                                    f_tokens.WriteLine(token + "\t" + t_O + "\t" + linea);
                                    token = ""; break;
                                case t.PUNTO:
                                    e_Act = e.ERROR;
                                    error(19, c);
                                    f_tokens.WriteLine(token + "\t" + linea);
                                    token = "";break;
                                default:
                                    error(191, c);
                                    e_Act = e.ERROR; break;
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[Comparacion.1 :20]
                        case e.O_COMPARA1:
                            switch (tipo(c)) {
                                case t.O_IGUAL://==
                                    e_Act = e.O_PARTE2;//mandar a comparacion.2 : 21
                                    t_O = "com\tIgualdad";
                                    token += c; break;//completar token
                                case t.O_NEGAR:
                                    error(20, a);
                                    e_Act = e.O_DIFERENTE1;
                                    t_O = "dif\tIgualdad";
                                    token = "" + c; break;
                                case t.LETRA:
                                    error(20, a);
                                    e_Act = e.IDENTIFICADOR;
                                    token = "" + c; break;
                                default:
                                    error(20, a);
                                    e_Act = e.ERROR; break;
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[Mayor:22]
                        case e.O_MAYOR:
                            switch (tipo(c)) {
                                case t.O_IGUAL://>=
                                    e_Act = e.O_PARTE2;//mandar a parte 2: 19
                                    t_O = "mayi\tMayor igual que";
                                    token += c; break;//completar token
                                case t.O_MAYOR:
                                    f_tokens.WriteLine(token + "\tmay" + "\tMayor que" + "\t" + linea);
                                    token = "" + c; break;
                                case t.LETRA:
                                    e_Act = e.IDENTIFICADOR;
                                    f_tokens.WriteLine(token + "\tmay" + "\tMayor que" + "\t" + linea);
                                    token = "" + c; break;
                                case t.DIGITO:
                                    e_Act = e.NUMERO;
                                    f_tokens.WriteLine(token + "\tmay" + "\tMayor que" + "\t" + linea);
                                    token = "" + c; break;
                                case t.O_DIVISION:
                                    e_Act = e.O_DIVISION;
                                    f_tokens.WriteLine(token + "\tmay" + "\tMayor que" + "\t" + linea);
                                    token = "" + c; break;
                                case t.BLOQUE:
                                    e_Act = e.INICIAL;
                                    f_tokens.WriteLine(token + "\tmay" + "\tMayor que" + "\t" + linea);
                                    f_tokens.WriteLine(c + "\tblq" + "\t" + tipo_s(c) + "\t" + linea);//   <--
                                    token = "" ; break;
                                case t.O_SUMA:
                                    e_Act = e.O_SUMA;
                                    f_tokens.WriteLine(token + "\tmay" + "\tMayor que" + "\t" + linea);
                                    token = "" + c; break;
                                case t.O_RESTA:
                                    e_Act = e.O_RESTA;
                                    f_tokens.WriteLine(token + "\tmay" + "\tMayor que" + "\t" + linea);
                                    token = "" + c; break;
                                case t.ESPACIO: case t.SALTO: case t.TAB: case t.CARRO:
                                    f_tokens.WriteLine(token + "\tmay" + "\tMayor que" + "\t" + linea);
                                    token = "";
                                    e_Act = e.INICIAL; break;
                                default:
                                    error(22, c);
                                    e_Act = e.ERROR; break;
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[Menor:24]
                        case e.O_MENOR:
                            switch (tipo(c)) {
                                case t.O_IGUAL://<=
                                    e_Act = e.O_PARTE2;//mandar a comparacion.2 : 21
                                    t_O = "meni\tMenor igual que";
                                    token += c; break;//completar token
                                case t.O_MENOR:
                                    f_tokens.WriteLine(token + "\tmen" + "\tMenor que" + "\t" + linea);
                                    token = "" + c; break;
                                case t.LETRA:
                                    e_Act = e.IDENTIFICADOR;
                                    f_tokens.WriteLine(token + "\tmen" + "\tMenor que" + "\t" + linea);
                                    token = "" + c; break;
                                case t.DIGITO:
                                    e_Act = e.NUMERO;
                                    f_tokens.WriteLine(token + "\tmen" + "\tMenor que" + "\t" + linea);
                                    token = "" + c; break;
                                case t.O_DIVISION:
                                    e_Act = e.O_DIVISION;
                                    f_tokens.WriteLine(token + "\tmen" + "\tMenor que" + "\t" + linea);
                                    token = "" + c; break;
                                case t.BLOQUE:
                                    e_Act = e.INICIAL;
                                    f_tokens.WriteLine(token + "\tmen" + "\tMenor que" + "\t" + linea);
                                    f_tokens.WriteLine(c + "\tblq" + "\t" + tipo_s(c) + "\t" + linea);//   <--
                                    token = ""; break;
                                case t.O_SUMA:
                                    e_Act = e.O_SUMA;
                                    f_tokens.WriteLine(token + "\tmen" + "\tMenor que" + "\t" + linea);
                                    token = "" + c; break;
                                case t.O_RESTA:
                                    e_Act = e.O_RESTA;
                                    f_tokens.WriteLine(token + "\tmen" + "\tMenor que" + "\t" + linea);
                                    token = "" + c; break;
                                case t.ESPACIO:case t.SALTO:case t.TAB:case t.CARRO:
                                    f_tokens.WriteLine(token + "\tmen" + "\tMenor que" + "\t" + linea);
                                    token = "";
                                    e_Act = e.INICIAL; break;
                                default:
                                    error(24, c);
                                    e_Act = e.ERROR; break;
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[OPERADOR]
                        case e.OPERA:// * %
                            switch (tipo(c)) {
                                case t.LETRA: case t.SUBRAYADO:
                                    e_Act = e.IDENTIFICADOR;//mandar id
                                    f_tokens.WriteLine(token+"\t"+tipo_s(c) + "\t" + linea);//   <--
                                    token = "" + c; break;//completar token
                                case t.O_SUMA: case t.O_RESTA:
                                    e_Act = e.O_RESTA;//mandar a -: 11
                                    f_tokens.WriteLine(token + "\t" + tipo_s(c) + "\t" + linea);//   <--
                                    token = "" + c; break;//completar token
                                case t.O_MULTIPLICACION: case t.O_MODULO:
                                    e_Act = e.ERROR;
                                    error(14, c); break;
                                case t.O_DIVISION:
                                    e_Act = e.O_DIVISION;//mandar a dividir: 6
                                    f_tokens.WriteLine(token + "\t" + tipo_s(c) + "\t" + linea);//   <--
                                    token = ""+c; break;//haver token
                                case t.ESPACIO: case t.SALTO:  case t.TAB: case t.CARRO:
                                    e_Act = e.INICIAL;
                                    token = "" + c; break;
                                default:
                                    error(14, c);
                                    e_Act = e.ERROR;
                                    token = ""; break;
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[Diferente 1: 26]
                        case e.O_DIFERENTE1:
                            switch (tipo(c)) {
                                case t.O_IGUAL:
                                    e_Act = e.O_PARTE2;//mandar aparte 2 (=)
                                    token += c; break;//completar token
                                case t.BLOQUE:
                                    e_Act = e.ERROR;
                                    error(26, c);
                                    f_tokens.WriteLine(c+"\t"+tipo_s(c) + "\t" + linea);//   <--
                                    token = ""; break;
                                default:
                                    error(26, c);
                                e_Act = e.ERROR;
                                    token = ""; break;
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[ERROR]
                        case e.ERROR:
                            switch (tipo(c)) {
                                case t.ESPACIO: case t.SALTO: case t.TAB: case t.CARRO:
                                    e_Act = e.INICIAL;//Sale de estado error hacia inicial: 0
                                    token = ""; break;//reset token
                                case t.BLOQUE:
                                    e_Act = e.INICIAL;
                                    f_tokens.WriteLine(c+"\t"+tipo_s(c) + "\t" + linea);//    <--
                                    token = ""; break;
                                case t.LETRA:
                                    e_Act = e.IDENTIFICADOR;
                                    token = ""+c; break;
                                case t.DIGITO:
                                    e_Act = e.NUMERO;
                                    token = "" + c; break;
                                case t.O_DIVISION:
                                    e_Act = e.O_DIVISION;
                                    token += c; break;
                            }
                            break;
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[OTRO]
                        default:
                            Console.WriteLine("Otro");
                            break;
                    }
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~(agrega ultimo token: de ser posible)
                if (token != "" && e_Act != e.ERROR && e_Act != e.O_DIFERENTE1 && e_Act != e.O_ASIGNA1 && e_Act != e.O_COMPARA1 && e_Act != e.PUNTO_DECIMAL)
                    switch (e_Act) {
                        case e.IDENTIFICADOR:
                            f_tokens.WriteLine(token + "\t"+palabraR(token)+"\n" + "\t" + linea); break;//   <-- 
                        case e.INICIAL:
                            f_tokens.WriteLine(token + "\t\n");break;//   <--
                        case e.NUMERO:
                            f_tokens.WriteLine(token + "\tNumero entero\n" + "\t" + linea);break;//   <--
                        case e.NUMERO_FLOAT:
                            f_tokens.WriteLine(token + "\tNumero flotante\n" + "\t" + linea);break;//   <--
                        case e.OPERA:
                            f_tokens.WriteLine(token + "\t"+tipo_s(c)+"\n" + "\t" + linea); break;//   <--
                        case e.O_DECREMENTO:
                            f_tokens.WriteLine(token + "\tDecremento\n" + "\t" + linea);break;//   <--
                        case e.O_DIFERENTE2:
                            f_tokens.WriteLine(token + "\tIgualdad\n" + "\t" + linea);break;//   <--
                        case e.O_DIVISION:
                            f_tokens.WriteLine(token + "\tDivision\n" + "\t" + linea);break;//   <--
                        case e.O_INCREMENTO:
                            f_tokens.WriteLine(token + "\tIncremento\n" + "\t" + linea);break;//   <--
                        case e.O_MAYOR:
                            f_tokens.WriteLine(token + "\tMayor que\n" + "\t" + linea);break;//   <--
                        case e.O_MAYOR_IGUAL:
                            f_tokens.WriteLine(token + "\tMayor igual que\n" + "\t" + linea);break;//   <--
                        case e.O_MENOR:
                            f_tokens.WriteLine(token + "\tMenor que\n" + "\t" + linea);break;//   <--
                        case e.O_MENOR_IGUAL:
                            f_tokens.WriteLine(token + "\tMenor igual que\n" + "\t" + linea);break;//   <--
                        case e.O_PARTE2:
                            f_tokens.WriteLine(token + "\t"+t_O+"\n" + "\t" + linea);break;//   <--
                        case e.O_RESTA:
                            f_tokens.WriteLine(token + "\tSigno menos\n" + "\t" + linea);break;//   <--
                        case e.O_SUMA:
                            f_tokens.WriteLine(token + "\tSigno mas\n" + "\t" + linea);break;//   <--
                    }
                else if(e_Act != e.INICIAL && e_Act != e.COMENTARIO && e_Act != e.COMENTARIOS)
                    error(100, c);
                sr.Close();
                f_tokens.Dispose();
                f_tokens.Close();
                f_errores.Dispose();
                f_errores.Close();
            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////
        /// Herramientas
        private t tipo(char c) {
            if (char.IsDigit(c)) return t.DIGITO;        // Numero
            else if ((c >= 'a' & c <= 'z')|(c >= 'A' & c <= 'Z')) return (byte)t.LETRA;// Letra
            else switch (c) {
                    case '_': return t.SUBRAYADO;
                    case ':': return t.DOS_PTOS;
                    case '=': return t.O_IGUAL;
                    case '>': return t.O_MAYOR;
                    case '<': return t.O_MENOR;
                    case '!': return t.O_NEGAR;
                    case '+': return t.O_SUMA;
                    case '-': return t.O_RESTA;
                    case '/': return t.O_DIVISION;
                    case '%': return t.O_MODULO;
                    case '*': return t.O_MULTIPLICACION;
                    case ';': case ',': case '(':case ')':case '{':case '}':return t.BLOQUE;
                    case '.': return t.PUNTO;
                    case ' ': return t.ESPACIO;
                    case '\n': return t.SALTO;
                    case '\t': return t.TAB;
                    case '\r': return t.CARRO;
                    default: return t.OTRO;
                }
        }
        //GET SET
        private string tipo_s(char c) {
            switch (c) {
                case '>': return "may\tMayor que";
                case '<': return "men\tMenor que";
                case '+': return "mas\tSigno mas";
                case '-': return "men\tSigno menos";
                case '/': return "div\tDivision";
                case '%': return "mod\tModulo";
                case '*': return "mul\tSigno de multiplicacion";
                case ';': return "blq\tPunto y coma";
                case ',': return "blq\tComa";
                case '(': return "blq\tParentesis que abre";
                case ')': return "blq\tParentesis que cierra";
                case '{': return "blq\tLlave que abre";
                case '}': return "blq\tLlave que cierra";
                default: return "otro";
            }
        }
        private string palabraR(string s) {
            if (Array.Exists(pR, e => e == s))
                return "pal\tPalabra reservada";
            else
                return "ide\tIdentificador";
        }
        private void error(byte e, char c) {
              Console.WriteLine("{0}\t{1}\t[ Error lexico {2}\t] {3}\t", linea, col, e, c);
            f_errores.WriteLine("{0}\t{1}\t[ Error lexico {2}\t] {3}\t", linea, col, e, c);//   <--
        }
    }
}
