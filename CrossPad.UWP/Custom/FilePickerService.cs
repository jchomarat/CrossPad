using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using CrossPad.Core.Services;
using Xamarin.Forms.Internals;
using System.Collections.Generic;

[assembly: Dependency(typeof(CrossPad.UWP.Custom.FileService))]
namespace CrossPad.UWP.Custom
{
    public class FileService : IFileService
    {
        public async Task<string> GetFilePath(string[] AllowedTypes = null)
        {
            var tcs = new TaskCompletionSource<string>();

            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;

            if (AllowedTypes != null && AllowedTypes.Length > 0)
            {
                AllowedTypes.ForEach(t =>
                {
                    picker.FileTypeFilter.Add(t.Split("|*")[1]);
                });
            }

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                return file.Path;
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<string> SetFilePath(string[] AllowedTypes = null)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            if (AllowedTypes != null && AllowedTypes.Length > 0)
            {
                AllowedTypes.ForEach(t =>
                {
                    picker.FileTypeChoices.Add(t.Split("|*")[0], new List<string>() { t.Split("|*")[1] });
                });
            }

            Windows.Storage.StorageFile file = await picker.PickSaveFileAsync();

            if (file != null)
            {
                return file.Path;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
