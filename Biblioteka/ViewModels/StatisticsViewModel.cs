using Biblioteka.Commands;
using Biblioteka.Data;
using Biblioteka.Models;
using Biblioteka.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Biblioteka.ViewModels
{
    public class StatisticsViewModel : ViewModelBase
    {
        private int numberOfReaders;
        private int numberOfBooks;
        private int numberOfActiveReaders;
        private int numberOfTakenBooks;
        private DateTime startDate;
        private DateTime finalDate;
        private List<StatsReader> topReaders;
        private int selectedReader;
        private int selectedTopReadersCategory;
        private int selectedTopBooksCategory;
        private List<StatsBook> topBooks;
        private int selectedBook;

        private ICommand showReaderDetailsCommand;
        private ICommand showBookDetailsCommand;
        private ICommand refreshUserDataCommand;
        private ICommand refreshBookDataCommand;

        public StatisticsViewModel()
        {
            this.startDate = DateTime.Today.AddDays(-30);
            this.finalDate = DateTime.Today;

            this.topReaders = DataPersister.GetTopReaders(this.startDate, this.finalDate, 5);
            this.topBooks = DataPersister.GetTopBooks(this.startDate, this.finalDate, 5);
            this.numberOfReaders = DataPersister.GetReadersCount();
            this.numberOfBooks = DataPersister.GetBooksCount();
            this.numberOfActiveReaders = DataPersister.GetActiveReadersCount(startDate, finalDate);
            this.numberOfTakenBooks = DataPersister.GetAllTakenBooksCountInPeriod(this.startDate, this.finalDate);
        }

        public int NumberOfReaders
        {
            get
            {
                return this.numberOfReaders;
            }
            set
            {
                this.numberOfReaders = value;
                OnPropertyChanged("NumberOfReaders");
            }
        }
        public int NumberOfBooks
        {
            get
            {
                return this.numberOfBooks;
            }
            set
            {
                this.numberOfBooks = value;
                OnPropertyChanged("NumberOfBooks");
            }
        }
        public int NumberOfActiveReaders
        {
            get
            {
                return this.numberOfActiveReaders;
            }
            set
            {
                this.numberOfActiveReaders = value;
                OnPropertyChanged("NumberOfActiveReaders");
            }
        }
        public int NumberOfTakenBooks
        {
            get
            {
                return this.numberOfTakenBooks;
            }
            set
            {
                this.numberOfTakenBooks = value;
                OnPropertyChanged("NumberOfTakenBooks");
            }
        }
        public DateTime StartDate
        {
            get
            {
                return this.startDate;
            }
            set
            {
                this.startDate = value;
                OnPropertyChanged("StartDate");
                UpdateReadersStats();
                UpdateBooksStats();
            }
        }
        public DateTime FinalDate
        {
            get
            {
                return this.finalDate;
            }
            set
            {
                this.finalDate = value;
                OnPropertyChanged("FinalDate");
                UpdateReadersStats();
                UpdateBooksStats();
            }
        }
        public List<StatsReader> TopReaders
        {
            get
            {
                if (this.topReaders == null) this.topReaders = DataPersister.GetTopReaders(this.StartDate, this.FinalDate, this.SelectedTopReadersCategory);
                return this.topReaders;
            }
            set
            {
                this.topReaders = value;
                OnPropertyChanged("TopReaders");
            }
        }
        public int SelectedReader
        {
            get
            {
                return this.selectedReader;
            }
            set
            {
                this.selectedReader = value;
                OnPropertyChanged("SelectedReader");
            }
        }
        public int SelectedTopReadersCategory
        {
            get
            {
                return this.selectedTopReadersCategory;
            }
            set
            {
                this.selectedTopReadersCategory = value;
                OnPropertyChanged("SelectedTopReadersCategory");
                UpdateReadersStats();
            }
        }
        public int SelectedTopBooksCategory
        {
            get
            {
                return this.selectedTopBooksCategory;
            }
            set
            {
                this.selectedTopBooksCategory = value;
                OnPropertyChanged("SelectedTopBooksCategory");
                UpdateBooksStats();
            }
        }
        public List<StatsBook> TopBooks
        {
            get
            {
                if (this.topBooks == null) this.topBooks = DataPersister.GetTopBooks(this.StartDate, this.FinalDate, this.SelectedTopBooksCategory);
                return this.topBooks;
            }
            set
            {
                this.topBooks = value;
                OnPropertyChanged("TopBooks");
            }
        }
        public int SelectedBook
        {
            get
            {
                return this.selectedBook;
            }
            set
            {
                this.selectedBook = value;
                OnPropertyChanged("SelectedBook");
            }
        }

        public ICommand ShowReaderDetailsCommand
        {
            get
            {
                if (this.showReaderDetailsCommand == null)
                {
                    this.showReaderDetailsCommand = new RelayCommand(this.HandleShowUserDetailsCommand);
                }
                return this.showReaderDetailsCommand;
            }
        }
        public ICommand ShowBookDetailsCommand
        {
            get
            {
                if(this.showBookDetailsCommand == null)
                {
                    this.showBookDetailsCommand = new RelayCommand(this.HandleShowBookDetailsCommand);
                }
                return this.showBookDetailsCommand;
            }
        }
        public ICommand RefreshUserDataCommand
        {
            get
            {
                if(this.refreshUserDataCommand == null)
                {
                    this.refreshUserDataCommand = new RelayCommand(this.HandleRefreshUserDataCommand);
                }
                return this.refreshUserDataCommand;
            }
        }
        public ICommand RefreshBookDataCommand
        {
            get
            {
                if (this.refreshBookDataCommand == null)
                {
                    this.refreshBookDataCommand = new RelayCommand(this.HandleRefreshBookDataCommand);
                }
                return this.refreshBookDataCommand;
            }
        }

        private void HandleShowUserDetailsCommand(object parameter)
        {
            if (this.TopReaders.Count == 0)
            {
                MessageBox.Show("Няма налични читатели!", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if (this.TopReaders.Count == 1 && this.SelectedReader == -1) this.SelectedReader = 0;
                else if (this.TopReaders.Count > 1 && this.SelectedReader == -1)
                {
                    MessageBox.Show("Моля изберете читател!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            string egn = this.TopReaders[this.SelectedReader].EGN;
            ReaderDetailsViewModel vm = new ReaderDetailsViewModel(egn);
            var win = new ReaderDetailsPage();
            win.DataContext = vm;
            win.Show();
        }
        private void HandleShowBookDetailsCommand(object parameter)
        {
            if (this.TopBooks.Count == 0)
            {
                MessageBox.Show("Няма налични книги!", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if (this.TopBooks.Count == 1 && this.SelectedBook == -1) this.SelectedBook = 0;
                else if (this.TopBooks.Count > 1 && this.SelectedBook == -1)
                {
                    MessageBox.Show("Моля изберете книга!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            string egn = this.TopBooks[this.SelectedBook].SerialNumber;
            BookDetailsViewModel vm = new BookDetailsViewModel(egn);
            var win = new BookDetailsPage();
            win.DataContext = vm;
            win.Show();
        }
        private void HandleRefreshUserDataCommand(object parameter)
        {
            this.NumberOfReaders = DataPersister.GetReadersCount();
            UpdateReadersStats();
        }
        private void HandleRefreshBookDataCommand(object parameter)
        {
            this.NumberOfBooks = DataPersister.GetBooksCount();
            UpdateBooksStats();
        }

        
        private void UpdateReadersStats()
        {
            this.NumberOfActiveReaders = DataPersister.GetActiveReadersCount(this.StartDate, this.FinalDate);
            int count;
            if (this.SelectedTopReadersCategory == 0) count = 5;
            else if (this.SelectedTopReadersCategory == 1) count = 10;
            else if (this.SelectedTopReadersCategory == 2) count = 20;
            else count = 50;
            this.TopReaders = DataPersister.GetTopReaders(this.StartDate, this.FinalDate, count);
        }
        private void UpdateBooksStats()
        {
            this.NumberOfTakenBooks = DataPersister.GetAllTakenBooksCountInPeriod(this.StartDate, this.FinalDate); 
            int count;
            if (this.SelectedTopBooksCategory == 0) count = 5;
            else if (this.SelectedTopBooksCategory == 1) count = 10;
            else if (this.SelectedTopBooksCategory == 2) count = 20;
            else count = 50;
            this.TopBooks = DataPersister.GetTopBooks(this.StartDate, this.FinalDate, count);
        }
    }
}
