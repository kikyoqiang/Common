using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;

namespace Console6
{
    class Class4
    {
        static void Main()
        {
            //IMyList<Dog> myDogs = new MyList<Dog>();

            //IMyList<Animal> myAnimals = myDogs;
            IMyList<Animal> myAnimals = new MyList<Animal>();

            IMyList<Dog> myDogs = myAnimals;
        }
    }
    interface IMyList<in T>
    {
        void ChangeT(T t);
    }
    public class MyList<T> : IMyList<T>
    {
        public void ChangeT(T t)
        {
            throw new NotImplementedException();
        }

        public T GetElement()
        {
            return default(T);
        }
    }
    public class Dog : Animal
    {

    }
    public class Animal
    {

    }
}
