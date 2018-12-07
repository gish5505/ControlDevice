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
      
        public int TotalBoard { get; private set; }

        public OutputBoard()
        {
            TotalBoard = PISODA2.TotalBoard();
            
            if (TotalBoard == 0)
                throw new ApplicationException("PISODA2 boards not found");
            else
                BoardActivation();

        }

        public void BoardActivation()
        {
            var result = PISODA2.ActiveBoard((byte)(TotalBoard));

            if (result != 0)
                throw new ApplicationException($"Board activation failure, error code: {result}");

        } 

        public void BoardJumperConfig()
        {
            byte boardNo = 0;

            var result = PISODA2.ReadJumper(boardNo, out byte jumper);

            if (result != 0)
                throw new ApplicationException($"Read Jumper error, error code:{result}");

        }

        public byte OutputMode { get; set } = 1;

        public void BoardPushValue(float targetCurrent)
        {
            const byte  boardNo = 1;
            const byte  channel = 2;            
            
            var result = PISODA2.DA(boardNo, channel, OutputMode, targetCurrent);

            AssertResul(result, $"PushValue error, code:{result}");
           
        }

        private void AssertResul(short result, string message)
        {
            if (result != 0)
                throw new ApplicationException(message);

        }
    }
}
