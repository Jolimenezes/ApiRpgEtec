using ApiRpgEtec.ViewModels.Usuarios;

namespace ApiRpgEtec.Views.Usuarios;

public partial class LoginView : ContentPage
{
	UsuarioViewModel UsuarioViewModel;
	public LoginView()
	{
		InitializeComponent();

		UsuarioViewModel = new UsuarioViewModel();
		BindingContext = UsuarioViewModel;
	}
}