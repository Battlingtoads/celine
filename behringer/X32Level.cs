using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
    public class X32Level
    {
        protected float m_fRawLevel;
        public float RawLevel 
        {
            get
            {
                return m_fRawLevel;
            }
            set
            {
                m_fRawLevel = value;
                m_fDbFSLevel = value;
                if(value == 0.0f)
                    m_fDbFSLevel = Constants.NO_LEVEL;
                else if(value <= .0625f)
                {
                    m_fDbFSLevel = m_fDbFSLevel.Remap(0.0f, -90, .0625f, -60);
                }
                else if(value <= .25f)
                {
                    m_fDbFSLevel = m_fDbFSLevel.Remap(0.0625f, -60, 0.25f, -30);
                }
                else if(value <= .5f)
                {
                    m_fDbFSLevel = m_fDbFSLevel.Remap(0.25f, -30, 0.5f, -10);
                }
                else if(value <= 1.0f)
                {
                    m_fDbFSLevel = m_fDbFSLevel.Remap(0.5f, -10, 1.0f, 10);
                }
            }
        }
        protected float m_fDbFSLevel;
        public float DbFSLevel
        {
            get
            {
                return m_fDbFSLevel;
            }
            set
            {
                m_fDbFSLevel = value;
                getRaw(value);
            }
        }
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
            if (fDB < -100 )
            {
                m_fRawLevel = 0.0f;
                return;
            }
            if(fDB > 10)
            {
                m_fRawLevel = 1.0f;
                return;
            }
            if(fDB == Constants.NO_LEVEL)
            {
                m_fRawLevel = 0.0f;
            }
            else if (fDB <= -60)
            {
                m_fRawLevel = m_fRawLevel.Remap(-100, 0.0f, -60, .0625f);
            }
            else if (fDB <= -30)
            {
                m_fRawLevel = m_fRawLevel.Remap(-60, 0.0625f, -30, .25f);
            }
            else if (fDB <= -10)
            {
                m_fRawLevel = m_fRawLevel.Remap(-30, 0.25f, -10, .5f);
            }
            else if (fDB <= 10)
            {
                m_fRawLevel = m_fRawLevel.Remap(-10, 0.5f, 10, 1.0f);
            }
        }
    }
}
