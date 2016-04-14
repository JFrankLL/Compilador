using System;
using System.IO;
using System.Windows.Forms;

public class Lex_auto{
    //Atributos
    private RichTextBox lexicoBox;//donde mostrar tokens
    private string ruta, token="";
    private enum e: byte { INICIO, NUMERO, LETRA, OTRO};//[ESTADOS]
    private enum t: byte { ALFA, NUM, SUB, PYC, OTRO}//[Tipo de CHAR]
    private byte e_Act = (byte)e.INICIO;//estado actual
    //Constructor
    public Lex_auto(RichTextBox lexicoBox, string ruta) {
        this.lexicoBox = lexicoBox;
        this.lexicoBox.Clear();
        this.ruta = ruta;
    }
    //Metodos




    //////////////////////////////////////////////////////////////////////////////////////////////////
    /// COMPILAR
    public void compilar() {
        char c = ' ';
        using (StreamReader sr = new StreamReader(ruta)) {
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
                            case (byte)t.ALFA: case (byte)t.SUB:
                                e_Act = (byte)e.LETRA;
                                token += c;//completar token
                                break;
                            case (byte)t.PYC:
                                lexicoBox.AppendText(";\n");
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
                            case (byte)t.ALFA: case (byte)t.SUB:
                                e_Act = (byte)e.LETRA;
                                lexicoBox.AppendText(token + "\n");//agrega token a la lista
                                token = char.ToString(c);//limpia el token y agrega nuevo char
                                break;
                            case (byte)t.PYC:
                                e_Act = (byte)e.INICIO;
                                lexicoBox.AppendText(token + "\n;\n");//2 token agregados
                                token = "";
                                break;
                            case (byte)t.OTRO:
                                e_Act = (byte)e.INICIO;//estado otro
                                lexicoBox.AppendText(token + "\n");//agrega token a la lista
                                token = "";//limpia el token
                                break;
                        }
                        break;
                    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[Letra:1]
                    case (byte)e.LETRA:
                        switch (tipo(c)) {
                            case (byte)t.ALFA: case (byte)t.SUB: case (byte)t.NUM:
                                token += c;//completar token
                                break;
                            case (byte)t.PYC:
                                e_Act = (byte)e.INICIO;
                                lexicoBox.AppendText(token + "\n;\n");//2 token agregados
                                token = "";
                                break;
                            case (byte)t.OTRO:
                                e_Act = (byte)e.INICIO;//estado otro
                                lexicoBox.AppendText(token + "\n");//agrega token a la lista
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
                lexicoBox.AppendText(token + "\n");//agrega token a la lista
            sr.Close();
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////
    /// Herramientas
    private byte tipo(char c) {
        if (char.IsDigit(c)) return (byte)t.NUM;        // Numero
        else if(Char.IsLetter(c)) return (byte)t.ALFA;  // Letra
        else if (c == ';') return (byte)t.PYC;          // ;
        else if (c == '_') return (byte)t.SUB;          // _
        else return (byte)t.OTRO;                       // Otro
    }
    //GET SET


}
