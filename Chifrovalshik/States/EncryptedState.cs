using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Chifrovalshik.States
{
    public class EncryptedState : State, IEncryptor
    {
        public EncryptedState(string filePath) : base(filePath)
        {
            Expansion = ".crypt";
        }

        public override void ChangeState(ref State state, string password)
        {
            state = new DecrytpedState(FilePath);

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
            string decryptedText = string.Empty;

            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Encoding.UTF8.GetBytes(password);
                rijAlg.IV = Encoding.UTF8.GetBytes(password);

                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(byteText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            decryptedText = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return Encoding.UTF8.GetBytes(decryptedText);
        }
    }
}