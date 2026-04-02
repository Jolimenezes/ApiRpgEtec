using ApiRpgEtec.ViewModels.Usuarios;

namespace ApiRpgEtec.Views.Usuarios;

public partial class CadastroView : ContentPage
{
	UsuarioViewModel viewModel;
	public CadastroView()
	{
		InitializeComponent();

		viewModel = new UsuarioViewModel();
		BindingContext = viewModel;
	}
}