using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Globalization;

namespace Mary
{
    class Settings
    {
        static public string PASSWORD = "", KEY = "", PUBSTATKEY = "", WELCOMEMSG = "", CHATKEY = "";

        static public int PORT = 29999, RADARWARNERX = 0, RADARWARNERY = 0;

        static public decimal SHIFTup = 0, SHIFTdown = 0;

        static public byte COPTRACKERMAINleftRight = 94, COPTRACKERMAINupDown = 26, DIGITALSPEEDOleftRight = 87, DIGITALSPEEDOupDown = 187,
            TRIPCOUNTERleftRight = 2, TRIPCOUNTERupDown = 165;

        static public bool CRUISECONTROL = false, SIREN = false, PERFORMANCE = false,
            CARCONTACT = false, COPSCANNER = false, COPHUDON = false, RESQHUDON = false, WELCOMEBUDDY = false, CHECKBUDDY = false,
            EXTRAHUDON = false, NUMPAD = false, CHATLOGGING = false, SKINCHECK = false, SPEEDCHECK = false,
            CHECKADMIN = false, AUTOLOCATE = false, UNITKPH = true, OUTGAUGE = true, SCRLWHEEL = false,
            AFKWARNING = false, RADARWARNER = false, TRIPCOUNTER = false, HIDEAD = false;

        static public List<byte> CPUset = new List<byte>() { 0, 90, 5, 10};
        static public List<byte> RAMset = new List<byte>() { 0, 98, 5, 10 };
        static public List<byte> CRUISECONTROLset = new List<byte>()  {95, 4, 4, 30};
        static public List<byte> SIRENset = new List<byte>() { 67, 4, 4, 28};
        static public List<byte> COPSCANNERset = new List<byte>() { 127, 8, 4, 42 };
        static public List<byte> COPHUDset = new List<byte>() { 67, 8, 4, 10 };
        static public List<byte> RESQUEHUDset = new List<byte>() { 67, 180, 4, 9 };
        static public List<byte> EXTRAHUDset = new List<byte>() { 110, 196, 4, 10 };
        static public List<byte> DIGITALGEARset = new List<byte>() { 30, 192, 5, 10 };
        static public List<byte> DIGITALFUELset = new List<byte>() { 40, 192, 5, 10};
        static public List<byte> DIGITALCLUTCHset = new List<byte>() { 50, 192, 5 ,10};
        static public List<byte> DIGITALENGINEset = new List<byte>() { 60, 192, 5, 10 };
        static public List<string> BUDDYS = new List<string>();

        static public string KEYCRUISESET = "insert", KEYCRUISEUP = "pageup", KEYCRUISEDOWN = "pagedown", KEYPUSHBUTTON = "altgr";


