using Biblioteka.Commands;
using Biblioteka.Data;
using Biblioteka.Models;
using Biblioteka.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Biblioteka.ViewModels
{
    public class TakeBookViewModel : ViewModelBase
    {
        private string readerEgn = "";
        private string readerName;
        private string bookSerialNumber;
        private string bookTitle;
        private DateTime dateOfTaking;
        private DateTime deadline;
        private SearchStudentViewModel searchStudentViewModel;
        private SearchBookViewModel searchBookViewModel;

        ICommand searchReaderCommand;
        ICommand searchBookCommand;
        ICommand closeWindowCommand;
        ICommand saveCommand;

        public TakeBookViewModel(string data, string name, bool isEGN)
        {
            searchStudentViewModel = new SearchStudentViewModel() { ChooseVisibility = Visibility.Visible };
            searchBookViewModel = new SearchBookViewModel() { ShowChooseButton = Visibility.Visible };
            if (isEGN)
            {
                this.ReaderEGN = data;
                this.ReaderName = name;
                this.BookSerialNumber = "";
            }
            else
            {
                this.BookSerialNumber = data;
                this.readerEgn = "";
                this.ReaderName = "";
            }
            this.DateOfTaking = DateTime.Today.Date;
            this.Deadline = DateTime.Today.Date.AddDays(20);
        }

        public string ReaderEGN
        {
            get
            {
                return this.readerEgn;
            }
            set
            {
                this.readerEgn = value;
                OnPropertyChanged("ReaderEGN");
            }
        }
        public string ReaderName
        {
            get
            {
                return this.readerName;
            }
            set
            {
                this.readerName = value;
                OnPropertyChanged("ReaderName");
            }
        }
        public string BookSerialNumber
        {
            get
            {
                return this.bookSerialNumber;
            }
            set
            {
                this.bookSerialNumber = value;
                OnPropertyChanged("BookSerialNumber");
            }
        }
        public string BookTitle
        {
            get
            {
                return this.bookTitle;
            }
            set
            {
                this.bookTitle = value;
                OnPropertyChanged("BookTitle");
            }
        }
        public DateTime DateOfTaking
        {
            get
            {
                return this.dateOfTaking;
            }
            set
            {
                this.dateOfTaking = value;
                OnPropertyChanged("DateOfTaking");
            }
        }
        public DateTime Deadline
        {
            get
            {
                return this.deadline;
            }
            set
            {
                this.deadline = value;
                OnPropertyChanged("Deadline");
            }
        }

        public string ChoosenReaderEGN
        {
            get
            {
                return this.searchStudentViewModel.ChoosenReaderEGN;
            }
        }
        public string ChoosenReaderName
        {
            get
            {
                return this.searchStudentViewModel.ChoosenReaderName;
            }
        }
        public string ChoosenBookSerialNumber
        {
            get
            {
                return this.searchBookViewModel.ChoosenSerialNumber;
            }
        }
        public string ChoosenBookTitle
        {
            get
            {
                return this.searchBookViewModel.ChoosenTitle;
            }
        }

        
        public ICommand SearchReaderCommand
        {
            get
            {
                if (this.searchReaderCommand == null)
                {
                    this.searchReaderCommand = new RelayCommand(this.HandleSearchReaderCommand);
                }
                return this.searchReaderCommand;
            }
        }
        public ICommand SearchBookCommand
        {
            get
            {
                if (this.searchBookCommand == null)
                {
                    this.searchBookCommand = new RelayCommand(this.HandleSearchBookCommand);
                }
                return this.searchBookCommand;
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
        public ICommand SaveCommand
        {
            get
            {
                if (this.saveCommand == null)
                {
                    this.saveCommand = new RelayCommand(this.HandleSaveCommand);
                }
                return this.saveCommand;
            }
        }


        private void HandleSearchReaderCommand(object parameter)
        {
            var win = new SearchStudentPage();
            win.DataContext = this.searchStudentViewModel;
            win.ShowDialog();
            this.ReaderEGN = this.ChoosenReaderEGN;
            this.ReaderName = this.ChoosenReaderName;
        }
        private void HandleSearchBookCommand(object parameter)
        {
            var win = new SearchBookPage();
            win.DataContext = this.searchBookViewModel;
            win.ShowDialog();
            this.BookSerialNumber = this.ChoosenBookSerialNumber;
            this.BookTitle = this.ChoosenBookTitle;
        }

        private void HandleCloseWindowCommand(object parameter)
        {
            var win = parameter as Window;
            win.Close();
        }

        private void HandleSaveCommand(object parameter)
        {
            string egn = this.ReaderEGN;
            string bookSerial = this.BookSerialNumber;
            DateTime d1 = this.DateOfTaking;
            DateTime d2 = this.Deadline;

            bool mistakeFound = false;
            StringBuilder strBuilder = new StringBuilder();

            if (!DataValidator.isValidInteger(bookSerial))
            {
                mistakeFound = true;
                strBuilder.Append("Моля въведете валиден номер на книга!\n");
            }
            else if (DataPersister.BookIsTaken(bookSerial))
            {
                mistakeFound = true;
                strBuilder.Append("Книгата с този номер е вече взета!!\n");
            }
            else if (!DataPersister.DatabaseContainsSerialNumber(bookSerial, "Books"))
            {
                mistakeFound = true;
                strBuilder.Append("Не съществува книга с този номер!\n");
            }

            if(!DataValidator.IsValidString(egn))
            {
                mistakeFound = true;
                strBuilder.Append("Моля въведете валидно ЕГН на читател!\n");
            }
            else if(!DataPersister.DatabaseContainsEGN(egn))
            {
                mistakeFound = true;
                strBuilder.Append("Не съществува читател с това ЕГН!\n");
            }
            

            if (mistakeFound)
            {
                MessageBox.Show(strBuilder.ToString(), "Некоректни данни", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                try
                {
                    TakenBookRecord record = new TakenBookRecord(0, egn, bookSerial, d1, d2);
                    DataInserter.TakeBook(record);
                    MessageBox.Show("Книгата е взета успешно!", "Некоректни данни", MessageBoxButton.OK, MessageBoxImage.Information);
                    var win = parameter as Window;
                    win.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Изникна проблем. Моля затворете базата данни и опитайте отново.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
