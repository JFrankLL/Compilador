using Sintactico;
using System;
using System.Collections.Generic;

namespace Compilador {
    class CodigoP {
        // [Atributos]
        private Nodo raiz;//Arbol semantico
        private Semantico semantico;//referencia al objeto semantico
        private int numero_label;
        public List<Instruccion> instrucciones;//Codigo P
        private bool bandera_if;
        // [Constructores]
        public CodigoP(Semantico semantico) {
            this.semantico = semantico;
            raiz = new Nodo(semantico.raiz);//nueva referencia del arbol semantico
            instrucciones = new List<Instruccion>();
            numero_label = 1;
        }
        // [Metodos]
        public string getInstrucciones() {
            string s = "";
            foreach (Instruccion i in instrucciones)
                s += i.toString() + "\n";
            return s;
        }
        public void analizar() {
            foreach (Nodo n in raiz.hijos) {
                switch (n.ntype) {
                    case nd_t.DECL://Declaracion
                        declaracion(n);
                        break;
                    case nd_t.SENT://Sentencia
                        sentencia(n);
                        break;
                }
            }
            instrucciones.Add(new Instruccion(opCodeTab.HALT));//fin
        }
        //***********************************************************************************************************************
        public void sentencia(Nodo t, int lbl=0) {
            int l1, l2;
            if (t == null)
                return;
            switch (t.getTipo()) {
                // *[ if ]-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                case tk_t.P_IF://jlt1, jlt 2, jmp 2, lbl 2, jmp 3, lbl 1, lbl 3
                    l1 = numero_label++;
                    sentencia(t.hijos[0], l1);//condicion IF
                    //Console.WriteLine(t.hijos[0].ntype);Console.ReadKey();
                    if (t.hijos[0].ntype == nd_t.BOOLEAN && t.hijos[0].val == 0 && t.hijos.Count < 2)
                        instrucciones.Add(new Instruccion(opCodeTab.JUMP, l1.ToString()));
                    foreach (Nodo n in t.hijos[1].hijos)
                        sentencia(n);//a donde brincar
                    l2 = numero_label++;
                    instrucciones.Add(new Instruccion(opCodeTab.JUMP, l2.ToString()));
                    instrucciones.Add(new Instruccion(opCodeTab.LABEL, l1.ToString()));
                    if (t.hijos.Count > 2 && t.hijos[2] != null) {//sentencias ELSE
                        foreach (Nodo n in t.hijos[2].hijos)
                            sentencia(n);
                    }
                    instrucciones.Add(new Instruccion(opCodeTab.LABEL, l2.ToString()));
                    break;
                // [ repeat ]-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                case tk_t.P_REPEAT:// REPEAT sent UNTIL exp
                    l1 = numero_label++;
                    instrucciones.Add(new Instruccion(opCodeTab.LABEL, l1.ToString()));
                    foreach(Nodo n in t.hijos[0].hijos){
                        sentencia(n);
                    }
                    sentencia(t.hijos[1], l1);
                    break;
                // [ while ] -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                case tk_t.P_WHILE://WHILE exp sent END
                    l1 = numero_label++;
                    l2 = numero_label++;
                    instrucciones.Add(new Instruccion(opCodeTab.LABEL, l2.ToString()));
                    sentencia(t.hijos[0], l1);
                    foreach (Nodo n in t.hijos[1].hijos) {
                        sentencia(n);
                    }
                    instrucciones.Add(new Instruccion(opCodeTab.JUMP, l2.ToString()));
                    instrucciones.Add(new Instruccion(opCodeTab.LABEL, l1.ToString()));
                    break;
                // *[IDENTIFICADORES] -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                case tk_t.IDE:
                    instrucciones.Add(new Instruccion(opCodeTab.LD, t.getToken().lexema));
                    break;
                // *[ := ]-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                case tk_t.O_ASIGNACION://x = valor
                    instrucciones.Add(new Instruccion(opCodeTab.LDA, t.hijos[0].getToken().lexema));//carga direccion de x
                    sentencia(t.hijos[1]);//carga valor
                    instrucciones.Add(new Instruccion(opCodeTab.STO));//comando de guardar
                    break;
                // *[== !=]  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  - 
                case tk_t.O_IGUALDAD:// a == b
                    sentencia(t.hijos[0]);// factor a
                    sentencia(t.hijos[1]);// factor b
                    if(lbl == 0)
                        instrucciones.Add(new Instruccion(opCodeTab.JEQ));// ==
                    else
                        instrucciones.Add(new Instruccion(opCodeTab.JNE, lbl.ToString()));// ==
                    break;
                case tk_t.O_DIFERENTE:// a != b
                    sentencia(t.hijos[0]);// factor a
                    sentencia(t.hijos[1]);// factor b
                    if (lbl == 0)
                        instrucciones.Add(new Instruccion(opCodeTab.JNE));// !=
                    else
                        instrucciones.Add(new Instruccion(opCodeTab.JEQ, lbl.ToString()));// !=
                    break;
                // *[ > >= < <= ]  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  
                case tk_t.O_MAYOR:// a > b
                    sentencia(t.hijos[0]);// factor a
                    sentencia(t.hijos[1]);// factor b
                    if (lbl == 0)
                        instrucciones.Add(new Instruccion(opCodeTab.JLE));// <=
                    else
                        instrucciones.Add(new Instruccion(opCodeTab.JLE, lbl.ToString()));// <=
                    break;
                case tk_t.O_MAY_I:// a >= b
                    sentencia(t.hijos[0]);// factor a
                    sentencia(t.hijos[1]);// factor b
                    if (lbl == 0)
                        instrucciones.Add(new Instruccion(opCodeTab.JLT));// <
                    else
                        instrucciones.Add(new Instruccion(opCodeTab.JLT, lbl.ToString()));// <
                    break;
                case tk_t.O_MENOR:// a < b
                    sentencia(t.hijos[0]);// factor a
                    sentencia(t.hijos[1]);// factor b
                    if (lbl == 0)
                        instrucciones.Add(new Instruccion(opCodeTab.JGE));// >=
                    else
                        instrucciones.Add(new Instruccion(opCodeTab.JGE, lbl.ToString()));// >=
                    break;
                case tk_t.O_MEN_I:// a <= b
                    sentencia(t.hijos[0]);// factor a
                    sentencia(t.hijos[1]);// factor b
                    if (lbl == 0)
                        instrucciones.Add(new Instruccion(opCodeTab.JGT));// >
                    else
                        instrucciones.Add(new Instruccion(opCodeTab.JGT, lbl.ToString()));// >
                    break;
                // *[+ -] -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                case tk_t.O_MAS:// 1 + 2
                    sentencia(t.hijos[0]);//factor a
                    sentencia(t.hijos[1]);//factor b
                    instrucciones.Add(new Instruccion(opCodeTab.ADD));// +
                    break;
                case tk_t.O_MENOS:// a - b
                    sentencia(t.hijos[0]);//factor a
                    sentencia(t.hijos[1]);//factor b
                    instrucciones.Add(new Instruccion(opCodeTab.SUB));// -
                    break;
                // *[ * / ]  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                case tk_t.O_DIV:// a / b
                    sentencia(t.hijos[0]);// factor a
                    sentencia(t.hijos[1]);// factor b
                    instrucciones.Add(new Instruccion(opCodeTab.DIV));// /
                    break;
                case tk_t.O_MUL:// a * b
                    sentencia(t.hijos[0]);// factor a
                    sentencia(t.hijos[1]);// factor b
                    instrucciones.Add(new Instruccion(opCodeTab.MUL));// /
                    break;
                // *[numeros]-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                case tk_t.N_INT:
#pragma warning disable CS1690 // Accessing a member on 'Nodo.val' may cause a runtime exception because it is a field of a marshal-by-reference class
                    instrucciones.Add(new Instruccion(opCodeTab.LDI, t.val.ToString()));
#pragma warning restore CS1690 // Accessing a member on 'Nodo.val' may cause a runtime exception because it is a field of a marshal-by-reference class
                    break;
                case tk_t.N_FLOAT:
#pragma warning disable CS1690 // Accessing a member on 'Nodo.val' may cause a runtime exception because it is a field of a marshal-by-reference class
                    instrucciones.Add(new Instruccion(opCodeTab.LDF, t.val.ToString()));
#pragma warning restore CS1690 // Accessing a member on 'Nodo.val' may cause a runtime exception because it is a field of a marshal-by-reference class
                    break;
                // *[res bool ] -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                case tk_t.P_TRUE:case tk_t.P_FALSE:
                    instrucciones.Add(new Instruccion(opCodeTab.LDB, t.val.ToString()));
                    break;
                // [boolean] -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                case tk_t.P_BOOLEAN:
                    t.val = 0;
                    break;
                // *[cin cout]  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                case tk_t.O_CIN:
                    instrucciones.Add(new Instruccion(opCodeTab.IN, t.hijos[0].getToken().lexema));
                    break;
                case tk_t.O_COUT:
                    instrucciones.Add(new Instruccion(opCodeTab.OUT, t.hijos[0].getToken().lexema));
                    break;
            }
        }
        //***********************************************************************************************************************
        public void declaracion(Nodo t) {
            if (t != null)
                return;
            foreach (Nodo n in t.hijos)
                instrucciones.Add(new Instruccion(opCodeTab.LDA, n.getToken().lexema));
        }
    }
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    class Instruccion {
        public opCodeTab tipo_mnemonico;
        public string token;

        public Instruccion(opCodeTab tipo_mnemonico, string token="") {
            this.tipo_mnemonico = tipo_mnemonico;
            this.token = token;
        }

        public string toString() {
            return tipo_mnemonico + " " + token;
        }
    }
}