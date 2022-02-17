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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutoLotModel;
using System.Data.Entity;
using System.Data;

namespace WpfApplication
{
    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {
         AutoLotEntitiesModel ctx = new AutoLotEntitiesModel();
        CollectionViewSource employeeVSource;
        CollectionViewSource timetrackingVSource;
        CollectionViewSource employeeClockingsVSource;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource employeeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("employeeViewSource")));
            System.Windows.Data.CollectionViewSource timetrackingViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("timetrackingViewSource")));
            employeeVSource =
((System.Windows.Data.CollectionViewSource)(this.FindResource("employeeViewSource")));
            employeeVSource.Source = ctx.Employees.Local;
            ctx.Employees.Load();

            timetrackingVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("timetrackingViewSource")));
            timetrackingVSource.Source = ctx.Timetrackings.Local;
            ctx.Timetrackings.Load();

            employeeClockingsVSource =
((System.Windows.Data.CollectionViewSource)(this.FindResource("employeeClockingsViewSource")));

          //  employeeClockingsVSource.Source = ctx.Clockings.Local;
            ctx.Clockings.Load();
            ctx.Timetrackings.Load();
            cmbEmployee.ItemsSource = ctx.Employees.Local;
           // cmbEmployee.DisplayMemberPath = "Firstname";
            cmbEmployee.SelectedValuePath = "EmpId";
            cmbTimetracking.ItemsSource = ctx.Timetrackings.Local;
            //cmbTimetracking.DisplayMemberPath = "Month";
            cmbTimetracking.SelectedValuePath = "TimeId";
            BindDataGrid();
        }

        private void btnNewTime_Click(object sender, RoutedEventArgs e)
        {
            SetValidationBinding();

            Timetracking timetracking = null;
            try
            {
                //instantiem Customer entity
                timetracking = new Timetracking()
                {
                    Hour = hourTextBox.Text.Trim(),
                    Month = monthTextBox.Text.Trim(),
                    Salary = salaryTextBox.Text.Trim()
                };
                //adaugam entitatea nou creata in context
                ctx.Timetrackings.Add(timetracking);
                timetrackingVSource.View.Refresh();
                //salvam modificarile
                ctx.SaveChanges();
            }
            //using System.Data;
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message);
            }
            SetValidationBinding();

        }
        private void btnNewEmp_Click(object sender, RoutedEventArgs e)
        {
            SetValidationBinding();

            Employee employee = null;
            try
            {
                //instantiem Customer entity
                employee = new Employee()
                {
                    Firstname = firstnameTextBox.Text.Trim(),
                    Lastname = lastnameTextBox.Text.Trim(),
                    Phone = phoneTextBox.Text.Trim()
                };
                //adaugam entitatea nou creata in context
                ctx.Employees.Add(employee);
                employeeVSource.View.Refresh();
                //salvam modificarile
                ctx.SaveChanges();
            }
            //using System.Data;
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message);
            }
            SetValidationBinding();

        }
        private void btnSaveTime_Click(object sender, RoutedEventArgs e)
        {
            SetValidationBinding();
            Timetracking timetracking = null;
            try
            {

                timetracking = (Timetracking)timetrackingDataGrid.SelectedItem;
                timetracking.Hour= hourTextBox.Text.Trim();
                timetracking.Month= monthTextBox.Text.Trim();
                timetracking.Salary = salaryTextBox.Text.Trim();
                //salvam modificarile
                ctx.SaveChanges();
            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message);
            }
            SetValidationBinding();
        }
        private void btnSaveEmp_Click(object sender, RoutedEventArgs e)
        {
            SetValidationBinding();

            Employee employee = null;
            try
            {
                 
                employee = (Employee)employeeDataGrid.SelectedItem;
                employee.Firstname = firstnameTextBox.Text.Trim();
                employee.Lastname = lastnameTextBox.Text.Trim();
                employee.Phone = phoneTextBox.Text.Trim();
                //salvam modificarile
                ctx.SaveChanges();
            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message);
            }
            SetValidationBinding();

        }
        private void btnDeleteEmp_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = null;
            try
            {
                employee = (Employee)employeeDataGrid.SelectedItem;
                ctx.Employees.Remove(employee);
                ctx.SaveChanges();
            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message);
            }
            employeeVSource.View.Refresh();
        }
        private void btnDeleteTime_Click(object sender, RoutedEventArgs e)
        {
            Timetracking timetracking = null;
            try
            {
                timetracking = (Timetracking)timetrackingDataGrid.SelectedItem;
                ctx.Timetrackings.Remove(timetracking);
                ctx.SaveChanges();
            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message);
            }
            timetrackingVSource.View.Refresh();
        }

        private void btnNextEmp_Click(object sender, RoutedEventArgs e)
        {
            employeeVSource.View.MoveCurrentToNext();
        }
        private void btnNextTime_Click(object sender, RoutedEventArgs e)
        {
            timetrackingVSource.View.MoveCurrentToNext();
        }
        private void btnPreviousEmp_Click(object sender, RoutedEventArgs e)
        {
            employeeVSource.View.MoveCurrentToPrevious();
        }
        private void btnPreviousTime_Click(object sender, RoutedEventArgs e)
        {
            timetrackingVSource.View.MoveCurrentToPrevious();
        }
        private void btnNextClock_Click(object sender, RoutedEventArgs e)
        {
            employeeClockingsVSource.View.MoveCurrentToNext();
        }
        private void btnPreviousClock_Click(object sender, RoutedEventArgs e)
        {
            employeeClockingsVSource.View.MoveCurrentToPrevious();
        }


        private void newClockings(object sender, RoutedEventArgs e)
        {
            Clocking clocking = null;
            Employee employee= (Employee)cmbEmployee.SelectedItem;
            Timetracking timetracking = (Timetracking)cmbTimetracking.SelectedItem;

            clocking = new Clocking()
            {
                EmpId = employee.EmpId,
                TimeId = timetracking.TimeId
            };
            ctx.Clockings.Add(clocking);
            ctx.SaveChanges();
            BindDataGrid();

        }
        private void saveClockings(object sender, RoutedEventArgs e)
        {
            dynamic selectedClocking= clockingsDataGrid.SelectedItem;
            try
            {
                int curr_id = selectedClocking.ClockId;
                var editedClocking = ctx.Clockings.FirstOrDefault(s => s.ClockId == curr_id);
                if (editedClocking != null)
                {
                    editedClocking.EmpId = Int32.Parse(cmbEmployee.SelectedValue.ToString());
                    editedClocking.TimeId = Convert.ToInt32(cmbTimetracking.SelectedValue.ToString());
                    //salvam modificarile
                    ctx.SaveChanges();
                }
            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message);
            }
            BindDataGrid();
            // pozitionarea pe item-ul curent
            employeeClockingsVSource.View.MoveCurrentTo(selectedClocking);
        
    }
        private void DeleteClockings(object sender, RoutedEventArgs e)
        {
            try
            {
                dynamic selectedClocking = clockingsDataGrid.SelectedItem;
                int curr_id = selectedClocking.ClockId;
                var deletedClocking = ctx.Clockings.FirstOrDefault(s => s.ClockId == curr_id);
                if (deletedClocking != null)
                {
                    ctx.Clockings.Remove(deletedClocking);
                    ctx.SaveChanges();
                    MessageBox.Show("Clocking Deleted Successfully", "Message");
                    BindDataGrid();
                }
            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BindDataGrid()
        {
            var queryClocking = from clo in ctx.Clockings
                             join emp in ctx.Employees on clo.EmpId equals
                             emp.EmpId
                             join tim in ctx.Timetrackings on clo.TimeId
                 equals tim.TimeId
                             select new
                             {
                                 clo.ClockId,
                                 clo.TimeId,
                                 clo.EmpId,
                                 emp.Firstname,
                                 emp.Lastname,
                                 tim.Hour,
                                 tim.Month,
                                 tim.Salary
                             };
            employeeClockingsVSource.Source = queryClocking.ToList();
        }
        private void SetValidationBinding()
        {
            Binding firstnameValidationBinding = new Binding();
            firstnameValidationBinding.Source = employeeVSource;
            firstnameValidationBinding.Path = new PropertyPath("Firstname");
            firstnameValidationBinding.NotifyOnValidationError = true;
            firstnameValidationBinding.Mode = BindingMode.TwoWay;
            firstnameValidationBinding.UpdateSourceTrigger =
           UpdateSourceTrigger.PropertyChanged;
            //string required
            firstnameValidationBinding.ValidationRules.Add(new StringNotEmpty());
            firstnameTextBox.SetBinding(TextBox.TextProperty,
           firstnameValidationBinding);

            Binding phoneValidationBinding = new Binding();
            phoneValidationBinding.Source = employeeVSource;
            phoneValidationBinding.Path = new PropertyPath("Phone");
            phoneValidationBinding.NotifyOnValidationError = true;
            phoneValidationBinding.Mode = BindingMode.TwoWay;
            phoneValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            phoneValidationBinding.ValidationRules.Add(new StringNotEmpty());
            phoneTextBox.SetBinding(TextBox.TextProperty, phoneValidationBinding);

            Binding monthValidationBinding = new Binding();
            monthValidationBinding.Source = timetrackingVSource;
            monthValidationBinding.Path = new PropertyPath("Month");
            monthValidationBinding.NotifyOnValidationError = true;
            monthValidationBinding.Mode = BindingMode.TwoWay;
            monthValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            monthValidationBinding.ValidationRules.Add(new StringNotEmpty());
            monthTextBox.SetBinding(TextBox.TextProperty, monthValidationBinding);

            Binding hourValidationBinding = new Binding();
            hourValidationBinding.Source = timetrackingVSource;
            hourValidationBinding.Path = new PropertyPath("Hour");
            hourValidationBinding.NotifyOnValidationError = true;
            hourValidationBinding.Mode = BindingMode.TwoWay;
            hourValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            hourValidationBinding.ValidationRules.Add(new StringNotEmpty());
            hourTextBox.SetBinding(TextBox.TextProperty, hourValidationBinding);


            Binding salaryValidationBinding = new Binding();
            salaryValidationBinding.Source = timetrackingVSource;
            salaryValidationBinding.Path = new PropertyPath("Salary");
            salaryValidationBinding.NotifyOnValidationError = true;
            salaryValidationBinding.Mode = BindingMode.TwoWay;
            salaryValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            salaryValidationBinding.ValidationRules.Add(new StringNotEmpty());
            salaryTextBox.SetBinding(TextBox.TextProperty, salaryValidationBinding);


            Binding lastnameValidationBinding = new Binding();
            lastnameValidationBinding.Source = employeeVSource;
            lastnameValidationBinding.Path = new PropertyPath("Lastname");
            lastnameValidationBinding.NotifyOnValidationError = true;
            lastnameValidationBinding.Mode = BindingMode.TwoWay;
            lastnameValidationBinding.UpdateSourceTrigger =
           UpdateSourceTrigger.PropertyChanged;
            //string min length validator
            lastnameValidationBinding.ValidationRules.Add(new
           StringMinLengthValidator());
            lastnameTextBox.SetBinding(TextBox.TextProperty, lastnameValidationBinding); //setare binding nou
        }

    }
}
