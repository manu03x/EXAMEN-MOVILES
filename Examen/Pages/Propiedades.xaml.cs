using SQLite;

namespace Examen.Pages;


public partial class Propiedades : ContentPage
{

    // Define la clase Propiedad que tiene cinco propiedades: Id, Nombre, Direccion, Telefono y CorreoElectronico.
    public class Propiedad
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Cantidad { get; set; }
        public string PrecioCosto { get; set; }
        public string PrecioVenta { get; set; }
        public string Url { get; set; }
    }

    // Define la clase PropiedadManager que maneja todas las operaciones CRUD para la entidad Propiedad.
    public class PropiedadManager
    {
        readonly SQLiteConnection database;
        public PropiedadManager(SQLiteConnection db)
        {
            database = db;
            database.CreateTable<Propiedad>();
        }

        // Obtener todos los Propiedads de la base de datos.

        public List<Propiedad> ObtenerPropiedades()
        {
            return database.Table<Propiedad>().ToList();
        }

        // Obtener un Propiedad por nombre.

        public Propiedad ObtenerPropiedadPorNombre(string nombre)
        {
            // Podrías considerar usar sentencias preparadas para evitar inyecciones SQL.
            return database.Table<Propiedad>().FirstOrDefault(c => c.Nombre == nombre);
        }

        // Guardar un Propiedad en la base de datos.
        public int GuardarPropiedad(Propiedad propiedad)
        {
            if (propiedad.Id != 0)
            {
                return database.Update(propiedad);
            }
            else
            {
                return database.Insert(propiedad);
            }
        }

        // Eliminar un Propiedad de la base de datos.

        public int EliminarPropiedad(Propiedad propiedad)
        {
            return database.Delete(propiedad);
        }
    }

    PropiedadManager propiedadManager;
    Propiedad propiedadEncontrado;

    public Propiedades()
    {
        InitializeComponent();

        //  La variable dbPath se utiliza más adelante para crear una instancia de SQLiteConnection, que se utiliza para conectarse a la base de datos y realizar operaciones CRUD en ella.
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "propiedades.db");

        // Crear una instancia de PropiedadManager y establecer la lista de Propiedads como origen de datos del ListView.
        var db = new SQLiteConnection(dbPath);

        propiedadManager = new PropiedadManager(db);

        var propiedades = propiedadManager.ObtenerPropiedades();

        propiedadesListView.ItemsSource = propiedades;
    }

    private void Guardar_Clicked(object sender, EventArgs e)
    {
        // Código para guardar un nuevo Propiedad o actualizar uno existente
        if (propiedadEncontrado != null)
        {
            // Modificar el Propiedad encontrado
            propiedadEncontrado.Descripcion = descripcionEntry.Text;
            propiedadEncontrado.Cantidad = cantidadEntry.Text;
            propiedadEncontrado.PrecioVenta = ventaEntry.Text;
            propiedadEncontrado.PrecioCosto = costoEntry.Text;
            propiedadEncontrado.Url = urlEntry.Text;

            propiedadManager.GuardarPropiedad(propiedadEncontrado);

            DisplayAlert("Modificar", "Registro Modificado!", "OK");
        }
        else
        {
            // Agregar un nuevo Propiedad
            var propiedad = new Propiedad
            {
                Nombre = nombreEntry.Text,
                Descripcion = descripcionEntry.Text,
                Cantidad = cantidadEntry.Text,
                PrecioCosto = costoEntry.Text,
                PrecioVenta = ventaEntry.Text,
                Url = urlEntry.Text
            };

            propiedadManager.GuardarPropiedad(propiedad);

            DisplayAlert("Agregar", "Registro Agregado!", "OK");
        }

        var propiedades = propiedadManager.ObtenerPropiedades();

        propiedadesListView.ItemsSource = propiedades;
    }

    private void Buscar_Clicked(object sender, EventArgs e)
    {
        // Código para buscar un Propiedad por nombre
        string nombre = nombreEntry.Text;

        propiedadEncontrado = propiedadManager.ObtenerPropiedadPorNombre(nombre);

        if (propiedadEncontrado != null)
        {
            descripcionEntry.Text = propiedadEncontrado.Descripcion;
            cantidadEntry.Text = propiedadEncontrado.Cantidad;
            ventaEntry.Text = propiedadEncontrado.PrecioVenta;
            urlEntry.Text = propiedadEncontrado.PrecioCosto;

            ModificarButton.IsEnabled = true;
        }
        else
        {
            // Mostrar un mensaje indicando que el Propiedad no fue encontrado.
            ModificarButton.IsEnabled = false;

            DisplayAlert("Buscar", "Registro no encontrado!", "OK");
        }
    }

    private void Eliminar_Clicked(object sender, EventArgs e)
    {
        // Código para eliminar un Propiedad
        if (propiedadEncontrado != null)
        {
            propiedadManager.EliminarPropiedad(propiedadEncontrado);

            LimpiarCampos();

            DisplayAlert("Eliminar", "Registro eliminado!", "OK");
        }
        else
        {
            // Mostrar un mensaje indicando que el Propiedad no fue encontrado.

            DisplayAlert("Eliminar", "Registro no encontrado!", "OK");
        }

        var Propiedads = propiedadManager.ObtenerPropiedades();

        propiedadesListView.ItemsSource = Propiedads;
    }

    private void Modificar_Clicked(object sender, EventArgs e)
    {
        // Código para habilitar campos y permitir modificaciones
        nombreEntry.IsEnabled = true;
        descripcionEntry.IsEnabled = true;
        cantidadEntry.IsEnabled = true;
        ventaEntry.IsEnabled = true;
        costoEntry.IsEnabled = true;
        urlEntry.IsEnabled = true;


    }

    private void LimpiarCampos()
    {
        // Código para limpiar los campos y deshabilitar los botones de Modificar y Eliminar
        
            nombreEntry.Text = string.Empty;
            descripcionEntry.Text = string.Empty;
            ventaEntry.Text = string.Empty;
            cantidadEntry.Text = string.Empty;
            costoEntry.Text = string.Empty;
            urlEntry.Text = string.Empty;
            propiedadEncontrado = null;
            ModificarButton.IsEnabled = false;
           // direccionEntry.IsEnabled = false;
           // telefonoEntry.IsEnabled = false;
           // correoEntry.IsEnabled = false;
          //  GuardarButton.IsEnabled = false;
    }

    }