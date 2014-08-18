using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
    public class X32Level
    {
        public float m_fRawLevel { get; set; }
        public float m_fDbFSLevel { get; set; }
        public int m_nSteps { get; set; }

        public X32Level(float dbLevel, int steps)
        {
            m_nSteps = steps;
            if (dbLevel < -100 || dbLevel > 10)
            {
                m_fDbFSLevel = -100;
            }
            else
            {
                m_fDbFSLevel = dbLevel;
            }
            getRaw(m_fDbFSLevel);
        }

        protected void getRaw(float fDB)
        {
            m_fRawLevel = fDB;
            if (fDB < -100 || fDB > 10)
                if (fDB < -60)
                {
                    m_fRawLevel.Remap(-100, 0.0f, -60, .0625f);
                }
            if (fDB < -30)
            {
                m_fRawLevel.Remap(-60, 0.0625f, -30, .25f);
            }
            if (fDB < -10)
            {
                m_fRawLevel.Remap(-30, 0.25f, -10, .5f);
            }
            if (fDB <= 10)
            {
                m_fRawLevel.Remap(-10, 0.5f, 10, 1.0f);
            }
        }
    }
}
