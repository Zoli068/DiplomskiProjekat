using System.IO;
using System.Threading.Tasks;

namespace Common.Communication
{
    /// <summary>
    /// Interfaces for describing method for Async Securing the Communication
    /// </summary>
    public interface IAsyncSecureCommunication
    {
        /// <summary>
        /// Definition of the method for securing a stream
        /// </summary>
        /// <param name="stream">The stream which we want to secure</param>
        /// <returns>The secured stream
        Task<Stream> SecureStream(Stream stream);
    }
}
