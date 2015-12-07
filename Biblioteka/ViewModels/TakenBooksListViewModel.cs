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
    public class TakenBooksListViewModel : ViewModelBase
    {
        private List<TakenBookRecord> takenBooks;
        private int selectedRecord;
        private bool enableButtons = true;
        private int numberOfAllTakenBooks;
        private int numberOfExpiredBooks;

        private ICommand changeListDataCommand;
        private ICommand showBookDetailsCommand;
        private ICommand showReaderDetailsCommand;

        public TakenBooksListViewModel()
        {
            this.numberOfAllTakenBooks = DataPersister.GetAllTakenBooks().Count;
            this.numberOfExpiredBooks = DataPersister.GetAllExpiredTakenBooks().Count;
        }

        public List<TakenBookRecord> TakenBooks
        {
            get
            {
                if (this.takenBooks == null)
                {
                    this.takenBooks = DataPersister.GetAllTakenBooks();
                }
                if (takenBooks.Count == 0) this.EnableButtons = false;
                return this.takenBooks;
            }
            set
            {
                this.takenBooks = value;
                OnPropertyChanged("TakenBooks");
            }
        }
        public int NumberOfAllTakenBooks
        {
            get
            {
                return numberOfAllTakenBooks;
            }
            set
            {
                this.numberOfAllTakenBooks = value;
                OnPropertyChanged("NumberOfAllTakenBooks");
            }
        }
        public int NumberOfAllExpiredBooks
        {
            get
            {
                return this.numberOfExpiredBooks;
            }
            set
            {
                this.numberOfExpiredBooks = value;
                OnPropertyChanged("NumberOfAllExpiredBooks");
            }
        }
        public int SelectedRecord
        {
            get
            {
                return this.selectedRecord;
            }
            set
            {
                this.selectedRecord = value;
                OnPropertyChanged("SelectedRecord");
            }
        }
        public bool EnableButtons
        {
            get
            {
                return this.enableButtons;
            }
            set
            {
                this.enableButtons = value;
                OnPropertyChanged("EnableButtons");
            }
        }
        public ICommand ChangeListDataCommand
        {
            get
            {
                if(this.changeListDataCommand == null)
                {
                    this.changeListDataCommand = new RelayCommand(this.HandleChangeListDataCommand);
                }
                return this.changeListDataCommand;
            }
        }
        public ICommand ShowBookDetailsCommand
        {
            get
            {
                if (this.showBookDetailsCommand == null)
                {
                    this.showBookDetailsCommand = new RelayCommand(this.HandleShowBookDetailsCommand);
                }
                return this.showBookDetailsCommand;
            }
        }
        public ICommand ShowReaderDetailsCommand
        {
            get
            {
                if (this.showReaderDetailsCommand == null)
                {
                    this.showReaderDetailsCommand = new RelayCommand(this.HandleShowReaderDetailsCommand);
                }
                return this.showReaderDetailsCommand;
            }
        }

        
        private void HandleChangeListDataCommand(object parameter)
        {
            try
            {
                string key = parameter as string;
                if (key.ToUpper().Equals("AllTakenBooks".ToUpper()))
                {
                    this.TakenBooks = DataPersister.GetAllTakenBooks(); 
                    this.NumberOfAllTakenBooks = this.TakenBooks.Count;
                }
                else
                {
                    this.TakenBooks = DataPersister.GetAllExpiredTakenBooks();
                    this.NumberOfAllExpiredBooks = this.TakenBooks.Count;
                }

                if (this.TakenBooks.Count == 0) this.EnableButtons = false;
                else this.EnableButtons = true;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Изникна проблем. Моля затворете базата данни и опитайте отново.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void HandleShowBookDetailsCommand(object parameter)
        {
            int pos = this.SelectedRecord;
            if (pos == -1 && this.TakenBooks.Count > 1)
            {
                MessageBox.Show("Моля изберете книга!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (this.TakenBooks.Count == 0)
            {
                MessageBox.Show("Няма налични книги!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (this.TakenBooks.Count == 1) this.SelectedRecord = 0;

            pos = this.SelectedRecord;
            string serial = this.TakenBooks[pos].SerialNumber;

            BookDetailsViewModel vm = new BookDetailsViewModel(serial);
            var win = new BookDetailsPage();
            win.DataContext = vm;
            win.Show();
        }
        private void HandleShowReaderDetailsCommand(object parameter)
        {
            int pos = this.SelectedRecord;
            if (pos == -1 && this.TakenBooks.Count > 1)
            {
                MessageBox.Show("Моля изберете читател!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (this.TakenBooks.Count == 0)
            {
                MessageBox.Show("Няма налични читатели!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (this.TakenBooks.Count == 1) this.SelectedRecord = 0;

            pos = this.SelectedRecord;
            string egn = this.TakenBooks[pos].ReaderEGN;

            ReaderDetailsViewModel vm = new ReaderDetailsViewModel(egn);
            var win = new ReaderDetailsPage();
            win.DataContext = vm;
            win.Show();
        }
    }
}
