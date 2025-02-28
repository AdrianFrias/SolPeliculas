using Business;
using Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPeliculas.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            N_Pelicula negocio = new N_Pelicula();
            List<E_Pelicula> peliculas = new List<E_Pelicula>();
            try
            {
                peliculas = negocio.ObtenerTodos();
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"Ocurrio un error: { ex.Message}";
            }
            return View("Principal", peliculas);

        }
        public ActionResult VistaAgregar()
        {
            return View("AgregarPelicula");
        }
        
        public ActionResult AgregarPelicula(E_Pelicula pelicula, HttpPostedFileBase archivoImagen)
        {
            try
            {
                //Crea la ruta donde se guardara el archivo
                //archivoImagen = null;
                string rutaArchivo = Path.Combine(Server.MapPath("~/Imagenes"), archivoImagen.FileName);
                //Guardar la imagen en el servidor
                archivoImagen.SaveAs(rutaArchivo);

                N_Pelicula negocio = new N_Pelicula();
                pelicula.nombreImagen = archivoImagen.FileName;

                negocio.AgregarPelicula(pelicula);
                TempData["mensaje"] = $"La pelicula: {pelicula.Nombre} se agrego correctamente";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"{ex.Message}";
                return View("AgregarPelicula");
            }
        }
        public ActionResult VistaEditar(int ID)
        {
            E_Pelicula pelicula = new E_Pelicula();
            N_Pelicula negocio = new N_Pelicula();
            try
            {
                pelicula = negocio.ObtenerPelicula(ID);
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"{ex.Message}";
                return View("INDEX");
            }
            return View("EditarPelicula", pelicula);
        }
        public ActionResult EditarPelicula(E_Pelicula pelicula)
        {
            N_Pelicula negocio = new N_Pelicula();
            try
            {
                negocio.ActualizarPelicula(pelicula);
                TempData["mensaje"] = $"La pelicula: {pelicula.Nombre} se actualizo correctamente";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"{ex.Message}";
                return View("EditarPelicula",pelicula);
            }
        }
        public ActionResult VistaEliminar(int ID)
        {
            E_Pelicula pelicula = new E_Pelicula();
            N_Pelicula negocio = new N_Pelicula();
            try
            {
                pelicula = negocio.ObtenerPelicula(ID);
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"{ex.Message}";
                return View("INDEX");
            }
            return View("EliminarPelicula", pelicula);
        }
        public ActionResult EliminarPelicula(E_Pelicula pelicula,string Eliminar)
        {
            N_Pelicula negocio = new N_Pelicula();
            try
            {   if(Eliminar== "Confirmar")
                {
                    negocio.EliminarPelicula(pelicula);
                    TempData["mensaje"] = $"La pelicula: {pelicula.Nombre} se Elimino correctamente";
                }
                else
                {
                    TempData["mensaje"] = $"Cancelada operacion de eliminacion de la pelicula: {pelicula.Nombre}";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"{ex.Message}";
                return View("EliminarPelicula", pelicula);
            }
        }
    }
}