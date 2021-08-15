using System.Collections.Generic;
namespace Mary
{
    class DefaultMinimap
    {
        static public void ConvertCoordToMinimap(Clients user)
        {
            int presetX = 0;
            int presetY = 0;
            int minimap = 0;
            if (Program.CurrentTrack.StartsWith("BL"))
            {
                presetX = 184 + (int)(user.P.x * 0.028);
                presetY = 44 - (int)(user.P.y * 0.048);
            }
            else if (Program.CurrentTrack.StartsWith("SO"))
            {
                presetX = 179 + (int)(user.P.x * 0.038);
                presetY = 45 - (int)(user.P.y * 0.067);
            }
            else if (Program.CurrentTrack.StartsWith("AS"))
            {
                presetX = 176 + (int)(user.P.x * 0.021);
                presetY = 42 - (int)(user.P.y * 0.037);
            }
            else if (Program.CurrentTrack.StartsWith("WE"))
            {
                presetX = 176 + (int)(user.P.x * 0.018);
                presetY = 40 - (int)(user.P.y * 0.0325);
            }
            else if (Program.CurrentTrack.StartsWith("KY"))
            {
                presetX = 178 + (int)(user.P.x * 0.022);
                presetY = 47 - (int)(user.P.y * 0.038);
            }
            else if (Program.CurrentTrack.StartsWith("FE"))
            {
                presetX = 174 + (int)(user.P.x * 0.0306);
                presetY = 36 - (int)(user.P.y * 0.053);
            }
            else if (Program.CurrentTrack.StartsWith("RO"))
            {
                presetX = 173 + (int)(user.P.x * 0.0455);
                presetY = 39 - (int)(user.P.y * 0.068);
            }
            else if (Program.CurrentTrack.StartsWith("AU"))
            {
                presetX = 177 + (int)(user.P.x * 0.025);
                presetY = 36 - (int)(user.P.y * 0.044);
            }
            int fps;
            if (Settings.FPS == 2 && Settings.MINIMAPLOCOLD == 2) fps = 0;
            else fps = -6;
            if (Settings.MINIMAPLOCOLD == 1) minimap = -156;
            user.P.MiniMapX = presetX + minimap;
            user.P.MiniMapY = presetY + fps;
        }

        static public int[] ReturnPresetMinimapValues()
        {
            int[] xy = new int[] { 100, 100 };
            if (Program.CurrentTrack.StartsWith("BL"))
            {
                xy[0] = 184; xy[1] = 42;
            }
            else if (Program.CurrentTrack.StartsWith("SO"))
            {
                xy[0] = 180; xy[1] = 42;
            }
            return xy;
        }

