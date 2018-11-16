using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PISO_Ns;

namespace ControlDevice.Models
{

    public interface IListenerBoard : IDisposable
    {
        int BoardNo { get; }



        ushort CardSearch();

        float CardPoll();


    }



    public class ListenerBoard : IListenerBoard
    {
        private readonly ConfigAddress _config = null;

        private ListenerBoard()
        {

        }

        public ListenerBoard(int boardNo)
        {
            BoardNo = boardNo;

            var result = PISO813.DriverInit();

            //expected errors list

            if (result != 0)
                throw new ApplicationException("Error initializing driver");

            _config = GetConfigAddressSpace();

        }


        public int BoardNo { get; private set; }




        public ushort CardSearch()
        {

            var result = PISO813.SearchCard(out ushort boardID, 0X800A00);

            if (result == 0)
                throw new ApplicationException($"Invalid board found");

            return boardID;
        }
        //physical card address required 813-board 0 da2-board 1

        
        private ConfigAddress GetConfigAddressSpace()
        {
            var boardId = CardSearch();


            var result = PISO813.GetConfigAddressSpace(boardId, out uint addrBase, out ushort irqNo, out ushort subVendor, out ushort subDevice, out ushort subAux, out ushort slotBus, out ushort slotDevice);


            if (result != 0)
                throw new ApplicationException($"Get Address failed, probably invalid BoardNo {result}");

            var config = new ConfigAddress()
            {
                AddressBase = addrBase,
                IrqNo = irqNo,
                SubVendor = subVendor,
                SubDevice = subDevice,
                SubAux = subAux,
                SlotBus = slotBus,
                SlotDevice = slotDevice
            };

            //portreset

            PISO813.OutputByte((ushort)addrBase, 1);  //channel reset?


            ushort Channel = 0;
            PISO813.SetChGain(addrBase, Channel, 1); //unipolar jp1 5v jp2 10v


            return config;
        }

        public class ConfigAddress
        {
            public uint AddressBase { get; set; }

            public ushort IrqNo { get; set; }

            public ushort SubVendor { get; set; }

            public ushort SubDevice { get; set; }

            public ushort SubAux { get; set; }

            public ushort SlotBus { get; set; }

            public ushort SlotDevice { get; set; }

        }

        /*
        public uint TakeFromPortInput()
        {
            
            var result = PISO813.InputByte((ushort)_config.AddressBase);

            return result;
        }

            */

       public float CardPoll()
        {

            ushort Jump20v = 0;
            ushort Bipolar = 0; //1-yes, 0-no

            var result = PISO813.AD_Float(_config.AddressBase,Jump20v, Bipolar);
            //should return -10 10, value scaled according to SetChGain
            if (result == -100)
                throw new ApplicationException("a/d converter failed");

            return result;
        }

        public float[] ReadBuffer()
        {
            throw new NotImplementedException();
        }
               


        public void Dispose()
        {
            PISO813.DriverClose();
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

 /*       public float[] Buffer()
        {
            throw new NotImplementedException();
        }
*/
        public void Dispose()
        {
            
        }

        public void InitOperation()
        {
            //throw new NotImplementedException();
        }

        public void StopOperation()
        {
            //throw new NotImplementedException();
        }

        public ushort CardSearch()
        {

            return 178;
        }

        public float CardPoll()
        {
            return 61;
        }
    }

}
