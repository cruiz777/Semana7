using Semana7.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Semana7.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Elemento : ContentPage
    {
        public int idSeleccionado;
        private SQLiteAsyncConnection con;
        IEnumerable<Estudiante> RUpdate;
        IEnumerable<Estudiante> RDelete;
        
        public Elemento(int id, string nombre, string usuario, string password)
        {
            InitializeComponent();
            idSeleccionado = id;
            con = DependencyService.Get<Database>().GetConnection();
            txtNombre.Text = nombre;
            txtUsuario.Text = usuario;
            txtPassword.Text = password;
        }

        public static IEnumerable<Estudiante> Delete(SQLiteConnection db, int id)
        {
            return db.Query<Estudiante>("DELETE FROM Estudiante Where Id= ? ", id);
        }
        public static IEnumerable<Estudiante> Update(SQLiteConnection db, string nombre, string usuario, string password, int id)
        {
            return db.Query<Estudiante>("UPDATE  Estudiante SET Nombre=?, Usuario=?, Password=? Where Id=? ", nombre, usuario, password, id);
        }
        private void btnActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(databasePath);
                RUpdate = Update(db, txtNombre.Text, txtUsuario.Text, txtPassword.Text, idSeleccionado);
                Navigation.PushAsync(new ConsultaRegistro());
            }
            catch (Exception )
            {
                throw;
            }

        }

        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(databasePath);
                RDelete = Delete(db, idSeleccionado);
                Navigation.PushAsync(new ConsultaRegistro());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}