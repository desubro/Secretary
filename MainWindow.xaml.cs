using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Speech.Synthesis;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Vladislav\Desktop\BD.accdb

//added
using System.Windows.Threading;
namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Create an instance of DispatcherTimer
        private DispatcherTimer dT = new DispatcherTimer();
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        DispatcherTimer timer;
        String path = "C:\\Users\\Vladislav\\Desktop\\BD.accdb";
        public MainWindow()
        {
            InitializeComponent();
            //Sets the time
            timer = new DispatcherTimer();
            synthesizer.Rate = 3;
            timer.Interval = TimeSpan.FromSeconds(1.00);
            timer.Start();
            timer.Tick += new EventHandler(delegate (object s, EventArgs a)
            {
                time.Text = DateTime.Now.ToString("HH:mm:ss");
            });

            //Sets the date
            date.Text = DateTime.Now.ToShortDateString();
        }

        private void btnEvents_Click(object sender, RoutedEventArgs e)
        {
            lblWhereWeAre.Content = "Мероприятия";
            eventGrid.Visibility = Visibility.Visible;
            birhtDayGrid.Visibility = Visibility.Hidden;
            newEventGrid.Visibility = Visibility.Hidden;
            configGrid.Visibility = Visibility.Hidden;
            btnNewEvent.Visibility = Visibility.Visible;

            CDataBase.TableWrite(path, "Мероприятия", eventsGridData);


        }

        private void btnBirthDay_Click(object sender, RoutedEventArgs e)
        {
            lblWhereWeAre.Content = "Дни Рождения";
            eventGrid.Visibility = Visibility.Hidden;
            birhtDayGrid.Visibility = Visibility.Visible;
            newEventGrid.Visibility = Visibility.Hidden;
            configGrid.Visibility = Visibility.Hidden;
            btnNewEvent.Visibility = Visibility.Hidden;

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
            newEventGrid.Visibility = Visibility.Hidden;
            configGrid.Visibility = Visibility.Visible;
            btnNewEvent.Visibility = Visibility.Hidden;

        }

        private void mainWindow_Initialized(object sender, EventArgs e)
        {
            CDataBase.TableWrite(path, "Мероприятия", homeEventsGridData);
            eventGrid.Visibility = Visibility.Hidden;
            birhtDayGrid.Visibility = Visibility.Hidden;
            newEventGrid.Visibility = Visibility.Hidden;
            configGrid.Visibility = Visibility.Hidden;
            btnNewEvent.Visibility = Visibility.Hidden;
        }

        private void btnNewEvent_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void btnHomePage_Click(object sender, RoutedEventArgs e)
        {
            lblWhereWeAre.Content = "Добро пожаловать";
            eventGrid.Visibility = Visibility.Hidden;
            birhtDayGrid.Visibility = Visibility.Hidden;
            newEventGrid.Visibility = Visibility.Hidden;
            configGrid.Visibility = Visibility.Hidden;
            btnNewEvent.Visibility = Visibility.Hidden;

            CDataBase.TableWrite(path, "Мероприятия", homeEventsGridData);

        }

        private void Window1_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //Set the interval of the Tick event to 1 sec
            dT.Interval = new TimeSpan(0, 0, 1);

            //Start the DispatcherTimer
            dT.Start();
            secondArc.StartAngle = (DateTime.Now.Second * 6);
            minuteArc.StartAngle = (DateTime.Now.Minute * 6);
            hourArc.StartAngle = (DateTime.Now.Hour * 30) + (DateTime.Now.Minute * 0.5);
            synthesizer.Speak("Добро пожаловать! Я ваш электронный секретарь!");

        }

        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            String date, name, description;
            name = tbEventName.Text.ToString();
            description = tbEventDescription.Text.ToString();
            date = dateOfEvent.Text.ToString() + " " + tbHours.Text.ToString() + ":" + tbMinuts.Text.ToString();

            CDataBase.insertEvent(path, date, name, description);

            lblWhereWeAre.Content = "Мероприятия";
            eventGrid.Visibility = Visibility.Visible;
            birhtDayGrid.Visibility = Visibility.Hidden;
            newEventGrid.Visibility = Visibility.Hidden;
            btnNewEvent.Visibility = Visibility.Visible;

            CDataBase.TableWrite(path, "Мероприятия", eventsGridData);
        }

        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("http://miet.ru/");
        }
    }
}
