using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.Serialization;

namespace storeroom
{
    [Serializable]
    [DataContract]
    class Pallete
    {
        private long id;
        [DataMember]
        public long Id
        {
            get { return this.id; }
            set
            {
                if (value >= 0)
                    this.id = value;
            }
        }
        private int width;
        [DataMember]
        public int Width
        {
            get { return this.width; }
            set
            {
                if (value > 0)
                    this.width = value;
            }
        }
        private int depth;
        [DataMember]
        public int Depth
        {
            get { return this.depth; }
            set
            {
                if (value > 0)
                    this.depth = value;
            }
        }
        private int height;
        [DataMember]
        public int Height
        {
            get { return this.height; }
            set
            {
                if (value > 0)
                    this.height = value;
            }
        }
        public Pallete() { boxes = new List<Box>(); }
        [DataMember]
        public List<Box> boxes { get; private set; }

        public Box getBoxById(long id) { return boxes.Find(b => b.Id == id); }
        public void addBox(Box box) { boxes.Add(box); }
        public void addBoxes(List<Box> boxes) { boxes.AddRange(boxes); }
        public void deleteBox(Box box) { boxes.Remove(box); }

        public int getVolume() {
            int volume = width * height * depth + boxes.Sum(b => b.getVolume());
            return volume;
        }

        public DateTime? getExpirationDate() {
            return boxes.Min(b => b.ExpirationDate);
        }

        public int getWeight() {
            return boxes.Sum(b => b.Weight) + 30;
        }
    }
}
