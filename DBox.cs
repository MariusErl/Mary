using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.FileProperties;
using Dropbox.Api.Files;
using Dropbox.Api.Stone;
using System.Text.RegularExpressions;


namespace Mary
{
    class DBox
    {
        static public string DownloadIP(string user)
        {
            string contentDl = "";
            Task.Run<string>((Func<string>)(() => { contentDl = DownloadTask(user).Result; return contentDl; }));

            while (contentDl == "") ;
            Console.WriteLine(contentDl);
            return contentDl;
        }
        static public string DownloadUsers(string content)
        {
            string contentDl = "";
            Task.Run<string>((Func<string>)(() => { contentDl = DownloadTask(content).Result; return contentDl; }));

            while (contentDl == "");
            Console.WriteLine(contentDl);
            list = (contentDl);
            return contentDl;
        }

        static public string list = "";

        public static async Task<string> DownloadTask(string file)
        {
            DropboxClient client;
            int num = 0;
            //if ((uint)num > 1U)
                client = new DropboxClient("_AOAzbTXCXIAAAAAAAAAF-oseLeTYtO7kbW1ISsYinhuA5hpUpwJF2JKlvlexd9z");
            string contentAsStringAsync;
            try
            {
                using (IDownloadResponse<FileMetadata> idownloadResponse = await client.Files.DownloadAsync("/" + file, (string)null))
                    contentAsStringAsync = await idownloadResponse.GetContentAsStringAsync();
            }
            finally
            {
                if (client != null)
                    ((IDisposable)client).Dispose();
            }
            return contentAsStringAsync;
        }
    }
}