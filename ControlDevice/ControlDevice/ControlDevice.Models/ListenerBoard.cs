using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniDAQ_Ns;

namespace ControlDevice.Models
{

    public interface IListenerBoard : IDisposable
    {
        int BoardNo { get; }

        
        float [] ReadBuffer();


    }



    public class ListenerBoard : IListenerBoard
    {
        
        private ListenerBoard()
        {

        }

        public ListenerBoard(int boardNo)
        {
            BoardNo = boardNo;
            var fValue = new float[3];
            
            ushort wTotalBoard = 0;

            var result = UniDAQ.Ixud_DriverInit(ref wTotalBoard);

            if (result != 0)
                throw new ApplicationException("Error initializing driver");

            result = UniDAQ.Ixud_GetAIBuffer(0, 3, fValue);
            if (result != 0)
                throw new ApplicationException("Buffer error");

            result = UniDAQ.Ixud_StartAI(0, 0, UniDAQ.IXUD_BI_20V, 10, 3);   //10 Hz, 3 samples per call
            if (result != 0)
                throw new ApplicationException("Error of data acquisition");

            


        }


        public int BoardNo { get; private set; }

        public float[] ReadBuffer()
        {
            float[] fValue = new float[3];
            var result = UniDAQ.Ixud_GetAIBuffer(0, 3, fValue);

            //float fValue = 0.0F;
            //result = UniDAQ.Ixud_ReadAI(0, 0, 0, ref fValue);


            return fValue;
        }

        public void Dispose()
        {
            UniDAQ.Ixud_DriverClose();
        }
    }

    public class ListenerBoardMock : IListenerBoard
    {
        private ListenerBoardMock()
        {

        }

        public ListenerBoardMock(int boardNo)
        {
            BoardNo = boardNo;
        }
        public int BoardNo { get; private set; }

        
        public float[] ReadBuffer()
        {
            var result = new[] { 1.0F, 2.0F, 3.0F };

            //Task.Run(() => Task.Delay(100));

            return result;
        }

        public float[] Buffer()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }
    }

}
