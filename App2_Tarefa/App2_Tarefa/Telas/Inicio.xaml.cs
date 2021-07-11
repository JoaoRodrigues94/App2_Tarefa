﻿using App2_Tarefa.Modelos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2_Tarefa.Telas
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class Inicio : ContentPage
  {
    public Inicio()
    {
      InitializeComponent();

      CultureInfo culture = new CultureInfo("pt-BR");
      string Data = DateTime.Now.ToString("dddd, dd {0} MMMM {0} yyyy", culture);
      
      DataHoje.Text = string.Format(Data, "de");

      //DataHoje.Text = DateTime.Now.DayOfWeek.ToString() + ", " + DateTime.Now.ToString("dd/MM");
      CarregarTarefas();
    }

    public void ActionGoCadastro(object sender, EventArgs args)
    {
      Navigation.PushAsync(new Cadastro());
    }
    private void CarregarTarefas()
    {
      SLTarefas.Children.Clear();

      List<Tarefa> Lista = new GerenciadorTarefa().Listagem();

      int i = 0;
      foreach (Tarefa tarefa in Lista)
      {
        LinhaStackLayout(tarefa, i);
        i++;
      }
    }
    public void LinhaStackLayout(Tarefa tarefa, int index)
    {
      Image Delete = new Image() { WidthRequest = 25, VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("remove.png") };
      if (Device.RuntimePlatform == Device.UWP)
      {
        Delete.Source = ImageSource.FromFile("Resources/remove.png");
      }
      TapGestureRecognizer DeleteTap = new TapGestureRecognizer();
      DeleteTap.Tapped += delegate
      {
        new GerenciadorTarefa().Deletar(index);
        CarregarTarefas();
      };

      Delete.GestureRecognizers.Add(DeleteTap);

      Image Prioridade = new Image() { WidthRequest = 30, VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("" + tarefa.Prioridade + ".png") };
      string texto = "";
      switch (tarefa.Prioridade)
      {
        case 1:
          texto = "Verde";
          break;
        case 2:
          texto = "Azul";
          break;
        case 3:
          texto = "Amarelo";
          break;
        case 4:
          texto = "Vermelho";
          break;
      }

      if (Device.RuntimePlatform == Device.UWP)
      {
        Prioridade.Source = ImageSource.FromFile("Resources/" + texto + ".png");
      }

      object StackCentral = null;
      if (tarefa.DataFinalizacao == null)
      {
        StackCentral = new Label() { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand, Text = tarefa.Nome };
      }
      else
      {
        StackCentral = new StackLayout() { VerticalOptions = LayoutOptions.Center, Spacing = 0, HorizontalOptions = LayoutOptions.FillAndExpand };
        ((StackLayout)StackCentral).Children.Add(new Label() { Text = tarefa.Nome, TextColor = Color.Gray });
        ((StackLayout)StackCentral).Children.Add(new Label() { Text = "Finalizado em " + tarefa.DataFinalizacao.Value.ToString("dd/MM/yyyy - hh:mm") + "h", TextColor = Color.Gray, FontSize = 10 });
      }

      Image Check = new Image() { WidthRequest = 25, VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("CheckOn.png") };
      if (Device.RuntimePlatform == Device.UWP)
      {
        Check.Source = ImageSource.FromFile("Resources/CheckOn.png");
      }
      if(tarefa.DataFinalizacao != null)
      {
        Check.Source = ImageSource.FromFile("checkOff.png");

        if (Device.RuntimePlatform == Device.UWP)
        {
          Check.Source = ImageSource.FromFile("Resources/checkOff.png");
        }
      }

      TapGestureRecognizer CheckTap = new TapGestureRecognizer();
      CheckTap.Tapped += delegate
      {
        new GerenciadorTarefa().Finalizar(index, tarefa);
        CarregarTarefas();
      };

      Check.GestureRecognizers.Add(CheckTap);

      StackLayout Linha = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 15 };

      Linha.Children.Add(Check);
      Linha.Children.Add((View)StackCentral);
      Linha.Children.Add(Prioridade);
      Linha.Children.Add(Delete);

      SLTarefas.Children.Add(Linha);
    }
  }
}