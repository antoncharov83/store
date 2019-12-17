using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace storeroom
{
    [Serializable]
    [DataContract]
    class Box
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
        private int weight;
        [DataMember]
        public int Weight
        {
            get { return this.weight; }
            set
            {
                if (value > 0)
                    this.weight = value;
            }
        }
        private DateTime? expirationDate;
        [DataMember]
        public DateTime? ExpirationDate 
        { 
            get { return this.expirationDate; } 
            set {
                this.expirationDate = value;
                this.productionDate = null;
            }
        }
        private DateTime? productionDate;
        [DataMember]
        public DateTime? ProductionDate
        {
            get { return this.productionDate; }
            set
            {
                if (value != null)
                {
                    this.productionDate = value;
                    this.expirationDate = this.productionDate?.AddDays(100);
                }
            }
        }

        public int getVolume() { return width * height * depth; }
    }
}
