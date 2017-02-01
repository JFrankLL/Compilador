using Sintactico;
using System;
using System.Collections.Generic;

namespace Compilador {
    public class Semantico {
        //[Atributos]
        public string s;//npi
        public Nodo raiz;//arbol
        public Dictionary<string, Nodo> hashTable = new Dictionary<string, Nodo>();//tabla hash
        public string resultados;//npi
        public List<string> errores = new List<string>();//npi
        public string erroresS;//errores de adeveras
        public System.Windows.Forms.DataGridView data;
        //[Constructor]
        public Semantico(Nodo t) {
            raiz = new Nodo(t);
            erroresS = "";
        }
        //[Metodos]
        public void analizar() {
            //declaracion(raiz.hijos[0]);
            //sentencia(raiz.hijos[0]);
            foreach (Nodo n in raiz.hijos) {
                //Console.WriteLine(n.ntype);
                switch (n.ntype) {
                    case nd_t.DECL:
                        declaracion(n);
                        break;
                    case nd_t.SENT:
                        sentencia(n);
                        break;
                    default:
                        break;
                }
            }
            //Console.WriteLine(raiz.hijos[0].hijos.Count);*/
            //foreach (Nodo n in raiz.hijos[0].hijos) Console.WriteLine(n.ntype + ":" + n.dtype);
            raiz.Text = "codigo";
            formato(raiz);
            //HASH
            //Console.WriteLine("ele hash: "+hashTable.Values.Count);
            s = "";
            string ss = "";
            int indice = 0;
            foreach (KeyValuePair<string, Nodo> k in hashTable) {
                foreach (int i in k.Value.lineas)
                    ss += i.ToString()+" ";
                s += k.Key.ToString() + " \t " + k.Value.ntype.ToString() + " \t " + hashTable[k.Value.getToken().lexema].val + " \tL: " + ss + "\n";
                data.Rows.Add(k.Key.ToString(),++indice, hashTable[k.Value.getToken().lexema].val,ss);
                ss = "";
            }
            //Console.WriteLine(s);
        }
        public void formato(Nodo t) {
            foreach (Nodo n in t.hijos) {
                n.Text = n.getToken().lexema + ", "+n.ntype+((n.ntype!=nd_t.DECL)?
                    (" ("+
                    ((n.ntype==nd_t.BOOLEAN)?((n.val==0)?"false":"true"):n.getValor())+
                    ")"):""
                    );
                //(n.ntype==nd_t.DECL?n.dtype.ToString():n.ntype.ToString());
                if (n.ntype == nd_t.ERROR_SEM)
                    n.ForeColor = System.Drawing.Color.Red;
                formato(n);
            }
        }
        //***********************************************************************************************************************
        public void sentencia(Nodo t) {
            if(t != null)
                switch (t.getTipo()) {
                    // [ if ]-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case tk_t.P_IF:
                        if (t.hijos[0] != null) {
                            sentencia(t.hijos[0]);
                            /*if (t.hijos[0].type != tk_t.BOOL)
                                t.hijos[0].msg = "Se esperaba resultado booleano.";*/
                            if (t.hijos[0].ntype == nd_t.ERROR_SEM)
                                t.ntype = nd_t.ERROR_SEM;
                        }
                        if (t.hijos[1] != null) {//sentencias if 
                            foreach (Nodo n in t.hijos[1].hijos) {
                                sentencia(n);
                                if (n.ntype == nd_t.ERROR_SEM) {
                                    n.ntype = nd_t.ERROR_SEM;//ERROR
                                    erroresS += "\nSentencia repeat mal hecha" + " en " + n.getToken().Linea;
                                }
                            }
                            //sentencia(t.hijos[1]);//respaldo
                            if (t.hijos[1].ntype == nd_t.ERROR_SEM)
                                t.ntype = nd_t.ERROR_SEM;
                        }
                        if (t.hijos.Count>2 && t.hijos[2] != null) {//sentencias then
                            foreach (Nodo n in t.hijos[2].hijos) {
                                sentencia(n);
                                if (n.ntype == nd_t.ERROR_SEM) {
                                    n.ntype = nd_t.ERROR_SEM;//ERROR
                                    erroresS += "\nSentencia repeat mal hecha" + " en " + n.getToken().Linea;
                                }
                            }
                            //sentencia(t.hijos[2]);//respaldo
                            if (t.hijos[2].ntype == nd_t.ERROR_SEM)
                                t.ntype = nd_t.ERROR_SEM;
                        }
                        break;
                    // [ repeat ]  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case tk_t.P_REPEAT:// REPEAT sent UNTIL exp
                        if (t.hijos[0] != null) {//sentencias
                            foreach (Nodo n in t.hijos[0].hijos) {
                                sentencia(n);
                                if (n.ntype == nd_t.ERROR_SEM) {
                                    n.ntype = nd_t.ERROR_SEM;//ERROR
                                    erroresS += "\nSentencia repeat mal hecha" + " en " + n.getToken().Linea;
                                }
                            }
                            /*sentencia(t.hijos[0]);//respaldo
                            if (t.hijos[0].ntype == nd_t.ERROR_SEM) {
                                t.ntype = nd_t.ERROR_SEM;//ERROR
                                erroresS += "\nSentencia repeat mal hecha" + " en " + t.getToken().Linea;
                            }*/
                        }
                        if (t.hijos[1] != null) {//exp
                            sentencia(t.hijos[1]);
                            if (t.hijos[1].ntype != nd_t.BOOLEAN) {//no es comparacion de algun tipo
                                t.ntype = nd_t.ERROR_SEM;//ERROR
                                erroresS += "\nCondicion repeat mal hecha" + " en " + t.getToken().Linea;
                            }
                        }
                        break;
                    // [ while ]-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case tk_t.P_WHILE://WHILE exp sent END
                        if (t.hijos[0] != null) {//exp
                            sentencia(t.hijos[0]);
                            if (t.hijos[0].ntype != nd_t.BOOLEAN) {
                                t.ntype = nd_t.ERROR_SEM;//ERROR
                                erroresS += "\n Condicion while mal hecha" + " en " + t.getToken().Linea;
                            }
                        }
                        if (t.hijos[1] != null) {//sen
                            foreach (Nodo n in t.hijos[1].hijos) {
                                sentencia(n);
                                if (n.ntype == nd_t.ERROR_SEM) {
                                    n.ntype = nd_t.ERROR_SEM;//ERROR
                                    erroresS += "\nSentencia repeat mal hecha" + " en " + n.getToken().Linea;
                                }
                            }
                            /*sentencia(t.hijos[1]);//respaldo
                            if (t.hijos[1].ntype == nd_t.ERROR_SEM) {
                                t.ntype = nd_t.ERROR_SEM;//ERROR
                                erroresS += "\n Sentencia while mal hecha" + " en " + t.getToken().Linea;
                            }*/
                        }
                        break;
                    // [IDENTIFICADORES] -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case tk_t.IDE:
                        if (hashTable.ContainsKey(t.getToken().lexema)) {
                            t.ntype = hashTable[t.getToken().lexema].ntype;
                            t.val = hashTable[t.getToken().lexema].val;
                            hashTable[t.getToken().lexema].lineas.Add(t.getToken().Linea);
                        } else {
                            t.ntype = nd_t.ERROR_SEM;
                            erroresS += "\nDeclaracion ausente: " + t.getToken().lexema + " en " + t.getToken().Linea;
                        }
                        break;
                    // [ := ]-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case tk_t.O_ASIGNACION:
                        sentencia(t.hijos[0]);//checar tabla de simbolos
                        sentencia(t.hijos[1]);
                        if (t.hijos[1].ntype == nd_t.ERROR_SEM || t.hijos[0].ntype == nd_t.ERROR_SEM) {// Error = 1+2 | a = Error
                            t.ntype = nd_t.ERROR_SEM;//arrastrar error
                        } else {
                            if (t.hijos[0].ntype == t.hijos[1].ntype) {// x x tipo igual
                                t.ntype = t.hijos[0].ntype;
                                t.val = t.hijos[1].val;//:=
                                t.hijos[0].val = t.val;
                                if (hashTable.ContainsKey(t.hijos[0].getToken().lexema))//actualizar hash
                                    hashTable[t.hijos[0].getToken().lexema].val = t.hijos[0].val;
                            } else {//tipo diferente
                                /*if (t.hijos[1].ntype == nd_t.FLOAT && t.hijos[0].ntype == nd_t.FLOAT) {//float float
                                    t.ntype = nd_t.FLOAT;//float
                                    t.val = t.hijos[1].val;//:=
                                    t.hijos[0].val = t.val;
                                } else */if (t.hijos[1].ntype == nd_t.INT && (t.hijos[0].ntype == nd_t.INT || t.hijos[0].ntype == nd_t.FLOAT)) {//(int | float) & int
                                    t.ntype = t.hijos[0].ntype;//float o int
                                    t.val = t.hijos[1].val;//:=
                                    t.hijos[0].val = t.val;
                                    if (hashTable.ContainsKey(t.hijos[0].getToken().lexema))//actualizar hash
                                        hashTable[t.hijos[0].getToken().lexema].val = t.hijos[0].val;
                                }/*if (t.hijos[1].ntype == nd_t.BOOLEAN && t.hijos[0].ntype == nd_t.BOOLEAN) {//bool bool
                                    t.ntype = nd_t.BOOLEAN;//bool
                                    //xxxxxxxx
                                } */else {
                                    t.ntype = nd_t.ERROR_SEM;//error
                                    erroresS += "\nTipos incongruentes [" + t.hijos[0].getToken().lexema + " es "+t.hijos[0].ntype+"]" + " en " + t.getToken().Linea;
                                }
                            }
                        }
                        t.ntype = nd_t.SENT;
                        /*if (t.hijos[0].getTipo() == tk_t.IDE && t.ntype != nd_t.ERROR_SEM) {// var=x
                            t.hijos[0].val = t.hijos[1].val;//asignacion
                            if (hashTable.ContainsKey(t.hijos[0].getToken().lexema)) {//actualizar hash
                                hashTable[t.hijos[0].getToken().lexema].val = t.hijos[0].val;
                            }
                        }*/
                        break;
                    // [== !=]  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  - 
                    case tk_t.O_IGUALDAD: case tk_t.O_DIFERENTE:
                        sentencia(t.hijos[0]);
                        sentencia(t.hijos[1]);
                        if (t.hijos[0].ntype == nd_t.ERROR_SEM || t.hijos[1].ntype == nd_t.ERROR_SEM)
                            t.ntype = nd_t.ERROR_SEM;
                        else {
                            if (t.hijos[0].ntype == t.hijos[1].ntype)
                                t.ntype = nd_t.BOOLEAN;
                            else {
                                if (t.hijos[0].ntype == nd_t.BOOLEAN || t.hijos[1].ntype == nd_t.BOOLEAN)
                                    t.ntype = nd_t.ERROR_SEM;
                                else
                                    t.ntype = nd_t.BOOLEAN;
                            }
                        }
                        break;
                    // [ > >= < <= ]  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  
                    case tk_t.O_MAYOR: case tk_t.O_MAY_I: case tk_t.O_MENOR: case tk_t.O_MEN_I:
                        sentencia(t.hijos[0]);
                        sentencia(t.hijos[1]);
                        if (t.hijos[0].getTipo() == tk_t.ERROR || t.hijos[1].getTipo() == tk_t.ERROR)
                            t.ntype = nd_t.ERROR_SEM;
                        else {
                            if (t.hijos[0].getTipo() == tk_t.P_BOOLEAN || t.hijos[1].getTipo() == tk_t.P_BOOLEAN) {
                                t.ntype = nd_t.ERROR_SEM;
                                erroresS += "\nExpresion desconocida" + " en " + t.getToken().Linea;
                            } else {
                                t.ntype = nd_t.BOOLEAN;
                                t.val = evalua(t);
                            }
                        }
                        break;
                    // [+ -] -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case tk_t.O_MAS: case tk_t.O_MENOS:// 1 + 2
                        sentencia(t.hijos[0]);//subfactor
                        if (t.hijos[1] != null) {
                            sentencia(t.hijos[1]);//subfactor
                            if (t.hijos[0].ntype == nd_t.ERROR_SEM || t.hijos[1].ntype == nd_t.ERROR_SEM)//hay error
                                t.ntype =  nd_t.ERROR_SEM;
                            else {//no hay error, factores posiblemente correctos
                                if (t.hijos[0].getTipo() == tk_t.P_BOOLEAN || t.hijos[1].getTipo() == tk_t.P_BOOLEAN)//bool = error
                                    t.ntype = nd_t.ERROR_SEM;
                                else {// factores correctos
                                    if (t.hijos[0].ntype == nd_t.FLOAT || t.hijos[1].ntype == nd_t.FLOAT) {//float
                                        t.ntype = nd_t.FLOAT;
                                        t.val = evalua(t);
                                    } else {//int
                                        t.ntype = nd_t.INT;
                                        t.val = evalua(t);
                                    }
                                }
                            }
                        } else {//2 parte es null
                            if (t.hijos[0].ntype == nd_t.ERROR_SEM || t.hijos[0].getTipo() == tk_t.P_BOOLEAN)
                                t.ntype = nd_t.ERROR_SEM;
                            else {
                                if (t.hijos[0].getTipo() == tk_t.N_FLOAT)
                                    t.ntype = nd_t.FLOAT;
                                else
                                    t.ntype = nd_t.INT;
                            }
                        }
                        break;
                    // [ * / ]  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case tk_t.O_DIV: case tk_t.O_MUL:// 1 * 2
                        sentencia(t.hijos[0]);
                        sentencia(t.hijos[1]);
                        if (t.hijos[0].ntype == nd_t.ERROR_SEM || t.hijos[1].ntype == nd_t.ERROR_SEM)
                            t.ntype = nd_t.ERROR_SEM;
                        else {
                            if (t.hijos[0].ntype == nd_t.BOOLEAN || t.hijos[1].ntype == nd_t.BOOLEAN)
                                t.ntype = nd_t.ERROR_SEM;
                            else {
                                if (t.hijos[0].ntype == nd_t.FLOAT || t.hijos[1].ntype == nd_t.FLOAT) { //x *float | float * x
                                    t.ntype = nd_t.FLOAT;
                                    t.val = evalua(t);
                                    //actualiza hash
                                } else {// int int
                                    t.ntype = nd_t.INT;
                                    t.val = evalua(t);
                                    if (t.ntype == nd_t.INT)
                                        t.val = double.Parse(((int)t.val).ToString());//parseo a int
                                    //actualiza hash
                                }
                            }
                        }
                        break;
                    // [numeros]-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case tk_t.N_INT:
                    case tk_t.N_FLOAT:
                        t.val = double.Parse(t.getToken().lexema);
                        break;
                    // [res bool ] -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case tk_t.P_TRUE:
                    case tk_t.P_FALSE:
                        t.val = (t.getToken().lexema == "true") ? 1 : 0;
                        t.ntype = nd_t.BOOLEAN;
                        break;
                    // [boolean]-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -
                    case tk_t.P_BOOLEAN:
                        t.val = 0;
                        break;
                    // [ERROR]  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  - 
                    case tk_t.ERROR:
                        t.ntype = nd_t.ERROR_SEM;
                        break;
                }
        }
        //***********************************************************************************************************************
        public void declaracion(Nodo t) {
            if (t != null) {
                tk_t tipo = t.getTipo();
                nd_t aux;
                switch (tipo) {//int a;
                    case tk_t.P_BOOLEAN: case tk_t.P_INT: case tk_t.P_FLOAT:
                        aux = ((tipo==tk_t.P_INT)?nd_t.INT:
                            ((tipo==tk_t.P_FLOAT)?nd_t.FLOAT:
                            nd_t.BOOLEAN));
                        //declaracion(t.hijos[1]);//hijo derecho
                        foreach (Nodo n in t.hijos)//paso de tipo a todos los hermanos
                            if (n.getTipo() == tk_t.IDE) {
                                n.val = 0;
                                n.ntype = aux;
                                if (hashTable.ContainsKey(n.getToken().lexema)) {
                                    n.ntype = nd_t.ERROR_SEM;//Ya existe esa variable //TODO:
                                    erroresS += "\n Ide: [" + n.getToken().lexema + "] Ya ha sido declarado" + " en " + t.getToken().Linea;
                                } else {//construye tabla hash
                                    Nodo nuevo = new Nodo(n);
                                    nuevo.setTipo(tipo);
                                    hashTable.Add(n.getToken().lexema, new Nodo(n));//llave, valor
                                    if (hashTable.ContainsKey(n.getToken().lexema))
                                        hashTable[n.getToken().lexema].lineas.Add(n.getToken().Linea);
                                }
                            }
                        break;
                    /*case tk_t.IDE:
                        t.val = 0;
                        if (hashTable.ContainsKey(t.getToken().lexema))
                            t.ntype = nd_t.ERROR;//Ya existe esa variable //TODO:
                        else
                            hashTable.Add(t.getToken().lexema, t);//llave, valor
                        /*if (t.hijos[0] != null) {
                            t.hijos[0].type = type;
                            declaracion (t.hijos[0]);
                        //}*/
                        //break;*/
                    default:
                        return;
                }
            }
        }
        //***********************************************************************************************************************
        public double evalua(Nodo t) {
            double val1=0, val2=0;

            if (t.hijos[0].getTipo() != tk_t.IDE)
                val1 = t.hijos[0].val;
            else if (hashTable.ContainsKey(t.hijos[0].getToken().lexema))//ide
                val1 = hashTable[t.hijos[0].getToken().lexema].val;
            if (t.hijos[1].getTipo() != tk_t.IDE)
                val2 = t.hijos[1].val;
            else if (hashTable.ContainsKey(t.hijos[1].getToken().lexema))//ide
                val2 = hashTable[t.hijos[1].getToken().lexema].val;

            switch (t.getTipo()) {
                case tk_t.O_MAS: return val1 + val2;
                case tk_t.O_MENOS: return val1 - val2;
                case tk_t.O_DIV:
                    if (val2 == 0) {
                        erroresS += "\nError aritmetico, division por 0" + " en " + t.getToken().Linea;
                        t.ntype = nd_t.ERROR_SEM;
                        return 0;
                    }
                    return val1 / val2;
                case tk_t.O_MUL: return val1 * val2;
                case tk_t.O_MODULO: return val1 % val2;
                case tk_t.O_MENOR: return (val1 < val2) ? 1 : 0;
                case tk_t.O_MEN_I: return (val1 <= val2) ? 1 : 0;
                case tk_t.O_MAYOR: return (val1 > val2) ? 1 : 0;
                case tk_t.O_MAY_I: return (val1 >= val2) ? 1 : 0;
                case tk_t.O_IGUALDAD: return (val1 == val2) ? 1 : 0;
                case tk_t.O_DIFERENTE: return (val1 != val2) ? 1 : 0;
                default: return 0;
            }
        }
    }
}
