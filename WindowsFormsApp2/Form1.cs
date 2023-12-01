using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Uri uri;
            if (textBox1.Text.Contains("http")) uri = new Uri(textBox1.Text);
            else uri = new Uri(@"https://" + textBox1.Text);
            webBrowser1.AllowNavigation = true;
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
            webBrowser1.Navigate(uri);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            textBox2.Text = webBrowser1.DocumentText;
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            textBox1.Text = webBrowser1.Url.ToString();
        }
    }
}
