using System;

namespace Tier3Lib_2_
{
    [Serializable]
    public class Tray
    {
        private string id;
        private Part[] list;

        public Tray(string id, Part[] list)
        {
            this.id = id;
            this.list = list;
        }

        public string GetId()
        {
            return id;
        }

        public Part[] GetParts()
        {
            return list;
        }
    }
}