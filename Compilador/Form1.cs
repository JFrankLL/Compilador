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
    public enum Message : uint {
        WM_VSCROLL = 0x0115
    }
    public enum ScrollBarCommands : uint {
        SB_THUMBPOSITION = 4
    }
    public partial class Form1 : Form{
        [DllImport("User32.dll")]
        public extern static int GetScrollPos(IntPtr hWnd, int nBar);
        [DllImport("User32.dll")]
        public extern static int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        //Atributos
        int indiceChar = 0, ren = 0, col = 0, edoActual=0, charAct=0;
        private string ruta = "", strTkn = "";
        //Constructor
        public Form1(){
            InitializeComponent();
            statusLabel.Text = string.Format("Ren: {0}, Col: {1}", 0, 0);
            nLineaBox.Font = codeBox.Font;
        }
        //////////////////////////////////////////////////////////////////////////////////////
        /// Basicos
        private void Form1_Load(object sender, EventArgs e) {//Evento: ventana cargada
            actualizaLineas();
        }
        private void codeBox_TextChanged(object sender, EventArgs e){//Area de codigo cambia
            actualizaLineas();
        }
        private void actualizaLineas() {//cuenta lineas
            int i = 0;
            nLineaBox.Clear();//limpia area de numeros
            switch (codeBox.Lines.Length) {
                case 0: nLineaBox.AppendText(++i + "\n"); break;//1
                default: foreach (string s in codeBox.Lines) nLineaBox.AppendText(++i + "\n"); break;// n
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////
        /// Menu 
        private void miAbrir_Click(object sender, EventArgs e){//Abrir archivo
            OpenFileDialog openFile1 = new OpenFileDialog();//ventana grafica de ruta
            openFile1.Filter = "SISH|*.sish;*txt";//extensiones disponibles
            if (openFile1.ShowDialog() == DialogResult.OK)//aceptar//cargar en el area de codigo como string
                codeBox.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText);
            ruta = openFile1.FileName;//resguardar ruta de archivo
        }
        private void guardarComo(object sender, EventArgs e) {//GuardarComo..
            ruta = "";
            guardar(sender, e);
        }
        private void guardar(object sender, EventArgs e) {//Guardar
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
        private void miSalir_Click(object sender, EventArgs e) { Application.Exit(); }
        ///////////////////////////////////////////////////////////////////////////////////
        ///  Textos
        private void code_Click(object sender, EventArgs e) {
            indiceChar = Int32.Parse(codeBox.SelectionStart.ToString());
            ren = codeBox.GetLineFromCharIndex(indiceChar);
            col = indiceChar - codeBox.GetFirstCharIndexFromLine(ren);
            statusLabel.Text = string.Format("Ren: {0}, Col: {1}", ren + 1, col);
            //actualizaLineas();
        }
        private void codeBox_VScroll(object sender, EventArgs e) {
            int nPos = GetScrollPos(codeBox.Handle, (int)ScrollBarType.SbVert);
            nPos <<= 16;
            uint wParam = (uint)ScrollBarCommands.SB_THUMBPOSITION | (uint)nPos;
            SendMessage(nLineaBox.Handle, (int)Message.WM_VSCROLL, new IntPtr(wParam), new IntPtr(0));
        }
        /*****************************************************************************************
                                               Compilar (Inicio)                                */
        private void compilar(object sender, EventArgs e) {//compilar
            guardar(sender, e);//respalda trabajo
            if (ruta != "") {//Si guardado
                
            }
        }
        /*                                    Compilar (FIN)
        ******************************************************************************************/
        
        
    }
}