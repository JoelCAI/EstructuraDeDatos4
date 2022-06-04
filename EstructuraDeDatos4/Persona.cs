using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructuraDeDatos4
{
    internal class Persona
    {
		private int _documento;
		private string _nombre;
		private string _apellido;
		private string _tipoTelefono;
		private int  _telefono;

		public int Documento
		{
			get { return this._documento; }
			set { this._documento = value; }
		}

		public string Nombre
		{
			get { return this._nombre; }
			set { this._nombre = value; }
		}

		public string Apellido
		{
			get { return this._apellido; }
			set { this._apellido = value; }
		}
		public string TipoTelefono
		{
			get { return this._tipoTelefono; }
			set { this._tipoTelefono = value; }
		}

		public int Telefono
		{
			get { return this._telefono; }
			set { this._telefono = value; }
		}


		public Persona(int documento, string nombre, string apellido,string tipoTelefono, int telefono)
		{

			this._documento = documento;
			this._nombre = nombre;
			this._apellido = apellido;
			this._tipoTelefono = tipoTelefono;
			this._telefono = telefono;

		}
	}
}
