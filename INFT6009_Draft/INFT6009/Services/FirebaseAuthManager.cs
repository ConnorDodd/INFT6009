using Firebase.Auth;
using Firebase.Auth.Providers;
using INFT6009.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INFT6009.Services
{
    public static class FirebaseAuthManager
    {
        private static readonly FirebaseAuthClient _authClient;

        public static UserModel CurrentUser { get; set; }

        static FirebaseAuthManager()
        {
            _authClient = new FirebaseAuthClient(new FirebaseAuthConfig()
            {
                ApiKey = "AIzaSyAwj5eNLLd3Ryqt_JY4cQbeEF6I2lurh6s",
                AuthDomain = "inft6009-2777f.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
             {
                 new EmailProvider()
             }
            });
        }

        public async static Task<bool> RegisterAccount(UserModel user, string password)
        {
            try
            {
                var credentials = await _authClient.CreateUserWithEmailAndPasswordAsync(user.Email, password);
                var id = credentials.User.Uid;
                user.UserId = id;

                return await FirebaseStorageManager.AddUser(user);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async static Task<bool> Login(string email, string password)
        {
            try
            {
                var credentials = await _authClient.SignInWithEmailAndPasswordAsync(email, password);
                var id = credentials.User.Uid;

                var user = await FirebaseStorageManager.GetUser(id);
                if (user == null)
                    return false;
                
                CurrentUser = user;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
