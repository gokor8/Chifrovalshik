namespace Chifrovalshik
{
    public interface IEncryptor
    { 
        byte[] EncryptDecrypt(byte[] byteText, string password);
    }
}