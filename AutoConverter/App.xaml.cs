﻿using AutoConverter.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AutoConverter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                //var mainView = new MainWindow();
                //mainView.Show();
                //mainView.DataContext = new MainViewModel();
            }
            catch (Exception)
            {
                // Do some exception handling
            }
        }
    }
}
