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
    public class SearchStudentViewModel : SearchTools
    {
        enum Options
        {
            EGN = 0,
            Name = 1,
            ClassNo = 2
        };

        private List<Reader> selectedReaders;
        private int selectedReader;
        private Visibility chooseVisibility = Visibility.Hidden;
        private string choosenReaderEGN;
        private string choosenReaderName;

        ICommand searchReader;
        ICommand removeReaderCommand;
        ICommand showDetailsCommand;
        ICommand chooseCommand;
        ICommand refreshDataCommand;

        public int SelectedReader
        {
            get
            {
                return this.selectedReader;
            }
            set
            {
                this.selectedReader = value;
                OnPropertyChanged("SelectedReaders");
            }
        }
        public string ChoosenReaderEGN
        {
            get
            {
                return this.choosenReaderEGN;
            }
            set
            {
                this.choosenReaderEGN = value;
                OnPropertyChanged("ChoosenReaderEGN");
            }
        }
        public string ChoosenReaderName
        {
            get
            {
                return this.choosenReaderName;
            }
            set
            {
                this.choosenReaderName = value;
                OnPropertyChanged("ChoosenReaderName");
            }
        }
        public Visibility ChooseVisibility
        {
            get
            {
                return this.chooseVisibility;
            }
            set
            {
                this.chooseVisibility = value;
                OnPropertyChanged("ChooseVisibility");
            }
        }
        public List<Reader> SelectedReaders
        {
            get
            {
                if (this.selectedReaders == null)
                {
                    try
                    {
                        this.selectedReaders = DataPersister.GetAllReaders();
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException();
                    }
                }
                return this.selectedReaders;
            }
            set
            {
                this.selectedReaders = value;
                OnPropertyChanged("SelectedReaders");
            }
        }
        public List<string> Paralelki
        {
            get
            {
                return ParalelkiTools.GetOnlyCharacters();
            }
        }



        public ICommand SearchReaderCommand
        {
            get
            {
                if (this.searchReader == null)
                {
                    this.searchReader = new RelayCommand(this.HandleSearchReaderCommand);
                }
                return this.searchReader;
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
        public ICommand ShowDetailsCommand
        {
            get
            {
                if (this.showDetailsCommand == null)
                {
                    this.showDetailsCommand = new RelayCommand(this.HandleShowDetailsCommand);
                }
                return this.showDetailsCommand;
            }
        }
        public ICommand ChooseCommand
        {
            get
            {
                if (this.chooseCommand == null)
                {
                    this.chooseCommand = new RelayCommand(this.HandleChooseCommand);
                }
                return this.chooseCommand;
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



        private void HandleSearchReaderCommand(object parameter)
        {
            int option = this.SearchOption;
            string searchKey = this.SearchValue;

            if (option == (int)Options.EGN)
            {
                SearchReaderByEGN(searchKey);
            }
            else if (option == (int)Options.Name)
            {
                SearchReaderByName(searchKey);
            }
            else
            {
                SearchReaderByClass();
            }
        }
        private void HandleRemoveReaderCommand(object parameter)
        {
            int pos = this.SelectedReader;
            if (pos == -1 && this.SelectedReaders.Count > 1)
            {
                MessageBox.Show("Моля изберете читател!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (this.SelectedReaders.Count == 0)
            {
                MessageBox.Show("Няма налични читатели!");
                return;
            }

            bool sure = MessageBox.Show("Сигурни ли сте, че искате да премахнете избрания читател?", "Потвърждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (sure)
            {
                try
                {
                    Reader readerToBeDeleted = this.SelectedReaders[pos];

                    //TODO: Delete all records that are affected by this operation

                    bool deleteQuestion = MessageBox.Show("Всички записи за читателя ще бъдат изтрити. Продължаване?", "Потвърждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
                    if (deleteQuestion)
                    {
                        try
                        {
                            DataInserter.DeleteReader(readerToBeDeleted.EGN);
                            MessageBox.Show("Всички записи за читателя бяха изтрити успешно!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.SelectedReaders = DataPersister.GetAllReaders();
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

                    List<Reader> newList = DataPersister.GetAllReaders();
                    this.SelectedReaders = newList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Изникна проблем. Моля затворете базата данни и опитайте отново.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                return;
            }
        }
        private void HandleShowDetailsCommand(object parameter)
        {
            int pos = this.SelectedReader;
            if (pos == -1 && this.SelectedReaders.Count > 1)
            {
                MessageBox.Show("Моля изберете читател!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (this.SelectedReaders.Count == 0)
            {
                MessageBox.Show("Няма налични читатели!", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (this.SelectedReaders.Count == 1) pos = 0;

            var context = new ReaderDetailsViewModel(this.SelectedReaders[pos].EGN);
            var win = new ReaderDetailsPage();
            win.DataContext = context;
            win.Show();
        }
        private void HandleChooseCommand(object parameter)
        {
            int pos = this.SelectedReader;
            if (pos == -1 && this.SelectedReaders.Count > 1)
            {
                MessageBox.Show("Моля изберете читател!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (this.SelectedReaders.Count == 0)
            {
                MessageBox.Show("Няма налични читатели!", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (this.SelectedReaders.Count == 1) this.SelectedReader = 0;
            this.ChoosenReaderEGN = this.SelectedReaders[this.SelectedReader].EGN;
            this.ChoosenReaderName = this.SelectedReaders[this.SelectedReader].Name;
            var win = parameter as Window;
            win.Close();
        }
        private void HandleRefreshDataCommand(object parameter)
        {
            HandleSearchReaderCommand(null);
        }


        private void SearchReaderByEGN(string searchKey)
        {
            try
            {
                if (searchKey == null || searchKey == String.Empty)
                {
                    this.SelectedReaders = null;
                    return;
                }
                searchKey = searchKey.Trim();

                Reader foundReader = DataPersister.GetReaderByEGN(searchKey);
                if (foundReader == null)
                {
                    MessageBox.Show("Не е намерен читател с това ЕГН!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    List<Reader> newList = new List<Reader>();
                    newList.Add(foundReader);
                    this.SelectedReaders.Clear();
                    this.SelectedReaders = newList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Изникна проблем. Моля затворете базата данни и опитайте отново.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void SearchReaderByName(string searchKey)
        {
            try
            {
                if (searchKey == null || searchKey == String.Empty)
                {
                    this.SelectedReaders = null;
                    return;
                }
                searchKey = searchKey.Trim();
                searchKey = searchKey.Replace('.', ' ');

                var searchNames = searchKey.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                List<Reader> oneMatchList = new List<Reader>();
                List<Reader> matchesList = new List<Reader>();
                List<Reader> allReaders = DataPersister.GetAllReaders();
                foreach (var reader in allReaders)
                {
                    var checkNames = reader.Name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    int numberOfMatches = 0;

                    foreach (var name in searchNames)
                    {
                        foreach (var check in checkNames)
                        {
                            if (name.ToUpper().Equals(check.ToUpper()))
                            {
                                numberOfMatches++;
                            }
                        }
                    }

                    if (numberOfMatches >= 2)
                    {
                        matchesList.Add(reader);
                    }
                    else if (numberOfMatches == 1)
                    {
                        oneMatchList.Add(reader);
                    }
                }

                if (matchesList.Count == 0 && oneMatchList.Count == 0)
                {
                    MessageBox.Show("Не е намерен читател с това име!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    matchesList.AddRange(oneMatchList);
                    this.SelectedReaders = matchesList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Изникна проблем. Моля затворете базата данни и опитайте отново.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void SearchReaderByClass()
        {
            try
            {
                int classNo = this.SearchClassNumber + 1;
                int paral = this.SearchClassParalelka;

                List<Reader> foundStudents = DataPersister.GetReaderByClass(classNo, paral);
                if (foundStudents.Count == 0)
                {
                    MessageBox.Show("Няма намерени ученици!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                this.SelectedReaders = foundStudents;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Изникна проблем. Моля затворете базата данни и опитайте отново.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
