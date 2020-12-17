using System;
using Microsoft.AspNetCore.DataProtection;

namespace CO.PaymentGateway.Encryption.EncryptionClient
{
    public class EncryptionClientManager : IEncryptionClient
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;

        private readonly string Key;

        public EncryptionClientManager(IDataProtectionProvider dataprotectionProvider)
        {
            _dataProtectionProvider = dataprotectionProvider;
            Key = Environment.GetEnvironmentVariable("ENCRYPTIONKEY");
        }

        public string Encrypt(string input)
        {
            var protector = _dataProtectionProvider.CreateProtector(Key);
            return protector.Protect(input);
        }

        public string Decrypt(string cipherText)
        {
            var protector = _dataProtectionProvider.CreateProtector(Key);
            return protector.Unprotect(cipherText);
        }
    }
}
