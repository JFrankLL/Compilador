using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Compilador {
    /*class Item {}
    class Symtab {public static HT[] hashTable; }
    class HT { public int tipo; public int memloc; }*/
    //********************************
    class MaquinaVirual{
        delegate void SetTextCallback(string text);
        delegate string SetTextCallback2();
        private string aux;
        int index, offset;
        Stack<Item> stack;
        string token;
        StringBuilder code;
        List<Item> memory;
        RichTextBox output;
        public static volatile bool ready;
        public MaquinaVirual(StringBuilder c, RichTextBox txt){
            index = offset = 0;
            code = c;
            stack = new Stack<Item>();
            memory = new List<Item>();
            output = txt;
            ready = false;
        }
        private void startMemory(){         //inicializa la memoria virtual
            memory.Clear();
            for (int i = 0; i < Symtab.hashTable.Length; i++)
                memory.Add(new Item());       //aqui podria mandar el mensaje de error de la variable no fue inicializada
            for (int i = 0; i < 211; i++){
                if (Symtab.hashTable[i] != null){
                    memory.RemoveAt(Symtab.hashTable[i].memloc);
                    memory.Insert(Symtab.hashTable[i].memloc, new Item(Symtab.hashTable[i].tipo));
                    Console.WriteLine("Locación de Memoria: " + Symtab.hashTable[i].memloc + " tipo: " + Symtab.hashTable[i].tipo);
                }
            }
            //por default todas las variables se inicializan en 0 o false
        }
        private bool getBoolean(String b){
            return b.Equals("1");
        }

        private void nextToken() {
            token = "";
            while (code[index] != '\n' && code[index] != ' ') {
                token += code[index];
                index++;
            }
            index++;
        }
        public void executeCode(){      
            Item a = null, b = null;
            String input;
            startMemory();
            nextToken();
            while (!token.Equals("STP")){
                switch (token){
                    case "LOD":         //Load: Cargar el valor de una variable
                        nextToken();
                        int loc = Symtab.st_lookup(token);
                        stack.Push(memory.ElementAt(loc));
                        break;
                    case "LDA":         //Load Address: Cargar la direccion de memoria de una variable
                        nextToken();
                        stack.Push(new Item(Symtab.st_lookup3(token), Symtab.st_lookup(token)));        //todas las direcciones son de tipo int
                        break;
                    case "LDI":         //Load Int: Cargar una constante numerica de tipo int
                        nextToken();
                        stack.Push(new Item("Int", int.Parse(token)));
                        break;
                    case "LDF":         //Load Float: Cargar una constante numerica de tipo float
                        nextToken();
                        stack.Push(new Item("Float", float.Parse(token)));
                        break;
                    case "LDB":         //Load Bool: Cargar valor booleano
                        nextToken();
                        stack.Push(new Item("Bool", getBoolean(token)));
                        break;
                    case "FJP":         //False Jump
                        nextToken();
                        a = stack.Pop();
                        if (a.type == "Int" && a.val_i == 0)
                            goToLabel();
                        else if (a.type == "Float" && a.val_f == 0)
                            goToLabel();
                        else if (a.type == "Bool" && !a.val_b)
                            goToLabel();
                        break;
                    case "JMP":         //Jump
                        nextToken();
                        goToLabel();
                        break;
                    case "LAB":         //Label: Definicion de un etiqueta (nada que hacer)
                        nextToken();
                        break;
                    case "STO":         //Store
                        a = stack.Pop();                //tope
                        b = stack.Pop();               //direccion
                        memory.RemoveAt(b.val_i);
                        if (a.type == "Int")
                            memory.Insert(b.val_i, new Item(a.type, a.val_i));
                        else if (a.type == "Float")
                            memory.Insert(b.val_i, new Item(a.type, a.val_f));
                        else if (a.type == "Bool")
                            memory.Insert(b.val_i, new Item(a.type, a.val_b));
                        break; 
                    case "RD":          //Read
                        input = read();
                        a = stack.Pop();                    //direccion de memoria
                        b = memory.ElementAt(a.val_i);            //valor a sobreescribir en memoria
                        try{
                            memory.RemoveAt(a.val_i);
                            if (b.type == "Int")
                                memory.Insert(a.val_i, new Item(b.type, int.Parse(input)));
                            else if (b.type == "Float")
                                memory.Insert(a.val_i, new Item(b.type, float.Parse(input)));
                            else if (b.type == "Bool")
                                memory.Insert(a.val_i, new Item(b.type, bool.Parse(input)));
                        }catch (Exception e){
                            print("\nError de formato: Los valores no coinciden\n");
                            print("Construcción Detenida\n");
                            return;
                        }
                        break;
                    case "WRT":         //Write: Escribe el tope de la pila y extrae
                        a = stack.Pop();
                        if (a.type == "Int")
                            print(a.val_i + "\n");
                        else if (a.type == "Float")
                            print(string.Format("{0:0.0##}",a.val_f) + "\n");
                        else if (a.type == "Bool")
                            print(a.val_b + "\n");
                        break;
                    case "ADD":         //Add: Suma los valores de tope y debajo de tope y deja el resultado en tope
                        b = stack.Pop();
                        a = stack.Pop();
                        if (a.type == "Int" && b.type == "Int")
                            stack.Push(new Item("Int", a.val_i + b.val_i));
                        else if (a.type == "Int" && b.type == "Float")
                            stack.Push(new Item("Float", a.val_i + b.val_f));
                        else if (a.type == "Float" && b.type == "Int")
                            stack.Push(new Item("Float", a.val_f + b.val_i));
                        else if (a.type == "Float" && b.type == "Float")
                            stack.Push(new Item("Float", a.val_f + b.val_f));
                        break;
                    case "SUB":         //Substract
                        b = stack.Pop();
                        a = stack.Pop();
                        if (a.type == "Int" && b.type == "Int")
                            stack.Push(new Item("Int", a.val_i - b.val_i));
                        else if (a.type == "Int" && b.type == "Float")
                            stack.Push(new Item("Float", a.val_i - b.val_f));
                        else if (a.type == "Float" && b.type == "Int")
                            stack.Push(new Item("Float", a.val_f - b.val_i));
                        else if (a.type == "Float" && b.type == "Float")
                            stack.Push(new Item("Float", a.val_f - b.val_f));
                        break;
                    case "MUL":         //Multiplication
                        b = stack.Pop();
                        a = stack.Pop();
                        if (a.type == "Int" && b.type == "Int")
                            stack.Push(new Item("Int", a.val_i * b.val_i));
                        else if (a.type == "Int" && b.type == "Float")
                            stack.Push(new Item("Float", a.val_i * b.val_f));
                        else if (a.type == "Float" && b.type == "Int")
                            stack.Push(new Item("Float", a.val_f * b.val_i));
                        else if (a.type == "Float" && b.type == "Float")
                            stack.Push(new Item("Float", a.val_f * b.val_f));
                        break;
                    case "DIV":         //Division
                        b = stack.Pop();
                        a = stack.Pop();
                        try{
                            if (a.type == "Int" && b.type == "Int")
                                stack.Push(new Item("Int", a.val_i / b.val_i));
                            else if (a.type == "Int" && b.type == "Float")
                                stack.Push(new Item("Float", a.val_i / b.val_f));
                            else if (a.type == "Float" && b.type == "Int")
                                stack.Push(new Item("Float", a.val_f / b.val_i));
                            else if (a.type == "Float" && b.type == "Float")
                                stack.Push(new Item("Float", a.val_f / b.val_f));
                        }catch (ArithmeticException e){
                            print("\nError aritmtico: División por cero" + "\n");
                            print("Construcción Detenida\n");
                            return;
                        }
                        break;
                    case "EQU":         //Equals to
                        b = stack.Pop();
                        a = stack.Pop();
                        if (a.type == "Int" && b.type == "Int")
                            stack.Push(new Item("Bool", a.val_i == b.val_i));
                        else if (a.type == "Int" && b.type == "Float")
                            stack.Push(new Item("Bool", a.val_i == b.val_f));
                        else if (a.type == "Float" && b.type == "Int")
                            stack.Push(new Item("Bool", a.val_f == b.val_i));
                        else if (a.type == "Float" && b.type == "Float")
                            stack.Push(new Item("Bool", a.val_f == b.val_f));
                        break;
                    case "NEQ":         //Not Equals to
                        b = stack.Pop();
                        a = stack.Pop();
                        if (a.type == "Int" && b.type == "Int")
                            stack.Push(new Item("Bool", a.val_i != b.val_i));
                        else if (a.type == "Int" && b.type == "Float")
                            stack.Push(new Item("Bool", a.val_i != b.val_f));
                        else if (a.type == "Float" && b.type == "Int")
                            stack.Push(new Item("Bool", a.val_f != b.val_i));
                        else if (a.type == "Float" && b.type == "Float")
                            stack.Push(new Item("Bool", a.val_f != b.val_f));
                        break;
                    case "GRT":         //Greater than
                        b = stack.Pop();
                        a = stack.Pop();
                        if (a.type == "Int" && b.type == "Int")
                            stack.Push(new Item("Bool", a.val_i > b.val_i));
                        else if (a.type == "Int" && b.type == "Float")
                            stack.Push(new Item("Bool", a.val_i > b.val_f));
                        else if (a.type == "Float" && b.type == "Int")
                            stack.Push(new Item("Bool", a.val_f > b.val_i));
                        else if (a.type == "Float" && b.type == "Float")
                            stack.Push(new Item("Bool", a.val_f > b.val_f));
						break;
                    case "GEQ":         //Greater Equals to
                        b = stack.Pop();
                        a = stack.Pop();
                        if (a.type == "Int" && b.type == "Int")
                            stack.Push(new Item("Bool", a.val_i >= b.val_i));
                        else if (a.type == "Int" && b.type == "Float")
                            stack.Push(new Item("Bool", a.val_i >= b.val_f));
                        else if (a.type == "Float" && b.type == "Int")
                            stack.Push(new Item("Bool", a.val_f >= b.val_i));
                        else if (a.type == "Float" && b.type == "Float")
                            stack.Push(new Item("Bool", a.val_f >= b.val_f));
                        break;
                    case "LET":         //Less than
                        b = stack.Pop();
                        a = stack.Pop();
                        if (a.type == "Int" && b.type == "Int")
                            stack.Push(new Item("Bool", a.val_i < b.val_i));
                        else if (a.type == "Int" && b.type == "Float")
                            stack.Push(new Item("Bool", a.val_i < b.val_f));
                        else if (a.type == "Float" && b.type == "Int")
                            stack.Push(new Item("Bool", a.val_f < b.val_i));
                        else if (a.type == "Float" && b.type == "Float")
                            stack.Push(new Item("Bool", a.val_f < b.val_f));
                        break;
                    case "LEQ":         //Less Equals to
                        b = stack.Pop();
                        a = stack.Pop();
                        if (a.type == "Int" && b.type == "Int")
                            stack.Push(new Item("Bool", a.val_i <= b.val_i));
                        else if (a.type == "Int" && b.type == "Float")
                            stack.Push(new Item("Bool", a.val_i <= b.val_f));
                        else if (a.type == "Float" && b.type == "Int")
                            stack.Push(new Item("Bool", a.val_f <= b.val_i));
                        else if (a.type == "Float" && b.type == "Float")
                            stack.Push(new Item("Bool", a.val_f <= b.val_f));
                        break;
                }
                nextToken();
            }
            print("\nConstrucción finalizada con éxito" + "\n");
        }
        private void goToLabel(){
            int i = 0;
            String tkn = "";
            while (!tkn.Equals(token)){
                while (!tkn.Equals("LAB")){
                    tkn = "";
                    while (code[i] != '\n' && code[i] != ' '){
                        tkn += code[i];
                        i++;
                    }
                    i++;
                }
                tkn = "";
                while (code[i] != '\n' && code[i] != ' '){
                    tkn += code[i];
                    i++;
                }
                i++;
            }
            index = i;
            token = tkn;
        }
        private void print(String s){
            SetText(s);
        }
        public String read(){
            SetText(">>  ");
            String s = "";
            while (!ready) ;
            s = SetText2();
            return aux;
        }
        private void SetText(string s){
            if (output.InvokeRequired){
                SetTextCallback d = new SetTextCallback(SetText);
                output.Invoke(d, new object[] { s });
            }else{
                output.SelectionStart = offset;
                output.Text += s;
                output.SelectionStart = output.TextLength;
                offset += s.Length;
            }
        }
        private string SetText2(){
            if (output.InvokeRequired){
                SetTextCallback2 d = new SetTextCallback2(SetText2); 
                output.Invoke(d, new object[] { });
                return "";
            }else{
                string s;
                int i = offset, tope = output.TextLength - 1;
                int word = tope - i;
                output.SelectionStart = offset;
                output.SelectionLength = word;
                s = output.SelectedText;
                ready = false;
                offset += s.Length + 1;
                s.Replace("\n", "");
                s.Replace("\r", "");
                s.Replace(" ", "");
                s.Trim();
                aux = output.SelectedText;
                output.SelectionStart = output.TextLength;
                return output.SelectedText;
            }
        }
    }
}
