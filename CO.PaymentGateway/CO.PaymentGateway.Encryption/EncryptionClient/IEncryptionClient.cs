namespace CO.PaymentGateway.Encryption.EncryptionClient
{
    public interface IEncryptionClient
    {
        string Encrypt(string input);

        string Decrypt(string cipherText);
    }
}