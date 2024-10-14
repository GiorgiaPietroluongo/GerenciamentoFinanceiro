using GerencFinanceiro.Models;
using System.Collections.Generic;

namespace GerencFinanceiro.Dal
{
    public interface IFinancasDAL
    {
        IEnumerable<Financas> GetAllFinancas();
        IEnumerable<Financas> GetAllFiltraFinancas(string criterio);
        void AddFinancas(Financas financas);
        int UpdateFinancas(Financas financas);
        Financas GetFinancas(int id);
        void DeleteFinancas(int id);
        Dictionary<string, decimal> CalculaFinancasPeriodo(int periodo);
        Dictionary<string, decimal> CalculaFinancasPeriodoSemanal(int periodo);
        Dictionary<string, decimal> GetCategoriasDespesas();
        Dictionary<string, decimal> GetCategoriasReceitas();
        Dictionary<string, decimal> CalculaFinancasPeriodoReceitas(int period);
    }
}