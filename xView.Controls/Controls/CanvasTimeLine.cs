using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Globalization;

namespace xView.Controls.Controls
{
    public class CanvasTimeLine:Canvas
    {
        static CanvasTimeLine()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CanvasTimeLine), new FrameworkPropertyMetadata(typeof(CanvasTimeLine)));
        }
        public CanvasTimeLine()
        {
            var shortLinebrush = (Brush)brushConverter.ConvertFrom("#FF6B6D6E");
            shortLine = new Pen(shortLinebrush, 0.5);
            this.SizeChanged += CanvasTimeLine_SizeChanged;
            MinHeight = 60;
            Height = 80;
        }

        private void CanvasTimeLine_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var canvas = sender as CanvasTimeLine;
            canvasWidth = (int)canvas.ActualWidth;
            canvasHeight=(int)canvas.ActualHeight;
            lineTop = 30;
            lineBottom = lineTop +10;
            longLineBottom = lineTop + 15;
            InvalidateVisual();
        }

        private void CanvasTimeLine_Loaded(object sender, RoutedEventArgs e)
        {

        }

        #region Layout
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            var width = this.Width;
            dc.DrawLine(horLine, new Point(0, lineTop), new Point(canvasWidth, lineTop));
            //当前值
            int Ypoint = 2;
            if ((Position > InPoint - 20 && Position < InPoint + 20) || (Position > OutPoint - 20 && Position < OutPoint + 20))
            {
                Ypoint += (int)ActualHeight-15;
            }
            dc.DrawText(GetFormattedTextByPosition(Position),GetPoint(Position, Ypoint));
            
           
            var times = Math.Ceiling(canvasWidth / Lineinterval / 5.0);
            for(int i=0;i<times;i++)
            {
                var startPoint = i * Lineinterval * 5;
                dc.DrawLine(longLine, new Point(startPoint, lineTop), new Point(startPoint, longLineBottom));
                //绘制时长
                dc.DrawText(GetFormattedTextByValue(startPoint), new Point(startPoint-5, longLineBottom+3));
                //
                for(int j=0;j<4;j++)
                {
                    startPoint += Lineinterval;
                    if(startPoint<ActualWidth)
                    {
                        dc.DrawLine(shortLine, new Point(startPoint, lineTop), new Point(startPoint, lineBottom));
                    }
                }
                
            }

            //inpoint
            var brush = InPoint == Position||OutPoint==Position ? overInPointLine : inPointLine;
            var point = GetPoint(InPoint, lineTop - 10);
            dc.DrawLine(brush, point, GetPoint(InPoint, lineTop + 10));
            dc.DrawLine(brush, point, new Point(point.X + 5, point.Y));
            dc.DrawLine(brush, Point.Add(point,new Vector(0,20)), new Point(point.X + 5, point.Y+20));
            dc.DrawText(GetFormattedTextByPosition(InPoint), GetPoint(InPoint, 2));
            //outpoint
            var outPoint = GetPoint(OutPoint, lineTop - 10);
            dc.DrawLine(brush, outPoint, GetPoint(OutPoint, lineTop + 10));
            dc.DrawLine(brush, outPoint, new Point(outPoint.X - 5, outPoint.Y));
            dc.DrawLine(brush, Point.Add(outPoint, new Vector(0, 20)), new Point(outPoint.X - 5, outPoint.Y + 20));
            dc.DrawText(GetFormattedTextByPosition(OutPoint), GetPoint(OutPoint, 2));
            //
        }
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            return base.ArrangeOverride(arrangeSize);
        }
        protected override Size MeasureOverride(Size constraint)
        {
            return base.MeasureOverride(constraint);
        }
        #endregion

        #region field
        BrushConverter brushConverter = new BrushConverter();
        Pen horLine=new System.Windows.Media.Pen(Brushes.DarkGray, 0.8);
        Pen shortLine = new Pen(Brushes.DarkGray,0.5);
        Pen longLine = new Pen(Brushes.Gray, 2);
        Pen inPointLine = new Pen(Brushes.YellowGreen, 2);
        Pen overInPointLine = new Pen(Brushes.Yellow, 2);
        /// <summary>
        /// 实际宽度,timeline不能超过
        /// </summary>
        int canvasWidth;
        /// <summary>
        /// 实际高度 timeline不能超过次高度
        /// </summary>
        int canvasHeight;
        /// <summary>
        /// 每个刻度的间隔
        /// </summary>
        int Lineinterval = 15;
        int lineTop;
        int lineBottom;
        int longLineBottom;
        #endregion

        #region Property 这些属性用来绘制In点，Out点，时间线，等

        public int Max
        {
            get { return (int)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Max.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxProperty =
            DependencyProperty.Register("Max", typeof(int), typeof(CanvasTimeLine), new PropertyMetadata(100,ValueChangedCallBack));

        private static void ValueChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var timeline = d as CanvasTimeLine;
            timeline.InvalidateVisual();
        }



        public int InPoint
        {
            get { return (int)GetValue(InPointProperty); }
            set { SetValue(InPointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InPoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InPointProperty =
            DependencyProperty.Register("InPoint", typeof(int), typeof(CanvasTimeLine), new PropertyMetadata(0, ValueChangedCallBack));



        public int OutPoint
        {
            get { return (int)GetValue(OutPointProperty); }
            set { SetValue(OutPointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OutPoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutPointProperty =
            DependencyProperty.Register("OutPoint", typeof(int), typeof(CanvasTimeLine), new PropertyMetadata(0, ValueChangedCallBack));


        public int Position
        {
            get { return (int)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Position.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(int), typeof(CanvasTimeLine), new PropertyMetadata(0, ValueChangedCallBack));






        #endregion

        #region funcs
        private FormattedText GetFormattedTextByPosition(int point)
        {
            return new FormattedText(point.ToString(),
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                new Typeface("Verdana"),
                12,
                Brushes.LightGreen);
        }
        private FormattedText GetFormattedTextByValue(int point)
        {
            return new FormattedText(GetValue(point).ToString(),
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                new Typeface("Verdana"),
                12,
                Brushes.LightGray);
        }
        /// <summary>
        /// 根据值获得绘制的坐标
        /// </summary>
        /// <param name="value"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Point GetPoint(int value,int height)
        {
            var off = ActualWidth / Max * value;
            return new Point(off, height);
        }
        /// <summary>
        /// 根据坐标获得当前值
        /// </summary>
        /// <param name="Point"></param>
        /// <returns></returns>
        private int GetValue(int Point)
        {
            var value = Max / ActualWidth * Point;
            return (int)value;
        }
        #endregion
    }
}
