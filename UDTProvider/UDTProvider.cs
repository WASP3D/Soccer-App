using BeeSys.Wasp.Communicator;
using BeeSys.Wasp.KernelController;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Threading;

namespace UDTProvider
{
    public class UdtProvider : IDisposable
    {
        #region Members

        public ConnectRespArgs CnctArgs { get; set; }
        public DataConnectRespArgs CurrentUdt { get; set; }
        public DataSet CurrentDataSet { get; set; }
        public Dictionary<string, UdtFilter> UdtFilters;
        public delegate void FilterChangedEventHandler(string param);
        public event FilterChangedEventHandler FilterChanged;

        private readonly string CommonPath;
        private readonly object _lock = new object();
        private readonly object _threadLock = new object();
        private readonly AutoResetEvent _autoResetEvent = new AutoResetEvent(false);
        private readonly Queue<ActionParams> CommandsQueue;

        private CRemoteHelper _objRemoteHelper;
        private UDTManagerHelper _mObjUdtHandler = null;
        private UDTDataManagerHelper _mObjUdtDataHandler = null;
        private string UDTSESSIONID = string.Empty;
        private string UDTDATASESSIONID = string.Empty;
        private UdtMetaDataInfo _mudtinfo = null;
        private bool IsClosing = false;
        List<UdtInfo> allUdtes = null;
        string ServiceUrlKey = string.Empty;

        #endregion

        public UdtProvider(string serviceurlkey)
        {
            ServiceUrlKey = serviceurlkey;
            CommonPath = Environment.GetEnvironmentVariable("Wasp3.5");
            CurrentDataSet = new DataSet("Soccer");
            UdtFilters = new Dictionary<string, UdtFilter>();
            CommandsQueue = new Queue<ActionParams>();
            ThreadPool.QueueUserWorkItem(new WaitCallback(DequeueCommands));
        }

        public bool InitializeConnection()
        {
            System.Diagnostics.Debug.WriteLine("InitializeConnection _CommonPath:" + CommonPath);
            if (string.IsNullOrEmpty(CommonPath))
                return false;
            var configfile = Path.Combine(CommonPath, "CommonConfig.config");
            System.Diagnostics.Debug.WriteLine("InitializeConnection configfile:" + configfile);
            XDocument xdoc = XDocument.Load(configfile);
            var url = from lv1 in xdoc.Descendants("add")
                      where lv1.Attribute("key").Value == ServiceUrlKey
                      select lv1.Attribute("value").Value;
            _objRemoteHelper = new CRemoteHelper(url.ElementAt(0));
            ConnectionInfo _objConnection = _objRemoteHelper.CheckConnection();
            System.Diagnostics.Debug.WriteLine("InitializeConnection _objConnection.status:" + _objConnection.status);
            if (_objConnection.status == Status.Connected)
            {
                _mObjUdtHandler = new UDTManagerHelper(_objRemoteHelper.GetUrl("UDTManager").sEndpointAddress);
                _mObjUdtDataHandler = new UDTDataManagerHelper(_objRemoteHelper.GetUrl("UDTDataManager").sEndpointAddress);
                UDTSESSIONID = _mObjUdtHandler.Connect();
                System.Diagnostics.Debug.WriteLine("InitializeConnection UDTSESSIONID:" + UDTSESSIONID);
                UDTDATASESSIONID = _mObjUdtDataHandler.Connect();
                System.Diagnostics.Debug.WriteLine("InitializeConnection UDTDATASESSIONID:" + UDTDATASESSIONID);
                return true;
            }

            return false;
        }

        public void Notify(string changedParameter)
        {
            if (FilterChanged != null)
                FilterChanged(changedParameter);
        }

        public bool InitializeUdt(string UdtName)
        {
            bool bResult = false;
            System.Diagnostics.Debug.WriteLine("InitializeUDT UdtName:" + UdtName);
            if (allUdtes == null)
            {
                allUdtes = _mObjUdtHandler.GetAllUdt();
            }
            if (allUdtes != null)
            {
                LoadUdt(UdtName);
                CurrentDataSet = GetUdtDataset(CurrentUdt);
                bResult = true;
            }
            else
                bResult = false;

            return bResult;
        }

