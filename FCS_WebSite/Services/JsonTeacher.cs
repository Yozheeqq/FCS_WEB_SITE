using FCS_WebSite.Models;
using System.Text.Json;

namespace FCS_WebSite.Services
{
    public class JsonTeacher : JsonFileService
    {
        public JsonTeacher(IWebHostEnvironment webHostEnvironment) : base(webHostEnvironment)
        {
        }

        private string JsonFileName =>
            Path.Combine(WebHostEnvironment.WebRootPath, "data", "teacher.json");

        public void WritePerson(Teacher teacher)
        {
            using(StreamWriter sw = new StreamWriter(JsonFileName, true))
            {
                sw.WriteLine(JsonSerializer.Serialize(teacher));
            }
        }
    }
}
