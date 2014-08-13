using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using SeasideResearch.LibCurlNet;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace TinyWindows
{
    class CurlThread : Easy
    {
        private static FileStream fileSteram = null;
        private static string apiUrl = "";
        private static string FilePath = "";
        private static string ApiKey = "";
        private static string ProxyType = "";
        private static string ProxyHost = "";
        private static string ProxyPort = "";
        private static Easy easy = null;

        public CurlThread()
        {
            Curl.GlobalInit((int)CURLinitFlag.CURL_GLOBAL_ALL);
            easy = new Easy();
        }

        public static string DownloadFile()
        {
            return "";
        }
    }
}