        public void RefreshUdt(string UdtName)
        {
            if (_mObjUdtHandler == null || _mObjUdtDataHandler == null)
            {
                Clear();
                _mObjUdtHandler = new UDTManagerHelper(_objRemoteHelper.GetUrl("UDTManager").sEndpointAddress);
                _mObjUdtDataHandler = new UDTDataManagerHelper(_objRemoteHelper.GetUrl("UDTDataManager").sEndpointAddress);

                UDTSESSIONID = _mObjUdtHandler.Connect();
                UDTDATASESSIONID = _mObjUdtDataHandler.Connect();
            }

            LoadUdt(UdtName);

            CurrentDataSet = GetUdtDataset(CurrentUdt);
        }

        public void UpdateUdt(int tableIndex, string[] Columns, string[] Values, string primaryColumn, string PrimaryValue, bool needrefresh)
        {
            ActionParams actionparams = new ActionParams
            {
                TableIndex = tableIndex,
                Columns = Columns,
                Values = Values,
                PrimaryColumn = primaryColumn,
                PrimaryValue = PrimaryValue,
                NeedRefresh = needrefresh,
                ActionType = ActionType.Update
            };
            EnqueueCommands(actionparams);
        }

        public void InsertUdt(int tableIndex, string[] Columns, string[] Values, bool needrefresh)
        {
            ActionParams actionparams = new ActionParams
            {
                TableIndex = tableIndex,
                Columns = Columns,
                Values = Values,
                NeedRefresh = needrefresh,
                ActionType = ActionType.Insert
            };
            EnqueueCommands(actionparams);
        }

        public void UpdateUdt(ActionParams actionparams)
        {
            System.Diagnostics.Debug.WriteLine("UpdateUDT Start:" + DateTime.Now.ToString("hh:mm:ss ffff"));
            if (actionparams.NeedRefresh)
            {
                RefreshUdt(CnctArgs.Udtname);
            }
            System.Diagnostics.Debug.WriteLine("UpdateUDT Start 1:" + DateTime.Now.ToString("hh:mm:ss ffff"));
            DataTable dtTable = CurrentDataSet.Tables[actionparams.TableIndex];
            DataRow[] dr = dtTable.Select(actionparams.PrimaryColumn + "='" + actionparams.PrimaryValue + "'");
            for (int i = 0; i < actionparams.Columns.Count(); i++)
            {
                dr[0][actionparams.Columns[i]] = actionparams.Values[i];
            }

            UdtArgs args = new UdtArgs();
            UpdateRowRespArgs respargs = new UpdateRowRespArgs();
            respargs.TableName = dr[0].Table.TableName;
            respargs.Data = Array.ConvertAll(dr[0].ItemArray, x => x.ToString());
            args.Action = "updaterow";
            UdtParams udtparams = new UdtParams();
            udtparams.UpdateRowParams = respargs;
            args.ActionParams = new UdtParams[1] { udtparams };
            args.Udtid = _mudtinfo.UdtId;
            args.UserName = Environment.UserName;
            System.Diagnostics.Debug.WriteLine("UpdateUDT End 0:" + DateTime.Now.ToString("hh:mm:ss ffff"));
            _mObjUdtDataHandler.UpdateRow(args);
            System.Diagnostics.Debug.WriteLine("UpdateUDT End:" + DateTime.Now.ToString("hh:mm:ss ffff"));
        }

