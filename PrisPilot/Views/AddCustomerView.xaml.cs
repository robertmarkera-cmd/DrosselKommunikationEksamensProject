using PrisPilot.Services;
using PrisPilot.ViewModels;
using PrisPilot.Services.Interfaces;
using System;
using System.Collections.Generic;
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

namespace PrisPilot.Views
{
    /// <summary>
    /// Interaction logic for AddCustomerView.xaml
    /// </summary>
    public partial class AddCustomerView : UserControl
    {
        private readonly AddCustomerViewModel ACM;

        public AddCustomerView()
        {
            InitializeComponent();
            ACM = new AddCustomerViewModel();
            DataContext = ACM;
        }
    }
}
