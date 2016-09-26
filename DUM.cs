using System;

namespace Mary
{
    class DUM
    {
        static public System.Timers.Timer RegularRutineTime;
        static public void RegularRutine(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!Program.insim.IsConnected) { Roleplay.CheckLFS(); }
            Clients me = Program.MySelf;

            if (Program.buttonsState > 1)
            {
                Clients ScannedCar = Tracker.ScanCars();
                if (Tracker.Trackee.Username != null)
                {
                    Tracker.TrackCar(Tracker.Trackee);
                    ButtonFactory.btnTracker1.Text = Tracker.Trackee.Username != null ? "^8" + Tracker.Trackee.P.SusInfo : (ScannedCar.Username != null ? "^3SCAN: ^8" + ScannedCar.P.SusInfo : "^3SCAN: ^8No car in range...");
                    ButtonFactory.btnTracker2.Text = "^6" + Tracker.GetDirectDirection(Tracker.GetAngel(Program.MySelf, Tracker.Trackee));
                    ButtonFactory.btnTracker3.Text = Tracker.SpeedDiffrence(Program.MySelf, Tracker.Trackee) + (Settings.UNITKPH ? "km/h" : "mph");
                    ButtonFactory.btnTracker4.Text = "^3" + Tracker.TrackeeDirectionRelativeToMe(Program.MySelf, Tracker.Trackee);
                    ButtonFactory.btnTracker5.Text = "^3" + (Settings.UNITKPH ? Program.CarSpeed(Tracker.Trackee.P.speed, true) : Program.CarSpeed(Tracker.Trackee.P.speed, false)) + (Settings.UNITKPH ? "km/h" : "mph");
                    ButtonFactory.OpenButton(ButtonFactory.btnTracker2);
                }
                else if (me.C.isCop && Settings.COPSCANNER)
                {
                    ButtonFactory.btnTracker1.Text = Tracker.Trackee.Username != null ? "^8" + Tracker.Trackee.P.SusInfo : (ScannedCar.Username != null ? "^3SCAN: ^8" + ScannedCar.P.SusInfo : "^3SCAN: ^8No car in range...");
                    ButtonFactory.OpenButton(ButtonFactory.btnTracker1);
                }

                if (Settings.TRIPCOUNTER)
                {
                    Trip.UpdateTripBtn();
                }

                if (Settings.PERFORMANCE)
                {
                    ButtonFactory.btnCPUusage2.Text = "^7" + Performance.getCurrentCpuUsage();
                    ButtonFactory.btnRAMfree2.Text = "^7" + Performance.getAvailableRAM();
                    ButtonFactory.OpenButton(ButtonFactory.btnCPUusage2);
                }
            }
            if (Program.IsOnline) Program.sendWlcMsg = true;
        }

        static public System.Timers.Timer FastRutineTime;
        static public void FastRutine(object sender, System.Timers.ElapsedEventArgs e)
        {
            Clients me = Program.MySelf;
            if (Settings.SPEEDCHECK) Tracker.TimeNullToHundred(me);
            if (Program.buttonsState > 1)
            {
                if (!Roleplay.PAUSE && Roleplay.SirenActive) Roleplay.UpdateSiren(false);
                if (Settings.CRUISECONTROL) CruiseControl.UpdateCruiseBtn(me);
                if (Settings.OUTGAUGE)
                {
                    //ButtonFactory.btnFuelOut2.Text = (Program.ViewedUser == Program.MySelf || Program.FULLVERSION) ? Math.Round((Memory.ReadFuel() / InSimDotNet.Helpers.CarHelper.GetFueltankSize(Program.ViewedUser.P.CarName)) * 100, 2).ToString("0.00") + "%" : "--";
                    ButtonFactory.btnFuelOut2.Text = Program.ViewedUser == Program.MySelf ? Math.Round((100 / InSimDotNet.Helpers.CarHelper.GetFueltankSize(Program.MySelf.P.CarName)) * (Program.MySelf.P.fuel * 100), 2).ToString("0.00") + "%" : "--";
                    //ButtonFactory.btnClutchOut2.Text = Math.Round(Memory.ReadClutchSlip() * 100, 2).ToString("0.00") + "%";
                    //ButtonFactory.btnEngineOut2.Text = (Program.ViewedUser == Program.MySelf || Program.FULLVERSION) ? Math.Round((1 - Memory.ReadEngDamage())*100, 2).ToString("0.00") + "%" : "--";
                    ButtonFactory.btnGearOut2.Text = Program.MySelf.P.Gear == 0 ? "R" : Program.MySelf.P.Gear == 1 ? "N" : (Program.MySelf.P.Gear - 1).ToString();

                    int carMaxRPM = InSimDotNet.Helpers.CarHelper.GetMaxRPM(me.P.CarName);
                    if ((me.P.RPM / carMaxRPM) > (float)Settings.SHIFTup)
                    {
                        ButtonFactory.btnGearUpDownOut.Text = "^1^J£";
                    }
                    else if (me.P.RPM < carMaxRPM * (float)Settings.SHIFTdown && me.P.RPM > 500 && me.P.Gear > 2)
                    {
                        ButtonFactory.btnGearUpDownOut.Text = "^2^J¥";
                    }
                    else
                    {
                        ButtonFactory.btnGearUpDownOut.Text = "";
                    }
                    ButtonFactory.OpenButton(ButtonFactory.btnGearUpDownOut);
                }
            }
        }

        static public System.Timers.Timer SlowRutineTime;
        static public void SlowRutine(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Settings.CHECKBUDDY && Program.BuddysNotice.Count > 0)
            {
                foreach (string bud in Program.BuddysNotice)
                {
                    string txt = Statistics.SearchBuddy(bud);
                    if (txt == "Invalid Ident-Key")
                    {
                        Program.ConsError("You have an invalid pubstat key.");
                        DUM.SlowRutineTime.Dispose();
                        return;
                    }
                    else if (txt.Length > 0)
                    {
                        Messages.SendLocalMsg(txt.Replace(",", ""));
                        Program.BuddysNotice.Remove(txt.Remove(0, 14).Split(',')[0]);
                    }
                }
            }
        }
    }
}