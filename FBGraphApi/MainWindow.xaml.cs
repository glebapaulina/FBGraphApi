using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Facebook;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace FBGraphApi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string phrase;
        int count;
        List<Comment> comments = new List<Comment>();
        public MainWindow()
        {
            
            InitializeComponent();

            FacebookClient client = new FacebookClient();
            dynamic result = client.Get("oauth/access_token", new
            {
                client_id = "...",
                client_secret = "...",
                grant_type = "client_credentials"
            });
            client.AccessToken = result.access_token;
                
            var response = (JsonObject)client.Get("1396920153731998/comments");

            GetComments(response);
           
        }
        internal List<Comment> GetComments(JsonObject response)
        {
            foreach (var comm in (JsonArray)response["data"])
                comments.Add(new Comment
                {
                    created_time = (string)(((JsonObject)comm)["created_time"]),
                    id = (string)(((JsonObject)comm)["id"]),
                    message = (string)(((JsonObject)comm)["message"])
                });
            return comments;

        }
       
        private void btnSzukaj_Click(object sender, RoutedEventArgs e)
        {
            CountMatches(comments);
            lblWystapieniaLicz.Content = count;
        }

        private void txtFraza_TextChanged(object sender, TextChangedEventArgs e)
        {
                phrase = txtFraza.Text;
        }

        private int CountMatches(List<Comment> comments)
        {
                var matches = from comm in comments
                                  where comm.message.ToLower().Contains(phrase)
                                  select comm;
            count = matches.Count() ;
            return count;
        }
            
    }
}
