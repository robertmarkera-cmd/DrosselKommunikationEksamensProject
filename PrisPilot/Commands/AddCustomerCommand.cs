using PrisPilot.Services.Interfaces;
using PrisPilot.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PrisPilot.Commands
{
    public class AddCustomerCommand : ICommand
    {
        
        public AddCustomerCommand()
        {
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            bool result = false;
            if (parameter is AddCustomerViewModel ACM)
            {
                result = ACM.CurrentCustomer.IsCustomerValid();
            }
            return result;
        }

        public void Execute(object? parameter)
        {
            if (parameter is AddCustomerViewModel ACM)
            {
                CustomerViewModel c = ACM.CurrentCustomer;
                string message =

                    "Er de indtastede oplysninger korrekte?\n\n" +
                    $"Firmanavn: {c.CompanyName}\n" +
                    $"CVR: {c.Cvr}\n" +
                    $"Email: {c.Email}\n" +
                    $"Telefonnummer: {c.TelephoneNumber}\n" +
                    $"adresse: {c.Address}\n" +
                    $"Kontaktperson: {c.ContactPerson}\n" +
                    $"Logo: {ACM.SelectedFilePath}";

                MessageBoxResult result = MessageBox.Show(message, "Indtastede oplysninger", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    ACM.AddToRepo();
                }
            }
            else
            {
                throw new ArgumentException("Temporary error");
            }
        }
    }
}
