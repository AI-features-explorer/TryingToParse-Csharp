using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ConsoleApp1
{
    class Program
    {

        internal static Auction AssembleAuction(Auction auction, List<Document> documents, List<Lot> lots)
        {
            auction.Documents = documents;
            auction.Lots = lots;
            return auction;
        }
        internal static readonly HttpClient _httpClient = new HttpClient();
        public static bool CheckInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public static List<Auction> Auctions { get; set; }

        public static void SetSearchParams(IAuctionWebsite website)
        {
            website.SubjectOfAuction = "бумага";
            website.CreatedFromDate = new DateTime(2020, 06, 18);
        }

        static void Main(string[] args)
        {
            #region headers
            _httpClient.BaseAddress = new Uri("http://zakupki.butb.by/auctions/reestrauctions.html");
            _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("ru-RU"));
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            _httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));
            _httpClient.DefaultRequestHeaders.Host = "zakupki.butb.by";
            #endregion

            int count = 0;
            List<IAuctionWebsite> auctionWebsites = new List<IAuctionWebsite>()
            {
                //new Goszakupki(),
                //new Icetrade(),
                new Zakupki_butb(_httpClient)
            };

            List<List<Auction>> auctionsList = new List<List<Auction>>()
            {
                new List<Auction>(),
                new List<Auction>(),
                new List<Auction>()
            };

            for (int i = 0; i < auctionWebsites.Count; i++)
            {
                SetSearchParams(auctionWebsites[i]);
                auctionsList[i] = auctionWebsites[i].LoadAuctions(); count++;
                for (int j = 0; j < auctionsList[i].Count; j++)
                {
                    if (auctionWebsites[i] is Zakupki_butb)
                    auctionsList[i][j] = auctionWebsites[i]
                       .LoadAuctionData(auctionsList[i][j], j.ToString());
                    else
                    auctionsList[i][j] = auctionWebsites[i]
                        .LoadAuctionData(auctionsList[i][j], auctionsList[i][j].RequestLink);

                    count++;
                }

                foreach (var item in auctionsList[i])
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
                        Console.WriteLine($"\tDocumentPath = {doc.DocumentPath}");
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

                Console.WriteLine("\n");
                Console.WriteLine("COUNT=" + count);
                
                Console.WriteLine("SITE=" + auctionWebsites[i]);
                Console.WriteLine("\n");
            }
        }
    }
}
