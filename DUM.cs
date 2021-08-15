using System;
using System.Collections.Generic;

namespace Mary
{
    class DUM
    {
        #region RegularRutine (1000ms)
        static public System.Timers.Timer RegularRutineTime;
        static public void RegularRutine(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!Program.insim.IsConnected) { Roleplay.CheckLFS(); }

            if (Program.buttonsState > 1)
            {
                if (Settings.TRIPCOUNTER)
                {
                    Trip.UpdateTripBtn();
                }

                if (Settings.PERFORMANCE)
                {
                    ButtonFactory.btnCPUusage2.Text = "^" + Settings.BUTTONCOLOURPREF + Performance.GetCurrentCpuUsage();
                    ButtonFactory.btnRAMfree2.Text = "^" + Settings.BUTTONCOLOURPREF + Performance.GetAvailableRAM();
                    ButtonFactory.OpenButton(ButtonFactory.btnCPUusage2);
                }
            }
            Statistics.GetRoadName(Identification.MySelf);
            if (Program.IsOnline) Program.sendWlcMsg = true;

            if (Program.hideADTimer)
            {
                adTimer++;
                if (adTimer >= 32)
                {
                    Messages.SendPublicMsg("!hidead");
                    Program.hideADTimer = false;
                    adTimer = 0;
                }
            }

            Roleplay.CarAlarm();

            if (Path.RecordMe)
            {
                Path.MyTrace.Add(Identification.MySelf.P.x + "," + Identification.MySelf.P.y);
            }
        }
        static public int adTimer = 0;
        #endregion

