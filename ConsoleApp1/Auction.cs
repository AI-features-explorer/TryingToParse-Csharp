using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Auction
    {
        public Guid IdAuction { get; set; }
        public string RequestLink { get; set; }
        public List<Document> Documents { get; set; }
        public List<Lot> Lots { get; set; }
        public string Organisation { get; set; }
        public string Subject { get; set; }
        public string SerialNamber { get; set; }
        public string Price { get; set; }
        public string EndDate { get; set; }
        public bool IsSaved { get; set; }
    }
}
