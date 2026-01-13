using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinueWithGoogle.Helper
{
    public interface IGoogleAuthService
    {
        Task<string> SignInWithGoogleAsync();
    }
}
