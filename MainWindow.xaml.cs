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
using System.Windows.Media.Animation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;

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
        int code, parse;
        String path = "C:\\Users\\Vladislav\\Desktop\\BD.accdb";
        public MainWindow()
        {
            InitializeComponent();
            //Sets the time
            timer = new DispatcherTimer();
            dateOfEvent.Value = DateTime.Now;
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
            btnNewEvent.Visibility = Visibility.Visible;
            newEventGrid.Visibility = Visibility.Hidden;
            configGrid.Visibility = Visibility.Hidden;
            birhtDayGrid.Visibility = Visibility.Hidden;
            homeGrid.Visibility = Visibility.Hidden;
            CDataBase.TableWrite(path, "Мероприятия", eventsGridData);
            slideAnimationTo(eventGrid);


        }

        private void btnBirthDay_Click(object sender, RoutedEventArgs e)
        {
            lblWhereWeAre.Content = "Дни Рождения";
            newEventGrid.Visibility = Visibility.Hidden;
            configGrid.Visibility = Visibility.Hidden;
            birhtDayGrid.Visibility = Visibility.Visible;
            homeGrid.Visibility = Visibility.Hidden;
            eventGrid.Visibility = Visibility.Hidden;
            CDataBase.TableWrite(path, "ДР", birthDayGridData);
            slideAnimationTo(birhtDayGrid);


        }

        private void btnNewEvent_Click(object sender, RoutedEventArgs e)
        {
            lblWhereWeAre.Content = "Новое мероприятие";
            newEventGrid.Visibility = Visibility.Visible;
            configGrid.Visibility = Visibility.Hidden;
            birhtDayGrid.Visibility = Visibility.Hidden;
            homeGrid.Visibility = Visibility.Hidden;
            slideAnimationFrom(eventGrid);
            slideAnimationTo(newEventGrid);
            tbEventName.Text = "Название";
            tbEventDescription.Text = "Описание";

        }

        private void btnConfigs_Click(object sender, RoutedEventArgs e)
        {
            configGrid.Visibility = Visibility.Visible;
            eventGrid.Visibility = Visibility.Hidden;
            birhtDayGrid.Visibility = Visibility.Hidden;
            newEventGrid.Visibility = Visibility.Hidden;
            lblWhereWeAre.Content = "Настройки";
            slideAnimationFrom(homeGrid);
            slideAnimationTo(configGrid);

        }

        private void mainWindow_Initialized(object sender, EventArgs e)
        {
            CDataBase.TableWriteLimit(path, "Мероприятия", homeEventsGridData);
            eventGrid.Visibility = Visibility.Hidden;
            birhtDayGrid.Visibility = Visibility.Hidden;
            newEventGrid.Visibility = Visibility.Hidden;
            configGrid.Visibility = Visibility.Hidden;
            btnNewEvent.Visibility = Visibility.Hidden;
            synthesizer.Rate = 2;
            synthesizer.Speak("Добро пожаловать! Я ваш электронный секретарь!");

        }

        private void btnHomePage_Click(object sender, RoutedEventArgs e)
        {

            lblWhereWeAre.Content = "Электронный секретарь";
            newEventGrid.Visibility = Visibility.Hidden;
            configGrid.Visibility = Visibility.Hidden;
            birhtDayGrid.Visibility = Visibility.Hidden;
            homeGrid.Visibility = Visibility.Visible;
            eventGrid.Visibility = Visibility.Hidden;
            CDataBase.TableWriteLimit(path, "Мероприятия", homeEventsGridData);
            slideAnimationTo(homeGrid);
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
        }

        //Добавление мероприятия
        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            String date, name, description;
            name = tbEventName.Text.ToString();
            description = tbEventDescription.Text.ToString();
            date = dateOfEvent.Text.ToString();

            CDataBase.insertEvent(path, date, name, description);

            lblWhereWeAre.Content = "Мероприятия";
            configGrid.Visibility = Visibility.Hidden;
            birhtDayGrid.Visibility = Visibility.Hidden;
            homeGrid.Visibility = Visibility.Hidden;
            eventGrid.Visibility = Visibility.Visible;
            CDataBase.TableWrite(path, "Мероприятия", eventsGridData);
            slideAnimationTo(eventGrid);
            slideAnimationFrom(newEventGrid);
        }

        //Переход на сайт миэт при клике по картинке
        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("http://miet.ru/");
        }

        //Удаление мероприятия
        private void btnDeleteEvent_Click(object sender, EventArgs e)
        {
            parse = eventsGridData.SelectedIndex;
            DataRowView dataRow = eventsGridData.SelectedValue as DataRowView;
            code = int.Parse(dataRow[0].ToString());
            var mbResult = MessageBox.Show("Вы действительно хотите удалить мероприятие?", "Удаление мероприятия",
         MessageBoxButton.YesNo);
            if (mbResult == MessageBoxResult.Yes)
            {
                CDataBase.EventRowDelete(path, code);
                CDataBase.TableWrite(path, "Мероприятия", eventsGridData);
            }

        }

        //Анимации панелей
        private void slideAnimationTo(Grid gridName)
        {
            Storyboard sb = new Storyboard();

            DoubleAnimation slide = new DoubleAnimation();
            slide.To = 0;
            slide.From = mainWindow.Width*1.5;
            slide.Duration = new Duration(TimeSpan.FromMilliseconds(300.0));

            // Set the target of the animation
            Storyboard.SetTarget(slide, gridName);
            Storyboard.SetTargetProperty(slide,
    new PropertyPath("RenderTransform.(TranslateTransform.X)"));
            // Kick the animation off
            sb.Children.Add(slide);
            sb.Begin();
        }
        private void slideAnimationFrom(Grid gridName)
        {
            Storyboard sb = new Storyboard();

            DoubleAnimation slide = new DoubleAnimation();
            slide.To = -mainWindow.Width * 1.5;
            slide.From = 0;
            slide.Duration = new Duration(TimeSpan.FromMilliseconds(300.0));

            // Set the target of the animation
            Storyboard.SetTarget(slide, gridName);
            Storyboard.SetTargetProperty(slide,
    new PropertyPath("RenderTransform.(TranslateTransform.X)"));
            // Kick the animation off
            sb.Children.Add(slide);
            sb.Begin();
        }
    }
}
