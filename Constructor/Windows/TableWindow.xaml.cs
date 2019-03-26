using Constructor.ViewModel;
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
using System.Windows.Shapes;

namespace Constructor.Windows
{
    public partial class TableWindow : Window
    {
        public TableWindow()
        {
            InitializeComponent();
        }

        private void ClickButton_Cancel(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void ClickButton_Send(object sender, ExecutedRoutedEventArgs e)
        {
            ((MainVM)Owner.DataContext).CreateTable(new Point(0, 0), int.Parse(CountColumn.Text), int.Parse(CountRow.Text));
            Close();
        }

        private void CountColumn_TextChanged(object sender, TextChangedEventArgs e)
        {
            //try
            //{
            //    if (int.Parse(CountColumn.Text) > 30)
            //    {
            //        CountColumn.Text = "30";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Ввод только для чисел");
            //}
        }

        private void CountRow_TextChanged(object sender, TextChangedEventArgs e)
        {
            //try
            //{
            //    if (int.Parse(CountRow.Text) > 30)
            //    {
            //        CountRow.Text = "30";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Ввод только для чисел");
            //}
        }
    }
}
