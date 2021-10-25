## SubModules Alert!

As we have submodules, don't forget to put the ```--recursive``` switch in the clone statement so that you clone all the submodules as well!

```git clone --recursive https://github.com/YatterOfficial/YatterCryptographyDemo.git```

# YatterCryptographyDemo

This demo shows how to use .NET Code to encrypt and decrypt, using RSA Public and Private key encryption.

- No third party libraries are used, just [System.Security.Cryptography](https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography?view=net-6.0)
- We have packaged the demo into it's own [Yatter.Security.Cryptography](https://github.com/YatterOfficial/Yatter.Security.Cryptography) namespace project repo, so that it can be cloned as-is, as well as packed into a Nuget package.

## Encrypting and Decrypting

Encryption and decryption, using RSA Public and Private keys, is just a two-line affair - and the following methods in the [demo Key Manager](https://github.com/YatterOfficial/Yatter.Security.Cryptography/blob/1eaceb6ece4f855966d4d75be70f23750666bdc9/Yatter.Security.Cryptography/CryptographyKeyManager.cs) class illustrate the point:

```
        public string RSAEncryptIntoBase64(string text)
        {
            var data = Encoding.UTF8.GetBytes(text);
            var cypher = rsaCryptoServiceProvider.Encrypt(data, false);

            return Convert.ToBase64String(cypher);
        }
```

and

```
        public string RSADecryptFromBase64String(string base64Cypher)
        {
            var encodedBytes = Convert.FromBase64String(base64Cypher);

            var decodedBytes = rsaCryptoServiceProvider.Decrypt(encodedBytes, false);

            return Encoding.UTF8.GetString(decodedBytes);
        }
```

These methods do, however, assume that you have already loaded a public and/or private key into the ```RSACryptoServiceProvider```, as follows.

## Loading, or creating, Public and Private Keys

Prior to encrypting and decrypting, you need to either create, or load, public and private keys into the ```RSACryptoServiceProvider```, as follows:

- Create new Public and Private Keys:

```
        public void CreateKeySet()
        {
            _publicKey = rsaCryptoServiceProvider.ExportParameters(false);
            _privateKey = rsaCryptoServiceProvider.ExportParameters(true);
        }

```

- Load existing Public and Private Keys:

```
        public int ImportRSAPrivateKey(ReadOnlySpan<byte> source)
        {
            int bytesRead;
            rsaCryptoServiceProvider.ImportRSAPrivateKey(source, out bytesRead);

            return bytesRead;
        }
```

and

```
        public int ImportRSAPublicKey(ReadOnlySpan<byte> source)
        {
            int bytesRead;
            rsaCryptoServiceProvider.ImportRSAPublicKey(source, out bytesRead);

            return bytesRead;
        }
```

Having created new keys, you can also export their binary PKCS#1 RSAPrivateKey and PKCS#1 RSAPublicKey as follows:

```
        public byte[] ExportRSAPrivateKeyBytes()
        {
            return rsaCryptoServiceProvider.ExportRSAPrivateKey();
        }
```

and

```
        public byte[] ExportRSAPublicKeyBytes()
        {
            return rsaCryptoServiceProvider.ExportRSAPublicKey();
        }
```

- If you want to save these, just convert them to a Base64 string, which you can later convert back to the original byte[] and import them again.
- You will, of course, need to distribute your Public key so that others can use it to encrypt data that they then send to you - and of course, when you receive data that has been encrypted with your public key, you will have to be able to retrieve your Private key so that you can decrypt what has been sent to you. Thus, exporting the keys and saving them is important!

We also export in PEM format, however don't yet support importing PEM format. To import PEM format, you will need to convert them into their binary (byte[]) PKCS#1 RSAPrivateKey and PKCS#1 RSAPublicKey forms and import using the available methods:

```
        public int ImportRSAPrivateKey(ReadOnlySpan<byte> source)
        {
            int bytesRead;
            rsaCryptoServiceProvider.ImportRSAPrivateKey(source, out bytesRead);

            return bytesRead;
        }
```

and 

```
        public int ImportRSAPublicKey(ReadOnlySpan<byte> source)
        {
            int bytesRead;
            rsaCryptoServiceProvider.ImportRSAPublicKey(source, out bytesRead);

            return bytesRead;
        }
```

Examples of PEM format are quite familiar:

```
-----BEGIN RSA PRIVATE KEY-----
MIICXAIBAAKBgQDIuBI/us8FF3LquXL9rEGnKIgNCk5Yh92rwHSnguxcx44ha15H
x8vjHdevvyONzYdRGS86M2EaYZBjCASxbKWxrkOkqmY7S6/QRYiPOF/+bJB5YHch
HiNgD1HgV94SKi0wcrQg3ZUy60bz/NhFyBDg4gxwsDl58B/ASVJ2RAS8BwIDAQAB
AoGAKZNvPEIutYhI5nPpbMAsjTppDAJxfgcOlI/12ejVmtY/C48Y5VduVSoOTjoS
XEIRYGwOC0kvOO6yoMC9cB7oJwrIg9VT1g+mtDt5Wc2gl/0gF5TmLFV1gubLQ256
6LOGeC6lNTqEizHYr3my2m8dY63z7EQdb7IRUd9d+SziFmECQQDs94Zaj6xFvOQX
HnPmYM5AFYpNZh5XFEffybZ1kNIr4ysYtTpBQnYTgOtwpA8EMbfMT2sbG7n/4m3q
jRota4R3AkEA2Nc5Q2ELwtFzlG0jPbxTApg552oS3txll8IN0+zmaKCV5b8WShFr
FdG8/pdxeKZTZXDK+HKr+hoiLK+87YU48QJAKID+pAgeyCTLT/BSmzQ8zNNhum0U
DopW5reRynKgmgPQX/7KIFF94UON+sjwECV3ZyLECfQpTMWlSAwqR00zbQJAM585
QLMvy5d1fpZk12OoF2wKDO+RwoCRpwlJpXQ2fh4M0X8mXUe8SJt/9NQ07VYbcIDj
sTYLfyUNkbncrmcS4QJBAK3PXTcttY+L5F+KSDY5RNkAiO6bxTqU+ZPnAH6elTbS
WHPTEUQxhfkWf9wUJgAiaq2HSMzXX9ichtUH8vPFKTY=
-----END RSA PRIVATE KEY-----
```

and

```
Exported Public Key in PEM format:
-----BEGIN PUBLIC KEY-----
MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDIuBI/us8FF3LquXL9rEGnKIgN
Ck5Yh92rwHSnguxcx44ha15Hx8vjHdevvyONzYdRGS86M2EaYZBjCASxbKWxrkOk
qmY7S6/QRYiPOF/+bJB5YHchHiNgD1HgV94SKi0wcrQg3ZUy60bz/NhFyBDg4gxw
sDl58B/ASVJ2RAS8BwIDAQAB
-----END PUBLIC KEY-----
```

However if you merely save the Base64 versions, they will look like this:

```
MIICXAIBAAKBgQDIuBI/us8FF3LquXL9rEGnKIgNCk5Yh92rwHSnguxcx44ha15Hx8vjHdevvyONzYdRGS86M2EaYZBjCASxbKWxrkOkqmY7S6/QRYiPOF/+bJB5YHchHiNgD1HgV94SKi0wcrQg3ZUy60bz/NhFyBDg4gxwsDl58B/ASVJ2RAS8BwIDAQABAoGAKZNvPEIutYhI5nPpbMAsjTppDAJxfgcOlI/12ejVmtY/C48Y5VduVSoOTjoSXEIRYGwOC0kvOO6yoMC9cB7oJwrIg9VT1g+mtDt5Wc2gl/0gF5TmLFV1gubLQ2566LOGeC6lNTqEizHYr3my2m8dY63z7EQdb7IRUd9d+SziFmECQQDs94Zaj6xFvOQXHnPmYM5AFYpNZh5XFEffybZ1kNIr4ysYtTpBQnYTgOtwpA8EMbfMT2sbG7n/4m3qjRota4R3AkEA2Nc5Q2ELwtFzlG0jPbxTApg552oS3txll8IN0+zmaKCV5b8WShFrFdG8/pdxeKZTZXDK+HKr+hoiLK+87YU48QJAKID+pAgeyCTLT/BSmzQ8zNNhum0UDopW5reRynKgmgPQX/7KIFF94UON+sjwECV3ZyLECfQpTMWlSAwqR00zbQJAM585QLMvy5d1fpZk12OoF2wKDO+RwoCRpwlJpXQ2fh4M0X8mXUe8SJt/9NQ07VYbcIDjsTYLfyUNkbncrmcS4QJBAK3PXTcttY+L5F+KSDY5RNkAiO6bxTqU+ZPnAH6elTbSWHPTEUQxhfkWf9wUJgAiaq2HSMzXX9ichtUH8vPFKTY=
```

and

```
MIGJAoGBAMi4Ej+6zwUXcuq5cv2sQacoiA0KTliH3avAdKeC7FzHjiFrXkfHy+Md16+/I43Nh1EZLzozYRphkGMIBLFspbGuQ6SqZjtLr9BFiI84X/5skHlgdyEeI2APUeBX3hIqLTBytCDdlTLrRvP82EXIEODiDHCwOXnwH8BJUnZEBLwHAgMBAAE=
```

The latter are quite easy to store anywhere, whether in a local file, or in an Azure Secrets key store.

_Don't store private keys in any mobile app because they are easy to extract from the binaries!_

You can, however, store public keys in any mobile app because they are designated as public anyway. People who use them can only use them to encrypt information that only you, with the private key, can then decrypt. 

## Console App Demo

- Just clone the repo and run it up, the Program.cs file does all the work and is as follows.
- The demo code is very busy, but is designed to prove importing and exporting of the keys, so there is a lot of repetitive code there.
- It calls the methods above, which although namespaced in a different project, are just raw C# files that only use System.Security.Cryptography

```
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
```
