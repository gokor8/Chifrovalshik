using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Chifrovalshik.States
{
    public class DecrytpedState : State, IEncryptor
    {
        public DecrytpedState(string filePath) : base(filePath)
        {
            Expansion = ".txt";
        }

        public override void ChangeState(ref State state, string password)
        {
            state = new EncryptedState(FilePath);

            string oldFilePath = FilePath + Expansion;
            string newFilePath = FilePath + state.Expansion;

            var fileBytes = File.ReadAllBytes(oldFilePath);

            fileBytes = EncryptDecrypt(fileBytes, password);

            File.Delete(oldFilePath);
            File.Create(newFilePath).Close();
            File.WriteAllBytes(newFilePath, fileBytes);
        }

        public byte[] EncryptDecrypt(byte[] byteText, string password)
        {
            string text = Encoding.UTF8.GetString(byteText);

            byte[] encrypted;

            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                
                rijAlg.Key = Encoding.UTF8.GetBytes(password);
                rijAlg.IV = Encoding.UTF8.GetBytes(password);

                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }
    }
}