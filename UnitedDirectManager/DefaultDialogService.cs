using Microsoft.Win32;
using System.IO;
using System.Windows;
using UnitedDirectManager.Interfeces;
using System.Drawing;
using System.Collections.Generic;

namespace UnitedDirectManager
{
    public class DefaultDialogService : IDialogService
    {
        public string FilePath { get; set; }

        public List<string> FilePathes { get; set; } = new List<string>();

        public bool OpenFileDialog()
        {
            FilePathes.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.png, *.webp) | *.jpg; *.png; *.webp";
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = Path.GetFullPath(@"D:\Dima\.NET\MU_Store\WebUI\Images\Clothes Images");
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    FilePathes.Add(filename);
                }
                return true;
            }
            return false;
        }

        public byte[] ImageToByteArray(string filePath)
        {
            Image img = Image.FromFile(filePath);
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
