using quiz_app.Models.Database;
using quiz_app.Models.Database.Tests;

namespace quiz_app.DbMock
{
    public class TestsMock
    {
        public static List<Test> GetList()
        {
            return new List<Test>()
            {
                new()
                {
                    Id = "81536f98-c504-4c13-93ff-4f6bd087858c",
                    Name = "Simple Math Test",
                    Description = "A simple test for basic math knowledge.",
                    Questions = new List<Question>()
                    {
                        new()
                        {
                            Text = "What is the result of 2+2=?",
                            FirstAnswer = "3",
                            SecondAnswer = "4",
                            ThirdAnswer = "5",
                            FourthAnswer = "6",
                            CorrectAnswer = CorrectAnswer.Second
                        },
                        new()
                        {
                            Text = "What is the result of 3+3=?",
                            FirstAnswer = "5",
                            SecondAnswer = "6",
                            ThirdAnswer = "7",
                            FourthAnswer = "8",
                            CorrectAnswer = CorrectAnswer.Second
                        },
                        new()
                        {
                            Text = "What is the result of 2*2=?",
                            FirstAnswer = "3",
                            SecondAnswer = "4",
                            ThirdAnswer = "5",
                            FourthAnswer = "6",
                            CorrectAnswer = CorrectAnswer.Second
                        }
                    }
                },
                new()
                {
                    Id = "c11bdaa9-c850-4fb5-87a8-455a5273a522",
                    Name = "Simple English Test",
                    Description = "A simple test for English knowledge.",
                    Questions = new List<Question>()
                    {
                        new()
                        {
                            Text = "She arrived at 8 p.m., opened the door and shouted \"Good ______!\"",
                            FirstAnswer = "morning",
                            SecondAnswer = "evening",
                            ThirdAnswer = "night",
                            FourthAnswer = "bye",
                            CorrectAnswer = CorrectAnswer.Second
                        },
                        new()
                        {
                            Text = "I decided to put the table _______ the wall of the living room so that it would be out of the way.",
                            FirstAnswer = "in front of",
                            SecondAnswer = "opposed to",
                            ThirdAnswer = "against",
                            FourthAnswer = "versus",
                            CorrectAnswer = CorrectAnswer.Third
                        },
                        new()
                        {
                            Text = "The sold-out tickets made it clear that the speaker we invited to our charity event was _______ popular.",
                            FirstAnswer = "somewhat",
                            SecondAnswer = "extremely",
                            ThirdAnswer = "relatively",
                            FourthAnswer = "objectively",
                            CorrectAnswer = CorrectAnswer.Second
                        },
                        new()
                        {
                            Text = "They've been married for over fifty years, but she still remembers the day she first ______.",
                            FirstAnswer = "keep on him",
                            SecondAnswer = "stuck on him",
                            ThirdAnswer = "fell for him",
                            FourthAnswer = "wed him",
                            CorrectAnswer = CorrectAnswer.Third
                        }
                    }
                }
            };
        }
    }
}
