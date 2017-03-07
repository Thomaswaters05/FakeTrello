using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FakeTrello.Models
{
    public class Board
    {
        [Key]
        public int BoardId { get; set; }

        public string Name { get; set; }

        public string URL { get; set; }

        public List<List> Lists { get; set; } // List # 1 a keyword //<> List # 2 is a File Ref // Lists = new var name

        //Auxiliary (i.e. not required to drive/define relationship)
        public TrelloUser Owner { get; set; } //owner and boards are NAVIGATION PROPERTIES
    }
}