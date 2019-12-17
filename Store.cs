using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace storeroom
{
    class Store
    {
        private List<Pallete> palletes;

        public Store() { palletes = new List<Pallete>(); }
        public void addPallete(Pallete pallete) { palletes.Add(pallete); }

        public void saveToJson(string filename) {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<Pallete>), new[] { typeof(Pallete), typeof(Box) });
            //MemoryStream ms = new MemoryStream();
            //jsonSerializer.WriteObject(ms, palletes);

            using (FileStream file = new FileStream(filename, FileMode.Create, System.IO.FileAccess.Write))
            {
            //    byte[] bytes = new byte[ms.Length];
            //    ms.Read(bytes, 0, (int)ms.Length);
            //    file.Write(bytes, 0, bytes.Length);
            //    ms.Close();
            //var serializedString = Encoding.UTF8.GetString(ms.ToArray());
            //ms.Close();
            //File.WriteAllText(filename, serializedString);
            jsonSerializer.WriteObject(file, palletes);
            }
        }

        public void loadFromJson(string filename) {
            using (MemoryStream ms = new MemoryStream())
            using (FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[file.Length];
                file.Read(bytes, 0, (int)file.Length);
                ms.Write(bytes, 0, (int)file.Length);
                ms.Position = 0;
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<Pallete>), new[] { typeof(Pallete), typeof(Box) });
                palletes = (List<Pallete>)jsonSerializer.ReadObject(ms);
            }
        }

        public List<Pallete> getGroupedBy() {
            //var groupedBoxes = from pallete in palletes
            //                   orderby pallete.getWeight()
            //                   group pallete by pallete.getExpirationDate()
            //                  ;
            var g = palletes
                .OrderBy(p => p.getWeight())
                .GroupBy(p => p.getExpirationDate())
                .SelectMany(p => p)
                .ToList()
                .OrderBy(p => p.getExpirationDate())
                .ToList();
            return g;

        }
        public List<Pallete> get3MaxExpirationDate() {
            var g = palletes
                .OrderBy(p => p.getVolume())
                .ThenBy(p => p.boxes.Max(b => b.ExpirationDate))
                .Take(3)
                .ToList();

            return g;
        }
    }
}
