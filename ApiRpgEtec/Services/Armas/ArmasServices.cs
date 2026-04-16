using ApiRpgEtec.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ApiRpgEtec.Services.Armas
{
    public class ArmasServices : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "http://joli.somee.com/RpgApi/Armas";

        private string _token;
        public ArmasServices(string token)
        {
            _request = new Request();
            _token = token;
        }

        public async Task<ObservableCollection<Arma>> GetArmasAsync()
        {
            string urlComplementar = string.Format("{0}", "/GetAll");
            ObservableCollection<Models.Arma> listaArma = await
            _request.GetAsync<ObservableCollection<Models.Arma>>(apiUrlBase + urlComplementar,
            _token);
            return listaArma;
        }
    }
}