        static public void LoadSettings()
        {
            try
            {
                StreamReader x = new StreamReader(@"settings.txt");
                string line;

                do
                {
                    line = x.ReadLine();
                    if (line != null)
                    {
                        string[] parts = line.Split('=');
                        if (line.Trim().StartsWith("//")) continue;
                        else
                        {
                            switch (parts[0])
                            {
                                case "password":
                                    PASSWORD = parts[1];
                                    break;
                                case "port":
                                    PORT = Convert.ToInt16(parts[1]);
                                    break;
                                case "cruisecontrol":
                                    CRUISECONTROL = (parts[1] == "1") ? true : false;
                                    break;
                                case "siren":
                                    SIREN = (parts[1] == "1") ? true : false;
                                    break;
                                case "performance":
                                    PERFORMANCE = (parts[1] == "1") ? true : false;
                                    break;
                                case "carcontact":
                                    CARCONTACT = (parts[1] == "1") ? true : false;
                                    break;
                                case "copscanner":
                                    COPSCANNER = (parts[1] == "1") ? true : false;
                                    break;
                                case "cophud":
                                    COPHUDON = (parts[1] == "1") ? true : false;
                                    break;
                                case "resqhud":
                                    RESQHUDON = (parts[1] == "1") ? true : false;
                                    break;
                                case "extrahud":
                                    EXTRAHUDON = (parts[1] == "1") ? true : false;
                                    break;
                                case "numpadkeys":
                                    NUMPAD = (parts[1] == "1") ? true : false;
                                    break;
                                case "chatlogging":
                                    CHATLOGGING = (parts[1] == "1") ? true : false;
                                    break;
                                case "skincheck":
                                    SKINCHECK = (parts[1] == "1") ? true : false;
                                    break;
                                case "speedcheck":
                                    SPEEDCHECK = (parts[1] == "1") ? true : false;
                                    break;
                                case "checkadmin":
                                    CHECKADMIN = (parts[1] == "1") ? true : false;
                                    break;
                                case "autolocate":
                                    AUTOLOCATE = (parts[1] == "1") ? true : false;
                                    break;
                                case "unit":
                                    UNITKPH = (parts[1] == "mph") ? false : true;
                                    break;
                                case "outgauge":
                                    OUTGAUGE = (parts[1] == "1") ? true : false;
                                    break;
                                case "welcomebuddy":
                                    WELCOMEBUDDY = (parts[1] == "1") ? true : false;
                                    break;
                                case "checkbuddy":
                                    CHECKBUDDY = (parts[1] == "1") ? true : false;
                                    break;
                                case "scrlwheel":
                                    SCRLWHEEL = (parts[1] == "1") ? true : false;
                                    break;
                                case "afkwarning":
                                    AFKWARNING = (parts[1] == "1") ? true : false;
                                    break;
                                case "radarwarning":
                                    RADARWARNER = (parts[1] == "1") ? true : false;
                                    break;
                                case "tripcounter":
                                    TRIPCOUNTER = (parts[1] == "1") ? true : false;
                                    break;
                                case "hidead":
                                    HIDEAD = (parts[1] == "1") ? true : false;
                                    break;
                                case "welcomemsg":
                                    WELCOMEMSG = parts[1];
                                    break;
                                case "key":
                                    KEY = parts[1];
                                    Program.MyUsername = KEY.Split(',')[0];
                                    break;
                                case "chatKey":
                                    CHATKEY = parts[1];
                                    break;
                                case "keysetcruisecontrolspeed":
                                    KEYCRUISESET = parts[1];
                                    break;
                                case "keyraisecruisecontrolspeed":
                                    KEYCRUISEUP = parts[1];
                                    break;
                                case "keylowercruisecontrolspeed":
                                    KEYCRUISEDOWN = parts[1];
                                    break;
                                case "keypushbutton":
                                    KEYPUSHBUTTON = parts[1];
                                    break;
                                case "pubstat":
                                    PUBSTATKEY = parts[1];
                                    break;
                                case "buddys":
                                    BUDDYS = parts[1].Split(',').ToList();
                                    break;
                                case "btncpu":
                                    CPUset[0] = Convert.ToByte(parts[1].Split(',')[0]);
                                    CPUset[1] = Convert.ToByte(parts[1].Split(',')[1]);
                                    CPUset[2] = Convert.ToByte(parts[1].Split(',')[2]);
                                    CPUset[3] = Convert.ToByte(parts[1].Split(',')[3]);
                                    break;
                                case "btnram":
                                    RAMset[0] = Convert.ToByte(parts[1].Split(',')[0]);
                                    RAMset[1] = Convert.ToByte(parts[1].Split(',')[1]);
                                    RAMset[2] = Convert.ToByte(parts[1].Split(',')[2]);
                                    RAMset[3] = Convert.ToByte(parts[1].Split(',')[3]);
                                    break;
                                case "btncruisecontrol":
                                    CRUISECONTROLset[0] = Convert.ToByte(parts[1].Split(',')[0]);
                                    CRUISECONTROLset[1] = Convert.ToByte(parts[1].Split(',')[1]);
                                    CRUISECONTROLset[2] = Convert.ToByte(parts[1].Split(',')[2]);
                                    CRUISECONTROLset[3] = Convert.ToByte(parts[1].Split(',')[3]);
                                    break;
                                case "btnsiren":
                                    SIRENset[0] = Convert.ToByte(parts[1].Split(',')[0]);
                                    SIRENset[1] = Convert.ToByte(parts[1].Split(',')[1]);
                                    SIRENset[2] = Convert.ToByte(parts[1].Split(',')[2]);
                                    SIRENset[3] = Convert.ToByte(parts[1].Split(',')[3]);
                                    break;
                                case "btncopscanner":
                                    COPSCANNERset[0] = Convert.ToByte(parts[1].Split(',')[0]);
                                    COPSCANNERset[1] = Convert.ToByte(parts[1].Split(',')[1]);
                                    COPSCANNERset[2] = Convert.ToByte(parts[1].Split(',')[2]);
                                    COPSCANNERset[3] = Convert.ToByte(parts[1].Split(',')[3]);
                                    break;
                                case "btncophud":
                                    COPHUDset[0] = Convert.ToByte(parts[1].Split(',')[0]);
                                    COPHUDset[1] = Convert.ToByte(parts[1].Split(',')[1]);
                                    COPHUDset[2] = Convert.ToByte(parts[1].Split(',')[2]);
                                    COPHUDset[3] = Convert.ToByte(parts[1].Split(',')[3]);
                                    break;
                                case "btnresqhud":
                                    RESQUEHUDset[0] = Convert.ToByte(parts[1].Split(',')[0]);
                                    RESQUEHUDset[1] = Convert.ToByte(parts[1].Split(',')[1]);
                                    RESQUEHUDset[2] = Convert.ToByte(parts[1].Split(',')[2]);
                                    RESQUEHUDset[3] = Convert.ToByte(parts[1].Split(',')[3]);
                                    break;
                                case "btnextrahud":
                                    EXTRAHUDset[0] = Convert.ToByte(parts[1].Split(',')[0]);
                                    EXTRAHUDset[1] = Convert.ToByte(parts[1].Split(',')[1]);
                                    EXTRAHUDset[2] = Convert.ToByte(parts[1].Split(',')[2]);
                                    EXTRAHUDset[3] = Convert.ToByte(parts[1].Split(',')[3]);
                                    break;
                                case "btntrackermain":
                                    COPTRACKERMAINleftRight = Convert.ToByte(parts[1].Split(',')[0]);
                                    COPTRACKERMAINupDown = Convert.ToByte(parts[1].Split(',')[1]);
                                    break;
                                case "btndigitalspeedo":
                                    DIGITALSPEEDOleftRight = Convert.ToByte(parts[1].Split(',')[0]);
                                    DIGITALSPEEDOupDown = Convert.ToByte(parts[1].Split(',')[1]);
                                    break;
                                case "btntripcounter":
                                    TRIPCOUNTERleftRight = Convert.ToByte(parts[1].Split(',')[0]);
                                    TRIPCOUNTERupDown = Convert.ToByte(parts[1].Split(',')[1]);
                                    break;
                                case "btndigitalgear":
                                    DIGITALGEARset[0] = Convert.ToByte(parts[1].Split(',')[0]);
                                    DIGITALGEARset[1] = Convert.ToByte(parts[1].Split(',')[1]);
                                    DIGITALGEARset[2] = Convert.ToByte(parts[1].Split(',')[2]);
                                    DIGITALGEARset[3] = Convert.ToByte(parts[1].Split(',')[3]);
                                    break;
                                case "btndigitalfuel":
                                    DIGITALFUELset[0] = Convert.ToByte(parts[1].Split(',')[0]);
                                    DIGITALFUELset[1] = Convert.ToByte(parts[1].Split(',')[1]);
                                    DIGITALFUELset[2] = Convert.ToByte(parts[1].Split(',')[2]);
                                    DIGITALFUELset[3] = Convert.ToByte(parts[1].Split(',')[3]);
                                    break;
                                case "btndigitalclutch":
                                    DIGITALCLUTCHset[0] = Convert.ToByte(parts[1].Split(',')[0]);
                                    DIGITALCLUTCHset[1] = Convert.ToByte(parts[1].Split(',')[1]);
                                    DIGITALCLUTCHset[2] = Convert.ToByte(parts[1].Split(',')[2]);
                                    DIGITALCLUTCHset[3] = Convert.ToByte(parts[1].Split(',')[3]);
                                    break;
                                case "btndigitalengine":
                                    DIGITALENGINEset[0] = Convert.ToByte(parts[1].Split(',')[0]);
                                    DIGITALENGINEset[1] = Convert.ToByte(parts[1].Split(',')[1]);
                                    DIGITALENGINEset[2] = Convert.ToByte(parts[1].Split(',')[2]);
                                    DIGITALENGINEset[3] = Convert.ToByte(parts[1].Split(',')[3]);
                                    break;
                                case "shiftup":
                                    SHIFTup = decimal.Parse(parts[1], CultureInfo.InvariantCulture);
                                    break;
                                case "shiftdown":
                                    SHIFTdown = decimal.Parse(parts[1], CultureInfo.InvariantCulture);
                                    break;
                                case "cophudbtn1function":
                                    COPHUD1 = parts[1].Split('%').ToArray();
                                    break;
                                case "cophudbtn2function":
                                    COPHUD2 = parts[1].Split('%').ToArray();
                                    break;
                                case "cophudbtn3function":
                                    COPHUD3 = parts[1].Split('%').ToArray();
                                    break;
                                case "cophudbtn4function":
                                    COPHUD4 = parts[1].Split('%').ToArray();
                                    break;
                                case "cophudbtn5function":
                                    COPHUD5 = parts[1].Split('%').ToArray();
                                    break;
                                case "cophudbtn6function":
                                    COPHUD6 = parts[1].Split('%').ToArray();
                                    break;
                                case "cophudbtn7function":
                                    COPHUD7 = parts[1].Split('%').ToArray();
                                    break;
                                case "cophudbtn8function":
                                    COPHUD8 = parts[1].Split('%').ToArray();
                                    break;
                                case "cophudbtn9function":
                                    COPHUD9 = parts[1].Split('%').ToArray();
                                    break;
                                case "cophudbtn10function":
                                    COPHUD10 = parts[1].Split('%').ToArray();
                                    break;
                                case "cophudbtn11function":
                                    COPHUD11 = parts[1].Split('%').ToArray();
                                    break;
                                case "cophudbtn12function":
                                    COPHUD12 = parts[1].Split('%').ToArray();
                                    break;
                                case "resqhudbtn1function":
                                    RESQHUD1 = parts[1].Split('%').ToArray();
                                    break;
                                case "resqhudbtn2function":
                                    RESQHUD2 = parts[1].Split('%').ToArray();
                                    break;
                                case "resqhudbtn3function":
                                    RESQHUD3 = parts[1].Split('%').ToArray();
                                    break;
                                case "resqhudbtn4function":
                                    RESQHUD4 = parts[1].Split('%').ToArray();
                                    break;
                                case "resqhudbtn5function":
                                    RESQHUD5 = parts[1].Split('%').ToArray();
                                    break;
                                case "resqhudbtn6function":
                                    RESQHUD6 = parts[1].Split('%').ToArray();
                                    break;
                                case "resqhudbtn7function":
                                    RESQHUD7 = parts[1].Split('%').ToArray();
                                    break;
                                case "extrahudbtn1function":
                                    EXTRAHUD1 = parts[1].Split('%').ToArray();
                                    break;
                                case "extrahudbtn2function":
                                    EXTRAHUD2 = parts[1].Split('%').ToArray();
                                    break;
                                case "extrahudbtn3function":
                                    EXTRAHUD3 = parts[1].Split('%').ToArray();
                                    break;
                                case "extrahudbtn4function":
                                    EXTRAHUD4 = parts[1].Split('%').ToArray();
                                    break;
                                case "extrahudbtn5function":
                                    EXTRAHUD5 = parts[1].Split('%').ToArray();
                                    break;
                                case "cophudbtn1name":
                                    COPHUD1NAME = parts[1];
                                    break;
                                case "cophudbtn2name":
                                    COPHUD2NAME = parts[1];
                                    break;
                                case "cophudbtn3name":
                                    COPHUD3NAME = parts[1];
                                    break;
                                case "cophudbtn4name":
                                    COPHUD4NAME = parts[1];
                                    break;
                                case "cophudbtn5name":
                                    COPHUD5NAME = parts[1];
                                    break;
                                case "cophudbtn6name":
                                    COPHUD6NAME = parts[1];
                                    break;
                                case "cophudbtn7name":
                                    COPHUD7NAME = parts[1];
                                    break;
                                case "cophudbtn8name":
                                    COPHUD8NAME = parts[1];
                                    break;
                                case "cophudbtn9name":
                                    COPHUD9NAME = parts[1];
                                    break;
                                case "cophudbtn10name":
                                    COPHUD10NAME = parts[1];
                                    break;
                                case "cophudbtn11name":
                                    COPHUD11NAME = parts[1];
                                    break;
                                case "cophudbtn12name":
                                    COPHUD12NAME = parts[1];
                                    break;
                                case "resqhudbtn1name":
                                    RESQ1NAME = parts[1];
                                    break;
                                case "resqhudbtn2name":
                                    RESQ2NAME = parts[1];
                                    break;
                                case "resqhudbtn3name":
                                    RESQ3NAME = parts[1];
                                    break;
                                case "resqhudbtn4name":
                                    RESQ4NAME = parts[1];
                                    break;
                                case "resqhudbtn5name":
                                    RESQ5NAME = parts[1];
                                    break;
                                case "resqhudbtn6name":
                                    RESQ6NAME = parts[1];
                                    break;
                                case "resqhudbtn7name":
                                    RESQ7NAME = parts[1];
                                    break;
                                case "extrabtn1name":
                                    EXTRAHUD1NAME = parts[1];
                                    break;
                                case "extrabtn2name":
                                    EXTRAHUD2NAME = parts[1];
                                    break;
                                case "extrabtn3name":
                                    EXTRAHUD3NAME = parts[1];
                                    break;
                                case "extrabtn4name":
                                    EXTRAHUD4NAME = parts[1];
                                    break;
                                case "extrabtn5name":
                                    EXTRAHUD5NAME = parts[1];
                                    break;
                                case "num0":
                                    NUM0 = parts[1].Split('%').ToArray();
                                    break;
                                case "num1":
                                    NUM1 = parts[1].Split('%').ToArray();
                                    break;
                                case "num2":
                                    NUM2 = parts[1].Split('%').ToArray();
                                    break;
                                case "num3":
                                    NUM3 = parts[1].Split('%').ToArray();
                                    break;
                                case "num4":
                                    NUM4 = parts[1].Split('%').ToArray();
                                    break;
                                case "num5":
                                    NUM5 = parts[1].Split('%').ToArray();
                                    break;
                                case "num6":
                                    NUM6 = parts[1].Split('%').ToArray();
                                    break;
                                case "num7":
                                    NUM7 = parts[1].Split('%').ToArray();
                                    break;
                                case "num8":
                                    NUM8 = parts[1].Split('%').ToArray();
                                    break;
                                case "num9":
                                    NUM9 = parts[1].Split('%').ToArray();
                                    break;
                                case "num/":
                                    NUMDelt = parts[1].Split('%').ToArray();
                                    break;
                                case "num*":
                                    NUMGanger = parts[1].Split('%').ToArray();
                                    break;
                                case "num+":
                                    NUMPluss = parts[1].Split('%').ToArray();
                                    break;
                                case "num,":
                                    NUMComma = parts[1].Split('%').ToArray();
                                    break;
                            }
                        }
                    }
                }
                while (line != null);
                {
                    x.Close();
                }
                CruiseControl.KeyCruiseControlSet = CruiseControl.DecideKeys(KEYCRUISESET);
                CruiseControl.KeyCruiseControlUp = CruiseControl.DecideKeys(KEYCRUISEUP);
                CruiseControl.KeyCruiseControlDown = CruiseControl.DecideKeys(KEYCRUISEDOWN);
                CruiseControl.KeyPushButton = CruiseControl.DecideKeys(KEYPUSHBUTTON);

                ButtonFactory.LoadButtonHudNames();
            }
            catch (Exception e) { Program.ConsError(e.ToString()); }
        }

