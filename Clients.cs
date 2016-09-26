using System;
using InSimDotNet.Packets;

namespace Mary
{
    public class Clients
	{
        public byte UniqueID, PlayerID;
        public string Username, PlayerName, PlayerNameRAW;
        public bool onTrack;
        public Connection C = new Connection();
        public Player P = new Player();

        #region C
        public class Connection
        {
            public bool isCop = false, isResQ = false;
            public string Country = "", Joindate = "", TimeCop = "", TimeRobber = "", TimeTotal = "";
            public Int32 KM= 0, Money = 0, Wealth = 0, XPCop = 0, XPRobber = 0, XPRP = 0;
            public long LFSdst, LFSfuel, LFSlaps, LFShosts, LFSwins, LFSfinished;
        }
        #endregion

        #region P
        public class Player
        {
            public int PType, AddedMassKG, IntakeRes, Gear = 0, x, y, speed, direction, heading, PitstopWilltake, TimeSpentInPit;
            public PlayerFlags Flags;
            public string CarName = "", SkinName = "", TiresFront, TiresRear;
            public float RPM = 0, tripDst, topSpeed, fuel;
            public string dirArrow, dirName, SusInfo;
            public bool refuling, passedHundred = false;
            public DateTime startDrive = DateTime.Now;
        }
        #endregion
    }
}