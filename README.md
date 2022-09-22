# liburlencoding-dotnet

[![](https://img.shields.io/github/v/tag/thechampagne/liburlencoding-dotnet?label=version)](https://github.com/thechampagne/liburlencoding-dotnet/releases/latest) [![](https://img.shields.io/github/license/thechampagne/liburlencoding-dotnet)](https://github.com/thechampagne/liburlencoding-dotnet/blob/main/LICENSE)

Dotnet binding for **liburlencoding**.

### API

```csharp
namespace URL
{
    public class Encoding
    {
        public static string? Encode(string data);

        public static string? EncodeBinary(string data);

        public static string? Decode(string data);

        public static string? DecodeBinary(string data);
    }
}
```

### References
 - [liburlencoding](https://github.com/thechampagne/liburlencoding)

### License

This repo is released under the [MIT](https://github.com/thechampagne/liburlencoding-dotnet/blob/main/LICENSE).
