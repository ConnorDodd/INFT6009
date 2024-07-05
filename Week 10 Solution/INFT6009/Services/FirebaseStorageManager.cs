using INFT6009.Models;
using Newtonsoft.Json.Linq;
using System.Text;

namespace INFT6009.Services
{
    internal class FirebaseStorageManager
    {
        private static readonly string _url = $"https://firestore.googleapis.com/v1/projects/inft6009-d3748/databases/(default)/documents/";
        static HttpClient client = new HttpClient();

        public static async Task<UserModel> GetUser(string userId)
        {
            try
            {
                //Get all user data from firebase
                var url = _url + "users";
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    //Read all user data into a JSON object
                    string json = await response.Content.ReadAsStringAsync();
                    JObject responseObject = JObject.Parse(json);
                    List<UserModel> users = new List<UserModel>();
                    if (!responseObject.HasValues)
                        return null;
                    //Loop through each user "document"
                    foreach (var document in responseObject["documents"])
                    {
                        var fields = document["fields"];
                        //If the user id field is the one we're looking for
                        var id = fields["userid"]?["stringValue"]?.ToString();
                        if (!String.IsNullOrEmpty(id))
                        {
                            //Read all fields and return
                            return new UserModel()
                            {
                                UserId = id,
                                Email = fields["email"]?["stringValue"]?.ToString(),
                                FirstName = fields["firstname"]?["stringValue"]?.ToString(),
                                LastName = fields["lastname"]?["stringValue"]?.ToString(),
                                AccountType = fields["accounttype"]?["stringValue"]?.ToString()
                            };
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<bool> AddUser(UserModel model)
        {
            try
            {
                //Manually format the request as a JSON string
                //This should be done by a library in practice
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

                //Send the JSON string as a POST request to the 'users' collection
                var response = await SendJson(jsonBody, "users");
                //Return true if the request was successful
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
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
