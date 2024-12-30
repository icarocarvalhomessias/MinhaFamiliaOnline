using System;
using System.Collections.Generic;

namespace FML.WebApp.MVC.Models
{
    public class Node
    {
        public Guid Id { get; set; }
        public List<Guid> Pids { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public Guid? Mid { get; set; }
        public Guid? Fid { get; set; }
        public string Img { get; set; }
    }

}
