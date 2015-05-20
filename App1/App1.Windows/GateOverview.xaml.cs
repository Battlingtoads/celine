using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using gnow.util.behringer;
using System.Diagnostics;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace App1
{
    public sealed partial class GateOverview : UserControl
    {
        private Frame ownerFrame;
        public GateOverview()
        {
            this.InitializeComponent();
            gainReduction.SetBackgroundFill(new SolidColorBrush(Colors.Red));
            this.Loaded += (o, e) => DrawGraph();
            ownerFrame = Window.Current.Content as Frame;

        }


        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set
            {
                SetValue(TitleProperty, value);
                titleButton.Content = value;
            }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(DynamicOverview), new PropertyMetadata("text", onValueChanged));


        private static void onValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
        }

        private void TitleClick(object sender, RoutedEventArgs e)
        {
            ownerFrame.Navigate(typeof(GateView));
        }

        private LineSegment threshHoldPoint;
        private LineSegment endPoint;
        private PathFigure curveFig;

        private void DrawGraph()
        {
            Threshold.ValueChanged += Threshold_ValueChanged;
            //make graph square
            if (responseGraph.ActualWidth > responseGraph.ActualHeight)
            {
                responseGraph.Width = responseGraph.ActualHeight;
            }
            else
            {
                responseGraph.Height = responseGraph.ActualWidth;
            }

            //set meter height
            gainReduction.Height = responseGraph.Height;
            UpdateLayout();

            //draw gridlines
            for (int i = 0; i < 9; i++)
            {
                Line Horizontal = new Line();
                Horizontal.Stroke = new SolidColorBrush(Colors.White);
                Horizontal.StrokeThickness = 1;
                Horizontal.X1 = 0;
                Horizontal.Y1 = (responseGraph.ActualHeight / 8) * i;
                Horizontal.X2 = responseGraph.ActualWidth;
                Horizontal.Y2 = (responseGraph.ActualHeight / 8) * i;
                responseGraph.Children.Add(Horizontal);
                Line Vertical = new Line();
                Vertical.Stroke = new SolidColorBrush(Colors.White);
                Vertical.StrokeThickness = 1;
                Vertical.X1 = (responseGraph.ActualWidth / 8) * i;
                Vertical.Y1 = 0;
                Vertical.X2 = (responseGraph.ActualWidth / 8) * i;
                Vertical.Y2 = responseGraph.ActualHeight;
                responseGraph.Children.Add(Vertical);
            }

            //draw curve

            //make pathgeometry
            PathGeometry curveGeo = new PathGeometry();

            //make pathfigure
            curveFig = new PathFigure();
            curveFig.StartPoint = new Point(0, responseGraph.ActualHeight);



            /*
             * LOGIC FOR COMPUTING CURVE
             * =========================
             * Threshold point is as follows:
             * X-Coordinate = responseGraphWidth * (RangeOfThresholds/Threshold)
             * Y-Coordinate = responseGraphHeight * (1 - RangeOfThresholds/Threshold)
             *
             * Endpoint:
             * X-Coordinate = responseGraphWidth
             * Y-Coordinate = responseGraphHeight * (1-RangeOfThreshholds/Threshold) * CompressionRatio
             */

            //add segments
            threshHoldPoint = new LineSegment() { Point = new Point(responseGraph.ActualWidth, 0) };
            endPoint = new LineSegment() { Point = new Point(responseGraph.ActualWidth, 0) };

            curveFig.Segments.Add(threshHoldPoint);
            curveFig.Segments.Add(endPoint);

            //add fig to geo
            curveGeo.Figures.Add(curveFig);

            //set geo to curve.data
            curve.Data = curveGeo;
            Canvas.SetZIndex(curve, 10);
        }

        private void Threshold_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            ThreshHoldText.Text = e.NewValue.ToString("##0.00") + "dB";
            float fractionalValue = ((float)e.NewValue).Remap((float)Threshold.Minimum, 0.0f, (float)Threshold.Maximum, 1f); ;

            threshHoldPoint.Point = new Point(responseGraph.ActualWidth * fractionalValue,
                                               responseGraph.ActualHeight * (1 - fractionalValue));
            curveFig.StartPoint = new Point(responseGraph.ActualWidth * fractionalValue, responseGraph.ActualHeight);
        }

        public void SetThreshold(float value)
        {
            Threshold.SetValue(value);
            ThreshHoldText.Text = value.ToString("##0.00") + "dB";
            float fractionalValue = ((float)value).Remap((float)Threshold.Minimum, 0.0f, (float)Threshold.Maximum, 1f); ;

            threshHoldPoint.Point = new Point(responseGraph.ActualWidth * fractionalValue,
                                               responseGraph.ActualHeight * (1 - fractionalValue));
            curveFig.StartPoint = new Point(responseGraph.ActualWidth * fractionalValue, responseGraph.ActualHeight);
        }

        public float GetThreshold()
        {
            return (float)Threshold.Value;
        }
    }
}
