using SteckOverflow.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteckOverflow.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string QuestionText { get; set; }
        public int UserId { get; set; }
        public virtual UserEntity User { get; set; }

        public DateTime PostedDate { get; set; }
        public DateTime? ClosedDate { get; set; }

        public List<AnswerModel> Answers { get; set; }
        public List<PostModel> PostModels { get; set; }


        public PostModel()
        {
            Answers = new List<AnswerModel>();
            PostModels = new List<PostModel>();
        }
    }

    public class AnswerModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int PostId { get; set; }
        public string AnswerUserFirstName { get; set; }
        public string AnswerUserLastName { get; set; }

        public DateTime AnsweredDate { get; set; }

    }

}
