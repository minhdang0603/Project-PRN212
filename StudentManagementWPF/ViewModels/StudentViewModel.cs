using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentManagementWPF.ViewModels
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string _phone;
        private string _courseHours;
        private string _score;

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged("FirstName");
                }
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged("LastName");
                }
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    OnPropertyChanged("Phone");
                }
            }
        }

        public string CourseHours
        {
            get => _courseHours;
            set
            {
                if (_courseHours != value)
                {
                    _courseHours = value;
                    OnPropertyChanged("CourseHours");
                }
            }
        }

        public string Score
        {
            get => _score;
            set
            {
                if (Score != value)
                {
                    _score = value;
                    OnPropertyChanged("Score");
                }
            }
        }

        public StudentViewModel()
        {
            Phone = string.Empty;
            CourseHours = string.Empty;
            Score = string.Empty;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
