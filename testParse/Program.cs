using System;
using System.Net;

namespace testParse
{
    enum Info
    {
        PurchaseSubject,
        CountPrice,
        Date
    }

    class Program
    {
        static void Main(string[] args)
        {

            //const int id = 843317;

            //#region parse visible table
            //var html = @"https://icetrade.by/tenders/all/view/" + id;
            //HtmlWeb web = new HtmlWeb();
            //var htmlDoc = web.Load(html);

            //List<string> keys = new List<string>();
            //List<string> values = new List<string>();

            //var nodes = htmlDoc.DocumentNode.SelectNodes(".//td[@class='w25 lft']");
            //foreach (var node in nodes)
            //{
            //    keys.Add(node.InnerText);
            //}

            //nodes = htmlDoc.DocumentNode.SelectNodes(".//td[@class = 'lft']");
            //foreach (var node in nodes)
            //{
            //    keys.Add(node.InnerText);
            //}

            //nodes = htmlDoc.DocumentNode.SelectNodes(".//td[@class = 'afv']");
            //foreach (var node in nodes)
            //{
            //    values.Add(node.InnerText);
            //}

            //for (int i = 0; i < keys.Count; i++)
            //{
            //    keys[i] = keys[i].Trim();
            //    values[i] = values[i].Trim();
            //    keys[i] = keys[i].Replace("\r\n", "");
            //    values[i] = values[i].Replace("\r\n", "");
            //    keys[i] = keys[i].Replace("\t", "");
            //    values[i] = values[i].Replace("\t", "");
            //    values[i] = values[i].Replace("&nbsp", "");
            //    values[i] = values[i].Replace(";", " ");
            //    values[i] = values[i].Replace("&quot", "");
            //    Console.ForegroundColor = ConsoleColor.Blue;
            //    Console.WriteLine($"{keys[i]}");
            //    Console.ForegroundColor = ConsoleColor.Yellow;
            //    Console.WriteLine($"{values[i]}\n");
            //}
            //#endregion

            //#region parce headers 
            //html = @"https://icetrade.by/tenders/all/view/" + id;
            //htmlDoc = web.Load(html);

            //List<string[]> list = new List<string[]>();

            //int j = 0;
            ////get all purchase subject
            //nodes = htmlDoc.DocumentNode.SelectNodes(".//div[@id='w0']");
            //foreach (var item in nodes)
            //{
            //    string str = item.InnerText.Replace("\t", "");
            //    str = str.Replace("<br>", "");
            //    str = str.Replace("\r\n", "");
            //    str = str.Replace("&nbsp;", " ");
            //    str = str.Replace("&ndash;", "-");
            //    str = str.Trim();
            //    str = Regex.Replace(str, @"\s+", " ");

            //    list.Add(new string[3]);
            //    list[j][(int)Info.PurchaseSubject] = str;
            //    j++;
            //}

            //j = 0;
            ////get all (count / price) values
            //nodes = htmlDoc.DocumentNode.SelectNodes(".//table[@id = 'lots_list']/tr/td[@class and string-length(@class)=0]");
            //foreach (var item in nodes)
            //{
            //    string str = item.InnerText.Replace("\t", "");
            //    str = str.Replace("<br>", "");
            //    str = str.Replace("\r\n", "");
            //    str = str.Replace("&nbsp;", " ");
            //    str = str.Replace("&ndash;", "-");
            //    str = str.Trim();
            //    str = Regex.Replace(str, @"\s+", " ");
            //    list[j][(int)Info.CountPrice] = str;
            //    j++;
            //}

            //#endregion

            //#region post request

            ////get all (date) values
            //string result;
            //using (WebClient client = new WebClient())
            //{
            //    byte[] response =
            //    client.UploadValues("https://icetrade.by/lots/viewLots", "POST", new NameValueCollection()
            //    {
            //        { "auction_id", $"{id}" }
            //    }); ;

            //    result = Encoding.UTF8.GetString(response);
            //}
            //#endregion

            //#region parse data of request 

            //HtmlDocument htmlDocPart = new HtmlDocument();
            //htmlDocPart.LoadHtml(result);

            //var dateNode = htmlDocPart.DocumentNode.SelectNodes(".//td[@colspan=3]");

            //j = 0;
            //for (int i = 0; i < dateNode.Count; i += 5)
            //{
            //    string str = dateNode[i].InnerText.Replace("\t", "");
            //    str = str.Replace("\r\n", "");
            //    list[j][(int)Info.Date] = str;
            //    j++;
            //    if (j >= list.Count)
            //    {
            //        break;
            //    }
            //}

            //Console.ForegroundColor = ConsoleColor.Green;
            //foreach (var item in list)
            //{
            //    Console.WriteLine($"Предмет закупки:\n{item[(int)Info.PurchaseSubject]}\n" +
            //        $"Количество/Стоимость:\n{item[(int)Info.CountPrice]}\n" +
            //        $"Дата:\n{item[(int)Info.Date]}\n");
            //}
            //Console.ResetColor();
            //#endregion

            //#region docs info
            //html = @"https://icetrade.by/tenders/all/view/" + id;
            //web = new HtmlWeb();
            //htmlDoc = web.Load(html);

            //List<string> RequestLinks = new List<string>();
            //List<string> Links = new List<string>();
            //List<string> Names = new List<string>();


            ////TODO: null ref

            ////get mtf'g doc names and links
            //nodes = htmlDoc.DocumentNode.SelectNodes(".//a[@class='modal']/@href");
            //foreach (var node in nodes)
            //{
            //    //names
            //    Console.WriteLine(node.InnerText);
            //    Names.Add(node.InnerText);
            //    //links
            //    Console.WriteLine(node.Attributes[1].Value.Replace("getFile", "download"));
            //    RequestLinks.Add(node.Attributes[1].Value.Replace("getFile", "download"));
            //}
            //#endregion

            //#region download files

            //WebClient webClient = new WebClient();
            //for (int i = 0; i < RequestLinks.Count; i++)
            //{
            //    webClient.DownloadFile(new Uri(RequestLinks[i]), @"d:\" + Names[i]);
            //}
            //#endregion

            string data;
            using (WebClient web1 = new WebClient())
            {
                 data = web1.DownloadString("https://icetrade.by/tenders/all/view/854548");
            }

            Console.WriteLine(data);

        }

    }
}
