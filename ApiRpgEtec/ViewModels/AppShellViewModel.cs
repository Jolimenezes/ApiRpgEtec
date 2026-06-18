using ApiRpgEtec.Services.Usuarios;
using ApiRpgEtec.ViewModels.Usuarios;
using Azure.Storage.Blobs;
using System.ComponentModel;

namespace ApiRpgEtec.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        private UsuariosServices uService;
        private static string conexaoAzureStorage = "DefaultEndpointsProtocol=https;AccountName=angelostorage;AccountKey=+Tsp6lH1hKHaKEwlvKQsBP07JSceJCJUJZQJQboH+YYi+JOMEZnxxamX76WlQMRIQ2dlvc/oC/99+AStWetYVQ==;EndpointSuffix=core.windows.net";
        private static string container = "arquivos";
        public AppShellViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            uService = new UsuariosServices(token);
            CarregarUsuarioAzure();
        }

        private byte[] foto;
        public byte[] Foto
        {
            get => foto;
            set
            {
                foto = value;
                OnPropertyChanged();
            }
        }

        public async void CarregarUsuarioAzure()
        {
            try
            {
                int usuarioId = Preferences.Get("UsuarioId", 0);
                string filename = $"{usuarioId}.jpg";
                var blobClient = new BlobClient(conexaoAzureStorage, container, filename);

                if (blobClient.Exists())
                {
                    Byte[] fileBytes;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        blobClient.OpenRead().CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    Foto = fileBytes;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", "Detalhes: " + ex.Message, "OK");
            }
        }
    }
}
