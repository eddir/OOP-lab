using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OOP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace OOP_lab
{
    class Task5: Task
    {

        public Task5()
        {
            number = 5;
            description = "Генерация исключений (общественный транспорт Казани).";
        }

        public override void Start()
        {
            string bus;
            try
            {
                if (!Program.TEST_MODE)
                {
                    Console.WriteLine("Введите номер маршрута:");
                    bus = Console.ReadLine().ToUpper();
                }
                else
                {
                    bus = "10A";
                }

                List<Dictionary<string, string>> info = SearchBus(bus);

                if (info != null)
                {
                    Console.WriteLine("Найдено {0} автобусов на линии. Средняя скорость - {1} км/ч .\n", info.Count, getAvarageSpeed(info));

                    foreach (Dictionary<string, string> unit in info)
                    {
                        Console.WriteLine("{0}, движется на {1}, {2} км/ч", unit["Address"], unit["Direction"], unit["Speed"]);
                    }
                }
            } catch (ArgumentException e)
            {
                Console.WriteLine("Не удалось выполнить запрос: {0}", e.Message);
            }
        }
        public static List<Dictionary<string, string>> SearchBus(string bus)
        {
            try
            {
                Console.WriteLine("\nЗапрашиваю данные...\n");

                String url = Program.TEST_MODE ? "http://data.kzn.ru:8082/api/v0/dynamic_datasets/bus.json" : "http://rostkov.pro/kzn-api.txt";

                JArray units = JArray.Parse(HttpGetRequest(url));

                // Можно было проще?
                /* Список автобусов с их характеристиками */
                List<Dictionary<string, string>> info = new List<Dictionary<string, string>>();


                /* Поиск автобусов на запрощенном маршруте среди всех автобусов в городе на линии */
                foreach (JObject unit in units.Children<JObject>())
                {
                    /* Анализировать только те автобусы, которые имеют заданный пользователем номер маршрута */
                    if (unit.SelectToken("data.Marsh").ToString().Equals(bus, StringComparison.OrdinalIgnoreCase))
                    {
                        string speed = unit.SelectToken("data.Speed").ToString();
                        string latitude = unit.SelectToken("data.Latitude").ToString();
                        string longtitude = unit.SelectToken("data.Longitude").ToString();
                        short azimuth = short.Parse(unit.SelectToken("data.Azimuth").ToString());
                        string direction, address = "";

                        /* Определение направления движения автобуса */
                        if (azimuth > 315 || azimuth < 45)
                        {
                            direction = "сервер";
                        }
                        else if (azimuth >= 45 && azimuth < 135)
                        {
                            direction = "восток";
                        }
                        else if (azimuth >= 135 && azimuth < 255)
                        {
                            direction = "юг";
                        }
                        else
                        {
                            direction = "запад";
                        }
                        try
                        {
                            /* Поискать адрес дома ближайщего от автобуса */
                            string response = HttpPostRequest(
                                "https://suggestions.dadata.ru/suggestions/api/4_1/rs/geolocate/address",
                                JsonConvert.SerializeObject(new Dictionary<string, string>()
                                {
                                    {"lat", latitude},
                                    {"lon", longtitude},
                                    {"count", "1"}
                                }),
                                "Token 5af76d5c7136c12247cfcb512af43ad811062494");
                            address = JObject.Parse(response).SelectToken("suggestions[0].value").ToString();
                        }
                        catch (WebException e)
                        {
                            Console.WriteLine("Не удалось получить данные от сервиса DaData. {0}", e.Message);
                            address = latitude + ", " + longtitude;
                        }
                        catch
                        {
                            /* Не всегда удаётся найти дом поблизости от автобуса. Можно вернуть координаты. */
                            address = latitude + ", " + longtitude;
                        }

                        info.Add(new Dictionary<string, string>()
                        {
                            { "Address", address},
                            { "Speed", speed},
                            { "Direction", direction},
                        });
                    }
                }

                if (info.Count > 0)
                {
                    return info;
                }
                else
                {
                    /* То, ради чего мы здесь собрались */
                    throw new ArgumentException(string.Format("На линии нет автобусов данного маршрута ({0})", bus));
                }

            }
            catch (WebException e)
            {
                Console.WriteLine("Не удалось получить данные от сервера ЦОДД. {0}", e.Message);
            }
            return null;
        }

        private static int getAvarageSpeed(List<Dictionary<string, string>> info)
        {
            /* Переменные для расчёта средней скорости автобуса */
            int totalSpeed = 0;
            int amountSpeed = 0;
            int currentSpeed;

            foreach (Dictionary<string, string> unit in info)
            {
                /* Расчёт средней скорости */
                currentSpeed = int.Parse(unit["Speed"].ToString());
                if (currentSpeed > 0)
                {
                    totalSpeed += currentSpeed;
                    amountSpeed++;
                }
            }
            return totalSpeed / amountSpeed;
        }

        private static string HttpGetRequest(string url)
        {
            HttpWebResponse response = null;
            string responseString = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "GET";

                response = (HttpWebResponse)request.GetResponse();

                StreamReader sr = new StreamReader(response.GetResponseStream());
                responseString = sr.ReadToEnd();
            }
            // Допустимо ли опускать catch, если нас не интересует вид исключения в данной функции 
            //(нас устраивает выброс исключения в вызывающию функцию) ?
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return responseString;
        }


        private static string HttpPostRequest(string url, string postData, string authorization)
        {
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            myHttpWebRequest.Method = "POST";
            myHttpWebRequest.Headers["Accept"] = "application/json";
            myHttpWebRequest.Headers["Authorization"] = authorization;

            byte[] data = Encoding.ASCII.GetBytes(postData);

            myHttpWebRequest.ContentType = "application/json";
            myHttpWebRequest.ContentLength = data.Length;

            Stream requestStream = myHttpWebRequest.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();

            string pageContent;

            using (HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse()) {

                Stream responseStream = myHttpWebResponse.GetResponseStream();

                StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);

                pageContent = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                responseStream.Close();
            }


            //myHttpWebResponse.Close();

            return pageContent;
        }

        public static String ToStringReadable(HttpWebRequest request)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("[Accept] = {0}", request.Accept);
            sb.AppendFormat("{0}[Address] = {1}", Environment.NewLine, request.Address);
            sb.AppendFormat("{0}[AllowAutoRedirect] = {1}", Environment.NewLine, request.AllowAutoRedirect);
            //sb.AppendFormat("{0}[AllowReadStreamBuffering] = {1}", Environment.NewLine, request.AllowReadStreamBuffering);
            sb.AppendFormat("{0}[AllowWriteStreamBuffering] = {1}", Environment.NewLine, request.AllowWriteStreamBuffering);
            sb.AppendFormat("{0}[AuthenticationLevel] = {1}", Environment.NewLine, request.AuthenticationLevel);
            sb.AppendFormat("{0}[AutomaticDecompression] = {1}", Environment.NewLine, request.AutomaticDecompression);
            sb.AppendFormat("{0}[CachePolicy] = {1}", Environment.NewLine, request.CachePolicy);
            sb.AppendFormat("{0}[ClientCertificates] = {1}", Environment.NewLine, request.ClientCertificates);
            sb.AppendFormat("{0}[Connection] = {1}", Environment.NewLine, request.Connection);
            sb.AppendFormat("{0}[ConnectionGroupName] = {1}", Environment.NewLine, request.ConnectionGroupName);
            sb.AppendFormat("{0}[ContentLength] = {1}", Environment.NewLine, request.ContentLength);
            sb.AppendFormat("{0}[ContentType] = {1}", Environment.NewLine, request.ContentType);
            sb.AppendFormat("{0}[ContinueDelegate] = {1}", Environment.NewLine, request.ContinueDelegate);
            //sb.AppendFormat("{0}[ContinueTimeout] = {1}", Environment.NewLine, request.ContinueTimeout);
            sb.AppendFormat("{0}[CookieContainer] = {1}", Environment.NewLine, request.CookieContainer);
            //sb.AppendFormat("{0}[CreatorInstance] = {1}", Environment.NewLine, request.CreatorInstance);
            sb.AppendFormat("{0}[Credentials] = {1}", Environment.NewLine, request.Credentials);
            //sb.AppendFormat("{0}[Date] = {1}", Environment.NewLine, request.Date);
            //sb.AppendFormat("{0}[DefaultCachePolicy] = {1}", Environment.NewLine, request.DefaultCachePolicy);
            //sb.AppendFormat("{0}[DefaultMaximumErrorResponseLength] = {1}", Environment.NewLine, request.DefaultMaximumErrorResponseLength);
            //sb.AppendFormat("{0}[DefaultMaximumResponseHeadersLength] = {1}", Environment.NewLine, request.DefaultMaximumResponseHeadersLength);
            sb.AppendFormat("{0}[Expect] = {1}", Environment.NewLine, request.Expect);
            sb.AppendFormat("{0}[HaveResponse] = {1}", Environment.NewLine, request.HaveResponse);
            sb.AppendFormat("{0}[Headers] = {1}", Environment.NewLine, request.Headers);
            //sb.AppendFormat("{0}[Host] = {1}", Environment.NewLine, request.Host);
            sb.AppendFormat("{0}[IfModifiedSince] = {1}", Environment.NewLine, request.IfModifiedSince);
            sb.AppendFormat("{0}[ImpersonationLevel] = {1}", Environment.NewLine, request.ImpersonationLevel);
            sb.AppendFormat("{0}[KeepAlive] = {1}", Environment.NewLine, request.KeepAlive);
            sb.AppendFormat("{0}[MaximumAutomaticRedirections] = {1}", Environment.NewLine, request.MaximumAutomaticRedirections);
            sb.AppendFormat("{0}[MaximumResponseHeadersLength] = {1}", Environment.NewLine, request.MaximumResponseHeadersLength);
            sb.AppendFormat("{0}[MediaType] = {1}", Environment.NewLine, request.MediaType);
            sb.AppendFormat("{0}[Method] = {1}", Environment.NewLine, request.Method);
            sb.AppendFormat("{0}[Pipelined] = {1}", Environment.NewLine, request.Pipelined);
            sb.AppendFormat("{0}[PreAuthenticate] = {1}", Environment.NewLine, request.PreAuthenticate);
            sb.AppendFormat("{0}[ProtocolVersion] = {1}", Environment.NewLine, request.ProtocolVersion);
            sb.AppendFormat("{0}[Proxy] = {1}", Environment.NewLine, request.Proxy);
            sb.AppendFormat("{0}[ReadWriteTimeout] = {1}", Environment.NewLine, request.ReadWriteTimeout);
            sb.AppendFormat("{0}[Referer] = {1}", Environment.NewLine, request.Referer);
            sb.AppendFormat("{0}[RequestUri] = {1}", Environment.NewLine, request.RequestUri);
            sb.AppendFormat("{0}[SendChunked] = {1}", Environment.NewLine, request.SendChunked);
            //sb.AppendFormat("{0}[ServerCertificateValidationCallback] = {1}", Environment.NewLine, request.ServerCertificateValidationCallback);
            sb.AppendFormat("{0}[ServicePoint] = {1}", Environment.NewLine, request.ServicePoint);
            //sb.AppendFormat("{0}[SupportsCookieContainer] = {1}", Environment.NewLine, request.SupportsCookieContainer);
            sb.AppendFormat("{0}[Timeout] = {1}", Environment.NewLine, request.Timeout);
            sb.AppendFormat("{0}[TransferEncoding] = {1}", Environment.NewLine, request.TransferEncoding);
            sb.AppendFormat("{0}[UnsafeAuthenticatedConnectionSharing] = {1}", Environment.NewLine, request.UnsafeAuthenticatedConnectionSharing);
            sb.AppendFormat("{0}[UseDefaultCredentials] = {1}", Environment.NewLine, request.UseDefaultCredentials);
            sb.AppendFormat("{0}[UserAgent] = {1}", Environment.NewLine, request.UserAgent);
            return sb.ToString();
        }
    }
}
