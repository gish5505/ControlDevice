using ControlDevice.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using PISO_Ns;
using System.Threading;
//used for console based input/testing, currently not active
namespace ControlDevice 
{
    class Program
    {
        private static OutputBoard outputBoard;

        static void Main(string[] args)
        {

                        //"using" is recommended for correct usage of IDisposable interface
                        using (IListenerBoard board = GetListenerBoard()) //expected listener board is piso-813 analog input card, expected output card is piso-da2/da2u
                        {
                            var boardId = board.CardSearch();

                            Console.WriteLine($"BoardId={boardId}");

                            var cardResult = board.CardPoll();
                            Console.WriteLine($"card result={cardResult}");
                            Console.ReadLine();
                        }
                      

            outputBoard = new OutputBoard(); //remove comments and set as startup for enabling console mode
            outputBoard.BoardPushValue(2);
        }

        private static IListenerBoard GetListenerBoard() //mock testing without driver
        {
            var configValue = ConfigurationManager.AppSettings["source"];

            IListenerBoard result;

            //if (configValue == "Mock")
            //    result = new ListenerBoardMock(0);
            //else
                result = new ListenerBoard(0); //number is system assigned boardNo

            return result;
        }


    }
}
