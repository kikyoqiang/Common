using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyCom
{
    [Guid("3B4F1CC4-D605-46BE-A6FB-EF46DE1359C2")]
    public interface MyCom_Interface
    {
        [DispId(1)]
        int Add(int a, int b);
    }
    [Guid("800D4CF9-54CD-433E-9727-9F71D32DE303"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface MyCom_Events
    {

    }
    [Guid("5569DDE6-6FAE-46F6-B7E3-C4B59F385FF9"), ClassInterface(ClassInterfaceType.None), ComSourceInterfaces(typeof(MyCom_Events))]
    public class Class1 : MyCom_Interface
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}
