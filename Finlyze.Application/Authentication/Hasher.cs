using Isopoh.Cryptography.Argon2;

namespace Finlyze.Application.Authentication.Hasher;

public static class PasswordHasher
{
    public static string GenerateHash(this string password) => Argon2.Hash(password);

    public static bool VerifyHash(this string password_hash, string password) => Argon2.Verify(password_hash, password);
}