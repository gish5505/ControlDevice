using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PISO_Ns;

namespace ControlDevice.Models
{

    public interface IListenerBoard : IDisposable //interfaces for implementing something else (check factory method)
    {
        int BoardNo { get; }

        ushort CardSearch();

        float CardPoll();
    }



    public class ListenerBoard : IListenerBoard
    {
        private readonly ConfigAddress _config = null;

        private ListenerBoard() //constructor
        {

        }

        public ListenerBoard(int boardNo)
        {
            BoardNo = boardNo;

            var result = PISO813.DriverInit();

            //expected errors list

            if (result != 0)
                throw new ApplicationException("Error initializing driver");

            CardSearch();

            _config = GetConfigAddressSpace();

        }


        public int BoardNo { get; private set; }




        public ushort CardSearch() //method for detecting card in system
        {

            var result = PISO813.SearchCard(out ushort boardID, 0X800A00);

            if (result != 0 && boardID >= 1)
                throw new ApplicationException($"piso 813 not found/working; error code: {result} , piso 813 in system : {boardID}");

            return boardID;
        }
        //physical card address required 813-board 0 da2-board 1

        private ConfigAddress GetConfigAddressSpace() //getting card charasteristics and prepping for activation
        {
//            var boardId = CardSearch();


            var result = PISO813.GetConfigAddressSpace(Convert.ToUInt16(BoardNo), out uint addrBase, out ushort irqNo, out ushort subVendor, out ushort subDevice, out ushort subAux, out ushort slotBus, out ushort slotDevice);


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


            ushort Channel = 0; //hardware requirement
            PISO813.SetChGain(addrBase, Channel, 0); //unipolar jp1 5v jp2 10v


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


       public float CardPoll() //use this method for getting data
       {

            ushort Jump20v = 0;
            ushort Bipolar = 0; //1-yes, 0-no

            float result = PISO813.AD_Float(_config.AddressBase,Jump20v, Bipolar);
            //should return -10 10, value scaled according to SetChGain
            if (result == -100)
                throw new ApplicationException("a/d converter failed");

            return result;
       }


        public void Dispose() //mechanism for releasing resources
        {
            PISO813.DriverClose();
        }
    }



    public class ListenerBoardMock : IListenerBoard
    {
        Random _rnd = new Random(DateTime.Now.Millisecond);

        public ListenerBoardMock()
        {

        }

        public int BoardNo => throw new NotImplementedException();

        private int _count = 2;
   
        public float CardPoll()
        {
            var result = _rnd.NextDouble() * 5;

            return (float)result;
            //return _count++;
        }

        public ushort CardSearch()
        {
            return (ushort)-_count--;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }





}
