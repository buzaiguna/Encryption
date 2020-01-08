using System;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using GetKey;

namespace Projects
{
    class Program
    {
        static string secretKey;
        static void Main(string[] args)
        {
            secretKey = getKey.GetKey();
            Console.WriteLine(secretKey);
            while (true)
            {
                Console.WriteLine("0 for create, 1 for encrypt, 2 for decrypt, 3 for exit...");
                var sel = Console.Read();
                if (sel == '0')
                {
                    CreateTxt();
                }
                else if (sel == '1')
                {
                    Encrypt();
                }
                else if (sel == '2')
                {
                    Decrypt();
                }
                else if (sel == '3')
                {
                    break;
                }
            }
            
        }

        static void Decrypt()
        {
            Console.WriteLine("begin to decrypt...");
            var aes = new AesManaged();
            aes.BlockSize = 128;
            aes.KeySize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Key = Encoding.UTF8.GetBytes(secretKey);
            aes.Padding = PaddingMode.PKCS7;
            aes.IV = Encoding.UTF8.GetBytes(secretKey);

            Console.WriteLine("begin to decrypt 1M file...");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            using (FileStream fout = new FileStream("test1_de.dat",FileMode.Create,FileAccess.Write))
            {
                using (CryptoStream cryptostream = new CryptoStream(fout, aes.CreateDecryptor(aes.Key, aes.IV), CryptoStreamMode.Write))
                {
                    using (FileStream fin = new FileStream("test1_en.dat", FileMode.Open, FileAccess.Read))
                    {
                        fin.CopyTo(cryptostream);
                    }
                }
            }
            sw.Stop();
            Console.WriteLine("file decrypt complete, time elapsed:{0} ms",sw.ElapsedMilliseconds);

            Console.WriteLine("begin to decrypt 100M file...");
            sw = new Stopwatch();
            sw.Start();
            using (FileStream fout = new FileStream("test2_de.dat",FileMode.Create,FileAccess.Write))
            {
                using (CryptoStream cryptostream = new CryptoStream(fout, aes.CreateDecryptor(aes.Key, aes.IV), CryptoStreamMode.Write))
                {
                    using (FileStream fin = new FileStream("test2_en.dat", FileMode.Open, FileAccess.Read))
                    {
                        fin.CopyTo(cryptostream);
                    }
                }
            }
            sw.Stop();
            Console.WriteLine("file decrypt complete, time elapsed:{0} ms",sw.ElapsedMilliseconds);

            Console.WriteLine("begin to decrypt 1G file...");
            sw = new Stopwatch();
            sw.Start();
            using (FileStream fout = new FileStream("test3_de.dat",FileMode.Create,FileAccess.Write))
            {
                using (CryptoStream cryptostream = new CryptoStream(fout, aes.CreateDecryptor(aes.Key, aes.IV), CryptoStreamMode.Write))
                {
                    using (FileStream fin = new FileStream("test3_en.dat", FileMode.Open, FileAccess.Read))
                    {
                        fin.CopyTo(cryptostream);
                    }
                }
            }
            sw.Stop();
            Console.WriteLine("file decrypt complete, time elapsed:{0} ms",sw.ElapsedMilliseconds);
        }

        static void Encrypt()
        {
            Console.WriteLine("begin to encrypt...");
            var aes = new AesManaged();
            aes.BlockSize = 128;
            aes.KeySize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Key = Encoding.UTF8.GetBytes(secretKey);
            aes.Padding = PaddingMode.PKCS7;
            aes.IV = Encoding.UTF8.GetBytes(secretKey);

            Console.WriteLine("begin to encrypt 1M file...");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            using (FileStream fout = new FileStream("test1_en.dat",FileMode.Create,FileAccess.Write))
            {
                using (CryptoStream cryptostream = new CryptoStream(fout, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write))
                {
                    using (FileStream fin = new FileStream("test1.dat", FileMode.Open, FileAccess.Read))
                    {
                        fin.CopyTo(cryptostream);
                    }
                }
            }
            sw.Stop();
            Console.WriteLine("file encrypt complete, time elapsed:{0} ms",sw.ElapsedMilliseconds);

            Console.WriteLine("begin to encrypt 100M file...");
            sw = new Stopwatch();
            sw.Start();
            using (FileStream fout = new FileStream("test2_en.dat",FileMode.Create,FileAccess.Write))
            {
                using (CryptoStream cryptostream = new CryptoStream(fout, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write))
                {
                    using (FileStream fin = new FileStream("test2.dat", FileMode.Open, FileAccess.Read))
                    {
                        fin.CopyTo(cryptostream);
                    }
                }
            }
            sw.Stop();
            Console.WriteLine("file encrypt complete, time elapsed:{0} ms",sw.ElapsedMilliseconds);

            Console.WriteLine("begin to encrypt 1G file...");
            sw = new Stopwatch();
            sw.Start();
            using (FileStream fout = new FileStream("test3_en.dat",FileMode.Create,FileAccess.Write))
            {
                using (CryptoStream cryptostream = new CryptoStream(fout, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write))
                {
                    using (FileStream fin = new FileStream("test3.dat", FileMode.Open, FileAccess.Read))
                    {
                        fin.CopyTo(cryptostream);
                    }
                }
            }
            sw.Stop();
            Console.WriteLine("file encrypt complete, time elapsed:{0} ms",sw.ElapsedMilliseconds);


        }
        static void CreateTxt()
        {
            Console.WriteLine("begin to create dat....");
            var random = new Random(0);
            Console.WriteLine("begin to create 1M file...");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            FileStream f1 = new FileStream("test1.dat",FileMode.Create,FileAccess.ReadWrite);
            for (int i=0; i<1024; i++)
            {
                var byt = new byte[1024];
                random.NextBytes(byt);
                f1.Write(byt);
            }
            f1.Close();
            sw.Stop();
            Console.WriteLine("file create complete, time elapsed:{0} ms",sw.ElapsedMilliseconds);

            Console.WriteLine("begin to create 100M file...");
            sw = new Stopwatch();
            sw.Start();
            FileStream f2 = new FileStream("test2.dat",FileMode.Create,FileAccess.ReadWrite);
            for (int i=0; i<1024*100; i++)
            {
                var byt = new byte[1024];
                random.NextBytes(byt);
                f2.Write(byt);
            }
            f2.Close();
            sw.Stop();
            Console.WriteLine("file create complete, time elapsed:{0} ms",sw.ElapsedMilliseconds);

            Console.WriteLine("begin to create 1G file...");
            sw = new Stopwatch();
            sw.Start();
            FileStream f3 = new FileStream("test3.dat",FileMode.Create,FileAccess.ReadWrite);
            for (int i=0; i<1024*1024; i++)
            {
                var byt = new byte[1024];
                random.NextBytes(byt);
                f3.Write(byt);
            }
            f3.Close();
            sw.Stop();
            Console.WriteLine("file create complete, time elapsed:{0} ms",sw.ElapsedMilliseconds);

        }
    }
}
