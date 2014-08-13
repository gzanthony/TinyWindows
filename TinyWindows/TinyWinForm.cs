using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Diagnostics;
using TinyWindows.lib;
using System.Xml;
using System.Collections;
using SeasideResearch.LibCurlNet;
using System.Runtime.Remoting.Messaging;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TinyWindows
{
    public partial class TinyWinForm : Form
    {
        #region 变量定义
        
        private string CurrentFilePath = ""; //文件绝对路径
        private ArrayList Lfiles = new ArrayList(); //文件数组
        private int FileCount = 0; //文件总数
        private int ConvertCount = 0; //转换次数
        private int KeyCount = 0; //密匙数量
        private int KeyConvertCount = 0; //单个密匙的已转换次数
        private int BeinConvertCount = 0; //总计转换次数
        private string RunPath = System.Environment.CurrentDirectory; //程序运行的当前绝对路径
        private string wxmlPath; //列表数据XML文件
        private string wxmlSettingPath; //设置数据XML文件
        private int CurrentKey = 0; //当前使用的密匙
        private string CurrentDownload;

        public static string apiUrl = ""; //调用的API接口地址
        public static string CurrentFileName; //当前转换文件
        public static List<string> ConvertImageFiles = new List<string>(); //文件列表
        public static List<string> TinyApiKeys = new List<string>(); //有效的密匙列表
        public static Hashtable TinyFiles = new Hashtable(); //文件哈希列表
        public static Hashtable TinyKeys = new Hashtable(); //API密匙列表
        public static string ProxyType = ""; //代理类型
        public static string ProxyHost = ""; //代理地址
        public static string ProxyPort = ""; //代理端口

        private delegate void FlushClient(); //代理
        #endregion
        public TinyWinForm()
        {
            InitializeComponent();
            this.Text = "TinyPNG " + String.Format("版本 {0}", AssemblyVersion);
            wxmlPath = @RunPath + "\\ImageFiles.xml";
            wxmlSettingPath = @RunPath + "\\setting.xml";
            //初始化数据和设置
            initXmlDataFiles();

            //debug convert thread
            TinyKeys.Add("9JLP3SYKOK_mOdHRQYwAe8n2T82RRKVO", 500); // tinypng06@bynt.cn
            TinyKeys.Add("f_mMZccjfr_fI9V3f9IVVgbIpGQ8ZfKr", 500); //Google Mail
            TinyKeys.Add("N7DTMHflPiph8BIC6iC4zzq83cKsjB5v", 500);
            TinyKeys.Add("HtE0DBDQlV6YgCYcQdIdPZ6xe9btar1-", 500);
        }

        private void Panel1_DragDrop(object sender, DragEventArgs e)
        {
            OpenFile(((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString());
        }

        private void Panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else e.Effect = DragDropEffects.None;
        }

        private void OpenFile(string filepath)
        {
            CurrentFilePath = filepath;
            FileInfo fileinfo = new FileInfo(filepath);
            ListFiles(fileinfo, ref Lfiles);

            if (TinyFiles.Count > 0)
            {
                BeginConvertButton.Enabled = true;
                GridDataUpdate();
                
                WriteToXML();
            }
            else
            {
                BeginConvertButton.Enabled = false;
            }
        }

        #region 遍历目录或只增加一个文件
        private void ListFiles(FileSystemInfo info, ref ArrayList FileList)
        {
            if (info.Attributes == FileAttributes.Directory)
            {
                DirectoryInfo dir = new DirectoryInfo(info.FullName);
                FileInfo[] allFile = dir.GetFiles();
                foreach (FileInfo fi in allFile)
                {
                    FileList.Add(fi.FullName);
                    if (MimeType.GetMIMEType(fi.FullName) == "image/png")
                    {
                        if (!TinyFiles.ContainsKey(fi.FullName)) TinyFiles.Add(fi.FullName, 0);
                    }
                }

                DirectoryInfo[] allDir = dir.GetDirectories();
                foreach (DirectoryInfo d in allDir)
                {
                    ListFiles(d, ref FileList);
                }
                return;
            }
            else
            {
                /*单个文件的处理*/
                System.Reflection.PropertyInfo[] properties = info.GetType().GetProperties();
                String FileMimeType = MimeType.GetMIMEType(properties[6].GetValue(info, null).ToString());
                if (FileMimeType == "image/png")
                {
                    String FullPath = properties[6].GetValue(info, null).ToString();
                    if (!TinyFiles.ContainsKey(FullPath)) TinyFiles.Add(FullPath, 0);
                }
                return;
            }
        }
        #endregion

        #region 将文件列表数据写入 XML 文件
        public void WriteToXML()
        {   
            if (TinyFiles.Count > 0)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(wxmlPath);
                XmlNode root = xmlDoc.SelectSingleNode("data");
                root.RemoveAll();

                foreach (DictionaryEntry obj in TinyFiles)
                {
                    XmlElement fsNode = xmlDoc.CreateElement("files");
                    fsNode.SetAttribute("path", obj.Key.ToString());
                    fsNode.SetAttribute("converted", obj.Value.ToString());

                    root.AppendChild(fsNode);
                }
                
                xmlDoc.Save(wxmlPath);
            }
            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(wxmlPath);
                XmlNode root = xmlDoc.SelectSingleNode("data");
                root.RemoveAll();
                xmlDoc.Save(wxmlPath);
            }

            return;
        }
        #endregion

        #region 更新数据视图
        public void GridDataUpdate()
        {
            if (TinyFiles.Count > 0)
            {
                DataSet ds = new DataSet();
                DataTable dt = ds.Tables.Add("ImageFiles");
                dt.Columns.Add("FileName");
                dt.Columns.Add("status");

                IDictionaryEnumerator enumerator = TinyFiles.GetEnumerator();
                DataRow row = null;
                while (enumerator.MoveNext())
                {
                    string k = (string)enumerator.Key;
                    Int16 s = Convert.ToInt16(enumerator.Value);
                    row = dt.NewRow();
                    row["FileName"] = k;

                    switch (s)
                    {
                        case 0:
                            row["status"] = "ready to convert";
                            break;
                        case 1:
                            row["status"] = "finish.";
                            break;
                        case 2:
                            row["status"] = "fail.";
                            break;
                        case 3:
                            row["status"] = "ready to download";
                            break;
                        default:
                            row["status"] = "wait to go.";
                            break;
                    }

                    dt.Rows.Add(row);
                }
                CompressTable(dt);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Width = 600;
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
        #endregion

        #region 过滤表中重复的项目
        private void CompressTable(DataTable table)
        {
            Dictionary<string, DataRow> dict = new Dictionary<string, DataRow>();
            List<DataRow> removeRows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                string key = row[0].ToString() + ";" + row[1].ToString();
                DataRow row1;
                if (dict.TryGetValue(key, out row1))
                {
                    if (row1[3] == DBNull.Value)
                    {
                        row1[3] = row[3].ToString();
                        row1[2] = row[2].ToString();
                    }
                    else
                    {
                        row1[2] = row1[2].ToString() + ";" + row[2].ToString();
                        row1[3] = int.Parse(row1[3].ToString()) + int.Parse(row[3].ToString());
                    }
                    removeRows.Add(row);
                }
                else
                {
                    dict.Add(key, row);
                }
            }

            foreach (DataRow row in removeRows) { table.Rows.Remove(row); }

            return;
        }
        #endregion

        #region 初始化XML数据
        private void initXmlDataFiles()
        {
            //读取图片文件列表
            FileInfo file = new FileInfo(wxmlPath);
            if (!file.Exists)
            {
                XmlTextWriter xtw = new XmlTextWriter(wxmlPath, Encoding.UTF8);
                xtw.WriteStartDocument();

                xtw.WriteStartElement("data");
                xtw.Flush();
                xtw.Close();
            }
            else
            {
                System.IO.Stream S = new System.IO.FileStream(@wxmlPath, System.IO.FileMode.Open);
                XmlTextReader xtr = new XmlTextReader(S);
                xtr.MoveToContent();

                while (xtr.Read())
                {
                    if (xtr.HasAttributes && xtr.Name == "files")
                    {
                        xtr.MoveToAttribute("path");
                        string fspath = xtr.Value;

                        xtr.MoveToAttribute("converted");
                        Int32 fscc = Convert.ToInt32(xtr.Value);
                        
                        TinyFiles.Add(fspath, fscc);
                    }
                }

                xtr.Close();

                if (TinyFiles.Count > 0)
                {
                    BeginConvertButton.Enabled = true;
                    GridDataUpdate();
                }
            }

            //读取设置列表
            FileInfo seFS = new FileInfo(wxmlSettingPath);
            if (!seFS.Exists)
            {
                
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration xmldecl;
                xmldecl = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(xmldecl);

                XmlElement root = xmlDoc.CreateElement("", "data", "");
                xmlDoc.AppendChild(root);

                XmlNode dataRoot = xmlDoc.SelectSingleNode("data");
                XmlElement ApiNode = xmlDoc.CreateElement("api");
                ApiNode.SetAttribute("url", "https://api.tinypng.com/shrink");
                dataRoot.AppendChild(ApiNode);

                XmlElement KeysNode = xmlDoc.CreateElement("keys");
                dataRoot.AppendChild(KeysNode);

                xmlDoc.AppendChild(root);
                xmlDoc.Save(wxmlSettingPath);
            }
            else
            {
                System.IO.Stream S = new System.IO.FileStream(@wxmlSettingPath, System.IO.FileMode.Open);
                XmlTextReader xtr = new XmlTextReader(S);
                xtr.MoveToContent();

                while (xtr.Read())
                {
                    if (xtr.HasAttributes && xtr.Name == "api")
                    {
                        xtr.MoveToAttribute("url");
                        apiUrl = xtr.Value;
                    } 
                    else 
                    if (xtr.HasAttributes && xtr.Name == "keys")
                    {
                        xtr.MoveToAttribute("string");
                        string apiText = xtr.Value;

                        xtr.MoveToAttribute("count");
                        Int32 apicount = Convert.ToInt32(xtr.Value);

                        if (apicount > 0) TinyKeys.Add(apiText, apicount);
                    }
                }

                xtr.Close();
            }
            return;
        }
        #endregion

        #region 退出程序
        private void MenuItemQuit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
        #endregion

        #region 关于我的弹出窗
        private void MenuItemAboutMe_Click(object sender, EventArgs e)
        {
            Form AboutMe = new AboutBox1();
            AboutMe.ShowDialog();
        }
        #endregion

        #region 清空列表
        private void MenuItemClearup_Click(object sender, EventArgs e)
        {
            ConvertImageFiles.Clear();
            TinyFiles.Clear();
            bindingSource1.Clear();
            dataGridView1.DataSource = null;
            BeginConvertButton.Enabled = false;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(wxmlPath);
            XmlNode root = xmlDoc.SelectSingleNode("data");
            root.RemoveAll();
            xmlDoc.Save(wxmlPath);
        }
        #endregion

        #region 程序集特性访问器

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(System.Reflection.AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    System.Reflection.AssemblyTitleAttribute titleAttribute = (System.Reflection.AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        #endregion

        private void MenuItemRegisterKEY_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://tinypng.com/developers");
        }

        #region 开始转换图片
        private void BeginConvertButton_Click(object sender, EventArgs e)
        {
            TinyApiKeys.Clear();

            if (apiUrl.Length == 0)
            {
                MessagePopup("没有配置 API 接口地址.", "提示");
            }
            else
            {
                if (TinyKeys.Count > 0)
                {
                    foreach (DictionaryEntry de in TinyKeys)
                    {
                        string k = de.Key.ToString();
                        Int32 c = Convert.ToInt32(de.Value);

                        if (c > 0) TinyApiKeys.Add(k);
                    }

                    if (TinyApiKeys.Count > 0)
                    {
                        BeginConvertButton.Enabled = false;
                        menuStrip1.Enabled = false;

                        //ProcessMessage.Items.Clear();
                        //ProcessMessage.Visible = true;
                        dataGridView1.Visible = false;

                        //先把所有路径存入LIST
                        ConvertImageFiles.Clear();
                        foreach (DictionaryEntry de in TinyFiles)
                        {
                            string path = de.Key.ToString();
                            if (int.Parse(de.Value.ToString()) == 0 || int.Parse(de.Value.ToString()) == 2)
                            {
                                ConvertImageFiles.Add(path);
                            }
                        }

                        if (ConvertImageFiles.Count > 0)
                        {
                            //Thread thread = new Thread(CrossThreadFlush);
                            //thread.IsBackground = true;
                            //thread.Start();
                            CurrentKey = 0;
                            TaskAsync();
                        }
                        else
                        {
                            MessagePopup("已将所有文件转换.", "提示");
                        }
                    }
                    else
                    {
                        MessagePopup("没有可用的密匙,请去 tinypng.com 申请!", "提示");
                    }
                }
                else
                {
                    MessagePopup("没有可用的密匙,请去 tinypng.com 申请!", "提示");
                }
            }
        }
        #endregion

        #region 提示框
        private void MessagePopup(string Message, string title)
        {
            MessageBox.Show(Message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        #endregion

        #region 异步线程处理
        private delegate void TaskWorkerDelegate();
        private bool _TaskIsRunning = false;
        public event AsyncCompletedEventHandler TaskCompleted;
        private readonly object _sync = new object();
        public static string responseBuf = "";
        public static string HttpHeaderBuffer = "";
        public static string Downloadfs = "";
        public static bool UploadProcess = true; //curl 是下载还是上传
        public static bool NextTask = false; //判断是否还有下一个任务
        public static JObject respObject = new JObject();
        public static FileStream fs = null;
        public static int fsSize = 0;

        public bool IsBusy
        {
            get { return _TaskIsRunning; }
        }

        public void TaskAsync()
        {
            CurrentFileName = ConvertImageFiles[0].ToString();
            TaskWorkerDelegate worker = new TaskWorkerDelegate(TaskWorker);
            AsyncCallback completedCallback = new AsyncCallback(TaskCompletedCallback);

            int TinyFileStats = int.Parse(TinyFiles.ContainsKey(CurrentFileName).ToString());
            if (TinyFileStats == 3)
            {
                UploadProcess = true;
                //ProcessMessage.Items.Add("download file:[" + CurrentFileName + "]");
            }
            else
            {
                //ProcessMessage.Items.Add("upload file:[" + CurrentFileName + "]");
            }
            

            lock (_sync)
            {
                if (!_TaskIsRunning)
                {
                    AsyncOperation async = AsyncOperationManager.CreateOperation(null);
                    worker.BeginInvoke(completedCallback, async);
                    _TaskIsRunning = true;
                }
                else
                {
                    Console.WriteLine("TaskWorker is buesy right now.");
                }
            }
        }

        private void TaskWorker()
        {
            string apikey = TinyApiKeys[CurrentKey].ToString();

            try
            {
                Curl.GlobalInit((int)CURLinitFlag.CURL_GLOBAL_ALL);
                Easy easy = new Easy();

                if (UploadProcess)
                {
                    Console.WriteLine("begin upload process.");
                    fs = new FileStream(CurrentFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                    byte[] byteData = new byte[1024 * 1024 * 5];
                    int fsLength = fs.Read(byteData, 0, byteData.Length);
                    //fs.Seek(0, SeekOrigin.Begin);

                    Easy.ReadFunction rf = new Easy.ReadFunction(OnReadData);
                    easy.SetOpt(CURLoption.CURLOPT_READFUNCTION, rf);
                    easy.SetOpt(CURLoption.CURLOPT_READDATA, fs);
                    easy.SetOpt(CURLoption.CURLOPT_URL, apiUrl);
                    easy.SetOpt(CURLoption.CURLOPT_USERPWD, "api:" + apikey);
                    easy.SetOpt(CURLoption.CURLOPT_POST, true);
                    easy.SetOpt(CURLoption.CURLOPT_POSTFIELDS, byteData);
                    easy.SetOpt(CURLoption.CURLOPT_POSTFIELDSIZE, fsLength);
                }
                else
                {
                    Console.WriteLine("begin download process.");
                    fsSize = int.Parse(respObject["output"]["size"].ToString());
                    fs = new FileStream(CurrentFileName, FileMode.Create, FileAccess.Write, FileShare.Write);
                    easy.SetOpt(CURLoption.CURLOPT_URL, respObject["output"]["url"].ToString());
                    easy.SetOpt(CURLoption.CURLOPT_WRITEDATA, fs);
                    NextTask = false;
                }

                //Easy.DebugFunction df = new Easy.DebugFunction(OnDebug);
                //easy.SetOpt(CURLoption.CURLOPT_DEBUGFUNCTION, df);

                Easy.WriteFunction wf = new Easy.WriteFunction(OnWriteData);
                easy.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, wf);

                Easy.ProgressFunction pf = new Easy.ProgressFunction(OnProgress);
                easy.SetOpt(CURLoption.CURLOPT_PROGRESSFUNCTION, pf);

                easy.SetOpt(CURLoption.CURLOPT_VERBOSE, true);
                easy.SetOpt(CURLoption.CURLOPT_PROXYTYPE, CURLproxyType.CURLPROXY_SOCKS5);
                easy.SetOpt(CURLoption.CURLOPT_PROXY, "127.0.0.1:1080");
                
                //easy.SetOpt(CURLoption.CURLOPT_TIMEOUT, 10);
                easy.SetOpt(CURLoption.CURLOPT_FOLLOWLOCATION, true);
                
                easy.SetOpt(CURLoption.CURLOPT_HEADER, true);
                easy.SetOpt(CURLoption.CURLOPT_SSL_VERIFYPEER, false);
                easy.SetOpt(CURLoption.CURLOPT_SSL_VERIFYHOST, 0);
                
                easy.Perform();

                int ResponseCode = 0;
                easy.GetInfo(CURLINFO.CURLINFO_RESPONSE_CODE, ref ResponseCode);
                
                int ResponseHeaderSize = 0;
                easy.GetInfo(CURLINFO.CURLINFO_HEADER_SIZE, ref ResponseHeaderSize);
                
                easy.Cleanup();

                Curl.GlobalCleanup();
                fs.Close();

                if (ResponseHeaderSize > 0 && UploadProcess != false)
                {
                    try
                    {
                        string _jerror = "";
                        string _jmessage = "";
                        bool hasDownloadUrl = false;

                        JObject jo = (JObject)JsonConvert.DeserializeObject(responseBuf);
                        if (jo.Property("error") != null) _jerror = jo["error"].ToString();
                        if (jo.Property("message") != null) _jmessage = jo["message"].ToString();
                        if (jo.Property("output") != null) hasDownloadUrl = true;

                        if (_jerror != "")
                        {
                            Console.WriteLine("Exception error, " + _jmessage);
                        }
                        else
                        {
                            if (hasDownloadUrl)
                            {
                                respObject = jo;
                                Console.WriteLine("Download Process Begin:" + respObject["output"]["url"]);
                                UploadProcess = false;
                                NextTask = true;
                            }
                        }
                    } catch(Exception e) {
                        Console.WriteLine("json data error," + e.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void OnDebug(CURLINFOTYPE infoType, String msg, Object extraData)
        {
            if (infoType == CURLINFOTYPE.CURLINFO_DATA_IN)
                Console.WriteLine(msg);

            if (infoType == CURLINFOTYPE.CURLINFO_HEADER_OUT)
                Console.WriteLine(msg);
        }

        public static Int32 OnReadData(Byte[] buf, Int32 size, Int32 nmemb,
        Object extraData)
        {
            FileStream fs = (FileStream)extraData;
            return fs.Read(buf, 0, size * nmemb);
        }

        public static Int32 OnWriteData(Byte[] buf, Int32 size, Int32 nmemb, Object extraData)
        {
            if (UploadProcess != true)
            {
                fs.Write(buf, size, nmemb);
            }
            else
            {
                string ResponseString = System.Text.Encoding.UTF8.GetString(buf);

                responseBuf = System.Text.Encoding.UTF8.GetString(buf);
                HttpHeaderBuffer += System.Text.Encoding.UTF8.GetString(buf);
            }
            return size * nmemb;
        }

        public static Int32 OnProgress(Object extraData, Double dlTotal,
        Double dlNow, Double ulTotal, Double ulNow)
        {
            Console.WriteLine("Progress: {0} {1} {2} {3}",
                dlTotal, dlNow, ulTotal, ulNow);
            return 0; // standard return from PROGRESSFUNCTION
        }

        private void TaskCompletedCallback(IAsyncResult ar)
        {
            TaskWorkerDelegate worker = (TaskWorkerDelegate)((AsyncResult)ar).AsyncDelegate;
            AsyncOperation async = (AsyncOperation)ar.AsyncState;

            worker.EndInvoke(ar);

            lock (_sync)
            {
                _TaskIsRunning = false;
            }

            AsyncCompletedEventArgs completedArgs = new AsyncCompletedEventArgs(null, false, null);
            async.PostOperationCompleted(
                delegate(object e) { OnTaskCompleted((AsyncCompletedEventArgs)e); },
                completedArgs);
        }

        protected virtual void OnTaskCompleted(AsyncCompletedEventArgs e)
        {
            if (TaskCompleted != null) {
                TaskCompleted(this, e);
            }

            if (UploadProcess == false && respObject.Count > 0 && ConvertImageFiles.Count > 0 && NextTask != false)
                TaskAsync();

            if (TinyFiles.Count == 0 || TinyApiKeys.Count == 0)
            {
                BeginConvertButton.Enabled = true;
                menuStrip1.Enabled = true;

                //ProcessMessage.Items.Clear();
                //ProcessMessage.Visible = false;
                dataGridView1.Visible = true;
            }
        }

        #endregion

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
