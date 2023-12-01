using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    class Zakupki_butb : IAuctionWebsite
    {
        //session params
        private static string IceWindowValue;
        private static string IceViewValue;
        private static string ViewStateValue;
        private readonly HttpClient _httpClient;

        //search params
        public string BaseLink { get; private set; }
        public string SubjectOfAuction { get; set; }
        public string AuctionNumber { get; set; }
        public DateTime CreatedFromDate { get; set; }
        public string Customer { get; set; }

        public Zakupki_butb(HttpClient HttpClient)
        {
            BaseLink = "http://zakupki.butb.by/auctions/reestrauctions.html";
            _httpClient = HttpClient;
        }

        //Session
        public void StartSession()
        {
            LoadSessionParams(LoadFirstPage().Result);

        }
        private void LoadSessionParams(string html)
        {
            HtmlAnalyzer htmlAnalyzer = new HtmlAnalyzer();
            IceWindowValue = htmlAnalyzer.GetValueTagValue(html, "name", "ice.window");
            IceViewValue = htmlAnalyzer.GetValueTagValue(html, "name", "ice.view");
            ViewStateValue = htmlAnalyzer.GetValueTagValue(html, "name", "javax.faces.ViewState");
        }
        private async Task<string> LoadFirstPage()
        {
            return await _httpClient.PostAsync(_httpClient.BaseAddress,
                new StringContent("fra=fra", Encoding.UTF8)).Result.Content.ReadAsStringAsync();
        }

        //Requests Content
        private FormUrlEncodedContent GetOpenFormContent()
        {
            return new FormUrlEncodedContent(new[]
                {
                    #region content
                    new KeyValuePair<string, string>("fra", "fra"),
                    new KeyValuePair<string, string>("ice.window", IceWindowValue),
                    new KeyValuePair<string, string>("ice.view", IceViewValue),
                    new KeyValuePair<string, string>("fra:j_idt197_button",""),
                    new KeyValuePair<string, string>("fra:_t199:0:_t202","on"),
                    new KeyValuePair<string, string>("fra:_t199:1:_t202","on"),
                    new KeyValuePair<string, string>("fra:_t199:2:_t202","on"),
                    new KeyValuePair<string, string>("fra:_t199:3:_t202","on"),
                    new KeyValuePair<string, string>("fra:_t199:4:_t202","on"),
                    new KeyValuePair<string, string>("fra:_t199:6:_t202","on"),
                    new KeyValuePair<string, string>("fra:_t199:7:_t202","on"),
                    new KeyValuePair<string, string>("fra:_t199:8:_t202","on"),
                    new KeyValuePair<string, string>("fra:_t199:9:_t202","on"),
                    new KeyValuePair<string, string>("fra:_t199:10:_t202","on"),
                    new KeyValuePair<string, string>("fra:_t199:11:_t202","on"),
                    new KeyValuePair<string, string>("icefacesCssUpdates",""),
                    new KeyValuePair<string, string>("fra:j_idcl", "fra:j_idt104"),
                    new KeyValuePair<string, string>("javax.faces.ViewState", ViewStateValue),
                    new KeyValuePair<string, string>("ice.window", IceWindowValue),
                    new KeyValuePair<string, string>("ice.view", IceViewValue), 
                    #endregion
                });
        }
        private FormUrlEncodedContent GetSearchContent()
        {
            return new FormUrlEncodedContent(new[]
            {
                #region content
                    new KeyValuePair<string, string>("fra", "fra"),
                    new KeyValuePair<string, string>("ice.window", IceWindowValue),
                    new KeyValuePair<string, string>("ice.view", IceViewValue),
                    #region search params 1
                    new KeyValuePair<string, string>("fra:j_idt111", SubjectOfAuction),
                    new KeyValuePair<string, string>("fra:j_idt123",""),
                    new KeyValuePair<string, string>("fra:j_idt125",""),
                    new KeyValuePair<string, string>("fra:j_idt127",AuctionNumber),
                    new KeyValuePair<string, string>("fra:j_idt129",""),
                    new KeyValuePair<string, string>("fra:j_idt131",""),
                    new KeyValuePair<string, string>("fra:j_idt133",Customer),
                    new KeyValuePair<string, string>("fra:j_idt135",""),
                    new KeyValuePair<string, string>("fra:j_idt138",$"{0}"),
                    new KeyValuePair<string, string>("fra:j_idt142",""),
                    new KeyValuePair<string, string>("fra:j_idt144",""),
                    new KeyValuePair<string, string>("fra:date1", Formatter.DateToString(CreatedFromDate)),
                    new KeyValuePair<string, string>("fra:date2",""),
                    new KeyValuePair<string, string>("fra:date3",""),
                    new KeyValuePair<string, string>("fra:date4",""),
                    new KeyValuePair<string, string>("fra:date5",""),
                    new KeyValuePair<string, string>("fra:date6",""),
                    new KeyValuePair<string, string>("fra:date7",""),
                    new KeyValuePair<string, string>("fra:date8",""),
                    #endregion
                    new KeyValuePair<string, string>("fra:j_idcl", "fra:_t182"),
                    new KeyValuePair<string, string>("fra:_t199:0:_t202","on"),
                    new KeyValuePair<string, string>("fra:_t199:1:_t202","on"),
                    new KeyValuePair<string, string>("fra:_t199:2:_t202","on"),
                    new KeyValuePair<string, string>("fra:_t199:3:_t202","on"),
                    new KeyValuePair<string, string>("fra:_t199:4:_t202","on"),
                    new KeyValuePair<string, string>("fra:_t199:6:_t202","on"),
                    new KeyValuePair<string, string>("fra:_t199:7:_t202","on"),
                    new KeyValuePair<string, string>("fra:_t199:8:_t202","on"),
                    new KeyValuePair<string, string>("fra:_t199:9:_t202","on"),
                    new KeyValuePair<string, string>("fra:_t199:10:_t202","on"),
                    new KeyValuePair<string, string>("fra:_t199:11:_t202","on"),
                    new KeyValuePair<string, string>("ice.focus", "fra:_t182"),
                    new KeyValuePair<string, string> ("fra:_t182", "Искать"),
                    new KeyValuePair<string, string>("javax.faces.ViewState", ViewStateValue),
                    new KeyValuePair<string, string>("javax.faces.source", "fra:_t182"),
                    new KeyValuePair<string, string>("ice.window", IceWindowValue),
                    new KeyValuePair<string, string>("ice.view", IceViewValue), 
                    #endregion
            });
        }
        private FormUrlEncodedContent GetAuctionContent(string id)
        {
            return new FormUrlEncodedContent(new[]
            {
                #region content
                new KeyValuePair<string, string>("fra", "fra"),
                new KeyValuePair<string, string>("ice.window", IceWindowValue),
                new KeyValuePair<string, string>("ice.view", IceViewValue),
                #region search params 1
                new KeyValuePair<string, string>("fra:j_idt111", SubjectOfAuction),
                new KeyValuePair<string, string>("fra:j_idt123",""),
                new KeyValuePair<string, string>("fra:j_idt125",""),
                new KeyValuePair<string, string>("fra:j_idt127", AuctionNumber),
                new KeyValuePair<string, string>("fra:j_idt129",""),
                new KeyValuePair<string, string>("fra:j_idt131",""),
                new KeyValuePair<string, string>("fra:j_idt133", Customer),
                new KeyValuePair<string, string>("fra:j_idt135",""),
                new KeyValuePair<string, string>("fra:j_idt138",$"{0}"),
                new KeyValuePair<string, string>("fra:j_idt142",""),
                new KeyValuePair<string, string>("fra:j_idt144",""),
                new KeyValuePair<string, string>("fra:date1",Formatter.DateToString(CreatedFromDate)),
                new KeyValuePair<string, string>("fra:date2",""),
                new KeyValuePair<string, string>("fra:date3",""),
                new KeyValuePair<string, string>("fra:date4",""),
                new KeyValuePair<string, string>("fra:date5",""),
                new KeyValuePair<string, string>("fra:date6",""),
                new KeyValuePair<string, string>("fra:date7",""),
                new KeyValuePair<string, string>("fra:date8",""),
                #endregion
                    
                new KeyValuePair<string, string>("fra:j_idcl", $"fra:auctionList:{id}:j_idt217"),
                new KeyValuePair<string, string>($"fra:auctionList:{id}:j_idt217", $"fra:auctionList:{id}:j_idt217"),

                new KeyValuePair<string, string>("fra:_t199:0:_t202","on"),
                new KeyValuePair<string, string>("fra:_t199:1:_t202","on"),
                new KeyValuePair<string, string>("fra:_t199:2:_t202","on"),
                new KeyValuePair<string, string>("fra:_t199:3:_t202","on"),
                new KeyValuePair<string, string>("fra:_t199:4:_t202","on"),
                new KeyValuePair<string, string>("fra:_t199:6:_t202","on"),
                new KeyValuePair<string, string>("fra:_t199:7:_t202","on"),
                new KeyValuePair<string, string>("fra:_t199:8:_t202","on"),
                new KeyValuePair<string, string>("fra:_t199:9:_t202","on"),
                new KeyValuePair<string, string>("fra:_t199:10:_t202","on"),
                new KeyValuePair<string, string>("fra:_t199:11:_t202","on"),
                new KeyValuePair<string, string>("ice.focus", $"fra:auctionList:{id}:j_idt217"),
                new KeyValuePair<string, string>("javax.faces.ViewState", ViewStateValue),
                new KeyValuePair<string, string>("javax.faces.source", $"fra:auctionList:{id}:j_idt217"),
                new KeyValuePair<string, string>("ice.window", IceWindowValue),
                new KeyValuePair<string, string>("ice.view", IceViewValue) 
                #endregion
            });

        }


        //Requests
        public async Task<string> HtmlRquest(FormUrlEncodedContent content)
        {
            return await _httpClient.PostAsync(BaseLink, content).Result.Content.ReadAsStringAsync();
        }
        public async void HtmlRequestNoData(FormUrlEncodedContent content)
        {
            await _httpClient.PostAsync(BaseLink, content).Result.Content.ReadAsStringAsync();
        }


         public DateTime startTime;
        //Loading data
        public List<Auction> LoadAuctions()
        {
            startTime = DateTime.Now;
            StartSession();
            HtmlRequestNoData(GetOpenFormContent());
            HtmlAnalyzer analyzer = new HtmlAnalyzer(HtmlRquest(GetSearchContent()).Result);
            HtmlNodeCollection nodes = analyzer.GetHtmlNodes(".//tbody[@id='fra:auctionList:tbody']//td");
            List<Auction> auctions = new List<Auction>();
            int counter = 1; Auction auction = new Auction();
            int lineCounter = 0;
            HtmlNode tempNode = null;
            foreach (var node in nodes)
            {
                if (node.ChildNodes.FindFirst("span") != null)
                {
                    tempNode = node.ChildNodes.FindFirst("span");

                    if (tempNode.Attributes[0].Value == $"fra:auctionList:{lineCounter}:_t211")
                        auction.SerialNamber = Formatter.FormatString(tempNode.InnerText);
                    else if (tempNode.Attributes[0].Value == $"fra:auctionList:{lineCounter}:_t218")
                        auction.Subject = Formatter.FormatString(tempNode.InnerText);
                    else if (tempNode.Attributes[0].Value == $"fra:auctionList:{lineCounter}:_t236")
                        auction.Organisation = Formatter.FormatString(tempNode.InnerText);
                    else if (tempNode.Attributes[0].Value == $"fra:auctionList:{lineCounter}:_t230")
                        auction.Price = Formatter.FormatString(tempNode.InnerText);
                    else if (tempNode.Attributes[0].Value == $"fra:auctionList:{lineCounter}:_t267"){ 
                        auction.EndDate = Formatter.FormatString(tempNode.InnerText); counter = 13; }
                }
                if (tempNode.Attributes[0].Value == $"fra:auctionList:{lineCounter}:_t211")
                {
                    if (DBCotroller.IsSaved(tempNode.InnerText))
                    {
                        auction = DBCotroller.LoadSingleObject(tempNode.InnerText);
                        counter = 13;
                    }
                    else counter++;
                }
                else
                {
                    counter++;
                }
                if (counter > 12)
                {
                    counter = 1;
                    lineCounter++;
                    auctions.Add(auction);
                    auction = new Auction();
                }
            }
            Console.WriteLine((DateTime.Now - startTime).Seconds);
            return auctions;
        }
        public Auction LoadAuctionData(Auction auction, string auctionLink)
        {
            startTime = DateTime.Now;
            List<Document> documents = new List<Document>();
            StartSession();
            HtmlRequestNoData(GetOpenFormContent());
            HtmlRequestNoData(GetSearchContent());
            HtmlAnalyzer analyzer = new HtmlAnalyzer(HtmlRquest(GetAuctionContent(auctionLink)).Result);

            HtmlNodeCollection nodes = analyzer.GetHtmlNodes("//table[@id ='j_idt116:findDocumListByText']//a");
            Document document = new Document();
            foreach (var item in nodes)
            {
                document.DocumentName = Formatter.FormatString(item.InnerText);
                document.DocLink = "http://zakupki.butb.by" + Formatter.FormatString(item.Attributes[1].Value);
                documents.Add(document);
                document = new Document();
            }

            List<Lot> lots = new List<Lot>();
            nodes = analyzer.GetHtmlNodes("//tbody[@id ='j_idt116:lotsList:tbody']//span");
            Lot lot = new Lot(); int counter = 1;
            foreach (var item in nodes)
            {
                switch (counter)
                {
                    case 3:
                        lot.Product = Formatter.FormatString(item.InnerText);
                        break;
                    case 4:
                        lot.Count = Formatter.FormatString(item.InnerText);
                        break;
                    case 11:
                        lot.Prise = Formatter.FormatString(item.InnerText);
                        counter = 15;
                        break;
                }
                counter++;
                if (counter > 15)
                {
                    counter = 1;
                    lots.Add(lot);
                    lot = new Lot();
                }
            }
            auction.Lots = lots; auction.Documents = documents;
            Console.WriteLine((DateTime.Now - startTime).Seconds);
            return auction;
        }
    }
}
