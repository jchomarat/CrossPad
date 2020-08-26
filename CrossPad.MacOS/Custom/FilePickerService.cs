using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using CrossPad.Core.Services;
using AppKit;

[assembly: Dependency(typeof(CrossPad.MacOS.Custom.FileService))]
namespace CrossPad.MacOS.Custom
{
    public class FileService : IFileService
    {
        public Task<string> GetFilePath(Tuple<string, string>[] AllowedTypes = null)
        {
            var tcs = new TaskCompletionSource<string>();

            var dlg = NSOpenPanel.OpenPanel;
            dlg.CanChooseFiles = true;
            dlg.CanChooseDirectories = false;
            dlg.AllowedFileTypes = AllowedTypes.Select(t => (t.Item2.StartsWith(".") ? t.Item2.Substring(1) : t.Item2)).ToArray<string>();

            if (dlg.RunModal() == 1)
            {
                // Nab the first file
                var url = dlg.Urls[0];

                if (url != null)
                {
                    tcs.SetResult(url.Path);
                }
            }
            else tcs.SetResult(string.Empty);

            return tcs.Task;
        }

        public Task<string> SetFilePath(Tuple<string, string>[] AllowedTypes = null)
        {
            var tcs = new TaskCompletionSource<string>();

            var dlg = new NSSavePanel();
            dlg.Title = "Save File";
            dlg.AllowedFileTypes = AllowedTypes.Select(t => (t.Item2.StartsWith(".") ? t.Item2.Substring(1) : t.Item2)).ToArray<string>();

            if (dlg.RunModal() == 1)
            {
                if (dlg.Url != null)
                {
                    tcs.SetResult(dlg.Url.Path);
                }
            }
            else tcs.SetResult(string.Empty);

            return tcs.Task;
        }
    }
}
