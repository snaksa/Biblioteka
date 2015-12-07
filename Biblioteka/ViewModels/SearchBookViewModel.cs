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
    public class SearchBookViewModel : SearchTools
    {
        enum Options
        {
            SerialNumber = 0,
            Author = 1,
            Title = 2,
            Genre = 3
        };

        private List<Book> foundBooks;
        private List<ArchivedBook> archivedBooks;
        private int selectedBook;
        private int selectedTabItem;
        private int selectedArchivedBook;
        private string choosenSerialNumber;
        private string choosenTitle;
        private int selectedGenre;
        private int searchBookOption;
        private Visibility genresVisibility = Visibility.Hidden;
        private Visibility showChooseButton = Visibility.Hidden; 


        ICommand searchCommand;
        ICommand chooseBookCommand;
        ICommand bookDetailsCommand;
        ICommand refreshDataCommand;

        public List<Book> FoundBooks
        {
            get
            {
                if (this.foundBooks == null)
                {
                    try
                    {
                        DataPersister.takenBooksSerials = DataPersister.GetTakenBooksSerials();
                        DataPersister.archivedBookSerials = DataPersister.GetArchivedBooksSerials();
                        this.foundBooks = DataPersister.GetAllBooks();
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException();
                    }
                }
                return this.foundBooks;
            }
            set
            {
                this.foundBooks = value;
                OnPropertyChanged("FoundBooks");
            }
        }
        public List<ArchivedBook> ArchivedBooks
        {
            get
            {
                if(this.archivedBooks == null)
                {
                    this.archivedBooks = DataPersister.GetAllArchivedBooks();
                }
                return archivedBooks;
            }
            set
            {
                this.archivedBooks = value;
                OnPropertyChanged("ArchivedBooks");
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
        public int SelectedTabItem
        {
            get
            {
                return this.selectedTabItem;
            }
            set
            {
                this.selectedTabItem = value;
                OnPropertyChanged("SelectedTabItem");
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
            }
        }
        public int SelectedArchivedBook
        {
            get
            {
                return this.selectedArchivedBook;
            }
            set
            {
                this.selectedArchivedBook = value;
                OnPropertyChanged("SelectedArchivedBook");
            }
        }
        public Visibility GenresVisibility
        {
            get
            {
                return this.genresVisibility;
            }
            set
            {
                this.genresVisibility = value;
                OnPropertyChanged("GenresVisibility");
            }
        }
        public Visibility ShowChooseButton
        {
            get
            {
                return this.showChooseButton;
            }
            set
            {
                this.showChooseButton = value;
                OnPropertyChanged("ShowChooseButton");
            }
        }
        public string ChoosenSerialNumber
        {
            get
            {
                return this.choosenSerialNumber;
            }
            set
            {
                this.choosenSerialNumber = value;
                OnPropertyChanged("ChoosenSerialNumber");
            }
        }
        public string ChoosenTitle
        {
            get
            {
                return this.choosenTitle;
            }
            set
            {
                this.choosenTitle = value;
                OnPropertyChanged("ChoosenTitle");
            }
        }
        public int SearchBookOption
        {
            get
            {
                return this.searchBookOption;
            }
            set
            {
                this.searchBookOption = value;
                if (value == 3)
                {
                    this.GenresVisibility = Visibility.Visible;
                    this.SearchValueEnabled = false;
                }
                else
                {
                    this.GenresVisibility = Visibility.Hidden;
                    this.SearchValueEnabled = true;
                }
                OnPropertyChanged("SearchBookOption");
            }
        }
        public List<string> Genres
        {
            get
            {
                return GenreTools.GetOnlyGenreText();
            }
        }
        


        public ICommand SearchCommand
        {
            get
            {
                if (this.searchCommand == null)
                {
                    this.searchCommand = new RelayCommand(this.HandleSearchCommand);
                }
                return this.searchCommand;
            }
        }
        public ICommand ChooseBookCommand
        {
            get
            {
                if(this.chooseBookCommand == null)
                {
                    this.chooseBookCommand = new RelayCommand(this.HandleChooseBookCommand);
                }
                return this.chooseBookCommand;
            }
        }
        public ICommand BookDetailsCommand
        {
            get
            {
                if (this.bookDetailsCommand == null)
                {
                    this.bookDetailsCommand = new RelayCommand(this.HandleBookDetailsCommand);
                }
                return this.bookDetailsCommand;
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

        
        private void HandleSearchCommand(object parameter)
        {
            int option = this.SearchBookOption;
            string key = this.SearchValue;

            try
            {
                DataPersister.takenBooksSerials = DataPersister.GetTakenBooksSerials();
                DataPersister.archivedBookSerials = DataPersister.GetArchivedBooksSerials();
                if (option == (int)Options.SerialNumber)
                {
                    if (!DataValidator.IsValidString(key))
                    {
                        this.FoundBooks = null;
                        this.ArchivedBooks = null;
                        return;
                    }
                    key = key.Trim();
                    SearchBookBySerialNumber(key);
                }
                else if (option == (int)Options.Author)
                {
                    if (!DataValidator.IsValidString(key))
                    {
                        this.FoundBooks = null;
                        this.ArchivedBooks = null;
                        return;
                    }
                    key = key.Trim();
                    SearchBookByAuthor(key);
                }
                else if(option == (int)Options.Title)
                {
                    if (!DataValidator.IsValidString(key))
                    {
                        this.FoundBooks = null;
                        this.ArchivedBooks = null;
                        return;
                    }
                    key = key.Trim();
                    SearchBookByTitle(key);
                }
                else
                {
                    SearchBookByGenre();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Изникна проблем. Моля затворете базата данни и опитайте отново.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void HandleChooseBookCommand(object parameter)
        {
            int pos = this.SelectedBook;
            if (pos == -1 && this.FoundBooks.Count > 1)
            {
                MessageBox.Show("Моля изберете книга!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (this.FoundBooks.Count == 0)
            {
                MessageBox.Show("Няма налични читатели!", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (this.FoundBooks.Count == 1) this.SelectedBook = 0;
            this.ChoosenSerialNumber = this.FoundBooks[this.SelectedBook].SerialNumber;
            this.ChoosenTitle = this.FoundBooks[this.SelectedBook].Title;
            var win = parameter as Window;
            win.Close();
        }
        private void HandleBookDetailsCommand(object parameter)
        {
            int tabIndex = this.SelectedTabItem;
            if (CheckSelectedData())
            {
                BookDetailsViewModel vm = new BookDetailsViewModel(this.ChoosenSerialNumber);
                var win = new BookDetailsPage();
                win.DataContext = vm;
                win.Show();
                DataPersister.takenBooksSerials = DataPersister.GetTakenBooksSerials();
                DataPersister.archivedBookSerials = DataPersister.GetArchivedBooksSerials();
            }
        }
        private void HandleRefreshDataCommand(object parameter)
        {
            HandleSearchCommand(null);
        }

        private bool CheckSelectedData()
        {
            int tabIndex = this.SelectedTabItem;
            int pos1 = this.SelectedBook;
            int pos2 = this.SelectedArchivedBook;
            if (tabIndex == 0)
            {
                if (pos1 == -1 && this.FoundBooks.Count > 1)
                {
                    MessageBox.Show("Моля изберете книга!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
                else if (this.FoundBooks.Count == 0)
                {
                    MessageBox.Show("Няма налични читатели!", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else if (this.FoundBooks.Count == 1) this.SelectedBook = 0;
                this.ChoosenSerialNumber = this.FoundBooks[this.SelectedBook].SerialNumber;
                return true;
            }
            else
            {
                if (pos2 == -1 && this.ArchivedBooks.Count > 1)
                {
                    MessageBox.Show("Моля изберете книга!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
                else if (this.ArchivedBooks.Count == 0)
                {
                    MessageBox.Show("Няма налични читатели!", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else if (this.ArchivedBooks.Count == 1) this.SelectedArchivedBook = 0;
                this.ChoosenSerialNumber = this.ArchivedBooks[this.SelectedArchivedBook].SerialNumber;
                return true;
            }
        }

        private void SearchBookBySerialNumber(string key)
        {
            if (!DataValidator.isValidInteger(key))
            {
                MessageBox.Show("Моля въведете вълиден номер!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int tabIndex = this.SelectedTabItem;
            if (tabIndex == 0)
            {
                if (!DataPersister.DatabaseContainsSerialNumber(key, "Books"))
                {
                    MessageBox.Show("Няма книга с този номер!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Book book = DataPersister.GetBookBySerialNumber(key);
                List<Book> newList = new List<Book>();
                newList.Add(book);
                this.FoundBooks = newList;
            }
            else
            {
                if (!DataPersister.ArchivedBooksContainBookSeralNumber(key))
                {
                    MessageBox.Show("Няма архивирана книга с този номер!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                ArchivedBook book = DataPersister.GetArchivedBookBySerialNumber(key);
                List<ArchivedBook> newList = new List<ArchivedBook>();
                newList.Add(book);
                this.ArchivedBooks = newList;
            }
        }
        private void SearchBookByAuthor(string key)
        {
            key = key.Replace('.', ' ');
            key = key.Replace('\"', ' ');
            var searchWords = key.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<Book> oneMatchList = new List<Book>();
            List<Book> matchesList = new List<Book>();
            List<Book> allBooks = new List<Book>();

            int tabIndex = this.SelectedTabItem;
            if (tabIndex == 0)
            {
                allBooks = DataPersister.GetAllBooks();
            }
            else
            {
                List<ArchivedBook> arBooks = DataPersister.GetAllArchivedBooks();
                foreach (var b in arBooks)
                {
                    allBooks.Add(b);
                }
            }

            
            foreach (var book in allBooks)
            {
                string author = book.Author;
                author = author.Replace('.', ' ');
                var authorNames = author.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int count = 0;

                foreach (var word in searchWords)
                {
                    foreach (var authorName in authorNames)
                    {
                        if (word.ToUpper().Equals(authorName.ToUpper()))
                        {
                            count++;
                        }
                    }
                }

                if (count == 1) oneMatchList.Add(book);
                else if (count > 1) matchesList.Add(book);
            }

            if (oneMatchList.Count == 0 && matchesList.Count == 0)
            {
                MessageBox.Show("Не е намерена книга с този автор!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                matchesList.AddRange(oneMatchList);
                if (tabIndex == 0) this.FoundBooks = matchesList;
                else
                {
                    List<ArchivedBook> newList = new List<ArchivedBook>();
                    foreach (var item in matchesList)
                    {
                        newList.Add((ArchivedBook)item);
                    }
                    this.ArchivedBooks = newList;
                }
            }
        }
        private void SearchBookByTitle(string key)
        {
            key = key.Replace('.', ' ');
            key = key.Replace('\"', ' ');

            var searchWords = key.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<Book> oneMatchList = new List<Book>();
            List<Book> matchesList = new List<Book>();
            List<Book> allBooks = new List<Book>();

            int tabIndex = this.SelectedTabItem;
            if (tabIndex == 0)
            {
                allBooks = DataPersister.GetAllBooks();
            }
            else
            {
                List<ArchivedBook> arBooks = DataPersister.GetAllArchivedBooks();
                foreach (var b in arBooks)
                {
                    allBooks.Add(b);
                }
            }

            foreach (var book in allBooks)
            {
                string title = book.Title;
                title = title.Replace('.', ' ');
                title = title.Replace('\"', ' ');
                var titleWords = title.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int count = 0;

                foreach (var word in searchWords)
                {
                    foreach (var titleWord in titleWords)
                    {
                        if (word.ToUpper().Equals(titleWord.ToUpper()))
                        {
                            count++;
                        }
                    }
                }

                if (count == 1) oneMatchList.Add(book);
                else if (count > 1) matchesList.Add(book);
            }

            if (oneMatchList.Count == 0 && matchesList.Count == 0)
            {
                MessageBox.Show("Не е намерена книга с това заглавие!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                matchesList.AddRange(oneMatchList);
                if (tabIndex == 0) this.FoundBooks = matchesList;
                else
                {
                    List<ArchivedBook> newList = new List<ArchivedBook>();
                    foreach (var item in matchesList)
                    {
                        newList.Add((ArchivedBook)item);
                    }
                    this.ArchivedBooks = newList;
                }
            }
        }
        private void SearchBookByGenre()
        {
            int genre = this.SelectedGenre;
            int tabIndex = this.SelectedTabItem;
            List<Book> books = new List<Book>();
            List<ArchivedBook> archBooks = new List<ArchivedBook>();
            if (tabIndex == 0)
            {
                books = DataPersister.GetBookByGenre(genre);
            }
            else
            {
                archBooks = DataPersister.GetArchivedBooksByGenre(genre);
            }

            if ((tabIndex == 0 && books.Count == 0) || (tabIndex == 1 && archBooks.Count == 0))
            {
                MessageBox.Show("Няма намерени книги с този жанр!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (tabIndex == 0) this.FoundBooks = books;
                else this.ArchivedBooks = archBooks;
            }
        }
        
    }
}