        static public void UpdateMinimapCops()
        {
            int copBtn = 0;
            ButtonFactory.MinimapTrackerCop2.Text = ""; ButtonFactory.MinimapTrackerCop3.Text = ""; ButtonFactory.MinimapTrackerCop4.Text = "";
            foreach (Clients cop in Identification.MySelf.P.Chase)
            {
                copBtn++;
                ConvertCoordToMinimap(cop);
                Tracker.GetTrackCarDirection(cop);
                if (copBtn == 1)
                {
                    ButtonFactory.MinimapTrackerCop1.LeftRight = (byte)Identification.MySelf.P.Chase[0].P.MiniMapX;
                    ButtonFactory.MinimapTrackerCop1.TopDown = (byte)Identification.MySelf.P.Chase[0].P.MiniMapY;
                    ButtonFactory.MinimapTrackerCop1.Text = (Settings.MINIMAP == 1 || Settings.MINIMAP == 3) ? "^1" + Identification.MySelf.P.Chase[0].P.dirArrow : "";
                    ButtonFactory.MinimapTrackerCop1Info.Text = "^1• ^8" + cop.PlayerName;
                    Tracker.CalculateTrail(cop, Identification.MySelf, ButtonFactory.MinimapTrackerCop1PBar, false);
                    ButtonFactory.OpenButton(ButtonFactory.MinimapTrackerCop1PBar);
                }
                if (copBtn == 2)
                {
                    ButtonFactory.MinimapTrackerCop2.LeftRight = (byte)Identification.MySelf.P.Chase[1].P.MiniMapX;
                    ButtonFactory.MinimapTrackerCop2.TopDown = (byte)Identification.MySelf.P.Chase[1].P.MiniMapY;
                    ButtonFactory.MinimapTrackerCop2.Text = (Settings.MINIMAP == 1 || Settings.MINIMAP == 3) ? "^3" + Identification.MySelf.P.Chase[1].P.dirArrow : "";
                    ButtonFactory.MinimapTrackerCop2Info.Text = "^3• ^8" + Identification.MySelf.P.Chase[1].PlayerName;
                    Tracker.CalculateTrail(cop, Identification.MySelf, ButtonFactory.MinimapTrackerCop2PBar, false);
                    ButtonFactory.OpenButton(ButtonFactory.MinimapTrackerCop2PBar);
                }
                if (copBtn == 3)
                {
                    ButtonFactory.MinimapTrackerCop3.LeftRight = (byte)Identification.MySelf.P.Chase[2].P.MiniMapX;
                    ButtonFactory.MinimapTrackerCop3.TopDown = (byte)Identification.MySelf.P.Chase[2].P.MiniMapY;
                    ButtonFactory.MinimapTrackerCop3.Text = (Settings.MINIMAP == 1 || Settings.MINIMAP == 3) ? "^5" + Identification.MySelf.P.Chase[2].P.dirArrow : "";
                    ButtonFactory.MinimapTrackerCop3Info.Text = "^5• ^8" + Identification.MySelf.P.Chase[2].PlayerName;
                    Tracker.CalculateTrail(cop, Identification.MySelf, ButtonFactory.MinimapTrackerCop3PBar, false);
                    ButtonFactory.OpenButton(ButtonFactory.MinimapTrackerCop3PBar);
                }
                if (copBtn == 4)
                {
                    ButtonFactory.MinimapTrackerCop4.LeftRight = (byte)Identification.MySelf.P.Chase[3].P.MiniMapX;
                    ButtonFactory.MinimapTrackerCop4.TopDown = (byte)Identification.MySelf.P.Chase[3].P.MiniMapY;
                    ButtonFactory.MinimapTrackerCop4.Text = (Settings.MINIMAP == 1 || Settings.MINIMAP == 3) ? "^6" + Identification.MySelf.P.Chase[3].P.dirArrow : "";
                    ButtonFactory.MinimapTrackerCop4Info.Text = "^6• ^8" + Identification.MySelf.P.Chase[3].PlayerName;
                    Tracker.CalculateTrail(cop, Identification.MySelf, ButtonFactory.MinimapTrackerCop4PBar, false);
                    ButtonFactory.OpenButton(ButtonFactory.MinimapTrackerCop4PBar);
                }
            }
            if (!Identification.FULLVERSION)
            {
                ButtonFactory.MinimapTrackerCop1.Text = ButtonFactory.MinimapTrackerCop2.Text = ButtonFactory.MinimapTrackerCop3.Text = ButtonFactory.MinimapTrackerCop4.Text = "";
            }

            ButtonFactory.OpenButton(ButtonFactory.MinimapTrackerCop1);
        }

        static public void UpdateCopsInChase(string Msg)
        {
            if (Msg.Contains(Identification.MySelf.PlayerNameRAW + " has been auto-busted")
                || Msg.StartsWith(Identification.MySelf.PlayerNameRAW + " has been busted")
                || Msg.StartsWith(Identification.MySelf.PlayerNameRAW + " escaped!")
                || Msg.StartsWith(Identification.MySelf.PlayerNameRAW + " paid a ")
                || Msg.StartsWith(Identification.MySelf.PlayerNameRAW + " was fined ") && !Msg.Contains(" for spamming."))
            {
                List<Clients> tempList = new List<Clients>(Identification.MySelf.P.Chase);
                foreach (Clients cop in tempList)
                {
                    Identification.MySelf.P.Chase.Remove(cop);
                    cop.P.SuspectTimeout = 150;
                }
                ButtonFactory.RemoveButton(ButtonFactory.MinimapTrackerCop1PBar);
                ButtonFactory.RemoveButton(ButtonFactory.MinimapTrackerCop2PBar);
                ButtonFactory.RemoveButton(ButtonFactory.MinimapTrackerCop3PBar);
                ButtonFactory.RemoveButton(ButtonFactory.MinimapTrackerCop4PBar);
                return;
            }
            Clients toRemove = new Clients();
            foreach (Clients cop in Identification.MySelf.P.Chase)
                if (Msg.Contains(cop.PlayerNameRAW + " lost contact with suspect.") || Msg.Contains(cop.PlayerNameRAW + " left the chase.") || Msg.Contains(cop.PlayerNameRAW + " Mary remove"))
                {
                    toRemove = cop;
                }
            if (toRemove.Username != null)
            {
                Identification.MySelf.P.Chase.Remove(toRemove);
                toRemove.P.SuspectTimeout = 150;
                ButtonFactory.RemoveButton(ButtonFactory.MinimapTrackerCop1PBar);
                ButtonFactory.RemoveButton(ButtonFactory.MinimapTrackerCop2PBar);
                ButtonFactory.RemoveButton(ButtonFactory.MinimapTrackerCop3PBar);
                ButtonFactory.RemoveButton(ButtonFactory.MinimapTrackerCop4PBar);
            }
        }
    }
}
