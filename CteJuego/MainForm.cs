/*
 * Creado usando SharpDevelop
 * Usuario: Luis Casillas
 * Fecha: 11/12/2008
 * Hora: 09:48 p.m.
 * 
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

namespace CteJuego
{
	public partial class MainForm : Form
	{
		HiloComs hcoms;
		Thread hcs;
		TcpClient cxnServidor;
		public MainForm(){
			InitializeComponent();
			cxnServidor=new TcpClient("127.0.0.1",5432);
			hcoms=new HiloComs(textBox2,textBox1,cxnServidor);
			hcs=new Thread(new ThreadStart(hcoms.com));
			hcs.Start();
		}
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
            hcoms.cierraCxnS();
			hcs.Abort();
		}
        void Button1Click(object sender, System.EventArgs e)
        {
            if (textBox2.Text != "") hcoms.enviar = true;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }
	}	
}
