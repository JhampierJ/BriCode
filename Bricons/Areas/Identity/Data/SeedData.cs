using Bricons.Data;
using Bricons.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
namespace Bricons.Areas.Identity.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            //using (var context = new BriconsContext(serviceProvider.GetRequiredService<DbContextOptions<BriconsContext>>()))
            //{
            //    if (context.Categorium.Any())
            //    {
            //        return; //Ya existen datos
            //    }
            //    var categorias = new Categorium[]
            //    {
            //        new Categorium { NombreCategoria="Ladrillos para muros"},
            //        new Categorium { NombreCategoria="Ladrillos para techos"},
            //    };

            //    context.Categorium.AddRange(categorias);
            //    context.SaveChanges();

            //    if (context.Producto.Any())
            //    {
            //        return; //Ya existen datos
            //    }

            //    string rutaImagen = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "logotipo.png");
            //    byte[] imageBytes = ConvertImageToBytes(rutaImagen);

            //    var productos = new Producto[]
            //    {
            //        new Producto { CategoriaID=1,NombreProducto="PurbaXD",Stock=11,Imagen=imageBytes},
            //         new Producto { CategoriaID=1,NombreProducto="PurbaXD",Stock=11,Imagen=imageBytes},
            //         new Producto { CategoriaID=1,NombreProducto="PurbaXD",Stock=11,Imagen=imageBytes},

            //         new Producto { CategoriaID=1,NombreProducto="PurbaXD",Stock=11,Imagen=imageBytes},
            //     new Producto { CategoriaID=1,NombreProducto="PurbaXD",Stock=11,Imagen=imageBytes},
            //     new Producto { CategoriaID=1,NombreProducto="PurbaXD",Stock=11,Imagen=imageBytes}



            //    };
            //    context.Producto.AddRange(productos);
            //    context.SaveChanges();
            //}
        }
        static byte[] ConvertImageToBytes(string imagePath)
        {
            try
            {
                byte[] imageBytes = File.ReadAllBytes(imagePath);
                return imageBytes;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al convertir la imagen a bytes: " + ex.Message);
                return null;
            }
        }
    }
}
