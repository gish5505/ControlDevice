using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PISO_DA2_Ns;

namespace ControlDevice.Models
{

    public class OutputBoard
    {
        const byte boardNo = 0; //board id in system by default and output channel used(on time of writing)
        const byte channel = 2;

        public int TotalBoard { get; private set; }

        public OutputBoard()        //check for boards
        {
            int TotalBoard = PISODA2.TotalBoard();

            if (TotalBoard == 0)
                AssertResul(TotalBoard,"PISODA2 boards not found");
            else
                BoardActivation();

        }

        public void BoardActivation()       //activate boards 1-16 currently only for one
        {

            short result = PISODA2.ActiveBoard(boardNo);

            if (result > 0)
                AssertResul(result, $"Board activation failure, error code: {result}");
            else
                BoardJumperConfig();

        } 

        public void BoardJumperConfig()     //read settings
        {

            short result = PISODA2.ReadJumper(boardNo, out byte jumper);


            if (result != 0)
                AssertResul(result, $"Read Jumper error, error code:{result}");
            else JumperSettings(jumper);
                
        }


        public void JumperSettings(short jumper)        //text status of jumper configuration
        {

            var states = new string[][] 
            {
                new string [] { "Current output is 0-20 mA on channel 1", "Current output is 4-20 mA on channel 1" },
                new string [] { "Reference voltage is –10 V on channel 1", "Reference voltage is –5 V on channel 1"},
                new string [] { "Bipolar setting on channel 1", "Unipolar setting on channel 1"},
                new string [] { "Current output is 0-20 mA on channel 2", "Current output is 4-20 mA on channel 2"},
                new string [] { "Reference voltage is –10 V on channel 2", "Reference voltage is –5 V on channel 2"},
                new string [] { "Bipolar setting on channel 2", "Unipolar setting on channel 2" }
            };

            var list = new List<string>();

            for (int i = 0, mask = 1; i < 6; i++)
            {
                var index = jumper & mask;
                index = index == 0 ? 0 : 1;
                var description = states[i][index];
                list.Add(description);

                mask = (mask << 1);
            }

        }

        public byte OutputMode { get; set; } = 1;       //0-voltage output; 1-current sink

        public void BoardPushValue(float targetCurrent) //use this method for pushing voltage value
        {

            var result = PISODA2.DA(boardNo, channel, OutputMode, targetCurrent);

            AssertResul(result, $"PushValue error, code:{result}");
           
        }

        private void AssertResul(int result, string message)      //method for exception throw
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
