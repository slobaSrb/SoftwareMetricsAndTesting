using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaci4
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
                //cvor 44 uslov
                Console.Write("44 ");
                //cvor 45 return
                Console.Write("45 ");
                return A;
            }
            else if (operation.Equals("and"))
            {
                //cvor 44 46 uslovi
                Console.Write("44 ");
                Console.Write("46 ");
                //cvor 47 return
                Console.Write("47 ");
                return A && B;
            } 
            else if(operation.Equals("or"))
            {
                //uslovi
                Console.Write("44 ");
                Console.Write("46 ");
                Console.Write("48 ");
                //cvor 49 return
                Console.Write("49 ");
                return A || B;
            }
            else if (operation.Equals("implication"))
            {
                //uslov
                Console.Write("44 ");
                Console.Write("46 ");
                Console.Write("48 ");
                Console.Write("50 ");
                //cvor 49 return
                Console.Write("51 ");
                return (!A) || B;
            }
            else if (operation.Equals("xor"))
            {
                Console.Write("44 ");
                Console.Write("46 ");
                Console.Write("48 ");
                Console.Write("50 ");
                Console.Write("52 ");
                // 53 return
                Console.Write("53 ");
                return (!A) == B;
            }
            else if (operation.Equals("not"))
            {
                //uslovi
                Console.Write("44 ");
                Console.Write("46 ");
                Console.Write("48 ");
                Console.Write("50 ");
                Console.Write("52 ");
                Console.Write("54 ");
                // 53 return
                Console.Write("55 ");
                return !A;
            }
            // 53 return
            Console.Write("56 ");
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
