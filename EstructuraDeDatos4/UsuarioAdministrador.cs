using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EstructuraDeDatos4
{
    internal class UsuarioAdministrador: Usuario
    {
		protected List<Persona> _persona;

		public List<Persona> Persona
		{
			get { return this._persona; }
			set { this._persona = value; }
		}

		public UsuarioAdministrador(string nombre, List<Persona> persona) : base(nombre)
		{
			this._persona = persona;
		}

		public void MenuAdministrador(List<Persona> persona)
		{

			Persona = persona;

			int opcion;
			do
			{

				Console.Clear();
				Console.WriteLine(" Bienvenido Usuario: *" + Nombre + "* ");
				opcion = Validador.PedirIntMenu("\n Menú de Registro de nuevas Personas: " +
									   "\n [1] Crear Persona" +
									   "\n [2] Grabar Persona" +
									   "\n [3] Leer Persona" +
									   "\n [4] Salir del Sistema.", 1, 4);

				switch (opcion)
				{
					case 1:
						DarAltaPersona();
						break;
					case 2:
						GrabarPersona();
						break;
					case 3:
						LeerPersona();
						break;
					case 4:

						break;

				}
			} while (opcion != 4);
		}

		public int BuscarPersonaDocumento(int documento)
		{
			for (int i = 0; i < this._persona.Count; i++)
			{
				if (this._persona[i].Documento == documento)
				{
					return i;
				}
			}
			/* si no encuentro el producto retorno una posición invalida */
			return -1;
		}

		Dictionary<int, Persona> personaLista = new Dictionary<int, Persona>();

		protected override void DarAltaPersona()
		{

			int documento;
			string nombre;
			string apellido;
			string tipoTelefono;

			int codigoPais;
			int codigoArea;
			int numeroTelefono;
			string telefono;

			string opcion;

			Console.Clear();
			documento = Validador.PedirIntMenu(" Ingrese el documento" +
											  "\n El documento debe estar entre este rango.", 1000000, 99999999);
			if (BuscarPersonaDocumento(documento) == -1)
			{
				VerPersona();
				Console.WriteLine("\n ¡En hora buena! Puede utilizar este Nombre para crear una Persona Nueva en su agenda");
				nombre = Validador.PedirCaracterString("\n Ingrese el nombre de la Persona", 0, 30);
				Console.Clear();
				apellido = Validador.PedirCaracterString("Ingrese el nombre de la Persona", 0, 30);
				Console.Clear();
				tipoTelefono = Validador.ValidarTipoTelefono("\n Ingrese el tipo de Telefono");

				codigoPais = Validador.PedirIntMenu("Ingrese el codigo de País del Teléfono", 1, 99);
				codigoArea = Validador.PedirIntMenu("Ingrese el codigo de Área del Teléfono", 1, 99);
				numeroTelefono = Validador.PedirIntMenu("Ingrese el número del Teléfono", 111111, 99999999);

				telefono = codigoPais.ToString() + codigoArea.ToString() + numeroTelefono.ToString();
				bool telefonoConvertido = int.TryParse(telefono, out int resultado);

				opcion = ValidarSioNoPersonaNoCreada("\n Está seguro que desea crear esta Persona? ", documento, nombre, apellido);

				if (opcion == "SI")
				{
					Persona p = new Persona(documento, nombre, apellido, tipoTelefono,resultado);
					AddPersona(p);
					personaLista.Add(documento, p);
					VerPersona();
					VerPersonaDiccionario();
					Console.WriteLine("\n Persona con Nombre *" + nombre + "* agregado exitósamente");
					Validador.VolverMenu();
				}
				else
				{
					VerPersona();
					Console.WriteLine("\n Como puede verificar no se creo ninguna Persona");
					Validador.VolverMenu();

				}

			}
			else
			{
				VerPersona();
				Console.WriteLine("\n Usted digitó el Documento *" + documento + "*");
				Console.WriteLine("\n Ya existe una persona con ese Documento");
				Console.WriteLine("\n Será direccionado nuevamente al Menú para que lo realice correctamente");
				Validador.VolverMenu();

			}

		}

		public void AddPersona(Persona persona)
		{
			this._persona.Add(persona);
		}


		protected override void GrabarPersona()
		{
			using (var archivoLista = new FileStream("archivoLista.txt", FileMode.Create))
			{
				using (var archivoEscrituraAgenda = new StreamWriter(archivoLista))
				{
					foreach (var persona in personaLista.Values)
					{

						var linea =
									"\n Documento de la Persona: " + persona.Documento +
									"\n Nombre de la Persona: " + persona.Nombre +
									"\n Apellido de la Persona: " + persona.Apellido +
									"\n Apellido de la Persona: " + persona.TipoTelefono +
									"\n Teléfono de la Persona: " + persona.Telefono;

						archivoEscrituraAgenda.WriteLine(linea);

					}

				}
			}
			VerPersona();
			Console.WriteLine("Se ha grabado los datos de las personas en la Agenda correctamente");
			Validador.VolverMenu();

		}

		protected override void LeerPersona()
		{
			Console.Clear();
			Console.WriteLine("\n Personas en la agenda: ");
			using (var archivoLista = new FileStream("archivoLista.txt", FileMode.Open))
			{
				using (var archivoLecturaAgenda = new StreamReader(archivoLista))
				{
					foreach (var persona in personaLista.Values)
					{


						Console.WriteLine(archivoLecturaAgenda.ReadToEnd());


					}

				}
			}
			Validador.VolverMenu();

		}


		protected string ValidarSioNoPersonaNoCreada(string mensaje, int documento, string nombre, string apellido)
		{

			string opcion;
			bool valido = false;
			string mensajeValidador = "\n Si esta seguro de ello escriba *" + "si" + "* sin los asteriscos" +
									  "\n De lo contrario escriba " + "*" + "no" + "* sin los asteriscos";
			string mensajeError = "\n Por favor ingrese el valor solicitado y que no sea vacio. ";

			do
			{
				VerPersona();

				Console.WriteLine(
								  "\n Documento de la Persona a Crear: " + documento +
								  "\n Nombre de la Persona a Crear: " + nombre +
								  "\n Apellido de la Persona a Crear: " + apellido);

				Console.WriteLine(mensaje);
				Console.WriteLine(mensajeError);
				Console.WriteLine(mensajeValidador);
				opcion = Console.ReadLine().ToUpper();
				string opcionC = "SI";
				string opcionD = "NO";

				if (opcion == "" || (opcion != opcionC) & (opcion != opcionD))
				{
					continue;

				}
				else
				{
					valido = true;
				}

			} while (!valido);

			return opcion;
		}


		protected string ValidarStringNoVacioNombre(string mensaje)
		{

			string opcion;
			bool valido = false;
			string mensajeValidador = "\n Por favor ingrese el valor solicitado y que no sea vacio.";


			do
			{
				VerPersona();
				Console.WriteLine(mensaje);
				Console.WriteLine(mensajeValidador);

				opcion = Console.ReadLine().ToUpper();

				if (opcion == "")
				{

					Console.Clear();
					Console.WriteLine("\n");
					Console.WriteLine(mensajeValidador);

				}
				else
				{
					valido = true;
				}

			} while (!valido);

			return opcion;
		}

		public void VerPersona()
		{
			Console.Clear();
			Console.WriteLine("\n Personas en Agenda");
			Console.WriteLine(" #\t\tDocumento.\t\tNombre.\t\tApellido.");
			for (int i = 0; i < Persona.Count; i++)
			{
				Console.Write(" " + (i + 1));

				Console.Write("\t\t");
				Console.Write(Persona[i].Documento);
				Console.Write("\t\t");
				Console.Write(Persona[i].Nombre);
				Console.Write("\t\t");
				Console.Write(Persona[i].Apellido);
				Console.Write("\t\t");

				Console.Write("\n");
			}

		}

		public void VerPersonaDiccionario()
		{
			Console.WriteLine("\n Personas en el Diccionario");
			for (int i = 0; i < personaLista.Count; i++)
			{
				KeyValuePair<int, Persona> persona = personaLista.ElementAt(i);

				Console.WriteLine("\n Documento: " + persona.Key);
				Persona personaValor = persona.Value;


				Console.WriteLine(" Nombre de la Persona: " + personaValor.Nombre);
				Console.WriteLine(" Apellido de la Persona: " + personaValor.Apellido);
				Console.WriteLine(" Tipo de Telefono de laPersona: " + personaValor.TipoTelefono);
				Console.WriteLine(" Telefono de la Persona: " + personaValor.Telefono);


			}


		}

	}
}
