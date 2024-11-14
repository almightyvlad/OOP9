using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Auto
{
    public string Name { get; set; }
    public string Type { get; set; }
    public Auto(string name, string type)
    {
        Name = name;
        Type = type;
    }
}

public class AutoCollection : IList<Auto>
{ 
    private Dictionary<int, Auto> autos = new Dictionary<int, Auto>();
    private int currentIndex = 0;
    public Auto this[int index]
    {
        get => autos[index];
        set => autos[index] = value;
    }
    public int IndexOf(Auto auto)
    {
        foreach(var pair  in autos)
        {
            if (pair.Key.Equals(auto.Name))
            {
                return pair.Key;
            }
            
        }
        return -1;
    }

    public void Insert(int index, Auto auto)
    {
        if (!autos.ContainsKey(index))
        {
            autos[index] = auto;
        }
        
    }

    public int Count => autos.Count;
    public bool IsReadOnly => false;
    public void RemoveAt(int index) => autos.Remove(index);
    public void Add(Auto auto)
    {
        autos[currentIndex++] = auto;
    }
    public void Clear() => autos.Clear();   
    public bool Contains(Auto auto) => autos.ContainsValue(auto);
    public void CopyTo(Auto[] array, int arrayIndex)
    {
        foreach (var pair in autos.Values)
        {
            array[arrayIndex++] = pair;
        } 
    }
    public bool Remove(Auto auto)
    {
        var index = IndexOf(auto);
        if (index >= 0)
        {
            RemoveAt(index);
            return true;
        }

        return false;
    }
    
    public IEnumerator<Auto> GetEnumerator() => autos.Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public Auto FindByName(string name)
    {
        foreach(var pair in autos.Values)
        {
            if (pair.Name.Equals(name))
            {
                return pair;
            }

        }

        return null;
    }
}

namespace OOP9
{
    internal class Program
    {

        static void Main(string[] args)
        {

            // 1
            AutoCollection autoList = new AutoCollection();

            Auto auto1 = new Auto("Mercedes-Benz S63 AMG coupe", "Coupe");
            Auto auto2 = new Auto("BMW X6", "SUV");
            Auto auto3 = new Auto("Audi A5", "Sedan");

            autoList.Add(auto1);
            autoList.Add(auto2);
            autoList.Add(auto3);

            foreach (Auto auto in autoList)
            {
                Console.WriteLine(auto.Name);
            }

            Console.WriteLine("===================");

            autoList.Remove(auto2);

            Console.WriteLine("After removing:");
            foreach (Auto auto in autoList)
            {
                Console.WriteLine(auto.Name);
            }

            Console.WriteLine("===================");

            Auto findAuto = autoList.FindByName("Audi A5");

            if (findAuto != null)
            {
                Console.WriteLine(findAuto.Name);
            } 
            else {
                Console.WriteLine("Not found");
            }

            // 2

            List<string> dict = new List<string>()
            {
                { "First" },
                { "Second" },
                { "Third" },

            };

            foreach(var dictEl in dict)
            {
                Console.WriteLine($" Value: {dictEl}");
            }

            Console.WriteLine("=====================");

            int n = 2;
            for (int i = 0; i < n; i++)
            {
                if (dict.Count > 0)
                {
                    dict.RemoveAt(i);
                }
            }

            foreach (var dictEl in dict)
            {
                Console.WriteLine($"Value: {dictEl}");
            }

            Console.WriteLine("=====================");

            dict.Add("Fourth");


            List<string> list = new List<string>(dict);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("=====================");

            var listFind = list.Find(l => l == "Fifth");

            if (listFind != null)
            {
                Console.WriteLine(listFind);
            } 
            else
            {
                Console.WriteLine("Not found");
            }

            Console.WriteLine("===================");

            // 3

            ObservableCollection<Auto> obsAutos = new ObservableCollection<Auto>();
            obsAutos.CollectionChanged += AutoList_CollectionChanged;

            obsAutos.Add(auto1);
            obsAutos.Add(auto2);
            obsAutos.Add(auto3);

            obsAutos.Remove(auto2); 

            obsAutos.Clear(); 
        }

        private static void AutoList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (Auto newAuto in e.NewItems)
                {
                    Console.WriteLine($"Added: {newAuto.Name}");
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (Auto oldAuto in e.OldItems)
                {
                    Console.WriteLine($"Removed: {oldAuto.Name}");
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
            {
                Console.WriteLine("Collection cleared.");
            }
        }
    }
}
