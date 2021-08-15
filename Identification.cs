using System;
using System.IO;
using System.Net;
using System.Text;


namespace Mary
{
    class Identification
    {
        static public string VERSIONNUMBER = "2.9 (updated 02.03.2021)";
        static public bool latestVersion = false;
        static public bool FULLVERSION = false;
        static public string MyUsername = "";
        static public Clients MySelf = new Clients();

        static public void CheckVersion()
        {
            //Removed due to open sourced code.
        }

        static public bool CheckOnlineKey(string hash)
        {
            //Removed due to open source
            return true;
        }

        static public void CheckKey()
        {
            try
            {
                //Removed due to open source
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        static string SHA256(string key)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(key));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}