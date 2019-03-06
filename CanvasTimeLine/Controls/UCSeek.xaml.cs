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

namespace CanvasTimeLine.Controls
{
    /// <summary>
    /// UCSeek.xaml 的交互逻辑
    /// </summary>
    public partial class UCSeek : UserControl
    {
        public UCSeek()
        {
            InitializeComponent();
        }

        private void Slider_MouseMove(object sender, MouseEventArgs e)
        {
            if(Mouse.LeftButton==MouseButtonState.Pressed)
            {
                Console.WriteLine(Math.Round((sender as Slider).Value));
            }
        }
        #region Propertity


        public int UCSeekMax
        {
            get { return (int)GetValue(UCSeekMaxProperty); }
            set { SetValue(UCSeekMaxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UCSeekMax.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UCSeekMaxProperty =
            DependencyProperty.Register("UCSeekMax", typeof(int), typeof(UCSeek), new PropertyMetadata(1000,UCSeekMaxCallBack));

        private static void UCSeekMaxCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ucseek = d as UCSeek;
            ucseek.slider.Maximum = (int)e.NewValue;
        }



        public int Position
        {
            get { return (int)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }
        /// <summary>
        /// UCSeek的当前值
        /// </summary>
        // Using a DependencyProperty as the backing store for Position.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(int), typeof(UCSeek), new PropertyMetadata(0,PositionCallBack));

        private static void PositionCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ucseek = d as UCSeek;
            ucseek.slider.Value = (int)e.NewValue;
        }


        /// <summary>
        /// In点
        /// </summary>
        public int InPoint
        {
            get { return (int)GetValue(InPointProperty); }
            set { SetValue(InPointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InPoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InPointProperty =
            DependencyProperty.Register("InPoint", typeof(int), typeof(UCSeek), new PropertyMetadata(0,InPointCallBack));

        private static void InPointCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ucseek = d as UCSeek;
            ucseek.timeLine.InPoint = (int)e.NewValue;
        }

        /// <summary>
        /// Out点
        /// </summary>
        public int OutPoint
        {
            get { return (int)GetValue(OutPointProperty); }
            set { SetValue(OutPointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OutPoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutPointProperty =
            DependencyProperty.Register("OutPoint", typeof(int), typeof(UCSeek), new PropertyMetadata(0,OutPointCallback));

        private static void OutPointCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ucseek = d as UCSeek;
            ucseek.timeLine.OutPoint = (int)e.NewValue;
        }



        public int Duration
        {
            get { return (int)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Duration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(int), typeof(UCSeek), new PropertyMetadata(1000,DurationCallBack));

        private static void DurationCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ucseek = d as UCSeek;
            ucseek.slider.Maximum = (int)e.NewValue;
        }





        #endregion
    }
}
