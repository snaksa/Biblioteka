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
    public class SearchInAllStudents : SearchTools
    {
        enum SearchOptions
        {
            EGN = 0,
            Name = 1,
            ClassNo = 2
        };

        enum SearchClassOptions
        {
            a = 0,
            b = 1,
            v = 2
        };

        private List<Student> allStudentsList;
        private int selectedStudentPosition;
        private Student selectedStudentData;

        ICommand searchStudentCommand;
        ICommand chooseStudentCommand;

        public List<Student> AllStudents
        {
            get
            {
                if(this.allStudentsList == null)
                {
                    this.allStudentsList = DataPersister.GetAllStudents();
                }
                return this.allStudentsList;
            }
            set
            {
                this.allStudentsList = value;
                OnPropertyChanged("AllStudents");
            }
        }
        public int SelectedStudentPosition
        {
            get
            {
                return this.selectedStudentPosition;
            }
            set
            {
                this.selectedStudentPosition = value;
                OnPropertyChanged("SelectedStudent");
            }
        }
        public Student SelectedStudentData
        {
            get
            {
                return this.selectedStudentData;
            }
            set
            {
                this.selectedStudentData = value;
                OnPropertyChanged("SelectedStudentData");
            }
        }
        public List<string> Paralelki
        {
            get
            {
                return ParalelkiTools.GetOnlyCharacters();
            }
        }

        public ICommand SearchStudentCommand
        {
            get
            {
                if(this.searchStudentCommand == null)
                {
                    this.searchStudentCommand = new RelayCommand(this.HandleSearchCommand);
                }
                return this.searchStudentCommand;
            }
        }
        public ICommand ChooseStudentCommand
        {
            get
            {
                if (this.chooseStudentCommand == null)
                {
                    this.chooseStudentCommand = new RelayCommand(this.HandleChooseStudentCommand);
                }
                return this.chooseStudentCommand;
            }
        }

        private void HandleSearchCommand(object parameter)
        {
            int option = this.SearchOption;

            if (option == (int)SearchOptions.EGN)
            {
                string searchKey = this.SearchValue;
                searchKey = searchKey.Trim();

                if(!DataValidator.IsValidString(searchKey))
                {
                    this.AllStudents = null;
                    return;
                }

                SearchStudentByEGN(searchKey);
            }
            else if (option == (int)SearchOptions.Name)
            {
                string searchKey = this.SearchValue;
                searchKey = searchKey.Trim();
                if (!DataValidator.IsValidString(searchKey))
                {
                    this.AllStudents = null;
                    return;
                }
                SearchStudentByName(searchKey);
            }
            else
            {
                SearchStudentByClassAndParalelka();
            }
        }
        private void HandleChooseStudentCommand(object parameter)
        {
            int pos = this.SelectedStudentPosition;
            if (pos == -1 && this.AllStudents.Count > 1)
            {
                MessageBox.Show("Моля изберете ученик!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }
            else if (this.AllStudents.Count == 0)
            {
                MessageBox.Show("Няма налични ученици!", "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (this.AllStudents.Count == 1) this.SelectedStudentPosition = 0;

            this.selectedStudentData = this.AllStudents[this.SelectedStudentPosition];
            var win = parameter as Window;
            win.Close();
        }


        private void SearchStudentByEGN(string searchKey)
        {
            Student foundStudent = DataPersister.GetStudentByEGN(searchKey);
            if (foundStudent == null)
            {
                MessageBox.Show("Не е намерен ученик с това ЕГН!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                List<Student> newList = new List<Student>();
                newList.Add(foundStudent);
                this.AllStudents.Clear();
                this.AllStudents = newList;
            }
        }
        private void SearchStudentByName(string key)
        {
            key = key.Replace('.', ' ');
            key = key.Replace('\"', ' ');
            var searchWords = key.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<Student> oneMatchList = new List<Student>();
            List<Student> matchesList = new List<Student>();
            List<Student> allStudents = DataPersister.GetAllStudents();

            foreach (var student in allStudents)
            {
                string name = student.Name;
                name = name.Replace('.', ' ');
                var studentNames = name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int count = 0;

                foreach (var word in searchWords)
                {
                    foreach (var studentName in studentNames)
                    {
                        if (word.ToUpper().Equals(studentName.ToUpper()))
                        {
                            count++;
                        }
                    }
                }

                if (count == 1) oneMatchList.Add(student);
                else if (count > 1) matchesList.Add(student);
            }

            if (oneMatchList.Count == 0 && matchesList.Count == 0)
            {
                MessageBox.Show("Не е намерен ученик с такова име!");
            }
            else
            {
                matchesList.AddRange(oneMatchList);
                this.AllStudents = matchesList;
            }
        }
        private void SearchStudentByClassAndParalelka()
        {
            int classNo = this.SearchClassNumber + 1;
            int paral = this.SearchClassParalelka;

            List<Student> foundStudents = DataPersister.GetStudentByClass(classNo, paral);
            if (foundStudents.Count == 0)
            {
                MessageBox.Show("Няма намерени ученици!", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            this.AllStudents = foundStudents;
        }

    }
}
