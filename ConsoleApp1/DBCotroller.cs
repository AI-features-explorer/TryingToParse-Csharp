using LiteDB;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal static class DBCotroller
    {
        public static void SaveData(List<Auction> auctions)
        {
            using (LiteDatabase LocalDatabase = new LiteDatabase("Local.db"))
            {
                ILiteCollection<Auction> collection = LocalDatabase.GetCollection<Auction>("Auction");
                foreach (var auction in auctions)
                    collection.Insert(auction);
            }
        }
        public static List<Auction> LoadData()
        {
            using (LiteDatabase LocalDatabase = new LiteDatabase("Local.db"))
            {
                List<Auction> auctions = new List<Auction>();
                ILiteCollection<Auction> collection = LocalDatabase.GetCollection<Auction>("Auction");
                var LadedData = collection.FindAll();
                foreach (Auction auction in LadedData)
                    auctions.Add(auction);
                return auctions;
            }
        }
        public static Auction LoadSingleObject(string SerialNamber)
        {
            using (LiteDatabase LocalDatabase = new LiteDatabase("Local.db"))
            {
                ILiteCollection<Auction> collection = LocalDatabase.GetCollection<Auction>("Auction");
                return collection.FindOne(SerialNamber);
            }
        }
        public static bool IsSaved(string SerialNamber)
        {
            using (LiteDatabase LocalDatabase = new LiteDatabase("Local.db"))
            {
                ILiteCollection<Auction> collection = LocalDatabase.GetCollection<Auction>("Auction");
                var LadedData = collection.FindAll();
                foreach (Auction savedAuction in LadedData)
                    if (SerialNamber == savedAuction.SerialNamber)
                        return true;
                return false;
            }
        }
        public static void DeleteBySerialNumber(string SerialNumber)
        {
            using (LiteDatabase LocalDatabase = new LiteDatabase("Local.db"))
            {
                ILiteCollection<Auction> collection = LocalDatabase.GetCollection<Auction>("Auction");
                collection.DeleteMany(x => x.SerialNamber.Equals(SerialNumber));
            }
        }
    }
}
