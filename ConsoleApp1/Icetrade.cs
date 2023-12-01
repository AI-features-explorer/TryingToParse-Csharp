using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ConsoleApp1
{
    class Icetrade : IAuctionWebsite
    {
        //search params
        public string BaseLink { get; private set; }
        public string SubjectOfAuction { get; set; }
        public string AuctionNumber { get; set; }
        public DateTime CreatedFromDate { get; set; }
        public string Customer { get; set; }
        public string Industry { get; set; } 

        public Icetrade()
        {
            BaseLink = "https://icetrade.by/search/auctions?";
        }

        //Requests
        private string GenerateRequest()
        {
            return $"search_text={SubjectOfAuction}&zakup_type[1]=1&" +
                $"zakup_type[2]=1&auc_num={AuctionNumber}&okrb=&company_title={Customer}&" +
                $"establishment=0&industries={Industry}&period=&created_from={Formatter.DateToString(CreatedFromDate)}&" +
                $"created_to=&request_end_from=&request_end_to=&t[Trade]=1&t[eTrade]=1&" +
                $"t[socialOrder]=1&t[singleSource]=1&t[Auction]=1&t[Request]=1&" +
                $"t[contractingTrades]=1&t[negotiations]=1&t[Other]=1&r[2]=2&r[7]=7&" +
                $"sort=num:desc&sbm=1&onPage=20";

        }

        //Loading data
        public List<Auction> LoadAuctions()
        {
            string requestLink = BaseLink + GenerateRequest();

            HtmlAnalyzer analyzer = new HtmlAnalyzer(new HtmlWeb().Load(requestLink));
            HtmlNodeCollection nodes = analyzer.GetHtmlNodes(".//table[@class='auctions w100']/tr/td[@class]");

            List<Auction> auctions = new List<Auction>();
            int counter = 1; Auction auction = new Auction();
            for (int i = 0; i < nodes.Count; i++)
            {
                switch (counter)
                {
                    case 1:
                        if (nodes[i].ChildNodes.FindFirst("a") != null)
                        {
                            var node = nodes[i].ChildNodes.FindFirst("a");
                            auction.Subject = Formatter.FormatString(node.InnerText);
                            foreach (var attribute in node.Attributes)
                                auction.RequestLink = Formatter.FormatString(attribute.Value);
                        }
                        break;
                    case 2:
                        auction.Organisation = Formatter.FormatString(nodes[i].InnerText).Replace(";","\"");
                        break;
                    case 4:
                        auction.SerialNamber = Formatter.FormatString(nodes[i].InnerText);
                        break;
                    case 5:
                        auction.Price = Formatter.FormatString(nodes[i].InnerText);
                        break;
                    case 6:
                        auction.EndDate = Formatter.FormatString(nodes[i].InnerText);
                        break;
                }
                if (counter == 4)
                {
                    if (DBCotroller.IsSaved(nodes[i].InnerText))
                    {
                        i += 2; auction = DBCotroller.LoadSingleObject(nodes[i].InnerText);
                        counter = 7;
                    }
                    else counter++;
                }
                else counter++;
                if (counter > 6)
                {
                    counter = 1;
                    auctions.Add(auction);
                    auction = new Auction();
                }
            }
            return auctions;
        }
        public Auction LoadAuctionData(Auction auction, string auctionLink)
        {
            List<Document> documents = new List<Document>();
            HtmlAnalyzer analyzer = new HtmlAnalyzer(new HtmlWeb().Load(auctionLink));
            HtmlNodeCollection nodes;
            nodes = analyzer.GetHtmlNodes("//p/a[@class='modal' or @target='blank']");
            Document document = new Document();

            foreach (var item in nodes)
            {
                document.DocumentName = item.InnerText;
                foreach (var attribute in item.Attributes)
                    if (attribute.Name == "href")
                        document.DocLink = Formatter.RemoveUnifiers(attribute.Value.Replace("getFile", "download"));
                documents.Add(document);
                document = new Document();
            }

            List<Lot> lots = new List<Lot>();
            nodes = analyzer.GetHtmlNodes("//table[@id='lots_list']//td[@class]");
            Lot lot = new Lot();
            foreach (var item in nodes)
            {
                if (item.ChildNodes.FindFirst("span") != null)
                {
                    string[] str = Formatter.FormatString(item.InnerText).Split(',');
                    lot.Count = str[0];
                    lot.Prise = str[1];
                    lots.Add(lot);
                    lot = new Lot();
                }
                else if (item.Attributes[0].Value.Contains("wordBreak"))
                    lot.Product = Formatter.FormatString(item.InnerText).Replace(";","\"");
            }

            auction.Documents = documents; auction.Lots = lots;

            return auction;
        }
    }
}
