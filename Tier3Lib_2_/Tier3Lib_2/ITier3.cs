using System;
using System.ServiceModel;

namespace Tier3Lib_2_
{
    [ServiceContract(Namespace = "http://Tier3_2.com")]
    interface ITier3
    {
        [OperationContract]
        string GetAnimalInfo(string animalId);

        [OperationContract]
        string GetTrayInfo(string trayId);

        [OperationContract]
        string GetPackInfo(int orderNumber);

        [OperationContract]
        string GetPartInfo(string animalId, int partId);

        [OperationContract]
        bool DeleteAnimal(string animalId);

        [OperationContract]
        bool DeletePart(string animalId, int partId);

        [OperationContract]
        bool CreatePackage(int totalWeight, string destination, Part[] list);

        [OperationContract]
        void InsertPack(int orderID, int weight, string destination);
    }
}
