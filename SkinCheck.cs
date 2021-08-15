using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mary
{
    class SkinCheck
    {
        static public void CheckSkin(Clients user)
        {
            List<string> SkinList = new List<string>();
            using (StreamReader Reader = new StreamReader(@"skins.txt"))
            {
                string linje = "";
                while ((linje = Reader.ReadLine()) != null)
                {
                    SkinList.Add(linje);
                }
            }
            for (int x = 0; x < SkinList.Count(); x++)
            {
                if (SkinList[x].ToLower().Equals(user.P.SkinName.ToLower()))
                {
                    Messages.SendLocalMsg(user.PlayerName + " ^1is using a private skin! (" + SkinList[x] + ")");
                }
            }
        }
    }
}