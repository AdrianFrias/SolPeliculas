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
                N_Genero negocioGenero = new N_Genero();
                List<E_Genero> generos = negocioGenero.ObtenerGeneros();
                ViewBag.CatalogoGeneros = generos;
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
            N_Genero negocio = new N_Genero();
            List<E_Genero> generos = negocio.ObtenerGeneros();
            ViewBag.CatalogoGeneros = generos;
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
                N_Genero negocioGenero = new N_Genero();
                List<E_Genero> generos = negocioGenero.ObtenerGeneros();
                ViewBag.CatalogoGeneros = generos;
                return View("AgregarPelicula");
            }
        }
        public ActionResult VistaEditar(int ID)
        {
            E_Pelicula pelicula = new E_Pelicula();
            N_Pelicula negocio = new N_Pelicula();
            N_Genero negocioGenero = new N_Genero();
            List<E_Genero> generos = negocioGenero.ObtenerGeneros();
            ViewBag.CatalogoGeneros = generos;
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
        public ActionResult EditarPelicula(E_Pelicula pelicula, HttpPostedFileBase archivoImagen)
        {
            N_Pelicula negocio = new N_Pelicula();
            try
            {
                if (archivoImagen != null)
                {
                    string rutaArchivo = Path.Combine(Server.MapPath("~/Imagenes"), archivoImagen.FileName);
                    archivoImagen.SaveAs(rutaArchivo);
                    pelicula.nombreImagen = archivoImagen.FileName;
                }
                else
                {
                    pelicula.nombreImagen = negocio.ObtenerPelicula(pelicula.IDPelicula).nombreImagen;
                }

                negocio.ActualizarPelicula(pelicula);
                TempData["mensaje"] = $"La pelicula: {pelicula.Nombre} se actualizo correctamente";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"{ex.Message}";
                N_Genero negocioGenero = new N_Genero();
                List<E_Genero> generos = negocioGenero.ObtenerGeneros();
                ViewBag.CatalogoGeneros = generos;
                return View("VistaEditar", pelicula);
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
        public ActionResult EliminarPelicula(E_Pelicula pelicula, string Eliminar)
        {
            N_Pelicula negocio = new N_Pelicula();
            try
            {
                if (Eliminar == "Confirmar")
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
        public ActionResult VistaGeneros()
        {
            N_Genero negocio = new N_Genero();
            List<E_Genero> generos = new List<E_Genero>();
            try
            {
                generos = negocio.ObtenerGeneros();
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"Ocurrio un error: { ex.Message}";
            }
            return View("GenerosPrincipal", generos);
        }
        public ActionResult VistaAgregarGenero()
        {
            return View("AgregarGenero");
        }
        public ActionResult AgregarGenero(E_Genero entidadgenero)
        {
            try
            {
                N_Genero negocio = new N_Genero();
                negocio.AgregarGenero(entidadgenero);
                TempData["mensaje"] = $"El genero {entidadgenero.Genero} ha sido agregado";
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"Error: {ex.Message}";
                return View("AgregarGenero");
            }
            return RedirectToAction("VistaGeneros");
        }

        public ActionResult VistaEditarGenero(int ID)
        {
            N_Genero negocio = new N_Genero();
            E_Genero genero = negocio.ObtenerGenero(ID);
            List<E_Genero> generos = negocio.ObtenerGeneros();
            ViewBag.CatalogoGeneros = generos;
            return View("EditarGenero", genero);
        }
        public ActionResult EditarGenero(E_Genero entidadgenero)
        {
            N_Genero negocio = new N_Genero();
            try
            {
                negocio.ActualizarGenero(entidadgenero);
                TempData["mensaje"] = $"El genero {entidadgenero.Genero} se actualizo correctamente";
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"Error: {ex.Message}";
                return View("EditarGenero", entidadgenero);
            }
            return RedirectToAction("VistaGeneros");
        }
        public ActionResult VistaEliminarGenero(int ID)
        {
            N_Genero negocio = new N_Genero();
            E_Genero genero = negocio.ObtenerGenero(ID);
            return View("EliminarGenero", genero);
        }
        public ActionResult EliminarGenero(E_Genero entidadgenero)
        {
            try
            {
                N_Genero negocio = new N_Genero();
                negocio.EliminarGenero(entidadgenero);
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"Error: No puede eliminarse, ese genero esta asociado a peliculas";
                return View("EliminarGenero", entidadgenero);
            }
            return RedirectToAction("VistaGeneros");
        }
        public ActionResult Buscar(string Buscador)
        {
            List<E_Pelicula> lista = new List<E_Pelicula>();
            N_Pelicula negocio = new N_Pelicula();
            if (Buscador == "")
            {
                lista = negocio.ObtenerTodos();
            }
            else
            {
                lista = negocio.BuscadorGenero(Convert.ToInt16(Buscador));
            }


            N_Genero negocioGenero = new N_Genero();
            List<E_Genero> generos = negocioGenero.ObtenerGeneros();
            ViewBag.CatalogoGeneros = generos;
            return View("Principal", lista);
        }

    }
}