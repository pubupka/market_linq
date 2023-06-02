using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace товары_поставщики_поставки_linq
{
    public class Item
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public Item(int id, string itemName)
        {
            Id = id;
            ItemName = itemName;
        }
    }
    public class Provider
    {
        public int Id { get; set; }
        public string ProviderName { get; set; }
        public Provider(int id, string providerName)
        {
            Id = id;
            ProviderName = providerName;
        }
    }
    public class Shipment
    {
        public int Id_Of_Item { get; set; }
        public int Id_Of_Provider { get; set; }
        public int Count_Of_Item { get; set; }
        public string Date { get; set; }

        public Shipment(int id_Of_Item, int id_Of_Provider, int count_Of_Item, string date)
        {
            Id_Of_Item = id_Of_Item;
            Id_Of_Provider = id_Of_Provider;
            Count_Of_Item = count_Of_Item;
            Date = date;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Item[] items =
            {
                new Item(1, "гиря"),
                new Item(2, "штанга"),
                new Item(3, "блин"),
                new Item(4, "гантель"),
                new Item(5, "тяжёлая гиря"),
                new Item(6, "очень тяжёлая гиря"),
                new Item(7, "не очень тяжёлая гиря"),
            };

            Provider[] providers =
            {
                new Provider(1, "михалыч"),
                new Provider(2, "палыч"),
                new Provider(3, "семёныч"),
            };

            Shipment[] shipments =
            {
                new Shipment(1, 1, 100, "02/06/2023"),
                new Shipment(5, 1, 200, "02/06/2023"),
                new Shipment(6, 1, 300, "02/06/2023"),
                new Shipment(7, 1, 400, "02/06/2023"),
                new Shipment(2, 2, 1000, "01/06/2023"),
                new Shipment(3, 2, 2000, "01/06/2023"),
                new Shipment(5, 2, 250, "02/06/2023"),
                new Shipment(4, 3, 50, "03/06/2023"),
            };

            //поставщик-товар-количество по дате
            var Provider_And_Count_By_Date = from shipment in shipments
                                             join provider in providers on shipment.Id_Of_Provider equals provider.Id
                                             join item in items on shipment.Id_Of_Item equals item.Id
                                             group new { name = provider.ProviderName, count = shipment.Count_Of_Item,
                                                                          item = item.ItemName, date = shipment.Date } by shipment.Date;

            //поставщики по товару
            var Providers_By_Item = from shipment in shipments
                                   join provider in providers on shipment.Id_Of_Provider equals provider.Id
                                   join item in items on shipment.Id_Of_Item equals item.Id
                                   group provider.ProviderName by item.ItemName;

            //товары по поставщику
            var Items_By_Provider = from shipment in shipments
                                    join provider in providers on shipment.Id_Of_Provider equals provider.Id
                                    join item in items on shipment.Id_Of_Item equals item.Id
                                    group item.ItemName by provider.ProviderName;

            foreach (var Date_Group in Provider_And_Count_By_Date)
            {
                Console.WriteLine(Date_Group.Key);
                foreach(var provider_and_count in Date_Group)
                {
                    Console.WriteLine($"{provider_and_count.name} поставил товар '{provider_and_count.item}' в количестве: {provider_and_count.count} штук.");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();

            foreach (var item in Providers_By_Item)
            {
                Console.Write($"Товар: {item.Key}, поставщики: ");
                foreach (var provider in item)
                {
                    Console.Write(provider + "   ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();

            foreach (var provider in Items_By_Provider)
            {
                Console.Write($"Поставщик: {provider.Key}, товары: ");
                foreach (var item in provider)
                {
                    Console.Write(item + "   ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
