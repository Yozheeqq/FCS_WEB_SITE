using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FCS_WebSite.Models;

namespace FCS_WebSite.Services
{
    /// <summary>
    /// Абстрактный класс для работы с json файлами
    /// </summary>
    public abstract class JsonFileService
    {
        /// <summary>
        /// Конструктор для инициализации
        /// </summary>
        /// <param name="webHostEnvironment">Информация о веб-хосте</param>
        public JsonFileService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        /// <summary>
        /// Веб-хост
        /// </summary>
        public IWebHostEnvironment WebHostEnvironment { get; }

        public void WritePerson(IPersonable person)
        {
            
        }
    }
}
