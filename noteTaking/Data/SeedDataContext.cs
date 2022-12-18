using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using noteTaking;
using noteTaking.Models;

namespace noteTaking.Data
{
    public class SeedDataContext
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new noteTakingContext(serviceProvider.GetRequiredService<DbContextOptions<noteTakingContext>>()))
            {
                context.Database.EnsureCreated();

                if (!context.Category.Any())
                {
                    context.Category.AddRange
                        (
                            new Category { SubjectName = "Technology" },
                            new Category { SubjectName = "Sports" },
                            new Category { SubjectName = "Fashion" }
                            
                        );
                    context.SaveChanges();
                }

                if (!context.Posts.Any())
                {
                    context.Posts.AddRange
                        (

                            new Posts
                            {

                                Title = "My first post",
                                Content = "This is the content of my first post.",
                                CategoryId = 1
                            },

                            new Posts
                            {

                                Title = "My second post",
                                Content = "This is the content of my second post.",
                                CategoryId = 2,
                                
                            },
                            new Posts
                            {

                                Title = "My third post",
                                Content = "This is the content of my third post.",
                                CategoryId = 3
                            }


                        ) ;
                    context.SaveChanges();

                    if (!context.Comments.Any())
                    {
                        context.Comments.AddRange
                            (
                                new Comments
                                {

                                    Comment = "This is a great post!",
                                    PostId = 1
                                },

                                new Comments
                                {

                                    Comment = "I agree, this is a great post!",
                                    PostId = 1
                                },

                                new Comments
                                {

                                    Comment = "I'm sorry, but I disagree.",
                                    PostId = 1
                                }

                               
                                
                            ); 
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
