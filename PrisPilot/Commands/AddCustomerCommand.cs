using PrisPilot.Services.Interfaces;
using PrisPilot.ViewModels;
using System;
using System.Collections.Generic;
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
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is AddCustomerViewModel ACM)
            {
                var c = ACM.CurrentCustomer;
                string message =
                    $"Firmanavn: {c.CompanyName}{Environment.NewLine}" +
                    $"CVR: {c.Cvr}{Environment.NewLine}" +
                    $"Email: {c.Email}{Environment.NewLine}" +
                    $"Telefonnummer: {c.TelephoneNumber}{Environment.NewLine}" +
                    $"adresse: {c.Address}{Environment.NewLine}" +
                    $"Kontaktperson: {c.ContactPerson}{Environment.NewLine}" +
                    $"Timepris: {c.HourlyCost}{Environment.NewLine}" +
                    $"Logo: {ACM.SelectedFilePath}";

                MessageBox.Show(message, "Indtastede oplysninger");
            }
            else
            {
                throw new ArgumentException("Temporary error");
            }
        }
    }
}
