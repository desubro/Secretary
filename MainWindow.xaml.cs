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

namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String path="H:\\Технол.программ.2014\\ЛАБЫ\\WpfApplication1\\BD.accdb";
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

            CDataBase.TableWrite(path, "Мероприятия", eventsGridData);


        }

        private void btnBirthDay_Click(object sender, RoutedEventArgs e)
        {
            lblWhereWeAre.Content = "Дни Рождения";
            eventGrid.Visibility = Visibility.Hidden;
            birhtDayGrid.Visibility = Visibility.Visible;
            btnNewEvent.Visibility = Visibility.Hidden;
            newEventGrid.Visibility = Visibility.Hidden;
            configGrid.Visibility = Visibility.Hidden;

            CDataBase.TableWrite(path, "ДР", birthDayGridData);
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
  
          


            String date, name, description;
            name = tbEventName.Text.ToString();
            description = tbEventDescription.Text.ToString();
            date = dateOfEvent.Text.ToString() + " " + tbHours.Text.ToString() + ":" + tbMinuts.Text.ToString();

            CDataBase.insertEvent(path, date, name, description);
           



        }


        private void btnNewEvent_DragEnter(object sender, DragEventArgs e)
        {

        }
    }
}
