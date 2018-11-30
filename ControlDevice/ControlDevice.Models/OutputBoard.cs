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
        private OutputBoard()
        {

        }

        int TotalBoard { get; }

        public OutputBoard(int totalBoard)
        {
            totalBoard = PISODA2.TotalBoard();
            TotalBoard = totalBoard;

            if (TotalBoard == 0)
                throw new ApplicationException("PISODA2 boards not found");
            else
                BoardActivation();


        }

        public void BoardActivation()
        {
            var result = PISODA2.ActiveBoard(Convert.ToByte(TotalBoard));

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

        public void BoardValuePush()
        {
            byte boardNo = 1;
            byte Channel = 2;
            byte outputMode = 1; // 0 - voltage output, 1 - current sink
            float targetCurrent = 1; //to be removed after implementing calculation interface

            var result = PISODA2.DA(boardNo, Channel, outputMode, targetCurrent);

            if (result != 0)
                throw new ApplicationException($"ValuePush error, code:{result}");

        }
    }
}
