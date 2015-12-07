using Biblioteka.Commands;
using Biblioteka.Data;
using Biblioteka.HelperClasses;
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
    public class BookDetailsViewModel : ViewModelBase
    {
        private string serialNumber;
        private string author;
        private string title;
        private string price;
        private string publishedYear;
        private string bookStatus;

        private string originalSerialNumber;
        private string originalAuthor;
        private string originalTitle;
        private string originalPrice;
        private string originalPublishedYear;
        private int originalGenre;
        private Visibility showStatusButton;
        private Visibility showArchiveButton;
        private Visibility showUnarchiveButton;
        private Reader takenBook;
        private bool enableSaveChangesButton;
        private int selectedGenre;
        private List<BookReader> bookReaders;
        private int selectedBookReader;

        ICommand showReaderDetailsCommand;
        ICommand saveChangesCommand;
        ICommand archiveBookCommand;
        ICommand unarchiveBookCommand;
        ICommand refreshDataCommand;

        public BookDetailsViewModel(string serial)
        {
            UpdateProperties(serial);
            this.EnableSaveChangesButton = false;
        }


        public string SerialNumber
        {
            get
            {
                return this.serialNumber;
            }
            set
            {
                this.serialNumber = value;
                OnPropertyChanged("SerialNumber");
                CheckForChanges();
            }
        }
        public string Author
        {
            get
            {
                return this.author;
            }
            set
            {
                this.author = value;
                OnPropertyChanged("Author");
                CheckForChanges();
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
                CheckForChanges();
            }
        }
        public string Price
        {
            get
            {
                return this.price;
            }
            set
            {
                this.price = value;
                OnPropertyChanged("Price");
                CheckForChanges();
            }
        }
        public string PublishedYear
        {
            get
            {
                return this.publishedYear;
            }
            set
            {
                this.publishedYear = value;
                OnPropertyChanged("PublishedYear");
                CheckForChanges();
            }
        }
        public string BookStatus
        {
            get
            {
                return this.bookStatus;
            }
            set
            {
                this.bookStatus = value;
                OnPropertyChanged("BookStatus");
            }
        }
        public Visibility ShowStatusButton
        {
            get
            {
                return this.showStatusButton;
            }
            set
            {
                this.showStatusButton = value;
                OnPropertyChanged("ShowStatusButton");
            }
        }
        public Visibility ShowArchiveButton
        {
            get
            {
                return this.showArchiveButton;
            }
            set
            {
                this.showArchiveButton = value;
                OnPropertyChanged("ShowArchiveButton");
            }
        }
        public Visibility ShowUnarchiveButton
        {
            get
            {
                return this.showUnarchiveButton;
            }
            set
            {
                this.showUnarchiveButton = value;
                OnPropertyChanged("ShowUnarchiveButton");
            }
        }
        public bool EnableSaveChangesButton
        {
            get
            {
                return this.enableSaveChangesButton;
            }
            set
            {
                this.enableSaveChangesButton = value;
                OnPropertyChanged("EnableSaveChangesButton");
            }
        }
        public List<string> Genres
        {
            get
            {
                return GenreTools.GetOnlyGenreText();
            }
        }
        public int SelectedGenre
        {
            get
            {
                return this.selectedGenre;
            }
            set
            {
                this.selectedGenre = value;
                OnPropertyChanged("SelectedGenre");
                CheckForChanges();
            }
        }
        public List<BookReader> BookReaders
        {
            get
            {
                if(this.bookReaders == null)
                {
                    this.bookReaders = DataPersister.GetBookReadersBySerialNumber(this.originalSerialNumber);
                }
                return this.bookReaders;
            }
            set
            {
                this.bookReaders = value;
                OnPropertyChanged("BookReaders");
            }
        }
        public int SelectedBookReader
        {
            get
            {
                return this.selectedBookReader;
            }
            set
            {
                this.selectedBookReader = value;
                OnPropertyChanged("SelectedBookReader");
            }
        }

        public string OriginalSerialNumber
        {
            get
            {
                return this.originalSerialNumber;
            }
            set
            {
                this.originalSerialNumber = value;
                OnPropertyChanged("OriginalSerialNumber");
            }
        }
        public string OriginalAuthor
        {
            get
            {
                return this.originalAuthor;
            }
            set
            {
                this.originalAuthor = value;
                OnPropertyChanged("OriginalAuthor");
            }
        }
        public string OriginalTitle
        {
            get
            {
                return this.originalTitle;
            }
            set
            {
                this.originalTitle = value;
                OnPropertyChanged("OriginalTitle");
            }
        }
        public string OriginalPrice
        {
            get
            {
                return this.originalPrice;
            }
            set
            {
                this.originalPrice = value;
                OnPropertyChanged("OriginalPrice");
            }
        }
        public string OriginalPublishedYear
        {
            get
            {
                return this.originalPublishedYear;
            }
            set
            {
                this.originalPublishedYear = value;
                OnPropertyChanged("OriginalPublishedYear");
            }
        }
        public int OriginalGenre
        {
            get
            {
                return this.originalGenre; ;
            }
            set
            {
                this.originalGenre = value;
                OnPropertyChanged("OriginalGenre");
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
        public ICommand SaveChangesCommand
        {
            get
            {
                if (this.saveChangesCommand == null)
                {
                    this.saveChangesCommand = new RelayCommand(this.HandleSaveChangesCommand);
                }
                return this.saveChangesCommand;
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
        public ICommand UnarchiveBookCommand
        {
            get
            {
                if (this.unarchiveBookCommand == null)
                {
                    this.unarchiveBookCommand = new RelayCommand(this.HandleUnarchiveBookCommand);
                }
                return this.unarchiveBookCommand;
            }
        }
        public ICommand RefreshDataCommand
        {
            get
            {
                if(this.refreshDataCommand == null)
                {
                    this.refreshDataCommand = new RelayCommand(this.HandleRefreshDataCommand);
                }
                return this.refreshDataCommand;
            }
        }

        
        private void HandleShowReaderDetailsCommand(object parameter)
        {
            string rd = parameter as string;
            string egn;
            if (rd.ToUpper().Equals("StatusButton".ToUpper()))
            {
                egn = this.takenBook.EGN;
            }
            else
            {
                if(this.BookReaders.Count == 0)
                {
                    MessageBox.Show("Няма читатели, които да са вземали книгата!", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    if (this.BookReaders.Count == 1 && this.SelectedBookReader == -1) this.SelectedBookReader = 0;
                    else if(this.BookReaders.Count > 1 && this.SelectedBookReader == -1)
                    {
                        MessageBox.Show("Моля изберете читател!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                egn = this.BookReaders[this.SelectedBookReader].ReaderEGN;
            }
            ReaderDetailsViewModel vm = new ReaderDetailsViewModel(egn);
            var win = new ReaderDetailsPage();
            win.DataContext = vm;
            win.Show();
        }
        private void HandleSaveChangesCommand(object parameter)
        {
            try
            {
                bool sure = MessageBox.Show("Сигурни ли сте, че искате да запазите промените?", "Потвърждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
                if (sure)
                {
                    if (ValidateData())
                    {
                        Book b = new Book(this.SerialNumber, this.Author, this.Title, this.Price, this.PublishedYear, this.SelectedGenre);
                        if (this.SerialNumber != this.originalSerialNumber)
                        {
                            DataInserter.UpdateBookReturnedBooks(this.SerialNumber, this.originalSerialNumber);
                            DataInserter.UpdateBookTakenBook(this.SerialNumber, this.originalSerialNumber);
                        }
                        if (DataPersister.DatabaseContainsSerialNumber(this.originalSerialNumber, "Books"))
                        {
                            DataInserter.UpdateBook(b, this.originalSerialNumber);
                        }
                        else
                        {
                            DataInserter.UpdateArchivedBook(b, this.originalSerialNumber);
                        }

                        this.originalSerialNumber = this.SerialNumber;
                        this.originalAuthor = this.Author;
                        this.originalTitle = this.Title;
                        this.originalPrice = this.Price;
                        this.originalPublishedYear = this.PublishedYear;
                        this.originalGenre = this.SelectedGenre;
                        this.EnableSaveChangesButton = false;
                        MessageBox.Show("Промените са запазени успешно!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void HandleArchiveBookCommand(object parameter)
        {
            if (DataPersister.BookIsTaken(this.originalSerialNumber))
            {
                MessageBox.Show("Книгата в момента е в читател и не може да бъде архивирана!", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ArchiveBookViewModel vm = new ArchiveBookViewModel();
            var win = new ArchiveBookPage();
            win.DataContext = vm;
            win.ShowDialog();
            if (vm.WindowClosedManually == false)
            {
                TransferBookIntoArchive(vm.ArchiveNumber, vm.DateOfArchivation);
                UpdateProperties(this.originalSerialNumber);
            }
        }
        private void HandleUnarchiveBookCommand(object parameter)
        {
            bool sure = MessageBox.Show("Сигурни ли сте, че искате да възстановите книгата?", "Потвърждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (sure)
            {
                ArchivedBook book = DataPersister.GetArchivedBookBySerialNumber(this.originalSerialNumber);
                DataInserter.DeleteArchivedBook(this.originalSerialNumber);
                DataInserter.AddBook(book);
                MessageBox.Show("Книгата беше възстановена успешно!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateProperties(this.originalSerialNumber);
            }
        }
        private void HandleRefreshDataCommand(object parameter)
        {
            UpdateProperties(this.originalSerialNumber);
        }



        private void UpdateProperties(string serial)
        {
            ArchivedBook selectedBook;
            bool isArchived = false;
            if (DataPersister.DatabaseContainsSerialNumber(serial, "Books"))
            {
                Book b = DataPersister.GetBookBySerialNumber(serial);
                selectedBook = new ArchivedBook(b.SerialNumber, b.Author, b.Title, b.Price, b.PublishedYear, b.Genre, "", DateTime.Now);
                this.ShowArchiveButton = Visibility.Visible;
                this.ShowUnarchiveButton = Visibility.Collapsed;
            }
            else
            {
                isArchived = true;
                selectedBook = DataPersister.GetArchivedBookBySerialNumber(serial);
                this.ShowArchiveButton = Visibility.Collapsed;
                this.ShowUnarchiveButton = Visibility.Visible;
            }
            this.originalSerialNumber = this.SerialNumber = selectedBook.SerialNumber;
            this.originalAuthor = this.Author = selectedBook.Author;
            this.originalTitle = this.Title = selectedBook.Title;
            this.originalPrice = this.Price = selectedBook.Price;
            this.originalPublishedYear = this.PublishedYear = selectedBook.PublishedYear;
            this.originalGenre = this.SelectedGenre = selectedBook.Genre;

            if (DataPersister.BookIsTaken(serial))
            {
                Reader r = DataPersister.GetTakenBookReaderInfoBySerialNumber(serialNumber);
                this.takenBook = r;
                this.BookStatus = "Взета от " + r.Name;
                this.ShowStatusButton = Visibility.Visible;
            }
            else if (isArchived)
            {
                this.BookStatus = "Архивирана | " + selectedBook.ArchiveNumber + " | " + selectedBook.ArchivedDate.Date;
                this.ShowStatusButton = Visibility.Hidden;
            }
            else
            {
                this.BookStatus = "Свободна";
                this.ShowStatusButton = Visibility.Hidden;
                this.takenBook = null;
            }
            this.EnableSaveChangesButton = false;
            this.BookReaders = null;
        }
        private void CheckForChanges()
        {
            if (this.originalSerialNumber != this.SerialNumber ||
                this.originalAuthor != this.Author ||
                this.originalTitle != this.Title ||
                this.originalPrice != this.Price ||
                this.originalPublishedYear != this.PublishedYear ||
                this.originalGenre != this.SelectedGenre)
            {
                this.EnableSaveChangesButton = true;
            }
            else
            {
                this.EnableSaveChangesButton = false;
            }
        }
        private bool ValidateData()
        {
            string serialNumber = this.SerialNumber;
            string author = this.Author;
            string title = this.Title;
            string price = this.Price;
            string year = this.PublishedYear;
            int genre = this.SelectedGenre;

            bool mistakeFound = false;
            StringBuilder strBuilder = new StringBuilder();

            try
            {
                if (!DataValidator.isValidInteger(serialNumber))
                {
                    mistakeFound = true;
                    strBuilder.Append("Моля въведете валиден номер!\n");
                }
                else if (serialNumber != originalSerialNumber)
                {
                    if (DataPersister.DatabaseContainsSerialNumber(serialNumber, "Books") || DataPersister.ArchivedBooksContainBookSeralNumber(serialNumber))
                    {
                        mistakeFound = true;
                        strBuilder.Append("Съществува книга с този номер!\n");
                    }
                }

                if (!DataValidator.IsValidString(author))
                {
                    mistakeFound = true;
                    strBuilder.Append("Моля въведете валиден автор!\n");
                }
                if (!DataValidator.IsValidString(title))
                {
                    mistakeFound = true;
                    strBuilder.Append("Моля въведете валидно заглавие!\n");
                }
                if (!DataValidator.IsValidDouble(price))
                {
                    mistakeFound = true;
                    strBuilder.Append("Моля въведете валидна цена!");
                }
                else if (!DataValidator.IsValidString(year))
                {
                    mistakeFound = true;
                    strBuilder.Append("Моля въведете валидна година!");
                }

                if (mistakeFound == true)
                {
                    MessageBox.Show(strBuilder.ToString(), "Невалидни данни", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
                return true;
            }
            catch (Exception)
            {

            }
            return true;
        }
        private void TransferBookIntoArchive(string number, DateTime date)
        {
            try
            {
                if (DataPersister.ArchivedBooksContainBookSeralNumber(this.originalSerialNumber))
                {
                    MessageBox.Show("Вече има архивирана книга с този сериен номер!", "Съществуващ номер", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Book b = DataPersister.GetBookBySerialNumber(this.originalSerialNumber);
                DataInserter.DeleteBookBySerialNumber(this.originalSerialNumber);
                DataInserter.ArchiveBook(b, number, date);
                MessageBox.Show("Книгата е архивирана успешно!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Изникна проблем. Моля затворете базата данни и опитайте отново.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
