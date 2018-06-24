using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// 原型模式
    /// </summary>
    public abstract class MonkeyKingPrototype
    {
        public string Id { get; set; }
        public MonkeyKingPrototype(string id)
        {
            this.Id = id;
        }
        public abstract MonkeyKingPrototype Clone();
    }

    public class ConCreatePrototype : MonkeyKingPrototype
    {
        public ConCreatePrototype(string id) : base(id)
        {
        }

        public override MonkeyKingPrototype Clone()
        {
            return (MonkeyKingPrototype)this.MemberwiseClone();
        }
    }
}
