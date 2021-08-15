using InSimDotNet.Out;
using System;

namespace Mary
{
    class Outgauge
    {
        static public OutGauge outgauge = new OutGauge();
        static public void ConnectOutgaugeToLFS()
        {
            outgauge.PacketReceived += Outgauge_PacketReceived;
            //Program.insim.Send(new InSimDotNet.Packets.IS_SMALL { SubT = InSimDotNet.Packets.SmallType.SMALL_SSG, UVal = 100, ReqI = 0 });
            outgauge.Connect("127.0.0.1", 30001);
            Program.Cons("Mary is now listening for OutGauge packets (ip: 127.0.0.1 port: 30001)");
        }

        static public bool heavyAcc = false;
        static public void Outgauge_PacketReceived(object sender, OutGaugeEventArgs e)
        {
            if (Program.buttonsState > 1)
            {
                ButtonFactory.btnSpeedOut.Text = "^" + Settings.BUTTONCOLOURPREF + (Settings.UNITKPH ? (int)(e.Speed * 60 * 60 / 1000) : (int)(e.Speed * 2.2369362920544025));
                ButtonFactory.btnRPMOut2.Text = "^" + Settings.BUTTONCOLOURPREF + (Settings.DIGITALRPM == 0 ? Progressbar.Progress(e.RPM, LibData.GetMaxRPM(e.Car), 20, true, true) : (e.RPM > LibData.GetMaxRPM(e.Car) * 0.95 ? "^1" : "") + Math.Round(e.RPM / 1000, 1).ToString("0.0"));
                ButtonFactory.OpenButton(ButtonFactory.btnSpeedOut);
            }
            if (Identification.MySelf.P.Gear != e.Gear)
            {
                Identification.MySelf.P.Gear = e.Gear;
                UpdateGear();
            }
            Identification.MySelf.P.RPM = e.RPM;
            Identification.MySelf.P.fuel = e.Fuel;

            //ButtonFactory.Coordinates.Text = "" + e.Throttle;//Memory.ReadThrottle();
            //ButtonFactory.OpenButton(ButtonFactory.Coordinates);

            //if (heavyAcc)Memory.WriteThrottle(1);
            //if (Memory.ReadBrake() != 0)
            //{ heavyAcc = false; }
        }
        static public void UpdateGear()
        {
            ButtonFactory.btnGearOut2.Text = "^" + Settings.BUTTONCOLOURPREF + (Identification.MySelf.P.Gear == 0 ? "R" : Identification.MySelf.P.Gear == 1 ? "N" : (Identification.MySelf.P.Gear - 1).ToString());
            if (Program.buttonsState > 1)
                ButtonFactory.OpenButton(ButtonFactory.btnGearUpDownOut);
        }
    }
}