using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class Message
    {

        [Key]
        public string clientuniqueid { get; set; }
        public string type { get; set; }
        public string message { get; set; }
        public string date { get; set; }
    }


    public class Message2
    {
        [Key]
        public string Id { get; set; }
        public string clientuniqueid { get; set; }
        public string type { get; set; }
        public string message { get; set; }
        public string date { get; set; }
    }
}
