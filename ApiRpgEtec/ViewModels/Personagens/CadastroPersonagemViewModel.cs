using System;
using System.Collections.Generic;
using System.Text;
using ApiRpgEtec.Services.Personagens;

namespace ApiRpgEtec.ViewModels.Personagens
{
    public class CadastroPersonagemViewModel : BaseViewModel
    {
        private PersonagemService pService;

        public CadastroPersonagemViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            pService = new PersonagemService(token);
        }
    }
}
