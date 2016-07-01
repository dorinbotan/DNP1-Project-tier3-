using DBAccess;
using System;

namespace Tier3Lib
{
    public class Tier3 : ITier3
    {
        string cn = "Data Source=(local);Initial Catalog=StorageDB;Integrated Security=True;Pooling=False";
        StorageDB db;

        public Tier3()
        {
            db = new StorageDB();
        }

        public bool SendTray(Tray tray)
        {
            bool toReturn = true;
            try
            {
                string animalID;
                Part[] parts = tray.GetParts();

                db.OpenConnection(cn);            

                for (int i = 0; i < parts.Length; i++)
                {   
                    animalID = GetAnimalId(parts[i].GetId());

                    if (!db.FindAnimal(animalID))                                           
                        db.InsertAnimal(animalID, GetAniamlType(parts[i].GetId()));         

                    db.InsertPart(animalID, tray.GetId(), GetPartId(parts[i].GetId()),
                        (int)(parts[i].GetWeight() * 1000));     
                }

                db.CloseConnection();
            }
            catch (Exception)
            {
                toReturn = false;
            }

            return toReturn;
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

        public bool SignIn(string login, string password)
        {                                
            db.OpenConnection(cn);
            bool toReturn = db.SignIn(login, password);
            db.CloseConnection();
            return toReturn;
        }

        public bool LogIn(string login, string password)
        {   
            db.OpenConnection(cn);
            string pass = db.GetPassword(login);  
            db.CloseConnection();
                                      
            return pass != null && pass.Trim().Equals(password.Trim());
        }

        public int GetLotNumber(string login)
        {
            db.OpenConnection(cn);
            int toReturn = db.GetLotNumber(login);
            db.IncrementLotNumber(login);
            db.CloseConnection();

            return toReturn;
        }
    }   
}