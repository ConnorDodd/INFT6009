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

        //Keep track of the currently signed-in user
        public static UserModel CurrentUser { get; set; }
        //Store last reason for failed auth to display in error message
        public static AuthErrorReason FailReason { get; set; } = AuthErrorReason.Undefined;


        //This is a static constructor, so it only runs once the first time
        //the FirebaseAuthManager class is used
        static FirebaseAuthManager()
        {
            //Initialize the authentication client using the details we copied from Firebase
            _authClient = new FirebaseAuthClient(new FirebaseAuthConfig()
            {
                ApiKey = "AIzaSyBKkOo0q5h6LEfnrGWY3N6l6WL7Nenlzow",
                AuthDomain = "inft6009-d3748.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
            });
        }

        //Register an account with Firebase
        public async static Task<bool> RegisterAccount(UserModel user, string password)
        {
            try
            {
                var credentials = await _authClient.
                    CreateUserWithEmailAndPasswordAsync(user.Email, password);
                var id = credentials.User.Uid;
                user.UserId = id;

                return true;
            }
            catch (FirebaseAuthException fae)
            {
                //Store failure reason
                FailReason = fae.Reason;
                return false;
            }
            catch (Exception ex)
            {
                FailReason = AuthErrorReason.Undefined;
                //Set a breakpoint here to view error messages VVV
                return false;
            }
        }

        //Try to log in with Firebase
        public async static Task<bool> Login(string email, string password)
        {
            try
            {
                var credentials = await _authClient.SignInWithEmailAndPasswordAsync(email, password);
                var id = credentials.User.Uid;

                //Set the current user if login was successful
                var user = await FirebaseStorageManager.GetUser(id);
                //Set the CurrentUser to the model got from the database
                //or an empty one if it's null
                CurrentUser = user ?? new UserModel();

                return true;
            }
            catch (Exception ex)
            {
                //Throws an exception if the login fails for any reason
                return false;
            }
        }
    }
}
