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

            using (FileStream file = new FileStream(filename, FileMode.Create, System.IO.FileAccess.Write))
            {
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
        public IEnumerable<dynamic> /*Dictionary<DateTime?, List<Pallete>>*/ getGroupedBy()
        {
            //var grouped = palletes
            //    .GroupBy(pallete => pallete.getExpirationDate())
            //    .OrderBy(palletes => palletes.Key)
            //    .ToDictionary(palletes => palletes.Key, palletes => palletes.OrderBy(pallete => pallete.getWeight()).ToList());
            var g = palletes
                .GroupBy(p => p.getExpirationDate())
                .Select(x => new { x.Key, Items = x.OrderBy(z => z.getExpirationDate()).ToArray() })
                .OrderBy(p => p.Key).ToArray();
            
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
