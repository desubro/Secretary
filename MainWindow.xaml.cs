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
using System.Data.OleDb;
using System.Data;

namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnEvents_Click(object sender, RoutedEventArgs e)
        {

            lblWhereWeAre.Content = "Мероприятия";
            eventGrid.Visibility = Visibility.Visible;
            birhtDayGrid.Visibility = Visibility.Hidden;
            btnNewEvent.Visibility = Visibility.Visible;
            newEventGrid.Visibility = Visibility.Hidden;
            configGrid.Visibility = Visibility.Hidden;
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=H:\\Технол.программ.2014\\ЛАБЫ\\WpfApplication1\\BD.accdb");
            con.Open();

            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * From Мероприятия;", con);
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            DataSet ds = new DataSet();

            da.Fill(ds, "Мероприятия");
            eventsGridData.ItemsSource = ds.Tables["Мероприятия"].DefaultView;
            con.Close();

        }

        private void btnBirthDay_Click(object sender, RoutedEventArgs e)
        {
            lblWhereWeAre.Content = "Дни Рождения";
            eventGrid.Visibility = Visibility.Hidden;
            birhtDayGrid.Visibility = Visibility.Visible;
            btnNewEvent.Visibility = Visibility.Hidden;
            newEventGrid.Visibility = Visibility.Hidden;
            configGrid.Visibility = Visibility.Hidden;

            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=H:\\Технол.программ.2014\\ЛАБЫ\\WpfApplication1\\BD.accdb");
            con.Open();

            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * From ДР;", con);
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            DataSet ds = new DataSet();

            da.Fill(ds, "ДР");
            birthDayGridData.ItemsSource = ds.Tables["ДР"].DefaultView;
            con.Close();
        }

        private void btnNewEvent_Click(object sender, RoutedEventArgs e)
        {
            lblWhereWeAre.Content = "Новое мероприятие";
            newEventGrid.Visibility = Visibility.Visible;
            configGrid.Visibility = Visibility.Hidden;



        }

        private void btnConfigs_Click(object sender, RoutedEventArgs e)
        {
            lblWhereWeAre.Content = "Настройки";
            eventGrid.Visibility = Visibility.Hidden;
            birhtDayGrid.Visibility = Visibility.Hidden;
            btnNewEvent.Visibility = Visibility.Hidden;
            newEventGrid.Visibility = Visibility.Hidden;
            configGrid.Visibility = Visibility.Visible;

        }

        private void mainWindow_Initialized(object sender, EventArgs e)
        {
            eventGrid.Visibility = Visibility.Hidden;
            birhtDayGrid.Visibility = Visibility.Hidden;
            btnNewEvent.Visibility = Visibility.Hidden;
            newEventGrid.Visibility = Visibility.Hidden;
            configGrid.Visibility = Visibility.Hidden;
        }

        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            lblWhereWeAre.Content = "Мероприятия";
            eventGrid.Visibility = Visibility.Visible;
            birhtDayGrid.Visibility = Visibility.Hidden;
            btnNewEvent.Visibility = Visibility.Hidden;
            newEventGrid.Visibility = Visibility.Hidden;
            configGrid.Visibility = Visibility.Hidden;
  
          

            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=H:\\Технол.программ.2014\\ЛАБЫ\\WpfApplication1\\BD.accdb");
            con.Open();
            String s1, s2, s3;
            s1 = tbEventName.Text.ToString();
            s2 = tbEventDescription.Text.ToString();
            s3 = dateOfEvent.Text.ToString() +" "+ tbHours.Text.ToString() +":"+ tbMinuts.Text.ToString();
           

            OleDbCommand cmd=new OleDbCommand("INSERT INTO Мероприятия (Дата, Название, Описание)" + " VALUES (?, ?, ?)", con);
            cmd.Parameters.Add("Дата",OleDbType.Date, 30).Value=s3;
            cmd.Parameters.Add("Название",OleDbType.VarWChar, 30).Value=s1;
            cmd.Parameters.Add("Описание",OleDbType.VarWChar, 30).Value=s2;            
            cmd.ExecuteNonQuery();
            con.Close();

        }


        private void btnNewEvent_DragEnter(object sender, DragEventArgs e)
        {

        }
    }
}
