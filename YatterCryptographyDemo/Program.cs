// See https://aka.ms/new-console-template for more information
using System;
using System.Text;
using Yatter.Security.Cryptography;


Console.WriteLine("Hello, RSA Cryptography!");

var originalText = "The quick brown fox.";

Console.WriteLine($"Original Text: {originalText}");

var cryptographyKeyManager = new CryptographyKeyManager();

cryptographyKeyManager.CreateKeySet();

var encoded = cryptographyKeyManager.RSAEncryptIntoBase64(originalText);

Console.WriteLine($"Original Text Encoded:");

Console.WriteLine(encoded);

var decoded = cryptographyKeyManager.RSADecryptFromBase64String(encoded);

if(decoded.Equals(originalText))
{
    Console.WriteLine("Yahoo! Decoded text equals original text!");

    Console.WriteLine();

    Console.WriteLine($"Exported Private Key in PEM format:");

    Console.WriteLine(cryptographyKeyManager.ExportRSAPrivateKeyPEMString());

    Console.WriteLine();

    Console.WriteLine($"Exported Public Key in PEM format:");
    Console.WriteLine(cryptographyKeyManager.ExportRSAPublicKeyPEMString());

    var privateKeyString = cryptographyKeyManager.ExportRSAPrivateKeyPEMString();
    var privateKeyBytes = cryptographyKeyManager.ExportRSAPrivateKeyBytes();
    var publicKeyString = cryptographyKeyManager.ExportRSAPublicKeyPEMString();
    var publicKeyBytes = cryptographyKeyManager.ExportRSAPublicKeyBytes();

    cryptographyKeyManager = null;

    cryptographyKeyManager = new CryptographyKeyManager();

    Console.WriteLine($"Clearing Key Manager ...");
    Console.WriteLine($"Importing Private Key Bytes ...");

    cryptographyKeyManager.ImportRSAPrivateKey(privateKeyBytes);

    originalText = " jumped over the lazy dog";
    Console.WriteLine($"New Original Text: {originalText}");

    Console.WriteLine($"");

    Console.WriteLine($"Encoding new original text ...");
    encoded = cryptographyKeyManager.RSAEncryptIntoBase64(originalText);
    Console.WriteLine($"Encoded Original Text:");
    Console.WriteLine(encoded);


    Console.WriteLine($"Decoding encoded text ...");
    decoded = cryptographyKeyManager.RSADecryptFromBase64String(encoded);

    Console.WriteLine();

    Console.WriteLine($"Decoded text:");
    Console.WriteLine(decoded);

    Console.WriteLine($"");

    Console.WriteLine($"Clearing Key Manager ...");
    cryptographyKeyManager = null;

    cryptographyKeyManager = new CryptographyKeyManager();

    Console.WriteLine($"");
    Console.WriteLine($"Converting Private Key Bytes to Base64 string ...");
    var privateKeyBytesAsBase64 = Convert.ToBase64String(privateKeyBytes);

    Console.WriteLine($"Converting Public Key Bytes to Base64 string ...");
    var publicKeyBytesAsBase64 = Convert.ToBase64String(publicKeyBytes);

    Console.WriteLine();

    Console.WriteLine("Private Key Bytes as Base64");

    Console.WriteLine(privateKeyBytesAsBase64);

    Console.WriteLine();

    Console.WriteLine("Public Key Bytes as Base64");

    Console.WriteLine();

    Console.WriteLine(publicKeyBytesAsBase64);

    cryptographyKeyManager = null;

    cryptographyKeyManager = new CryptographyKeyManager();

    Console.WriteLine($"");
    Console.WriteLine($"Converting Base64 Private Key string back to bytes ...");
    privateKeyBytes = System.Convert.FromBase64String(privateKeyBytesAsBase64);
    Console.WriteLine($"Converting Base64 Public Key back to bytes ...");
    publicKeyBytes = System.Convert.FromBase64String(publicKeyBytesAsBase64);

    Console.WriteLine($"");

    Console.WriteLine($"Importing new Private Key Bytes into Key Manager ...");
    cryptographyKeyManager.ImportRSAPrivateKey(privateKeyBytes);

    Console.WriteLine($"");
    
    originalText = "The dog was so lazy he didn't even notice the quick brown fox!";
    Console.WriteLine($"New Original Text: {originalText}");
    Console.WriteLine();

    Console.WriteLine($"Encoding new original text ...");
    encoded = cryptographyKeyManager.RSAEncryptIntoBase64(originalText);

    Console.WriteLine();
    Console.WriteLine($"Encoded new original text:");
    Console.WriteLine(encoded);

    Console.WriteLine($"");
    Console.WriteLine($"Decoding new original text ...");
    decoded = cryptographyKeyManager.RSADecryptFromBase64String(encoded);
    Console.WriteLine();

    Console.WriteLine($"Decoded new original text:");
    Console.WriteLine(decoded);

    Console.WriteLine();

    Console.WriteLine();


}

Console.ReadLine();


