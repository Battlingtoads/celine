using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
	/// <summary>A set of meters.</summary>
    public class X32MeterGroup
    {
        private Constants.METER_TYPE _type;

		/// <summary>The type of meter. See <see cref="Constants.METER_TYPE"/>.
        public Constants.METER_TYPE type { get { return (_type); } }
        protected List<float> values;

		/// <summary>Values of the meters</summary>
		public List<float> Values
		{
            get { return (List<float>)values; }
		}

		/// <summary>Constructor for Meter Group</summary>
		/// <param name="bytes">The array of float values contained in a byte array</param>
		/// <param name="t">The type of meter group</param>
        public X32MeterGroup(byte[] bytes, Constants.METER_TYPE t)
        {
            values = new List<float>();
            _type = t;
            for(int i = 0; i < bytes.Length/4; i++)
            {
                byte[] bFloat = new byte[4];
                for(int j = 0; j < 4; j++)
                {
                    bFloat[j] = bytes[i * 4 + j];
                }
                //if (BitConverter.IsLittleEndian) bFloat = Utilities.swapEndian(bFloat);
                float fFloat = BitConverter.ToSingle(bFloat, 0);
                values.Add(fFloat);
            }
        }
    }
}
