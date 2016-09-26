using System;
using System.Text;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace Mary
{
    class Identification
    {
        static public string VERSIONNUMBER = "1.2";
        static public string newVersion = "q";
        static public bool latestVersion = true;
        static public void CheckVersion()
        {
            //If you wish to control which version of your application is used, this is 1 simple but very effective way.
            WebRequest request = WebRequest.Create("http://www.domain.com/mary/version.txt");
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            string html = "";
            bool okVersion = false;
            string txt = "";
            using (StreamReader srr = new StreamReader(data))
            {
                while ((html = srr.ReadLine()) != null)
                {
                    if (html == newVersion)
                    {
                        okVersion = true;
                    }
                    txt += html;
                }
                latestVersion = (txt.Remove(0, txt.Length-1) == newVersion) ? true : false;
            }
            if (!okVersion)
            {
                Program.Cons("You have an old version of the InSim application and it is no longer accepted.");
                System.Threading.Thread.Sleep(10000);
                Environment.Exit(0);
            }
            else
            {
                if (latestVersion)
                {
                    Program.Cons("Version OK [Latest version]");
                }
                else
                {
                    Program.Cons("Version OK [Not latest version]");
                }
            }
        }

        static public bool CheckOnlineKey(string hash)
        {
            bool legit = false;
            WebRequest request = WebRequest.Create("http://www.domain.com/mary/keys.txt");
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            string html = "";
            using (StreamReader srr = new StreamReader(data))
            {
                while ((html = srr.ReadLine()) != null)
                {
                    if (html == hash.ToLower())
                    {
                        legit = true;
                    }
                }
            }
            return legit;
        }

        //The key system works like this:
        //In the settings.txt file, there is a field "key="
        //This field also controls what username is allowed to use the tool. So it could looke like this "key=mariusmm,myKey"
        //The username is split by a comma and then the actual key. I used all that text (mariusmm,myKey)
        //converted it to a MD5 hash (506aab94c22bb4017a052cc96e8e1f10), and the I put that line of values/numbers into a text file I used as my 'keybook'.
        //What Mary does, is MD5 hash your "key=" value, and check if it exist. The keys is hosted online for everyone to see, but in a MD5 hash state, so it's practicly (not theoreticly)
        //impossible to know the keys. It's simple but really effective method I used for a long time.
        static public void CheckKey()
        {
            string username = Settings.KEY.Split(',')[0];
            string hashedKey = GetHashString(Settings.KEY);
            if (CheckOnlineKey(hashedKey))
            {
                Program.MyUsername = username;
                Program.Cons("Key OK [ID: " + username + "]");
            }
            else
            {
                Program.Cons("Key NOT OK - Shutting down application.");
                System.Threading.Thread.Sleep(5000);
                Environment.Exit(0);
            }
        }

        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}