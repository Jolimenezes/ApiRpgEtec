using ApiRpgEtec.ViewModels.Usuarios;

namespace ApiRpgEtec.Views.Usuarios;

public partial class LocalizacaoView : ContentPage
{
	LocalizacaoViewModel viewModel;
	public LocalizacaoView()
	{
		InitializeComponent();

		viewModel = new LocalizacaoViewModel();
		//viewModel.InicializarMapa();
		viewModel.ExibirUsuariosMapa();

		BindingContext = viewModel;
	}
}