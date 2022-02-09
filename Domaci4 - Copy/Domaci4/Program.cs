using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaci4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("ulaz.txt");
            StreamWriter sw = new StreamWriter("izlaz.txt");
            while (!sr.EndOfStream)
            {
                HashSet<string> expectedSet = new HashSet<string>();
                string izrazForUse = sr.ReadLine().Trim();
                int numOf = PrimeService.CountVars(izrazForUse);
                sw.WriteLine("izraz " + izrazForUse);
                sw.WriteLine("ima " + numOf + " promenljivih");
                HashSet<String> result = PrimeService.ActiveVars(izrazForUse, numOf, PrimeService.MakeTable(PrimeService.CountVars(izrazForUse)));
                sw.WriteLine("aktivnost promenljivih");
                for (int i = 0; i < result.Count; i++)
                {
                    sw.WriteLine(result.ElementAt(i));
                }
                sw.WriteLine();
                sw.Flush();
            }
            sw.Close();

        }
    }

    public class PrimeService
    {
        public static List<int> path;
        public static Dictionary<int, List<Boolean>> MakeTable(int numOfVars)
        {
            Dictionary<int, List<bool>> table = new Dictionary<int, List<bool>>();
            int counter = 0;
            Boolean val = true;
            int n = (int)Math.Pow(2, numOfVars);
            for (int i = 0; i < numOfVars; i++)
            {
                //cvor 5 za inicijalizaciju for petlje
                table.Add(i, new List<bool>());
                //cvor 6 i++ i provera
                for (int k = 0; k < n; k++)
                {
                    //cvor 6 obuhvata i if uslov
                    table[i].Add(val);
                    if (++counter >= n / Math.Pow(2, i + 1))
                    {
                        //cvor 7
                        val = !val;
                        counter = 0;
                    }
                }
            }

            return table;
        }
        public static int CountVars(string izraz)
        {
            return ExtractVars(izraz).Count;
        }
        public static HashSet<String> ExtractVars(string izraz)
        {
            path = new List<int>();
            //cvor 1 obuhvata i for initial
            path.Add(1);
            HashSet<string> variables = new HashSet<string>();
            List<string> expressions = izraz.Split('(', ')').ToList();
            //cvor 2 i++ i uslov
            path.Add(2);
            for (int i = 0; i < expressions.Count; i++)
            {
                //cvor 3 uslov
                path.Add(3);
                if (expressions[i].Trim().Equals(""))
                {
                    //cvor 4
                    path.Add(4);
                    expressions.RemoveAt(i);
                    i--;
                }
                else
                {
                    //cvor 5 uslov
                    path.Add(5);
                    if (i % 2 == 0)
                    {
                        //cvor 6 uslov
                        path.Add(6);
                        if (!expressions[i].Trim().Contains(' '))
                        {
                            //cvor 7
                            path.Add(7);
                            VarsAdd(expressions[i].Trim(), expressions[i].Trim(), ref variables);
                        }
                        else
                        {
                            string[] part = expressions[i].Trim().Split(' ');
                            if (part.Length == 2)
                            {
                                //cvor 8 part split i if uslov
                                path.Add(8);
                                VarsAdd(part[1].Trim(), part[1].Trim(), ref variables);
                            }
                            else if (part.Length == 5)
                            {
                                //cvor 8 part split i if uslov i uslov cvor 9
                                path.Add(8);
                                path.Add(9);
                                VarsAdd(part[1].Trim(), part[4].Trim(), ref variables);
                            }
                            else if (part.Length == 3)
                            {
                                //cvor 8 part split i if uslov i uslov cvor 9 i 10
                                path.Add(8);
                                path.Add(9);
                                path.Add(10);
                                VarsAdd(part[0].Trim(), part[2].Trim(), ref variables);
                            }
                            else if (part.Length == 4 && part[0].Equals("not"))
                            {
                                //cvor 8 part split i if uslov i uslov cvor 9 do 11
                                path.Add(8);
                                path.Add(9);
                                path.Add(10);
                                path.Add(11);
                                VarsAdd(part[1].Trim(), part[3].Trim(), ref variables);
                            }
                            else if (part.Length == 4 && part[2].Equals("not"))
                            {
                                //cvor 8 part split i if uslov i uslov cvor 9 do 12
                                path.Add(8);
                                path.Add(9);
                                path.Add(10);
                                path.Add(11);
                                path.Add(12);
                                VarsAdd(part[0].Trim(),part[3].Trim(), ref variables);
                            }
                        }
                    }
                }
                path.Add(2);
            }
            
            //cvor 14
            path.Add(14);
            for (int i = 0; i < path.Count; i++)
            {
                Console.Write(path.ElementAt(i) + ", ");
            }
            Console.WriteLine();
            return variables;
        }

        private static void VarsAdd(string var1, string var2, ref HashSet<string> variables)
        {
            //cvor 13
            path.Add(13);
            variables.Add(var1);
            variables.Add(var2);
        }
        public static HashSet<string> ActiveVars(string izraz, int numOfVars, Dictionary<int, List<Boolean>> table)
        {
            // cvor 1 ulazni cvor
            Console.Write("1 ");
            int brojac = 0;
            String glavnaOperacija = "";
            List<List<bool>> listaVrednostiPodizraza = new List<List<bool>>();
            List<string> vars = new List<string>();
            List<string> expressions = izraz.Split(new char[] { '(', ')' }).ToList();
            //cvor 2 za inicijalizaciju for
            Console.Write("2 ");
            //cvor 3 za i++ i uslov
            Console.Write("3 ");
            for (int i = 0; i < expressions.Count; i++)
            {
                //cvor 4 obuhvata i if uslov jer se sve izvrsava uvek po redu
                Console.Write("4 ");
                List<bool> firstVarValues;
                List<bool> secondVarValues;
                if (expressions[i].Trim().Equals(""))
                {
                    //cvor 5
                    Console.Write("5 ");
                    expressions.RemoveAt(i);
                    i--;
                }
                else
                {
                    //cvor 6
                    Console.Write("6 ");
                    if (i % 2 == 0)
                    {
                        //cvor 7
                        Console.Write("7 ");
                        if (!expressions[i].Trim().Contains(' '))
                        {
                            //cvor 14 15 16 17 18
                            FindOperatorsOperandValsAndNames(table, vars, out firstVarValues, out secondVarValues, expressions[i].Trim(), expressions[i].Trim());
                            //cvor 19 20 21 
                            CalculateExpression(ref brojac, listaVrednostiPodizraza, firstVarValues, firstVarValues, "and", "", "");
                        }
                        else
                        {
                            //cvor 9 part split i prvi if uslov se izvrsavaju uvek jedan za drugim pa ih sjedinjujem
                            string[] part = expressions[i].Trim().Split(' ');
                            if (part.Length == 2)
                            {
                                // cvor 9 samo if uslov
                                Console.Write("9 ");
                                //cvor 14 15 16 17 18
                                FindOperatorsOperandValsAndNames(table, vars, out firstVarValues, out secondVarValues, part[1].Trim(), part[1].Trim());
                                //cvor 19 20 21 
                                CalculateExpression(ref brojac, listaVrednostiPodizraza, firstVarValues, firstVarValues, "and", "not", "not");
                            }
                            else if (part.Length == 5)
                            {
                                // cvor 10 samo if uslov ali se i 9 izvrsio
                                Console.Write("9 ");
                                Console.Write("10 ");
                                //cvor 14 15 16 17 18
                                FindOperatorsOperandValsAndNames(table, vars, out firstVarValues, out secondVarValues, part[1].Trim(), part[4].Trim());
                                //cvor 19 20 21 
                                CalculateExpression(ref brojac, listaVrednostiPodizraza, firstVarValues, secondVarValues, part[2].Trim(), "not", "not");
                            }
                            else if (part.Length == 3)
                            {
                                // cvor 11 samo if uslov ali se i 9 izvrsio i svi do 11
                                Console.Write("9 ");
                                Console.Write("10 ");
                                Console.Write("11 ");
                                //cvor 14 15 16 17 18
                                FindOperatorsOperandValsAndNames(table, vars, out firstVarValues, out secondVarValues, part[0].Trim(), part[2].Trim());
                                //cvor 19 20 21  
                                CalculateExpression(ref brojac, listaVrednostiPodizraza, firstVarValues, secondVarValues, part[1].Trim(), "", "");
                            }
                            else if (part.Length == 4 && part[0].Equals("not"))
                            {
                                // cvor 12 samo if uslov ali se i 9 izvrsio i svi do 12
                                Console.Write("9 ");
                                Console.Write("10 ");
                                Console.Write("11 ");
                                Console.Write("12 ");
                                //cvor 14 15 16 17 18
                                FindOperatorsOperandValsAndNames(table, vars, out firstVarValues, out secondVarValues, part[1].Trim(), part[3].Trim());
                                //cvor 19 20 21 
                                CalculateExpression(ref brojac, listaVrednostiPodizraza, firstVarValues, secondVarValues, part[2].Trim(), "not", "");
                            }
                            else if (part.Length == 4 && part[2].Equals("not"))
                            {
                                // cvor 13 samo if uslov ali se i 9 izvrsio i svi do 13
                                Console.Write("9 ");
                                Console.Write("10 ");
                                Console.Write("11 ");
                                Console.Write("12 ");
                                Console.Write("13 ");
                                //cvor 14 15 16 17 18
                                FindOperatorsOperandValsAndNames(table, vars, out firstVarValues, out secondVarValues, part[0].Trim(), part[3].Trim());
                                //cvor 19 20 21 
                                CalculateExpression(ref brojac, listaVrednostiPodizraza, firstVarValues, secondVarValues, part[1].Trim(), "", "not");
                            }
                        }
                    }
                    else
                    {
                        //cvor 8
                        Console.Write("8 ");
                        glavnaOperacija = expressions[i].Trim();
                    }
                }
                Console.Write("3 ");
            }



            #region Nodes N
            //cvor 22 konacnalista i for initial i = 0
            Console.Write("22 ");
            List<bool> konacnaVrednostIzraza = new List<bool>();
            konacnaVrednostIzraza = listaVrednostiPodizraza[0];
            // uslov i i++
            Console.Write("23 ");
            for (int i = 0; i < listaVrednostiPodizraza.Count - 1; i++)
            {
                //initial for
                Console.Write("24 ");
                //uslov i j++
                Console.Write("25 ");
                for (int j = 0; j < listaVrednostiPodizraza[i].Count; j++)
                {
                    Console.Write("26 ");
                    konacnaVrednostIzraza[j] = new Operation(konacnaVrednostIzraza[j], listaVrednostiPodizraza[i + 1][j]).Calculate(glavnaOperacija);
                    Console.Write("25 ");
                }
                Console.Write("23 ");
            }
            //cvor 27 reztext for initial
            Console.Write("27 ");
            HashSet<string> rez = new HashSet<string>();
            //cvor 28 uslov i i--
            Console.Write("28 ");
            for (int i = table.Keys.Count - 1; i >= 0; i--)
            {
                //cvor 29 aktivna markirani i for initial
                Console.Write("29 ");
                bool aktivna = false;
                List<int> markirani = new List<int>();
                //cvor 30 uslov i j++
                Console.Write("30 ");
                for (int j = 0; j < konacnaVrednostIzraza.Count; j++)
                {
                    //cvor 42
                    Console.Write("42 ");
                    if((j / ((int)Math.Pow(2, i + 1) / 2)) % 2 == 0)
                    {
                        //cvor 43 
                        Console.Write("43 ");
                        markirani.Add((int)(j + (int)Math.Pow(2, i + 1) / 2));
                    }
                    Console.Write("31 ");
                    if (konacnaVrednostIzraza.Count > (j + (int)Math.Pow(2, i + 1) / 2) && konacnaVrednostIzraza[j] != konacnaVrednostIzraza[j + (int)Math.Pow(2, i + 1) / 2] && !markirani.Contains(j))
                    {
                        //cvor 32 sadrzi i inicijalizaciju petlje for
                        Console.Write("32 ");
                        aktivna = true;
                        rez.Add(vars.ElementAt(table.Keys.Count - i - 1) + " je aktivna [");
                        int broj = 0;
                        //cvor 33 uslov i ++k
                        Console.Write("33 ");
                        for (int k = 0; k < table.Keys.Count; ++k)
                        {
                            Console.Write("34 ");
                            if (k != (table.Keys.Count - i - 1))
                            {
                                Console.Write("35 ");
                                string pom = rez.ElementAt(rez.Count - 1);
                                rez.RemoveWhere(x => x.Equals(rez.ElementAt(rez.Count - 1)));
                                rez.Add(pom + vars.ElementAt(k) + "=" + table.ElementAt(k).Value[j + (int)Math.Pow(2, i + 1) / 2] + " ");
                                broj++;
                            }
                            Console.Write("33 ");
                        }
                        Console.Write("36 ");
                        if (broj == 0)
                        {
                            Console.Write("37 ");
                            string pom = rez.ElementAt(rez.Count - 1);
                            rez.RemoveWhere(x => x.Equals(rez.ElementAt(rez.Count - 1)));
                            rez.Add(pom + "uvek]");
                        }
                        else
                        {
                            Console.Write("38 ");
                            string pom = rez.ElementAt(rez.Count - 1);
                            rez.RemoveWhere(x => x.Equals(rez.ElementAt(rez.Count - 1)));
                            rez.Add(pom.Trim() + "]");
                        }
                    }
                    Console.Write("30 ");
                }
                Console.Write("39 ");
                markirani.Clear();
                if (!aktivna)
                {
                    Console.Write("40 ");
                    rez.Add(vars.ElementAt(table.Keys.Count - i - 1) + " nije aktivna!");
                }
                Console.Write("28 ");
            }
            #region

            #endregion
            //Console.WriteLine(rezText);
            #endregion

            //foreach (KeyValuePair<int, List<bool>> k in table)
            //{
            //    Console.Write(vars.ElementAt(k.Key) + ": ");
            //    foreach (bool b in k.Value)
            //    {
            //        Console.Write(b + " ");
            //    }
            //    Console.WriteLine();
            //}
            // 41 izlazni cvor
            Console.Write("41 ");
            return rez;

        }

        private static void FindOperatorsOperandValsAndNames(Dictionary<int, List<bool>> table, List<string> vars, out List<bool> firstVarValues, out List<bool> secondVarValues, string var1, string var2)
        {
            //cvor 14
            Console.Write("14 ");
            if (!vars.Contains(var1))
            {
                //cvor 15
                Console.Write("15 ");
                vars.Add(var1);
            }
            //cvor 16
            Console.Write("16 ");
            if (!vars.Contains(var2))
            {
                //cvor 17
                Console.Write("17 ");
                vars.Add(var2);
            }
            //cvor 18
            Console.Write("18 ");
            firstVarValues = table[vars.IndexOf(var1)];
            secondVarValues = table[vars.IndexOf(var2)];
        }

        private static void CalculateExpression(ref int brojac, List<List<bool>> listaVrednostiPodizraza, List<bool> firstVarValues, List<bool> secondVarValues, string operacija, string op1, string op2)
        {
            // cvor 19 initial for brojac++ lista add
            Console.Write("19 ");
            listaVrednostiPodizraza.Add(new List<bool>());
            brojac++;
            // cvor 20 i++ i uslov
            Console.Write("20 ");
            for (int i = 0; i < firstVarValues.Count; i++)
            {
                //cvor 21
                Console.Write("21 ");
                listaVrednostiPodizraza[brojac - 1].Add(new Operation(new Operation(firstVarValues[i]).Calculate(op1), new Operation(secondVarValues[i]).Calculate(op2)).Calculate(operacija));
                //cvor 23
                Console.Write("20 ");
            }
        }
    }
}
//Dictionary<string, List<bool>> d = new Dictionary<string, List<bool>>();
//d.Add("a", new List<bool>());
//d.Add("b", new List<bool>());
//d.Add("c", new List<bool>());
//for (int i = 0; i < d.Count; ++i)
//{
//    KeyValuePair<string, List<bool>> keyValuePair = d.ElementAt(i);
//    value = true;
//    int counter = 0;
//    for (int j = 0; j < n; ++j)
//    {
//        keyValuePair.Value.Add(value);
//        if (++counter >= n / divide)
//        {
//            value = !value;
//            counter = 0;
//        }
//    }
//    divide *= 2;
//}


