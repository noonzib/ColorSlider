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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ColorSlide
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        string currentColor="000000";
        int _numberCount = 16;

        public string CurrentColor { get => currentColor; set => currentColor = value; }

        public MainWindow()
        {
            InitializeComponent();
            Random random = new Random();
            for(int i = 0; i < _numberCount; i++)
            {
                SlidePanel slidePanel = new SlidePanel(i* random.Next(9999999));
                MainGrid.Children.Add(slidePanel);
                //Thread.Sleep(400);
            }
        }


    }
}
