using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Console3
{
    [Serializable]
    public class DeepCopy : ICloneable
    {
        public string message = "我是原始A";
        public object Clone()
        {
            Stream ms = null;
            try
            {
                using (ms = new MemoryStream())
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ms, this);
                    ms.Position = 0;
                    return bf.Deserialize(ms);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Instance.WriteError("", ex);
                return null;
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                }
            }
        }
    }
}
