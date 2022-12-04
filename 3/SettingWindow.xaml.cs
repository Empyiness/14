using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _3
{
    /// <summary>
    /// Логика взаимодействия для SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            if(!int.TryParse(RowCountBox.Text, out int rowCount) || !int.TryParse(ColumnCountBox.Text, out int columnCount)) 
            {
                MessageBox.Show("Введите числа!");
                return;
            }
            BufferClass.RowCount = rowCount;
            BufferClass.ColumnCount = columnCount; StreamWriter fileSaveArray = new StreamWriter(@".\config.ini");
            fileSaveArray.WriteLine(rowCount);
            fileSaveArray.WriteLine(columnCount);
            fileSaveArray.Close();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RowCountBox.Text = BufferClass.RowCount.ToString();
            ColumnCountBox.Text = BufferClass.ColumnCount.ToString();
        }
    }
}
