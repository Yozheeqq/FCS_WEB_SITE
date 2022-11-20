using FCS_WebSite.Models;
using FCS_WebSite_v2.Data.Forms;
using Microsoft.EntityFrameworkCore;

namespace FCS_WebSite_v2.Data.DB
{
    public class DBObjects
    {
        public static ApplicationDbContext Content
        {
            get;
            set;
        }

        public static void InitialPupil(Pupil pupil)
        {
            Content.Pupil.Add(pupil);
            Content.SaveChanges();
        }

        public static void InitialTeacher(Teacher teacher)
        {
            Content.Teacher.Add(teacher);
            Content.SaveChanges();
        }

        public static void InitialForm(Form form)
        {
            Content.Form.Add(form);
            Content.SaveChanges();
        }

        public static void InitialFormQuestion(FormQuestion formQuestion)
        {
            Content.FormQuestion.Add(formQuestion);
            Content.SaveChanges();
        }

        public static void InitialFormQuestionAnswer(FormQuestionAnswer formQuestionAnswer)
        {
            Content.FormQuestionAnswer.Add(formQuestionAnswer);
            Content.SaveChanges();
        }

        public static DbSet<Pupil> GetPupil()
        {
            return Content.Pupil;
        }

        public static DbSet<Teacher> GetTeacher()
        {
            return Content.Teacher;
        }

        public static DbSet<Form> GetForm()
        {
            return Content.Form;
        }

        public static DbSet<FormQuestion> GetFormQuestions()
        {
            return Content.FormQuestion;
        }

        public static DbSet<FormQuestionAnswer> GetFormQuestionAnswers()
        {
            return Content.FormQuestionAnswer;
        }
    }
}
