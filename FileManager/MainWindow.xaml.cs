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
using TotalCommanderProjectV2.Views;

namespace TotalCommanderProjectV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // pola
        FileManager fileManager;

        // konstruktor
        public MainWindow()
        {
            this.fileManager = new FileManager();
            InitializeComponent();
        }

        //events
        private void LeftPanel_GotFocus(object sender, RoutedEventArgs e)
        {
           if (rightPanel._FilesPanel.ListBox.SelectedItems.Count > 0)
            {
                rightPanel._FilesPanel.ListBox.UnselectAll();
            }
        }
        private void RightPanel_GotFocus(object sender, RoutedEventArgs e)
        {
            if (leftPanel._FilesPanel.ListBox.SelectedItems.Count > 0)
            {
                leftPanel._FilesPanel.ListBox.UnselectAll();
            }
        }
        // metody
        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.F1) return;

            e.Handled = true;
            if (rightPanel._FilesPanel.ListBox.SelectedItems.Count > 0)
            {
               fileManager.CopyItem(rightPanel._FilesPanel.ListBox.SelectedItem, leftPanel._FilesPanel);
            }
            else if(leftPanel._FilesPanel.ListBox.SelectedItems.Count > 0)
            {
               fileManager.CopyItem(leftPanel._FilesPanel.ListBox.SelectedItem, rightPanel._FilesPanel);
            }
        }
    }
}
