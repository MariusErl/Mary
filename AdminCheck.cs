using System;
using System.Collections.Generic;
using System.Net;
using System.IO;

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
                WebRequest request = WebRequest.Create("http://insim.city-driving.co.uk/stats.php?type=tcmembers");
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                using (StreamReader Sr = new StreamReader(data))
                {
                    String linje;
                    while ((linje = Sr.ReadLine()) != null)
                    {
                        if (linje.Contains("target=\"_blank\">"))
                        {
                            AdminList.Add(linje.Remove(0, 152).Split('"')[0].ToLower());
                        }
                    }
                }
                if (showResult) ListAdmins();
            }
            catch (Exception e)
            {
                Program.ConsError(e.ToString());
            }
        }

        static public void ListAdmins()
        {
            Messages.SendLocalMsg("Admins online:");
            bool adminsOnline = false;
            for (int x = 0; x < AdminList.Count; x++)
            {
                foreach (Clients all in Program.AllConnections)
                {
                    if (all.Username.ToLower() == AdminList[x])
                    {
                        adminsOnline = true;
                        Messages.SendLocalMsg(all.PlayerName + " ^8(" + all.Username + ")");
                    }
                }
            }
            if (!adminsOnline) Messages.SendLocalMsg("No admins online on this server.");
        }
    }
}