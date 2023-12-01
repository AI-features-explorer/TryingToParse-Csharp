using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace testRequest
{
    //TDOD: NEEDS TO BIIIG REFACTORING!
    public static class ClientSession
    {
        private static string GetValueOfHtmlAttribute(HtmlAttributeCollection attributes)
        {
            string result = null;

            foreach (var item in attributes)
            {
                if (item.Name == "value")
                {
                    result = item.Value;
                    break;
                }
            }
            
            return result;
        }

        public async static Task<string> POST()
        {
            const string URL = "http://zakupki.butb.by/auctions/reestrauctions.html";

            CookieContainer sessionCookies = new CookieContainer();
            string IceWindowValue;
            string IceViewValue;
            string ViewStateValue;
            string SubjectOfProcurement;
            string ProductmrntID;

            using (var client = new HttpClient(new HttpClientHandler
            {
                CookieContainer = sessionCookies,
                UseCookies = true,
            }))
            {
                #region headers
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("ru-RU"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                client.DefaultRequestHeaders.Connection.Add("keep-alive");
                client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));
                client.DefaultRequestHeaders.Host = "zakupki.butb.by";
                #endregion

                await client.GetAsync(URL); //start session
                var html = await client.PostAsync(URL, new StringContent("fra=fra", Encoding.UTF8)).Result.Content.ReadAsStringAsync();

                HtmlWeb web = new HtmlWeb();
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);

                #region params
                var node = htmlDoc.DocumentNode.SelectSingleNode(".//*[@name = 'ice.window']");
                IceWindowValue = GetValueOfHtmlAttribute(node.Attributes);
                node = htmlDoc.DocumentNode.SelectSingleNode(".//*[@name = 'ice.view']");
                IceViewValue = GetValueOfHtmlAttribute(node.Attributes);
                node = htmlDoc.DocumentNode.SelectSingleNode(".//*[@name = 'javax.faces.ViewState']");
                ViewStateValue = GetValueOfHtmlAttribute(node.Attributes);
                SubjectOfProcurement = "бумага";
                ProductmrntID = "fra:auctionList:0:j_idt217";
                #endregion

                Console.WriteLine($"{IceViewValue}\n{IceWindowValue}\n{ViewStateValue}\n");


                #region form request open
                var content = new FormUrlEncodedContent(new[]
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
                #endregion

                await client.PostAsync(URL, content);

                //TDOD: find a cout of results
                #region searching
                content = new FormUrlEncodedContent(new[]
                {
                    #region content
                    new KeyValuePair<string, string>("fra", "fra"),
                    new KeyValuePair<string, string>("ice.window", IceWindowValue),
                    new KeyValuePair<string, string>("ice.view", IceViewValue),
                    #region search params 1
                    new KeyValuePair<string, string>("fra:j_idt111", SubjectOfProcurement),
                    new KeyValuePair<string, string>("fra:j_idt123",""),
                    new KeyValuePair<string, string>("fra:j_idt125",""),
                    new KeyValuePair<string, string>("fra:j_idt127",""),
                    new KeyValuePair<string, string>("fra:j_idt129",""),
                    new KeyValuePair<string, string>("fra:j_idt131",""),
                    new KeyValuePair<string, string>("fra:j_idt133",""),
                    new KeyValuePair<string, string>("fra:j_idt135",""),
                    new KeyValuePair<string, string>("fra:j_idt138",$"{2}"),
                    new KeyValuePair<string, string>("fra:j_idt142",""),
                    new KeyValuePair<string, string>("fra:j_idt144",""),
                    new KeyValuePair<string, string>("fra:date1",""),
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
                #endregion

                await client.PostAsync(URL, content);

                #region temp
                //TDOD: cycle of parse results
                //#region find
                //content = new FormUrlEncodedContent(new[]
                //{
                //    #region content
                //    new KeyValuePair<string, string>("fra", "fra"),
                //    new KeyValuePair<string, string>("ice.window", IceWindowValue),
                //    new KeyValuePair<string, string>("ice.view", IceViewValue),
                //    #region search params 1
                //    new KeyValuePair<string, string>("fra:j_idt111", SubjectOfProcurement),
                //    new KeyValuePair<string, string>("fra:j_idt123",""),
                //    new KeyValuePair<string, string>("fra:j_idt125",""),
                //    new KeyValuePair<string, string>("fra:j_idt127",""),
                //    new KeyValuePair<string, string>("fra:j_idt129",""),
                //    new KeyValuePair<string, string>("fra:j_idt131",""),
                //    new KeyValuePair<string, string>("fra:j_idt133",""),
                //    new KeyValuePair<string, string>("fra:j_idt135",""),
                //    new KeyValuePair<string, string>("fra:j_idt138",$"{2}"),
                //    new KeyValuePair<string, string>("fra:j_idt142",""),
                //    new KeyValuePair<string, string>("fra:j_idt144",""),
                //    new KeyValuePair<string, string>("fra:date1",""),
                //    new KeyValuePair<string, string>("fra:date2",""),
                //    new KeyValuePair<string, string>("fra:date3",""),
                //    new KeyValuePair<string, string>("fra:date4",""),
                //    new KeyValuePair<string, string>("fra:date5",""),
                //    new KeyValuePair<string, string>("fra:date6",""),
                //    new KeyValuePair<string, string>("fra:date7",""),
                //    new KeyValuePair<string, string>("fra:date8",""),
                //    #endregion

                //    new KeyValuePair<string, string>("fra:j_idcl", ProductmrntID),
                //    new KeyValuePair<string, string>(ProductmrntID, ProductmrntID),

                //    new KeyValuePair<string, string>("fra:_t199:0:_t202","on"),
                //    new KeyValuePair<string, string>("fra:_t199:1:_t202","on"),
                //    new KeyValuePair<string, string>("fra:_t199:2:_t202","on"),
                //    new KeyValuePair<string, string>("fra:_t199:3:_t202","on"),
                //    new KeyValuePair<string, string>("fra:_t199:4:_t202","on"),
                //    new KeyValuePair<string, string>("fra:_t199:6:_t202","on"),
                //    new KeyValuePair<string, string>("fra:_t199:7:_t202","on"),
                //    new KeyValuePair<string, string>("fra:_t199:8:_t202","on"),
                //    new KeyValuePair<string, string>("fra:_t199:9:_t202","on"),
                //    new KeyValuePair<string, string>("fra:_t199:10:_t202","on"),
                //    new KeyValuePair<string, string>("fra:_t199:11:_t202","on"),



                //    new KeyValuePair<string, string>("ice.focus", ProductmrntID),
                //    //new KeyValuePair<string, string> ("fra:_t182", "Искать"),

                //    new KeyValuePair<string, string>("javax.faces.ViewState", ViewStateValue),
                //    new KeyValuePair<string, string>("javax.faces.source", ProductmrntID),
                //    new KeyValuePair<string, string>("ice.window", IceWindowValue),
                //    new KeyValuePair<string, string>("ice.view", IceViewValue), 
                //    #endregion
                //});
                //#endregion

                //var result = await client.PostAsync(URL, content);
                //return result.Content.ReadAsStringAsync().Result;
                #endregion
            }

            using (var client = new HttpClient(new HttpClientHandler
            {
                CookieContainer = sessionCookies,
                UseCookies = true,
            }))
            {
                #region find
                var content = new FormUrlEncodedContent(new[]
                {
                    #region content
                    new KeyValuePair<string, string>("fra", "fra"),
                    new KeyValuePair<string, string>("ice.window", IceWindowValue),
                    new KeyValuePair<string, string>("ice.view", IceViewValue),
                    #region search params 1
                    new KeyValuePair<string, string>("fra:j_idt111", SubjectOfProcurement),
                    new KeyValuePair<string, string>("fra:j_idt123",""),
                    new KeyValuePair<string, string>("fra:j_idt125",""),
                    new KeyValuePair<string, string>("fra:j_idt127",""),
                    new KeyValuePair<string, string>("fra:j_idt129",""),
                    new KeyValuePair<string, string>("fra:j_idt131",""),
                    new KeyValuePair<string, string>("fra:j_idt133",""),
                    new KeyValuePair<string, string>("fra:j_idt135",""),
                    new KeyValuePair<string, string>("fra:j_idt138",$"{0}"),
                    new KeyValuePair<string, string>("fra:j_idt142",""),
                    new KeyValuePair<string, string>("fra:j_idt144",""),
                    new KeyValuePair<string, string>("fra:date1",""),
                    new KeyValuePair<string, string>("fra:date2",""),
                    new KeyValuePair<string, string>("fra:date3",""),
                    new KeyValuePair<string, string>("fra:date4",""),
                    new KeyValuePair<string, string>("fra:date5",""),
                    new KeyValuePair<string, string>("fra:date6",""),
                    new KeyValuePair<string, string>("fra:date7",""),
                    new KeyValuePair<string, string>("fra:date8",""),
                    #endregion
                    
                    new KeyValuePair<string, string>("fra:j_idcl", ProductmrntID),
                    new KeyValuePair<string, string>(ProductmrntID, ProductmrntID),

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



                    new KeyValuePair<string, string>("ice.focus", ProductmrntID),

                    new KeyValuePair<string, string>("javax.faces.ViewState", ViewStateValue),
                    new KeyValuePair<string, string>("javax.faces.source", ProductmrntID),
                    new KeyValuePair<string, string>("ice.window", IceWindowValue),
                    new KeyValuePair<string, string>("ice.view", IceViewValue), 
                    #endregion
                });
                #endregion

                var result = await client.PostAsync(URL, content);
                return result.Content.ReadAsStringAsync().Result;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ClientSession.POST().Result);
        }
    }
}
