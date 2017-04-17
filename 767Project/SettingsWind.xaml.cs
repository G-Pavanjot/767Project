using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for SettingsWind.xaml
    /// </summary>
    public partial class SettingsWind : Window
    {

        String StreamingProg; //broadcasting software name
        String SettingsPath = @"./Settings.txt"; //path to save the settings info


        /*
         * Open the settings window to allow the user to save their broadcasting software info
         */ 
        public SettingsWind()
        {
            InitializeComponent();
            if (File.Exists(SettingsPath)) //check if a settings file already exists
            {
                StreamReader settingsRead = File.OpenText(SettingsPath);
                txtName.Text = settingsRead.ReadLine(); //fill the textbox with the existing settings info
                settingsRead.Close();
            }
        }

        private void txtName_GotFocus(object sender, RoutedEventArgs e)
        {
            txtName.Text = ""; //delete the text inside the textbox when it is in focus (easy for user to type)
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            lblSaved.Visibility = Visibility.Visible;
            StreamingProg = txtName.Text; //obtain the broadcasting info name
            StreamWriter settingsSave = File.CreateText(SettingsPath); //write the broadcasting info name into the settings textfile at the SettingsPath
            settingsSave.Write(StreamingProg);
            settingsSave.Close();
            
            Delay();
        }


        //Delay the closing of the window so the user knows that it has been successfully saved
        private void Delay()
        {
            System.Threading.Thread.Sleep(500);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); //close the window
        }
    }
}
