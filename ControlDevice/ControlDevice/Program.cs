using ControlDevice.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using PISO_Ns;
using System.Threading;

namespace ControlDevice 
{
    class Program
    {
        static void Main(string[] args)
        {
            
                        
            using (IListenerBoard board = GetListenerBoard()) //expected listener board is piso-813 analog input card, expected output card is piso-da2/da2u
            {
//                var boardId = board.CardSearch();

//                Console.WriteLine($"BoardId={boardId}");

                var cardResult = board.CardPoll();
                Console.WriteLine($"card result={cardResult}");
                Console.ReadLine();
                board.Dispose();

            }

            
            
        }

        private static IListenerBoard GetListenerBoard()
        {
            var configValue = ConfigurationManager.AppSettings["source"];

            IListenerBoard result;

            //if (configValue == "Mock")
            //    result = new ListenerBoardMock(0);
            //else
                result = new ListenerBoard(0);

            return result;
        }
    }
}
