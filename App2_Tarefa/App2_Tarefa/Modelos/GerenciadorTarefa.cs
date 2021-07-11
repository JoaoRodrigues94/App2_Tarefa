using System;
using System.Collections.Generic;
using System.Text;

namespace App2_Tarefa.Modelos
{
  public class GerenciadorTarefa
  {
    private List<Tarefa> Lista { get; set; }
    public void Salvar(Tarefa tarefa)
    {
      Lista = Listagem();
      Lista.Add(tarefa);

      SalvarNoProperties(Lista);
    }
    public void Deletar(int index)
    {
      Lista = Listagem();
      Lista.RemoveAt(index);

      SalvarNoProperties(Lista);
    }
    public void Finalizar(int index, Tarefa tarefa)
    {
      Lista = Listagem();
      Lista.RemoveAt(index);

      tarefa.DataFinalizacao = DateTime.Now;
      Lista.Add(tarefa);
      SalvarNoProperties(Lista);
    }
    public List<Tarefa> Listagem()
    {

      return ListagemProperties();
    }
    private void SalvarNoProperties(List<Tarefa> Lista)
    {
      if (App.Current.Properties.ContainsKey("Tarefas"))
      {
        App.Current.Properties.Remove("Tarefas");
      }
      App.Current.Properties.Add("Tarefas", Lista);
    }
    private List<Tarefa> ListagemProperties()
    {
      if (App.Current.Properties.ContainsKey("Tarefas"))
      {
        return (List<Tarefa>)App.Current.Properties["Tarefas"];
      }
      return new List<Tarefa>();
    }
  }
}
