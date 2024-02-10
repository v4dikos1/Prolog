namespace Prolog.Core.EntityFramework.Features.Encryption.Internal.Exceptions;

/// <summary>
/// Exception for decryption failures.
/// </summary>
[Serializable]
internal class DecryptionException : Exception
{
    public DecryptionException()
    {
    }

    public DecryptionException(string message) : base(message)
    {
    }

    public DecryptionException(string message, Exception inner) : base(message, inner)
    {
    }
}
