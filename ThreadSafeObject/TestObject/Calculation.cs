using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestObject
{
    public class Calculation
    {
        public int i;

        public int Value
        {
            get { return i; }
            set { i = value; }
        }

        public int this[string key]
        {
            get { return i; }
            set { i = value; }
        }

        public int Add()
        {
            i++;
            return i;
        }

        public int Operation(int j)
        {
            i += j;
            return i;
        }

        public int Decrease()
        {
            i--;
            return i;
        }

        public int TwoOperations()
        {
            Add();
            return Decrease();
        }

    }
}
