using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using CrossPad.Core.Services;
using Microsoft.Win32;

[assembly: Dependency(typeof(CrossPad.WPF.Custom.FileService))]
namespace CrossPad.WPF.Custom
{
    public class FileService : IFileService
    {
        public Task<string> GetFilePath(Tuple<string, string>[] AllowedTypes = null)
        {
            var tcs = new TaskCompletionSource<string>();

            OpenFileDialog pickDialog = new OpenFileDialog();
            if (AllowedTypes != null && AllowedTypes.Length > 0)
            {
                pickDialog.Filter = string.Join('|', AllowedTypes.Select(t => $"{t.Item1}|*{t.Item2}").ToArray());
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

        public Task<string> SetFilePath(Tuple<string, string>[] AllowedTypes = null)
        {
            var tcs = new TaskCompletionSource<string>();

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = string.Join('|', AllowedTypes.Select(t => $"{t.Item1}|*{t.Item2}").ToArray());
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
