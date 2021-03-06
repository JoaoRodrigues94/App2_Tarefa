using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App2_Tarefa.Modelos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2_Tarefa.Telas
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class Cadastro : ContentPage
  {
    private byte Prioridade { get; set; }
    public Cadastro()
    {
      InitializeComponent();
    }
    public void PrioridadeSelectAction(object sender, EventArgs args)
    {
      var Stacks = SLPrioridades.Children;

      foreach (var Linha in Stacks)
      {
        Label LblPrioridade = ((StackLayout)Linha).Children[1] as Label;
        LblPrioridade.TextColor = Color.Gray;
      }

      ((Label)((StackLayout)sender).Children[1]).TextColor = Color.White;
      FileImageSource Source = ((Image)((StackLayout)sender).Children[0]).Source as FileImageSource;
      
      String Prioridade = Source.File.ToString().Replace("Resources/", "").Replace(".png", "").Replace("Vermelho", "4").Replace("Amarelo", "3").Replace("Azul", "2").Replace("Verde", "1");

      this.Prioridade = byte.Parse(Prioridade);
    }
    public void SalvarAction(object sender, EventArgs args)
    {
      bool ErroExiste = false;

      if(TxtNome.Text == null || TxtNome.Text.Trim().Length <= 0)
      {
        ErroExiste = true;
        DisplayAlert("Erro", "Nome não preenchido", "OK");
      }
      
      if(Prioridade <= 0)
      {
        ErroExiste = true;
        DisplayAlert("Erro", "Prioridade não informada", "OK");
      }

      if(ErroExiste == false)
      {
        //Salva esses dados
        Tarefa tarefa = new Tarefa();
        tarefa.Nome = TxtNome.Text.Trim();
        tarefa.Prioridade = this.Prioridade;

        new GerenciadorTarefa().Salvar(tarefa);

        App.Current.MainPage = new NavigationPage(new Inicio());
      }
    }
  }
}