using GerencFinanceiro.Dal;
using GerencFinanceiro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GerencFinanceiro.Controllers
{
    public class FinancasController : Controller
    {
        private readonly IFinancasDAL _dal; // Injeção de dependência para IFinancasDAL
        private readonly AppDbContext _context; // Injeção de dependência para AppDbContext

        // Construtor unificado
        public FinancasController(IFinancasDAL dal, AppDbContext context)
        {
            _dal = dal; // Inicializa IFinancasDAL
            _context = context; // Inicializa AppDbContext
        }

        // GET: Despesas
        public IActionResult Index(string criterio)
        {
            var lstFinancas = _dal.GetAllFinancas().ToList();
            if (!String.IsNullOrEmpty(criterio))
            {
                lstFinancas = _dal.GetAllFiltraFinancas(criterio).ToList();
            }
            return View(lstFinancas);
        }

        public ActionResult AddEditDespesa(int itemId)
        {
            Financas model = new Financas();
            if (itemId > 0)
            {
                model = _dal.GetFinancas(itemId);
            }
            return PartialView("_despesaForm", model);
        }

        public ActionResult AddEditReceita(int itemId)
        {
            Financas model = new Financas();
            if (itemId > 0)
            {
                model = _dal.GetFinancas(itemId);
            }
            return PartialView("_receitaForm", model);
        }

        [HttpPost]
        public ActionResult Create(Financas novaFinanca)
        {
            if (novaFinanca.Categoria != null
                && novaFinanca.Date != null
                && novaFinanca.ItemNome != null
                && novaFinanca.Valor != null)
            {
                if (novaFinanca.ItemId > 0)
                {
                    _dal.UpdateFinancas(novaFinanca);
                }
                else
                {
                    _dal.AddFinancas(novaFinanca);
                }
            }
            return RedirectToAction("Index");
        }

        // Método para obter dados do dashboard
        public IActionResult GetDashboardData()
        {
            var despesas = _context.Financas
                .Where(f => !f.IsReceita)
                .GroupBy(f => f.Date.Month)
                .Select(g => new { Mes = g.Key, Total = g.Sum(f => f.Valor) })
                .ToList();

            var receitas = _context.Financas
                .Where(f => f.IsReceita)
                .GroupBy(f => f.Date.Month)
                .Select(g => new { Mes = g.Key, Total = g.Sum(f => f.Valor) })
                .ToList();

            // Adicione logs para verificar os dados
            Console.WriteLine($"Despesas: {JsonConvert.SerializeObject(despesas)}");
            Console.WriteLine($"Receitas: {JsonConvert.SerializeObject(receitas)}");

            return Json(new { Despesas = despesas, Receitas = receitas });
        }



        [HttpPost]
        public IActionResult Delete(int id)
        {
            _dal.DeleteFinancas(id);
            return RedirectToAction("Index");
        }

        public ActionResult FinancaResumo(int period)
        {
            var financaResumo = _dal.CalculaFinancasPeriodo(period);
            return PartialView("_despesaReport", financaResumo);
        }

        public JsonResult GetFinancaPorPeriodo(int period)
        {
            // Recupera as receitas do período
            Dictionary<string, decimal> receitas = _dal.CalculaFinancasPeriodoReceitas(period);

            // Retorna um objeto JSON que inclui a propriedade "Receitas"
            return new JsonResult(new { Receitas = receitas });
        }



        public JsonResult GetFinancaPorPeriodoSemanal()
        {
            Dictionary<string, decimal> financaPeriodoSemanal = _dal.CalculaFinancasPeriodoSemanal(7);
            return new JsonResult(financaPeriodoSemanal);
        }

        public JsonResult GetCategoriasDespesas()
        {
            Dictionary<string, decimal> categorais = _dal.GetCategoriasDespesas();
            return new JsonResult(categorais);
        }

        public JsonResult GetCategoriasReceitas()
        {
            Dictionary<string, decimal> categorais = _dal.GetCategoriasReceitas();
            return new JsonResult(categorais);
        }

        public IActionResult Dashboard()
        {
            // Aqui você pode chamar GetDashboardData e passar os dados
            var dashboardData = GetDashboardData();

            return View(dashboardData);
        }



    }
}
