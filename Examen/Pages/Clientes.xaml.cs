using SQLite;

namespace Examen.Pages;


public partial class Clientes : ContentPage
{

    // Define la clase Contacto que tiene cinco propiedades: Id, Nombre, Direccion, Telefono y CorreoElectronico.
    public class Contacto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string Url { get; set; }
    }

    // Define la clase ContactoManager que maneja todas las operaciones CRUD para la entidad Contacto.
    public class ContactoManager
    {
        readonly SQLiteConnection database;
        public ContactoManager(SQLiteConnection db)
        {
            database = db;
            database.CreateTable<Contacto>();
        }

        // Obtener todos los contactos de la base de datos.

        public List<Contacto> ObtenerContactos()
        {
            return database.Table<Contacto>().ToList();
        }

        // Obtener un contacto por nombre.

        public Contacto ObtenerContactoPorNombre(string nombre)
        {
            // Podrías considerar usar sentencias preparadas para evitar inyecciones SQL.
            return database.Table<Contacto>().FirstOrDefault(c => c.Nombre == nombre);
        }

        // Guardar un contacto en la base de datos.
        public int GuardarContacto(Contacto contacto)
        {
            if (contacto.Id != 0)
            {
                return database.Update(contacto);
            }
            else
            {
                return database.Insert(contacto);
            }
        }

        // Eliminar un contacto de la base de datos.

        public int EliminarContacto(Contacto contacto)
        {
            return database.Delete(contacto);
        }
    }

    ContactoManager contactoManager;
    Contacto contactoEncontrado;

    public Clientes()
    {
        InitializeComponent();

        //  La variable dbPath se utiliza más adelante para crear una instancia de SQLiteConnection, que se utiliza para conectarse a la base de datos y realizar operaciones CRUD en ella.
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "contacts.db");

        // Crear una instancia de ContactoManager y establecer la lista de contactos como origen de datos del ListView.
        var db = new SQLiteConnection(dbPath);

        contactoManager = new ContactoManager(db);

        var contactos = contactoManager.ObtenerContactos();

        contactosListView.ItemsSource = contactos;
    }

    private void Guardar_Clicked(object sender, EventArgs e)
    {
        // Código para guardar un nuevo contacto o actualizar uno existente
        if (contactoEncontrado != null)
        {
            // Modificar el contacto encontrado
            contactoEncontrado.Direccion = direccionEntry.Text;
            contactoEncontrado.Telefono = telefonoEntry.Text;
            contactoEncontrado.CorreoElectronico = correoEntry.Text;
            contactoEncontrado.Url = urlEntry.Text;

            contactoManager.GuardarContacto(contactoEncontrado);

            DisplayAlert("Modificar", "Registro Modificado!", "OK");
        }
        else
        {
            // Agregar un nuevo contacto
            var contacto = new Contacto
            {
                Nombre = nombreEntry.Text,
                Direccion = direccionEntry.Text,
                Telefono = telefonoEntry.Text,
                CorreoElectronico = correoEntry.Text,
                Url = urlEntry.Text
            };

            contactoManager.GuardarContacto(contacto);

            DisplayAlert("Agregar", "Registro Agregado!", "OK");
        }

        var contactos = contactoManager.ObtenerContactos();

        contactosListView.ItemsSource = contactos;
    }

    private void Buscar_Clicked(object sender, EventArgs e)
    {
        // Código para buscar un contacto por nombre
        string nombre = nombreEntry.Text;

        contactoEncontrado = contactoManager.ObtenerContactoPorNombre(nombre);

        if (contactoEncontrado != null)
        {
            direccionEntry.Text = contactoEncontrado.Direccion;
            telefonoEntry.Text = contactoEncontrado.Telefono;
            correoEntry.Text = contactoEncontrado.CorreoElectronico;
            urlEntry.Text = contactoEncontrado.Url;

            ModificarButton.IsEnabled = true;
        }
        else
        {
            // Mostrar un mensaje indicando que el contacto no fue encontrado.
            ModificarButton.IsEnabled = false;

            DisplayAlert("Buscar", "Registro no encontrado!", "OK");
        }
    }

    private void Eliminar_Clicked(object sender, EventArgs e)
    {
        // Código para eliminar un contacto
        if (contactoEncontrado != null)
        {
            contactoManager.EliminarContacto(contactoEncontrado);

            LimpiarCampos();

            DisplayAlert("Eliminar", "Registro eliminado!", "OK");
        }
        else
        {
            // Mostrar un mensaje indicando que el contacto no fue encontrado.

            DisplayAlert("Eliminar", "Registro no encontrado!", "OK");
        }

        var contactos = contactoManager.ObtenerContactos();

        contactosListView.ItemsSource = contactos;
    }

    private void Modificar_Clicked(object sender, EventArgs e)
    {
        // Código para habilitar campos y permitir modificaciones
        direccionEntry.IsEnabled = true;
        telefonoEntry.IsEnabled = true;
        correoEntry.IsEnabled = true;
        GuardarButton.IsEnabled = true;
    }

    private void LimpiarCampos()
    {
        // Código para limpiar los campos y deshabilitar los botones de Modificar y Eliminar
        
            nombreEntry.Text = string.Empty;
            direccionEntry.Text = string.Empty;
            telefonoEntry.Text = string.Empty;
            correoEntry.Text = string.Empty;
            urlEntry.Text = string.Empty;
            contactoEncontrado = null;
            ModificarButton.IsEnabled = false;
           // direccionEntry.IsEnabled = false;
           // telefonoEntry.IsEnabled = false;
           // correoEntry.IsEnabled = false;
          //  GuardarButton.IsEnabled = false;
    }

    }