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
    public class AddReaderViewModel : ViewModelBase
    {
        private string egn;
        private string name;
        private string address;
        private string serialNumber;
        private DateTime date;

        private int selectedClass;
        private int selectedParalelka;
        private int selectedPosition;
        private Visibility showClassChoice;

        ICommand addReader;
        ICommand closeWindow;
        ICommand searchStudent;
        SearchInAllStudents searchInAllStudentsViewModel;

        public AddReaderViewModel()
        {
            this.date = DateTime.Today;
            this.searchInAllStudentsViewModel = new SearchInAllStudents();
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
        public List<string> Paralelki
        {
            get
            {
                return ParalelkiTools.GetOnlyCharacters();
            }
        }

        public ICommand AddReader
        {
            get
            {
                if (this.addReader == null)
                {
                    this.addReader = new RelayCommand(this.HandleAddReaderCommand);
                }
                return this.addReader;
            }
        }
        public ICommand CloseWindow
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
        public ICommand SearchStudentCommand
        {
            get
            {
                if (this.searchStudent == null)
                {
                    this.searchStudent = new RelayCommand(this.HandleSearchStudent);
                }
                return this.searchStudent;
            }
        }



        private void HandleAddReaderCommand(object parameter)
        {
            string egn = this.EGN;
            string name = this.Name;
            string address = this.Address;
            string serialNumber = this.SerialNumber;

            int classNo = this.SelectedClass + 1;
            int paral = this.SelectedParalelka;
            if (this.SelectedPosition == 1)
            {
                classNo = 0;
                paral = 0;
            }
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
                else if (DataPersister.DatabaseContainsEGN(egn))
                {
                    mistakeFound = true;
                    strBuilder.Append("Вече има читател с това ЕГН!\n");
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
                else if (DataPersister.DatabaseContainsSerialNumber(serialNumber, "Readers"))
                {
                    mistakeFound = true;
                    strBuilder.Append("Вече има читател с този номер!\n");
                }
                if (chosenDate.Date > DateTime.Today.Date)
                {
                    mistakeFound = true;
                    strBuilder.Append("Датата, която сте избрали е от бъдещето. Моля изберете валидна дата!");
                }

                if (mistakeFound == true)
                {
                    MessageBox.Show(strBuilder.ToString(), "Некоректни данни", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    Reader reader = new Reader(egn, name, address, serialNumber, classNo, paral, chosenDate);

                    DataInserter.AddReader(reader);
                    MessageBox.Show("Читателят беше добавен успешно!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    var win = parameter as Window;
                    win.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Изникна проблем. Моля затворете базата данни и опитайте отново.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void HandleCloseWindowCommand(object parameter)
        {
            var win = parameter as Window;
            win.Close();
        }
        private void HandleSearchStudent(object parameter)
        {
            var win = new SearchAllStudentsPage();
            win.DataContext = this.searchInAllStudentsViewModel;
            win.ShowDialog();
            if (this.searchInAllStudentsViewModel.SelectedStudentData != null)
            {
                Student stud = this.searchInAllStudentsViewModel.SelectedStudentData;
                this.EGN = stud.EGN;
                this.Name = stud.Name;

                if (DataValidator.IsValidString(stud.Address)) this.Address = stud.Address;
                else this.Address = "няма информация";

                this.SelectedClass = stud.ClassNo - 1;
                this.SelectedParalelka = stud.Paralelka;
            }
        }

    }
}
