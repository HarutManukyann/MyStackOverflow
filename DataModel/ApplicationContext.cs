using Microsoft.EntityFrameworkCore;
using SteckOverflow.DataModel.Models;


namespace SteckOverflow.DataModel
{
    class ApplicationContext
    {

        internal class MyContext : DbContext
        {
            public DbSet<UserEntity> Users { get; set; }
            public DbSet<PostsModel> Posts { get; set; }
            public DbSet<AnswersModel> Answers { get; set; }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                //optionsBuilder.UseSqlServer("Data Source=s5;Initial Catalog=Harut_Stack;User Id=sa;Password=sa.123");
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-H5DPS4S;Database=netCore.Project;Trusted_Connection=True;MultipleActiveResultSets=true");


            }
        }
        public ApplicationContext()
        {

        }
    }
}
