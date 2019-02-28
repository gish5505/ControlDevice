﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PISO_DA2_Ns;

namespace ControlDevice.Models
{

    public class OutputBoard
    {
        const byte boardNo = 0;
        const byte channel = 2;

        public int TotalBoard { get; private set; }

        public OutputBoard()        //check for boards
        {
            TotalBoard = PISODA2.TotalBoard();

            if (TotalBoard == 0)
                throw new ApplicationException("PISODA2 boards not found");
            else
                BoardActivation();

        }

        public void BoardActivation()       //activate boards 1-16 currently only for one
        {

            var result = PISODA2.ActiveBoard(boardNo);

            if (result > 0)
                AssertResul(result, $"Board activation failure, error code: {result}");
            else
                BoardJumperConfig();

        } 

        public void BoardJumperConfig()     //read settings
        {

            var result = PISODA2.ReadJumper(boardNo, out byte jumper);


            if (result != 0)
                AssertResul(result, $"Read Jumper error, error code:{result}");
            else JumperSettings(jumper);
                
        }


        public void JumperSettings(short jumper)        
        {

            var states = new string[][] {
                new string [] { "JP3 is set at 0-20 mA for the current output of channel 1", "JP3 is set at 4-20 mA for the current output of channel 1" },
                new string [] { "JP4 is set at –10 V for internal refernce voltage source of channel 1", "JP4 is set at –5 V for internal refernce voltage source of channel 1"},
                new string [] { "", ""},
                new string [] { "", ""},
                new string [] { "", ""},
                new string [] { "", ""}
            };

            var list = new List<string>();

            for (int i = 0, mask = 1; i < 6; i++)
            {
                var index = jumper & mask;
                var description = states[i][index];
                list.Add(description);

                mask = mask << 1;
            }

        }

        public byte OutputMode { get; set; } = 1;       //0-voltage output; 1-current sink

        public void BoardPushValue(float targetCurrent)
        {

            var result = PISODA2.DA(boardNo, channel, OutputMode, targetCurrent);

            AssertResul(result, $"PushValue error, code:{result}");
           
        }

        private void AssertResul(short result, string message)      //method for exception throw
        {
            if (result != 0)
                throw new ApplicationException(message);

        }

        private void Dispose()          //release resource
        {

            PISODA2.CloseBoard((byte)TotalBoard);

        }
    }
}
