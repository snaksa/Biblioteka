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
    public class SettingsPageViewModel : ViewModelBase
    {
        private List<Genre> allGenres;
        private List<Paralelka> allParalelki;
        private int selectedGenre;
        private int selectedParalka;

        private ICommand renameGenreCommand;
        private ICommand addGenreCommand;
        private ICommand removeGenreCommand;

        private ICommand renameParalelkaCommand;
        private ICommand addParalelkaCommand;
        private ICommand removeParalelkaCommand;

        public List<Genre> AllGenres
        {
            get
            {
                if (this.allGenres == null)
                {
                    this.allGenres = DataPersister.GetAllGenres();
                }
                return this.allGenres;
            }
            set
            {
                this.allGenres = value;
                OnPropertyChanged("AllGenres");
            }
        }
        public List<Paralelka> AllParalelki
        {
            get
            {
                if (this.allParalelki == null)
                {
                    this.allParalelki = DataPersister.GetAllParalelki();
                }
                return this.allParalelki;
            }
            set
            {
                this.allParalelki = value;
                OnPropertyChanged("AllParalelki");
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
        public int SelectedParalelka
        {
            get
            {
                return this.selectedParalka;
            }
            set
            {
                this.selectedParalka = value;
                OnPropertyChanged("SelectedParalelka");
            }
        }

        public ICommand RenameGenreCommand
        {
            get
            {
                if (this.renameGenreCommand == null)
                {
                    this.renameGenreCommand = new RelayCommand(this.HandleRenameGenreCommand);
                }
                return this.renameGenreCommand;
            }
        }
        public ICommand AddGenreCommand
        {
            get
            {
                if (this.addGenreCommand == null)
                {
                    this.addGenreCommand = new RelayCommand(this.HandleAddGenreCommand);
                }
                return this.addGenreCommand;
            }
        }
        public ICommand RemoveGenreCommand
        {
            get
            {
                if(this.removeGenreCommand == null)
                {
                    this.removeGenreCommand = new RelayCommand(this.HandleRemoveGenreCommand);
                }
                return this.removeGenreCommand;
            }
        }


        public ICommand RenameParalelkaCommand
        {
            get
            {
                if (this.renameParalelkaCommand == null)
                {
                    this.renameParalelkaCommand = new RelayCommand(this.HandleRenameParalelkaCommand);
                }
                return this.renameParalelkaCommand;
            }
        }
        public ICommand AddParalelkaCommand
        {
            get
            {
                if (this.addParalelkaCommand == null)
                {
                    this.addParalelkaCommand = new RelayCommand(this.HandleAddParalelkaCommand);
                }
                return this.addParalelkaCommand;
            }
        }
        public ICommand RemoveParalelkaCommand
        {
            get
            {
                if (this.removeParalelkaCommand == null)
                {
                    this.removeParalelkaCommand = new RelayCommand(this.HandleRemoveParalelkaCommand);
                }
                return this.removeParalelkaCommand;
            }
        }
        
        
        private void HandleRenameGenreCommand(object parameter)
        {
            int genre = this.SelectedGenre;
            if(CheckSelectedGenre(genre))
            {
                TextInputViewModel vm = new TextInputViewModel("Преименуване на жанр");
                vm.InputText = GenreTools.TextFromNumber(this.SelectedGenre);
                var win = new TextInputPage();
                win.DataContext = vm;
                win.ShowDialog();
                if (!vm.Cancelled)
                {
                    bool sureRenameGenre = MessageBox.Show(String.Format("Жанрът \"{0}\" ще бъде преименуван на \"{1}\"! Продължаване?", GenreTools.TextFromNumber(this.SelectedGenre), vm.InputText),
                        "Потвърждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
                    if (sureRenameGenre)
                    {
                        try
                        {
                            DataInserter.RenameGenre(this.SelectedGenre, vm.InputText);
                            MessageBox.Show("Жанрът беше преименуван успешно!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.AllGenres = null;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Изникна проблем. Моля затворете базата данни и опитайте отново.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }
        private void HandleAddGenreCommand(object parameter)
        {
            TextInputViewModel vm = new TextInputViewModel("Добавяне на жанр");
            var win = new TextInputPage();
            win.DataContext = vm;
            win.ShowDialog();

            if(!vm.Cancelled)
            {
                try
                {
                    var genres = DataPersister.GetAllGenres();
                    int index = genres[genres.Count - 1].Index;
                    DataInserter.AddGenre(vm.InputText, index + 1);
                    MessageBox.Show("Жанрът е добавен успешно!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.AllGenres = null;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Изникна проблем. Моля затворете базата данни и опитайте отново.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void HandleRemoveGenreCommand(object parameter)
        {
            int genre = this.SelectedGenre;
            if (CheckSelectedGenre(genre))
            {
                if (this.SelectedGenre == 0)
                {
                    MessageBox.Show("Не може да премахнете този жанр!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                bool sureRemoveGenre = MessageBox.Show(String.Format("Жанрът \"{0}\" ще бъде премахнат! Продължаване?", GenreTools.TextFromNumber(this.SelectedGenre)),
                        "Потвърждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
                if (sureRemoveGenre)
                {
                    try
                    {

                        DataInserter.DeleteGenre(this.SelectedGenre);
                        DataInserter.DecreaseGenresInAllTables(this.SelectedGenre);
                        MessageBox.Show("Жанрът е премахнат успешно!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.AllGenres = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Изникна проблем. Моля затворете базата данни и опитайте отново.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void HandleRenameParalelkaCommand(object parameter)
        {
            int par = this.SelectedParalelka;
            if (CheckSelectedParalelka(par))
            {
                TextInputViewModel vm = new TextInputViewModel("Преименуване на паралелка");
                vm.InputText = ParalelkiTools.CharFromNumber(this.SelectedParalelka);
                var win = new TextInputPage();
                win.DataContext = vm;
                win.ShowDialog();
                if (!vm.Cancelled)
                {
                    bool sureRenamePar = MessageBox.Show(String.Format("Паралелка \"{0}\" ще бъде преименувана на \"{1}\"! Продължаване?", ParalelkiTools.CharFromNumber(this.SelectedParalelka), vm.InputText),
                        "Потвърждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
                    if (sureRenamePar)
                    {
                        try
                        {
                            DataInserter.RenameParalelka(this.SelectedParalelka, vm.InputText);
                            MessageBox.Show("Паралелката беше преименувана успешно!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.AllParalelki = null;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Изникна проблем. Моля затворете базата данни и опитайте отново.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }
        private void HandleAddParalelkaCommand(object parameter)
        {
            TextInputViewModel vm = new TextInputViewModel("Добавяне на паралелка");
            var win = new TextInputPage();
            win.DataContext = vm;
            win.ShowDialog();

            if (!vm.Cancelled)
            {
                try
                {
                    var paralelki = DataPersister.GetAllParalelki();
                    int index = paralelki[paralelki.Count - 1].Index;
                    DataInserter.AddParalelka(vm.InputText, index + 1);
                    MessageBox.Show("Паралелката е добавена успешно!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.AllParalelki = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Изникна проблем. Моля затворете базата данни и опитайте отново.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void HandleRemoveParalelkaCommand(object parameter)
        {
            int par = this.SelectedParalelka;
            if (CheckSelectedParalelka(par))
            {
                if (DataPersister.StudentsInParalelka(this.SelectedParalelka))
                {
                    MessageBox.Show("Не може да премахнете тази паралелка, защото има читатели към нея!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                bool sureRemoveParalelka = MessageBox.Show(String.Format("Паралелка \"{0}\" ще бъде премахната! Продължаване?", ParalelkiTools.CharFromNumber(this.SelectedParalelka)),
                        "Потвърждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
                if (sureRemoveParalelka)
                {
                    try
                    {

                        DataInserter.DeleteParalelka(this.SelectedParalelka);
                        DataInserter.DecreaseParalelki(this.SelectedParalelka);
                        MessageBox.Show("Паралелката е премахната успешно!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.AllParalelki = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Изникна проблем. Моля затворете базата данни и опитайте отново.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }


        private bool CheckSelectedGenre(int genre)
        {
            if (genre == -1 && this.AllGenres.Count > 1)
            {
                MessageBox.Show("Моля изберете жанр!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (this.AllGenres.Count == 0)
            {
                MessageBox.Show("Няма налични жанрове!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (genre == -1 && this.AllGenres.Count == 1)
            {
                this.SelectedGenre = 0;
                return true;
            }
            return true;
        }
        private bool CheckSelectedParalelka(int par)
        {
            if (par == -1 && this.AllParalelki.Count > 1)
            {
                MessageBox.Show("Моля изберете паралелка!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (this.AllParalelki.Count == 0)
            {
                MessageBox.Show("Няма налични паралелки!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (par == -1 && this.AllParalelki.Count == 1)
            {
                this.SelectedParalelka = 0;
                return true;
            }
            return true;
        }  
    }
}
