using System.Threading.Tasks;
using Xamarin.Forms;
using CrossPad.Core.Services;
using Microsoft.Win32;

[assembly: Dependency(typeof(CrossPad.WPF.Custom.FileService))]
namespace CrossPad.WPF.Custom
{
    public class FileService : IFileService
    {
        public Task<string> GetFilePath(string[] AllowedTypes = null)
        {
            var tcs = new TaskCompletionSource<string>();

            OpenFileDialog pickDialog = new OpenFileDialog();
            if (AllowedTypes != null && AllowedTypes.Length > 0)
            {
                pickDialog.Filter = string.Join('|', AllowedTypes);
            }

            if (pickDialog.ShowDialog() == true)
            {
                tcs.SetResult(pickDialog.FileName);
            }
            else
            {
                tcs.SetResult(string.Empty);
            }

            return tcs.Task;
        }

        public Task<string> SetFilePath(string[] AllowedTypes = null)
        {
            var tcs = new TaskCompletionSource<string>();

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = string.Join('|', AllowedTypes);
            saveDialog.ShowDialog();

            if (!string.IsNullOrEmpty(saveDialog.FileName))
            {
                // The user has picked a location, send it back so that the file can be saved
                tcs.SetResult(saveDialog.FileName);
            }
            else
            {
                tcs.SetResult(string.Empty);
            }

            return tcs.Task;
        }
    }
}
