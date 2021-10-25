## SubModules Alert!

As we have submodules, don't forget to put the ```--recursive``` switch in the clone statement so that you clone all the submodules as well!

```git clone --recursive https://github.com/YatterOfficial/YatterCryptographyDemo.git```

# YatterCryptographyDemo

This demo shows how to use .NET Code to encrypt and decrypt, using RSA Public and Private key encryption.

- No third party libraries are used, just [System.Security.Cryptography](https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography?view=net-6.0)
- We have packaged the demo into it's own [Yatter.Security.Cryptography](https://github.com/YatterOfficial/Yatter.Security.Cryptography) namespace project repo, so that it can be cloned as-is, as well as packed into a Nuget package.
