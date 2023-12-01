using LiteDB;
using System;
using System.Collections.Generic;

namespace testDB_NoSQL
{
    internal static class DBCotroller
    {
        public static void SaveData(List<Auction> auctions, LiteDatabase LocalDatabase)
        {
            using (LocalDatabase)
            {
                ILiteCollection<Auction> collection = LocalDatabase.GetCollection<Auction>("Auction");
                foreach (var auction in auctions)
                    collection.Insert(auction);
            }
        }
        public static List<Auction> LoadData(LiteDatabase Database)
        {
            using (LiteDatabase LocalDatabase = Database)
            {
                List<Auction> auctions = new List<Auction>();
                ILiteCollection<Auction> collection = LocalDatabase.GetCollection<Auction>("Auction");
                var LadedData = collection.FindAll();
                foreach (Auction auction in LadedData)
                    auctions.Add(auction);
                return auctions;
            }
        }
        public static bool IsSaved(string SerialNamber, LiteDatabase LocalDatabase)
        {
            using (LocalDatabase)
            {
                ILiteCollection<Auction> collection = LocalDatabase.GetCollection<Auction>("Auction");
                var LadedData = collection.FindAll();
                foreach (Auction savedAuction in LadedData)
                    if (SerialNamber == savedAuction.SerialNamber)
                        return true;
                return false;
            }
        }
        public static void DeleteByName(LiteDatabase LocalDatabase, string name)
        {
            using (LocalDatabase)
            {
                ILiteCollection<Auction> collection = LocalDatabase.GetCollection<Auction>("Auction");
                collection.DeleteMany(x => x.SerialNamber.Equals(name));
            }
        }
    }

    public class Auction
    {
        public Guid IdAuction { get; set; }
        public string RequestLink { get; set; }
        public string Organisation { get; set; }
        public string Subject { get; set; }
        public string SerialNamber { get; set; }
        public string Price { get; set; }
        public string EndDate { get; set; }
        public bool IsSaved { get; set; }
        public List<Document> Documents { get; set; }
        public List<Lot> Lots { get; set; }
    }
    public class Document
    {
        public string DocLink { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
    }
    public class Lot
    {
        public string Product { get; set; }
        public string Prise { get; set; }
        public string Count { get; set; }
    }



    class Program
    {
        public static LiteDatabase liteDatabase = new LiteDatabase("Local.db");
        static void Main(string[] args)
        {
            List<Auction> auctions = new List<Auction>();
            for (int i = 0; i < 5; i++)
            {
                Auction auction = new Auction();
                auction.SerialNamber = "00" + i;
                auction.Price = 1 + "00";
                auction.Organisation = "Org" + i;
                auction.Subject = "Subj" + i;
                auction.RequestLink = "ReqLink" + i;
                auction.Documents = new List<Document>()
                {
                    new Document() { DocLink = "link" + i, DocumentName = "doc" + i, DocumentPath = "docPath" + i},
                    new Document() { DocLink = "link" + i+1, DocumentName = "doc" + i+1, DocumentPath = "docPath" + i+1},
                    new Document() { DocLink = "link" + i+2, DocumentName = "doc" + i+2, DocumentPath = "docPath" + i+2},
                };
                auction.Lots = new List<Lot>()
                {
                    new Lot() {Count = i.ToString(), Prise = (i+100).ToString(), Product = "prod" + i  },
                    new Lot() {Count = (i+1).ToString(), Prise = (i+101).ToString(), Product = "prod" + i+1  },
                    new Lot() {Count = (i+2).ToString(), Prise = (i+102).ToString(), Product = "prod" + i+2  }
                };
                auctions.Add(auction);
            }

            DBCotroller.SaveData(auctions, liteDatabase);

            var auc = DBCotroller.LoadData(liteDatabase);

            foreach (Auction item in auc)
            {
                Console.WriteLine($"SerialNamber = {item.SerialNamber}");
                Console.WriteLine($"Price = {item.Price}");
                Console.WriteLine($"Organisation = {item.Organisation}");
                Console.WriteLine($"RequestLink = {item.RequestLink}");
                Console.WriteLine($"Subject = {item.Subject}");
                Console.WriteLine();
                foreach (Document doc in item.Documents)
                {
                    Console.WriteLine();
                    Console.WriteLine($"\tDocLink = {doc.DocLink}");
                    Console.WriteLine($"\tDocumentName = {doc.DocumentName}");
                    Console.WriteLine($"\tDocumentName = {doc.DocumentName}");
                    Console.WriteLine("\t_____________________________________");
                }
                Console.WriteLine();
                foreach (Lot lot in item.Lots)
                {
                    Console.WriteLine();
                    Console.WriteLine($"\tProduct = {lot.Product}");
                    Console.WriteLine($"\tCount = {lot.Count}");
                    Console.WriteLine($"\tPrise = {lot.Prise}");
                    Console.WriteLine("\t_____________________________________");
                }
                Console.WriteLine("_____________________________________________________________");
                Console.WriteLine();
            }
        }
    }
}
