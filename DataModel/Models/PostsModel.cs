using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteckOverflow.DataModel.Models
{
    public class PostsModel
    {
       
        public int Id { get; set; }
        public string Title { get; set; }
        public string QuestionText { get; set; }
        public int UserId { get; set; }
       
        public virtual UserEntity User { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime? ClosedDate { get; set; }
       
        public PostsModel()
        {
            PostedDate = DateTime.Now;
        }
    }
}
