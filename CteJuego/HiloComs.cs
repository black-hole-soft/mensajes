using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;


namespace CteJuego
{
    class HiloComs
    {
        TextBox m, r;
        TcpClient cxnServ;
        NetworkStream flujo;
        internal bool enviar = false;
        internal HiloComs(TextBox m, TextBox r, TcpClient cxnServ)
        {
            this.m = m;
            this.r = r;
            this.cxnServ = cxnServ;
            flujo = cxnServ.GetStream();
        }
        delegate string delLeeTBM();
        string leeTBM()
        {
            if (m.InvokeRequired)
            {
                return (string)m.Invoke(new delLeeTBM(leeTBM));
            }
            else return m.Text;
        }
        delegate void delEscTB(string msg, TextBox d);
        void escTB(string msg, TextBox d)
        {
            if (d.InvokeRequired)
            {
                d.Invoke(new delEscTB(escTB), msg, d);
            }
            else d.Text = msg;
        }
        internal void cierraCxnS()
        {
            System.Text.ASCIIEncoding cod = new System.Text.ASCIIEncoding();
            byte[] datos = cod.GetBytes("@X");
            flujo.Write(datos, 0, datos.Length);
            cxnServ.Close();
        }
        internal void com()
        {
            byte[] datos = null;
            System.Text.ASCIIEncoding cod = new System.Text.ASCIIEncoding();
            int nBytes = 0;
            while (true)
            {
                if (enviar)
                {
                    datos = cod.GetBytes(leeTBM());
                    flujo.Write(datos, 0, datos.Length);
                    datos = new byte[256];
                    nBytes = flujo.Read(datos, 0, datos.Length);
                    flujo.Flush();
                    escTB(cod.GetString(datos, 0, nBytes), r);
                    escTB("", m);
                    enviar = false;
                }
                Thread.Sleep(10);
            }
        }
    }
}
