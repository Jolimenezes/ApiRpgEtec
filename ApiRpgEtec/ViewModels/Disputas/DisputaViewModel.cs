using ApiRpgEtec.Models;
using ApiRpgEtec.Services.Disputas;
using ApiRpgEtec.Services.PersonagemHabilidades;
using ApiRpgEtec.Services.Personagens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace ApiRpgEtec.ViewModels.Disputas
{
    public class DisputaViewModel : BaseViewModel
    {
        private PersonagemService pService;
        public ObservableCollection<Personagem> PersonagensEncontrados { get; set; }
        public Personagem Atacante { get; set; }
        public Personagem Oponente { get; set; }

        private DisputaService dService;
        public Disputa DisputaPersonagens { get; set; }
        private PersonagemHabilidadeService phService;
        public ObservableCollection<PersonagemHabilidade> Habilidades { get; set; }

        public DisputaViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            pService = new PersonagemService(token);
            dService = new DisputaService(token);
            phService = new PersonagemHabilidadeService(token);

            Atacante = new Personagem();
            Oponente = new Personagem();
            DisputaPersonagens = new Disputa();

            PersonagensEncontrados = new ObservableCollection<Personagem>();

            PesquisarPersonagensCommand = new Command<string>(async (string pesquisa) => { await PesquisarPersonagens(pesquisa); });
            DisputaComArmaCommand = new Command(async () => { await ExecutarDisputaArmada(); });
            DisputaComHabilidadeCommand = new Command(async () => { await ExecutarDisputaHabilidades(); });
            DisputaGeralCommand = new Command(async () => { await ExecutarDisputaGeral(); });
        }
        public ICommand PesquisarPersonagensCommand {  get; set; }
        public ICommand DisputaComArmaCommand { get; set; }
        public ICommand DisputaComHabilidadeCommand { get; set; }
        public ICommand DisputaGeralCommand { get; set; }

        public async Task PesquisarPersonagens(string textoPesquisaPersonagem)
        {
            try
            {
                PersonagensEncontrados = await pService.GetByNomeAproximadoAsync(textoPesquisaPersonagem);
                OnPropertyChanged(nameof(PersonagensEncontrados));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "OK");
            }
        }

        public string DescricaoPersonagemAtacante
        {
            get => Atacante.Nome;
        }
        public string DescricaoPersonagemOponente
        {
            get => Oponente.Nome;
        }

        public ObservableCollection<PersonagemHabilidade> HabilidadesAtacante { get; set; } = new ObservableCollection<PersonagemHabilidade>();
        public ObservableCollection<PersonagemHabilidade> HabilidadesOponente { get; set; } = new ObservableCollection<PersonagemHabilidade>();

        // Propriedades para capturar o item selecionado em cada Picker
        private PersonagemHabilidade habilidadeAtacanteSelecionada;
        public PersonagemHabilidade HabilidadeAtacanteSelecionada
        {
            get => habilidadeAtacanteSelecionada;
            set
            {
                habilidadeAtacanteSelecionada = value;
                OnPropertyChanged();
            }
        }

        private PersonagemHabilidade habilidadeOponenteSelecionada;
        public PersonagemHabilidade HabilidadeOponenteSelecionada
        {
            get => habilidadeOponenteSelecionada;
            set
            {
                habilidadeOponenteSelecionada = value;
                OnPropertyChanged();
            }
        }

        public async void SelecionarPersonagem(Personagem p)
        {
            try
            {
                string tipoCombatente = await Application.Current.MainPage.DisplayActionSheet("Atacante ou Oponente?", "Cancelar", "", "Atacante", "Oponente");

                if (tipoCombatente == "Atacante")
                {
                    Atacante = p;
                    OnPropertyChanged(nameof(DescricaoPersonagemAtacante));

                    var habs = await phService.GetPersonagemHabilidadesAsync(Atacante.Id);
                    HabilidadesAtacante.Clear();
                    foreach (var h in habs) HabilidadesAtacante.Add(h);
                }
                else if (tipoCombatente == "Oponente")
                {
                    Oponente = p;
                    OnPropertyChanged(nameof(DescricaoPersonagemOponente));

                    var habs = await phService.GetPersonagemHabilidadesAsync(Oponente.Id);
                    HabilidadesOponente.Clear();
                    foreach (var h in habs) HabilidadesOponente.Add(h);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "OK");
            }
        }

        private Personagem personagemSelecionado;
        public Personagem PersonagemSelecionado
        {
            set
            {
                if (value != null)
                {
                    personagemSelecionado = value;
                    SelecionarPersonagem(personagemSelecionado);
                    OnPropertyChanged();
                    PersonagensEncontrados.Clear();
                }
            }
        }

        private string textoBuscaDigitado = string.Empty;
        public string TextoBuscaDigitado
        {
            get { return textoBuscaDigitado; }
            set
            {
                if ((value != null && !string.IsNullOrEmpty(value) && value.Length > 0))
                {
                    textoBuscaDigitado = value;
                    _ = PesquisarPersonagens(textoBuscaDigitado);
                }
                else 
                {
                    PersonagensEncontrados.Clear();
                }
            }
        }

        private async Task ExecutarDisputaArmada()
        {
            try
            {
                DisputaPersonagens.AtacanteId = Atacante.Id;
                DisputaPersonagens.OponenteId = Oponente.Id;

                DisputaPersonagens.Narracao = string.Empty;

                DisputaPersonagens = await dService.PostDisputaComArmaAsync(DisputaPersonagens);

                await Application.Current.MainPage.DisplayAlert("Resultado", DisputaPersonagens.Narracao, "Ok");
            }
            catch (Exception ex) 
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "OK");
            }
        }

        public async Task ObterHabilidadesAsync(int personagemId)
        {
            try
            {
                Habilidades = await phService.GetPersonagemHabilidadesAsync(personagemId);
                OnPropertyChanged(nameof(Habilidades));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "OK");
            }
        }

        private PersonagemHabilidade habilidadeSelecionada;
        public PersonagemHabilidade PersonagemHabilidade
        {
            get { return habilidadeSelecionada; }
            set 
            {
                if (value != null)
                {
                    try
                    {
                        habilidadeSelecionada = value;
                        OnPropertyChanged();
                    }
                    catch (Exception ex)
                    {
                          Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
                    }
                }
            }
        }

        private async Task ExecutarDisputaHabilidades()
        {
            try
            {
                DisputaPersonagens.AtacanteId = Atacante.Id;
                DisputaPersonagens.OponenteId = Oponente.Id;

                DisputaPersonagens.Narracao = string.Empty;

                if (HabilidadeAtacanteSelecionada != null)
                {
                    DisputaPersonagens.HabilidadeId = HabilidadeAtacanteSelecionada.HabilidadeId;
                }

                DisputaPersonagens = await dService.PostDisputaComHabilidadesAsync(DisputaPersonagens);

                await Application.Current.MainPage.DisplayAlert("Resultado", DisputaPersonagens.Narracao, "Ok");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "OK");
            }
        }

        private async Task ExecutarDisputaGeral()
        {
            try
            {
                ObservableCollection<Personagem> lista = await pService.GetPersonagensAsync();
                DisputaPersonagens.ListaIdPersonagens = lista.Select(p => p.Id).ToList();

                DisputaPersonagens = await dService.PostDisputaGeralAsync(DisputaPersonagens);

                string resultados = string.Join(" | ", DisputaPersonagens.Resultados);
                await Application.Current.MainPage.DisplayAlert("Resultado", resultados, "Ok");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "OK");
            }
        }
    }
}
