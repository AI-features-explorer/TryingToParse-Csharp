using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Goszakupki : IAuctionWebsite
    {
        //search params
        public string BaseLink { get; private set; }
        public string SubjectOfAuction { get; set; }
        public string AuctionNumber { get; set; }
        public DateTime CreatedFromDate { get; set; }
        public string Customer { get; set; }
        public string Industry { get; set; }

        public Goszakupki()
        {
            BaseLink = "https://goszakupki.by/tenders/posted?";
        }

        //Requests
        private string GenerateRequest()
        {
            return $"TendersSearch[num]={AuctionNumber}" +
                $"&TendersSearch[text]={SubjectOfAuction}" +
                $"&TendersSearch[unp]=&TendersSearch[customer_text]={Customer}" +
                $"&TendersSearch[unpParticipant]=&TendersSearch[participant_text]=" +
                $"&TendersSearch[price_from]=&TendersSearch[price_to]=" +
                $"&TendersSearch[created_from]={Formatter.DateToString(CreatedFromDate)}" +
                $"&TendersSearch[created_to]=&TendersSearch[request_end_from]=" +
                $"&TendersSearch[request_end_to]=&TendersSearch[auction_date_from]=" +
                $"&TendersSearch[auction_date_to]=&TendersSearch[industry]={Industry}" +
                $"&TendersSearch[type]=&TendersSearch[status]=" +
                $"&TendersSearch[region][]=2&TendersSearch[region][]=7" +
                $"&TendersSearch[appeal]=";

        }

        //Loading data
        public List<Auction> LoadAuctions()
        {
            string requestLink = BaseLink + GenerateRequest();
            HtmlAnalyzer analyzer = new HtmlAnalyzer(new HtmlWeb().Load(requestLink));

            HtmlNodeCollection nodes = analyzer.GetHtmlNodes(".//tbody/tr[@data-key]/td");

            List<Auction> auctions = new List<Auction>();
            int counter = 1; Auction auction = new Auction();
            for (int i = 0; i < nodes.Count; i++)
            {
                switch (counter)
                {
                    case 1:
                        auction.SerialNamber = Formatter.FormatString(nodes[i].InnerText);
                        break;
                    case 2:
                        if (nodes[i].ChildNodes.FindFirst("a") != null)
                        {
                            var node = nodes[i].ChildNodes.FindFirst("a");
                            auction.Subject = node.InnerText;
                            auction.Organisation = Formatter.FormatString(nodes[i].InnerText.Replace(node.InnerText, string.Empty));

                            foreach (var attribute in node.Attributes)
                            {
                                auction.RequestLink = "https://goszakupki.by" + attribute.Value;
                            }
                        }
                        break;
                    case 5:
                        auction.EndDate = Formatter.FormatString(nodes[i].InnerText);
                        break;
                    case 6:
                        auction.Price = Formatter.FormatString(nodes[i].InnerText); ;
                        break;
                }

                if (counter == 1)
                {
                    if (DBCotroller.IsSaved(nodes[i].InnerText))
                    {
                        i += 5; auction = DBCotroller.LoadSingleObject(nodes[i].InnerText);
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
        public Auction LoadAuctionData( Auction auction, string auctionLink)
        {
            HtmlAnalyzer analyzer = new HtmlAnalyzer(new HtmlWeb().Load(auctionLink));

            List<Document> documents = new List<Document>();
            HtmlNodeCollection nodes = analyzer.GetHtmlNodes(".//a[@class='modal-link']");
            Document document = new Document();
            foreach (var item in nodes)
            {
                document.DocumentName = item.InnerText;
                foreach (var attribute in item.Attributes)
                    if (attribute.Name == "href")
                        document.DocLink = "https://goszakupki.by" + Formatter.RemoveUnifiers(attribute.Value) + "&download=1";
                documents.Add(document);
                document = new Document();
            }

            List<Lot> lots = new List<Lot>();
            nodes = analyzer.GetHtmlNodes(".//td[@class='lot-description' or @class='lot-count-price']");
            Lot lot = new Lot();
            for (int i = 0; i < nodes.Count; i++)
            {
                if (i % 2 != 0)
                {
                    string[] str = Formatter.FormatString(nodes[i].InnerText).Split(',');
                    lot.Count = str[0];
                    lot.Prise = str[1];
                    lots.Add(lot);
                    lot = new Lot();
                }
                else lot.Product = Formatter.FormatString(nodes[i].InnerText);
            }

            auction.Documents = documents; auction.Lots = lots;
            return auction;
        }

    }
}
