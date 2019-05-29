using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TotalCommanderProjectV2.DataModels;
using TotalCommanderProjectV2.Tolls;

namespace TotalCommanderProjectV2.Views
{
    /// <summary>
    /// Interaction logic for Panel.xaml
    /// </summary>
    public partial class FilesPanel : UserControl
    {
        // pola
        FileManager fileManager;
        string currentLocation = @"C:\Users\Paweł\Desktop\Pliki";

        // właściwości
        public bool IsSelected { get; internal set; }
        public string CurrentLocation { get => currentLocation; set => currentLocation = value; }

        // konstruktor
        public FilesPanel()
        {
            this.fileManager = new FileManager();
            InitializeComponent();
            RefreshPath(currentLocation);
        }

        // metody
        public void RefreshPath(string path)
        {
            DiscDirectory directory = new DiscDirectory(path);
            List<DiscElement> discElements = new List<DiscElement>();
            discElements.AddRange(directory.GetAllFiles());

            //filesStack.Children.Clear();
            ListBox.Items.Clear();

            foreach (DiscElement discElement in discElements)
            {
                if(discElement is DiscFile)
                {
                    FileView fileView = new FileView(discElement as DiscFile);
                    ListBox.Items.Add(fileView);
                    fileView.FileDeleteEvent += FileView_FileDeleteEvent;
                    fileView.FileOpenEvent += FileView_FileOpenEvent;
                }
                else
                {
                    DirectoryView directoryView = new DirectoryView(discElement as DiscDirectory);
                    ListBox.Items.Add(directoryView);
                    directoryView.directoryOpenEvent += DirectoryView_directoryOpenEvent;
                }
            }
        }
        internal void FileView_FileOpenEvent(DiscFile myFile)
        {
            fileManager.FileOpen(myFile, _FilesPanel);
        }
        internal void FileView_FileDeleteEvent(DiscFile myFile)
        {
            fileManager.FileDelete(myFile, _FilesPanel);
        }
        internal void DirectoryView_directoryOpenEvent(DiscDirectory myDir)
        {
            fileManager.DirectoryOpen(myDir, _FilesPanel);
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            fileManager.DirectoryBack(_FilesPanel);
        }
        private void btnNewFolder_Click(object sender, RoutedEventArgs e)
        {
            fileManager.CreateFolder(txtNewFolderName.Text,_FilesPanel);
        }
        private void TxtPathSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            else
            {
                e.Handled = true;
                fileManager.SearchPath(txtPathSearch.Text, _FilesPanel);
            }

        }
        private void BtnName_Click(object sender, RoutedEventArgs e)
        {
            fileManager.Name_Click(_FilesPanel);
        }
        private void BtnDate_Click(object sender, RoutedEventArgs e)
        {
            fileManager.Date_Click(_FilesPanel);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(ListBox.SelectedItem is FileView)
            {
                FileView fileView = (FileView)ListBox.SelectedItem;
                FileView_FileDeleteEvent(fileView.MyFile);
            }
            else
            {
                MessageBox.Show("wrong selection");
            }
        }
        private void TxtSearchItem_KeyUp(object sender, KeyEventArgs e)
        {
                e.Handled = true;
                if(txtSearchItem.Text != null)
                {
                    fileManager.SearchItem(txtSearchItem.Text, _FilesPanel);
                }
        }
    }
}
