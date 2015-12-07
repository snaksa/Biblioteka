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
    public class ArchiveBookViewModel : ViewModelBase
    {
        private string archiveNumber;
        private DateTime date;
        private bool windowClosedManually = true;

        ICommand archiveBookCommand;
        ICommand closeWindowCommand;

        public ArchiveBookViewModel()
            : this("", DateTime.Today) { }

        public ArchiveBookViewModel(string number, DateTime date)
        {
            this.ArchiveNumber = number;
            this.DateOfArchivation = date;
        }

        public string ArchiveNumber
        {
            get
            {
                return this.archiveNumber;
            }
            set
            {
                this.archiveNumber = value;
                OnPropertyChanged("ArchiveNumber");
            }
        }
        public DateTime DateOfArchivation
        {
            get
            {
                return this.date;
            }
            set
            {
                this.date = value;
                OnPropertyChanged("DateOfArchivation");
            }
        }
        public bool WindowClosedManually
        {
            get
            {
                return this.windowClosedManually;
            }
            set
            {
                this.windowClosedManually = value;
                OnPropertyChanged("WindowClosedManually");
            }
        }

        public ICommand ArchiveBookCommand
        {
            get
            {
                if (this.archiveBookCommand == null)
                {
                    this.archiveBookCommand = new RelayCommand(this.HandleArchiveBookCommand);
                }
                return this.archiveBookCommand;
            }
        }
        public ICommand CloseWindowCommand
        {
            get
            {
                if (this.closeWindowCommand == null)
                {
                    this.closeWindowCommand = new RelayCommand(this.HandleCloseWindowCommand);
                }
                return this.closeWindowCommand;
            }
        }

        private void HandleArchiveBookCommand(object parameter)
        {
            bool mistakeFound = false;
            StringBuilder strBuilder = new StringBuilder();
            if(!DataValidator.isValidInteger(this.ArchiveNumber))
            {
                mistakeFound = true;
                strBuilder.Append("Моля въведете валиден номер!\n");
            }
            else if (DataPersister.ArchivedBooksContainArchiveNumber(this.ArchiveNumber))
            {
                mistakeFound = true;
                strBuilder.Append("Вече има книга с този архивен номер!\n");
            }

            if (mistakeFound == true)
            {
                MessageBox.Show(strBuilder.ToString(), "Некоректни данни", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            this.WindowClosedManually = false;
            var win = parameter as Window;
            win.Close();
        }
        private void HandleCloseWindowCommand(object parameter)
        {
            this.WindowClosedManually = true;
            var win = parameter as Window;
            win.Close();
        }
    }
}
