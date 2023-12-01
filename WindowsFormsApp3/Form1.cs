using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public class Auction
        {
            public Guid IdAuction { get; set; }
            public string RequestLink { get; set; }
            public string Organisation { get; set; }
            public string Subject { get; set; }
            public string SerialNamber { get; set; }
            public string Price { get; set; }
            public string EndDate { get; set; }
            public bool IsSaved { get; set; }
            public List<Document> Documents { get; set; }
            public List<Lot> Lots { get; set; }
        }
        public class Document
        {
            public string DocLink { get; set; }
            public string DocumentName { get; set; }
            public string DocumentPath { get; set; }
        }
        public class Lot
        {
            public string Product { get; set; }
            public string Prise { get; set; }
            public string Count { get; set; }
        }

        List<Auction> auctions = new List<Auction>();

        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 5; i++)
            {
                Auction auction = new Auction();
                auction.SerialNamber = "00" + i;
                auction.Price = 1 + "00";
                auction.Organisation = "Org" + i;
                auction.Subject = "Subj" + i;
                auction.RequestLink = "ReqLink" + i;
                auction.Documents = new List<Document>()
                {
                    new Document() { DocLink = "link" + i, DocumentName = "doc" + i, DocumentPath = "docPath" + i},
                    new Document() { DocLink = "link" + i+1, DocumentName = "doc" + i+1, DocumentPath = "docPath" + i+1},
                    new Document() { DocLink = "link" + i+2, DocumentName = "doc" + i+2, DocumentPath = "docPath" + i+2},
                };
                auction.Lots = new List<Lot>()
                {
                    new Lot() {Count = i.ToString(), Prise = (i+100).ToString(), Product = "prod" + i  },
                    new Lot() {Count = (i+1).ToString(), Prise = (i+101).ToString(), Product = "prod" + i+1  },
                    new Lot() {Count = (i+2).ToString(), Prise = (i+102).ToString(), Product = "prod" + i+2  }
                };
                auctions.Add(auction);
            }
            dataGridView1.DataSource = auctions;
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.DataSource = auctions[dataGridView1.CurrentRow.Index].Documents;
            dataGridView3.DataSource = auctions[dataGridView1.CurrentRow.Index].Lots;
        }
    }
}
