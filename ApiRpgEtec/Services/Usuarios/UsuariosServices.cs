using ApiRpgEtec.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiRpgEtec.Services.Usuarios
{
    public class UsuariosServices : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "joli.somee.com/RpgApi/Usuarios";

        public UsuariosServices()
        {
            _request = new Request();
        }

        public async Task<Usuario> PostRegistrarUsuarioAsync(Usuario u)
        {
            string urlComplementar = "/Registrar";
            u.Id = await _request.PostReturnIntAsync(apiUrlBase + urlComplementar, u, string.Empty);

            return u;
        }

        public async Task<Usuario> PostAutentificarUsuarioAsync(Usuario u)
        {
            string urlComplementar = "/Autentificar";
            u = await _request.PostAsync(apiUrlBase + urlComplementar, u, string.Empty);

            return u;
        }
    }
}
