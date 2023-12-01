using System;

namespace ConsoleApp2
{
    class Program
    {


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
        }
    }
}
