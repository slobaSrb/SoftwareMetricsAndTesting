using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTestProject1
{
    public class Methods
    {


        public static int CountVars()
        {
            return ExtractVars().Count;
        }
        private static HashSet<String> ExtractVars()
        {
            // cvor 1 obuhvata i inicijalizaciju for petlje
            Console.WriteLine("1 ");
            StreamReader sr = new StreamReader("ulaz.txt");
            string izraz = sr.ReadLine();
            string[] splitter = new string[] { "not", "and", "or", "xor", "implication", "(", ")" };
            List<String> variables = izraz.Split(splitter, StringSplitOptions.None).ToList();
            //cvor 2 uslov i i++
            Console.WriteLine("2 ");
            for (int i = 0; i < variables.Count; i++)
            {
                // cvor 3
                Console.WriteLine("3 ");
                if (variables.ElementAt(i).Trim().Equals(""))
                {
                    // cvor 4
                    Console.WriteLine("4 ");
                    variables.Remove(variables.ElementAt(i));
                    i--;
                }
                else
                {
                    //cvor 5
                    Console.WriteLine("5 ");
                    string pom = variables.ElementAt(i).Trim();
                    int index = variables.IndexOf(variables.ElementAt(i));
                    variables.RemoveAt(index);
                    variables.Insert(index, pom);
                }
                //cvor 3
                Console.WriteLine("2 ");
            }
            //cvor 7 izlazni
            Console.WriteLine("6 ");
            return variables.ToHashSet();
        }

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

        #region activeVarsOld
        /*
        public static String ActiveVarsOrg()
        {
            #region Nodes 
            // cvor 0
            Console.Write("1 ");
            HashSet<String> vars = ExtractVars();
            //cvor 7
            Console.Write("7 ");
            Dictionary<String, List<Boolean>> table = new Dictionary<String, List<Boolean>>();
            table = vars.ToDictionary(h => h, g => new List<Boolean>());
            int counter = 0;
            Boolean val = true;
            int n = (int)Math.Pow(2, table.Count);
            int divide = 2;
            StreamReader sr = new StreamReader("ulaz.txt");
            string izraz = sr.ReadLine();
            #endregion
            //cvor 8
            Console.Write("8 ");
            for (int i = 0; i < vars.Count; i++)
            {
                Console.Write("9 ");
                for (int k = 0; k < n; k++)
                {
                    Console.Write("10 ");
                    table[vars.ElementAt(i)].Add(val);
                    if (++counter >= n / Math.Pow(divide, i + 1))
                    {
                        Console.Write("11 ");
                        val = !val;
                        counter = 0;
                    }
                    Console.Write("9 ");
                }
                Console.Write("8 ");
            }
            //cvor 12
            Console.Write("12 ");
            List<String> operations = new List<String>();
            string pomIzraz = izraz;
            int opStartIndex = -1;
            int opEndIndex = -1;
            int indOfZatZagr = -1;
            int brojac = 0;
            string operation = "";
            string secondVar = "";
            String glavnaOperacija = "";
            List<List<bool>> listaVrednostiPodizraza = new List<List<bool>>();



            List<string> expressions = izraz.Split(new char[] { '(', ')' }).ToList();
            //cvor 13
            Console.Write("13 ");
            for (int i = 0; i < expressions.Count; i++)
            {
                Console.Write("14 ");
                List<bool> firstVarValues;
                List<bool> secondVarValues;
                if (expressions[i].Trim().Equals(""))
                {
                    Console.Write("15 ");
                    expressions.RemoveAt(i);
                    i--;
                }
                else
                {
                    Console.Write("16 ");
                    if (i % 2 == 0)
                    {
                        Console.Write("17 ");
                        if (!expressions[i].Trim().Contains(' '))
                        {
                            Console.Write("19 ");
                            listaVrednostiPodizraza.Add(new List<bool>());
                            firstVarValues = table[expressions[i].Trim()];
                            CalculateExpression(brojac, listaVrednostiPodizraza, firstVarValues, firstVarValues, "and", "", "");
                            brojac++;
                        }
                        else
                        {
                            Console.Write("20 ");
                            string[] part = expressions[i].Trim().Split(' ');
                            if (part.Length == 2)
                            {
                                Console.Write("21 ");
                                listaVrednostiPodizraza.Add(new List<bool>());
                                firstVarValues = table[part[1].Trim()];
                                CalculateExpression(brojac, listaVrednostiPodizraza, firstVarValues, firstVarValues, "and", "not", "not");
                                brojac++;
                            }
                            else if (part.Length == 5)
                            {
                                Console.Write("22 ");
                                listaVrednostiPodizraza.Add(new List<bool>());
                                firstVarValues = table[part[1].Trim()];
                                secondVarValues = table[part[4].Trim()];
                                operation = part[2].Trim();
                                CalculateExpression(brojac, listaVrednostiPodizraza, firstVarValues, secondVarValues, operation, "not", "not");
                                brojac++;
                            }
                            else if (part.Length == 3)
                            {
                                Console.Write("23 ");
                                listaVrednostiPodizraza.Add(new List<bool>());
                                firstVarValues = table[part[0].Trim()];
                                secondVarValues = table[part[2].Trim()];
                                operation = part[1].Trim();
                                CalculateExpression(brojac, listaVrednostiPodizraza, firstVarValues, secondVarValues, operation, "", "");
                                brojac++;
                            }
                            else if (part.Length == 4 && part[0].Equals("not"))
                            {
                                Console.Write("24 ");
                                listaVrednostiPodizraza.Add(new List<bool>());
                                firstVarValues = table[part[1].Trim()];
                                secondVarValues = table[part[3].Trim()];
                                operation = part[2].Trim();
                                CalculateExpression(brojac, listaVrednostiPodizraza, firstVarValues, secondVarValues, operation, "not", "");
                                brojac++;
                            }
                            else if (part.Length == 4 && part[2].Equals("not"))
                            {
                                Console.Write("25 ");
                                listaVrednostiPodizraza.Add(new List<bool>());
                                firstVarValues = table[part[0].Trim()];
                                secondVarValues = table[part[3].Trim()];
                                operation = part[1].Trim();
                                CalculateExpression(brojac, listaVrednostiPodizraza, firstVarValues, secondVarValues, operation, "", "not");
                                brojac++;
                            }
                        }
                    }
                    else
                    {
                        Console.Write("18 ");
                        glavnaOperacija = expressions[i].Trim();
                    }
                }
                Console.Write("13 ");
            }


            #region Nodes N
            //cvor 28
            Console.Write("28 ");
            List<bool> konacnaVrednostIzraza = new List<bool>();
            konacnaVrednostIzraza = listaVrednostiPodizraza[0];
            Console.Write("29 ");
            for (int i = 0; i < listaVrednostiPodizraza.Count - 1; i++)
            {
                Console.Write("30 ");
                for (int j = 0; j < listaVrednostiPodizraza[i].Count; j++)
                {
                    Console.Write("31 ");
                    konacnaVrednostIzraza[j] = new Operation(konacnaVrednostIzraza[j], listaVrednostiPodizraza[i + 1][j]).Calculate(glavnaOperacija);
                    Console.Write("30 ");
                }
                Console.Write("29 ");
            }
            Console.Write("32 ");
            String rezText = "";
            Console.Write("33 ");
            for (int i = table.Keys.Count - 1; i >= 0; i--)
            {
                bool aktivna = false;
                List<int> markirani = new List<int>();
                for (int j = 0; j < konacnaVrednostIzraza.Count; j++)
                {
                    if (konacnaVrednostIzraza.Count > (j + (int)Math.Pow(2, i + 1)/2) && !markirani.Contains(j) && konacnaVrednostIzraza[j] != konacnaVrednostIzraza[j + (int)Math.Pow(2, i + 1) / 2])
                    {
                        markirani.Add((int)(j + Math.Pow(2, i)));
                        aktivna = true;
                        rezText += table.Keys.ElementAt(table.Keys.Count - i - 1) + " je aktivna : ";
                        int broj = 0;
                        for (int k = 0; k < table.Keys.Count; k++)
                        {
                            if (!table.Keys.ElementAt(k).Equals(table.Keys.ElementAt(table.Keys.Count - i - 1)))
                            {
                                rezText += table.Keys.ElementAt(k) + "=" + table.ElementAt(k).Value[j] + " ";
                                broj++;
                            }
                        }

                        if (broj == 0)
                        {
                            rezText += "uvek";
                        }
                        else
                        {
                            rezText += " \n";
                        }
                    }
                }
                if (!aktivna)
                {
                    rezText += table.Keys.ElementAt(table.Keys.Count - i - 1) + " nije aktivna : \n";
                }
            }
            #region

            /*
            for (int i = table.Keys.Count - 1; i >= 0; i--)
            {
                bool aktivna = false;
                List<int> markirani = new List<int>();

                for (int j = 0; j < konacnaVrednostIzraza.Count; j++)
                {
                    if ((j + Math.Pow(2, i)) < konacnaVrednostIzraza.Count)
                    {
                        if (!markirani.Contains(j))
                        {
                            markirani.Add((int)(j + Math.Pow(2, i)));
                        }
                        if (!markirani.Contains(j) && konacnaVrednostIzraza[j] != konacnaVrednostIzraza[(int)(j + Math.Pow(2, i))])
                        {
                            aktivna = true;
                            rezText += table.Keys.ElementAt(table.Keys.Count - i - 1) + " je aktivna : ";
                            int broj = 0;
                            for (int k = 0; k < table.Keys.Count; k++)
                            {
                                if (!table.Keys.ElementAt(k).Equals(table.Keys.ElementAt(table.Keys.Count - i - 1)))
                                {
                                    rezText += table.Keys.ElementAt(k) + "=" + table.ElementAt(k).Value[j] + " ";
                                    broj++;
                                }
                            }
                            if (broj == 0)
                            {
                                rezText += "uvek";
                            }
                            else
                            {
                                rezText += " \n";
                            }
                        }
                    }
                }
                if (!aktivna)
                {
                    rezText += table.Keys.ElementAt(table.Keys.Count - i - 1) + " nije aktivna : \n";
                }
            }


            #endregion
            //cvor 49
            Console.Write("49 ");
            Console.WriteLine();
            Console.WriteLine(rezText);
            #endregion

            foreach (KeyValuePair<string, List<bool>> k in table)
            {
                Console.Write(k.Key + ": ");
                foreach (bool b in k.Value)
                {
                    Console.Write(b + " ");
                }
                Console.WriteLine();
            }
            return rezText;

        }


    */
        #endregion
        public static String ActiveVars(int numOfVars, Dictionary<int, List<Boolean>> table)
        {
            // cvor 1 ulazni cvor
            Console.Write("1 ");
            StreamReader sr = new StreamReader("ulaz.txt");
            string izraz = sr.ReadLine();
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
            String rezText = "\n";
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
                    Console.Write("31 ");
                    if (konacnaVrednostIzraza.Count > (j + (int)Math.Pow(2, i + 1) / 2) && konacnaVrednostIzraza[j] != konacnaVrednostIzraza[j + (int)Math.Pow(2, i + 1) / 2] && !markirani.Contains(j))
                    {
                        //cvor 32 sadrzi i inicijalizaciju petlje for
                        Console.Write("32 ");
                        markirani.Add((int)(j + Math.Pow(2, i+1)/2));
                        aktivna = true;
                        rezText += vars.ElementAt(table.Keys.ElementAt(table.Keys.Count - i - 1)) + " je aktivna : ";
                        int broj = 0;
                        //cvor 33 uslov i ++k
                        Console.Write("33 ");
                        for (int k = 0; k < table.Keys.Count; ++k)
                        {
                            Console.Write("34 ");
                            if (k!=(table.Keys.Count - i -1))
                            {
                                Console.Write("35 ");
                                rezText += vars.ElementAt(k) + "=" + table.ElementAt(k).Value[j + (int)Math.Pow(2, i + 1) / 2] + " ";
                                broj++;
                            }
                            Console.Write("33 ");
                        }
                        Console.Write("36 ");
                        if (broj == 0)
                        {
                            Console.Write("37 ");
                            rezText += "uvek\n";
                        }
                        else
                        {
                            Console.Write("38 ");
                            rezText += " \n";
                        }
                    }
                    Console.Write("30 ");
                }
                Console.Write("39 ");
                if (!aktivna)
                {
                    Console.Write("40 ");
                    rezText += vars.ElementAt(table.Keys.ElementAt(table.Keys.Count - i - 1)) + " nije aktivna : \n";
                }
                Console.Write("28 ");
            }
            #region

            #endregion
            Console.WriteLine(rezText);
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
            return rezText;

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
                listaVrednostiPodizraza[brojac-1].Add(new Operation(new Operation(firstVarValues[i]).Calculate(op1), new Operation(secondVarValues[i]).Calculate(op2)).Calculate(operacija));
                //cvor 23
                Console.Write("20 ");
            }
        }


        public bool IsPrime(int candidate)
        {
            if(candidate < 0)
            {
                throw new Exception();
            }

            if((candidate % 2) == 0)
            {
                if(candidate == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            for (int i = 3; i*i <= candidate; i+=2)
            {
                if((candidate % i) == 0)
                {
                    return false;
                }
            }
            return candidate != 1;
        }

        //[Fact]
        //public void IsPrime_InputIsLessThan0
        //{
        //    Assert.Throws<Exception>(()=>_primeService.IsPrime(-5));
        //}

        [TestClass]
        public class UnitTest1
        {
            [TestMethod]
            public void TestMethod1()
            {
                Assert.AreEqual("0", Methods.ActiveVars(CountVars(), MakeTable(CountVars())));
            }
        }
    }
}


