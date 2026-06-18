using ApiRpgEtec.ViewModels.Disputas;

namespace ApiRpgEtec.Views.Disputas;

public partial class ListagemView : ContentPage
{
	DisputaViewModel viewModel;
	public ListagemView()
	{
		InitializeComponent();

		viewModel = new DisputaViewModel();
        BindingContext = viewModel;
    }
}