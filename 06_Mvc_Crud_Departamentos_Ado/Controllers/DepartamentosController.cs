using _06_Mvc_Crud_Departamentos_Ado.Models;
using _06_Mvc_Crud_Departamentos_Ado.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _06_Mvc_Crud_Departamentos_Ado.Controllers
{
    public class DepartamentosController : Controller
    {

        RepositoryDepartamentos repo;

        public DepartamentosController()
        {
            this.repo = new RepositoryDepartamentos();
        }

        public IActionResult Index()
        {

            List<Departamento> departamentos = this.repo.GetDepartamentos();

            return View(departamentos);
        }

        public IActionResult Details(int idDepartamento)
        {
            Departamento departamento = this.repo.FindDepartamento(idDepartamento);

            return View(departamento);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Departamento departamento)
        {
            this.repo.CreateDepartamento(departamento.IdDepartamento, departamento.Nombre, departamento.Localidad);
            ViewData["MENSAJE"] = "Departamento insertado";

            //return View();
            return RedirectToAction("Index");
        }

        //PARA ELIMINAR NO VAMOS A TENER NINGUNA VISTA, DIRECTAMENTE PONDREMOS UN ENLACE DENTRO DE INDEX Y QUE LLAAME A ESTE METODO.
        //DESPUES DE LEEER EL METODO LO LLEVAMOS A INDEX
        public IActionResult Delete(int idDepartamento)
        {
            this.repo.DeleteDepartamento(idDepartamento);

            return RedirectToAction("Index");
        }

        public IActionResult Update(int idDepartamento)
        {
            Departamento departamento = this.repo.FindDepartamento(idDepartamento);

            return View(departamento);
        }

        [HttpPost]
        public IActionResult Update(Departamento departamento)
        {
            this.repo.UpdateDepartamento(departamento.IdDepartamento, departamento.Nombre, departamento.Localidad);
            ViewData["MENSAJE"] = "Departamento actualizado";

            return RedirectToAction("Index");
        }
    }
}
