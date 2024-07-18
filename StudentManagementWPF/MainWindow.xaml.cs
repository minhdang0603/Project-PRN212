using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BussinessObject;
using DataAccess.Repository;
using StudentManagementWPF.ViewModels;

namespace StudentManagementWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IStudentRepository studentRepository;
        private ICourseRepository courseRepository;
        private IScoreRepository scoreRepository;
        public StudentViewModel StudentViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            studentRepository = new StudentRepository();
            courseRepository = new CourseRepository();
            scoreRepository = new ScoreRepository();

            StudentViewModel = new StudentViewModel();
            this.DataContext = StudentViewModel;
        }

        private void tbControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (tbControl.SelectedItem is TabItem tabItem)
                {
                    switch (tabItem.Header.ToString())
                    {
                        case "Student":
                            LoadStudents();
                            break;
                        case "Course":
                            LoadCourses();
                            break;
                        case "Report":
                            LoadScore();
                            break;
                    }
                }
            }
        }

        private void LoadStudents()
        {
            try
            {
                var list = studentRepository.GetStudents();
                dgStudent.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadCourses()
        {
            try
            {
                var list = courseRepository.GetCourses();
                dgCourse.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadScore()
        {
            try
            {
                var studentList = studentRepository.GetStudents();
                var courseList = courseRepository.GetCourses();
                cboCourses.ItemsSource = courseList;
                cboStudents.ItemsSource = studentList;
                cboCourses.SelectedValuePath = "CourseId";
                cboCourses.DisplayMemberPath = "CourseName";
                cboCourses.SelectedIndex = 1;
                cboStudents.SelectedValuePath = "StudentId";
                cboStudents.DisplayMemberPath = "FullName";
                cboStudents.SelectedIndex = 1;
                LoadScoreByCourse(int.Parse(cboCourses.SelectedValue.ToString()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void LoadScoreByCourse(int courseId)
        {
            try
            {
                var scoreList = scoreRepository.GetScoresByCourse(courseId);
                dgScore.ItemsSource = scoreList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void ClearStudent()
        {
            txtStudentId.Text = string.Empty;
            txtFristName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAddress.Text = string.Empty;
            dpDob.SelectedDate = null;
            txtSearch.Text = string.Empty;
        }

        private void ClearCourse()
        {
            txtCourseId.Text = string.Empty;
            txtCourseName.Text = string.Empty;
            txtCourseHours.Text = string.Empty;
            txtCourseDescription.Text = string.Empty;
        }

        private void ClearScore()
        {
            txtScore.Text = string.Empty;
            txtScoreDesciption.Text = string.Empty;
        }

        #region StudentCRUD
        private void dgStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is DataGrid)
            {
                var student = (sender as DataGrid).SelectedItem as Student;
                if (student != null)
                {
                    DateTime.TryParse(student.DateOfBirth.ToString(), out var date);
                    txtStudentId.Text = student.StudentId.ToString();
                    txtFristName.Text = student.FirstName;
                    txtLastName.Text = student.LastName;
                    txtPhone.Text = student.Phone;
                    dpDob.SelectedDate = date;
                    txtAddress.Text = student.Address;
                }
            }
        }

        private void btnAddStudet_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtStudentId.Text))
            {
                MessageBox.Show("Student already exist!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFristName.Text) ||
                string.IsNullOrEmpty(txtLastName.Text) ||
                !Regex.IsMatch(txtPhone.Text, @"^0\d{9}$"))
            {
                MessageBox.Show("Please correct the errors before adding.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            DateOnly.TryParse(dpDob.Text, out var selectedDate);

            if (selectedDate >= currentDate)
            {
                MessageBox.Show("Selected date cannot exceed today", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int age = currentDate.Year - selectedDate.Year;
            if (age < 1 || age > 100)
            {
                MessageBox.Show("Age must be in the range from 1 to 100", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                Student student = new Student()
                {
                    FirstName = txtFristName.Text,
                    LastName = txtLastName.Text,
                    Phone = txtPhone.Text,
                    Address = txtAddress.Text,
                    DateOfBirth = selectedDate
                };
                studentRepository.AddStudent(student);
                MessageBox.Show("Add student successfully", "Sucess", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearStudent();
                LoadStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void btnUpdateStudent_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudentId.Text))
            {
                MessageBox.Show("Please choose a student to edit!", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(StudentViewModel.FirstName) ||
                string.IsNullOrEmpty(StudentViewModel.LastName) ||
                !Regex.IsMatch(StudentViewModel.Phone, @"^0\d{9}$"))
            {
                MessageBox.Show("Please correct the errors before adding.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            DateOnly.TryParse(dpDob.Text, out var selectedDate);

            if (selectedDate >= currentDate)
            {
                MessageBox.Show("Selected date cannot exceed today", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int age = currentDate.Year - selectedDate.Year;
            if (age < 1 || age > 100)
            {
                MessageBox.Show("Age must be in the range from 1 to 100", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var student = studentRepository.GetStudentById(int.Parse(txtStudentId.Text));
                student.FirstName = txtFristName.Text;
                student.LastName = txtLastName.Text;
                student.Address = txtAddress.Text;
                student.Phone = txtPhone.Text;
                student.DateOfBirth = selectedDate;
                studentRepository.UpdateStudent(student);
                dgStudent.SelectedItem = null;
                LoadStudents();
                ClearStudent();
                MessageBox.Show($"Update student {student.StudentId} successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnDeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudentId.Text))
            {
                MessageBox.Show("Please choose a student to delete!", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show($"Do you want to delete student {txtLastName.Text} {txtFristName.Text}. The student score will also be deleted too.", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                return;
            }

            try
            {
                var student = studentRepository.GetStudentById(int.Parse(txtStudentId.Text));
                studentRepository.DeleteStudent(student);
                dgStudent.SelectedItem = null;
                LoadStudents();
                ClearStudent();
                var name = $"{txtFristName.Text} {txtLastName.Text}";
                MessageBox.Show($"Delete student {name} successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnSearchStudent_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Please enter a student's name!", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var list = studentRepository.FindStudentByName(txtSearch.Text);
                dgStudent.SelectedItem = null;
                ClearStudent();
                dgStudent.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefreshStudent_Click(object sender, RoutedEventArgs e)
        {
            ClearStudent();
            LoadStudents();
        }
        #endregion

        #region CourseCRUD
        private void dgCourse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is DataGrid)
            {
                var course = (sender as DataGrid).SelectedItem as Course;
                if (course != null)
                {
                    txtCourseId.Text = course.CourseId.ToString();
                    txtCourseName.Text = course.CourseName;
                    txtCourseHours.Text = course.CourseHours.ToString();
                    txtCourseDescription.Text = course.CourseDescription;
                }
            }
        }

        private void btnAddCourse_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCourseId.Text))
            {
                MessageBox.Show("This course already existed!", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCourseName.Text))
            {
                MessageBox.Show("Please enter course's name", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtCourseHours.Text, out var courseHourses))
            {
                MessageBox.Show("Please correct the errors before adding.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                Course course = new Course()
                {
                    CourseName = txtCourseName.Text,
                    CourseHours = courseHourses,
                    CourseDescription = txtCourseDescription.Text
                };
                courseRepository.AddCourse(course);
                LoadCourses();
                ClearCourse();
                MessageBox.Show("Add course successfully", "Sucess", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnUpdateCourse_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCourseId.Text))
            {
                MessageBox.Show("Please choose a course to delete!", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCourseName.Text))
            {
                MessageBox.Show("Please enter course's name", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtCourseHours.Text, out var courseHourses))
            {
                MessageBox.Show("Please correct the errors before adding.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var course = courseRepository.GetCourseById(int.Parse(txtCourseId.Text));
                course.CourseHours = courseHourses;
                course.CourseName = txtCourseName.Text;
                course.CourseDescription = txtCourseDescription.Text;
                courseRepository.UpdateCourse(course);
                LoadCourses();
                ClearCourse();
                MessageBox.Show("Update course successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteCourse_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCourseId.Text))
            {
                MessageBox.Show("Please choose a course to delete!", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            MessageBoxResult result = MessageBox.Show("Do you want to delete this course! The course information will be deleted too!", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                return;
            }

            try
            {
                var course = courseRepository.GetCourseById(int.Parse(txtCourseId.Text));
                courseRepository.DeleteCourse(course);
                LoadCourses();
                ClearCourse();
                MessageBox.Show("Delete course successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnRefreshCourse_Click(object sender, RoutedEventArgs e)
        {
            ClearCourse();
        }
        #endregion

        #region ScoreCRUD
        private void dgScore_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.Source is DataGrid)
            {
                var score = (sender as DataGrid).SelectedItem as Score;
                if (score != null)
                {
                    cboCourses.SelectedValue = score.CourseId;
                    cboStudents.SelectedValue = score.StudentId;
                    txtScore.Text = score.StudentScore.ToString();
                    txtScoreDesciption.Text = score.StudentDescription;
                }
            }
        }

        private void cboCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = cboCourses.SelectedItem as Course;
            if(selected != null)
            {
                LoadScoreByCourse(selected.CourseId);
            }
        }

        private void btnAddScore_Click(object sender, RoutedEventArgs e)
        {
            if(!decimal.TryParse(txtScore.Text, out var score))
            {
                MessageBox.Show("Please enter the score to add", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return ;
            }

            if(score < 0 || score > 10)
            {
                MessageBox.Show("Score must be in the range from 0 to 10", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                int courseId = int.Parse(cboCourses.SelectedValue.ToString());
                int studetnId = int.Parse(cboStudents.SelectedValue.ToString());
                var studentScore = scoreRepository.GetScoreByCourseIdAndStudentId(courseId, studetnId);
                if(studentScore != null)
                {
                    MessageBox.Show($"Student {studentScore.Student.FullName} already have score in course {studentScore.Course.CourseName}", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Score newScore = new Score()
                {
                    CourseId = courseId,
                    StudentId =studetnId,
                    StudentScore = score,
                    StudentDescription = txtScoreDesciption.Text
                };

                scoreRepository.AddScore(newScore);
                LoadScoreByCourse(courseId);
                ClearScore();
                MessageBox.Show("Add score successful", "Sucess", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnUpdateScore_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgScore.SelectedItem as Score;
            if (selected == null)
            {
                MessageBox.Show("Please choose a student score to edit", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(txtScore.Text, out var score))
            {
                MessageBox.Show("Please enter the score to add", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (score < 0 || score > 10)
            {
                MessageBox.Show("Score must be in the range from 0 to 10", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var editScore = scoreRepository.GetScoreByCourseIdAndStudentId(selected.CourseId, selected.StudentId);
                editScore.StudentScore = score;
                editScore.StudentDescription = txtScoreDesciption.Text;
                scoreRepository.UpdateScore(editScore);
                ClearScore();
                LoadScoreByCourse(selected.CourseId);
                MessageBox.Show("Edit score successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnDeleteScore_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgScore.SelectedItem as Score;
            if (selected == null)
            {
                MessageBox.Show("Please choose a student score to delete", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Do you want to delete this student's score?", "Delete", MessageBoxButton.YesNo,MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                return;
            }

            try
            {
                var score = scoreRepository.GetScoreByCourseIdAndStudentId(selected.CourseId, selected.StudentId);
                scoreRepository.DeleteScore(score);
                ClearScore();
                LoadScoreByCourse(selected.CourseId);
                MessageBox.Show("Delete student's score successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnRefreshScore_Click(object sender, RoutedEventArgs e)
        {
            ClearScore();
        }
        #endregion

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}