using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using gnow.util.behringer;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace App1
{
    [TemplatePart(Name = CanvasPartName, Type = typeof(Canvas))]
    [TemplatePart(Name = ThumbPartName, Type = typeof(Rectangle))]
    [TemplatePart(Name = CirclePartName, Type = typeof(Ellipse))]
    [TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "Disabled", GroupName = "CommanStates")]
    public sealed class Knob : RangeBase
    {
        private const string ThumbPartName = "PART_Thumb";
        private const string CirclePartName = "PART_Circle";
        private const string CanvasPartName = "PART_Canvas";
        private static readonly float MINIMUM_ANGLE = -150.0f;
        private static readonly float MAXIMUM_ANGLE = 150.0f;
        private static readonly double CHANGE_THRESHOLD = 1.0f;
        private float angle = 0;
        private Point dragStart;
        private bool capturing;
        private float AngleValueRatio = 1;
        private double range;

        private Canvas canvas;


        public Knob()
        {
            this.DefaultStyleKey = typeof(Knob);
            this.Loaded += Knob_Loaded;
            SmallChange = .5;
            ChangeSpeed = .5;
        }

        private void Knob_Loaded(object sender, RoutedEventArgs e)
        {
            range = Maximum - Minimum;
            AngleValueRatio = (float)(300 / range);
            float startingAngle = ((float)Value).Remap((float)Minimum, MINIMUM_ANGLE, (float)Maximum, MAXIMUM_ANGLE);
            RotateKnob(startingAngle);
        }

        private void Slider_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(this.IsEnabled)
            {
                VisualStateManager.GoToState(this, "Normal", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "Disabled", true);
            }
        }

        protected override void OnPointerWheelChanged( PointerRoutedEventArgs e)
        {

            PointerPoint point = e.GetCurrentPoint(this);
            if (point.Properties.MouseWheelDelta < 0)
                RotateKnob(-(float)SmallChange * AngleValueRatio);
            else
                RotateKnob((float)SmallChange * AngleValueRatio);
            e.Handled = true;

        }
        
        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            dragStart = e.GetCurrentPoint(this).Position;
            CapturePointer(e.Pointer);
            capturing = true;           
            base.OnPointerPressed(e);
        }

        protected override void OnPointerMoved(PointerRoutedEventArgs e)
        {
            if (capturing == false)
                return;

            Point end = e.GetCurrentPoint(this).Position;
            Point start = dragStart;
            float oldAngle = angle;
            double distance = (start.Y - end.Y) * ChangeSpeed;
            int smallChanges = (int)(distance / SmallChange);
            
            //TODO: This deals with small movement cases. Make this work better
            if (Math.Abs(smallChanges) < 1 && Math.Abs(distance) > CHANGE_THRESHOLD )
            {
                if (distance < 0)
                {
                    smallChanges = -1;
                }
                else
                {
                    smallChanges = 1;
                }
            }
            RotateKnob((float)(smallChanges * SmallChange)*AngleValueRatio);
            dragStart.Y = end.Y;
            base.OnPointerMoved(e);
        }

        protected override void OnPointerReleased(PointerRoutedEventArgs e)
        {
            capturing = false;
            base.OnPointerReleased(e);
        }

        private void RotateKnob(float change)
        {
            angle += change;
            if(angle < MINIMUM_ANGLE)
            {
                angle = MINIMUM_ANGLE;
            }
            if(angle > MAXIMUM_ANGLE)
            {
                angle = MAXIMUM_ANGLE;
            }
            canvas = GetTemplateChild(CanvasPartName) as Canvas;
            canvas.RenderTransform = new RotateTransform();
            (canvas.RenderTransform as RotateTransform).Angle = angle;
            double oldValue = Value;
            Value += change / AngleValueRatio;
            OnValueChanged(oldValue, Value);
        }



        public double ChangeSpeed
        {
            get { return (double)GetValue(ChangeSpeedProperty); }
            set { SetValue(ChangeSpeedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChangeSpeed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChangeSpeedProperty =
            DependencyProperty.Register("ChangeSpeed", typeof(double), typeof(Knob), new PropertyMetadata(0));

        
    }
}
