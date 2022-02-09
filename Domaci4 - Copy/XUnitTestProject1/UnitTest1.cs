using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Xunit;
using System.Linq;
using Domaci4;
namespace XUnitTestProject1
{
    // u projektu Domaci4 postoje ulaz i izlaz fajlovi koji vraca trazeno, broj promenljivih i aktivne varijable
    // ulazTest.txt za testiranje ActiveVars metode unosi se izraz odvojen zarezom od ocekivanog izlaza
    // ulazTestExtract.txt za testiranje da li su navedene (ocekivane) varijable odvojene zarezom od izraza vracene
    // ulazTestPrimePaths.txt za testiranje da li se prosti putevi nalaze u test putu koji izvrsi metoda ExtractVars za zadati test ulaz
    public class UnitTest1
    {
        [Theory]
        [ClassData(typeof(DataClass))]

        public void ExpectedActiveVars(string izraz, int numOfVars, Dictionary<int, List<bool>> table, HashSet<String> expected)
        {
            var result = PrimeService.ActiveVars(izraz, numOfVars, table);
            Assert.Equal(expected, result);
        }

        public class DataClass : IEnumerable<object[]>
        {

            public IEnumerator<object[]> GetEnumerator()
            {
                PrimeService _primeService = new PrimeService();
                StreamReader sr = new StreamReader("ulazTest.txt");
                while (!sr.EndOfStream)
                {
                    HashSet<string> expectedSet = new HashSet<string>();
                    string line = sr.ReadLine();
                    string[] izraz = line.Split(",");
                    string firstPart = izraz[0].Trim();
                    for (int i = 1; i < izraz.Length; i++)
                    {
                        expectedSet.Add(izraz[i].Trim());
                    }
                    yield return new object[] { firstPart, PrimeService.CountVars(firstPart), PrimeService.MakeTable(PrimeService.CountVars(firstPart)), expectedSet };
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        [Theory]
        [ClassData(typeof(DataClassExtract))]
        public void ExpectedExtractedVars(string izraz, HashSet<String> expected)
        {
            var result = PrimeService.ExtractVars(izraz);
            Assert.Equal(expected, result);
        }
        public class DataClassExtract : IEnumerable<object[]>
        {

            public IEnumerator<object[]> GetEnumerator()
            {
                PrimeService _primeService = new PrimeService();
                StreamReader sr = new StreamReader("ulazTestExtract.txt");
                while (!sr.EndOfStream)
                {
                    HashSet<string> expectedSet = new HashSet<string>();
                    string line = sr.ReadLine();
                    string[] izraz = line.Split(",");
                    string firstPart = izraz[0].Trim();
                    for (int i = 1; i < izraz.Length; i++)
                    {
                        expectedSet.Add(izraz[i].Trim());
                    }
                    yield return new object[] { firstPart, expectedSet };
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(DataClassPaths))]
        public void ArePrimePathsInTestPath(bool result, List<int> path)
        {
            //Domaci4.PrimeService.path;
            Assert.True(result,$"{path.ToString()} is in test");
        }

        public class DataClassPaths : IEnumerable<object[]>
        {

            public IEnumerator<object[]> GetEnumerator()
            {
                List<List<int>> primes; 
                StreamReader sr = new StreamReader("ulazTestPrimePaths.txt");
                while (!sr.EndOfStream)
                {
                    if (sr.ReadLine().Contains("prime"))
                    {
                        primes = new List<List<int>>();
                        string nextLine = sr.ReadLine();
                        while (!nextLine.Contains("test"))
                        {
                            primes.Add(new List<int>());
                            List<string> pom = nextLine.Split(',').ToList();
                            for (int i = 0; i < pom.Count; i++)
                            {
                                primes[primes.Count - 1].Add(int.Parse(pom[i].Trim()));
                            }
                            nextLine = sr.ReadLine();
                        }
                        string izraz = sr.ReadLine().Trim();
                        Domaci4.PrimeService.ExtractVars(izraz);

                        for (int i = 0; i < primes.Count; i++)
                        {
                            var result = Domaci4.PrimeService.path.ListContains(primes[i]);
                            yield return new object[] { result, primes[i] };
                        }
                    }
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }



        //[Fact]
        //public void Test1()
        //{
        //    Assert.Throws<Exception>(() => _primeService.IsPrime(-5));
        //}
        //[Fact]
        //public void InputIs1_ReturnFalse()
        //{
        //    var result = _primeService.IsPrime(1);
        //    Assert.False(result, "1 it is not prime");
        //}
        //[Theory]
        //[InlineData(0)]
        //[InlineData(1)]
        //public void isPrime_ValuesLessThan2_ReturnFalse(int value)
        //{
        //    var result = _primeService.IsPrime(value);
        //    Assert.False(result, $"{value} should not be prime");
        //}

        //[Theory]
        //[InlineData(2, true)]
        //[InlineData(3, true)]
        //[InlineData(10, false)]
        //public void isPrime_Values(int value, bool expectedResult)
        //{
        //    var result = _primeService.IsPrime(value);
        //    Assert.Equal(result, expectedResult);
        //}

    }
    public static class Extension
    {
        public static bool ListContains(this List<int> test, List<int> prime)
        {
            string testStr = "";
            string primeStr = "";

            foreach (var item in test)
            {
                testStr += item + " ";
            }

            foreach (var item in prime)
            {
                primeStr += item + " ";
            }

            if (testStr.Trim().Contains(primeStr.Trim()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    #region Collapse comments
    /*  public class PrimeService
      {
          public Dictionary<int, List<Boolean>> MakeTable(int numOfVars)
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
          public int CountVars(string izraz)
          {
              return ExtractVars(izraz).Count;
          }
          public HashSet<String> ExtractVars(string izraz)
          {

              string[] splitter = new string[] { "not", "and", "or", "xor", "implication", "(", ")" };

              HashSet<String> variables = izraz.Split(splitter, StringSplitOptions.None).ToHashSet();

              for (int i = 0; i < variables.Count; i++)
              {
                  if (variables.ElementAt(i).Trim().Equals(""))
                  {
                      variables.Remove(variables.ElementAt(i));
                      i--;
                  }
                  else
                  {
                      string pom = variables.ElementAt(i).Trim();
                      variables.Remove(variables.ElementAt(i));
                      variables.Add(pom);
                  }
              }

              return variables;
          }
          public HashSet<string> ActiveVars(string izraz, int numOfVars, Dictionary<int, List<Boolean>> table)
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
                      Console.Write("31 ");
                      if (konacnaVrednostIzraza.Count > (j + (int)Math.Pow(2, i + 1) / 2) && konacnaVrednostIzraza[j] != konacnaVrednostIzraza[j + (int)Math.Pow(2, i + 1) / 2] && !markirani.Contains(j))
                      {
                          //cvor 32 sadrzi i inicijalizaciju petlje for
                          Console.Write("32 ");
                          markirani.Add((int)(j + Math.Pow(2, i + 1) / 2));
                          aktivna = true;
                          rez.Add(vars.ElementAt(table.Keys.ElementAt(table.Keys.Count - i - 1)) + " je aktivna [");
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
                  if (!aktivna)
                  {
                      Console.Write("40 ");
                      rez.Add(vars.ElementAt(table.Keys.ElementAt(table.Keys.Count - i - 1)) + " nije aktivna!");
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
          public bool IsPrime(int candidate)
          {
              if (candidate < 0)
              {
                  throw new Exception();
              }

              if ((candidate % 2) == 0)
              {
                  if (candidate == 2)
                  {
                      return true;
                  }
                  else
                  {
                      return false;
                  }
              }

              for (int i = 3; i * i <= candidate; i += 2)
              {
                  if ((candidate % i) == 0)
                  {
                      return false;
                  }
              }
              return candidate != 1;
          }
      }*/
    #endregion
}
