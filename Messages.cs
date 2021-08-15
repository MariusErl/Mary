using InSimDotNet;
using InSimDotNet.Packets;
using System;
using System.IO;
using System.Linq;

namespace Mary
{
    class Messages
    {
        static public bool afkMode = false, autoJoinChase = false;
        public static void ServerChat(InSim insim, IS_MSO mso)
        {
            try
            {
                string Msg = mso.Msg.Substring(mso.TextStart, (mso.Msg.Length - mso.TextStart));
                var user = Program.GetClient(mso.UCID, true);

                if (mso.UserType == UserType.MSO_SYSTEM)
                {
                    ChatAutomatics.CheckAuto(Msg);
                }

                if (mso.UserType == UserType.MSO_O)
                {
                    Commands.RunCommand(Msg);
                }

                if (Settings.CHATLOGGING)
                {
                    LogMsg(mso.Msg);
                }
            }
            catch (Exception e) { Program.ConsError(e.ToString()); }
        }


        static public void LogMsg(string msg)
        {
            StreamWriter Srrrr = new StreamWriter(@"Chatlogs\\" + CHATLOGDIR + ".txt", true);
            Srrrr.WriteLine(DateTime.Now + " : " + msg);
            Srrrr.Flush();
            Srrrr.Close();
        }

        static public void SendLocalMsg(string msg)
        {
            IS_MSL bajss = new IS_MSL { Msg = "^3Mary - ^8" + msg };
            Program.insim.Send(bajss);
        }

        static public void SendPublicMsg(string msg)
        {
            try
            {
                Program.insim.Send(msg.Replace("{", "").Replace("}", ""));
            }
            catch (Exception e) { Program.ConsError(e.ToString()); }
        }

        static public void SendMultiMsg(string[] msgs)
        {
            for (int x = 0; x < msgs.Count(); x++)
            {
                SendPublicMsg(msgs[x]);
                System.Threading.Thread.Sleep(550);
            }
        }

        static public void SendMsgRunScript(string msg)
        {
            string temp = msg;
            //First check if it got presets so mary must input some variables
            if (MsgContainsScript(msg))
            {
                temp = ScriptText(msg);
            }
            //Then check if it is multi - lined text
            if (msg.Contains("%"))
            {
                string[] splitMsg = temp.Split('%');
                SendMultiMsg(splitMsg);
            }
            else
            {
                SendPublicMsg(temp);
            }
        }

        static public bool MsgContainsScript(string msg)
        {
            bool ja = false;
            string[] Presets = new string[] { "{CARBEHIND}", "{SUSNAME}", "{SUSCAR}", "{SUSSPEEDKPH}", "{SUSSPEEDMPH}", "{SUSDIR}", "{MYCAR}", "{MYSPEEDKPH}", "{MYSPEEDMPH}",
                "{SUSDIST}", "{CARBEHINDDIST}","{MYLOC}", "{SUSLOC}", "{MYGRID}", "{SUSGRID}", "{MYSPEEDLIMKPH}", "{MYSPEEDLIMMPH}", "{SUSSPEEDLIMKPH}", "{SUSSPEEDLIMMPH}" };
            foreach (string streng in Presets) if (msg.Contains(streng)) ja = true;
            return ja;
        }

        static public string ScriptText(string msg)
        {
            string updatedMsg = msg;

            updatedMsg = updatedMsg.
                Replace("{CARBEHIND}", Tracker.ScanCarsBehind().PlayerName).
                Replace("{SUSNAME}", Tracker.Trackee.PlayerName).
                Replace("{SUSCAR}", Tracker.Trackee.P.CarName).
                Replace("{SUSSPEEDKPH}", "" + (Settings.UNITKPH ? Tracker.Trackee.P.speed : Math.Round(Tracker.Trackee.P.speed * 1.60934, 0))).
                Replace("{SUSSPEEDMPH}", "" + (Settings.UNITKPH ? Math.Round(Tracker.Trackee.P.speed * 0.621371, 0) : Tracker.Trackee.P.speed)).
                Replace("{SUSDIR}", Tracker.Trackee.P.dirName).
                Replace("{MYCAR}", Identification.MySelf.P.CarName).
                Replace("{MYSPEEDKPH}", "" + (Settings.UNITKPH ? Identification.MySelf.P.speed : Math.Round(Identification.MySelf.P.speed * 1.60934, 0))).
                Replace("{MYSPEEDMPH}", "" + (Settings.UNITKPH ? Math.Round(Identification.MySelf.P.speed * 0.621371, 0) : Identification.MySelf.P.speed)).
                Replace("{SUSDIST}", "" + Tracker.GetDirectDistance(Identification.MySelf, Tracker.Trackee)).
                Replace("{CARBEHINDDIST}", "" + Tracker.GetDirectDistance(Identification.MySelf, Tracker.ScanCarsBehind())).
                Replace("{MYLOC}", Identification.MySelf.P.Location).
                Replace("{SUSLOC}", Tracker.Trackee.P.Location).
                Replace("{MYGRID}", Tracks.CalculateGrid((int)Identification.MySelf.P.x, (int)Identification.MySelf.P.y)).
                Replace("{SUSGRID}", Tracks.CalculateGrid((int)Tracker.Trackee.P.x, (int)Tracker.Trackee.P.y)).
                Replace("{MYSPEEDLIMKPH}", Identification.MySelf.P.SpeedlimitKph.ToString()).
                Replace("{MYSPEEDLIMMPH}", Identification.MySelf.P.SpeedlimitMph.ToString()).
                Replace("{SUSSPEEDLIMKPH}", Tracker.Trackee.P.SpeedlimitKph.ToString()).
                Replace("{SUSSPEEDLIMMPH}", Tracker.Trackee.P.SpeedlimitMph.ToString());
            return updatedMsg;
        }

        static public string CHATLOGDIR = "";
        static public void CreateChatlogFile()
        {
            CHATLOGDIR = DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss");
            TextWriter tw = new StreamWriter(@"Chatlogs\\" + CHATLOGDIR + ".txt", true);
            tw.WriteLine("New chatlog started: " + DateTime.Now);
            tw.Close();
        }
    }
}