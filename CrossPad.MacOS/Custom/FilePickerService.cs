using Xamarin.Forms;
using CrossPad.Core.Services;
using AppKit;

[assembly: Dependency(typeof(CrossPad.MacOS.Custom.FileService))]
namespace CrossPad.MacOS.Custom
{
    public class FileService : IFileService
    {
        public string GetFilePath(string[] AllowedTypes = null)
        {
            var dlg = NSOpenPanel.OpenPanel;
            dlg.CanChooseFiles = true;
            dlg.CanChooseDirectories = false;
            dlg.AllowedFileTypes = new string[] { "txt" };

            if (dlg.RunModal() == 1)
            {
                // Nab the first file
                var url = dlg.Urls[0];

                if (url != null)
                {
                    return url.Path;
                }
            }
            return string.Empty;
        }

        public string SetFilePath(string[] AllowedTypes = null)
        {
            var dlg = new NSSavePanel();
            dlg.Title = "Save File";
            dlg.AllowedFileTypes = new string[] { "txt" };

            if (dlg.RunModal() == 1)
            {
                if (dlg.Url != null)
                {
                    return dlg.Url.Path;
                }
            }
            return string.Empty;
        }
    }
}
