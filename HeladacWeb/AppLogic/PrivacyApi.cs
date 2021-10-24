using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.AppLogic
{
    public class PrivacyApi
    {
        const string baseUrl = "https://api.lithic.com";
        const string apiKey = "";
        RestClient client;
        public PrivacyApi()
        {
            client = new RestClient(baseUrl);
        }
        public void getNewCreateCardFromPrivacy()
        {
            var request = new RestRequest("v1/card", Method.POST);
            client.Timeout = -1;
            request.AddHeader("Authorization", "api-key "+ apiKey);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new {memo= "New Card", spend_limit=1,spend_limit_duration= "TRANSACTION", state="OPEN",type ="SINGLE_USE"});
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
}
