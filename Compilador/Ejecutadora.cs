using Sintactico;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Compilador {
    class Ejecutadora {
        // [Atributos]
        private List<Instruccion> instrucciones;//Codigo P
        private List<Etiqueta> etiquetas;//lista de numero de instruccion para etiquetas
        private Stack<Dato> pila;//memoria
        private Dictionary<string, Nodo> hashTable;//tabla hash
        private RichTextBox consola;
        // [Constructor]
        public Ejecutadora(List<Instruccion> instrucciones, Dictionary<string, Nodo> hashTable) {
            this.instrucciones = instrucciones;//referencia
            etiquetas = new List<Etiqueta>();
            pila = new Stack<Dato>();
            this.hashTable = hashTable;//referencia

            listaEtiquetas();

        }
        // [Metodos]
        public void setConsola(RichTextBox consola) {
            this.consola = consola;
        }
        //***************************************************************************************************************
        public void ejecutar() {
            Console.Clear();
            consola.Text = "";
            for (int i=0; i<instrucciones.Count; i++) {//recorre las instrucciones (codigo P)
                Dato a, b, aux;
                //paso a paso-------------
                //Console.Write(instrucciones[i].tipo_mnemonico + " " + instrucciones[i].token + "     [");
                //foreach (Dato d in pila)Console.Write(d + ", ");Console.WriteLine("]");
                //Console.ReadLine();
                //------------------------
                switch (instrucciones[i].tipo_mnemonico) {
                    // *[Halt: fin]-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case opCodeTab.HALT:
                        Console.WriteLine("Ejecucion terminada");
                        //Console.ReadKey();
                        break;
                    // *[IN: cin]  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case opCodeTab.IN:
                        Console.Write("Insertar valor para " + instrucciones[i].token + ": ");
                        string entrada = Console.ReadLine();
                        entrada.Replace("\n", "");
                        entrada.Replace("\r", "");
                        entrada.Replace(" ", "");
                        entrada.Trim();
                        try {
                            insertaHash(instrucciones[i].token, double.Parse(entrada));
                        } catch (Exception e) {
                            Console.WriteLine("Formato incorrecto");
                            consola.AppendText("Formato incorrecto" + "\n" 
                                + "El programa no fue ejecutado." + "\n");
                            return;
                        }
                        Console.WriteLine();//salto de linea
                        break;
                    // [OUT: cout]-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case opCodeTab.OUT:
                        if (!isHash(instrucciones[i].token)) {//palabra personal
                            Console.Write(/*instrucciones[i].token+*/"\n");
                            consola.AppendText(/*instrucciones[i].token + */"\n");
                            break;
                        }
                        //Console.Write(instrucciones[i].token + ": ");
                        //consola.AppendText(instrucciones[i].token + ": ");
                        if (tipoHash(instrucciones[i].token) == tk_t.P_BOOLEAN) {
                            int aux_s = (int)cargaHash(instrucciones[i].token);
                            switch (aux_s) {
                                case 0:
                                    Console.WriteLine("False");
                                    consola.AppendText("False" + "\n");
                                    break;
                                case 1:
                                    Console.WriteLine("True");
                                    consola.AppendText("True" + "\n");
                                    break;
                                default:
                                    Console.WriteLine("Boolean desconocido");
                                    consola.AppendText("Boolean desconocido" + "\n");
                                    break;
                            }
                        } else {
                            Console.Write(cargaHash(instrucciones[i].token));
                            consola.AppendText(cargaHash(instrucciones[i].token).ToString());
                        }
                        break;
                    // *[ADD: +]-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case opCodeTab.ADD:
                        b = pila.Pop();
                        a = pila.Pop();
                        pila.Push(new Dato(a.valor + b.valor));
                        break;
                    // *[SUB: -]-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case opCodeTab.SUB:
                        b = pila.Pop();
                        a = pila.Pop();
                        pila.Push(new Dato(a.valor - b.valor));
                        break;
                    // *[MUL: *]-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case opCodeTab.MUL:
                        b = pila.Pop();
                        a = pila.Pop();
                        pila.Push(new Dato(a.valor * b.valor));
                        break;
                    // *[DIV: /]-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case opCodeTab.DIV:
                        b = pila.Pop();
                        a = pila.Pop();
                        pila.Push(new Dato(a.valor / b.valor));
                        break;
                    // *[LD: carga valor]-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    //busca un valor de identificador
                    case opCodeTab.LD:
                        aux = new Dato(cargaHash(instrucciones[i].token));
                        pila.Push(aux);//carga valor de parametro
                        break;
                    // *[STO: guardar]-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case opCodeTab.STO:
                        aux = pila.Pop();//valor a insertar
                        insertaHash(pila.Peek().identificador, aux.valor);
                        pila.Pop();
                        break;
                    // [LDA: carga dir] -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case opCodeTab.LDA:
                        pila.Push(new Dato(instrucciones[i].token));//carga direccion
                        break;
                    // [LDC: carga cons]-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case opCodeTab.LDC:
                        break;
                    // [JLT: <]-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case opCodeTab.JLT:
                        b = pila.Pop();
                        a = pila.Pop();
                        if (a.valor < b.valor) {//incumple
                            foreach (Etiqueta e in etiquetas) {//busca posicion de etiqueta
                                if (e.numero == instrucciones[i].token) {
                                    i = e.linea;
                                    break;
                                }
                            }
                        }
                        break;
                    // [JLE: <=]  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case opCodeTab.JLE:
                        b = pila.Pop();
                        a = pila.Pop();
                        if (a.valor <= b.valor) {//incumple
                            foreach (Etiqueta e in etiquetas) {//busca posicion de etiqueta
                                if (e.numero == instrucciones[i].token) {
                                    i = e.linea;
                                    break;
                                }
                            }
                        }
                        break;
                    // [JGT: >]-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case opCodeTab.JGT:
                        b = pila.Pop();
                        a = pila.Pop();
                        if (a.valor > b.valor) {//incumple
                            foreach (Etiqueta e in etiquetas) {//busca posicion de etiqueta
                                if (e.numero == instrucciones[i].token) {
                                    i = e.linea;
                                    break;
                                }
                            }
                        }
                        break;
                    // [JGE: >=]  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case opCodeTab.JGE:
                        b = pila.Pop();
                        a = pila.Pop();
                        if (a.valor >= b.valor) {//incumple
                            foreach (Etiqueta e in etiquetas) {//busca posicion de etiqueta
                                if (e.numero == instrucciones[i].token) {
                                    i = e.linea;
                                    break;
                                }
                            }
                        }
                        break;
                    // [JEQ: ==]  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    // brinca a lbl si no cumple: while(a!=b);end lbl
                    case opCodeTab.JEQ:
                        b = pila.Pop();
                        a = pila.Pop();
                        if (a.valor == b.valor) {//incumple
                            foreach (Etiqueta e in etiquetas) {//busca posicion de etiqueta
                                if (e.numero == instrucciones[i].token) {
                                    i = e.linea;
                                    break;
                                }
                            }
                        }
                        break;
                    // *[JNE: !=]  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    // brinca a lbl si no cumple: while(a==b);end lbl
                    case opCodeTab.JNE:
                        b = pila.Pop();
                        a = pila.Pop();
                        if (a.valor != b.valor) {//incumple
                            foreach (Etiqueta e in etiquetas) {//busca posicion de etiqueta
                                if (e.numero == instrucciones[i].token) {
                                    i = e.linea;
                                    Console.WriteLine(i);
                                    break;
                                }
                            }
                        }
                        break;
                    // *[LABEL] -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case opCodeTab.LABEL:
                        //etiquetas.Add(new Etiqueta(i, instrucciones[i].token));//agrega etiqueta nueva
                        break;
                    // *[JUMP]  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case opCodeTab.JUMP:
                        //Console.WriteLine("====================jump "+instrucciones[i].token);
                        foreach (Etiqueta e in etiquetas)//busca posicion de etiqueta
                            if (e.numero == instrucciones[i].token) {
                                i = e.linea;
                                break;
                            }
                        break;
                    // [F_JUMP]-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case opCodeTab.F_JUMP:
                        break;
                    // *[LDB: carga bool]-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case opCodeTab.LDB:
                        aux = new Dato(tk_t.P_BOOLEAN, double.Parse(instrucciones[i].token));
                        pila.Push(aux);
                        break;
                    // *[LDF: carga float]  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case opCodeTab.LDF:
                        aux = new Dato(tk_t.N_FLOAT, double.Parse(instrucciones[i].token));
                        pila.Push(aux);
                        break;
                    // *[LDI: carga int] -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case opCodeTab.LDI:
                        aux = new Dato(tk_t.N_INT, double.Parse(instrucciones[i].token));
                        pila.Push(aux);
                        break;
                }

                
            }
        }
        //***************************************************************************************************************
        private void listaEtiquetas() {
            int ii = 0;
            foreach (Instruccion i in instrucciones) {
                if (i.tipo_mnemonico == opCodeTab.LABEL)
                    etiquetas.Add(new Etiqueta(ii, instrucciones[ii].token));//agrega etiqueta nueva
                ii++;
            }
        }

        private string leer() {
            return Console.ReadLine();
        }
        private void imprimir(string elemento) {
            Console.WriteLine(elemento);
        }
        private void insertaHash(string lexema, double val) {
            if (hashTable.ContainsKey(lexema)) {//actualizar hash
                switch (hashTable[lexema].getTipo()) {
                    case tk_t.P_INT:
                        hashTable[lexema].val = (int)val;//actualiza valor
                        break;
                    case tk_t.P_FLOAT:
                        hashTable[lexema].val = (float)val;//actualiza valor
                        break;
                    case tk_t.P_BOOLEAN:
                        hashTable[lexema].val = (val>0)?1:0;//actualiza valor
                        break;
                }
                //hashTable[lexema].val = val;//actualiza valor
            }
        }
        private double cargaHash(string lexema) {
            if (hashTable.ContainsKey(lexema))//actualizar hash
                return hashTable[lexema].val;
            return 0;
        }
        private tk_t tipoHash(string lexema) {
            Nodo val;
            if (hashTable.ContainsKey(lexema)) {
                val = hashTable[lexema];
                switch (val.getTipo()) {
                    case tk_t.N_INT:case tk_t.N_FLOAT:
                        return val.getTipo();
                    case tk_t.P_BOOLEAN:
                        return val.getTipo();
                    default:
                        return tk_t.ERROR;
                }
            } else
                return tk_t.ERROR;
        }
        private bool isHash(string lexema) {
            if (hashTable.ContainsKey(lexema))
                return true;
            return false;
        }
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        class Etiqueta {
            public int linea;
            public string numero;
            public Etiqueta(int linea, string numero) {
                this.linea = linea;
                this.numero = numero;
            }
        }
        class Dato {
            public tk_t tipo;
            public double valor;
            public string identificador;
            public Dato(tk_t tipo, double valor) {
                this.tipo = tipo;
                this.valor = valor;
            }
            public Dato(double valor) {
                this.tipo = tk_t.NULL;//sin tipo
                this.valor = valor;
            }
            public Dato(string identificador) {
                this.tipo = tk_t.IDE;
                this.identificador = identificador;
            }

            public override string ToString() {
                return tipo + " " + valor + " " + identificador;
            }
        }
    }
}
