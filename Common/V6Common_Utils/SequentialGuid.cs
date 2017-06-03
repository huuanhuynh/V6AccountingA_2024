using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V6Soft.Common.Utils
{
    public sealed class SequentialGuid
    {
        private static int[] _SqlOrderMap = null;

        private static int[] SqlOrderMap
        {
            get
            {
                if (_SqlOrderMap == null)
                {
                    _SqlOrderMap = new int[16] { 3, 2, 1, 0, 5, 4, 7, 6, 9, 8, 15, 14, 13, 12, 11, 10 };
                    // 3 - the least significant byte in Guid ByteArray [for SQL Server ORDER BY clause]
                    // 10 - the most significant byte in Guid ByteArray [for SQL Server ORDERY BY clause]
                }
                return _SqlOrderMap;
            }
        }

        public static SequentialGuid operator ++(SequentialGuid sequentialGuid)
        {
            byte[] bytes = sequentialGuid.ToByteArray();
            for (int mapIndex = 0; mapIndex < 16; mapIndex++)
            {
                int bytesIndex = SqlOrderMap[mapIndex];
                bytes[bytesIndex]++;
                if (bytes[bytesIndex] != 0)
                {
                    break; // No need to increment more significant bytes
                }
            }
            sequentialGuid.Value = new Guid(bytes);
            return sequentialGuid;
        }


        private Guid m_InternalGuid;

        public SequentialGuid()
            : this(Guid.NewGuid())
        {
        }

        public SequentialGuid(Guid guid)
        {
            m_InternalGuid = guid;
        }

        /// <summary>
        ///     Gets Guid value of this instance.
        /// </summary>
        public Guid Value
        {
            get
            {
                return m_InternalGuid;
            }
            private set
            {
                m_InternalGuid = value;
            }
        }

        public override string ToString()
        {
            return m_InternalGuid.ToString();
        }

        public byte[] ToByteArray()
        {
            return m_InternalGuid.ToByteArray();
        }
    }
}
