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
using System.Xml.Linq;

namespace UDTProvider
{
    public class UDTProvider
    {
        private string _CommonPath;
        CRemoteHelper _objRemoteHelper;
        ConnectionStatus info;
        CUDTManagerHelper _mObjUdtHandler;
        public CUdtArgs CurrentUDT { get; set; }
        public DataSet CurrentDataSet { get;set;}
        public Dictionary<string, UdtFilter> UdtFilters;
        public delegate void FilterChangedEventHandler(string param);
        public event FilterChangedEventHandler FilterChanged;
        public UDTProvider()
        {
           _CommonPath =  Environment.GetEnvironmentVariable("Wasp3.5");
           CurrentDataSet = new DataSet("Soccer");
           UdtFilters = new Dictionary<string, UdtFilter>();
        }

       
        public bool InitializeConnection()
        {
          
           // string url=string.Empty;
            if (string.IsNullOrEmpty(_CommonPath))
                return false;
            var configfile= Path.Combine(_CommonPath, "CommonConfig.config");

            XDocument xdoc = XDocument.Load(configfile);
            var url = from lv1 in xdoc.Descendants("add")
                      where lv1.Attribute("key").Value == "LOCALMANAGERURL"
            select lv1.Attribute("value").Value ;
            _objRemoteHelper = new CRemoteHelper(url.ElementAt(0));
             info = _objRemoteHelper.Connect();
            _mObjUdtHandler = new CUDTManagerHelper(CRemoteHelper.GetDisconnectedUrl("UDTManager"));
           
             return true;
        }
        public void Notify(string changedParameter)
        {
            if (FilterChanged != null)
                FilterChanged(changedParameter);
        }
        public void InitializeUDT(string UdtName)
        {
            CurrentUDT = _mObjUdtHandler.GetUdtByName(UdtName);
            using (XmlReader reader = XmlReader.Create(new StringReader(CurrentUDT.FORMAT)))
            {
                CurrentDataSet.ReadXmlSchema(reader);
            }

            if (!string.IsNullOrEmpty(CurrentUDT.DATA))
            {
                using (XmlReader reader = XmlReader.Create(new StringReader(CurrentUDT.DATA)))
                {
                    CurrentDataSet.ReadXml(reader);
                }
            }
        }
        public void UpdateUDT(int tableIndex,string[] Columns,string[]Values,string primaryColumn,string PrimaryValue)
        {
            DataTable dtTable = CurrentDataSet.Tables[tableIndex];
            DataRow[] dr = dtTable.Select(primaryColumn + "='" + PrimaryValue+"'");
            for(int i=0;i<Columns.Count();i++)
            {
                dr[0][Columns[i]] = Values[i];
            }
            var udtTable = new UdtTable();
            udtTable.UDTROWDATA = dr[0].ItemArray.Select(o => o.ToString()).ToList();
            udtTable.UdtTableName = dr[0].Table.TableName;
            List<UdtTable> udtTables = new List<UdtTable> { udtTable };
            CurrentUDT.UDTTABLE = udtTables;
            _mObjUdtHandler.UpadteUdtRow(CurrentUDT);
        }

        public void InsertUDTData(int tableIndex, string[] Columns, string[] Values)
        {
            DataTable dtTable = CurrentDataSet.Tables[tableIndex];
            DataRow dr= dtTable.NewRow();
            for (int i = 0; i < Columns.Count(); i++)
            {
                dr[Columns[i]] = Values[i];
            }
            var udtTable = new UdtTable();
            udtTable.UDTROWDATA = dr.ItemArray.Select(o => o.ToString()).ToList();
            udtTable.UdtTableName = dr.Table.TableName;
            List<UdtTable> udtTables = new List<UdtTable> { udtTable };
            CurrentUDT.UDTTABLE = udtTables;
            _mObjUdtHandler.AddDefaultRow(CurrentUDT);
            //Testing merging at same time
            //sreenath added
        }

    }
    public struct UdtFilter
    {
        public int TableIndex;
        public string FilterColumn;
        public string FilterValue;

    }
}
