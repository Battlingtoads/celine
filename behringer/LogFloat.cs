using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
    class LogFloat
    {
        private float minimum;
        private float maximum;
        private int numSteps;
        private float stepSize;
        private float _Value;
        public float Value
        {
            get{return _Value;}
            set
            {
                //Algorithm: adjustedValue = 10^Log(value).ToStep(stepSize)
                float tempVal = (float)Math.Log(value);
                tempVal.ToStep(stepSize);
                _Value = (float)Math.Pow(10, tempVal);
            }
        }
        public LogFloat(float min, float max, int steps)
        {
            minimum = min;
            maximum = max;
            numSteps = steps;
            Value = min;
            double range = Math.Log((double)maximum) - Math.Log((double)minimum);
            stepSize = (float)(range / numSteps);
        }

        public static implicit operator float(LogFloat f)
        {
            return f.Value;
        }
    }
}
