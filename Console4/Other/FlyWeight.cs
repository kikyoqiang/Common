using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console4
{
    public abstract class FlyWeight
    {
        public abstract void Operation(int extrinsicState);
    }
    public class FlyWeightFactory
    {
        public Hashtable flyWeights = new Hashtable();
        public FlyWeightFactory()
        {
            flyWeights.Add("A", new Fly1("A"));
            flyWeights.Add("B", new Fly1("B"));
            flyWeights.Add("C", new Fly1("C"));
        }
        public FlyWeight GetFlyWeight(string key)
        {
            return flyWeights[key] as FlyWeight;
        }
    }
    public class Fly1 : FlyWeight
    {
        private string intrinsicState;
        public Fly1(string innerState)
        {
            this.intrinsicState = innerState;
        }
        public override void Operation(int extrinsicState)
        {
            Console.WriteLine("具体实现类: intrinsicstate {0}, extrinsicstate {1}", intrinsicState, extrinsicState);
        }
    }
}
