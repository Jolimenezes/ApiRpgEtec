using ApiRpgEtec.ViewModels.Armas;

namespace ApiRpgEtec.Views.Armas;

public partial class ListagemView : ContentPage
{
    ListagemArmaViewModel viewModel;
	public ListagemView()
	{
        InitializeComponent();

        viewModel = new ListagemArmaViewModel();
        BindingContext = viewModel;
        Title = "Armas - App Rpg Etec";
    }
}