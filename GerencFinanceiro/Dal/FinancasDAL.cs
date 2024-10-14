using GerencFinanceiro.Dal;
using GerencFinanceiro.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class FinancasDAL : IFinancasDAL
{
    private readonly AppDbContext db;

    public FinancasDAL(AppDbContext context)
    {
        db = context;
    }

    // Obter todas as despesas
    public IEnumerable<Financas> GetAllFinancas()
    {
        try
        {
            return db.RelatorioFinancas.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter Financas: {ex.Message}");
            throw;
        }
    }

    // Filtra os registros com base na string de busca
    public IEnumerable<Financas> GetAllFiltraFinancas(string criterio)
    {
        try
        {
            return GetAllFinancas()
                .Where(x => x.ItemNome.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) != -1);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao filtrar financas: {ex.Message}");
            throw;
        }
    }


    // Adicionar uma nova despesa
    public void AddFinancas(Financas financa)
    {
        try
        {
            db.RelatorioFinancas.Add(financa);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao adicionar despesa: {ex.Message}");
            throw;
        }
    }

    // Atualizar uma despesa    
    public int UpdateFinancas(Financas financa)
    {
        try
        {
            db.Entry(financa).State = EntityState.Modified;
            db.SaveChanges();
            return 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao atualizar financa: {ex.Message}");
            throw;
        }
    }

    // Obter uma despesa pelo seu id
    public Financas GetFinancas(int id)
    {
        try
        {
            Financas financa = db.RelatorioFinancas.Find(id);
            return financa;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter financa: {ex.Message}");
            throw;
        }
    }

    // Deletar uma despesa
    public void DeleteFinancas(int id)
    {
        try
        {
            Financas desp = db.RelatorioFinancas.Find(id);
            if (desp == null)
            {
                Console.WriteLine($"Financa com ID {id} não encontrada.");
                return;
            }
            db.RelatorioFinancas.Remove(desp);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao deletar financa: {ex.Message}");
            throw;
        }
    }


    // Calcula despesa semestral
    public Dictionary<string, decimal> CalculaFinancasPeriodo(int periodo)
    {
        Dictionary<string, decimal> SomaFinancasPeriodo = new Dictionary<string, decimal>();

        decimal despAlimentacao = db.RelatorioFinancas
            .Where(cat => cat.Categoria == "Alimentacao" && cat.Date > DateTime.Now.AddMonths(-periodo) && cat.IsReceita == false)
            .Select(cat => cat.Valor)
            .Sum();

        decimal despCompras = db.RelatorioFinancas
            .Where(cat => cat.Categoria == "Compras" && cat.Date > DateTime.Now.AddMonths(-periodo) && cat.IsReceita == false)
            .Select(cat => cat.Valor)
            .Sum();

        decimal despTransporte = db.RelatorioFinancas
            .Where(cat => cat.Categoria == "Transporte" && cat.Date > DateTime.Now.AddMonths(-periodo) && cat.IsReceita == false)
            .Select(cat => cat.Valor)
            .Sum();

        decimal despSaude = db.RelatorioFinancas
            .Where(cat => cat.Categoria == "Saude" && cat.Date > DateTime.Now.AddMonths(-periodo) && cat.IsReceita == false)
            .Select(cat => cat.Valor)
            .Sum();

        decimal despMoradia = db.RelatorioFinancas
            .Where(cat => cat.Categoria == "Moradia" && cat.Date > DateTime.Now.AddMonths(-periodo) && cat.IsReceita == false)
            .Select(cat => cat.Valor)
            .Sum();

        decimal despLazer = db.RelatorioFinancas
            .Where(cat => cat.Categoria == "Lazer" && cat.Date > DateTime.Now.AddMonths(-periodo) && cat.IsReceita == false)
            .Select(cat => cat.Valor)
            .Sum();

        SomaFinancasPeriodo.Add("Alimentacao", despAlimentacao);
        SomaFinancasPeriodo.Add("Compras", despCompras);
        SomaFinancasPeriodo.Add("Transporte", despTransporte);
        SomaFinancasPeriodo.Add("Saude", despSaude);
        SomaFinancasPeriodo.Add("Moradia", despMoradia);
        SomaFinancasPeriodo.Add("Lazer", despLazer);

        return SomaFinancasPeriodo;
    }

    public Dictionary<string, decimal> CalculaFinancasPeriodoSemanal(int periodo)
    {
        Dictionary<string, decimal> SomaFinancasPeriodoSemanal = new Dictionary<string, decimal>();

        decimal despAlimentacao = db.RelatorioFinancas
            .Where(cat => cat.Categoria == "Alimentacao" && cat.Date > DateTime.Now.AddDays(-periodo) && cat.IsReceita == false)
            .Select(cat => cat.Valor)
            .Sum();

        decimal despCompras = db.RelatorioFinancas
            .Where(cat => cat.Categoria == "Compras" && cat.Date > DateTime.Now.AddDays(-periodo) && cat.IsReceita == false)
            .Select(cat => cat.Valor)
            .Sum();

        decimal despTransporte = db.RelatorioFinancas
            .Where(cat => cat.Categoria == "Transporte" && cat.Date > DateTime.Now.AddDays(-periodo) && cat.IsReceita == false)
            .Select(cat => cat.Valor)
            .Sum();

        decimal despSaude = db.RelatorioFinancas
            .Where(cat => cat.Categoria == "Saude" && cat.Date > DateTime.Now.AddDays(-periodo) && cat.IsReceita == false)
            .Select(cat => cat.Valor)
            .Sum();

        decimal despMoradia = db.RelatorioFinancas
            .Where(cat => cat.Categoria == "Moradia" && cat.Date > DateTime.Now.AddDays(-periodo) && cat.IsReceita == false)
            .Select(cat => cat.Valor)
            .Sum();

        decimal despLazer = db.RelatorioFinancas
            .Where(cat => cat.Categoria == "Lazer" && cat.Date > DateTime.Now.AddDays(-periodo) && cat.IsReceita == false)
            .Select(cat => cat.Valor)
            .Sum();

        SomaFinancasPeriodoSemanal.Add("Alimentacao", despAlimentacao);
        SomaFinancasPeriodoSemanal.Add("Compras", despCompras);
        SomaFinancasPeriodoSemanal.Add("Transporte", despTransporte);
        SomaFinancasPeriodoSemanal.Add("Saude", despSaude);
        SomaFinancasPeriodoSemanal.Add("Moradia", despMoradia);
        SomaFinancasPeriodoSemanal.Add("Lazer", despLazer);

        return SomaFinancasPeriodoSemanal;
    }

    public Dictionary<string, decimal> GetCategoriasDespesas()
    {
        Dictionary<string, decimal> categorias = new Dictionary<string, decimal>();

        categorias.Add("Alimentacao", 0);
        categorias.Add("Compras", 0);
        categorias.Add("Transporte", 0);
        categorias.Add("Saude", 0);
        categorias.Add("Moradia", 0);
        categorias.Add("Lazer", 0);

        return categorias;
    }

    public Dictionary<string, decimal> GetCategoriasReceitas()
    {
        Dictionary<string, decimal> categorias = new Dictionary<string, decimal>();

        categorias.Add("Salario", 0);
        categorias.Add("Freelance", 0);
        categorias.Add("Comissoes", 0);
        categorias.Add("Bonus", 0);
        categorias.Add("13° Salario", 0);
        categorias.Add("Horas extras", 0);

        return categorias;
    }

    public Dictionary<string, decimal> CalculaFinancasPeriodoReceitas(int period)
    {
        throw new NotImplementedException();
    }
}