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
    public partial class DirectoryView : UserControl
    {
        // pola
        DiscDirectory myDir;

        // konstruktor
        public DirectoryView(DiscDirectory MyDir)
        {
            InitializeComponent();
            this.myDir = MyDir;
            FirstTxt.Text = myDir.GetName();
            SecondTxt.Text = myDir.GetCreationDate().ToLongDateString();
        }

        // eventy
        public delegate void DirectoryWasOpened(DiscDirectory myDir);
        public event DirectoryWasOpened directoryOpenEvent;

        // metody
        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (directoryOpenEvent != null)
            {
                directoryOpenEvent.Invoke(myDir);
            }
        }
    }
}