        #region FastRutine (500ms)
        static public System.Timers.Timer FastRutineTime;
        static public void FastRutine(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Clients me = Identification.MySelf;
                if (Settings.SPEEDCHECK) Tracker.TimeNullToHundred(me);
                if (Program.buttonsState > 1)
                {
                    Clients ScannedCar = new Clients();
                    if (me.C.isCop && Settings.COPSCANNER && Tracker.Trackee.Username == null)
                    {
                        ScannedCar = Tracker.ScanCars();
                    }
                    if (Tracker.Trackee.Username != null)
                    {
                        Statistics.GetRoadName(Tracker.Trackee);
                        ButtonFactory.btnTracker1.Text = "^8" + Tracker.Trackee.P.SusInfo;
                        Tracker.UpdateTrail(Tracker.Trackee);
                    }
                    else if (me.C.isCop && Settings.COPSCANNER)
                    {
                        ButtonFactory.btnTracker1.Text = ScannedCar.Username != null ? "^3SCAN: ^8" + ScannedCar.P.SusInfo : "^3SCAN: ^8No car in range...";
                        ButtonFactory.OpenButton(ButtonFactory.btnTracker1);
                    }

                    if (Settings.MINIMAPLOCOLD != 0 && me.P.Chase.Count > 0)
                    {
                        Tracker.UpdateTrail(me);
                        for (int z = 0; z < me.P.Chase.Count; z++)
                        {
                            Tracker.UpdateTrail(me.P.Chase[z]);
                        }
                    }
                    else if (ButtonFactory.ButtonsUsed.Contains(ButtonFactory.MinimapTrackerCop1))
                    {
                        ButtonFactory.RemoveButton(ButtonFactory.MinimapTrackerCop1);
                    }

                    if (Roleplay.SirenActive) Roleplay.UpdateSiren(false);
                    if (Settings.CRUISECONTROL) CruiseControl.UpdateCruiseBtn(me);
                    if (Settings.OUTGAUGE)
                    {
                        //ButtonFactory.btnFuelOut2.Text = "^" + Settings.BUTTONCOLOURPREF + (Identification.FULLVERSION || Identification.MySelf == Program.ViewedUser ? (fuel <= 3 ? "^1" : "") + fuel.ToString("0.00") + "%" : "--");
                        //ButtonFactory.btnClutchOut2.Text = "^" + Settings.BUTTONCOLOURPREF + (Identification.FULLVERSION || Identification.MySelf == Program.ViewedUser ? Math.Round(Memory.ReadClutchSlip() * 100, 2).ToString("00.00") + "%" : "--");
                        //ButtonFactory.btnEngineOut2.Text = "^" + Settings.BUTTONCOLOURPREF + (Identification.FULLVERSION || Identification.MySelf == Program.ViewedUser ? Math.Round((1 - Memory.ReadEngDamage()) * 100, 2).ToString("00.00") + "%" : "--");

                        int carMaxRPM = LibData.GetMaxRPM(me.P.CarName);
                        if ((me.P.RPM / carMaxRPM) > (float)Settings.SHIFTup)
                        {
                            ButtonFactory.btnGearUpDownOut.Text = "^1" + UnicodeEncoding.FatUp;
                        }
                        else if (me.P.RPM < carMaxRPM * (float)Settings.SHIFTdown && me.P.RPM > 500 && me.P.Gear > 2)
                        {
                            ButtonFactory.btnGearUpDownOut.Text = "^2" + UnicodeEncoding.FatDown;
                        }
                        else
                        {
                            ButtonFactory.btnGearUpDownOut.Text = "";
                        }
                        if (Program.buttonsState > 1)
                            ButtonFactory.OpenButton(ButtonFactory.btnGearUpDownOut);
                    }

                    // Drifting.CheckDrift(Identification.MySelf);
                }
            }
            catch (Exception ee) { Program.ConsError(ee.ToString()); }
        }
        #endregion

        #region SuperFastRutine (100ms)
        static public System.Timers.Timer SuperFastRutineTime;
        static public void SuperFastRutine(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (Program.buttonsState > 1)
                {
                    lock (ButtonFactory.ButtonsUsed)
                    {
                        if (Tracker.Trackee.Username != null)
                        {
                            Tracker.TrackCar(Tracker.Trackee);
                            ButtonFactory.btnTracker2.Text = "^6" + Tracker.GetDirectDirection(Tracker.GetAngele(Identification.MySelf, Tracker.Trackee));
                            ButtonFactory.btnTracker3.Text = Tracker.SpeedDiffrence(Identification.MySelf, Tracker.Trackee) + (Settings.UNITKPH ? "km/h" : "mph");
                            ButtonFactory.btnTracker4.Text = "^3" + Tracker.TrackeeDirectionRelativeToMe(Identification.MySelf, Tracker.Trackee);
                            ButtonFactory.btnTracker5.Text = Tracker.GetDirectDistance(Identification.MySelf, Tracker.Trackee) + "m";
                            ButtonFactory.OpenButton(ButtonFactory.btnTracker2);
                            Tracker.CalculateTrail(Identification.MySelf, Tracker.Trackee, ButtonFactory.btnTracker6, true);

                            if (Settings.MINIMAP == 1 || Settings.MINIMAP == 3)
                            {
                                DefaultMinimap.ConvertCoordToMinimap(Tracker.Trackee);
                                ButtonFactory.MinimapTrackerSusp.LeftRight = (byte)Tracker.Trackee.P.MiniMapX;
                                ButtonFactory.MinimapTrackerSusp.TopDown = (byte)Tracker.Trackee.P.MiniMapY;
                                ButtonFactory.MinimapTrackerSusp.Text = "^3" + Tracker.Trackee.P.dirArrow;
                                if (!Identification.FULLVERSION)
                                {
                                    ButtonFactory.MinimapTrackerSusp.Text = "";
                                }
                            }
                            ButtonFactory.OpenButton(ButtonFactory.btnTracker2);
                        }
                        if (Settings.MINIMAPLOCOLD != 0 && Identification.MySelf.P.Chase.Count > 0)
                        {
                            DefaultMinimap.UpdateMinimapCops();
                        }
                    }
                }
            }
            catch (Exception ee) { Program.Cons(ee.ToString()); }
        }
        #endregion

        #region SlowRutine (5000ms)
        static public List<string> toDelete = new List<string>();
        static public System.Timers.Timer SlowRutineTime;
        static public void SlowRutine(object sender, System.Timers.ElapsedEventArgs e)
        {
            toDelete.Clear();
            if (Settings.CHECKBUDDY && Program.BuddysNotice.Count > 0)
            {
                foreach (string bud in Program.BuddysNotice)
                {
                    Statistics.SearchBuddy(bud);
                }
            }
            foreach (string delete in toDelete)
            {
                if (Program.BuddysNotice.Contains(delete))
                { Program.BuddysNotice.Remove(delete); }
            }
        }
        #endregion
    }
}