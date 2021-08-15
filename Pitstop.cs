using InSimDotNet;
using InSimDotNet.Packets;
using System;

namespace Mary
{
    class Pitstop
    {
        public static void PitStop(InSim insim, IS_PIT pit)
        {
            var user = Program.GetClient(pit.PLID, false);
            if (user == Identification.MySelf)
            {
                PITButton = new System.Timers.Timer(1000)
                {
                    Enabled = true
                };
                PITButton.Elapsed += new System.Timers.ElapsedEventHandler(UpdatePITButton);

                if (pit.Work.HasFlag(PitWorkFlags.PSE_REFUEL))
                {
                    //Messages.SendLocalMsg("Refuel");
                    user.P.refuling = true;
                }
                if (pit.Work.HasFlag(PitWorkFlags.PSE_FR_DAM))
                {
                    //Messages.SendLocalMsg("Front damage");
                    Messages.SendLocalMsg("Sjelden skade: PSE_FR_DAM");
                }
                if (pit.Work.HasFlag(PitWorkFlags.PSE_RE_DAM))
                {
                    //Messages.SendLocalMsg("Rear damage");
                    Messages.SendLocalMsg("Sjelden skade: PSE_RE_DAM");
                }
                if (pit.Work.HasFlag(PitWorkFlags.PSE_FR_WHL))
                {
                    //Messages.SendLocalMsg("Front wheel changed");
                    Messages.SendLocalMsg("Sjelden skade: PSE_FR_WHL");
                }
                if (pit.Work.HasFlag(PitWorkFlags.PSE_RE_WHL))
                {
                    //Messages.SendLocalMsg("Rear wheel changed");
                    Messages.SendLocalMsg("Sjelden skade: PSE_RE_WHL");
                }

                if (pit.Work.HasFlag(PitWorkFlags.PSE_SETUP))
                {
                    user.P.PitstopWilltake += 3;
                    //Messages.SendLocalMsg("Change on setup");
                }
                if (pit.Work.HasFlag(PitWorkFlags.PSE_BODY_MAJOR))
                {
                    user.P.PitstopWilltake += 12;
                    //Messages.SendLocalMsg("Body major damage");
                }
                if (pit.Work.HasFlag(PitWorkFlags.PSE_BODY_MINOR))
                {
                    user.P.PitstopWilltake += 6;
                    //Messages.SendLocalMsg("Body minor damage");
                }
                if (pit.Work.HasFlag(PitWorkFlags.PSE_LE_FR_DAM))
                {
                    user.P.PitstopWilltake += 8;
                    //Messages.SendLocalMsg("Left front damage");
                }
                if (pit.Work.HasFlag(PitWorkFlags.PSE_LE_RE_DAM))
                {
                    user.P.PitstopWilltake += 8;
                    //Messages.SendLocalMsg("Left rear damage");
                }
                if (pit.Work.HasFlag(PitWorkFlags.PSE_RI_FR_DAM))
                {
                    user.P.PitstopWilltake += 8;
                    //Messages.SendLocalMsg("Right front damage");
                }
                if (pit.Work.HasFlag(PitWorkFlags.PSE_RI_RE_DAM))
                {
                    user.P.PitstopWilltake += 8;
                    //Messages.SendLocalMsg("Right rear damage");
                }
                if (pit.Work.HasFlag(PitWorkFlags.PSE_RI_FR_WHL))
                {
                    user.P.PitstopWilltake += 3;
                    //Messages.SendLocalMsg("Right frontwheel changed");
                }
                if (pit.Work.HasFlag(PitWorkFlags.PSE_LE_FR_WHL))
                {
                    user.P.PitstopWilltake += 3;
                    //Messages.SendLocalMsg("Left frontwheel changed");
                }
                if (pit.Work.HasFlag(PitWorkFlags.PSE_LE_RE_WHL))
                {
                    user.P.PitstopWilltake += 3;
                    //Messages.SendLocalMsg("Left rearwheel changed");
                }
                if (pit.Work.HasFlag(PitWorkFlags.PSE_RI_RE_WHL))
                {
                    user.P.PitstopWilltake += 3;
                    //Messages.SendLocalMsg("Right rearwheel changed");
                }
            }
        }

        static public System.Timers.Timer PITButton = new System.Timers.Timer(1000);
        static public void PitFinished(InSim insim, IS_PSF psf)
        {
            try
            {
                var user = Program.GetClient(psf.PLID, false);
                if (user == Identification.MySelf && PITButton.Enabled)
                {
                    PITButton.Dispose();
                    //Buttons.RemoveButton(Program.MySelf, "pitbtn1");
                    // Buttons.RemoveButton(Program.MySelf, "pitbtn2");
                    user.P.PitstopWilltake = 0;
                    user.P.TimeSpentInPit = 0;
                    user.P.refuling = false;
                }
            }
            catch (Exception e) { Program.ConsError(e.ToString()); }
        }

        static public void UpdatePITButton(object sender, System.Timers.ElapsedEventArgs e)
        {
            var user = Identification.MySelf;
            user.P.TimeSpentInPit++;
            int PitStopWillTake = user.P.PitstopWilltake;
            int TimeSpentinPit = user.P.TimeSpentInPit;
            _ = PitStopWillTake - TimeSpentinPit;

            //if (timeleft < 0)
            //{
            //    timeleft = 0;
            //}
            //string refuelStar = "";
            //if (user.P.refuling)
            //{
            //    refuelStar = "^1 + fuling";
            //}
            //Buttons.OpenButton(Program.MySelf, "pitbtn1", "^3Approx pittime left:" + timeleft + refuelStar + "^3,  Total [" + TimeSpentinPit + "]", ButtonStyles.ISB_DARK, 5, 60, 70, 65, 0);
            //Buttons.OpenButton(Program.MySelf, "pitbtn2", Progressbar.Progress(TimeSpentinPit, PitStopWillTake, 150), ButtonStyles.ISB_DARK, 5, 40, 80, 70, 0);
        }
    }
}
