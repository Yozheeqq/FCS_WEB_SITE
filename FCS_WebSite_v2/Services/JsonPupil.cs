using Microsoft.AspNetCore.Hosting;
using FCS_WebSite.Models;
using System.Text.Json;

namespace FCS_WebSite.Services
{
    public class JsonPupil : JsonFileService
    {
        public JsonPupil(IWebHostEnvironment webHostEnvironment) : base(webHostEnvironment)
        {
        }

        private string JsonFileName =>
            Path.Combine(WebHostEnvironment.WebRootPath, "data", "pupil.json");

        public void WritePerson(Pupil pupil)
        {
            using (StreamWriter sw = new StreamWriter(JsonFileName, true))
            {
                sw.WriteLine(JsonSerializer.Serialize(pupil));
            }
        }
    }
}