#region While petlja

/*

//cvor 13
Console.Write("13 ");
while (izraz.Length > 0)
{
    //cvor 14
    Console.Write("14 ");
    List<bool> firstVarValues;
    List<bool> secondVarValues;
    listaVrednostiPodizraza.Add(new List<bool>());

    izraz = izraz.Trim();
    int indOfOtvZagr = izraz.IndexOf('(');
    izraz = izraz.Substring(indOfOtvZagr + 1, izraz.Length - 1).Trim();

    opStartIndex = izraz.IndexOf(' ');


    if (opStartIndex == -1)
    {
        //cvor 15
        Console.Write("15 ");
        indOfZatZagr = izraz.IndexOf(')');
        string firstVarNot = izraz.Substring(0, indOfZatZagr).Trim();
        izraz = izraz.Substring(indOfZatZagr + 1, izraz.Length - indOfZatZagr - 1).Trim();
        firstVarValues = table[firstVarNot];
        CalculateExpression(brojac, listaVrednostiPodizraza, firstVarValues, firstVarValues, "and", "", "");
        FindMainOperation(ref izraz, ref glavnaOperacija);
        brojac++;
        continue;
    }
    //cvor 16
    Console.Write("16 ");
    string firstVar = izraz.Substring(0, opStartIndex).Trim();

    if (firstVar.Equals("not"))
    {
        //cvor 17
        Console.Write("17 ");
        string operationNot = firstVar;
        indOfZatZagr = izraz.IndexOf(')');
        string firstVarNot = izraz.Substring(operationNot.Length + 1, indOfZatZagr - operationNot.Length - 1).Trim();
        if (firstVarNot.Contains(' '))
        {
            //cvor 18
            Console.Write("18 ");
            int index = firstVarNot.IndexOf(' ');
            string prviOperand = firstVarNot.Substring(0, index);
            firstVarNot = firstVarNot.Substring(index, firstVarNot.Length - index).Trim();
            index = firstVarNot.IndexOf(' ');
            string operacija = firstVarNot.Substring(0, index);
            firstVarNot = firstVarNot.Substring(index, firstVarNot.Length - index).Trim();
            secondVar = firstVarNot;
            index = izraz.IndexOf(' ');
            if (secondVar.Contains(' '))
            {
                //cvor 19
                Console.Write("19 ");
                string not = secondVar.Split(' ')[0];
                string secondRealVar = secondVar.Split(' ')[1].Substring(0, secondVar.Split(' ')[1].Length);
                firstVarValues = table[prviOperand];
                secondVarValues = table[secondRealVar];
                string op1 = "not";
                string op2 = "not";
                izraz = izraz.Substring(indOfZatZagr + 1, izraz.Length - indOfZatZagr - 1).Trim();
                CalculateExpression(brojac, listaVrednostiPodizraza, firstVarValues, secondVarValues, operacija, op1, op2);
                FindMainOperation(ref izraz, ref glavnaOperacija);
                brojac++;
                continue;

            }
            else
            {
                //cvor 20
                Console.Write("20 ");
                firstVarValues = table[prviOperand];
                secondVarValues = table[secondVar];
                string op1 = "not";
                string op2 = "";
                izraz = izraz.Substring(indOfZatZagr + 1, izraz.Length - indOfZatZagr - 1).Trim();

                CalculateExpression(brojac, listaVrednostiPodizraza, firstVarValues, secondVarValues, operacija, op1, op2);
                FindMainOperation(ref izraz, ref glavnaOperacija);

                brojac++;
                continue;
            }

        }
        else
        {
            //cvor 24
            Console.Write("24 ");
            izraz = izraz.Substring(indOfZatZagr + 1, izraz.Length - indOfZatZagr - 1).Trim();
            firstVarValues = table[firstVarNot];
            CalculateExpression(brojac, listaVrednostiPodizraza, firstVarValues, firstVarValues, "and", operationNot, operationNot);
            FindMainOperation(ref izraz, ref glavnaOperacija);
            brojac++;
            continue;
        }
    }
    //cvor 25
    Console.Write("25 ");
    izraz = izraz.Substring(firstVar.Length, izraz.Length - firstVar.Length).Trim();
    opEndIndex = izraz.IndexOf(' ');
    operation = izraz.Substring(0, opEndIndex).Trim();
    izraz = izraz.Substring(opEndIndex +1, izraz.Length - opEndIndex-1).Trim();
    indOfZatZagr = izraz.IndexOf(')');
    secondVar = izraz.Substring(0, indOfZatZagr).Trim();
    if (secondVar.Contains(' '))
    {
        //cvor 26
        Console.Write("26 ");
        string not = secondVar.Split(' ')[0];
        string secondRealVar = secondVar.Split(' ')[1].Substring(0, secondVar.Split(' ')[1].Length);
        firstVarValues = table[firstVar];
        secondVarValues = table[secondRealVar];
        izraz = izraz.Substring(indOfZatZagr + 1, izraz.Length - indOfZatZagr - 1).Trim();
        CalculateExpression(brojac, listaVrednostiPodizraza, firstVarValues, secondVarValues, operation, "", "not");
        FindMainOperation(ref izraz, ref glavnaOperacija);
        brojac++;
        continue;

    }
    //cvor 27
    Console.Write("27 ");
    izraz = izraz.Substring(indOfZatZagr + 1, izraz.Length - indOfZatZagr - 1).Trim();
    firstVarValues = table[firstVar];
    secondVarValues = table[secondVar];
    CalculateExpression(brojac, listaVrednostiPodizraza, firstVarValues, secondVarValues, operation, "", "");
    FindMainOperation(ref izraz, ref glavnaOperacija);
    brojac++;
    //cvor 13
    Console.Write("13 ");
}

*/
#endregion
