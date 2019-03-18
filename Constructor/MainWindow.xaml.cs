﻿using Constructor.ViewModel;
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

namespace Constructor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainVM ViewModel
        {
            get
            {
                return (MainVM)DataContext;
            }
        }

        private void ClickButton_CreateTextBox(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.Template.State = State.createText;
        }

        private void ClickButton_CreateTable(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.Template.State = State.createTable;
            TableWindow tableWindow = new TableWindow();
            tableWindow.Focusable = true;
            tableWindow.Owner = this;
            tableWindow.Show();            
        }

        private void ClickButton_CreateImage(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.Template.State = State.createImage;
        }

        private void CreateElement_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (ViewModel.Template.State == State.createText)
            {
                Point newLocation = Mouse.GetPosition(constructor);
                ViewModel.CreateTextBox(newLocation);
            }
            else if (ViewModel.Template.State == State.createImage)
            {
                Point newLocation = Mouse.GetPosition(constructor);
                ViewModel.CreateImage(newLocation);
            }
        }

        private void Select(object sender, ExecutedRoutedEventArgs e)
        {
            Point location = Mouse.GetPosition(constructor);
            ViewModel.Select(location);
        }

        private void DeleteSelectedTable(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.DeleteSelectedTable();
        }
    }
}