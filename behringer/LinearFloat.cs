using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gnow.util.behringer
{
    public class LinearFloat
    {
        private float minimum;
        private float maximum;
        private float stepSize;
        private float _Value;
        public float Value
        {
            get{return _Value;}
            set
            {
                _Value = (float)(stepSize * Math.Round((double)((value * 1000) / (stepSize * 1000))));
            }
        }

        public LinearFloat(float min, float max, float step)
        {
            minimum = min;
            maximum = max;
            stepSize = step;
            Value = min;
        }

        public static implicit operator float(LinearFloat f)
        {
            return f.Value;
        }
           
    }
}
