using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public abstract class Builder
    {
        public abstract void BuildPartCPU();
        public abstract void BuildPartMainBoard();
        public abstract Computer GetComputer();
    }

    public class Director
    {
        public void Construct(Builder builder)
        {
            builder.BuildPartCPU();
            builder.BuildPartMainBoard();
        }
    }

    public class Computer
    {
        private IList parts = new List<string>();
        public void Add(string part)
        {
            parts.Add(part);
        }
        public void Show()
        {
            Console.WriteLine("电脑开始在组装.......");
            foreach (var part in parts)
            {
                Console.WriteLine("组件" + part + "已装好");
            }
            Console.WriteLine("电脑组装好了");
        }
    }

    public class Builder1 : Builder
    {
        Computer computer = new Computer();
        public override void BuildPartCPU()
        {
            computer.Add("cpu1");
        }

        public override void BuildPartMainBoard()
        {
            computer.Add("main board1");
        }

        public override Computer GetComputer()
        {
            return computer;
        }
    }

    public class Builder2 : Builder
    {
        Computer computer = new Computer();
        public override void BuildPartCPU()
        {
            computer.Add("cpu2");
        }

        public override void BuildPartMainBoard()
        {
            computer.Add("main board2");
        }

        public override Computer GetComputer()
        {
            return computer;
        }
    }
}
