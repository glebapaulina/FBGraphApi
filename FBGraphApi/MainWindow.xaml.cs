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


            //retrieving comments for the post
            var response = (JsonObject)client.Get("1396920153731998/comments");
            List<Comment> comments = new List<Comment>();
            foreach (var comm in (JsonArray)response["data"])
                comments.Add(new Comment
                {
                    created_time = (string)(((JsonObject)comm)["created_time"]),
                    id = (string)(((JsonObject)comm)["id"]),
                    message = (string)(((JsonObject)comm)["message"])
                });

            //extracting meesages from comments
            List<string> messages = new List<string>();
            foreach (var item in comments)
            {
                string json = JsonConvert.SerializeObject(item);
                Comment deserializedComment = JsonConvert.DeserializeObject<Comment>(json);
                messages.Add(deserializedComment.message);
            }
            
            

            List<string> match = new List<string>();
            foreach (string mess in messages)
            {
                if (mess.Contains("M"))
                {
                    match.Add(mess);
                }
            }
        }
    }
}
