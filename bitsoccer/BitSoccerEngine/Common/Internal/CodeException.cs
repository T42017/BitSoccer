using System;

public class CodeException : SystemException
{
    public CodeException(string assembly, string type)
        : base("Invalid code in " + assembly + " in type " + type)
    {
    }
}
