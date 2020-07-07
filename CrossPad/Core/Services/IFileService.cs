namespace CrossPad.Core.Services
{
    public interface IFileService
    {
        /// <summary>
        /// Method to implement to open a file picker
        /// </summary>
        /// <param name="AllowedTypes">Array of allowed files types. Format "Text files|*.txt"</param>
        /// <returns>The full path of the selected file. Empty if none selected</returns>
        string GetFilePath(string[] AllowedTypes = null);

        string SetFilePath(string[] AllowedTypes = null);
    }
}
