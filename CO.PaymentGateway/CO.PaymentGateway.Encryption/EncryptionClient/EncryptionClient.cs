using Microsoft.AspNetCore.DataProtection;

namespace CO.PaymentGateway.Encryption.EncryptionClient
{
    public class EncryptionClient
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;

        private const string Key = "encryption_key_for_CO_Paym3n7_G4t3W410!";

        public EncryptionClient(IDataProtectionProvider dataprotectionProvider)
        {
            _dataProtectionProvider = dataprotectionProvider;
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
