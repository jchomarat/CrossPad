using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using CrossPad.Core.Services;
using CrossPad.Core.Helpers;
using System.Diagnostics;

namespace CrossPad
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        string statusText = "No file selected";
        string textContent = string.Empty;
        string fileName = string.Empty;
        bool isDirty = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public string StatusText
        {
            set
            {
                if (statusText != value)
                {
                    statusText = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("StatusText"));
                }
            }
            get { return statusText; }
        }

        public string TextContent
        {
            set
            {
                if (textContent != value)
                {
                    textContent = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("TextContent"));
                }
            }
            get { return textContent; }
        }

        public bool IsDirty
        {
            set
            {
                if (isDirty != value)
                {
                    isDirty = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("IsDirty"));
                }
            }
            get { return isDirty; }
        }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void Open_Clicked(object sender, EventArgs e)
        {
            // Call the Picker from the current platform implementation of the service
            string selectedFileName = DependencyService.Get<IFileService>().GetFilePath(new string[] { "Text file|*.txt" });
            if (!string.IsNullOrEmpty(selectedFileName))
            {
                // Store the file name and path to save it later
                fileName = selectedFileName;
                
                // Open the file
                string fileContent = FilesHelper.ReadAll(fileName);
                if (!string.IsNullOrEmpty(fileContent))
                {
                    // Update the UI
                    TextContent = fileContent;
                    IsDirty = false;
                    StatusText = fileName;
                }
            }
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            await this.Save();
        }

        private async void New_Clicked(object sender, EventArgs e)
        {
            // Reset all the UI (but save first if required)
            if (IsDirty)
            {
                await Save(true);
            }

            fileName = string.Empty;
            TextContent = string.Empty;
            IsDirty = false;
            StatusText = "No file selected";
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Handle first launch & no change at all
            if (e.OldTextValue != null && e.OldTextValue != e.NewTextValue)
            {
                // Mark the document as dirty, meaning it will have to be saved
                IsDirty = true;
            }
        }

        /// <summary>
        /// Save the current text
        /// </summary>
        /// <param name="AskFirst">Prompt the user to save first</param>
        public async Task Save(bool AskFirst = false)
        {
            if (AskFirst)
            {
                // Ask the user if s/he wants to save the file first
                var answer = await DisplayAlert("Save document", "Do you wan't to save your document?", "Yes", "No");
                if (!answer)
                {
                    // The user has decided not to save, do nothing
                    return;
                }
            }

            if (!string.IsNullOrEmpty(fileName))
            {
                // The file exists, just save the content
                if (FilesHelper.WriteAll(fileName, TextContent))
                {
                    IsDirty = false;
                }
            }
            else
            {
                // Ask the user where to save the file
                // Call the dialog to pick destination from the current platform implementation of the service
                string newFileName = DependencyService.Get<IFileService>().SetFilePath(new string[] { "Text file|*.txt" });

                if (!string.IsNullOrEmpty(newFileName))
                {
                    if (FilesHelper.WriteAll(newFileName, TextContent))
                    {
                        fileName = newFileName;
                        IsDirty = false;
                        StatusText = fileName;
                    }
                }
            }
        }
    }
}
