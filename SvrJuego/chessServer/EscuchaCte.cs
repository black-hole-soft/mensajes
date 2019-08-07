using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace chessServer
{
    class EscuchaCte
    {
        Label[] etiqs;
        TcpClient cte = null;
        NetworkStream flujo = null;
        int nCte;
        public bool unload = false;
        internal EscuchaCte(TcpClient c, Label[] e, int n)
        {
            cte = c;
            flujo = c.GetStream();
            nCte = n;
            etiqs = e;
        }
        delegate string delLeeEtiq(Label etiq);
        string leeEtiq(Label etiq)
        {
            if (etiq.InvokeRequired)
                return (string)etiq.Invoke(new delLeeEtiq(leeEtiq), etiq);
            else return etiq.Text;
        }
        delegate void delCambiaTxt(string t);
        void cambiaTxt(string nvoTxt)
        {
            if (etiqs[nCte].InvokeRequired)
                etiqs[nCte].Invoke(new delCambiaTxt(cambiaTxt), nvoTxt);
            else etiqs[nCte].Text = nvoTxt;
        }
        internal void atiende()
        {
            byte[] bytes = new byte[256];
            string datos = null;
            int i = 0;
            while (cte != null)
            {
                if ((i = flujo.Read(bytes, 0, bytes.Length)) != 0)
                {
                    datos = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    datos = datos.ToUpper();
                    if (datos.Equals("@X"))
                        cierraCte();
                    else
                    {
                        cambiaTxt(datos);
                        notificaEdo();
                    }
                }
                Thread.Sleep(10);
            }
        }
        internal void cierraCte()
        {
            if (cte != null)
            {
                cte.Close();
                cte = null;
            }
            unload = true;
        }
        internal void notificaEdo()
        {
            byte[] mensaje = null;
            string datos = "";
            for ( int i = 0; i < 10; i++ )
                if (etiqs[i] != null)
                    datos += etiqs[i].Text + "|";
            mensaje = Encoding.ASCII.GetBytes(datos);
            flujo.Write(mensaje, 0, mensaje.Length);
            flujo.Flush();
        }
    }
}
