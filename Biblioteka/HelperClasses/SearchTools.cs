using Biblioteka.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Biblioteka.HelperClasses
{
    public class SearchTools : ViewModelBase
    {
        private int searchOption;
        private int searchClassNumber;
        private int searchClassParalelka;
        private string searchValue;

        private bool searchValueEnabled = true;
        private Visibility searchClassOptionsVisibility = Visibility.Hidden;

        public int SearchOption
        {
            get
            {
                return this.searchOption;
            }
            set
            {
                this.searchOption = value;
                if (value == 2)
                {
                    this.SearchClassOptionsVisibility = Visibility.Visible;
                    this.SearchValueEnabled = false;
                }
                else
                {
                    this.SearchClassOptionsVisibility = Visibility.Hidden;
                    this.SearchValueEnabled = true;
                }
                OnPropertyChanged("SearchOption");
            }
        }
        public int SearchClassNumber
        {
            get
            {
                return this.searchClassNumber;
            }
            set
            {
                this.searchClassNumber = value;
                OnPropertyChanged("SearchClassNumber");
            }
        }
        public int SearchClassParalelka
        {
            get
            {
                return this.searchClassParalelka;
            }
            set
            {
                this.searchClassParalelka = value;
                OnPropertyChanged("SearchClassParalelka");
            }
        }
        public string SearchValue
        {
            get
            {
                return this.searchValue;
            }
            set
            {
                this.searchValue = value;
                OnPropertyChanged("SearchValue");
            }
        }

        public bool SearchValueEnabled
        {
            get
            {
                return this.searchValueEnabled;
            }
            set
            {
                this.searchValueEnabled = value;
                OnPropertyChanged("SearchValueEnabled");
            }
        }
        public Visibility SearchClassOptionsVisibility
        {
            get
            {
                return this.searchClassOptionsVisibility;
            }
            set
            {
                this.searchClassOptionsVisibility = value;
                OnPropertyChanged("SearchClassOptionsVisibility");
            }
        }
    }
}
