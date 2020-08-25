using System.Threading.Tasks;

namespace CrossPad.Core.Services
{
    public interface IFileService
    {
        /// <summary>
        /// Method to implement to open a file picker
        /// </summary>
        /// <param name="AllowedTypes">Array of allowed files types. Format "Text files|*.txt"</param>
        /// <returns>The full path of the selected file. Empty if none selected</returns>
        Task<string> GetFilePath(string[] AllowedTypes = null);

        /// <summary>
        /// Method to implement to save a file to
        /// </summary>
        /// <param name="AllowedTypes">Array of allowed files types. Format "Text files|*.txt"</param>
        /// <returns>The location and file name of the new file to save</returns>
        Task<string> SetFilePath(string[] AllowedTypes = null);
    }
}
