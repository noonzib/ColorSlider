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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorSlide
{
    /// <summary>
    /// DynamicNumberControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DynamicNumberControl : UserControl
    {
        public DynamicNumberControl()
        {
            InitializeComponent();
        }

        public void Move(int target)
        {
            Storyboard storyboard = FindResource("NumberSlideStoryboard1") as Storyboard;
            ThicknessAnimationUsingKeyFrames thicknessAnimationUsingKeyFrames = storyboard.Children[0] as ThicknessAnimationUsingKeyFrames;
            (thicknessAnimationUsingKeyFrames.KeyFrames[0] as SplineThicknessKeyFrame).Value = new Thickness(-8, target * -173, -8, 0);

            storyboard.Begin();
        }
    }
}
