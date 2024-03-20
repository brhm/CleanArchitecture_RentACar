using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Hashing;

public static class HashingHelper
{
    public static void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
    {
        using HMACSHA512 hmac=new HMACSHA512();

        passwordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        passwordSalt = hmac.Key;
    }
    public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using HMACSHA512 hmac=new HMACSHA512(passwordSalt);

        byte[] computedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes((string)password));

        return computedHash.SequenceEqual(passwordHash);
    }
}
