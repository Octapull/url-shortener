using api.Configuration;
using Microsoft.Extensions.Options;

namespace api.Services.Impl;

public class CodeGenerator : ICodeGenerator
{
    private readonly ShortLinkOptions _shortLinkOptions;

    public CodeGenerator(IOptions<ShortLinkOptions> options)
    {
        _shortLinkOptions = options.Value;
    }

    public string GenerateCode()
    {
        var random = new Random();
        var lenght = _shortLinkOptions.DefaultLength;
        var chars = _shortLinkOptions.AllowedCharacters;
        var code = new char[lenght];
        
        for(int i = 0; i < lenght; i++)
        {
            code[i] = chars[random.Next(chars.Length)];
        }

        return new string(code);

    }
}