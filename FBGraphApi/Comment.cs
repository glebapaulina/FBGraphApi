using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBGraphApi
{
    class Comment
    {
        public string created_time;
        public string id;
        public string message;
        public Comment() { }
        public Comment(string created_time, string id, string message)
        {
            this.created_time = created_time;
            this.id = id;
            this.message = message;
        }
    }
}
