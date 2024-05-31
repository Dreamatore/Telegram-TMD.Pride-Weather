using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram_TMD.Pride
{
    internal class MainProgram
    {
        static void Main(string[] args)
        {
            DataBase.CreateTable();
            DataBase.UpdateCity(123, "Moscow");
            var city = DataBase.ReadCity(123);
            Console.Title = "TMD.PRIDE.RIOT by SPD";

            TelegramBot bot = new TelegramBot();
            bot.WorkBot();          
        }
    } 
}