using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AccessAdmin.Client.Model;
using Newtonsoft.Json;
using NLog;

namespace AccessAdmin.Client.BusinessLogic
{
    public class UserHelper
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        private string serviceBase = "http://localhost:58304/api/login/";
        private string requestServiceBase = "http://localhost:58304/api/requests/";
        private string userServiceBase = "http://localhost:58304/api/user";

        public async Task<int> AuthenticateUser(string email, string password)
        {
            var result = string.Empty;
            var exists = 0;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.serviceBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                logger.Info($"Starting the check for user with email {email}");
                var urlAddress = serviceBase + email + "/" + password;

                HttpResponseMessage response = await client.GetAsync(urlAddress).ConfigureAwait(continueOnCapturedContext:false);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }

                try
                {
                    exists = JsonConvert.DeserializeObject<int>(result);
                    logger.Info($"Check for email {email} is done.");
                }
                catch (Exception e)
                {
                    this.logger.Error($"Error in AuthenticateUser - {e}");
                }
            }

            return exists;
        }

        public async Task<bool> IsUserAdmin(string email)
        {
            var result = string.Empty;
            var isAdmin = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.serviceBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(serviceBase + email).ConfigureAwait(continueOnCapturedContext: false);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }

                try
                {
                    isAdmin = JsonConvert.DeserializeObject<bool>(result);
                    
                }
                catch (Exception e)
                {
                    this.logger.Error($"Error in IsUserAdmin - {e}");
                }
            }

            return isAdmin;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var result = string.Empty;
            var user = new User();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.serviceBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                var urlAddress = userServiceBase + "/"  + email;

                HttpResponseMessage response = await client.GetAsync(urlAddress).ConfigureAwait(continueOnCapturedContext: false);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }

                try
                {
                    user = JsonConvert.DeserializeObject<User>(result);
                }
                catch (Exception e)
                {
                    this.logger.Error($"Error in GetUserByEmail - {e}");
                }
            }

            return user;
        }

        public async Task<ObservableCollection<Requests>> GetRequests()
        {
            var result = string.Empty;
            var list = new ObservableCollection<Requests>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.requestServiceBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var urlAddress = requestServiceBase;

                try
                {
                    HttpResponseMessage response = await client.GetAsync(urlAddress).ConfigureAwait(continueOnCapturedContext: false);
                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }

                    list = JsonConvert.DeserializeObject<ObservableCollection<Requests>>(result);
                }
                catch (Exception e)
                {
                    this.logger.Error($"Error in GetRequests - {e}");
                }
            }

            return list;
        }

        public async void MakeRequest(Requests req)
        {
            var route = "makerequest";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.requestServiceBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(req);

                var urlAddress = requestServiceBase + route;
                HttpResponseMessage response = await client.PostAsync(urlAddress, new StringContent(json, Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
            }
        }

        public async void SolveRequest(Requests req)
        {
            var route = "solverequest";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.requestServiceBase);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(req);

                var urlAddress = requestServiceBase + route;
                HttpResponseMessage response = await client.PostAsync(urlAddress, new StringContent(json, Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
            }
        }
    }

    public static class AdminHelper
    {
        public static string UserMail { get; set; }
    }
}
