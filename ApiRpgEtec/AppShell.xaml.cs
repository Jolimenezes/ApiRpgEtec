using ApiRpgEtec.ViewModels;
using ApiRpgEtec.Views.Personagens;

namespace ApiRpgEtec
{
    public partial class AppShell : Shell
    {
        AppShellViewModel viewModel;
        public AppShell()
        {
            InitializeComponent();

            viewModel = new AppShellViewModel();
            BindingContext = viewModel;

            string login = Preferences.Get("UsuarioUserName", string.Empty);
            lblLogin.Text = login;

            Routing.RegisterRoute("cadPersonagemView", typeof(CadastroPersonagemView));
        }
    }
}
