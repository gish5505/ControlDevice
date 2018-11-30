using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace PISO_DA2_Ns
{
    public class PISODA2
    { 
        public const int NoError = 0;
        public const int ActiveBoardError = 1;
        public const int ExceedFindBoards = 2;
        public const int DriverNoOpen = 3;
        public const int BoardNoActive = 4;
        public const int WriteEEPROMError = 5;
        public const int ParameterError = 6;


        [DllImport("PISODA.dll", EntryPoint = "PISODA_GetDllVersion")]
        public static extern  int GetDllVersion();


        [DllImport("PISODA.dll", EntryPoint = "PISODA_ActiveBoard")]
        public static extern short ActiveBoard(byte BoardNo);


        [DllImport("PISODA.dll", EntryPoint = "PISODA_CloseBoard")]
        public static extern int CloseBoard(byte BoardNo);


        [DllImport("PISODA.dll", EntryPoint = "PISODA_TotalBoard")]
        public static extern int TotalBoard();


        [DllImport("PISODA.dll", EntryPoint = "PISODA_GetCardInf")]
        public static extern int GetCardInf(byte BoardNo , out long dwVID, out long dwDID , out long dwSVID , out long dwSDID , out long dwSAuxID , out long dwIrq );


        [DllImport("PISODA.dll", EntryPoint = "PISODA_IsBoardActive")]
        public static extern  byte IsBoardActive( byte BoardNo );


        [DllImport("PISODA.dll", EntryPoint = "PISODA_2DA_Hex")]
        public static extern int PISODA_2DA_Hex(byte BoardNo, byte bChannel, int wValue);


        [DllImport("PISODA.dll", EntryPoint = "PISODA_DA")]
        public static extern  short DA( byte BoardNo , byte  bChannel , byte  bOpt ,float  fValue ) ;


        [DllImport("PISODA.dll", EntryPoint = "PISODA_ReadJumper")]
        public static extern  short ReadJumper( byte BoardNo , out byte Jumper );


        [DllImport("PISODA.dll", EntryPoint = "PISODA_ReadEEP")]
        public static extern short ReadEEP( byte  BoardNo, out short wValue ) ;


        [DllImport("PISODA.dll", EntryPoint = "PISODA_WriteEEP")]
        public static extern short WriteEEP( byte BoardNo , out short wValue ) ;


        [DllImport("PISODA.dll", EntryPoint = "PISODA_OutputByte")]
        public static extern void  OutputByte( byte BoardNo , long  dwOffset , byte  bValue );


        [DllImport("PISODA.dll", EntryPoint = "PISODA_InputByte")]
        public static extern byte InputByte( byte BoardNo , long  dwOffset ) ;


        [DllImport("PISODA.dll", EntryPoint = "PISODA_OutputWord")]
        public static extern void  OutputWord( byte BoardNo ,  long dwOffset , int  wValue );


        [DllImport("PISODA.dll", EntryPoint = "PISODA_InputWord")]
        public static extern int InputWord(byte BoardNo, long dwOffset);
    
    }

}


