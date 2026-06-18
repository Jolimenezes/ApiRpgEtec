using ApiRpgEtec.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiRpgEtec.Services.Disputas
{
    public class DisputaService : Request
    {
        private readonly Request _request;
        private string _token;

        private const string _apiUrlBase = "http://joli.somee.com/RpgApi/Disputas";

        public DisputaService(string token)
        {
            _request = new Request();
            _token = token;
        }

        public async Task<Disputa> PostDisputaComArmaAsync(Disputa d)
        {
            string urlComplementar = "/Arma";
            return await _request.PostAsync<Disputa>(_apiUrlBase + urlComplementar, d, _token);
        }
        public async Task<Disputa> PostDisputaComHabilidadesAsync(Disputa d) 
        {
            string urlComplementar = "/Habilidade";
            return await _request.PostAsync<Disputa>(_apiUrlBase + urlComplementar, d, _token);
        }
        public async Task<Disputa> PostDisputaGeralAsync(Disputa d)
        {
            string urlComplementar = "/DisputaEmGrupo";
            return await _request.PostAsync<Disputa>(_apiUrlBase + urlComplementar, d, _token);
        }
    }
}