        static public string COPHUD1NAME = "";
        static public string COPHUD2NAME = "";
        static public string COPHUD3NAME = "";
        static public string COPHUD4NAME = "";
        static public string COPHUD5NAME = "";
        static public string COPHUD6NAME = "";
        static public string COPHUD7NAME = "";
        static public string COPHUD8NAME = "";
        static public string COPHUD9NAME = "";
        static public string COPHUD10NAME = "";
        static public string COPHUD11NAME = "";
        static public string COPHUD12NAME = "";

        static public string[] COPHUD1 = new string[] { "" };
        static public string[] COPHUD2 = new string[] { "" };
        static public string[] COPHUD3 = new string[] { "" };
        static public string[] COPHUD4 = new string[] { "" };
        static public string[] COPHUD5 = new string[] { "" };
        static public string[] COPHUD6 = new string[] { "" };
        static public string[] COPHUD7 = new string[] { "" };
        static public string[] COPHUD8 = new string[] { "" };
        static public string[] COPHUD9 = new string[] { "" };
        static public string[] COPHUD10 = new string[] { "" };
        static public string[] COPHUD11 = new string[] { "" };
        static public string[] COPHUD12 = new string[] { "" };

        static public string RESQ1NAME = "";
        static public string RESQ2NAME = "";
        static public string RESQ3NAME = "";
        static public string RESQ4NAME = "";
        static public string RESQ5NAME = "";
        static public string RESQ6NAME = "";
        static public string RESQ7NAME = "";

