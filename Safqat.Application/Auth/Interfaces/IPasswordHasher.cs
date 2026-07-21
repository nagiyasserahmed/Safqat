using System;
using System.Collections.Generic;
using System.Text;

namespace Safqat.Application.Auth.Interfaces
{
    public interface IPasswordHasher
    {
        string Hash(string password);

        bool Verify(string hash, string password);
    }
}