//for (int i = 0; i < vars.Count; i++)
//{
//    for (int k = 0; k < counter; k++)
//    {
//        for (int j = 0; j < (int)(Math.Pow(2, vars.Count) / counter); j++)
//        {
//            table[vars.ElementAt(i)].Add(val);
//        }
//        val = !val;
//    }
//    counter = counter * 2;
//}



//for (int i = 0; i < vars.Count * Math.Pow(2, vars.Count); i++)
//{
//    if ((i / vars.Count) <= Math.Pow(2, table.Count) / (2 * (counter + 1)))
//    {
//        table[vars.ElementAt(counter / vars.Count)].Add(true);
//    }
//    else
//    {
//        table[vars.ElementAt(counter / vars.Count)].Add(false);
//    }
//    counter++;
//}

//foreach (var item in vars)
//{
//    Console.Write(item + "\t");
//}
//Console.WriteLine();
//foreach (var item in table.Values)
//{
//    foreach (var item1 in item)
//    {
//        Console.Write(item1 + "\t");
//    }
//    Console.WriteLine();
//}

//for (int i = 0; i < table.Keys.Count; i++)
//{
//    Console.Write(table.Keys.ElementAt(i) + "\t");
//}

//int counter1 = 0;
//for (int i = 0; i < table.Values.Count; i++)
//{
//    for (int j = 0; j < table.Keys.Count; j++)
//    {
//        Console.Write(table.Values.ElementAt(j)[counter1].ToString() + "\t");
//    }
//    counter1++;
//    counter1 /= table.Values.Count;
//}
