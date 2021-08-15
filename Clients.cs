using InSimDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Mary
{
    public class Clients
    {
        public byte UniqueID, PlayerID;
        public string Username, PlayerName, PlayerNameRAW;
        public bool onTrack = false;
        public List<int> ButtonIDs = new List<int>();
        public Connection C = new Connection();
        public Player P = new Player();
        public Drift D = new Drift();

        #region C
        public class Connection
        {
            public bool isCop = false, isResQ = false, isCiv = false, autoSpeedtrap = true;
            public string Country = "", Joindate = "", TimeCop = "", TimeRobber = "", TimeTotal = "";
            public string KM = "", Bank = "", Wallet = "", XPCop = "", XPRobber = "", XPRP = "";
            public long LFSdst, LFSfuel, LFSlaps, LFShosts, LFSwins, LFSfinished;
        }
        #endregion

        #region P
        public class Player
        {
            public int PType, AddedMassKG, IntakeRes, Gear = 0, speed, direction, heading, PitstopWilltake, TimeSpentInPit, MiniMapX, MiniMapY, SpeedlimitKph, SpeedlimitMph, SuspectTimeout = 150;
            public PlayerFlags Flags;
            public string CarName = "", SkinName = "", TiresFront, TiresRear, Location = "spectating";
            public float RPM = 0, tripDst, topSpeed, fuel, x = 0, y = 0;
            public string dirArrow, dirName, SusInfo;
            public bool refuling, passedHundred = false, joinMode = false, Chasing = false;
            public DateTime startDrive = DateTime.Now;
            public Stopwatch afkStart = new Stopwatch();
            public List<Clients> Chase = new List<Clients>();
            public List<string> SuspectTrail = new List<string>();
        }
        #endregion

        public class Drift
        {
            public double Heading;
            public double Direction;
            public double totaldriftscore = 0;
            public double lastdriftscore = 0;
            public double absangle;
            public double bestscore;
            public double averagescore;
            public int updates = 0;
            public int driftticks = 0;
            // public double minangle = 15;
            public DateTime lastdrift = DateTime.Now;
            public bool cleaned = false;
            public bool driftLeftside = false;
            public DateTime driftTingSide = DateTime.Now;
            public int combo = 0;
            public int lastcombo = 0;
        }
    }
}