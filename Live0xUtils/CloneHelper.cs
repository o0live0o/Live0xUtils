using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Live0xUtils
{
     public abstract class CloneHelper
    {
        //深拷贝
        public static object Clone<T>(T t)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, t);
                memoryStream.Position = 0;
                return binaryFormatter.Deserialize(memoryStream);
            }
            catch
            {
                throw;
            }
        }
    }
}
