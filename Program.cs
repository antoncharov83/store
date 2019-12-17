using System;
using System.Collections.Generic;

namespace storeroom
{
    class Program
    {
        static void Main(string[] args)
        {
            //Box box = new Box();
            //box.Depth = 120;
            //box.Height = 100;
            //box.Weight = 80;
            //box.ProductionDate = DateTime.Parse("12.12.2019");
            //Box box2 = new Box();
            //box2.Depth = 20;
            //box2.Height = 70;
            //box2.Weight = 50;
            //box2.ProductionDate = DateTime.Parse("01.12.2019");
            //Box box3 = new Box();
            //box3.Depth = 60;
            //box3.Height = 40;
            //box3.Weight = 70;
            //box3.ExpirationDate = DateTime.Parse("12.11.2019");

            //Pallete pallete = new Pallete();
            //pallete.Depth = 300;
            //pallete.Height = 500;
            //pallete.Width = 700;
            //pallete.addBox(box);
            //pallete.addBox(box2);

            //Pallete pallete2 = new Pallete();
            //pallete2.Depth = 200;
            //pallete2.Height = 400;
            //pallete2.Width = 900;
            //pallete2.addBox(box3);

            Store store = new Store();

            //store.addPallete(pallete);
            //store.addPallete(pallete2);

            //store.saveToJson("store.json");
            store.loadFromJson("store.json");
            List<Pallete> palletes = store.getGroupedBy();

            palletes.ForEach(p => System.Console.WriteLine("ExDate:{0}, Weight:{1}", p.getExpirationDate(), p.getWeight()));

            palletes = store.get3MaxExpirationDate();
            System.Console.WriteLine("=================================================================================");
            palletes.ForEach(p => {
                System.Console.WriteLine("ExDate:{0}, Weight:{1}, Volume:{2}", p.getExpirationDate(), p.getWeight(), p.getVolume());
                p.boxes.ForEach(b => System.Console.WriteLine("    ExDate:{0}", b.ExpirationDate));
            });
        }
    }
}
