using Google.Cloud.Firestore;
using INFT6009.Models;
using Microsoft.Maui.ApplicationModel.Communication;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace INFT6009.Services
{
    public class FirebaseStorageManager
    {
        private static readonly string _url = $"https://firestore.googleapis.com/v1/projects/inft6009-2777f/databases/(default)/documents/";
        static HttpClient client = new HttpClient();

        static FirebaseStorageManager()
        {

        }

        public static async Task<bool> AddUser(UserModel model)
        {
            try
            {
                string jsonBody = $@"
                {{
                    ""fields"": {{
                        ""userid"": {{
                            ""stringValue"": ""{model.UserId}""
                        }},
                        ""email"": {{
                            ""stringValue"": ""{model.Email}""
                        }},
                        ""firstname"": {{
                            ""stringValue"": ""{model.FirstName}""
                        }},
                        ""lastname"": {{
                            ""stringValue"": ""{model.LastName}""
                        }},
                        ""accounttype"": {{
                            ""stringValue"": ""{model.AccountType}""
                        }}
                    }}
                }}";

                var response = await SendJson(jsonBody, "users");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async Task<UserModel> GetUser(string id)
        {
            try
            {
                //Construct a query to be executed on our Firestore database, retrieving users where the userid == our id
                string jsonBody = $@"{{
                    ""structuredQuery"": {{
                        ""from"": [{{
                            ""collectionId"": ""users"",
                            ""allDescendants"": false
                        }}],

                        ""where"": {{
                            ""fieldFilter"": {{
                                ""field"": {{
                                    ""fieldPath"": ""userid""
                                }},
                                ""op"": ""EQUAL"",
                                ""value"": {{
                                    ""stringValue"": ""{id}"",
                                }}
                            }}
                        }}
                    }}
                }}";

                var response = await SendJson(jsonBody, ":runQuery");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var responseObject = JArray.Parse(json).ToObject<List<JObject>>();
                    if (responseObject.Count == 0)
                        return null;
                    var raw = responseObject[0];
                    var document = raw["document"];
                    var fields = document["fields"];
                    UserModel model = new UserModel()
                    {
                        DocumentUrl = document["name"]?.ToString(),
                        UserId = fields["userid"]?["stringValue"]?.ToString(),
                        AccountType = fields["accounttype"]?["stringValue"]?.ToString(),
                        Email = fields["email"]?["stringValue"]?.ToString(),
                        FirstName = fields["firstname"]?["stringValue"]?.ToString(),
                        LastName = fields["lastname"]?["stringValue"]?.ToString(),
                    };

                    return model;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<bool> AddQuest(QuestModel model)
        {
            try
            {
                string jsonBody = $@"
                {{
                    ""fields"": {{
                        ""userid"": {{
                            ""stringValue"": ""{model.UserId}""
                        }},
                        ""description"": {{
                            ""stringValue"": ""{model.Description}""
                        }},
                        ""address"": {{
                            ""stringValue"": ""{model.Address}""
                        }},
                        ""latitude"": {{
                            ""stringValue"": ""{model.Latitude}""
                        }},
                        ""longitude"": {{
                            ""stringValue"": ""{model.Longitude}""
                        }},
                        ""when"": {{
                            ""stringValue"": ""{model.When}""
                        }}
                    }}
                }}";

                var response = await SendJson(jsonBody, "quests");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async Task<List<QuestModel>> GetQuests()
        {
            try
            {
                var url = _url + "quests";
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    JObject responseObject = JObject.Parse(json);
                    List<QuestModel> quests = new List<QuestModel>();
                    if (!responseObject.HasValues)
                        return quests;
                    foreach (var document in responseObject["documents"])
                    {
                        var fields = document["fields"];

                        quests.Add(new QuestModel()
                        {
                            UserId = fields["userid"]?["stringValue"]?.ToString(),
                            Description = fields["description"]?["stringValue"]?.ToString(),
                            Address = fields["address"]?["stringValue"]?.ToString(),
                            Latitude = fields["latitude"]?["stringValue"]?.ToString(),
                            Longitude = fields["longitude"]?["stringValue"]?.ToString(),
                            When = fields["when"]?["stringValue"]?.ToString(),
                        });
                    }

                    return quests;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static Task<HttpResponseMessage> SendJson(string json, string collection)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var url = _url + collection;

            return client.PostAsync(url, content);
        }
    }
}
