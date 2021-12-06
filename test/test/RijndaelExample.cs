using System;
using System.IO;
using System.Security.Cryptography;

namespace test
{

    public class RijndaelExample
    {
        /// <summary>
        /// 执行demo操作
        /// </summary>
        /// <returns></returns>
        public void Execute()
        {
            try
            {
                string original = "Here is no money! what are you doning?";

                // Create a new instance of the RijndaelManaged
                // class.  This generates a new key and initialization
                // vector (IV).
                using (RijndaelManaged myRijndael = new RijndaelManaged())
                {
                    // myRijndael.GenerateKey();
                    // myRijndael.GenerateIV();
                    //var key = Convert.ToBase64String(myRijndael.Key);
                    // var iv = Convert.ToBase64String(myRijndael.IV);
                    myRijndael.Key = Convert.FromBase64String("WRq3Al3IRNok5S/CNvXwtz32e9BZOhQ1f6TBa5OwAC8=");

                    // Encrypt the string to an array of bytes.
                    // byte[] encrypted = EncryptStringToBytes(original, myRijndael.Key, myRijndael.IV);
                    byte[] encrypted = Convert.FromBase64String("d6c90rfzzEYoO8qVzsvq2yi/IwIBfuNC9FmTwdazjhmEOzhWJFS32ispPcOkDbbE");
                    // Decrypt the bytes to a string.
                    string roundtrip = DecryptStringFromBytes(encrypted, myRijndael.Key, myRijndael.IV);

                    //Display the original data and the decrypted data.
                    Console.WriteLine("Original:   {0}", original);
                    Console.WriteLine("Round Trip: {0}", roundtrip);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.ECB;
                rijAlg.Padding = PaddingMode.PKCS7;
                // rijAlg.KeySize = 128;
                rijAlg.Key = Key;
                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, null);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;
                rijAlg.Mode = CipherMode.ECB;
                rijAlg.Padding = PaddingMode.PKCS7;
                // rijAlg.KeySize = 128;
                rijAlg.Key = Key;
                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor();

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        static string ConvertTo16(byte[] buffers)
        {
            var res = string.Empty;
            foreach (var buffer in buffers)
            {
                res += buffer.ToString("X2");
            }
            return res;
        }
    }
}