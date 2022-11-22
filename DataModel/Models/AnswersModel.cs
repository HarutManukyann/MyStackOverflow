using System;

namespace SteckOverflow.DataModel.Models
{
    public class AnswersModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int PostId { get; set; }
        public virtual PostsModel Post { get; set; }
        public int AnswerUserId { get; set; }
        public virtual UserEntity AnswerUser { get; set; }
        public int? Upvotes { get; set; }
        public int? Downvotes { get; set; }
        public DateTime AnsweredDate { get; set; }
        public AnswersModel()
        {
            AnsweredDate = DateTime.Now;

        }
    }
}
