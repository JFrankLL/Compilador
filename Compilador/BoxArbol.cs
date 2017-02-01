using System.Windows.Forms;
namespace Compilador {
    class BoxArbol : TreeView{
        public bool estado = false;
        public BoxArbol() { }
        public void setEstado() { estado = true; }
    }
}
