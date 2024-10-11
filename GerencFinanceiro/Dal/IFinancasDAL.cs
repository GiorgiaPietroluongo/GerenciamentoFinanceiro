using GerencFinanceiro.Models;
using System.Collections.Generic;

namespace GerencFinanceiro.Dal
{
    public interface IFinancasDAL
    {
        IEnumerable<RelatorioDespesa> GetAllDespesas();
        IEnumerable<RelatorioDespesa> GetAllFiltraDespesa(string criterio);
        void AddDespesa(RelatorioDespesa despesa);
        int UpdateDespesa(RelatorioDespesa despesa);
        RelatorioDespesa GetDespesa(int id);
        void DeleteDespesa(int id);
        Dictionary<string, decimal> CalculaDespesaPeriodo(int periodo);
        Dictionary<string, decimal> CalculaDespesaPeriodoSemanal(int periodo);
    }
}
