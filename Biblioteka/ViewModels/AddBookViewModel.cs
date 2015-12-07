using Biblioteka.Commands;
using Biblioteka.Data;
using Biblioteka.HelperClasses;
using Biblioteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Biblioteka.ViewModels
{
    public class AddBookViewModel : ViewModelBase
    {
        private int selectedGenre;
        ICommand addBook;
        ICommand closeWindow;
        public AddBookViewModel() { }

        public string SerialNumber { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string PublishedYear { get; set; }
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
            }
        }

        
        public ICommand AddBookCommand
        {
            get
            {
                if(this.addBook == null)
                {
                    this.addBook = new RelayCommand(this.HandleAddBookCommand);
                }
                return this.addBook;
            }
        }
        public ICommand CloseWindowCommand
        {
            get
            {
                if (this.closeWindow == null)
                {
                    this.closeWindow = new RelayCommand(this.HandleCloseWindowCommand);
                }
                return this.closeWindow;
            }
        }


        private void HandleAddBookCommand(object parameter)
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
                else if (DataPersister.DatabaseContainsSerialNumber(serialNumber, "Books") || DataPersister.ArchivedBooksContainBookSeralNumber(serialNumber))
                {
                    mistakeFound = true;
                    strBuilder.Append("Съществува книга с този номер!\n");
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
                    MessageBox.Show(strBuilder.ToString(), "Некоректни данни", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    Book book = new Book(serialNumber, author, title, price, year, genre);
                    DataInserter.AddBook(book);
                    MessageBox.Show("Книгата беше добавена успешно!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    var win = parameter as Window;
                    win.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Изникна проблем. Моля затворете базата данни и опитайте отново.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void HandleCloseWindowCommand(object parameter)
        {
            var win = parameter as Window;
            win.Close();
        }
    }
}
