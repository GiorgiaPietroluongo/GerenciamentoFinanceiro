using GerencFinanceiro.Dal;
using GerencFinanceiro.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GerencFinanceiro.Controllers
{
    public class DespesaController : Controller
    {
        private readonly IFinancasDAL _dal;
        public DespesaController(IFinancasDAL dal)
        {
            _dal = dal;
        }
        // GET: Despesas
        public IActionResult Index(string criterio)
        {
            var lstDespesas = _dal.GetAllDespesas().ToList();
            if (!String.IsNullOrEmpty(criterio))
            {
                lstDespesas = _dal.GetAllFiltraDespesa(criterio).ToList();
            }
            return View(lstDespesas);
        }
        public ActionResult AddEditDespesa(int itemId)
        {
            RelatorioDespesa model = new RelatorioDespesa();
            if (itemId > 0)
            {
                model = _dal.GetDespesa(itemId);
            }
            return PartialView("_despesaForm", model);
        }
        [HttpPost]
        public ActionResult Create(RelatorioDespesa novaDespesa)
        {
            if (ModelState.IsValid)
            {
                if (novaDespesa.ItemId > 0)
                {
                    _dal.UpdateDespesa(novaDespesa);
                }
                else
                {
                    _dal.AddDespesa(novaDespesa);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _dal.DeleteDespesa(id);
            return RedirectToAction("Index");
        }
        public ActionResult DespesaResumo()
        {
            return PartialView("_despesaReport");
        }
        public JsonResult GetDepesaPorPeriodo()
        {
            Dictionary<string, decimal> despesaPeriodo = _dal.CalculaDespesaPeriodo(7);
            return new JsonResult(despesaPeriodo);
        }
        public JsonResult GetDepesaPorPeriodoSemanal()
        {
            Dictionary<string, decimal> despesaPeriodoSemanal = _dal.CalculaDespesaPeriodoSemanal(7);
            return new JsonResult(despesaPeriodoSemanal);
        }
    }
}