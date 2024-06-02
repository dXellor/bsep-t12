using System.ComponentModel.Design;
using System.Security.Cryptography;
using System.Text;

namespace bsep_dll.Helpers.Encryption;

public static class EncryptionUtils
{
    public static string EncryptWithEncodingType(string data, string key, string iv, EncodingType encodingType)
    {
        byte[] dataToEncrypt;
        switch (encodingType)
        {
            case EncodingType.UTF8:
                dataToEncrypt = Encoding.UTF8.GetBytes(data);
                return Convert.ToBase64String(EncryptData(dataToEncrypt, key, iv));
            
            case EncodingType.BASE64:
                dataToEncrypt = Convert.FromBase64String(data);
                return Convert.ToBase64String(EncryptData(dataToEncrypt, key, iv));
            
            default:
                throw new ArgumentException("Encoding not supported");
        }
    }
    
    public static string DecryptWithEncodingType(string data, string key, string iv, EncodingType encodingType)
    {
        byte[] dataToDecrypt;
        switch (encodingType)
        {
            case EncodingType.UTF8:
                dataToDecrypt = Convert.FromBase64String(data);
                return Encoding.UTF8.GetString(DecryptData(dataToDecrypt, key, iv));
            
            case EncodingType.BASE64:
                dataToDecrypt = Convert.FromBase64String(data);
                return Convert.ToBase64String(DecryptData(dataToDecrypt, key, iv));
            
            default:
                throw new ArgumentException("Encoding not supported");
        }
    }

    private static byte[] EncryptData(byte[] data, string key, string iv)
    {
        using var aes = Aes.Create();
        aes.Key = Convert.FromBase64String(key);
        aes.IV = Convert.FromBase64String(iv);
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        
        var encryptor = aes.CreateEncryptor();

        var encryptedBytes = encryptor.TransformFinalBlock(data, 0, data.Length);
        return encryptedBytes;
    }
    
    private static byte[] DecryptData(byte[] data, string key, string iv)
    {
        using var aes = Aes.Create();
        aes.Key = Convert.FromBase64String(key);
        aes.IV = Convert.FromBase64String(iv);
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var decryptor = aes.CreateDecryptor();
        var decryptedBytes = decryptor.TransformFinalBlock(data, 0, data.Length);

        return decryptedBytes;
    }
}