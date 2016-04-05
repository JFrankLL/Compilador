using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Compilador{
    public partial class Form1 : Form{

        int indiceChar = 0, ren = 0, col = 0, estado=0;
        private string ruta = "", strTkn = "";

        public Form1(){
            InitializeComponent();
            statusLabel.Text = string.Format("Ren: {0}, Col: {1}", 0, 0);
            /*foreach (string line in richTextBox1.Lines) {
                string[] strings = { "if", "else", "for", "do", "while", "int", "float", "double", "string", "short", ";" };
                int n_linea;
                foreach (string s in strings) {
                    if ((n_linea = richTextBox1.Find(s)) > 0) {
                        richTextBox1.Select(n_linea, s.Length);
                        richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                    }
                }
            }
            this.richTextBox1.DeselectAll();*/
            nLineaBox.Font = codeBox.Font;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e){
            Console.WriteLine("Codigo cambiado");
        }

        private void Form1_Load(object sender, EventArgs e) {
            actualizaLineas();
            Console.WriteLine("Cargado");
        }

        private void codeBox_TextChanged(object sender, EventArgs e){
            actualizaLineas();
        }
        ///
        /// Botones Archivo menu 
        ///
        private void miAbrir_Click(object sender, EventArgs e){
            OpenFileDialog openFile1 = new OpenFileDialog();
            openFile1.Filter = "SISH|*.sish;*txt";
            if (openFile1.ShowDialog() == DialogResult.OK)
                codeBox.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText);
            ruta = openFile1.FileName;
        }
        private void guardarToolStripMenuItem_Click(object sender, EventArgs e) {
            if (ruta != "") {
                StreamWriter sw = File.CreateText(ruta);
                foreach (string linea in codeBox.Lines)
                    sw.WriteLine(linea);
                sw.Flush();
                sw.Close();
            } else {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "SISH|*.sish|TXT|*txt";
                dialog.Title = "Save a File";
                if (dialog.ShowDialog() == DialogResult.OK) {
                    StreamWriter sw = File.CreateText(dialog.FileName);
                    foreach (string linea in codeBox.Lines)
                        sw.WriteLine(linea);
                    sw.Flush();
                    sw.Close();
                }
                ruta = dialog.FileName;
            }
        }
        private void miGuardarComo_Click(object sender, EventArgs e){
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "SISH|*.sish|TXT|*txt";
            dialog.Title = "Save a File";
            if (dialog.ShowDialog() == DialogResult.OK) {
                StreamWriter sw = File.CreateText(dialog.FileName);
                foreach (string linea in codeBox.Lines)
                    sw.WriteLine(linea);
                sw.Flush();
                sw.Close();
            }
            ruta = dialog.FileName;
        }
        private void miSalir_Click(object sender, EventArgs e){
            Application.Exit();
        }
        /// Fin botones archivo
        
        private void code_Click(object sender, EventArgs e) {
            indiceChar = Int32.Parse(codeBox.SelectionStart.ToString());
            ren = codeBox.GetLineFromCharIndex(indiceChar);
            col = indiceChar - codeBox.GetFirstCharIndexFromLine(ren);
            statusLabel.Text = string.Format("Ren: {0}, Col: {1}", ren+1, col);
        }

        private void actualizaLineas() {
            int i = 0;
            nLineaBox.Clear();
            foreach (string s in codeBox.Lines)
                nLineaBox.AppendText(++i + "\n");
            nLineaBox.ScrollToCaret();
        }
        /// 
        /// Compilacion
        ///

        private char leerChar(int indice) {
            if (ruta == "")
                return ' ';
            StreamReader sr = new StreamReader(ruta);
            string linea;
            int i = 0;
            while ((linea = sr.ReadLine()) != null)
                foreach (char c in linea.ToCharArray())
                    if (++i == indice) {
                        sr.Close();
                        return c;
                    }
            sr.Close();
            return ' ';
        }

        private bool autmataLexico(int edo, char entrada) {//Falta hacer el retorno del estado final = TRUE
            Console.WriteLine(entrada);
            switch (edo) {
                case 0://Estado inicial
                    if(entrada >= '0' && entrada <= '9') {
                        estado = 1;
                        strTkn += entrada;
                        return true;
                    } else if (entrada >= 'a' && entrada <= 'z') {//letra
                        estado = 2;
                        strTkn += entrada;
                        return true;
                    } else if (entrada == ';') {//letra
                        estado = 0;
                        lexicoBox.AppendText(strTkn + "\n");
                        lexicoBox.AppendText(";\n");
                        strTkn = "";
                        return true;
                    } else {//Otra cosa
                        estado = 0;
                        strTkn = "\n";
                        return true;
                    }
                    break;
                case 1://Puro numer
                    if (entrada >= '0' && entrada <= '9') {
                        estado = 1;
                        strTkn += entrada;
                        return true;
                    } else if (entrada >= 'a' && entrada <= 'z') {//letra ERROR
                        estado = 10;
                        lexicoBox.AppendText(strTkn + "\n");
                        strTkn = "";
                        return true;
                    } else if (entrada == ';') {//;
                        estado = 0;
                        lexicoBox.AppendText(strTkn + "\n");
                        lexicoBox.AppendText(";\n");
                        strTkn = "";
                        return true;
                    } else {//Otra cosa
                        lexicoBox.AppendText(strTkn+"\n");
                        estado = 0;
                        strTkn = "";
                        return true;
                    }
                case 2://Identificadores
                    if (entrada >= 'a' && entrada <= 'z') {//letra
                        edo = 2;
                        return true;
                    }else if (entrada == '_') {//subrayado
                        edo = 2;
                        return true;
                    }
                    break;
                default:
                    break;
            }
            return false;
        }
        //Empezar accion
        private void empezarToolStripMenuItem_Click(object sender, EventArgs e) {

            lexicoBox.Text = "";
            int i = 0;
            while (i++ != -1)
                if(!autmataLexico(estado, leerChar(i)))
                    break;
        }
        /// FIN Compilacion
    }
}