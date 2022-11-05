using Q1.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Q1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PRN221_Spr22Context context = new PRN221_Spr22Context();

        public void Reload()
        {
            context = new PRN221_Spr22Context();

            rbMale.IsChecked = true;

            lvEmployees.ItemsSource = context.Employees.ToList();
        }

        public MainWindow()
        {
            InitializeComponent();

            rbMale.IsChecked = true;

            lvEmployees.ItemsSource = context.Employees.ToList();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Reload();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee();
            employee.Name = txtEmployeeName.Text;
            employee.Gender = rbMale.IsChecked.Value ? "Male" : "Female";
            employee.Dob = dpDob.SelectedDate.Value;
            employee.Phone = txtPhone.Text;
            employee.Idnumber = txtIDNumber.Text;

            context.Employees.Add(employee);
            context.SaveChanges();

            Reload();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            int id = Int32.Parse(txtEmployeeId.Text);

            Employee employee = context.Employees.FirstOrDefault(e => e.Id == id);

            employee.Name = txtEmployeeName.Text;
            employee.Gender = rbMale.IsChecked.Value ? "Male" : "Female";
            employee.Dob = dpDob.SelectedDate.Value;
            employee.Phone = txtPhone.Text;
            employee.Idnumber = txtIDNumber.Text;

            context.Employees.Update(employee);
            context.SaveChanges();

            Reload();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = Int32.Parse(txtEmployeeId.Text);

            Employee employee = context.Employees.FirstOrDefault(e => e.Id == id);

            context.Employees.Remove(employee);
            context.SaveChanges();

            Reload();
        }

        private void listView_Click(object sender, RoutedEventArgs e)
        {
            Employee item = (Employee)(sender as ListView).SelectedItem;
            
            if(item != null)
            {
                if (item.Gender.Equals("Male"))
                {
                    rbMale.IsChecked = true;
                    rbFemale.IsChecked = false;
                }
                else if (item.Gender.Equals("Female"))
                {
                    rbMale.IsChecked = false;
                    rbFemale.IsChecked = true;
                }
            }
        }
    }
}