        static public string[] RESQHUD1 = new string[] { "" };
        static public string[] RESQHUD2 = new string[] { "" };
        static public string[] RESQHUD3 = new string[] { "" };
        static public string[] RESQHUD4 = new string[] { "" };
        static public string[] RESQHUD5 = new string[] { "" };
        static public string[] RESQHUD6 = new string[] { "" };
        static public string[] RESQHUD7 = new string[] { "" };

        static public string EXTRAHUD1NAME = "";
        static public string EXTRAHUD2NAME = "";
        static public string EXTRAHUD3NAME = "";
        static public string EXTRAHUD4NAME = "";
        static public string EXTRAHUD5NAME = "";

        static public string[] EXTRAHUD1 = new string[] { "" };
        static public string[] EXTRAHUD2 = new string[] { "" };
        static public string[] EXTRAHUD3 = new string[] { "" };
        static public string[] EXTRAHUD4 = new string[] { "" };
        static public string[] EXTRAHUD5 = new string[] { "" };

        static public string[] NUM0 = new string[] { "" };
        static public string[] NUM1 = new string[] { "" };
        static public string[] NUM2 = new string[] { "" };
        static public string[] NUM3 = new string[] { "" };
        static public string[] NUM4 = new string[] { "" };
        static public string[] NUM5 = new string[] { "" };
        static public string[] NUM6 = new string[] { "" };
        static public string[] NUM7 = new string[] { "" };
        static public string[] NUM8 = new string[] { "" };
        static public string[] NUM9 = new string[] { "" };
        static public string[] NUMDelt = new string[] { "" };
        static public string[] NUMGanger = new string[] { "" };
        static public string[] NUMPluss = new string[] { "" };
        static public string[] NUMComma = new string[] { "" };
    }
}