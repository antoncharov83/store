using System;
using System.Collections.Generic;

namespace storeroom
{
    class Program
    {
        static void Main(string[] args)
        {
            Box box = new Box();
            box.Id = 1;
            box.Depth = 120;
            box.Height = 100;
            box.Width = 50;
            box.Weight = 80;
            box.ProductionDate = DateTime.Parse("12.12.2019");
            
            Box box2 = new Box();
            box2.Id = 2;
            box2.Depth = 20;
            box2.Height = 70;
            box2.Width = 60;
            box2.Weight = 50;
            box2.ProductionDate = DateTime.Parse("01.12.2019");
            
            Box box3 = new Box();
            box3.Id = 3;
            box3.Depth = 60;
            box3.Height = 40;
            box3.Width = 60;
            box3.Weight = 70;
            box3.ExpirationDate = DateTime.Parse("10.11.2019");
            
            Box box4 = new Box();
            box4.Id = 4;
            box4.Depth = 65;
            box4.Height = 45;
            box4.Width = 90;
            box4.Weight = 72;
            box4.ExpirationDate = DateTime.Parse("10.11.2019");
            
            Box box5 = new Box();
            box5.Id = 5;
            box5.Depth = 54;
            box5.Height = 21;
            box5.Width = 70;
            box5.Weight = 23;
            box5.ProductionDate = DateTime.Parse("02.10.2019");

            Pallete pallete = new Pallete();
            pallete.Id = 1;
            pallete.Depth = 300;
            pallete.Height = 500;
            pallete.Width = 700;
            pallete.addBox(box);
            pallete.addBox(box2);

            Pallete pallete2 = new Pallete();
            pallete2.Id = 2;
            pallete2.Depth = 200;
            pallete2.Height = 400;
            pallete2.Width = 900;
            pallete2.addBox(box3);

            Pallete pallete3 = new Pallete();
            pallete3.Id = 3;
            pallete3.Depth = 150;
            pallete3.Height = 340;
            pallete3.Width = 777;
            pallete3.addBox(box4);
            pallete3.addBox(box5);

            Store store = new Store();

            store.addPallete(pallete);
            store.addPallete(pallete2);
            store.addPallete(pallete3);

            //store.saveToJson("store.json");
            //store.loadFromJson("store.json");
            /*Dictionary<DateTime?,List<Pallete>>*/
            IEnumerable<dynamic> palletesDict = store.getGroupedBy();

            foreach (var groupedPalletes in palletesDict)
            {
                System.Console.WriteLine("ExDate:{0}", groupedPalletes.Key);
                foreach (var p in groupedPalletes.Items)
                {
                    System.Console.WriteLine("=====weight:{0}, id:{1}", p.getWeight(), p.Id);
                }
            }
            List<Pallete> palletes = store.get3MaxExpirationDate();
            System.Console.WriteLine("=================================================================================");
            palletes.ForEach(p => {
                System.Console.WriteLine("ExDate:{0}, Weight:{1}, Volume:{2}", p.getExpirationDate(), p.getWeight(), p.getVolume());
                p.boxes.ForEach(b => System.Console.WriteLine("    ExDate:{0}", b.ExpirationDate));
            });
        }
    }
}
