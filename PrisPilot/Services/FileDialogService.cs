using Microsoft.Win32;
using PrisPilot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrisPilot.Services
{
    public class FileDialogService : IFileDialogService
    {
        public string OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // Filters for the dialog
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                return openFileDialog.FileName;
            }

            //If the user closes the dialog, return an empty string
            return string.Empty;
        }
    }
}
