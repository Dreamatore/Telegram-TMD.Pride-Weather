using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram_TMD.Pride
{
    internal class MainProgram
    {
        static void Main(string[] args)
        {           

            Console.Title = "TMD.PRIDE.RIOT by SPD";

            TelegramBot bot = new TelegramBot();
            bot.WorkBot();          
        }
    }
}