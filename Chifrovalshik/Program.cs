using System;
using System.IO;

namespace Chifrovalshik
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var fileCryptor = new FileCryptor();

            while (true)
            {
                Console.WriteLine("Введите пароль");
                string password = Console.ReadLine();

                if (password is "1234567890123456") 
                    fileCryptor.ChangeFileType(password);
            }
        }
    }
}
