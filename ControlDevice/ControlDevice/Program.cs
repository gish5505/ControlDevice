﻿using ControlDevice.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using PISO_Ns;

namespace ControlDevice 
{
    class Program
    {
        static void Main(string[] args)
        {
            
                        
            using (IListenerBoard board = GetListenerBoard()) //expected listener board is piso-813 analog input card, expected output card is piso-da2/da2u
            {
                for (int i = 0; i < 100; i++)
                {
                    var results = board.ReadBuffer();



                }
            }

            //board.Dispose();
            
        }

        private static IListenerBoard GetListenerBoard()
        {
            var configValue = ConfigurationManager.AppSettings["source"];

            IListenerBoard result;

            if (configValue == "Mock")
                result = new ListenerBoardMock(0);
            else
                result = new ListenerBoard(0);

            return result;
        }
    }
}
