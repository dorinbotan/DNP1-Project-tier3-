using DBAccess;
using System;

namespace Tier3Lib_2_
{
    public class Tier3 : ITier3
    {
        string cn = "Data Source=(local);Initial Catalog=StorageDB;Integrated Security=True;Pooling=False";
        StorageDB db;

        public Tier3()
        {
            db = new StorageDB();
        }

        private int GetPartId(string partId)
        {
            return int.Parse(ProcessPartId(partId)[4]);
        }

        private string GetAniamlType(string partId)
        {
            return ProcessPartId(partId)[0];
        }

        private string GetAnimalId(string partId)
        {
            string[] tmp = ProcessPartId(partId);
            return tmp[0] + "-" + tmp[1] + "-" + tmp[2] + "-" + tmp[3];
        }

        private string[] ProcessPartId(string partId)
        {
            char[] delimiterChars = { '-' };
            return partId.Split(delimiterChars);
        }

        public string GetAnimalInfo(string animalId)
        {
            db.OpenConnection(cn);
            string toReturn = db.GetAnimalInfo(animalId);
            db.CloseConnection();
            return toReturn;
        }

        public string GetTrayInfo(string trayId)
        {
            db.OpenConnection(cn);
            string toReturn = db.GetTrayInfo(trayId);
            db.CloseConnection();
            return toReturn;
        }

        public string GetPackInfo(int orderNumber)
        {
            db.OpenConnection(cn);
            string toReturn = db.GetPackInfo(orderNumber);
            db.CloseConnection();
            return toReturn;
        }

        public string GetPartInfo(string animalId, int partId)
        {
            db.OpenConnection(cn);
            string toReturn = db.GetPartInfo(animalId, partId);
            db.CloseConnection();

            return toReturn;
        }

        public bool DeleteAnimal(string animalId)
        {
            db.OpenConnection(cn);
            bool toReturn = db.DeleteAnimal(animalId);
            db.CloseConnection();
            return toReturn;
        }

        public bool DeletePart(string animalId, int partId)
        {
            db.OpenConnection(cn);
            bool toReturn = db.DeletePart(animalId, partId);
            db.CloseConnection();
            return toReturn;
        }

        public bool CreatePackage(int totalWeight, string destination, Part[] list)
        {
            bool toReturn = true;
            string[] trayIdBackup = new string[list.Length];

            db.OpenConnection(cn);

            int packageNumber = db.GetPackageNumber();
            db.CloseConnection();
            db.OpenConnection(cn);

            db.InsertPackage(packageNumber, totalWeight, destination);

            try
            {
                for (int i = 0; i < list.Length; i++)
                {
                    string animalId = GetAnimalId(list[i].GetId());
                    int partId = GetPartId(list[i].GetId());
                    string trayId = db.GetPartTray(animalId, partId);
                    trayIdBackup[i] = trayId;
                    int weight = db.GetPartWeight(animalId, partId);

                    db.DeletePart(animalId, partId);
                    db.InsertSPart(animalId, trayId, partId, weight, packageNumber);
                }
            }
            catch (Exception)
            {
                // Add back all the parts removed before    
                for (int i = 0; i < list.Length; i++)
                {
                    if (trayIdBackup[i] != null)
                    {
                        string animalID = GetAnimalId(list[i].GetId());
                        db.InsertPart(animalID, trayIdBackup[i], GetPartId(list[i].GetId()),
                                    (int)(list[i].GetWeight() * 1000));
                    }
                }

                toReturn = false;
                db.DeletePackage(packageNumber);
            }

            db.CloseConnection();

            return toReturn;
        }

        public void InsertPack(int orderID, int weight, string destination)
        {
            db.OpenConnection(cn);

            db.InsertPackage(orderID, weight, destination);

            db.CloseConnection();
        }
    }
}