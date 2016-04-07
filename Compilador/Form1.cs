using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Compilador{
    public enum ScrollBarType : uint {
        SbHorz = 0,
        SbVert = 1,
        SbCtl = 2,
        SbBoth = 3
    }
    public enum Message : uint { WM_VSCROLL = 0x0115 }
    public enum ScrollBarCommands : uint { SB_THUMBPOSITION = 4 }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////
    //Class [Ventana]
    public partial class Form1 : Form{
        [DllImport("User32.dll")] public extern static int GetScrollPos(IntPtr hWnd, int nBar);
        [DllImport("User32.dll")] public extern static int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        //Atributos
        int indiceChar = 0, ren = 0, col = 0;
        private string ruta = "";
        //Constructor
        public Form1(){
            InitializeComponent();
            statusLabel.Text = string.Format("Ren: {0}, Col: {1}", 0, 0);
            nLineaBox.Font = codeBox.Font;
        }
        // [Basicos] - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private void Form1_Load(object sender, EventArgs e) { actualizaLineas(sender, e); }//Evento: ventana cargada
        private void codeBox_TextChanged(object sender, EventArgs e){ actualizaLineas(sender, e); }//Area de codigo cambia
        private void actualizaLineas(object sender, EventArgs e) {//cuenta lineas
            int i = 0;
            nLineaBox.Clear();//limpia area de numeros
            switch (codeBox.Lines.Length) {
                case 0: nLineaBox.AppendText(++i + "\n"); break;//1
                default: foreach (string s in codeBox.Lines) nLineaBox.AppendText(++i + "\n"); break;// n
            }
            codeBox_VScroll(sender, e);
        }
        // [Menu] - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private void nuevoArch(object sender, EventArgs e) {//[Nuevo archivo]
            ruta = "";//sin guardar
            codeBox.Clear();//limpia codigo
            actualizaLineas(sender, e);//cuenta linea (pone un 1)
        }
        private void miAbrir_Click(object sender, EventArgs e){//[Abrir archivo]
            OpenFileDialog openFile1 = new OpenFileDialog();//ventana grafica de ruta
            openFile1.Filter = "SISH|*.sish;*txt";//extensiones disponibles
            if (openFile1.ShowDialog() == DialogResult.OK)//aceptar//cargar en el area de codigo como string
                codeBox.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText);
            ruta = openFile1.FileName;//resguardar ruta de archivo
        }
        private void guardar(object sender, EventArgs e) {//[Guardar]
            SaveFileDialog dialog = new SaveFileDialog();//ventana
            dialog.Filter = "SISH|*.sish|TXT|*txt";
            dialog.Title = "Save a File";
            if (ruta == "" && dialog.ShowDialog() == DialogResult.OK)// primera vez (guardar como)
                ruta = dialog.FileName;
            if (ruta != "") {
                StreamWriter sw = File.CreateText(ruta);//guardador crea archivo
                foreach (string linea in codeBox.Lines)
                    sw.WriteLine(linea);//escribe linea
                sw.Flush();
                sw.Close();//Quitar ultimas lineas (extras)
                FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.ReadWrite);
                fs.SetLength(fs.Length - 2);
                fs.Close();
            }
        }
        private void guardarComo(object sender, EventArgs e) {//[GuardarComo..]
            ruta = "";
            guardar(sender, e);
        }
        private void cortar_Click(object sender, EventArgs e) { codeBox.Copy(); codeBox.SelectedText = ""; }//ctrl + x
        private void copiar_Click(object sender, EventArgs e) { codeBox.Copy(); }//ctrl + c
        private void pegar_Click(object sender, EventArgs e) { codeBox.Paste(); }//ctrl + v
        private void miSalir_Click(object sender, EventArgs e) { Application.Exit(); }
        private void acerca_Click(object sender, EventArgs e) {
            MessageBox.Show("Compilador desarrollado en C#\nParticipantes:\nJose Francisco Lopez Lopez\nLuis Gerardo Gutierrez Castañeda",
                "Acerca de..", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        // [Textos] - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        private void code_Click(object sender, EventArgs e) {//[codigo click]
            indiceChar = Int32.Parse(codeBox.SelectionStart.ToString());//carrete
            ren = codeBox.GetLineFromCharIndex(indiceChar);//linea
            col = indiceChar - codeBox.GetFirstCharIndexFromLine(ren);//columna
            statusLabel.Text = string.Format("Ren: {0}, Col: {1}", ren + 1, col);//actualiza label
            codeBox_VScroll(sender, e);//sincroniza numeros
        }
        private void codeBox_VScroll(object sender, EventArgs e) {//[scroll V]
            int nPos = GetScrollPos(codeBox.Handle, (int)ScrollBarType.SbVert);
            nPos <<= 16;
            uint wParam = (uint)ScrollBarCommands.SB_THUMBPOSITION | (uint)nPos;
            SendMessage(nLineaBox.Handle, (int)Message.WM_VSCROLL, new IntPtr(wParam), new IntPtr(0));
        }

        /*****************************************Compilar (Inicio)*****************************************/
        private void compilar(object sender, EventArgs e) {//compilar
            guardar(sender, e);//respalda trabajo
            if (ruta != "") {//Si guardado
                
            }
        }
        /******************************************Compilar (FIN)******************************************/
    }
}