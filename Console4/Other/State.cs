using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console4
{
    public abstract class State
    {
        public Account Account { get; set; }
        public double Balance { get; set; }
        public double Interest { get; set; }
        public double LowerLimit { get; set; }
        public double UpperLimit { get; set; }

        public abstract void Deposit(double amount);
        public abstract void WithDraw(double amount);
        public abstract void PayInterest();
    }
    public class RedState : State
    {
        public RedState(State state)
        {
            this.Balance = state.Balance;
            Account = state.Account;
            Interest = 0.00;
            LowerLimit = -100.00;
            UpperLimit = 0.00;
        }
        public override void Deposit(double amount)
        {
            Balance += amount;
        }

        public override void PayInterest()
        {
            // 没有利息
        }

        public override void WithDraw(double amount)
        {
            Console.WriteLine("没有钱可以取了！");
        }

        private void StateChangeCheck()
        {
            if (Balance > UpperLimit)
            {
                Account.state = new SilverState(this);
            }
        }
    }
    public class SilverState : State
    {
        public SilverState(State state) : this(state.Balance, state.Account)
        {

        }

        public SilverState(double balance, Account account)
        {
            this.Balance = balance;
            this.Account = account;
            Interest = 0.00;
            LowerLimit = 0.00;
            UpperLimit = 1000.00;
        }

        public override void Deposit(double amount)
        {
            Balance += amount;
            StateChangeCheck();
        }

        public override void PayInterest()
        {
            Balance += Interest * Balance;
            StateChangeCheck();
        }

        public override void WithDraw(double amount)
        {
            Balance -= amount;
            StateChangeCheck();
        }

        private void StateChangeCheck()
        {
            if (Balance < 0.0)
            {
                Account.state = new RedState(this);
            }
            else if (Balance < LowerLimit)
            {
                Account.state = new SilverState(this);
            }
        }
    }
    public class GoldState : State
    {
        public GoldState(State state)
        {
            this.Balance = state.Balance;
            this.Account = state.Account;
            Interest = 0.05;
            LowerLimit = 1000.00;
            UpperLimit = 1000000.00;
        }

        public override void Deposit(double amount)
        {
            Balance += amount;
            StateChangeCheck();
        }

        public override void PayInterest()
        {
            Balance += Interest * Balance;
            StateChangeCheck();
        }

        public override void WithDraw(double amount)
        {
            Balance -= amount;
            StateChangeCheck();
        }

        private void StateChangeCheck()
        {
            if (Balance < 0.0)
            {
                Account.state = new RedState(this);
            }
            else if (Balance < LowerLimit)
            {
                Account.state = new SilverState(this);
            }
        }
    }
    public class Account
    {
        public State state { get; set; }
        public string Owner { get; set; }
        public Account(string owner)
        {
            this.Owner = owner;
        }
        public double Balance { get { return state.Balance; } }
    }
}
