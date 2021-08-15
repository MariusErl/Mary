using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Mary
{
    class AdminCheck
    {
        static public List<string> AdminList = new List<string>();
        static public void Check(bool showResult)
        {
            AdminList.Clear();
            try
            {
                //Removed due to open source version.
                if (showResult) ListAdmins();
            }
            catch (Exception e) { Program.ConsError(e.ToString()); }
        }

        static public void ListAdmins()
        {
            foreach (Clients all in Program.AllConnections)
            {
                if (AdminList.ToArray().Contains(all.Username.ToLower()))
                {
                    Messages.SendLocalMsg(all.PlayerName + " ^8is insim admin.");
                }
            }
        }
    }
}