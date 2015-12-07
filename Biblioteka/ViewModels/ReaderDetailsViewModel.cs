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
    public class ReaderDetailsViewModel : ViewModelBase
    {
        private string egn;
        private string name;
        private string address;
        private string serialNumber;
        private DateTime date;

        private string originalEGN;
        private string originalName;
        private string originalAddress;
        private string originalSerialNumber;
        private DateTime originalDate;
        private int originalClass;
        private int originalParalelka;
        private int originalPosition;

        private List<TakenBookRecord> takenBooks;
        private int selectedTakenBook;
        private int selectedReturnedBook;
        private List<ReturnedBookRecord> returnedBooks;
        private bool enableSaveChangesButton;
        private int selectedClass;
        private int selectedParalelka;
        private int selectedPosition;
        private Visibility showClassChoice;


        ICommand saveChangesCommand;
        ICommand takeBookCommand;
        ICommand returnBookCommand;
        ICommand removeReaderCommand;
        ICommand bookDetailsCommand;
        ICommand refreshDataCommand;

        public ReaderDetailsViewModel(string egn)
        {
            UpdateProperties(egn);
        }

        public string EGN
        {
            get
            {
                return this.egn;
            }
            set
            {
                this.egn = value;
                OnPropertyChanged("EGN");
                CheckForChanges();
            }
        }
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                OnPropertyChanged("Name");
                CheckForChanges();
            }
        }
        public string Address
        {
            get
            {
                return this.address;
            }
            set
            {
                this.address = value;
                OnPropertyChanged("Address");
                CheckForChanges();
            }
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
        public DateTime DateOfCreation
        {
            get
            {
                return this.date.Date;
            }
            set
            {
                this.date = value;
                OnPropertyChanged("DateOfCreation");
            }
        }

        public List<string> ParalelkiOptions
        {
            get
            {
                return ParalelkiTools.GetOnlyCharacters();
            }
        }

        public List<TakenBookRecord> TakenBooks
        {
            get
            {
                if (this.takenBooks == null)
                {
                    this.takenBooks = DataPersister.GetTakenBooksByEGN(this.originalEGN);

                }
                return this.takenBooks;
            }
            set
            {
                this.takenBooks = value;
                OnPropertyChanged("TakenBooks");
            }
        }

        public List<ReturnedBookRecord> ReturnedBooks
        {
            get
            {
                if (this.returnedBooks == null)
                {
                    var list1 = DataPersister.GetReturnedBooksByEGN(this.EGN);

                    var list2 = DataPersister.GetArchivedReturnedBooks(this.originalEGN);
                    list1.AddRange(list2);
                    this.returnedBooks = list1;
                }
                return this.returnedBooks;
            }
            set
            {
                this.returnedBooks = value;
                OnPropertyChanged("ReturnedBooks");
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

        public int SelectedTakenBook
        {
            get
            {
                return this.selectedTakenBook;
            }
            set
            {
                this.selectedTakenBook = value;
                OnPropertyChanged("SelectedTakenBook");
            }
        }
        public int SelectedReturnedBook
        {
            get
            {
                return this.selectedReturnedBook;
            }
            set
            {
                this.selectedReturnedBook = value;
                OnPropertyChanged("SelectedReturnedBook");
            }
        }

        public int SelectedClass
        {
            get
            {
                return this.selectedClass;
            }
            set
            {
                this.selectedClass = value;
                OnPropertyChanged("SelectedClass");
                CheckForChanges();
            }
        }
        public int SelectedParalelka
        {
            get
            {
                return this.selectedParalelka;
            }
            set
            {
                this.selectedParalelka = value;
                OnPropertyChanged("SelectedParalelka");
                CheckForChanges();
            }
        }

        public int SelectedPosition
        {
            get
            {
                return this.selectedPosition;
            }
            set
            {
                this.selectedPosition = value;
                if (this.selectedPosition == 0) this.ShowClassChoice = Visibility.Visible;
                else this.ShowClassChoice = Visibility.Hidden;
                OnPropertyChanged("SelectedPosition");
                CheckForChanges();
            }
        }
        public Visibility ShowClassChoice
        {
            get
            {
                return this.showClassChoice;
            }
            set
            {
                this.showClassChoice = value;
                OnPropertyChanged("ShowClassChoice");
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
        public ICommand TakeBookCommand
        {
            get
            {
                if (this.takeBookCommand == null)
                {
                    this.takeBookCommand = new RelayCommand(this.HandleTakeNewBookCommand);
                }
                return this.takeBookCommand;
            }
        }
        public ICommand ReturnBookCommand
        {
            get
            {
                if (this.returnBookCommand == null)
                {
                    this.returnBookCommand = new RelayCommand(this.HandleReturnBookCommand);
                }
                return this.returnBookCommand;
            }
        }
        public ICommand RemoveReaderCommand
        {
            get
            {
                if (this.removeReaderCommand == null)
                {
                    this.removeReaderCommand = new RelayCommand(this.HandleRemoveReaderCommand);
                }
                return this.removeReaderCommand;
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


        private void HandleSaveChangesCommand(object parameter)
        {
            try
            {
                if (ValidateData())
                {
                    //TODO: Update the classNo
                    //string par = ParalelkiTools.CharFromNumber(this.selectedParalelka);
                    //if (this.SelectedParalelka == 0) par = "а";
                    //else if (this.SelectedParalelka == 1) par = "б";
                    //else par = "в";
                    bool sure = MessageBox.Show("Сигурни ли сте, че искате да запазите промените?", "Потвърждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
                    if (sure)
                    {
                        if (this.SelectedPosition == 1)
                        {
                            this.SelectedClass = -1;
                            this.SelectedParalelka = 0;
                        }
                        Reader newReader = new Reader(this.EGN, this.Name, this.Address, this.SerialNumber, this.SelectedClass + 1, this.SelectedParalelka, this.DateOfCreation);
                        if (this.EGN != this.originalEGN)
                        {
                            DataInserter.UpdateReaderReturnedBooks(this.EGN, originalEGN);
                            DataInserter.UpdateReaderTakenBooks(this.EGN, originalEGN);
                        }
                        DataInserter.UpdateReader(newReader, originalEGN);

                        this.originalEGN = this.egn;
                        this.originalAddress = this.Address;
                        this.originalName = this.Name;
                        this.originalSerialNumber = this.SerialNumber;
                        this.originalDate = this.DateOfCreation;
                        this.originalClass = this.SelectedClass;
                        this.originalParalelka = this.SelectedParalelka;
                        this.originalPosition = this.SelectedPosition;
                        this.EnableSaveChangesButton = false;
                        MessageBox.Show("Промените са запазени успешно!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else return;
            }
            catch (Exception ex)
            {

            }
        }

        private void HandleTakeNewBookCommand(object parameter)
        {
            var dataContext = new TakeBookViewModel(this.EGN, this.Name, true);
            var win = new TakeBookPage();
            win.DataContext = dataContext;
            win.Show();
        }

        private void HandleReturnBookCommand(object parameter)
        {
            int pos = this.SelectedTakenBook;
            if (pos == -1 && this.TakenBooks.Count > 1)
            {
                MessageBox.Show("Моля изберете книга!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (this.TakenBooks.Count == 0)
            {
                MessageBox.Show("Няма налични книги!", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (this.TakenBooks.Count == 1) pos = 0;

            bool sure = MessageBox.Show("Сигурни ли сте, че читателят връща книгата?", "Потвърждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (sure)
            {
                try
                {
                    DataInserter.DeleteTakenBookRecord(this.TakenBooks[pos].ID);
                    string egn = this.TakenBooks[pos].ReaderEGN;
                    string serial = this.TakenBooks[pos].SerialNumber;
                    DateTime d1 = this.TakenBooks[pos].DateOfTaking;
                    DateTime d2 = DateTime.Today.Date;
                    bool retunedOnTime = d2.Date > this.TakenBooks[pos].Deadline.Date ? false : true;
                    ReturnedBookRecord rec = new ReturnedBookRecord(0, egn, serial, d1, d2, retunedOnTime);
                    DataInserter.SaveReturnedRecord(rec);

                    this.TakenBooks = null;
                    this.ReturnedBooks = null;
                    MessageBox.Show("Книгата е върната успешно!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Изникна проблем. Моля затворете базата данни и опитайте отново.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else return;

        }

        private void HandleRemoveReaderCommand(object parameter)
        {
            bool sure = MessageBox.Show("Сигурни ли сте, че искате да премахнете читателя?", "Потвърждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (sure)
            {
                bool deleteQuestion = MessageBox.Show("Всички записи за читателя ще бъдат изтрити. Продължаване?", "Потвърждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
                if (deleteQuestion)
                {
                    try
                    {
                        DataInserter.DeleteReader(this.EGN);
                        MessageBox.Show("Всички записи за читателя бяха изтрити успешно!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        var win = parameter as Window;
                        win.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Изникна проблем. Моля затворете базата данни и опитайте отново.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Операцията е прекратена!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
        }
        private void HandleBookDetailsCommand(object parameter)
        {
            int pos;
            string serial;
            string button = parameter.ToString();
            if (button.ToUpper().Equals("Taken".ToUpper()))
            {
                pos = this.SelectedTakenBook;
                if (pos == -1 && this.TakenBooks.Count > 1)
                {
                    MessageBox.Show("Моля изберете книга!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);

                    return;
                }
                else if (this.TakenBooks.Count == 0)
                {
                    MessageBox.Show("Няма налични книги!", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (this.TakenBooks.Count == 1) this.SelectedTakenBook = 0;

                pos = this.SelectedTakenBook;
                serial = this.TakenBooks[pos].SerialNumber;
            }
            else
            {
                pos = this.SelectedReturnedBook;
                if (pos == -1 && this.ReturnedBooks.Count > 1)
                {
                    MessageBox.Show("Моля изберете книга!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else if (this.ReturnedBooks.Count == 0)
                {
                    MessageBox.Show("Няма налични книги!", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (this.ReturnedBooks.Count == 1) this.SelectedReturnedBook = 0;
                pos = this.SelectedReturnedBook;
                serial = this.ReturnedBooks[pos].SerialNumber;
            }


            BookDetailsViewModel vm = new BookDetailsViewModel(serial);
            var win = new BookDetailsPage();
            win.DataContext = vm;
            win.Show();
        }
        private void HandleRefreshDataCommand(object parameter)
        {
            UpdateProperties(this.originalEGN);
        }


        private void UpdateProperties(string egn)
        {
            Reader selectedReader = DataPersister.GetReaderByEGN(egn);
            this.originalEGN = this.EGN = selectedReader.EGN;
            this.originalName = this.Name = selectedReader.Name;
            this.originalAddress = this.Address = selectedReader.Address;
            this.originalSerialNumber = this.SerialNumber = selectedReader.SerialNumber;
            this.originalDate = this.DateOfCreation = selectedReader.DateOfCreation;
            this.originalClass = this.SelectedClass = selectedReader.ClasNo - 1;
            int par = selectedReader.Paralelka;
            //if (selectedReader.Paralelka == "а") par = 0;
            //else if (selectedReader.Paralelka == "б") par = 1;
            //else par = 2;
            this.originalParalelka = this.SelectedParalelka = par;

            if (selectedReader.ClasNo == 0)
            {
                this.originalPosition = this.SelectedPosition = 1;
                this.ShowClassChoice = Visibility.Hidden;
            }
            else
            {
                this.originalPosition = this.SelectedPosition = 0;
                this.ShowClassChoice = Visibility.Visible;
            }

            this.enableSaveChangesButton = false;
            this.TakenBooks = null;
            this.ReturnedBooks = null;
        }
        private bool ValidateData()
        {
            string egn = this.EGN.Trim();
            string name = this.Name.Trim();
            string address = this.Address.Trim();
            string serialNumber = this.SerialNumber;
            int classNo = this.SelectedClass + 1;
            int paral = this.SelectedParalelka;
            DateTime chosenDate = this.DateOfCreation;

            bool mistakeFound = false;
            StringBuilder strBuilder = new StringBuilder();

            try
            {
                if (!DataValidator.IsValidString(egn))
                {
                    mistakeFound = true;
                    strBuilder.Append("Моля въведете валидно ЕГН!\n");
                }
                else if (egn != originalEGN)
                {
                    if (DataPersister.DatabaseContainsEGN(egn))
                    {
                        mistakeFound = true;
                        strBuilder.Append("Вече има читател с това ЕГН!\n");
                    }
                }

                if (!DataValidator.IsValidString(name))
                {
                    mistakeFound = true;
                    strBuilder.Append("Моля въведете валидно име!\n");
                }
                if (!DataValidator.IsValidString(address))
                {
                    mistakeFound = true;
                    strBuilder.Append("Моля въведете валиден адрес!\n");
                }
                if (!DataValidator.isValidInteger(serialNumber))
                {
                    mistakeFound = true;
                    strBuilder.Append("Моля въведете валиден номер!\n");
                }
                else if (serialNumber != originalSerialNumber)
                {
                    if (DataPersister.DatabaseContainsSerialNumber(serialNumber, "Readers"))
                    {
                        mistakeFound = true;
                        strBuilder.Append("Вече има читател с този номер!\n");
                    }
                }
                if (this.SelectedPosition == 0 && this.SelectedClass == -1)
                {
                    mistakeFound = true;
                    strBuilder.Append("Моля изберете клас!\n");
                }

                if (mistakeFound == true)
                {
                    MessageBox.Show(strBuilder.ToString(), "Некоректни данни", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {

            }
            return true;
        }
        public void CheckForChanges()
        {
            if (this.EGN != this.originalEGN ||
                this.Name != this.originalName ||
                this.Address != this.originalAddress ||
                this.SerialNumber != this.originalSerialNumber ||
                this.SelectedPosition != this.originalPosition)
            {
                this.EnableSaveChangesButton = true;
                return;
            }
            if (this.SelectedPosition == 0)
            {
                if (this.SelectedParalelka != this.originalParalelka || this.SelectedClass != this.originalClass)
                {
                    this.EnableSaveChangesButton = true;
                    return;
                }
            }
            this.EnableSaveChangesButton = false;
        }
    }
}
