using System;
using System.Collections.Generic;

namespace Mary
{
    class Tracks
    {
        static public string CalculateGrid(int x, int y)
        {
            //X-axe:    1 = -1120m
            //Y-axe:    1 = 1120m

            string alfabet = "ABCDEFGHIJKLMNOP";

            string a = alfabet.Substring((-1 * (-160 + -1120 - x) / 160), 1);
            string b = ((320 + 1120 - y) / 160).ToString();

            return a + b;
        }
        //Montana
        static public int[][] ASZone0 = new int[][] { new int[] { -361, -323, -186, -42, -51 }, new int[] { 959, 1056, 1048, 964, 804 } };//Devils corner
        static public int[][] ASZone1 = new int[][] { new int[] { -47, -74, -148, -212, -129 }, new int[] { 802, 464, 391, 476, 843 } };//The Bridge
        static public int[][] ASZone2 = new int[][] { new int[] { -72, -149, 92, 149, 173, 97 }, new int[] { 468, 391, -23, -45, 14, 216 } };//Corkscrew
        static public int[][] ASZone3 = new int[][] { new int[] { 149, 1025, 1018, 925, 430, 60, -135, -135, 89 }, new int[] { -45, -516, -715, -716, -343, -86, -79, 0, -22 } };//Highway 2 North
        static public int[][] ASZone4 = new int[][] { new int[] { 1014, 1009, 957, 925 }, new int[] { -714, -1033, -1030, -713 } };//Highway 2 South
        static public int[][] ASZone5 = new int[][] { new int[] { 1010, 997, 851, 764, 832, 918 }, new int[] { -1037, -1134, -1130, -983, -950, -942 } };//Simons Way
        static public int[][] ASZone6 = new int[][] { new int[] { 839, 767, 504, 384, 469, 755 }, new int[] { -955, -987, -906, -727, -697, -820 } };//Lower Manor Rd.
        static public int[][] ASZone7 = new int[][] { new int[] { 332, 458, 467, 385, 306 }, new int[] { -441, -457, -697, -728, -559 } };//Gass Station
        static public int[][] ASZone8 = new int[][] { new int[] { 459, 334, -26, -68, 97, 421 }, new int[] { -458, -442, -244, -192, -171, -345 } };//Upper Manor Rd.
        static public int[][] ASZone9 = new int[][] { new int[] { -32, -116, -177, -195, -69 }, new int[] { -238, -378, -375, -240, -192 } };//Shopping Mall
        static public int[][] ASZone10 = new int[][] { new int[] { -116, -126, -215, -480, -918, -937, -649, -407, -206, -177 }, new int[] { -376, -633, -924, -1081, -1100, -1019, -982, -943, -671, -374 } };//Hillside Road
        static public int[][] ASZone11 = new int[][] { new int[] { -919, -1027, -1054, -951 }, new int[] { -1098, -1084, -992, -942 } };//Drifters Corner
        static public int[][] ASZone12 = new int[][] { new int[] { -950, -658, -611, -706, -939 }, new int[] { -942, -810, -841, -929, -997 } };//Lower Hillside Rd.
        static public int[][] ASZone13 = new int[][] { new int[] { -659, -663, -617, -579, -543, -611 }, new int[] { -811, -727, -656, -657, -790, -843 } };//Interstate Junction
        static public int[][] ASZone14 = new int[][] { new int[] { -563, -365, -324, -340, -291, -543 }, new int[] { -726, -145, -145, -377, -634, -785 } };//Main Street South
        static public int[][] ASZone15 = new int[][] { new int[] { -117, -196, -194, -138 }, new int[] { -212, -239, -6, 41 } };//Hillside Pass
        static public int[][] ASZone16 = new int[][] { new int[] { -134, -183, -243, -317, -323, -327, -268, -202 }, new int[] { 43, 85, 67, -128, -146, -189, -155, -12 } };//Pond Lane
        static public int[][] ASZone17 = new int[][] { new int[] { -371, -379, -363, -212, -152, -280, -307, -327, -334 }, new int[] { -146, 198, 231, 476, 398, 224, 232, 170, -146 } };//Main Street South
        static public int[][] ASZone18 = new int[][] { new int[] { -391, -442, -336, -352, -305, -253, -294, -365 }, new int[] { 167, 305, 542, 650, 751, 643, 454, 229 } };//Snake Pass
        static public int[][] ASZone19 = new int[][] { new int[] { -398, -285, -556, -677 }, new int[] { 1016, 944, -78, -76 } };//Highway 1 North
        static public int[][] ASZone20 = new int[][] { new int[] { -684, -664, -571, -576, -576, -635, -659, -612, -613 }, new int[] { -195, -67, -61, -189, -664, -660, -438, -440, -191 } };//Highway 1 South
        static public int[][] ASZone21 = new int[][] { new int[] { -682, -614, -612, -684 }, new int[] { -194, -190, -445, -436 } };//Safezone 1
        static public int[][] ASZone22 = new int[][] { new int[] { -283, -304, -330, -333, -324, -241 }, new int[] { 226, 233, 164, -143, -145, 68 } };//Safezone 2
        static public int[][][] AS = new int[][][] { ASZone0, ASZone1, ASZone2, ASZone3, ASZone4, ASZone5, ASZone6, ASZone7, ASZone8, ASZone9, ASZone10, ASZone11, ASZone12, ASZone13, ASZone14, ASZone15, ASZone16, ASZone17, ASZone18, ASZone19, ASZone20, ASZone21, ASZone22 };

        //static public void FindRoad(Clients User)
        //{
        //    int X = (int)User.P.x;
        //    int Y = (int)User.P.y;
        //    int[][][] Zone = LoadCorrectZones();

        //    bool insideOnePoly = false;
        //    //Loop through all AS zones
        //    for (int alleSoner = 0; alleSoner < Zone.Length; alleSoner++)
        //    {
        //        if (InPolygon(Zone[alleSoner][0].Length, Zone[alleSoner][0], Zone[alleSoner][1], X, Y))
        //        {
        //            List<string> SoneInformasjon = GetNameFromZone(alleSoner);
        //            User.P.Location = SoneInformasjon[0];
        //            User.P.SpeedlimitKph = Convert.ToInt16(SoneInformasjon[1]);
        //            insideOnePoly = true;
        //        }
        //        if (!insideOnePoly)
        //        {
        //            User.P.Location = "Offroad";
        //        }
        //    }
        //}
    }
}