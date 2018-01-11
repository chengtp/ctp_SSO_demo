using SSOProjectOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SSOProjectOne.Common
{

    public static class HttpClientHeaper<T>
    {
        private static HttpClient client;
        private static T model;

        static HttpClientHeaper()
        {
            model = default(T);
            client = new HttpClient();
            //  var uri = ConfigurationManager.AppSettings["content-api-host"];
            var uri = StaticParameter.SsoApiUri;
            client.BaseAddress = new Uri(uri);
        }

        public static T Get(string requestUri)
        {
            model = default(T);
            client.GetAsync(requestUri).ContinueWith(
                (getTask) =>
                {
                    if (getTask.IsCanceled)
                    {
                        return;
                    }
                    if (getTask.IsFaulted)
                    {
                        throw getTask.Exception;
                    }
                    HttpResponseMessage response = getTask.Result;

                    // Check that response was successful or throw exception
                    response.EnsureSuccessStatusCode();

                    // Read response asynchronously as JToken and write out top facts for each country
                    response.Content.ReadAsAsync<T>().ContinueWith(
                            (contentTask) =>
                            {
                                if (contentTask.IsCanceled)
                                {
                                    return;
                                }
                                if (contentTask.IsFaulted)
                                {
                                    throw contentTask.Exception;
                                }
                                model = contentTask.Result;
                            }).Wait();
                }).Wait();
            return model;
        }


        public static T Post(string requestUri, T modelView)
        {
            model = default(T);
            client.PostAsJsonAsync<T>(requestUri, modelView).ContinueWith(
                (getTask) =>
                {
                    if (getTask.IsCanceled)
                    {
                        return;
                    }
                    if (getTask.IsFaulted)
                    {
                        throw getTask.Exception;
                    }
                    HttpResponseMessage response = getTask.Result;

                    // Check that response was successful or throw exception
                    response.EnsureSuccessStatusCode();

                    // Read response asynchronously as JToken and write out top facts for each country
                    response.Content.ReadAsAsync<T>().ContinueWith(
                            (contentTask) =>
                            {
                                if (contentTask.IsCanceled)
                                {
                                    return;
                                }
                                if (contentTask.IsFaulted)
                                {
                                    throw contentTask.Exception;
                                }
                                model = contentTask.Result;
                            }).Wait();
                }).Wait();
            return model;
        }

        public static T PostQuery(string requestUri, IQueryable query)
        {
            model = default(T);
            client.PostAsJsonAsync<IQueryable>(requestUri, query).ContinueWith(
                (getTask) =>
                {
                    if (getTask.IsCanceled)
                    {
                        return;
                    }
                    if (getTask.IsFaulted)
                    {
                        throw getTask.Exception;
                    }
                    HttpResponseMessage response = getTask.Result;

                    // Check that response was successful or throw exception
                    response.EnsureSuccessStatusCode();

                    // Read response asynchronously as JToken and write out top facts for each country
                    response.Content.ReadAsAsync<T>().ContinueWith(
                            (contentTask) =>
                            {
                                if (contentTask.IsCanceled)
                                {
                                    return;
                                }
                                if (contentTask.IsFaulted)
                                {
                                    throw contentTask.Exception;
                                }
                                model = contentTask.Result;
                            }).Wait();
                }).Wait();
            return model;
        }

        public static T Put(string requestUri, T modelView)
        {
            model = default(T);
            client.PutAsJsonAsync<T>(requestUri, modelView).ContinueWith(
               (getTask) =>
               {
                   if (getTask.IsCanceled)
                   {
                       return;
                   }
                   if (getTask.IsFaulted)
                   {
                       throw getTask.Exception;
                   }
                   HttpResponseMessage response = getTask.Result;

                   // Check that response was successful or throw exception
                   response.EnsureSuccessStatusCode();

                   // Read response asynchronously as JToken and write out top facts for each country
                   response.Content.ReadAsAsync<T>().ContinueWith(
                           (contentTask) =>
                           {
                               if (contentTask.IsCanceled)
                               {
                                   return;
                               }
                               if (contentTask.IsFaulted)
                               {
                                   throw contentTask.Exception;
                               }
                               model = contentTask.Result;
                           }).Wait();
               }).Wait();
            return model;
        }


        public static T Delete(string requestUri)
        {
            model = default(T);
            client.DeleteAsync(requestUri).ContinueWith(
               (getTask) =>
               {
                   if (getTask.IsCanceled)
                   {
                       return;
                   }
                   if (getTask.IsFaulted)
                   {
                       throw getTask.Exception;
                   }
                   HttpResponseMessage response = getTask.Result;

                   // Check that response was successful or throw exception
                   response.EnsureSuccessStatusCode();

                   // Read response asynchronously as JToken and write out top facts for each country
                   response.Content.ReadAsAsync<T>().ContinueWith(
                           (contentTask) =>
                           {
                               if (contentTask.IsCanceled)
                               {
                                   return;
                               }
                               if (contentTask.IsFaulted)
                               {
                                   throw contentTask.Exception;
                               }
                               model = contentTask.Result;
                           }).Wait();
               }).Wait();
            return model;
        }
    }

}
