using Biblioteka.Commands;
using Biblioteka.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Biblioteka.ViewModels
{
    public class TextInputViewModel : ViewModelBase
    {
        private string inputText;
        private bool cancelled = true;
        private string title;
        private ICommand okCommand;
        private ICommand cancelCommand;

        public string InputText
        {
            get
            {
                return this.inputText;
            }
            set
            {
                this.inputText = value;
                OnPropertyChanged("InputText");
            }
        }
        public bool Cancelled
        {
            get
            {
                return this.cancelled;
            }
            set
            {
                this.cancelled = value;
                OnPropertyChanged("Cancelled");
            }
        }
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
                OnPropertyChanged("Title");
            }
        }
        public ICommand OkCommand
        {
            get
            {
                if (this.okCommand == null)
                {
                    this.okCommand = new RelayCommand(this.HandleOkCommand);
                }
                return this.okCommand;
            }
        }
        public ICommand CancelCommand
        {
            get
            {
                if (this.cancelCommand == null)
                {
                    this.cancelCommand = new RelayCommand(this.HandleCancelCommand);
                }
                return this.cancelCommand;
            }
        }

        public TextInputViewModel(string title)
        {
            this.title = title;
        }

        private void HandleOkCommand(object parameter)
        {
            if (!DataValidator.IsValidString(this.InputText))
            {
                MessageBox.Show("Моля въведете валидна стойност в полето!", "Некоректни данни", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            this.Cancelled = false;
            var win = parameter as Window;
            win.Close();
        }
        private void HandleCancelCommand(object parameter)
        {
            this.Cancelled = true;
            var win = parameter as Window;
            win.Close();
        }
    }
}
