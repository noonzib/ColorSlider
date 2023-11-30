using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
using System.Windows.Threading;

namespace ColorSlide
{
    /// <summary>
    /// SlidePanel.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SlidePanel : UserControl
    {
        string currentColor = "000000";
        DispatcherTimer timer;
        int _numberCount = 6;
        Popup myPopup;

        public string CurrentColor { get => currentColor; set => currentColor = value; }
        public int Seed { get => seed; set => seed = value; }

        public SlidePanel(int seed)
        {
            InitializeComponent();
            random = new Random(seed);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1800);
            timer.Tick += Timer_Tick;


            for (int i = 0; i < _numberCount; i++)
            {
                DynamicNumberControl dynamicNumberControl = new DynamicNumberControl();
                NumberStack.Children.Add(dynamicNumberControl);
            }

            this.Loaded += SlidePanel_Loaded; ;
            this.MouseDoubleClick += SlidePanel_MouseDoubleClick; ;
        }

        Random random;
        private void Timer_Tick(object sender, EventArgs e)
        {

            int red = random.Next(256);
            int green = random.Next(256);
            int blue = random.Next(256);

            string redValue = red.ToString("X2");
            string greenValue = green.ToString("X2");
            string blueValue = blue.ToString("X2");

            string colorString = redValue + greenValue + blueValue;
            CurrentColor = colorString;


            this.Resources["BackgroundColor"] = new SolidColorBrush(Color.FromRgb((byte)(red), (byte)(green), (byte)random.Next(blue)));
            //System.Diagnostics.Debug.WriteLine(colorString);

            StartAnimation(colorString);
        }


        private void SlidePanel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(CurrentColor);
            myPopup = new Popup();
            TextBlock popupText = new TextBlock();
            popupText.Text = "Copied!";
            popupText.Background = Brushes.White;
            popupText.Foreground = this.Resources["BackgroundColor"] as Brush;
            popupText.FontSize = 18;
            popupText.FontWeight = FontWeights.Bold;
            popupText.Padding = new Thickness(4);
            myPopup.Child = popupText;
            myPopup.StaysOpen = false;

            myPopup.AllowsTransparency = true;
            myPopup.PopupAnimation = PopupAnimation.Fade;
            myPopup.Placement = PlacementMode.MousePoint;
            myPopup.Opened += Popup_Opened;
            myPopup.IsOpen = true;


            MainGrid.Children.Add(myPopup);
        }
        private void Popup_Opened(object sender, EventArgs e)
        {
            StartCloseTimer();
        }

        private void StartCloseTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1d);
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            timer.Tick -= TimerTick;
            myPopup.IsOpen = false;
        }

        private void SlidePanel_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        int seed;


        private void StartAnimation(int[] colorNum)
        {
            for (int i = 0; i < _numberCount; i++)
            {
                (NumberStack.Children[i] as DynamicNumberControl).Move(colorNum[i]);
            }
        }

        private void StartAnimation(string rannum)
        {
            for (int i = 0; i < _numberCount; i++)
            {
                int target = Convert.ToInt32(rannum.Substring(i, 1), 16);
                (NumberStack.Children[i + 1] as DynamicNumberControl).Move(target);
            }
        }
    }
}
