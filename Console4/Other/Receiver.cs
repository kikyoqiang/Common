using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console4
{
    public class Receiver
    {
        public void Run()
        {
            Console.WriteLine("run 1000");
        }
    }
    public abstract class Command
    {
        protected Receiver receiver;
        public Command(Receiver receiver)
        {
            this.receiver = receiver;
        }
        public abstract void Action();
    }
    public class Invoke
    {
        public Command command;
        public Invoke(Command command)
        {
            this.command = command;
        }
        public void Execute()
        {
            command.Action();
        }
    }
    public class Command1 : Command
    {
        public Command1(Receiver receiver) : base(receiver)
        {
        }

        public override void Action()
        {
            receiver.Run();
        }
    }
}
