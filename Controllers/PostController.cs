using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SteckOverflow.DataModel.Models;
using SteckOverflow.Models;
using System.Linq;
using static SteckOverflow.DataModel.ApplicationContext;

namespace SteckOverflow.Controllers
{
    public class PostController : Controller
    {
        MyContext context = new MyContext();


        public IActionResult PostList(string sortOrder, string searchString)
        {
            ViewData["IdSortParm"] = sortOrder == "Id" ? "Id_desc" : "Id";
            ViewData["UserIdSortParm"] = sortOrder == "UserId" ? "userId_desc" : "UserId";
            ViewData["CreateSortParm"] = sortOrder == "PostedDate" ? "createDate_desc" : "PostedDate";
            ViewData["CloseSortParm"] = sortOrder == "ClosedDate" ? "closeDate_desc" : "ClosedDate";
            ViewData["Filter"] = searchString;
            var names = from x in context.Posts
                        select x;

            if (searchString != null)
            {
                names = names.Where(x => x.Title.Contains(searchString) || x.QuestionText.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Id_desc":
                    names = names.OrderByDescending(x => x.Id);
                    break;
                case "Id":
                    names = names.OrderBy(x => x.Id);
                    break;
                case "userId_desc":
                    names = names.OrderByDescending(x => x.UserId);
                    break;
                case "UserId":
                    names = names.OrderBy(x => x.UserId);
                    break;
                case "createDate_desc":
                    names = names.OrderByDescending(s => s.PostedDate);
                    break;
                case "PostedDate":
                    names = names.OrderBy(x => x.PostedDate);
                    break;
                case "closeDate_desc":
                    names = names.OrderByDescending(x => x.ClosedDate);
                    break;
                case "ClosedDate":
                    names = names.OrderBy(x => x.ClosedDate);
                    break;
                default:
                    names = names.OrderBy(x => x.Id);
                    break;
            }
            return View(names.ToList());
        }

        public ActionResult InPost(int postId)
        {
            var userid = HttpContext.Session.GetInt32("Id");
            if (userid == null)
                return RedirectToAction(nameof(HomeController.Login), "Home");

            var post = context.Posts.Where(x => x.Id == postId).Select(p => new PostModel
            {
                Id = p.Id,
                Title = p.Title,
                QuestionText = p.QuestionText,
                UserId = p.UserId,
                PostedDate = p.PostedDate


            }).SingleOrDefault();




            post.Answers = context.Answers.Where(x => x.PostId == postId).Select(a => new AnswerModel
            {
                Id = a.Id,
                Text = a.Text,
                AnswerUserFirstName = a.AnswerUser.FirstName,
                AnswerUserLastName = a.AnswerUser.LastName,
                AnsweredDate = a.AnsweredDate,
            }).ToList();

            return View(post);
        }

        public IActionResult MyPosts()
        {
            var userid = HttpContext.Session.GetInt32("Id");
            var filter = context.Posts.Where(x => x.UserId == userid);
            return View(filter);
        }
        [HttpGet]
        public IActionResult CreateAns(int postId)
        {
            AnswerModel ans = new AnswerModel();
            ans.PostId = postId;
            return PartialView("_CreateAns", ans);
        }
        [HttpPost]
        public IActionResult CreateAns(AnswersModel ans, int postId)
        {

            var userid = HttpContext.Session.GetInt32("Id");


            //var postid = HttpContext.Session.GetInt32("");
            using (var db = new MyContext())
            {

                db.Add(new AnswersModel
                {
                    Text = ans.Text,
                    AnsweredDate = ans.AnsweredDate,
                    AnswerUserId = (int)userid,
                    PostId = ans.PostId

                });
                db.SaveChanges();


            }
            return RedirectToAction("InPost", "Post", new { postId = postId });

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Create(PostModel postModel)
        {

            var userid = HttpContext.Session.GetInt32("Id");
            using (var db = new MyContext())
            {
                db.Add(new PostsModel
                {
                    Title = postModel.Title,
                    QuestionText = postModel.QuestionText,
                    UserId = (int)userid

                });
                db.SaveChanges();


            }
            return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        public ActionResult Edit(int Id) //post id 
        {

            var post = context.Posts.Where(x => x.Id == Id).SingleOrDefault();

            return View(post);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int Id, PostsModel postModel)
        {

            if (Id == postModel.Id)
            {
                var newpost = context.Posts.Where(x => x.Id == Id).SingleOrDefault();
                newpost.Title = postModel.Title;
                newpost.QuestionText = postModel.QuestionText;
                newpost.ClosedDate = postModel.ClosedDate;
                context.SaveChanges();
            }
            return RedirectToAction("PostList", "Post");
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var post = context.Posts.Where(x => x.Id == Id).SingleOrDefault();

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int Id, PostsModel postModel)
        {
            if (postModel.Id == Id)
            {
                context.Posts.Remove(postModel);
                var postAnswers = context.Answers.Where(a => a.PostId == Id);
                context.Answers.RemoveRange(postAnswers);
                context.SaveChangesAsync();
            }
            return RedirectToAction("PostList", "Post");
        }

    }
}
