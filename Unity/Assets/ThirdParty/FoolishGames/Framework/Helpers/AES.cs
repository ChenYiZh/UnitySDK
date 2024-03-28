using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using FoolishGames.Log;

public class AES
{
    private static KeyValuePair<string, byte[]> _IVPair = new KeyValuePair<string, byte[]>();

    public static string Encrypt(string str, string key, string iv)
    {
        if (_IVPair.Key != iv)
        {
            _IVPair = new KeyValuePair<string, byte[]>(iv, Encoding.UTF8.GetBytes(iv));
        }
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        return Encrypt(str, keyBytes, _IVPair.Value);
    }

    public static string Decrypt(string str, string key, string iv)
    {
        if (_IVPair.Key != iv)
        {
            _IVPair = new KeyValuePair<string, byte[]>(iv, Encoding.UTF8.GetBytes(iv));
        }
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        return Decrypt(str, keyBytes, _IVPair.Value);
    }

    public static string Encrypt(string str, byte[] key, byte[] iv)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(str);
        byte[] encrypted = Encrypt(buffer, key, iv);
        return Convert.ToBase64String(encrypted);
    }

    public static string Decrypt(string str, byte[] key, byte[] iv)
    {
        byte[] encrypted = Convert.FromBase64String(str);
        byte[] buffer = Decrypt(encrypted, key, iv);
        return Encoding.UTF8.GetString(buffer);
    }

    public static byte[] Encrypt(byte[] buffer, byte[] key, byte[] iv)
    {
        if (buffer == null || buffer.Length <= 0)
        {
            FConsole.WriteException(new ArgumentNullException("buffer"));
            return null;
        }
        if (key == null || key.Length <= 0)
        {
            FConsole.WriteException(new ArgumentNullException("key"));
            return null;
        }
        if (iv == null || iv.Length <= 0)
        {
            FConsole.WriteException(new ArgumentNullException("iv"));
            return null;
        }

        byte[] encrypted;
        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;
            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    //using (StreamWriter swEncrypt = new StreamWriter(csEncrypt, Encoding.UTF8))
                    //{
                    //    //Write all data to the stream.
                    //    swEncrypt.Write(buffer);
                    //}
                    //encrypted = msEncrypt.ToArray();
                    csEncrypt.Write(buffer, 0, buffer.Length);
                    csEncrypt.FlushFinalBlock();
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }

    public static byte[] Decrypt(byte[] encrypted, byte[] key, byte[] iv)
    {
        if (encrypted == null || encrypted.Length <= 0)
        {
            FConsole.WriteException(new ArgumentNullException("encrypted"));
            return null;
        }
        if (key == null || key.Length <= 0)
        {
            FConsole.WriteException(new ArgumentNullException("key"));
            return null;
        }
        if (iv == null || iv.Length <= 0)
        {
            FConsole.WriteException(new ArgumentNullException("iv"));
            return null;
        }

        byte[] buffer = null;
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(encrypted))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    //using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    //{

                    //    // Read the decrypted bytes from the decrypting stream
                    //    // and place them in a string.
                    //    plaintext = srDecrypt.ReadToEnd();     
                    //}

                    using (MemoryStream srDecrypt = new MemoryStream())
                    {
                        int length;
                        byte[] temp = new byte[1024];
                        while ((length = csDecrypt.Read(temp, 0, temp.Length)) > 0)
                        {
                            srDecrypt.Write(temp, 0, length);
                        }
                        buffer = srDecrypt.GetBuffer();
                    }
                }
            }
        }

        return buffer;
    }
}
