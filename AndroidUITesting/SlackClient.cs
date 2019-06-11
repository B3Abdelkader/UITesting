using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TDDevices
{
    //A simple C# class to post messages to a Slack channel
    //Note: This class uses the Newtonsoft Json.NET serializer available via NuGet
    public class SlackClient
    {
        private readonly Uri _uri;
        private readonly Encoding _encoding = new UTF8Encoding();

        //LAB Shapr initial https://hooks.slack.com/services/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
        public SlackClient(string urlWithAccessToken= "https://hooks.slack.com/services/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")//Shapr Android
        {
            _uri = new Uri(urlWithAccessToken);
        }

        //Post a message using simple strings
        public void PostMessage(string text, string username = null, string channel = null, List<Attachment> attch = null)
        {
            Payload payload = new Payload()
            {
                Channel = channel,
                Username = username,
                Text = text,
                Attachments = attch
            };
            PostMessage(payload);
        }

        //Post a message using a Payload object
        public void PostMessage(Payload payload)
        {
            string payloadJson = JsonConvert.SerializeObject(payload);
            using (WebClient client = new WebClient())
            {
                NameValueCollection data = new NameValueCollection();
                data["payload"] = payloadJson;
                var response = client.UploadValues(_uri, "POST", data);
                //The response text is usually "ok"
                string responseText = _encoding.GetString(response);
            }
        }
    }

    //This class serializes into the Json payload required by Slack Incoming WebHooks
    public class Payload
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; }
    }

    public class Action
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class Attachment
    {
        [JsonProperty("fallback")]
        public string Fallback { get; set; }
        [JsonProperty("actions")]
        public List<Action> Actions { get; set; }
    }
}