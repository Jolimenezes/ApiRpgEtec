using ApiRpgEtec.Views.Personagens;

namespace ApiRpgEtec
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("cadPersonagemView", typeof(CadastroPersonagemView));
        }
    }
}
