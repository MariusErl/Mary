using InSimDotNet.Out;

namespace Mary
{
    class Outgauge
    {
        static public OutGauge outgauge = new OutGauge();
        static public void ConnectOutgaugeToLFS()
        {
            outgauge.PacketReceived += outgauge_PacketReceived;
            outgauge.Connect("127.0.0.1", 30001);
            Program.Cons(outgauge.IsConnected ? "Outgauge is now connected." : "Outgauge failed to connect. Check your LFS->cfg file.");
        }

        static void outgauge_PacketReceived(object sender, OutGaugeEventArgs e)
        {
            if (Program.buttonsState > 1)
            {
                ButtonFactory.btnSpeedOut.Text = "" + ((Settings.UNITKPH) ? (int)(e.Speed * 60 * 60 / 1000) : (int)(e.Speed * 2.2369362920544025));
                ButtonFactory.OpenButton(ButtonFactory.btnSpeedOut);
            }
            if (Program.MySelf.P.Gear != e.Gear)
            {
                Program.MySelf.P.Gear = e.Gear;
                UpdateGear();
            }
            Program.MySelf.P.RPM = e.RPM;
            Program.MySelf.P.fuel = e.Fuel;
        }
        static public void UpdateGear()
        {
            ButtonFactory.btnGearOut2.Text = Program.MySelf.P.Gear == 0 ? "R" : Program.MySelf.P.Gear == 1 ? "N" : (Program.MySelf.P.Gear - 1).ToString();
            if (Program.buttonsState > 1)
                ButtonFactory.OpenButton(ButtonFactory.btnGearUpDownOut);
        }
    }
}