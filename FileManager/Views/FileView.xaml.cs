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

namespace TotalCommanderProjectV2.Views
{
    public partial class FileView : UserControl
    {
        // pola
        DiscFile myFile;

        public DiscFile MyFile { get => myFile; set => myFile = value; }

        // konstruktor
        public FileView(DiscFile MyFile)
        {
            InitializeComponent();
            this.myFile = MyFile;
            FirstTxt.Text = myFile.GetName();
            SecondTxt.Text = myFile.GetCreationDate().ToLongDateString();
        }

        // eventy
        public delegate void FileWasDelete(DiscFile myFile);
        public event FileWasDelete FileDeleteEvent;

        public delegate void FileWasOpen(DiscFile myFile);
        public event FileWasOpen FileOpenEvent;

        // metody
        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(FileOpenEvent != null)
            {
                FileOpenEvent.Invoke(myFile);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (FileDeleteEvent != null)
            {
                FileDeleteEvent.Invoke(myFile);
            }
        }
    }
}
