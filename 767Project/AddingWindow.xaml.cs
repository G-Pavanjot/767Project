using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace _767Project
{
    /// <summary>
    /// Interaction logic for AddingWindow.xaml
    /// </summary>
    public partial class AddingWindow : Window
    {

        DataGrid progLst; //the datagrid from the main window


        /*
         * Open the adding window so the user can enter their own custom program names that they wish to hide
         */ 
        public AddingWindow(DataGrid lst)
        {
            InitializeComponent();
            progLst = lst; //get access to the main window's datagrid
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); //close window
        }

        private void txtAdd_GotFocus(object sender, RoutedEventArgs e)
        {
            txtAdd.Text = ""; //delete the text inside the textbox when the user just gets focus on it (easier to use)
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            progLst.Items.Add(new { SelectProg = true, ProgName = txtAdd.Text, procID = -10 }); //add the program name to the main datagrid with it already being checked, since this process may not already be running, it is given a processID of -10

            progLst.Items.SortDescriptions.Add(new SortDescription("ProgName", ListSortDirection.Ascending)); //resort the datagrid
            this.Close(); //close window
        }
    }
}