        public void InsertUdt(ActionParams actionparams)
        {
            System.Diagnostics.Debug.WriteLine("InsertUDTData Start:" + DateTime.Now.ToString("hh:mm:ss ffff"));
            if (actionparams.NeedRefresh)
            {
                RefreshUdt(CnctArgs.Udtname);
            }
            System.Diagnostics.Debug.WriteLine("InsertUDTData Start 1:" + DateTime.Now.ToString("hh:mm:ss ffff"));
            DataTable dtTable = CurrentDataSet.Tables[actionparams.TableIndex];
            DataRow dr = dtTable.NewRow();
            for (int i = 0; i < actionparams.Columns.Count(); i++)
            {
                dr[actionparams.Columns[i]] = actionparams.Values[i];
            }
            if (dr.Table.Columns.Count > 1 && dr.Table.Columns[1].ColumnName.Contains("_ID") && dtTable.Rows.Count > 0)
            {
                var parentkey = dtTable.Rows[0][1];
                string parentTable = string.Empty;
                if (string.IsNullOrEmpty(Convert.ToString(parentkey)))
                {
                    for (int i = 0; i < CurrentDataSet.Relations.Count; i++)
                    {
                        if (CurrentDataSet.Relations[i].ChildTable.TableName == dr.Table.TableName)
                        {
                            parentTable = CurrentDataSet.Relations[i].ParentTable.TableName;
                            break;
                        }
                    }
                    if (!string.IsNullOrEmpty(parentTable))
                    {
                        DataTable dtParent = CurrentDataSet.Tables[parentTable];
                        string columnName = "T" + parentTable + "_ID";
                        if (dtParent != null && dtParent.Rows.Count > 0 && dtParent.Columns.Contains(columnName))
                        {
                            parentkey = dtParent.Rows[0][columnName];

                        }
                    }
                }

                dr[1] = parentkey;

            }
            if (dr.Table.Columns.Contains("Visibility") && string.IsNullOrEmpty(Convert.ToString(dr["Visibility"])))
            {
                dr["Visibility"] = true;
            }
            if (!actionparams.Columns.Contains("ID") && dtTable.Columns.Contains("ID"))
            {
                DataRow drlast = dtTable.Rows[dtTable.Rows.Count - 1];
                int id = 0;
                if (drlast != null && drlast["ID"] != null)
                {
                    int.TryParse(Convert.ToString(drlast["ID"]), out id);
                }
                id++;
                dr["ID"] = id;
            }
            UdtArgs args = new UdtArgs();
            AddRowRespArgs respargs = new AddRowRespArgs();
            respargs.TableName = dr.Table.TableName;
            respargs.Data = Array.ConvertAll(dr.ItemArray, x => x.ToString());
            args.Action = "addrow";
            UdtParams udtparams = new UdtParams();
            udtparams.AddRowParams = respargs;
            args.ActionParams = new UdtParams[1] { udtparams };
            args.Udtid = _mudtinfo.UdtId;
            args.UserName = Environment.UserName;
            System.Diagnostics.Debug.WriteLine("InsertUDTData End 0:" + DateTime.Now.ToString("hh:mm:ss ffff"));
            _mObjUdtDataHandler.AddRow(args);

            //Testing merging at same time
            //sreenath added

            System.Diagnostics.Debug.WriteLine("InsertUDTData End:" + DateTime.Now.ToString("hh:mm:ss ffff"));
        }

