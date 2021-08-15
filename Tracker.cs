using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mary
{
    class Tracker
    {
        static public Clients Trackee = new Clients();

        static public void EditUserToTrack(string input)
        {
            try
            {
                bool userisOnline = false;
                Clients oldUserToLocate = Trackee;

                foreach (Clients everyone in Program.AllConnections)
                {
                    if (everyone.Username.ToLower().Equals(input))
                    {
                        Trackee = everyone;
                        userisOnline = true;
                    }
                }
                if (!userisOnline)
                {
                    Messages.SendLocalMsg("^1Unknown user...");
                    return;
                }

                if (Trackee.Equals(oldUserToLocate))
                {
                    Messages.SendLocalMsg("^1User no longer being tracked: ^8" + Trackee.PlayerName);
                    Trackee.P.SuspectTrail.Clear();
                    Trackee = new Clients();
                    Task taskkk = Task.Factory.StartNew(() => ButtonFactory.RemoveButton(ButtonFactory.btnTracker2));
                }
                else if (userisOnline)
                {
                    Messages.SendLocalMsg("^2Tracking: ^8" + Trackee.PlayerName);
                    Trackee.P.SuspectTimeout = 150;
                    Trackee.P.SuspectTrail.Clear();
                }
            }
            catch (Exception e) { Program.ConsError(e.ToString()); }
        }

        static public Clients ScanCars()
        {
            List<Clients> temp = new List<Clients>(Program.AllConnections);
            Clients ScannedCar = new Clients();
            foreach (Clients all in temp)
            {
                if (all != Identification.MySelf && all.onTrack && !all.C.isCop && all.P.speed > 0)
                {
                    int dst = GetDirectDistance(Identification.MySelf, all);
                    if (dst < 250)
                    {
                        double ang = GetAngele(Identification.MySelf, all);
                        if (ang > 315 || ang < 45)
                        {
                            if (dst < GetDirectDistance(Identification.MySelf, ScannedCar))
                            {
                                Statistics.GetRoadName(all);
                                ScannedCar = all;
                            }
                        }
                    }
                }
            }
            TrackCar(ScannedCar);
            return ScannedCar;
        }

        static public Clients ScanCarsBehind()
        {
            List<Clients> temp = new List<Clients>(Program.AllConnections);
            Clients ScannedCarBackwards = new Clients();
            foreach (Clients all in temp)
            {
                if (all != Identification.MySelf && all != Trackee && all.onTrack && !all.C.isCop && all.P.speed > 0)
                {
                    int dst = GetDirectDistance(Identification.MySelf, all);
                    if (dst < 100)
                    {
                        double ang = GetAngele(Identification.MySelf, all);
                        if (ang > 90 && ang < 270)
                        {
                            double dir = Identification.MySelf.P.heading / 182 - all.P.heading / 182;
                            if (dir < 0.0)
                            {
                                dir = 360.0 + dir;
                            }
                            if (dst < GetDirectDistance(Identification.MySelf, ScannedCarBackwards) && (dir < 90 || dir > 270))
                            {
                                ScannedCarBackwards = all;
                            }
                        }
                    }
                }
            }
            return ScannedCarBackwards;
        }

        static public void TrackCar(Clients userToTrack)
        {
            if (userToTrack.onTrack)
            {
                GetTrackCarDirection(userToTrack);
                string speed = userToTrack.P.speed + (Settings.UNITKPH ? "km/h" : "mph");
                userToTrack.P.SusInfo = userToTrack.PlayerName + "^8 - [" + userToTrack.P.CarName + "] - [" + speed + " / " + GetDirectDistance(Identification.MySelf, userToTrack) + "m] - [^3" + userToTrack.P.Location + "^8]";
            }
            else
            {
                userToTrack.P.SusInfo = userToTrack.PlayerName + "^8 - [^1speccing^8]";
                userToTrack.P.dirName = "[^1speccing^8]";
            }
        }

        #region Air-line dst between cars
        static public int GetDirectDistance(Clients FirstCar, Clients SecondCar)
        {
            return (int)Math.Sqrt(Math.Pow(SecondCar.P.x - FirstCar.P.x, 2.0) + Math.Pow(SecondCar.P.y - FirstCar.P.y, 2.0));
        }

        static public float GetDirectDistanceDecimal(Clients FirstCar, Clients SecondCar)
        {
            return (float)Math.Sqrt(Math.Pow(SecondCar.P.x - FirstCar.P.x, 2.0) + Math.Pow(SecondCar.P.y - FirstCar.P.y, 2.0));
        }
        #endregion

        #region CarDirection relative to track north-south and east-west
        static public void GetTrackCarDirection(Clients user)
        {
            int dir = user.P.direction;

            if (user.P.speed > 1)
            {
                if (Program.CurrentTrack.StartsWith("BL") || (Program.CurrentTrack.StartsWith("FE")) || (Program.CurrentTrack.StartsWith("AS")) || (Program.CurrentTrack.StartsWith("SO")) || (Program.CurrentTrack.StartsWith("WE")) || Program.CurrentTrack.StartsWith("KY") || Program.CurrentTrack.StartsWith("RO") || Program.CurrentTrack.StartsWith("AU"))
                {
                    if (dir >= 0 && dir < 4096 || dir >= 61440 && dir <= 65536)
                    {
                        user.P.dirName = "North";
                        user.P.dirArrow = UnicodeEncoding.N;
                    }
                    if (dir >= 4096 && dir < 12288)
                    {
                        user.P.dirName = "North-West";
                        user.P.dirArrow = UnicodeEncoding.NV;
                    }
                    if (dir >= 12288 && dir < 20480)
                    {
                        user.P.dirName = "West";
                        user.P.dirArrow = UnicodeEncoding.V;
                    }
                    if (dir >= 20480 && dir < 28672)
                    {
                        user.P.dirName = "South-West";
                        user.P.dirArrow = UnicodeEncoding.SV;
                    }
                    if (dir >= 28672 && dir < 36864)
                    {
                        user.P.dirName = "South";
                        user.P.dirArrow = UnicodeEncoding.S;
                    }
                    if (dir >= 36864 && dir < 45056)
                    {
                        user.P.dirName = "South-East";
                        user.P.dirArrow = UnicodeEncoding.SØ;
                    }
                    if (dir >= 45056 && dir < 53248)
                    {
                        user.P.dirName = "East";
                        user.P.dirArrow = UnicodeEncoding.Ø;
                    }
                    if (dir >= 53248 && dir < 61440)
                    {
                        user.P.dirName = "North-East";
                        user.P.dirArrow = UnicodeEncoding.NØ;
                    }
                }
                else
                    user.P.dirName = "track not supported";

            }
            else
            {
                user.P.dirName = "standstill";
                user.P.dirArrow = "‡";
            }
        }
        #endregion

        #region Angele between two cars
        static public double GetAngele(Clients FirstCar, Clients SecondCar)
        {
            double ang = (Math.Atan2((SecondCar.P.x - FirstCar.P.x), (SecondCar.P.y - FirstCar.P.y)) * 180.0) / 3.1415926535897931;
            if (ang < 0.0)
            {
                ang = 360.0 + ang;
            }
            double dir = ang + FirstCar.P.heading / 182;
            if (dir > 360.0)
            {
                dir -= 360.0;
            }
            return Math.Round(dir, 0);
        }
        #endregion

        #region CarDirection relative to my car
        static public string TrackeeDirectionRelativeToMe(Clients FirstCar, Clients SecondCar)
        {
            string retning = "";
            double dir = FirstCar.P.heading / 182 - SecondCar.P.heading / 182;
            if (dir < 0.0)
            {
                dir = 360.0 + dir;
            }
            //nanana
            if (dir >= 337.5 || dir >= 0 && dir < 22.5)
            {
                //nord
                retning = UnicodeEncoding.N;
            }
            else if (dir >= 22.5 && dir < 67.5)
            {
                //nordøst
                retning = UnicodeEncoding.NØ;
            }
            else if (dir >= 67.5 && dir < 112.5)
            {
                //øst
                retning = UnicodeEncoding.Ø;
            }
            else if (dir >= 112.5 && dir < 157.5)
            {
                // sørøst
                retning = UnicodeEncoding.SØ;
            }
            else if (dir >= 157.5 && dir < 202.5)
            {
                // sør
                retning = UnicodeEncoding.S;
            }
            else if (dir >= 202.5 && dir < 247.5)
            {
                // sørvest
                retning = UnicodeEncoding.SV;
            }
            else if (dir >= 247.5 && dir < 292.5)
            {
                // vest
                retning = UnicodeEncoding.V;
            }
            else if (dir >= 292.5 && dir < 337.5)
            {
                // nordvest
                retning = UnicodeEncoding.NV;
            }
            if (!Trackee.onTrack)
            {
                retning = "^1#";
            }
            return retning;
        }
        #endregion

        #region Returns correct arrow for angel between two cars
        static public string GetDirectDirection(double dir)
        {
            string retning = "";

            if (dir >= 337.5 || dir >= 0 && dir < 22.5)
            {
                //nord
                retning = UnicodeEncoding.N;
            }
            else if (dir >= 22.5 && dir < 67.5)
            {
                //nordøst
                retning = UnicodeEncoding.NØ;
            }
            else if (dir >= 67.5 && dir < 112.5)
            {
                //øst
                retning = UnicodeEncoding.Ø;
            }
            else if (dir >= 112.5 && dir < 157.5)
            {
                // sørøst
                retning = UnicodeEncoding.SØ;
            }
            else if (dir >= 157.5 && dir < 202.5)
            {
                // sør
                retning = UnicodeEncoding.S;
            }
            else if (dir >= 202.5 && dir < 247.5)
            {
                // sørvest
                retning = UnicodeEncoding.SV;
            }
            else if (dir >= 247.5 && dir < 292.5)
            {
                // vest
                retning = UnicodeEncoding.V;
            }
            else if (dir >= 292.5 && dir < 337.5)
            {
                // nordvest
                retning = UnicodeEncoding.NV;
            }
            if (!Trackee.onTrack)
            {
                retning = "^1#";
            }
            return retning;
        }
        #endregion

        static public bool SLOWDOWNmsg = false;
        static public string SpeedDiffrence(Clients FirstCar, Clients SecondCar)
        {
            int mySpeed = Settings.UNITKPH ? FirstCar.P.speed : (int)(FirstCar.P.speed * 1.6);
            int susSpeed = Settings.UNITKPH ? SecondCar.P.speed : (int)(SecondCar.P.speed * 1.6);
            int speedDiff = susSpeed - mySpeed;

            string colour;
            if (susSpeed > mySpeed) { colour = "^2"; SLOWDOWNmsg = false; }
            else if (susSpeed - mySpeed < -40 && GetDirectDistance(FirstCar, SecondCar) < 200) { colour = "^1"; SLOWDOWNmsg = true; }
            else { colour = "^6"; SLOWDOWNmsg = false; }
            return colour + speedDiff;
        }

        #region 0-100 checker
        static public bool isMoving = false;
        static public void TimeNullToHundred(Clients conn)
        {
            int currSpeed = conn.P.speed;
            if (currSpeed > 0 && !isMoving)
            {
                isMoving = true;
                conn.P.startDrive = DateTime.Now;
            }
            else if (currSpeed == 0)
            {
                isMoving = false;
                conn.P.passedHundred = false;
            }

            if (!conn.P.passedHundred)
            {
                if (Settings.UNITKPH && currSpeed >= 100)
                {
                    conn.P.passedHundred = true;
                    Messages.SendLocalMsg("Accelerated to ^3100 kph ^8in ^3" + Math.Round(DateTime.Now.Subtract(conn.P.startDrive).TotalMilliseconds / 1000, 2) + "s" + " ^8[" + conn.P.CarName + "]");
                }
                else if (!Settings.UNITKPH && currSpeed >= 60)
                {
                    conn.P.passedHundred = true;
                    Messages.SendLocalMsg("Accelerated to ^360 mph ^8in ^3" + Math.Round(DateTime.Now.Subtract(conn.P.startDrive).TotalMilliseconds / 1000, 2) + "s" + " ^8[" + conn.P.CarName + "]");
                }
            }
        }
        #endregion

        static public void TopSpeedChecker()
        {
            Clients me = Identification.MySelf;

            if (me.P.speed > me.P.topSpeed)
            {
                me.P.topSpeed = me.P.speed;
            }
        }

        static public void UpdateTrail(Clients user)
        {
            user.P.SuspectTrail.Add((int)user.P.x + "," + (int)user.P.y);
            if (user.P.SuspectTrail.Count > 20)//new values added every 0.5 sec, if more then 20 entries then delete oldest, cause only 10 second trail
            {
                user.P.SuspectTrail.RemoveAt(0);
            }
        }

        #region Calculate chasebar
        static public void CalculateTrail(Clients cop, Clients suspect, ButtonFactory.Buttons button, bool IamCop)
        {
            //-Upg: Cops in chase now have an additional 200m/60deg zone ahead of the car to prevent loss of contact to suspects in clear sight.
            //Details: Cops in chase must be within a 40m wide x 10 second long trace behind the suspect OR within a 50m (tested, correct value is 90m) radius to the suspect to be considered "in contact".
            //-Chg: Chase-System: When suspect goes offroad, contact radius is increased to 100m.
            int trailDst = 99;
            int closestTrailDst = 500;
            Clients TimeOut = IamCop ? suspect : cop;//motsatt

            if (suspect.P.SuspectTrail.Count > 19 && suspect.onTrack)
            {
                for (int z = 19; z >= 0; z--)
                {
                    float x = Convert.ToSingle(suspect.P.SuspectTrail[z].Split(',')[0]);
                    float y = Convert.ToSingle(suspect.P.SuspectTrail[z].Split(',')[1]);

                    int trailListDist = (int)Math.Sqrt(Math.Pow(x - cop.P.x, 2.0) + Math.Pow(y - cop.P.y, 2.0));
                    if (trailListDist <= 40 && closestTrailDst > trailListDist)
                    {
                        trailDst = z;
                        closestTrailDst = trailListDist;
                        TimeOut.P.SuspectTimeout = 150;
                    }
                }
            }
            else
            { button.Text = "^7Loading..."; return; }

            int realDist = GetDirectDistance(cop, suspect);
            int minTrailRadius = 40;
            int minRadius = 90; if (suspect.P.Location.Contains("OFFROAD")) minRadius = 100;

            string text;
            if (realDist <= 40)
            {
                text = Progressbar.Progress(9, 10, 300, true, false);
                TimeOut.P.SuspectTimeout = 150;
            }
            else if (closestTrailDst < minTrailRadius)
            {
                //Messages.SendLocalMsg("On 10 second trail");
                text = Progressbar.Progress(trailDst, 19, 300, true, false);
                TimeOut.P.SuspectTimeout = 150;
            }
            else if (realDist < minRadius)
            {
                //Messages.SendLocalMsg("Within 90m");
                text = Progressbar.Progress(90 - realDist, minRadius, 300, true, false);
                TimeOut.P.SuspectTimeout = 150;
            }
            else if (cop.P.Location == suspect.P.Location && suspect.P.Location.StartsWith("^"))
            {
                //Messages.SendLocalMsg("Same zone");
                text = Progressbar.Progress(0, 19, 300, true, false);
                TimeOut.P.SuspectTimeout = 150;
            }
            else
            {
                TimeOut.P.SuspectTimeout--;
                text = "^7Cooldown: " + TimeOut.P.SuspectTimeout / 10;
            }
            //if (realDist < minRadius && closestTrailDst < minTrailRadius && realDist > 70 && suspect.P.speed > 40)
            //{
            //suspect.P.SuspectTimeout = 150;
            //int trailNum = (int)Math.Round((double)(100 * (19 - trailDst)) / 19);
            //int directNum = (int)Math.Round((double)(100 * realDist) / minRadius);
            //text = Progressbar.Progress(100 - ((trailNum + directNum) / 2), 90, 300, true, false);
            //Messages.SendLocalMsg("Trailpercent: " + trailNum + ", directpercent: " + directNum + ", avrg: " + (trailNum + directNum) / 2);
            //}
            if (SLOWDOWNmsg && IamCop)
            {
                text = "^1SLOW DOWN";
            }
            if (TimeOut.P.SuspectTimeout < 0) { text = "^1lllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllllll"; }
            if (cop.P.joinMode) { text = "Still joinmode..."; }
            button.Text = text;
        }
        #endregion
    }
}