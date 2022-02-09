using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public class Operation
    {
        public int Priority { get; set; }
        public bool A { get; set; }
        public bool B { get; set; }
        public Operation(bool a)
        {
            A = a;
        }
        public Operation(bool a, bool b)
        {
            A = a;
            B = b;
        }
        public bool Calculate(String operation)
        {
            if (operation.Equals(""))
            {
                return !this.Not();
            }
            if (operation.Equals("and"))
            {
                return this.And();
            } 
            else if(operation.Equals("or"))
            {
                return this.Or();
            }
            else if (operation.Equals("implication"))
            {
                return this.Implication();
            }
            else if (operation.Equals("xor"))
            {
                return this.Xor();
            }
            else if (operation.Equals("not"))
            {
                return this.Not();
            }
            return false;
        }
        public bool And()
        {
            return A && B;
        }
        public bool Or()
        {
            return A || B;
        }
        public bool Implication()
        {
            return (!A) || B;
        }
        public bool Xor()
        {
            return A == (!B);
        }
        public bool Not()
        {
            return !A;
        }
    }
}
