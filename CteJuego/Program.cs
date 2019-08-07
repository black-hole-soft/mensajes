/*
 * Creado usando SharpDevelop
 * Usuario: Luis Casillas
 * Fecha: 11/12/2008
 * Hora: 09:48 p.m.
 * 
 */

using System;
using System.Windows.Forms;

namespace CteJuego
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
		
	}
}
