using HtmlAgilityPack;

namespace ConsoleApp1
{
    class HtmlAnalyzer
    {
        private static HtmlDocument _currentDocument;

        public HtmlDocument CurrentDocument { get => _currentDocument; set => _currentDocument = value; }

        public HtmlAnalyzer(HtmlDocument html)
        {
            _currentDocument = html;
        }
        public HtmlAnalyzer(string html)
        {
            _currentDocument.LoadHtml(html);
        }
        public HtmlAnalyzer()
        {
            _currentDocument = new HtmlDocument();
        }

        public HtmlNodeCollection GetHtmlNodes(string Xpath)
        {
            return _currentDocument.DocumentNode.SelectNodes(Xpath);
        }
        public HtmlNode GetHtmlSingleNode(string Xpath)
        {
            return _currentDocument.DocumentNode.SelectSingleNode(Xpath);
        }
        public string GetValueTagValue(string html, string tag, string value)
        {
            _currentDocument.LoadHtml(html);
            HtmlNode node = _currentDocument.DocumentNode.SelectSingleNode($".//*[@{tag}='{value}']");
            return GetAttributeValue(node.Attributes, "value");
        }
        private static string GetAttributeValue(HtmlAttributeCollection attributes, string attributeName)
        {
            string result = null;
            foreach (var item in attributes)
                if (item.Name == attributeName)
                {
                    result = item.Value;
                    break;
                }
            return result;
        }
    }
}
