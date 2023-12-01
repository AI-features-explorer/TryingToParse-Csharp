using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    interface IAuctionWebsite
    {
        string BaseLink { get; }
        string SubjectOfAuction { get; set; }
        string AuctionNumber { get; set; }
        DateTime CreatedFromDate { get; set; }
        string Customer { get; set; }
        List<Auction> LoadAuctions();
        Auction LoadAuctionData(Auction auction, string auctionLink);
    }
}
