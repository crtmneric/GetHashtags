
using DevExpress.XtraReports.UI;
using GetHashtags.Classes;
using GetHashtags.Report;
using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetHashtags
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            
        }
          List<String> twits = new List<String>();
          List<String> user = new List<String>();

          int listCounter = 0;
       

        void getTwitsss()
        {

            string consumerKey = "rLagBOovIItduBvpqBkwET4a2";
            string consumerSecret = "ZAD8yrhTD7weuGUwT2UuoqHo4XwQvyMwnm0CThpLY1AKIkkmIF";
            string accessToken = "571107840-2egxs2sZ2DWA5XZu7J1kL6MboQXHOM9NXK2C3Puq";
            string accessTokenSecret = "9iOKuc8s5maaTMCtG87sdOBzFOFDg2x7T0WDzm4mGAe79";
            var auth = new SingleUserAuthorizer
            {
                CredentialStore = new InMemoryCredentialStore
                {
                    ConsumerKey = consumerKey,
                    ConsumerSecret = consumerSecret,
                    OAuthToken = accessToken,
                    OAuthTokenSecret = accessTokenSecret
                }
            };


            var twitterCtx = new TwitterContext(auth);
            DateTime date = DateTime.Parse(txtDat.Text);
            string searchTerm = txtHashTag.Text;

            // oldest id you already have for this search term
            ulong sinceID = 1;

            // used after the first query to track current session
            //ulong maxID;


            for (int i =0; i <  Convert.ToInt32(txtCount.Text)/100; i++)
            {
                List<Status> searchResponse =
                    (from search in twitterCtx.Search
                     where search.Type == SearchType.Search &&
                           search.Query == searchTerm &&
                           search.Count == 100 &&
                           search.SinceID == sinceID&&
                           search.Until==date
                     select search.Statuses)
                    .SingleOrDefault();


                searchResponse.ForEach(tweet =>twits.Add(tweet.Text));
                searchResponse.ForEach(tweet => user.Add(tweet.User.ScreenNameResponse));
                progressBarControl1.Position+=100/(Convert.ToInt32(txtCount.Text)/100);
            }
            btnReport.Enabled = true;
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }
        private List<Data> CreateData(List<String> todo)
        {
            List<Data> data = new List<Data>();

            foreach (String itemcık in todo)
            {
                Data item1 = new Data();
                item1.Twit = itemcık;
                item1.User = user[listCounter];
                listCounter++;
                data.Add(item1);
            }
            
           

            return data;
        }

    

        private void btnScan_Click(object sender, EventArgs e)
        {
            twits.Clear();
            user.Clear();
            progressBarControl1.Position = 0;
            listCounter = 0;
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            getTwitsss();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            TwitReport order = new TwitReport();
            order.DataSource = CreateData(twits);
            ReportPrintTool tool = new ReportPrintTool(order);
            tool.ShowRibbonPreviewDialog();
            btnReport.Enabled = false;
          
        }
    }
}
