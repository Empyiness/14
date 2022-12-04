using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibMatrix;
using Lib_12;
using System.Windows.Threading;
using System.IO;

namespace _3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Matrix<int> matrix;
        public MainWindow()
        {
            InitializeComponent();
            matrix = new Matrix<int>();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorization = new AuthorizationWindow();
            authorization.Owner = this;
            authorization.ShowDialog();
            try
            {
                StreamReader fileOpenMatrix = new StreamReader(@".\config.ini");
                BufferClass.RowCount = Convert.ToInt32(fileOpenMatrix.ReadLine());
                BufferClass.ColumnCount = Convert.ToInt32(fileOpenMatrix.ReadLine());
                fileOpenMatrix.Close();
            }
            catch
            {
                BufferClass.RowCount = 5;
                BufferClass.ColumnCount = 4;
            }
            matrix = new Matrix<int>(BufferClass.RowCount, BufferClass.ColumnCount);
            matrix.Generate();
            DataGridMatrix.ItemsSource = VisualArray.ToDataTable(matrix).DefaultView;
            SizeBlock.Text = matrix.Rows + "x" + matrix.Columns;
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            int rowcount = BufferClass.RowCount;
            int columncount = BufferClass.ColumnCount;
            if (RowBox.Text != "")
            {
                if (!Int32.TryParse(RowBox.Text, out rowcount))
                {
                    MessageBox.Show("Введите число!");
                    RowBox.Clear();
                    RowBox.Focus();
                    return;
                }
            }
            if (ColumnBox.Text != "")
            {
                if (!Int32.TryParse(ColumnBox.Text, out columncount))
                {
                    MessageBox.Show("Введите число!");
                    ColumnBox.Clear();
                    ColumnBox.Focus();
                    return;
                }
            }
            matrix = new Matrix<int>(rowcount, columncount);
            matrix.Generate();
            DataGridMatrix.ItemsSource = VisualArray.ToDataTable(matrix).DefaultView;
            SizeBlock.Text = matrix.Rows + "x" + matrix.Columns;
        }
        private void Calculate(object sender, RoutedEventArgs e)
        {
            if (!Int32.TryParse(ColumnNumberBox.Text, out int numberOfColumn))
            {
                MessageBox.Show("Введите номер столбца!!");
                ColumnNumberBox.Clear();
                ColumnNumberBox.Focus();
                return;
            }
            SumBox.Text = matrix.Sum(numberOfColumn).ToString();
            MultyBox.Text = matrix.Multiply(numberOfColumn).ToString();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            SumBox.Clear();
            MultyBox.Clear();
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            string path = @".\matrix" + matrix.Extension;
            matrix.Save(path);
            DataGridMatrix.ItemsSource = VisualArray.ToDataTable(matrix).DefaultView;
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            string path = @".\matrix" + matrix.Extension;
            matrix.Load(path);
            DataGridMatrix.ItemsSource = VisualArray.ToDataTable(matrix).DefaultView;
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            matrix.DefaultInit();
            DataGridMatrix.ItemsSource = VisualArray.ToDataTable(matrix).DefaultView;
        }
        private void Setting(object sender, RoutedEventArgs e)
        {
            BufferClass.RowCount = matrix.Rows;
            BufferClass.ColumnCount = matrix.Columns;
            SettingWindow setting = new SettingWindow();
            setting.Owner = this;
            setting.ShowDialog();
            if (BufferClass.RowCount > matrix.Rows || BufferClass.ColumnCount > matrix.Columns)
            {
                matrix = new Matrix<int>(BufferClass.RowCount, BufferClass.ColumnCount);
                matrix.Generate();
                DataGridMatrix.ItemsSource = VisualArray.ToDataTable(matrix).DefaultView;
                SizeBlock.Text = matrix.Rows + "x" + matrix.Columns;
                return;
            }
            matrix.Rows = BufferClass.RowCount;
            matrix.Columns = BufferClass.ColumnCount;
            DataGridMatrix.ItemsSource = VisualArray.ToDataTable(matrix).DefaultView;
            SizeBlock.Text = matrix.Rows + "x" + matrix.Columns;
        }

        private void About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Дана матрица размера M * N и целое число K (1 < K < N). Найти сумму и\r\nпроизведение элементов K-го столбца данной матрицы.");
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SelectedItem(object sender, DataGridCellEditEndingEventArgs e)
        {
            ItemBlock.Text = e.Row.GetIndex() + "x" + e.Column.DisplayIndex;
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(MessageBox.Show("Вы уверены?", "Выход", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
