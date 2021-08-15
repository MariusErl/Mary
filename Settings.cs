using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Mary
{
    class Settings
    {
        static public string PASSWORD = "", KEY = "", PUBSTATKEY = "", WELCOMEMSG = "", CHATKEY = "";

        static public int PORT = 29999, BUTTONCOLOURPREF = 8, DIGITALRPM = 2;

        static public decimal SHIFTup = 0, SHIFTdown = 0, OPACITY = 1;


        static public byte COPTRACKERMAINleftRight = 94, COPTRACKERMAINupDown = 26, DIGITALSPEEDOleftRight = 87, DIGITALSPEEDOupDown = 187,
            TRIPCOUNTERleftRight = 2, TRIPCOUNTERupDown = 165, FPS = 2, MINIMAPLOCOLD = 2, MINIMAP = 2, MINIMAPSIZE = 1;

        static public bool CRUISECONTROL = false, CRUISECONTROLSPEEDLIMIT = false, SIREN = false, PERFORMANCE = false,
            CARCONTACT = false, COPSCANNER = false, COPHUDON = false, RESQHUDON = false, WELCOMEBUDDY = false, CHECKBUDDY = false,
            EXTRAHUDON = false, EXTRAHUDEROLEPENDANTON = false, NUMPAD = false, CHATLOGGING = false, SKINCHECK = false, SPEEDCHECK = false,
            CHECKADMIN = false, AUTOLOCATE = false, UNITKPH = true, OUTGAUGE = true, SCRLWHEEL = false,
            AFKWARNING = false, TRIPCOUNTER = false, HIDEAD = false, DEBUG = false, CARALARM = true, MINIMAPGRID = true;

        static public List<byte> CPUset = new List<byte>() { 0, 90, 5, 10 };
        static public List<byte> RAMset = new List<byte>() { 0, 98, 5, 10 };
        static public List<byte> CRUISECONTROLset = new List<byte>() { 95, 4, 4, 30 };
        static public List<byte> SIRENset = new List<byte>() { 67, 4, 4, 28 };
        static public List<byte> COPSCANNERset = new List<byte>() { 127, 8, 4, 42 };
        static public List<byte> COPHUDset = new List<byte>() { 67, 8, 4, 10 };
        static public List<byte> RESQUEHUDset = new List<byte>() { 67, 180, 4, 9 };
        static public List<byte> EXTRAHUDset = new List<byte>() { 110, 196, 4, 10 };
        static public List<byte> EXTRAHUDROLEPENDANTset = new List<byte>() { 0, 108, 4, 10 };
        static public List<byte> DIGITALGEARset = new List<byte>() { 30, 192, 5, 10 };
        static public List<byte> DIGITALFUELset = new List<byte>() { 40, 192, 5, 10 };
        static public List<byte> DIGITALCLUTCHset = new List<byte>() { 50, 192, 5, 10 };
        static public List<byte> DIGITALENGINEset = new List<byte>() { 60, 192, 5, 10 };
        static public List<byte> DIGITALRPMset = new List<byte>() { 80, 192, 5, 10 };
        static public List<byte> COPBARS = new List<byte>() { 0, 170 };
        static public System.Drawing.Point MINIMAPLOC = new System.Drawing.Point(1269, 215);
        static public List<string> BUDDYS = new List<string>();

        static public string KEYCRUISESET = "insert", KEYCRUISEUP = "pageup", KEYCRUISEDOWN = "pagedown", KEYPUSHBUTTON = "end";


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
                                case "btncolourprefix":
                                    BUTTONCOLOURPREF = Convert.ToInt16(parts[1]);
                                    break;
                                case "digitalrpm":
                                    DIGITALRPM = Convert.ToInt16(parts[1]);
                                    break;
                                case "debug":
                                    DEBUG = (parts[1] == "1");
                                    break;
                                case "cruisecontrol":
                                    CRUISECONTROL = (parts[1] == "1");
                                    break;
                                case "cruisecontrolspeedlimit":
                                    CRUISECONTROLSPEEDLIMIT = (parts[1] == "1");
                                    break;
                                case "siren":
                                    SIREN = (parts[1] == "1");
                                    break;
                                case "performance":
                                    PERFORMANCE = (parts[1] == "1");
                                    break;
                                case "carcontact":
                                    CARCONTACT = (parts[1] == "1");
                                    break;
                                case "copscanner":
                                    COPSCANNER = (parts[1] == "1");
                                    break;
                                case "cophud":
                                    COPHUDON = (parts[1] == "1");
                                    break;
                                case "resqhud":
                                    RESQHUDON = (parts[1] == "1");
                                    break;
                                case "extrahud":
                                    EXTRAHUDON = (parts[1] == "1");
                                    break;
                                case "extrahudrolependant":
                                    EXTRAHUDEROLEPENDANTON = (parts[1] == "1");
                                    break;
                                case "numpadkeys":
                                    NUMPAD = (parts[1] == "1");
                                    break;
                                case "chatlogging":
                                    CHATLOGGING = (parts[1] == "1");
                                    break;
                                case "skincheck":
                                    SKINCHECK = (parts[1] == "1");
                                    break;
                                case "speedcheck":
                                    SPEEDCHECK = (parts[1] == "1");
                                    break;
                                case "checkadmin":
                                    CHECKADMIN = (parts[1] == "1");
                                    break;
                                case "autolocate":
                                    AUTOLOCATE = (parts[1] == "1");
                                    break;
                                case "unit":
                                    UNITKPH = parts[1] != "mph";
                                    break;
                                case "outgauge":
                                    OUTGAUGE = (parts[1] == "1");
                                    break;
                                case "welcomebuddy":
                                    WELCOMEBUDDY = (parts[1] == "1");
                                    break;
                                case "checkbuddy":
                                    CHECKBUDDY = (parts[1] == "1");
                                    break;
                                case "scrlwheel":
                                    SCRLWHEEL = (parts[1] == "1");
                                    break;
                                case "afkwarning":
                                    AFKWARNING = (parts[1] == "1");
                                    break;
                                case "tripcounter":
                                    TRIPCOUNTER = (parts[1] == "1");
                                    break;
                                case "hidead":
                                    HIDEAD = (parts[1] == "1");
                                    break;
                                case "caralarm":
                                    CARALARM = (parts[1] == "1");
                                    break;
                                case "minimapgrid":
                                    MINIMAPGRID = (parts[1] == "1");
                                    break;
                                case "welcomemsg":
                                    WELCOMEMSG = parts[1];
                                    break;
                                case "key":
                                    KEY = parts[1];
                                    Identification.MyUsername = KEY.Split(',')[0];
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
                                case "btnextrahudrolependant":
                                    EXTRAHUDROLEPENDANTset[0] = Convert.ToByte(parts[1].Split(',')[0]);
                                    EXTRAHUDROLEPENDANTset[1] = Convert.ToByte(parts[1].Split(',')[1]);
                                    EXTRAHUDROLEPENDANTset[2] = Convert.ToByte(parts[1].Split(',')[2]);
                                    EXTRAHUDROLEPENDANTset[3] = Convert.ToByte(parts[1].Split(',')[3]);
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
                                case "btndigitalrpm":
                                    DIGITALRPMset[0] = Convert.ToByte(parts[1].Split(',')[0]);
                                    DIGITALRPMset[1] = Convert.ToByte(parts[1].Split(',')[1]);
                                    DIGITALRPMset[2] = Convert.ToByte(parts[1].Split(',')[2]);
                                    DIGITALRPMset[3] = Convert.ToByte(parts[1].Split(',')[3]);
                                    break;
                                case "btncopbars":
                                    COPBARS[0] = Convert.ToByte(parts[1].Split(',')[0]);
                                    COPBARS[1] = Convert.ToByte(parts[1].Split(',')[1]);
                                    break;
                                case "minimapNew":
                                    MINIMAP = Convert.ToByte(parts[1].Split(',')[0]);
                                    break;
                                case "minimapLoc":
                                    MINIMAPLOC = new System.Drawing.Point(Convert.ToInt16(parts[1].Split(',')[0]), Convert.ToInt16(parts[1].Split(',')[1]));
                                    OPACITY = decimal.Parse(parts[1].Split(',')[2], CultureInfo.InvariantCulture);
                                    MINIMAPSIZE = Convert.ToByte(parts[1].Split(',')[3]);
                                    break;
                                case "shiftup":
                                    SHIFTup = decimal.Parse(parts[1], CultureInfo.InvariantCulture);
                                    break;
                                case "shiftdown":
                                    SHIFTdown = decimal.Parse(parts[1], CultureInfo.InvariantCulture);
                                    break;
                                case "cophudbtn1function":
                                    COPHUD1 = parts[1];
                                    break;
                                case "cophudbtn2function":
                                    COPHUD2 = parts[1];
                                    break;
                                case "cophudbtn3function":
                                    COPHUD3 = parts[1];
                                    break;
                                case "cophudbtn4function":
                                    COPHUD4 = parts[1];
                                    break;
                                case "cophudbtn5function":
                                    COPHUD5 = parts[1];
                                    break;
                                case "cophudbtn6function":
                                    COPHUD6 = parts[1];
                                    break;
                                case "cophudbtn7function":
                                    COPHUD7 = parts[1];
                                    break;
                                case "cophudbtn8function":
                                    COPHUD8 = parts[1];
                                    break;
                                case "cophudbtn9function":
                                    COPHUD9 = parts[1];
                                    break;
                                case "cophudbtn10function":
                                    COPHUD10 = parts[1];
                                    break;
                                case "cophudbtn11function":
                                    COPHUD11 = parts[1];
                                    break;
                                case "cophudbtn12function":
                                    COPHUD12 = parts[1];
                                    break;
                                case "resqhudbtn1function":
                                    RESQHUD1 = parts[1];
                                    break;
                                case "resqhudbtn2function":
                                    RESQHUD2 = parts[1];
                                    break;
                                case "resqhudbtn3function":
                                    RESQHUD3 = parts[1];
                                    break;
                                case "resqhudbtn4function":
                                    RESQHUD4 = parts[1];
                                    break;
                                case "resqhudbtn5function":
                                    RESQHUD5 = parts[1];
                                    break;
                                case "resqhudbtn6function":
                                    RESQHUD6 = parts[1];
                                    break;
                                case "resqhudbtn7function":
                                    RESQHUD7 = parts[1];
                                    break;
                                case "extrahudbtn1function":
                                    EXTRAHUD1 = parts[1];
                                    break;
                                case "extrahudbtn2function":
                                    EXTRAHUD2 = parts[1];
                                    break;
                                case "extrahudbtn3function":
                                    EXTRAHUD3 = parts[1];
                                    break;
                                case "extrahudbtn4function":
                                    EXTRAHUD4 = parts[1];
                                    break;
                                case "extrahudbtn5function":
                                    EXTRAHUD5 = parts[1];
                                    break;
                                case "extrahudrolependantcopbtn1function":
                                    EXTRAHUDROLEPENDANTCOP1 = parts[1];
                                    break;
                                case "extrahudrolependantcopbtn2function":
                                    EXTRAHUDROLEPENDANTCOP2 = parts[1];
                                    break;
                                case "extrahudrolependantcopbtn3function":
                                    EXTRAHUDROLEPENDANTCOP3 = parts[1];
                                    break;
                                case "extrahudrolependantcopbtn4function":
                                    EXTRAHUDROLEPENDANTCOP4 = parts[1];
                                    break;
                                case "extrahudrolependantcopbtn5function":
                                    EXTRAHUDROLEPENDANTCOP5 = parts[1];
                                    break;
                                case "extrahudrolependantcopbtn6function":
                                    EXTRAHUDROLEPENDANTCOP6 = parts[1];
                                    break;
                                case "extrahudrolependantcopbtn7function":
                                    EXTRAHUDROLEPENDANTCOP7 = parts[1];
                                    break;
                                case "extrahudrolependantcopbtn8function":
                                    EXTRAHUDROLEPENDANTCOP8 = parts[1];
                                    break;
                                case "extrahudrolependantcopbtn9function":
                                    EXTRAHUDROLEPENDANTCOP9 = parts[1];
                                    break;
                                case "extrahudrolependantcopbtn10function":
                                    EXTRAHUDROLEPENDANTCOP10 = parts[1];
                                    break;
                                case "extrahudrolependanttowbtn1function":
                                    EXTRAHUDROLEPENDANTTOW1 = parts[1];
                                    break;
                                case "extrahudrolependanttowbtn2function":
                                    EXTRAHUDROLEPENDANTTOW2 = parts[1];
                                    break;
                                case "extrahudrolependanttowbtn3function":
                                    EXTRAHUDROLEPENDANTTOW3 = parts[1];
                                    break;
                                case "extrahudrolependanttowbtn4function":
                                    EXTRAHUDROLEPENDANTTOW4 = parts[1];
                                    break;
                                case "extrahudrolependanttowbtn5function":
                                    EXTRAHUDROLEPENDANTTOW5 = parts[1];
                                    break;
                                case "extrahudrolependanttowbtn6function":
                                    EXTRAHUDROLEPENDANTTOW6 = parts[1];
                                    break;
                                case "extrahudrolependanttowbtn7function":
                                    EXTRAHUDROLEPENDANTTOW7 = parts[1];
                                    break;
                                case "extrahudrolependanttowbtn8function":
                                    EXTRAHUDROLEPENDANTTOW8 = parts[1];
                                    break;
                                case "extrahudrolependanttowbtn9function":
                                    EXTRAHUDROLEPENDANTTOW9 = parts[1];
                                    break;
                                case "extrahudrolependanttowbtn10function":
                                    EXTRAHUDROLEPENDANTTOW10 = parts[1];
                                    break;
                                case "extrahudrolependantcivbtn1function":
                                    EXTRAHUDROLEPENDANTCIV1 = parts[1];
                                    break;
                                case "extrahudrolependantcivbtn2function":
                                    EXTRAHUDROLEPENDANTCIV2 = parts[1];
                                    break;
                                case "extrahudrolependantcivbtn3function":
                                    EXTRAHUDROLEPENDANTCIV3 = parts[1];
                                    break;
                                case "extrahudrolependantcivbtn4function":
                                    EXTRAHUDROLEPENDANTCIV4 = parts[1];
                                    break;
                                case "extrahudrolependantcivbtn5function":
                                    EXTRAHUDROLEPENDANTCIV5 = parts[1];
                                    break;
                                case "extrahudrolependantcivbtn6function":
                                    EXTRAHUDROLEPENDANTCIV6 = parts[1];
                                    break;
                                case "extrahudrolependantcivbtn7function":
                                    EXTRAHUDROLEPENDANTCIV7 = parts[1];
                                    break;
                                case "extrahudrolependantcivbtn8function":
                                    EXTRAHUDROLEPENDANTCIV8 = parts[1];
                                    break;
                                case "extrahudrolependantcivbtn9function":
                                    EXTRAHUDROLEPENDANTCIV9 = parts[1];
                                    break;
                                case "extrahudrolependantcivbtn10function":
                                    EXTRAHUDROLEPENDANTCIV10 = parts[1];
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
                                case "extrabtnrolependantcop1name":
                                    EXTRAHUDROLEPENDANTCOP1NAME = parts[1];
                                    break;
                                case "extrabtnrolependantcop2name":
                                    EXTRAHUDROLEPENDANTCOP2NAME = parts[1];
                                    break;
                                case "extrabtnrolependantcop3name":
                                    EXTRAHUDROLEPENDANTCOP3NAME = parts[1];
                                    break;
                                case "extrabtnrolependantcop4name":
                                    EXTRAHUDROLEPENDANTCOP4NAME = parts[1];
                                    break;
                                case "extrabtnrolependantcop5name":
                                    EXTRAHUDROLEPENDANTCOP5NAME = parts[1];
                                    break;
                                case "extrabtnrolependantcop6name":
                                    EXTRAHUDROLEPENDANTCOP6NAME = parts[1];
                                    break;
                                case "extrabtnrolependantcop7name":
                                    EXTRAHUDROLEPENDANTCOP7NAME = parts[1];
                                    break;
                                case "extrabtnrolependantcop8name":
                                    EXTRAHUDROLEPENDANTCOP8NAME = parts[1];
                                    break;
                                case "extrabtnrolependantcop9name":
                                    EXTRAHUDROLEPENDANTCOP9NAME = parts[1];
                                    break;
                                case "extrabtnrolependantcop10name":
                                    EXTRAHUDROLEPENDANTCOP10NAME = parts[1];
                                    break;
                                case "extrabtnrolependanttow1name":
                                    EXTRAHUDROLEPENDANTTOW1NAME = parts[1];
                                    break;
                                case "extrabtnrolependanttow2name":
                                    EXTRAHUDROLEPENDANTTOW2NAME = parts[1];
                                    break;
                                case "extrabtnrolependanttow3name":
                                    EXTRAHUDROLEPENDANTTOW3NAME = parts[1];
                                    break;
                                case "extrabtnrolependanttow4name":
                                    EXTRAHUDROLEPENDANTTOW4NAME = parts[1];
                                    break;
                                case "extrabtnrolependanttow5name":
                                    EXTRAHUDROLEPENDANTTOW5NAME = parts[1];
                                    break;
                                case "extrabtnrolependanttow6name":
                                    EXTRAHUDROLEPENDANTTOW6NAME = parts[1];
                                    break;
                                case "extrabtnrolependanttow7name":
                                    EXTRAHUDROLEPENDANTTOW7NAME = parts[1];
                                    break;
                                case "extrabtnrolependanttow8name":
                                    EXTRAHUDROLEPENDANTTOW8NAME = parts[1];
                                    break;
                                case "extrabtnrolependanttow9name":
                                    EXTRAHUDROLEPENDANTTOW9NAME = parts[1];
                                    break;
                                case "extrabtnrolependanttow10name":
                                    EXTRAHUDROLEPENDANTTOW10NAME = parts[1];
                                    break;
                                case "extrabtnrolependantciv1name":
                                    EXTRAHUDROLEPENDANTCIV1NAME = parts[1];
                                    break;
                                case "extrabtnrolependantciv2name":
                                    EXTRAHUDROLEPENDANTCIV2NAME = parts[1];
                                    break;
                                case "extrabtnrolependantciv3name":
                                    EXTRAHUDROLEPENDANTCIV3NAME = parts[1];
                                    break;
                                case "extrabtnrolependantciv4name":
                                    EXTRAHUDROLEPENDANTCIV4NAME = parts[1];
                                    break;
                                case "extrabtnrolependantciv5name":
                                    EXTRAHUDROLEPENDANTCIV5NAME = parts[1];
                                    break;
                                case "extrabtnrolependantciv6name":
                                    EXTRAHUDROLEPENDANTCIV6NAME = parts[1];
                                    break;
                                case "extrabtnrolependantciv7name":
                                    EXTRAHUDROLEPENDANTCIV7NAME = parts[1];
                                    break;
                                case "extrabtnrolependantciv8name":
                                    EXTRAHUDROLEPENDANTCIV8NAME = parts[1];
                                    break;
                                case "extrabtnrolependantciv9name":
                                    EXTRAHUDROLEPENDANTCIV9NAME = parts[1];
                                    break;
                                case "extrabtnrolependantciv10name":
                                    EXTRAHUDROLEPENDANTCIV10NAME = parts[1];
                                    break;
                                case "num0":
                                    NUM0 = parts[1];
                                    break;
                                case "num1":
                                    NUM1 = parts[1];
                                    break;
                                case "num2":
                                    NUM2 = parts[1];
                                    break;
                                case "num3":
                                    NUM3 = parts[1];
                                    break;
                                case "num4":
                                    NUM4 = parts[1];
                                    break;
                                case "num5":
                                    NUM5 = parts[1];
                                    break;
                                case "num6":
                                    NUM6 = parts[1];
                                    break;
                                case "num7":
                                    NUM7 = parts[1];
                                    break;
                                case "num8":
                                    NUM8 = parts[1];
                                    break;
                                case "num9":
                                    NUM9 = parts[1];
                                    break;
                                case "num/":
                                    NUMDelt = parts[1];
                                    break;
                                case "num*":
                                    NUMGanger = parts[1];
                                    break;
                                case "num+":
                                    NUMPluss = parts[1];
                                    break;
                                case "num,":
                                    NUMComma = parts[1];
                                    break;
                                case "minimap":
                                    if (parts[1] == "1") MINIMAPLOCOLD = 1;
                                    else if (parts[1] == "2") MINIMAPLOCOLD = 2;
                                    else MINIMAPLOCOLD = 0;
                                    break;
                                case "fps":
                                    if (parts[1] == "1") FPS = 1;
                                    else if (parts[1] == "2") FPS = 2;
                                    else FPS = 0;
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

        static public string COPHUD1NAME = "", COPHUD2NAME = "", COPHUD3NAME = "", COPHUD4NAME = "", COPHUD5NAME = "", COPHUD6NAME = "", COPHUD7NAME = "", COPHUD8NAME = "", COPHUD9NAME = "", COPHUD10NAME = "", COPHUD11NAME = "", COPHUD12NAME = "";
        static public string COPHUD1 = "", COPHUD2 = "", COPHUD3 = "", COPHUD4 = "", COPHUD5 = "", COPHUD6 = "", COPHUD7 = "", COPHUD8 = "", COPHUD9 = "", COPHUD10 = "", COPHUD11 = "", COPHUD12 = "";
        static public string RESQ1NAME = "", RESQ2NAME = "", RESQ3NAME = "", RESQ4NAME = "", RESQ5NAME = "", RESQ6NAME = "", RESQ7NAME = "";
        static public string RESQHUD1 = "", RESQHUD2 = "", RESQHUD3 = "", RESQHUD4 = "", RESQHUD5 = "", RESQHUD6 = "", RESQHUD7 = "";
        static public string EXTRAHUD1NAME = "", EXTRAHUD2NAME = "", EXTRAHUD3NAME = "", EXTRAHUD4NAME = "", EXTRAHUD5NAME = "";
        static public string EXTRAHUDROLEPENDANTCOP1NAME = "", EXTRAHUDROLEPENDANTCOP2NAME = "", EXTRAHUDROLEPENDANTCOP3NAME = "", EXTRAHUDROLEPENDANTCOP4NAME = "", EXTRAHUDROLEPENDANTCOP5NAME = "", EXTRAHUDROLEPENDANTCOP6NAME = "", EXTRAHUDROLEPENDANTCOP7NAME = "", EXTRAHUDROLEPENDANTCOP8NAME = "", EXTRAHUDROLEPENDANTCOP9NAME = "", EXTRAHUDROLEPENDANTCOP10NAME = "";
        static public string EXTRAHUDROLEPENDANTTOW1NAME = "", EXTRAHUDROLEPENDANTTOW2NAME = "", EXTRAHUDROLEPENDANTTOW3NAME = "", EXTRAHUDROLEPENDANTTOW4NAME = "", EXTRAHUDROLEPENDANTTOW5NAME = "", EXTRAHUDROLEPENDANTTOW6NAME = "", EXTRAHUDROLEPENDANTTOW7NAME = "", EXTRAHUDROLEPENDANTTOW8NAME = "", EXTRAHUDROLEPENDANTTOW9NAME = "", EXTRAHUDROLEPENDANTTOW10NAME = "";
        static public string EXTRAHUDROLEPENDANTCIV1NAME = "", EXTRAHUDROLEPENDANTCIV2NAME = "", EXTRAHUDROLEPENDANTCIV3NAME = "", EXTRAHUDROLEPENDANTCIV4NAME = "", EXTRAHUDROLEPENDANTCIV5NAME = "", EXTRAHUDROLEPENDANTCIV6NAME = "", EXTRAHUDROLEPENDANTCIV7NAME = "", EXTRAHUDROLEPENDANTCIV8NAME = "", EXTRAHUDROLEPENDANTCIV9NAME = "", EXTRAHUDROLEPENDANTCIV10NAME = "";
        static public string EXTRAHUD1 = "", EXTRAHUD2 = "", EXTRAHUD3 = "", EXTRAHUD4 = "", EXTRAHUD5 = "";
        static public string EXTRAHUDROLEPENDANTCOP1 = "", EXTRAHUDROLEPENDANTCOP2 = "", EXTRAHUDROLEPENDANTCOP3 = "", EXTRAHUDROLEPENDANTCOP4 = "", EXTRAHUDROLEPENDANTCOP5 = "", EXTRAHUDROLEPENDANTCOP6 = "", EXTRAHUDROLEPENDANTCOP7 = "", EXTRAHUDROLEPENDANTCOP8 = "", EXTRAHUDROLEPENDANTCOP9 = "", EXTRAHUDROLEPENDANTCOP10 = "";
        static public string EXTRAHUDROLEPENDANTTOW1 = "", EXTRAHUDROLEPENDANTTOW2 = "", EXTRAHUDROLEPENDANTTOW3 = "", EXTRAHUDROLEPENDANTTOW4 = "", EXTRAHUDROLEPENDANTTOW5 = "", EXTRAHUDROLEPENDANTTOW6 = "", EXTRAHUDROLEPENDANTTOW7 = "", EXTRAHUDROLEPENDANTTOW8 = "", EXTRAHUDROLEPENDANTTOW9 = "", EXTRAHUDROLEPENDANTTOW10 = "";
        static public string EXTRAHUDROLEPENDANTCIV1 = "", EXTRAHUDROLEPENDANTCIV2 = "", EXTRAHUDROLEPENDANTCIV3 = "", EXTRAHUDROLEPENDANTCIV4 = "", EXTRAHUDROLEPENDANTCIV5 = "", EXTRAHUDROLEPENDANTCIV6 = "", EXTRAHUDROLEPENDANTCIV7 = "", EXTRAHUDROLEPENDANTCIV8 = "", EXTRAHUDROLEPENDANTCIV9 = "", EXTRAHUDROLEPENDANTCIV10 = "";
        static public string NUM0 = "", NUM1 = "", NUM2 = "", NUM3 = "", NUM4 = "", NUM5 = "", NUM6 = "", NUM7 = "", NUM8 = "", NUM9 = "", NUMDelt = "", NUMGanger = "", NUMPluss = "", NUMComma = "";
    }
}