using Xamarin.Forms;
using CrossPad.Core.Services;
using Microsoft.Win32;

[assembly: Dependency(typeof(CrossPad.WPF.Custom.FileService))]
namespace CrossPad.WPF.Custom
{
    public class FileService : IFileService
    {
        public string GetFilePath(string[] AllowedTypes = null)
        {
            OpenFileDialog pickDialog = new OpenFileDialog();
            if (AllowedTypes != null && AllowedTypes.Length > 0)
            {
                pickDialog.Filter = string.Join('|', AllowedTypes);
            }

            if (pickDialog.ShowDialog() == true)
            {
                return pickDialog.FileName;
            }
            else
            {
                return string.Empty;
            }
                //txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
        }

        public string SetFilePath(string[] AllowedTypes = null)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = string.Join('|', AllowedTypes);
            saveDialog.ShowDialog();

            if (!string.IsNullOrEmpty(saveDialog.FileName))
            {
                // The user has picked a location, send it back so that the file can be saved
                return saveDialog.FileName;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
