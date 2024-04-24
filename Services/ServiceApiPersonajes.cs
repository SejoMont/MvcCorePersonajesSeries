using MvcCorePersonajesSeries.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcCorePersonajesSeries.Services
{
    public class ServiceApiPersonajes
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceApiPersonajes(IConfiguration configuration)
        {
            this.header =
                new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlApi = configuration.GetValue<string>
                ("ApiUrls:ApiPersonajes");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "api/Personajes/GetPersonajes";
            List<Personaje> data = await
                this.CallApiAsync<List<Personaje>>(request);
            return data;
        }
        public async Task<List<string>> GetSeriesAsync()
        {
            string request = "api/Personajes/GetSeries";
            List<string> data = await
                this.CallApiAsync<List<string>>(request);
            return data;
        }

        public async Task<List<Personaje>> GetPersonajesSeriesAsync(string serie)
        {
            string request = "api/Personajes/GetPersonajesSerie/" + serie;
            List<Personaje> data = await
                this.CallApiAsync<List<Personaje>>(request);
            return data;
        }

        public async Task<Personaje> FindPersonajeAsync(int id)
        {
            string request = "api/Personajes/FindPersonaje/" + id;
            Personaje data = await this.CallApiAsync<Personaje>(request);
            return data;
        }

        public async Task DeletePersonajeAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Personajes/DeletePersonaje/" + id;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
            }
        }

        public async Task InsertPersonajeAsync
            (Personaje personaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Personajes/PostPersonaje";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);

                string json = JsonConvert.SerializeObject(personaje);

                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }

        public async Task UpdatePersonajeAsync(Personaje personaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Personajes/PutPersonaje";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);

                string json = JsonConvert.SerializeObject(personaje);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PutAsync(request, content);
            }
        }
    }
}
