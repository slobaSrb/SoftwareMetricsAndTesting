using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrika_Prime_Path_Coverage
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("ulaz.txt");
            Dictionary<int, List<int>> graf = new Dictionary<int, List<int>>();
            List<List<int>> prostiPutevi = new List<List<int>>();
            List<List<int>> testPutevi = new List<List<int>>();


            int brCvorova = int.Parse(sr.ReadLine().Split()[0]);
            int brGrana = int.Parse(sr.ReadLine().Split()[0]);

            for (int i = 0; i < brGrana; i++)
            {
                string[] words = sr.ReadLine().Split();
                int kljuc = int.Parse(words[0]);
                int vrednost = int.Parse(words[1]);
                if (!graf.ContainsKey(kljuc))
                {
                    graf.Add(kljuc, new List<int>());
                    graf[kljuc].Add(vrednost);
                }
                else
                {
                    graf[kljuc].Add(vrednost);
                }
            }

            int pocetniCvor = int.Parse(sr.ReadLine().Split()[0]);
            string[] zavrsniWords = sr.ReadLine().Split();

            List<int> zavrsniCvorovi = new List<int>();

            for (int i = 0; i < zavrsniWords.Length; i++)
            {
                int zavrsniCvor = -1;
                if (int.TryParse(zavrsniWords[i], out zavrsniCvor))
                {
                    zavrsniCvorovi.Add(zavrsniCvor);
                }
            }
            //za svaki prost put jedan test put koji ga pokriva

            List<List<int>> primePaths = Program.primePaths(graf);
            StreamWriter sw = new StreamWriter("izlaz.txt");
            for (int i = 0; i < primePaths.Count; i++)
            {
                string izlaz = "";
                for (int j = 0; j < primePaths[i].Count; j++)
                {
                    Console.Write(primePaths[i][j] + " ");
                    izlaz += primePaths[i][j].ToString() + " ";
                }
                Console.WriteLine();
                sw.WriteLine(izlaz);
            }
            sw.WriteLine();
            sw.Flush();
            
            //upisati u fajl proste puteve

            //svi test putevi koji ih pokrivaju
            Console.WriteLine();
            for (int i = 0; i < primePaths.Count; i++)
            {
                List<int> lista = testPut(pocetniCvor, zavrsniCvorovi, primePaths[i], graf);
                string izlaz = "";
                for (int j = 0; j < lista.Count; j++)
                {
                    Console.Write(lista[j] + " ");
                    izlaz += lista[j].ToString() + " ";
                }
                Console.WriteLine();
                sw.WriteLine(izlaz);
            }
            sw.Close();


        }

        static List<int> testPut(int pocetniCvor, List<int> zavrsniCvorovi, List<int> primePath, Dictionary<int, List<int>> graf)
        {
            List<int> prvaLista = new List<int>();
            List<int> drugaLista = new List<int>();
            Stack<int> stak = new Stack<int>();
            List<List<int>> putevi = new List<List<int>>();
            stak.Push(pocetniCvor);
            putevi.Add(new List<int>());
            putevi[0].Add(pocetniCvor);
            int p = 0;
            bool found = false;
            Random rand = new Random();

            while (stak.Count > 0)
            {
                if (putevi[p][putevi[p].Count - 1] == primePath[0])
                {
                    prvaLista = new List<int>(putevi[p]);
                    found = true;
                    break;
                }
                int cvor = stak.Pop();
                int broj = 0;
                try
                {

                    for (int i = 0; i < graf[cvor].Count; i++)
                    {
                        //    if (putevi[i].Contains(cvor) && putevi[i].Contains(graf[cvor][i]))
                        //    {

                        List<int> list = new List<int>();
                        list.Add(graf[cvor][i]);
                        putevi.Add(putevi.Where(put=>put[put.Count-1]==cvor).Last().Concat(list).ToList());
                        if (putevi[p + i + 1][putevi[p + i + 1].Count - 1] == primePath[0])
                        {
                            prvaLista = new List<int>(putevi[p + i + 1]);
                            found = true;
                            broj = i;
                            break;
                        }

                        stak.Push(graf[cvor][i]);
                        //}
                    }
                    if (found)
                    {
                        break;
                    }
                    p += graf[cvor].Count;
                    //mozda treba random broj od 1 do graf[cvor].Count
                }
                catch (KeyNotFoundException e)
                {
                    //.....
                }
                if (prvaLista.Count > 0)
                {
                    break;
                }
            }

            stak.Clear();
            putevi.Clear();
            stak.Push(primePath[primePath.Count - 1]);
            putevi.Add(new List<int>());
            putevi[0].Add(primePath[primePath.Count - 1]);
            p = 0;
            found = false;



            while (stak.Count > 0)
            {
                if (zavrsniCvorovi.Contains(primePath[primePath.Count - 1]))
                {
                    drugaLista = new List<int>(primePath[primePath.Count - 1]);
                    found = true;
                    break;
                }
                int cvor = stak.Pop();
                int broj = 0;
                try
                {
                    if (zavrsniCvorovi.Contains(cvor))
                    {
                        found = true;
                        break;
                    }
                    for (int i = 0; i < graf[cvor].Count; i++)
                    {
                        //    if (putevi[i].Contains(cvor) && putevi[i].Contains(graf[cvor][i]))
                        //    {
                        List<int> list = new List<int>();
                        list.Add(graf[cvor][i]);
                        putevi.Add(putevi.Where(put => put[put.Count - 1] == cvor).Last().Concat(list).ToList());



                        for (int j = 0; j < zavrsniCvorovi.Count; j++)
                        {
                            if (zavrsniCvorovi[j] == putevi[p + i + 1][putevi[p + i + 1].Count - 1])
                            {
                                found = true;
                                broj = i;
                            }
                            if (found)
                            {
                                break;
                            }
                        }

                        if (found)
                        {
                            drugaLista = new List<int>(putevi[p + i + 1]);
                            break;
                        }
                        stak.Push(graf[cvor][i]);
                        //}
                    }
                    p += graf[cvor].Count;
                    //mozda treba random broj od 1 do graf[cvor].Count
                }
                catch (KeyNotFoundException e)
                {
                    //.....
                }
                if (drugaLista.Count > 0)
                {
                    break;
                }
            }
            if (prvaLista.Count > 0) prvaLista.RemoveAt(prvaLista.Count - 1);
            if (drugaLista.Count > 0) drugaLista.RemoveAt(0);
            return prvaLista.Concat(primePath).ToList().Concat(drugaLista).ToList();
        }
        static bool potput(List<int> potput, List<int> put)
        {
            if (potput.Count > put.Count) return false;

            for (int i = 0; i < put.Count - potput.Count + 1; i++)
            {
                if (Enumerable.SequenceEqual(put.GetRange(i, potput.Count), potput))
                {
                    return true;
                }
            }

            return false;
        }

        static List<List<int>> primePaths(Dictionary<int, List<int>> graf)
        {
            List<List<int>> jednostavni = new List<List<int>>();

            Stack<int> stek = new Stack<int>();

            for (int j = 0; j < graf.Count; j++)
            {
                int pom = graf.Keys.ToArray()[j];
                List<int> pomList = new List<int>();
                pomList.Add(pom);
                jednostavni.Add(pomList);
            }

            int i = 0;

            while (i < jednostavni.Count)
            {
                int j = jednostavni[i].Count - 1;
                try
                {
                    for (int k = 0; k < graf[jednostavni[i][j]].Count; k++)
                    {
                        List<int> put = new List<int>();
                        put = new List<int>(jednostavni[i]);
                        put.Add(graf[jednostavni[i][j]][k]);
                        if (!put.GetRange(put.Count < 2 ? 0 : 1, put.Count - 2 < 0 ? 0 : put.Count - 1).Contains(put[0])
                        && !put.GetRange(0, put.Count - 2 < 0 ? 0 : put.Count - 1).Contains(put[put.Count - 1]))
                        {
                            jednostavni.Add(put);
                        }
                        if (put[0] == put[put.Count - 1] && !put.GetRange(put.Count < 2 ? 0 : 1, put.Count - 2 < 0 ? 0 : put.Count - 2).Contains(put[put.Count - 1]))
                        {
                            jednostavni.Add(put);
                            continue;
                        }


                    }
                }
                catch (KeyNotFoundException e)
                {
                    Console.WriteLine(e);
                }
                i++;
            }


            for (int k = 0; k < jednostavni.Count - 1; k++)
            {
                for (int l = k + 1; l < jednostavni.Count; l++)
                {
                    if (potput(jednostavni[k], jednostavni[l]))
                    {
                        jednostavni.RemoveAt(k);
                        k--;
                        break;
                    }
                }
            }
            return jednostavni;
        }
    }
}
