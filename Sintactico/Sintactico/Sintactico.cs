using Compilador;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Sintactico {
    class Sintactico {
        //*********************** MAIN ***********************
        static void Main(string[] args) {
            new Sintactico().run();//(inicio) Sintactico
        }
        //****************************************************
        //  [Atributos]
        public static StreamWriter f_arbol, f_errores;
        public List<Token> tokens = new List<Token>();//tokens del codigo
        public List<string> errores = new List<string>();//errores lexicos
        public Nodo raiz;//Raiz del codigo
        Token tkn_act = new Token("null","vacio");
        public int pivote = 0;//pivote lista tokens
        //  [Contructor]
        public Sintactico() { raiz = new Nodo(); }
        //  [Metodos]
        public Nodo run() {//analizar
            f_arbol = File.CreateText("arbol.txt");//Crea archivo para guardar tokens
            f_errores = File.CreateText("errores_sin.txt");
            cargarTokens();
            raiz = prog();//arbol
            preOrden(raiz);//a texto
            f_arbol.Close();
            f_errores.Close();
            return raiz;
        }
        /////////////////////////////////////////////////////////////////////////
        //                      ┌─┐┬─┐┌─┐┌┬┐┌─┐┌┬┐┬┌─┐┌─┐                      //
        //                      │ ┬├┬┘├─┤│││├─┤ │ ││  ├─┤                      //
        //                      └─┘┴└─┴ ┴┴ ┴┴ ┴ ┴ ┴└─┘┴ ┴                      //
        public Nodo prog() {return l_dec();}//[programa]
        public Nodo l_dec() {//------------------------------------------------[lista-declaracion]
            Nodo nodo_prog = new Nodo(new Token("prg", "codigo"));//Sus hijos son todo (declaraciones, sentencias y demas...)
            while (sigToken())//leer todos los token posibles hasta el final
                foreach (Nodo n in dec().hijos)//desagrupa listas
                    nodo_prog.setHijo(n);
            return nodo_prog;
        }
        public Nodo dec() {//--------------------------------------------------[declaracion]
            if (tkn_act.tipo == tk_t.P_INT || tkn_act.tipo == tk_t.P_FLOAT || tkn_act.tipo == tk_t.P_BOOLEAN) {
                Nodo n = new Nodo();
                n.setHijo(dec_var());
                return n;//se subio un nivel""
            }
            return l_sent(); // - - - - - lista sentencias()
        }
        public Nodo dec_var() {//----------------------------------------------[declaracion-variable]
            // - - - - - tipo
            Nodo nodo_t = tipo();
            // - - - - - id
            sigToken();
            if (tkn_act.tipo == tk_t.IDE)
                nodo_t.setHijo(new Nodo(tkn_act));
            else {
                nodo_t.setHijo(new Nodo(new Token("error", "IDENTIFICADOR")));
                antToken();
                f_errores.WriteLine("error" + " IDENTIFICADOR");
            }
            // - - - - - , ID
            sigToken();
            while (tkn_act.tipo == tk_t.B_COMA) {
                sigToken();
                if (tkn_act.tipo == tk_t.IDE) {
                    nodo_t.setHijo(new Nodo(tkn_act));
                } else {
                    f_errores.WriteLine("error" + " IDENTIFICADOR");
                    nodo_t.setHijo(new Nodo(new Token("error", "IDENTIFICADOR")));
                    antToken();
                    break;
                }
                sigToken();
            }
            // - - - - - ;
            //sigToken();
            if (tkn_act.tipo != tk_t.B_PYC) {
                nodo_t.setHijo(new Nodo(new Token("error","PUNTOyCOMA_decVar")));
                f_errores.WriteLine("error" + " PUNTO Y COMA");
                antToken();
            }
            return nodo_t;
        }
        public Nodo tipo() {//-------------------------------------------------[tipo]
            if (tkn_act.tipo == tk_t.P_INT || tkn_act.tipo == tk_t.P_FLOAT || tkn_act.tipo == tk_t.P_BOOLEAN) {
                Nodo aux = new Nodo(tkn_act);
                aux.ntype = nd_t.DECL;//nodo de tipo declaracion [s]
                return aux;
            }
            return new Nodo("TIPO faltante");
        }
        public Nodo l_sent() {//-----------------------------------------------[lista-sentencias]
            //lista—sentencias —> lista—sentencia sentencia |vacio //{sentencia}
            Nodo nodo_lSent = new Nodo(new Token("null","lista"));
            nodo_lSent.setHijo(sent());
            nodo_lSent.ntype = nd_t.SENT;//nodo de tipo sentencia [s]
            return nodo_lSent;
        }
        public Nodo sent() {//-------------------------------------------------[sentencia]
            switch (tkn_act.tipo) {
                case tk_t.P_IF:     return sel();  //seleccion  (if)
                case tk_t.P_WHILE:  return itera();//iteracion  (while)
                case tk_t.P_REPEAT: return repe(); //repeticion (repeat)
                case tk_t.O_CIN:    return snt_i();//sent-in    (cin)
                case tk_t.O_COUT:   return snt_o();//sent-out   (cout)
                case tk_t.IDE: return asign();//asignacion (:=)
                case tk_t.B_PYC: return sent_expr();// (;)
                default:
                    f_errores.WriteLine("error" + " INESPERADO114");
                    return new Nodo(new Token("error","INESPERADO_sent"));
            }
        }
        public Nodo asign() {//------------------------------------------------[asignacion]
            Nodo nodo_asigna = new Nodo(new Token("null","asigna"));
            // - - - - - id
            if (tkn_act.tipo == tk_t.IDE)
                nodo_asigna.setHijo(new Nodo(tkn_act));
            else {
                f_errores.WriteLine("error" + " IDENTIFICADOR");
                nodo_asigna.setHijo(new Nodo(new Token("error", "IDENTIFICADORa")));
                antToken();
            }
            // - - - - - :=
            sigToken();
            if (tkn_act.tipo == tk_t.O_ASIGNACION)
                nodo_asigna.setToken( tkn_act);
            else {
                antToken();
                f_errores.WriteLine("error" + " IDENTIFICADOR");
                nodo_asigna.setToken(new Token("error", "IDENTIFICADORb"));
            }
            // - - - - - sent-expresion()
            sigToken();
            foreach (Nodo n in sent_expr().hijos)
                nodo_asigna.setHijo(n);
            return nodo_asigna;
        }
        public Nodo sent_expr() {//--------------------------------------------[sent-expresion]
            Nodo nodo_sentExpr = new Nodo(new Token("null","expresion"));
            if (tkn_act.tipo != tk_t.B_PYC) {//no es ;
                nodo_sentExpr.setHijo(expr());
                sigToken();
                if (tkn_act.tipo != tk_t.B_PYC) {
                    f_errores.WriteLine("error" + " PUNTO Y COMA");
                    nodo_sentExpr.setHijo(new Nodo(new Token("error", "PUNTOyCOMA_sentExpr"+tkn_act.lexema)));
                }
                return nodo_sentExpr;
            }
            if (tkn_act.tipo == tk_t.B_PYC)
                return new Nodo(tkn_act);
            else {
                f_errores.WriteLine("error" + " INESPERADO");
                return new Nodo(new Token("error", "INESPERADO_sent_expre"));
            }
        }
        public Nodo sel() {//--------------------------------------------------[seleccion]
            Nodo nodo_if = new Nodo(new Token("null", "seleccion"));
            Nodo nodo_lista_sent = new Nodo(new Token("null", "lista_sent"));
            Nodo nodo_lista_sent_2 = new Nodo(new Token("null", "lista_sent"));

            if (tkn_act.tipo == tk_t.P_IF) {//if
                nodo_if.setToken(tkn_act);
                //sigToken();
                /*if (tkn_act.tipo == tk_t.B_PA) {
                    sigToken();
                    nodo_if.setHijo(expr());//exp
                    if (tkn_act.tipo != tk_t.B_PC)
                        nodo_if.setHijo(new Nodo(new Token("error"," falta )")));
                }else {*/
                sigToken();
                nodo_if.setHijo(expr());//exp
                //}
                sigToken();
                if (tkn_act.tipo == tk_t.P_THEN) {//then
                    sigToken();
                    while (tkn_act.tipo != tk_t.P_END && tkn_act.tipo != tk_t.P_ELSE) {//genera lista de sentencias (instrucciones)
                        nodo_lista_sent.setHijo(sent());//sentencia 'n'
                        sigToken();
                    }
                    nodo_if.setHijo(nodo_lista_sent);//Construye hijo#1 del if
                    /*nodo_if.setHijo(sent());//respaldo
                    sigToken();*/
                    if (tkn_act.tipo == tk_t.P_END)//end
                        return nodo_if;
                    else if (tkn_act.tipo == tk_t.P_ELSE) {//else
                        sigToken();
                        while (tkn_act.tipo != tk_t.P_END) {//genera lista de sentencias (instrucciones)
                            nodo_lista_sent_2.setHijo(sent());//sentencia 'n'
                            sigToken();
                        }
                        nodo_if.setHijo(nodo_lista_sent_2);//Construye hijo#1 del if

                        /*nodo_if.setHijo(sent());//respaldo
                        sigToken();*/

                        if (tkn_act.tipo == tk_t.P_END)
                            return nodo_if;
                        else {
                            antToken();
                            f_errores.WriteLine("error" + " END");
                            nodo_if.setHijo(new Nodo(new Token("error", "END1")));
                            return nodo_if;
                        }
                    } else {
                        antToken();
                        f_errores.WriteLine("error" + " END");
                        nodo_if.setHijo(new Nodo(new Token("error", "END2")));
                        return nodo_if;
                    }
                } else {
                    antToken();
                    f_errores.WriteLine("error" + " THEN");
                    nodo_if.setHijo(new Nodo(new Token("error", "falta THEN")));
                }
            } else {
                antToken();
                f_errores.WriteLine("error" + " IF");
                return new Nodo(new Token("error", "IF_falta"));
            }
            antToken();
            f_errores.WriteLine("error" + " IF");
            return nodo_if;
        }
        public Nodo itera() {//------------------------------------------------[iteracion]
            Nodo nodo_while = new Nodo(new Token("null","iteracion"));
            Nodo nodo_lista_sent = new Nodo(new Token("null", "lista_sent"));
            nodo_while.setToken(tkn_act);//while
            sigToken();
            nodo_while.setHijo(expr());//expr
            sigToken();
            /*nodo_while.setHijo(sent());//sent
            sigToken();*/

            while (tkn_act.tipo != tk_t.P_END) {//genera lista de sentencias (instrucciones)
                nodo_lista_sent.setHijo(sent());//sentencia 'n'
                sigToken();
            }
            nodo_while.setHijo(nodo_lista_sent);//Construye hijo#1 del repeat

            if (tkn_act.tipo == tk_t.P_END)
                return nodo_while;
            antToken();
            f_errores.WriteLine("error" + " END");
            nodo_while.setHijo(new Nodo(new Token("error")));
            return nodo_while;
        }
        public Nodo repe() {//-------------------------------------------------[repeticion]
            Nodo nodo_rep = new Nodo(new Token("null", "until_rep"));
            Nodo nodo_lista_sent = new Nodo(new Token("null", "lista_sent"));
            nodo_rep.setToken(tkn_act);//REPEAT
            sigToken();

            while (tkn_act.tipo != tk_t.P_UNTIL) {//genera lista de sentencias (instrucciones)
                nodo_lista_sent.setHijo(sent());//sentencia 'n'
                sigToken();
            }
            nodo_rep.setHijo(nodo_lista_sent);//Construye hijo#1 del repeat

            if (tkn_act.tipo == tk_t.P_UNTIL) {//UNTIL
                sigToken();
                nodo_rep.setHijo(expr());//exp
            } else {
                antToken();
                f_errores.WriteLine("error" + " UNTIL");
                nodo_rep.setHijo(new Nodo(new Token("error","UNTIL_repe")));//sentencia
            }
            sigToken();
            if (tkn_act.tipo == tk_t.B_PYC)// todo: ;? repeat
                return nodo_rep;
            f_errores.WriteLine("error" + " PUNTO Y COMA");
            nodo_rep.setHijo(new Nodo(new Token("error", "PUNTOyCOMA_cin")));
            return nodo_rep;
        }
        public Nodo snt_i() {//------------------------------------------------[sent-in]
            Nodo nodo = new Nodo();
            nodo.setToken( tkn_act);//CIN
            sigToken();
            if (tkn_act.tipo == tk_t.IDE) {
                nodo.setHijo(new Nodo(tkn_act));
                sigToken();
                if (tkn_act.tipo == tk_t.B_PYC) {
                    return nodo;
                }
                f_errores.WriteLine("error" + " PUNTO Y COMA");
                return new Nodo(new Token("error", "PUNTOyCOMA_cin"));
            }
            f_errores.WriteLine("error" + " INESPERADO");
            return new Nodo(new Token("error"));
        }
        public Nodo snt_o() {//------------------------------------------------[sent-out]
            Nodo nodo = new Nodo();
            nodo.setToken(tkn_act);//COUT
            sigToken();
            nodo.setHijo(expr());//exp
            sigToken();
            if (tkn_act.tipo != tk_t.B_PYC) {
                f_errores.WriteLine("error" + " INESPERADO");
                nodo.setHijo(new Nodo(new Token("error", "PUNTOyCOMA_cin")));
            }
            return nodo;
        }
        public Nodo expr() {//-------------------------------------------------[expresion]
            Nodo nodo_expr = new Nodo(new Token("null","expresion"));
            // exp {op exp} // 3+5 <= 8*8
            nodo_expr.setHijo(expr_simple());
            // - - - 
            sigToken();
            if (tkn_act.tipo == tk_t.O_MENOR || tkn_act.tipo == tk_t.O_MEN_I || tkn_act.tipo == tk_t.O_MAYOR ||
                tkn_act.tipo == tk_t.O_MAY_I || tkn_act.tipo == tk_t.O_IGUALDAD || tkn_act.tipo == tk_t.O_DIFERENTE) {
                nodo_expr.setToken( tkn_act);//op
                // - - - expr
                sigToken();
                nodo_expr.setHijo(expr_simple());
                return nodo_expr; 
            }//else
                antToken();
            return nodo_expr.hijos[0];
        }
        public Nodo empadrar(Nodo hijo, Token t) {
            Nodo nodo = new Nodo(t);
            nodo.setHijo(hijo);
            return nodo;
        }
        public Nodo expr_simple() {//-----------------------------------------------[expresion-simple]
            //expr—simple suma—op termino | termino
            //1+2+3+4
            Nodo nodo = new Nodo(new Token("null", "exprSimple"));
            nodo.setHijo(term());//numero
            sigToken();
            if (tkn_act.tipo == tk_t.O_MAS || tkn_act.tipo == tk_t.O_MENOS) {// mas numers
                nodo.setToken(tkn_act);//+
                sigToken();
                nodo.setHijo(term());//numero_2
            } else {//Solo 1 numero
                antToken();
                return nodo.hijos[0];
            }
            // - - - - - - - - - - - muchos
            sigToken();
            Nodo p = nodo;//respaldo
            while (tkn_act.tipo == tk_t.O_MAS || tkn_act.tipo == tk_t.O_MENOS) {
                Nodo n = empadrar(p, tkn_act);//pone + arriba de arbol mas bajo
                sigToken();
                n.setHijo(term());//noombra + o -
                p = n;//cambia puntero
                sigToken();
                //cicla.. si se puede
            }
            antToken();
            return p;
            /*
            Nodo nieto = new Nodo();
            nieto.setHijo(new Nodo(tkn_act));
            sigToken();
            nieto.setToken(tkn_act);
            sigToken();
            nieto.setHijo(new Nodo(tkn_act));
            sigToken();
            Nodo hijo = rec(nieto, tkn_act);
            sigToken();
            Nodo padre = rec(hijo, tkn_act);
            padre.setHijo(new Nodo(tkn_act));
            return padre;
            */

            /*Nodo node = term();
            node.setHijo(new Nodo(tkn_act));
            sigToken();
            while (tkn_act.tipo == tk_t.O_MAS || tkn_act.tipo == tk_t.O_MENOS) {
                node.setToken(tkn_act);
                sigToken();
                node.setHijo(term());
                sigToken();
            }
            antToken();
            return node;*/



            //1+2+3
            /*Nodo nodo_exprS = new Nodo(new Token("null", "simple"));
            //term {op term} // todo: recursivdad
            nodo_exprS.setHijo(term());//factor
            sigToken();
            if (tkn_act.tipo == tk_t.O_MAS || tkn_act.tipo == tk_t.O_MENOS) {
                nodo_exprS.setToken(tkn_act);
                sigToken();
                nodo_exprS.setHijo(expr_simple());
                return nodo_exprS;
            }
            antToken();
            return nodo_exprS.hijos[0];//factor*/
        }
        public Nodo term() {//-------------------------------------------------[termnio]
            //termino-> termino mult—op factor | factor
            Nodo nodo = new Nodo(new Token("null", "exprSimple"));
            nodo.setHijo(fac());//numero
            sigToken();
            if (tkn_act.tipo == tk_t.O_MUL || tkn_act.tipo == tk_t.O_DIV || tkn_act.tipo == tk_t.O_MODULO) {// mas numers
                nodo.setToken(tkn_act);//*
                sigToken();
                nodo.setHijo(fac());//numero_2
            } else {//Solo 1 numero
                antToken();
                return nodo.hijos[0];
            }
            // - - - - - - - - - - - muchos
            sigToken();
            Nodo p = nodo;//respaldo
            while (tkn_act.tipo == tk_t.O_MUL || tkn_act.tipo == tk_t.O_DIV || tkn_act.tipo == tk_t.O_MODULO) {
                Nodo n = empadrar(p, tkn_act);//pone * arriba de arbol mas bajo
                sigToken();
                n.setHijo(fac());//noombra + o -
                p = n;//cambia puntero
                sigToken();
                //cicla.. si se puede
            }
            antToken();
            return p;
            //expr-simple-> expr—simple suma—op termino | termino
            //termino-> termino mult—op factor | factor

            /*Nodo node = fac();
            node.setHijo(new Nodo(tkn_act));
            sigToken();
            while (tkn_act.tipo == tk_t.O_MUL || tkn_act.tipo == tk_t.O_DIV || tkn_act.tipo == tk_t.O_MODULO) {
                node.setToken(tkn_act);
                sigToken();
                node.setHijo(fac());
                sigToken();
            }
            antToken();
            return node;//*/
            /*Nodo nodo = new Nodo(new Token("null", "term"));
            //factor {mul factor}
            nodo.setHijo(fac());
            sigToken();
            if (tkn_act.tipo == tk_t.O_MUL || tkn_act.tipo == tk_t.O_DIV || tkn_act.tipo == tk_t.O_MODULO) {
                nodo.setToken(tkn_act);
                sigToken();
                nodo.setHijo(term());
                return nodo;
            } 
            antToken();
            return nodo.hijos[0];//*/
        }
        public Nodo muit_o() {//-----------------------------------------------[mult-op]
            Nodo nodo = new Nodo(new Token("null", "MuitOP_padre"));
            if (tkn_act.tipo == tk_t.O_MUL || tkn_act.tipo == tk_t.O_DIV || tkn_act.tipo == tk_t.O_MODULO)
                return new Nodo(tkn_act);
            nodo.setHijo(new Nodo(new Token("error", "muit esperado")));
            return nodo;
        }
        public Nodo fac() {//--------------------------------------------------[factor]
            Nodo nodo_exprS = new Nodo(new Token("null", "factor"));
            //(exp)
            if (tkn_act.tipo == tk_t.B_PA) {
                sigToken();
                nodo_exprS = (expr());
                sigToken();
                if (tkn_act.tipo != tk_t.B_PC) {
                    f_errores.WriteLine("error" + " Parentesis cierre");
                    nodo_exprS.setHijo(new Nodo(new Token("error", "PARENTESIS_CIERRE")));
                }
                return nodo_exprS;
            }
            //numero
            if (tkn_act.tipo == tk_t.N_INT || tkn_act.tipo == tk_t.N_FLOAT || tkn_act.tipo == tk_t.P_FALSE || tkn_act.tipo == tk_t.P_TRUE) {
                return new Nodo(tkn_act);
            }
            //identificador
            if (tkn_act.tipo == tk_t.IDE)
                return new Nodo(tkn_act);
            else {
                f_errores.WriteLine("error" + " INESPERADO480");
                return new Nodo(new Token("error", "INESPERADO_fac"));
            }
        }
        /////////////////////////////////////////////////////////////////////////
        public void cargarTokens() {//carga la lista de tokens en memoria 
            string linea = "";
            using (StreamReader sr = new StreamReader("tokens.txt")) {
                while ((linea = sr.ReadLine()) != null) {//lee lineas de tokens.txt
                    if (linea == "")//arch vacio
                        return;
                    string s1 = linea.Split((char)9).GetValue(1).ToString();
                    string s0 = linea.Split((char)9).GetValue(0).ToString();
                    int s2;
                    try {
                        s2 = int.Parse(linea.Split((char)9).GetValue(3).ToString());//linea
#pragma warning disable CS0168 // The variable 'e' is declared but never used
                    } catch (Exception e) { s2 = 0; }
#pragma warning restore CS0168 // The variable 'e' is declared but never used
                    tokens.Add(new Token(s2, s1, s0));//nuevo lexema|token
                }
            }
        }
        public bool sigToken(string s="") {//lee el token siguiente (avanza)
            if (pivote >= tokens.Count) return false; //no hay mas tokens sin leer
            tkn_act = tokens[pivote++];//token leido

            return true;
        }
        public void antToken() { if (pivote > 0) pivote--; }//lee token anterior (regresa)
        public void preOrden(Nodo raiz, string identado="") {//recorre arbol y escribe en archivo txt
            if (raiz == null) return;//no hay arbol
            f_arbol.WriteLine("{0}{1}", identado, raiz.getToken().ToString());
            foreach (Nodo n in raiz.hijos)
                preOrden(n, identado + "\t");//hijos
        }
    }
}