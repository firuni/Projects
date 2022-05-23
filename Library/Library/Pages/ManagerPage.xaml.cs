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

namespace Library.Pages
{
    public partial class ManagerPage : Page
    {
        private static Employee currentEmployee = new Employee();
        public ManagerPage(Employee EP)
        {
            InitializeComponent();
            DataContext = currentEmployee;
            currentEmployee = EP;
            using (LiteraryPleasureEntities LP = new LiteraryPleasureEntities())
            {
                EP = LP.Employees.Where(p => p.Код_сотрудника == currentEmployee.Код_сотрудника).FirstOrDefault();
                fioLabel.Content = currentEmployee.Фамилия + " " + currentEmployee.Имя + " " + currentEmployee.Отчество + " :3";
            }
        }
    }
}
