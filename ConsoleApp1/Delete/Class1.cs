using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace ConsoleApp1
{
    class Class1
    {
        static void Main22(string[] args)
        {
            // 个人所得税方式
            InterestOperation operation = new InterestOperation(new GeRen());
            Console.WriteLine("个人支付的税为：{0}", operation.GetTax(5000.00));

            // 企业所得税
            operation = new InterestOperation(new QiYe());
            Console.WriteLine("企业支付的税为：{0}", operation.GetTax(50000.00));

            Console.Read();
        }
    }
    public interface ITaxStragety
    {
        double CalculateTax(double income);
    }
    public class GeRen : ITaxStragety
    {
        public double CalculateTax(double income)
        {
            return income * 0.12;
        }
    }
    public class QiYe : ITaxStragety
    {
        public double CalculateTax(double income)
        {
            return (income - 3500) > 0 ? (income - 3500) * 0.045 : 0.0;
        }
    }
    public class InterestOperation
    {
        private ITaxStragety m_strategy;
        public InterestOperation(ITaxStragety TaxStragety)
        {
            this.m_strategy = TaxStragety;
        }
        public double GetTax(double income)
        {
            return m_strategy.CalculateTax(income);
        }
    }
}
