using System;

namespace Mary
{
    class Tracker
    {
        static public Clients Trackee = new Clients();

        static public void EditUserToTrack(string input)
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
            }

            if (Trackee.Equals(oldUserToLocate))
            {
                Messages.SendLocalMsg("^1User no longer being tracked: ^8" + Trackee.PlayerName);
                Trackee = new Clients();
                ButtonFactory.RemoveButton(ButtonFactory.btnTracker2);
                if (Messages.resQonMission) Messages.resQonMission = false;
            }
            else if (Trackee.Username.Length > 1 && userisOnline)
            {
                Messages.SendLocalMsg("^2Tracking: ^8" + Trackee.PlayerName);
            }
        }

        static public Clients ScanCars()
        {
            Clients ScannedCar = new Clients();
            foreach (Clients all in Program.AllConnections)
            {
                if (all != Program.MySelf && all.onTrack && !all.C.isCop && Program.CarSpeed(all.P.speed, true) > 0)
                {
                    int dst = GetDirectDistance(Program.MySelf, all);
                    if (dst < 250)
                    {
                        double ang = GetAngel(Program.MySelf, all);
                        if (ang > 315 || ang < 45)
                        {
                            if (dst < GetDirectDistance(Program.MySelf, ScannedCar))
                            {
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
            double dir = 0;
            double ang = 0;
            Clients ScannedCarBackwards = new Clients();
            foreach (Clients all in Program.AllConnections)
            {
                if (all != Program.MySelf && all != Trackee && all.onTrack && !all.C.isCop && Program.CarSpeed(all.P.speed, true) > 0)
                {
                    int dst = GetDirectDistance(Program.MySelf, all);
                    if (dst < 100)
                    {
                        ang = GetAngel(Program.MySelf, all);
                        if (ang > 90 && ang < 270)
                        {
                            dir = Program.MySelf.P.heading / 182 - all.P.heading / 182;
                            if (dir < 0.0)
                            {
                                dir = 360.0 + dir;
                            }
                            if (dst < GetDirectDistance(Program.MySelf, ScannedCarBackwards) && (dir < 90 || dir > 270))
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
                    string speed = (Settings.UNITKPH) ? Program.CarSpeed(userToTrack.P.speed, true) + "km/h" : Program.CarSpeed(userToTrack.P.speed, false) + "mph";
                    userToTrack.P.SusInfo = userToTrack.PlayerName + "^8 - [" + userToTrack.P.CarName + "] - [" + speed + " / " + GetDirectDistance(Program.MySelf, userToTrack) + "m] - [" + userToTrack.P.dirName + "]";
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
            return (int)Math.Sqrt(Math.Pow(SecondCar.P.x / 65536 - FirstCar.P.x / 65536, 2.0) + Math.Pow(SecondCar.P.y / 65536 - FirstCar.P.y / 65536, 2.0));
        }
        #endregion

        #region CarDirection relative to track north-south and east-west
        static public void GetTrackCarDirection(Clients user)
        {
            int dir = user.P.direction;

            if (user.P.speed > 0)
            {
                switch (Program.CurrentTrack)
                {
                    case "FE1X":
                    case "AS5X":
                    case "SO4X":
                    case "BL1X":
                        if (dir >= 0 && dir < 4096 || dir >= 61440 && dir <= 65536)
                        {
                            user.P.dirName = "North";
                        }
                        if (dir >= 4096 && dir < 12288)
                        {
                            user.P.dirName = "North-West";
                        }
                        if (dir >= 12288 && dir < 20480)
                        {
                            user.P.dirName = "West";
                        }
                        if (dir >= 20480 && dir < 28672)
                        {
                            user.P.dirName = "South-West";
                        }
                        if (dir >= 28672 && dir < 36864)
                        {
                            user.P.dirName = "South";
                        }
                        if (dir >= 36864 && dir < 45056)
                        {
                            user.P.dirName = "South-East";
                        }
                        if (dir >= 45056 && dir < 53248)
                        {
                            user.P.dirName = "East";
                        }
                        if (dir >= 53248 && dir < 61440)
                        {
                            user.P.dirName = "North-East";
                        }
                        break;
                    case "KY3X":
                    case "WE1X":
                        if (dir >= 0 && dir < 4096 || dir >= 61440 && dir <= 65536)
                        {
                            user.P.dirName = "South";
                        }
                        if (dir >= 4096 && dir < 12288)
                        {
                            user.P.dirName = "South-East";
                        }
                        if (dir >= 12288 && dir < 20480)
                        {
                            user.P.dirName = "East";
                        }
                        if (dir >= 20480 && dir < 28672)
                        {
                            user.P.dirName = "North-East";
                        }
                        if (dir >= 28672 && dir < 36864)
                        {
                            user.P.dirName = "North";
                        }
                        if (dir >= 36864 && dir < 45056)
                        {
                            user.P.dirName = "North-West";
                        }
                        if (dir >= 45056 && dir < 53248)
                        {
                            user.P.dirName = "West";
                        }
                        if (dir >= 53248 && dir < 61440)
                        {
                            user.P.dirName = "South-West";
                        }
                        break;

                    default:
                        user.P.dirName = "track not supported";
                        break;
                }
            }
            else
            {
                user.P.dirName = "standstill";
                user.P.dirArrow = "‡";
            }
        }
        #endregion

        #region Angel between two cars
        static public double GetAngel(Clients FirstCar, Clients SecondCar)
        {
            double ang = (Math.Atan2((SecondCar.P.x / 65536 - FirstCar.P.x / 65536), (SecondCar.P.y / 65536 - FirstCar.P.y / 65536)) * 180.0) / 3.1415926535897931;
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
                retning = "^H¡ô";
            }
            else if (dir >= 22.5 && dir < 67.5)
            {
                //nordøst
                retning = "^H¡ù";
            }
            else if (dir >= 67.5 && dir < 112.5)
            {
                //øst
                retning = "^H¡÷";
            }
            else if (dir >= 112.5 && dir < 157.5)
            {
                // sørøst
                retning = "^H¡û";
            }
            else if (dir >= 157.5 && dir < 202.5)
            {
                // sør
                retning = "^H¡õ";
            }
            else if (dir >= 202.5 && dir < 247.5)
            {
                // sørvest
                retning = "^H¡ú";
            }
            else if (dir >= 247.5 && dir < 292.5)
            {
                // vest
                retning = "^H¡ö";
            }
            else if (dir >= 292.5 && dir < 337.5)
            {
                // nordvest
                retning = "^H¡ø";
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
                retning = "^H¡ô";
            }
            else if (dir >= 22.5 && dir < 67.5)
            {
                //nordøst
                retning = "^H¡ù";
            }
            else if (dir >= 67.5 && dir < 112.5)
            {
                //øst
                retning = "^H¡÷";
            }
            else if (dir >= 112.5 && dir < 157.5)
            {
                // sørøst
                retning = "^H¡û";
            }
            else if (dir >= 157.5 && dir < 202.5)
            {
                // sør
                retning = "^H¡õ";
            }
            else if (dir >= 202.5 && dir < 247.5)
            {
                // sørvest
                retning = "^H¡ú";
            }
            else if (dir >= 247.5 && dir < 292.5)
            {
                // vest
                retning = "^H¡ö";
            }
            else if (dir >= 292.5 && dir < 337.5)
            {
                // nordvest
                retning = "^H¡ø";
            }
            if (!Trackee.onTrack)
            {
                retning = "^1#";
            }
            return retning;
        }
        #endregion

        static public string SpeedDiffrence(Clients FirstCar, Clients SecondCar)
        {
            int diff = Settings.UNITKPH ? (Program.CarSpeed(SecondCar.P.speed, true) - Program.CarSpeed(FirstCar.P.speed, true)) : Program.CarSpeed(SecondCar.P.speed, false) - Program.CarSpeed(FirstCar.P.speed, false);
            return (diff < 0) ? "^1" + diff : "^2+" + diff;
        }

        #region 0-100 checker
        static public bool isMoving = false;
        static public void TimeNullToHundred(Clients conn)
        {
            int currSpeed = Program.CarSpeed(conn.P.speed, true);
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

            if (currSpeed >= 100 && !conn.P.passedHundred)
            {
                conn.P.passedHundred = true;
                Messages.SendLocalMsg("Accelerated to ^3" + (Settings.UNITKPH ? "100 km/h" : "60 mph") + " ^8in ^3" + Math.Round(DateTime.Now.Subtract(conn.P.startDrive).TotalMilliseconds / 1000, 2) + "s" + " ^8[" + conn.P.CarName + "]");
            }
        }
        #endregion

        static public void TopSpeedChecker()
        {
            Clients me = Program.MySelf;
            int currSpeed = Program.CarSpeed(me.P.speed, (Settings.UNITKPH ? true : false));

            if (currSpeed > me.P.topSpeed)
            {
                me.P.topSpeed = currSpeed;
            }
        }
    }
}