        private void EnqueueCommands(ActionParams actionparams)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("UpdateUDT Enqueue Start:" + DateTime.Now.ToString("hh:mm:ss ffff"));
                lock (_lock)
                {
                    CommandsQueue.Enqueue(actionparams);
                }
                System.Diagnostics.Debug.WriteLine("UpdateUDT Enqueue End:" + DateTime.Now.ToString("hh:mm:ss ffff"));
                _autoResetEvent.Set();
            }
            catch { }
        }

        private void DequeueCommands(object objState)
        {
            while (!IsClosing)
            {
                lock (_threadLock)
                {
                    try
                    {
                        if (CommandsQueue.Count > 0)
                        {
                            ActionParams data;
                            lock (_lock)
                            {
                                data = CommandsQueue.Dequeue();
                            }
                            if (data != null)
                            {
                                switch (data.ActionType)
                                {
                                    case ActionType.Insert:
                                        InsertUdt(data);
                                        break;
                                    case ActionType.Update:
                                        UpdateUdt(data);
                                        break;
                                }
                            }
                        }
                        else
                        {
                            _autoResetEvent.WaitOne(2000);
                        }
                    }
                    catch (ThreadAbortException ex)
                    {
                        Thread.ResetAbort();
                    }
                    catch { }
                }
            }
        }

        private void LoadUdt(string udtname)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("LoadUDT udtname:" + udtname);

                var args = allUdtes.FirstOrDefault(x => x.Udtname.Equals(udtname));
                System.Diagnostics.Debug.WriteLine("LoadUDT 1");
                if (args != null)
                {
                    _mudtinfo = new UdtMetaDataInfo();
                    _mudtinfo.UdtId = args.Udtid;
                    _mudtinfo.UdtName = args.Udtname;
                    CnctArgs = _mObjUdtHandler.LoadUdt(UDTSESSIONID, args.Udtid);
                    CurrentUdt = _mObjUdtDataHandler.LoadUdt(UDTDATASESSIONID, args.Udtid);
                    System.Diagnostics.Debug.WriteLine("LoadUDT 2");
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("LoadUDT ex:" + ex.Message);
            }
        }

        private void Clear()
        {
            if (_mObjUdtHandler != null)
            {
                if (!string.IsNullOrEmpty(UDTSESSIONID))
                {
                    if (_mudtinfo != null)
                    {
                        _mObjUdtHandler.UnloadUdt(UDTSESSIONID, _mudtinfo.UdtId);
                    }
                    _mObjUdtHandler.DisConnect(UDTSESSIONID);
                }
                _mObjUdtHandler.Dispose();
                _mObjUdtHandler = null;
            }
            if (_mObjUdtDataHandler != null)
            {
                if (!string.IsNullOrEmpty(UDTDATASESSIONID))
                {
                    if (_mudtinfo != null)
                    {
                        _mObjUdtDataHandler.UnloadUdt(UDTDATASESSIONID, _mudtinfo.UdtId);
                    }
                    _mObjUdtDataHandler.DisConnect(UDTDATASESSIONID);
                }
                _mObjUdtDataHandler.Dispose();
                _mObjUdtDataHandler = null;
            }
            UDTSESSIONID = string.Empty;
            UDTDATASESSIONID = string.Empty;
            CnctArgs = null;
            CurrentUdt = null;
            if (CurrentDataSet != null)
            {
                CurrentDataSet.Dispose();
                CurrentDataSet = null;
            }
        }

        private DataSet GetUdtDataset(DataConnectRespArgs args)
        {
            DataSet dataSet = null;
            try
            {
                if (args != null)
                {
                    dataSet = new DataSet(args.Udtid);
                    if (!string.IsNullOrEmpty(args.Schema))
                    {
                        using (XmlReader reader = XmlReader.Create(new StringReader(args.Schema)))
                        {
                            dataSet.ReadXmlSchema(reader);
                        }

                        if (!string.IsNullOrEmpty(args.Data))
                        {
                            using (XmlReader reader = XmlReader.Create(new StringReader(args.Data)))
                            {
                                dataSet.ReadXml(reader);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                dataSet = null;
            }

            return dataSet;
        }

        public void Dispose()
        {
            IsClosing = true;
            if (_objRemoteHelper != null)
            {
                _objRemoteHelper.Dispose();
                _objRemoteHelper = null;
            }
            Clear();
        }
    }

    public struct UdtFilter
    {
        public int TableIndex;
        public string FilterColumn;
        public string FilterValue;

    }

    public class UdtMetaDataInfo
    {
        public string UdtId { get; set; }
        public string UdtName { get; set; }
    }

    public class ActionParams
    {
        public int TableIndex { get; set; }

        public string[] Columns { get; set; }

        public string[] Values { get; set; }

        public string PrimaryColumn { get; set; }

        public string PrimaryValue { get; set; }

        public bool NeedRefresh { get; set; }

        public ActionType ActionType { get; set; }

    }

    public enum ActionType
    {
        Insert,
        Update
    }
}
