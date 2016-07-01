using System;

namespace Tier3Lib_2_
{
    [Serializable]
    public class Part
    {
        private string id;
        private double weight;

        // id - animalId-partId          
        public Part(string id, double weight)
        {
            this.id = id;
            this.weight = weight;
        }

        public string GetId()
        {
            return id;
        }

        public double GetWeight()
        {
            return weight;
        }
    }
}
