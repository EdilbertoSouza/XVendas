using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

namespace XVendas.Droid
{
    class FirebaseAuthenticator
    {
        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            var user = await Firebase.Auth.FirebaseAuth.Instance.
                            SignInWithEmailAndPasswordAsync(email, password);
            var token = await user.User.GetTokenAsync(false);
            return token.Token;
        }

        public async Task<string> RegsiterWithEmailPassword(string email, string password)
        {
            var user = await Firebase.Auth.FirebaseAuth.Instance.
                                                CreateUserWithEmailAndPasswordAsync(email, password);
            var token = await user.User.GetTokenAsync(false);
            return token.Token;
        }
    }
}