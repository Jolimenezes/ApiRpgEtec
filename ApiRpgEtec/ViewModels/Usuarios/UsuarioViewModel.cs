using ApiRpgEtec.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Newtonsoft.Json;
using ApiRpgEtec.Services.Usuarios;
using ApiRpgEtec.Views.Usuarios;
//using ApiRpgEtec.Views.Personagens;
using ApiRpgEtec.Views.Armas;


namespace ApiRpgEtec.ViewModels.Usuarios
{
    public class UsuarioViewModel : BaseViewModel
    {
        private UsuariosServices uService;
        public ICommand AutenticarCommand { get; set; }
        public ICommand RegistrarCommand { get; set; }
        public ICommand DirecionarCadastroCommand { get; set; }

        public UsuarioViewModel() 
        {
           uService = new UsuariosServices();
            InicializarCommands();
        }

        public void InicializarCommands()
        {
            AutenticarCommand = new Command(async () => await AutenticarUsuario());
            RegistrarCommand = new Command(async () => await RegistrarUsuario());
            DirecionarCadastroCommand = new Command(async () => await DirecionarParaCadastro());
        }

        private string login = string.Empty;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged();
            }
        }

        private string senha = string.Empty;
        public string Senha
        {
            get { return senha; }
            set
            {
                senha = value;
                OnPropertyChanged();
            }
        }

        public async Task AutenticarUsuario()
        {
            try
            {
                Usuario u = new Usuario();
                u.Username = Login;
                u.PasswordString = Senha;

                Usuario uAutenticado = await uService.PostAutentificarUsuarioAsync(u);

                if (!string.IsNullOrEmpty(uAutenticado.Token))
                {
                    string mensagem = $"Bem-vindo(a) {uAutenticado.Username}.";

                    Preferences.Set("UsuarioId", uAutenticado.Id);
                    Preferences.Set("UsuarioUsername", uAutenticado.Username);
                    Preferences.Set("UsuarioPerfil", uAutenticado.Perfil);
                    Preferences.Set("UsuarioToken", uAutenticado.Token);

                    await Application.Current.MainPage.DisplayAlert("Informação", mensagem, "Ok");

                    Application.Current.MainPage = new ListagemView();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Informação", "Dados incorretos :(", "Ok");
                }
            }
            catch (Exception ex)
            {
                {
                    await Application.Current.MainPage.DisplayAlert("Informação", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
                }
            }
        }

        public async Task RegistrarUsuario()
        {
            try
            {
                Usuario u = new Usuario();
                u.Username = Login;
                u.PasswordString = Senha;

                Usuario uRegistrado = await uService.PostRegistrarUsuarioAsync(u);

                if (uRegistrado.Id != 0)
                {
                    string mensagem = $"Usuário Id {uRegistrado.Id} registrado com sucesso";


                    await Application.Current.MainPage.DisplayAlert("Informação", mensagem, "Ok");

                    Application.Current.MainPage.Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                {
                    await Application.Current.MainPage.DisplayAlert("Informação", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
                }
            }

        }

        public async Task DirecionarParaCadastro()
        {
            try
            {
                Application.Current.MainPage.Navigation.PushAsync(new CadastroView());

            }
            catch (Exception ex)
            {
                {
                    await Application.Current.MainPage.DisplayAlert("Informação", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
                }
            }

        }

    }
}
