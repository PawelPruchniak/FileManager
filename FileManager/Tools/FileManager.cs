using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using TotalCommanderProjectV2.DataModels;
using TotalCommanderProjectV2.Views;
using WpfApp7.Views.PreviewsWindows;

namespace TotalCommanderProjectV2.Tolls
{
    public class FileManager
    {
        // pola
        private int NamebtnMode = 0;
        private int DatebtnMode = 0;
        bool imageOrTextIsOpen = false;

        // metody
        internal void CopyItem(Object obj, FilesPanel filesPanel)
        {
            if (obj is FileView)
            {
                FileView fileView = (FileView)obj;
                DiscElement discElement = fileView.MyFile;
                try
                {
                    string file_path = discElement.GetPath();
                    string file_name = discElement.GetName();
                    if (File.Exists(filesPanel.CurrentLocation + @"\" + file_name))
                    {
                        MessageBox.Show("File already exists");
                    }
                    else
                    {
                        File.Copy(file_path, filesPanel.CurrentLocation + @"\" + file_name);
                        filesPanel.RefreshPath(filesPanel.CurrentLocation);
                    }
                }
                catch (Exception exe)
                {
                    MessageBox.Show("error: " + exe);
                }
            }
            else
            {
                MessageBox.Show("You can copy only a file");
            }
            
        }
        internal void CreateFolder(string newFolderName, FilesPanel filesPanel)
        {
            if (newFolderName != null)
            {
                if (filesPanel.CurrentLocation != null)
                {
                    string folder_path = filesPanel.CurrentLocation + @"\" + newFolderName;
                    if (!Directory.Exists(folder_path))
                    {
                        Directory.CreateDirectory(folder_path);
                        MessageBox.Show("Folder Created");
                        filesPanel.RefreshPath(filesPanel.CurrentLocation);
                    }
                    else
                    {
                        MessageBox.Show("Folder already exists");
                    }
                }
            }
        }
        internal void DirectoryBack(FilesPanel filesPanel)
        {
            if (imageOrTextIsOpen == true)
            {
                filesPanel.ListBox.Items.Clear();
                filesPanel.RefreshPath(filesPanel.CurrentLocation);
                imageOrTextIsOpen = false;
            }
            else
            {
                try
                {
                    DiscDirectory discDirectory = new DiscDirectory(filesPanel.CurrentLocation);
                    int index = discDirectory.GetPath().LastIndexOf(@"\");
                    string path = discDirectory.GetPath().Substring(0, index);
                    filesPanel.CurrentLocation = path;
                    filesPanel.RefreshPath(filesPanel.CurrentLocation);
                    NamebtnMode = 0;
                    Name_Click(filesPanel);
                }
                catch (Exception exe)
                {
                    MessageBox.Show("error: " + exe.ToString());
                }
            }
        }
        internal void DirectoryOpen(DiscDirectory discDirectory,FilesPanel filesPanel)
        {
            filesPanel.CurrentLocation = discDirectory.GetPath();
            filesPanel.RefreshPath(filesPanel.CurrentLocation);
            NamebtnMode = 0;
            Name_Click(filesPanel);
        }
        internal void FileDelete(DiscFile discFile, FilesPanel filesPanel)
        {
            try
            {
                File.Delete(discFile.GetPath());
                filesPanel.RefreshPath(filesPanel.CurrentLocation);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        internal void FileOpen(DiscFile discFile, FilesPanel filesPanel)
        {
            string extension = System.IO.Path.GetExtension(discFile.GetPath()) ;
            if (extension == ".txt")
            {
                //Process.Start(discFile.GetPath());
                TextView textView = new TextView();

                string path = discFile.GetPath();
                string filetext = File.ReadAllText(path);

                byte[] bytes = Encoding.Unicode.GetBytes(filetext);
                filetext = Encoding.Unicode.GetString(bytes);

                textView.RichTextBox1.AppendText(filetext);

                filesPanel.ListBox.Items.Clear();
                filesPanel.ListBox.Items.Add(textView);
                imageOrTextIsOpen = true;
                
            }
            else if( extension == ".png" || extension == ".jpg")
            {
                ImageView image = new ImageView();
                image.xPicture1.Source = new BitmapImage(new Uri(discFile.GetPath().ToString(), UriKind.RelativeOrAbsolute));

                filesPanel.ListBox.Items.Clear();
                filesPanel.ListBox.Items.Add(image);
                imageOrTextIsOpen = true;
            }
            else
            {
                Process.Start(discFile.GetPath());
            }
        }
        internal void SearchPath(string newPath, FilesPanel  filesPanel)
        {
            if(Directory.Exists(newPath))
            {
                filesPanel.CurrentLocation = newPath;
                filesPanel.RefreshPath(filesPanel.CurrentLocation);
            }
            else
            {
                MessageBox.Show("Wrong Path!");
            }
        }
        internal void Name_Click(FilesPanel filesPanel)
        {
            if (DatebtnMode != 0)
            {
                DatebtnMode = 0;
                filesPanel.btnDate.Content = "Date";
            }
            if (NamebtnMode == 0)
            {
                NamebtnMode = 1;
            }
            else
            {
                if (NamebtnMode == 1)
                {
                    NamebtnMode = 2;
                }
                else
                {
                    NamebtnMode = 1;
                }
            }
            switch (NamebtnMode)
            {
                case 1:
                    filesPanel.btnName.Content = "▼ Name";
                    filesPanel.RefreshPath(filesPanel.CurrentLocation);
                    break;
                case 2:
                    filesPanel.btnName.Content = "▲ Name";
                    Sort(filesPanel, "name");
                    break;
            }
        }
        internal void Date_Click(FilesPanel filesPanel)
        {
            if (NamebtnMode != 0)
            {
                NamebtnMode = 0;
                filesPanel.btnName.Content = "Name";
            }
            if (DatebtnMode == 0)
            {
                DatebtnMode = 1;
            }
            else
            {
                if (DatebtnMode == 1)
                {
                    DatebtnMode = 2;
                }
                else
                {
                    DatebtnMode = 1;
                }
            }
            switch (DatebtnMode)
            {
                case 1:
                    filesPanel.btnDate.Content = "▼ Date";
                    Sort(filesPanel, "date");
                    break;
                case 2:
                    filesPanel.btnDate.Content = "▲ Date";
                    Sort(filesPanel, "date");
                    break;
            }
        }
        private void Sort(FilesPanel filesPanel, string type)
        {
            DiscDirectory directory = new DiscDirectory(filesPanel.CurrentLocation);

            List<DiscElement> listAll = new List<DiscElement>();
            List<DiscElement> listDirectory = new List<DiscElement>();
            List<DiscElement> listFile = new List<DiscElement>();

            listAll.AddRange(directory.GetAllFiles());
            filesPanel.ListBox.Items.Clear();

            foreach (DiscElement discElement in listAll)
            {
                if (discElement is DiscDirectory)
                {
                    listDirectory.Add(discElement);
                }
                else
                {
                    listFile.Add(discElement);
                }
            }

            if (type == "date")
            {
                if (DatebtnMode == 1)
                {
                    listDirectory = listDirectory.OrderBy(o => o.GetCreationDate()).ToList();
                    listFile = listFile.OrderBy(o => o.GetCreationDate()).ToList();
                }
                else
                {
                    listDirectory = listDirectory.OrderByDescending(o => o.GetCreationDate()).ToList();
                    listFile = listFile.OrderByDescending(o => o.GetCreationDate()).ToList();
                }
            }
            else
            {
                listDirectory = listDirectory.OrderByDescending(o => o.GetName()).ToList();
                listFile = listFile.OrderByDescending(o => o.GetName()).ToList();
            }

            foreach (DiscElement discElement in listDirectory)
            {
                DirectoryView directoryView = new DirectoryView(discElement as DiscDirectory);
                filesPanel.ListBox.Items.Add(directoryView);
                directoryView.directoryOpenEvent += filesPanel.DirectoryView_directoryOpenEvent;
            }
            foreach (DiscElement discElement in listFile)
            {
                FileView fileView = new FileView(discElement as DiscFile);
                filesPanel.ListBox.Items.Add(fileView);
                fileView.FileDeleteEvent += filesPanel.FileView_FileDeleteEvent;
                fileView.FileOpenEvent += filesPanel.FileView_FileOpenEvent;
            }
        }
        internal void SaveTextFile(TextView textView)
        {
            /*
            TextRange range;
            FileStream fStream;
            range = new TextRange(textView.RichTextBox1.Document.ContentStart, textView.RichTextBox1.Document.ContentEnd);
            fStream = new FileStream(textView.FLOW123.Name, FileMode.Create);
            range.Save(fStream, DataFormats.XamlPackage);
            fStream.Close();
            */
        }
        internal void SearchItem(string _itemPath, FilesPanel filesPanel)
        {
            DiscDirectory directory = new DiscDirectory(filesPanel.CurrentLocation);
            List<DiscElement> discElements = new List<DiscElement>();
            discElements.AddRange(directory.GetAllFiles());

            //filesStack.Children.Clear();
            filesPanel.ListBox.Items.Clear();

            foreach (DiscElement discElement in discElements)
            {
                if(discElement.GetName().ToLower().Contains(_itemPath.ToLower()))
                {
                    if (discElement is DiscFile)
                    {
                        FileView fileView = new FileView(discElement as DiscFile);
                        filesPanel.ListBox.Items.Add(fileView);
                        fileView.FileDeleteEvent += filesPanel.FileView_FileDeleteEvent;
                        fileView.FileOpenEvent += filesPanel.FileView_FileOpenEvent;
                    }
                    else
                    {
                        DirectoryView directoryView = new DirectoryView(discElement as DiscDirectory);
                        filesPanel.ListBox.Items.Add(directoryView);
                        directoryView.directoryOpenEvent += filesPanel.DirectoryView_directoryOpenEvent;
                    }
                }
            }
        }
    }
}
