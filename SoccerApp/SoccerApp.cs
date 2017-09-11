using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using UDTProvider;
using Beesys.Wasp.Workflow;
using BeeSys.Wasp.Communicator;
using System.Xml.Linq;
using System.IO;
using BeeSys.Wasp.KernelController;
using System.Xml;
using BeeSys.Wasp3D.FormDesigner2;
using BeeSys.Wasp3D.Controls2;
using System.Reflection;
namespace SoccerApp
{
    public partial class SoccerApp : Form
    {
        #region Class Members

        private UDTProvider.UdtProvider m_objUDTMatchSchedule;
        private UDTProvider.UdtProvider m_objUDTMatch;
        private string ActiveMatch = string.Empty;
        private DateTime CountDownTarget = DateTime.Now;
        private int SrNo = 1;
        private DateTime MatchPartStartTime;
        private CWaspFileHandler m_objWaspFileHandler;
        private CRemoteHelper m_objRemoteHelper;
        private string m_sCommonPath;
        private SceneHandler m_objsceneHandler = null;
        private List<ScenInfo> m_lstSceneCollection = null;
        private IPlayer m_objPlayer = null;
        private string NAME = string.Empty;
        private string MATCHUDTNAME = string.Empty;
        private string MATCHUDTID = string.Empty;
        private IPlayer m_objplayertodelete = null;
        private PlayerGetter objPlayergetter = null;
        private const string m_surlformat = "net.tcp://{0}:{1}/TcpBinding/WcfTcpLink";
        private string m_serverurl = string.Empty;
        private string hometeamshortname = string.Empty;
        private string awayteamshortname = string.Empty;
        private readonly bool isformloaded = false;

        #endregion

        #region Constructor

        public SoccerApp()
        {
            try
            {
                InitializeComponent();
                Init();
                isformloaded = true;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializing the components
        /// </summary>
        private void Init()
        {
            try
            {
                m_serverurl = string.Format(m_surlformat, ConfigurationManager.AppSettings["stingserverip"], ConfigurationManager.AppSettings["stingserverport"]);
                m_objWaspFileHandler = new CWaspFileHandler();
                m_objsceneHandler = new SceneHandler();
                objPlayergetter = new PlayerGetter();
                SetUI();
                InitializeWasp();
                InitilizeMatchScheduleUdt();
                InitializeCombos();
                FillMatchDetails();
                Fillgrid();
                m_objsceneHandler.FileHandler = m_objWaspFileHandler;
                m_objsceneHandler.Initialize();
                m_objsceneHandler.Hometeamscore = lblHomeScore.Text;
                m_objsceneHandler.Awayteamscore = lblAwayScore.Text;
                m_objsceneHandler.Hometeamshortname = hometeamshortname;
                m_objsceneHandler.Awayteamshortname = awayteamshortname;
                m_objsceneHandler.SetMatchUdt();
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// Set the UI controls colors and set selection index of combos
        /// </summary>
        private void SetUI()
        {
            lblCounter.ForeColor = Color.FromArgb(0, 142, 188);
            pnlHmTeam.BackColor = Color.FromArgb(228, 228, 228);
            pnlAwayTeam.BackColor = Color.FromArgb(228, 228, 228);
            pnlcntr.BackColor = Color.FromArgb(228, 228, 228);
            pnlMActions.BackColor = Color.FromArgb(86, 110, 123);
            btnFoul.BackColor = Color.FromArgb(182, 182, 182);
            btnYellow.BackColor = Color.FromArgb(182, 182, 182);
            btnShots.BackColor = Color.FromArgb(182, 182, 182);
            btnShotsOff.BackColor = Color.FromArgb(182, 182, 182);
            btnCorner.BackColor = Color.FromArgb(182, 182, 182);
            btnRed.BackColor = Color.FromArgb(182, 182, 182);
            btnSubstitute.BackColor = Color.FromArgb(182, 182, 182);
            pnlPlay.BackColor = Color.FromArgb(86, 109, 123);
            pnlActions.BackColor = Color.FromArgb(86, 109, 123);
            dgvMatchevents.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(176, 174, 175);
            dgvMatchevents.EnableHeadersVisualStyles = false;
            lblCounter.BackColor = Color.FromArgb(228, 228, 228);
            cmbMatch.BackColor = Color.FromArgb(182, 182, 182);
            cmbMatchPart.BackColor = Color.FromArgb(182, 182, 182);
            pnlActions.BackColor = Color.FromArgb(196, 196, 196);
            pnlLabels.BackColor = Color.FromArgb(86, 109, 123);
            lblMatchevents.ForeColor = Color.FromArgb(51, 51, 51);
            pnlMatch.BackColor = Color.FromArgb(196, 196, 196);

            cmbMatch.SelectedIndex = -1;
            cmbMatchPart.SelectedIndex = -1;
        }

        /// <summary>
        /// Create wasp kc connection and get template manager object
        /// </summary>
        private void InitializeWasp()
        {
            try
            {
                m_sCommonPath = Environment.GetEnvironmentVariable("Wasp3.5");
                System.Diagnostics.Debug.WriteLine("InitializeWasp Commonpath:" + m_sCommonPath);
                var configfile = Path.Combine(m_sCommonPath, "CommonConfig.config");
                System.Diagnostics.Debug.WriteLine("InitializeWasp configfile:" + configfile);
                XDocument xdoc = XDocument.Load(configfile);
                var url = from lv1 in xdoc.Descendants("add")
                          where lv1.Attribute("key").Value == "LOCALMANAGERURL"
                          select lv1.Attribute("value").Value;
                System.Diagnostics.Debug.WriteLine("InitializeWasp Url:" + url.ElementAt(0));
                m_objRemoteHelper = new CRemoteHelper(url.ElementAt(0));
                ConnectionInfo info = m_objRemoteHelper.CheckConnection();
                if (info.status == Status.Connected)
                {
                    ServiceUrl templatemgrserviceurl = CRemoteHelper.GetDisconnectedUrl("TemplateManager");
                    m_objWaspFileHandler = new CWaspFileHandler();
                    m_objWaspFileHandler.Initialize(templatemgrserviceurl);
                    LoadTemplates();
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// Fill templates list by reading the templates.xml
        /// </summary>
        private void LoadTemplates()
        {
            try
            {
                if (m_lstSceneCollection == null)
                {
                    m_lstSceneCollection = new List<ScenInfo>();
                }
                XDocument xdoc = XDocument.Load("templates.xml");
                var templates = xdoc.Descendants("template");
                foreach (var item in templates)
                {
                    ScenInfo sc = new ScenInfo();
                    sc.Id = item.Attribute("id").Value;
                    sc.Name = item.Attribute("name").Value;
                    sc.Description = item.Attribute("description").Value;
                    bool ismanual = false;
                    bool.TryParse(item.Attribute("ismanual").Value, out ismanual);
                    sc.ismanual = ismanual;
                    sc.inuse = false;
                    m_lstSceneCollection.Add(sc);

                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// Initialize SoccerMatchSchedule udt by creating its UdtProvider object
        /// </summary>
        private void InitilizeMatchScheduleUdt()
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["udtname"]))
            {
                try
                {
                    if (m_objUDTMatchSchedule != null)
                    {
                        m_objUDTMatchSchedule.Dispose();
                        m_objUDTMatchSchedule = null;
                    }
                    m_objUDTMatchSchedule = new UDTProvider.UdtProvider();
                    m_objUDTMatchSchedule.InitializeConnection();
                    m_objUDTMatchSchedule.InitializeUdt(ConfigurationManager.AppSettings["udtname"]);
                }
                catch (Exception ex)
                {
                    LogWriter.WriteLog(ex);
                }
            }
        }

        /// <summary>
        /// Initialize SoccerMatch udt by creating its UdtProvider object
        /// </summary>
        private void InitializeMatchUdt()
        {
            if (!string.IsNullOrEmpty(MATCHUDTNAME))
            {
                if (m_objUDTMatch != null)
                {
                    m_objUDTMatch.Dispose();
                    m_objUDTMatch = null;
                }
                m_objUDTMatch = new UDTProvider.UdtProvider();
                m_objUDTMatch.InitializeConnection();
                m_objUDTMatch.InitializeUdt(MATCHUDTNAME);
                MATCHUDTID = m_objUDTMatch.CnctArgs.Udtid;
            }
            else
            {
                MATCHUDTID = string.Empty;
                m_objUDTMatch = null;
            }
        }

        /// <summary>
        /// Fill properties of match combos and call method to fill combo
        /// </summary>
        private void InitializeCombos()
        {
            cmbMatch.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbMatch.AutoCompleteSource = AutoCompleteSource.CustomSource;
            // Get the Match List from UDT and add to cmbstr, Fill the Combo, and set the selected Item
            FillMatchList();
            cmbMatchPart.AutoCompleteMode = AutoCompleteMode.Suggest;
            cmbMatchPart.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmbMatchPart.SelectedIndex = 0;

            // Get the Match part List from UDT and add to cmbstr1, Fill the Combo, and set the selected Item
        }

        /// <summary>
        /// Fill match list by reading data from SoccerMatchSchedule udt
        /// </summary>
        private void FillMatchList()
        {
            try
            {
                DataSet dt = m_objUDTMatchSchedule.CurrentDataSet;
                var dtmatch = dt.Tables[1];
                AutoCompleteStringCollection cmbstr = new AutoCompleteStringCollection();
                foreach (DataRow item in dtmatch.Rows)
                {
                    cmbstr.Add(item["Name"].ToString());
                    cmbMatch.Items.Add(item["Name"].ToString());
                }
                cmbMatch.AutoCompleteCustomSource = cmbstr;
                DataRow[] dr = dtmatch.Select("Active=true");
                if (dr.Count() > 0)
                {
                    MATCHUDTNAME = dr[0]["UDT Name"].ToString();
                    int index = cmbMatch.FindString(dr[0]["Name"].ToString());
                    if (index != -1)
                    {
                        try
                        {
                            cmbMatch.SelectedIndex = index;
                        }
                        catch { }
                        UdtFilter filter = new UdtFilter();
                        filter.FilterColumn = "Name";
                        filter.FilterValue = cmbMatch.Text;
                        filter.TableIndex = 5;
                        if (!m_objUDTMatchSchedule.UdtFilters.ContainsKey("Active Match"))
                            m_objUDTMatchSchedule.UdtFilters.Add("Active Match", filter);
                        else
                            m_objUDTMatchSchedule.UdtFilters["Active Match"] = filter;
                        m_objUDTMatchSchedule.Notify("Active Match");
                    }

                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// Set active match udt name and initialize its udts
        /// </summary>
        /// <param name="matchname"></param>
        private void SetMatchUdtName()
        {
            try
            {
                DataSet dt = m_objUDTMatchSchedule.CurrentDataSet;
                var dtmatch = dt.Tables[1];
                DataRow[] dr = dtmatch.Select("Active=true");
                MATCHUDTNAME = dr[0]["UDT Name"].ToString();
                InitializeMatchUdt();
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// Fill the match events on the grid by reading data from MatchEvents udt table of SoccerMatch
        /// </summary>
        private void FillMatchEvents()
        {
            try
            {
                if (m_objUDTMatch != null)
                {
                    DataTable dt = m_objUDTMatch.CurrentDataSet.Tables[5];
                    foreach (DataRow item in dt.Rows)
                    {
                        DataGridViewRow dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
                        dgv.Cells[0].Value = item["EventID"].ToString(); //SrNo
                        dgv.Cells[1].Value = item["Time"].ToString(); //Time
                        dgv.Cells[2].Value = item["MatchPart"].ToString(); //Match Part
                        dgv.Cells[3].Value = item["EventType"].ToString(); //Event memo
                        dgv.Cells[4].Value = item["Team"].ToString();//team Name
                        dgv.Cells[5].Value = item["Player"].ToString();//player Name
                        dgvMatchevents.Rows.Add(dgv);
                        int sr = Convert.ToInt32(item["EventID"].ToString());
                        if (SrNo < sr)
                        {
                            SrNo = sr;
                        }
                    }
                    SrNo++;
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// Selects the team on the basis of active match
        /// </summary>
        /// <param name="ActiveMatch"></param>
        private void SelectTeams(string ActiveMatch)
        {
            try
            {
                DataTable dtmatch = m_objUDTMatchSchedule.CurrentDataSet.Tables[1];
                DataRow[] dr = dtmatch.Select("Name= '" + ActiveMatch + "'");
                if (dr.Count() > 0)
                {
                    lblHomeTeam.Text = dr[0]["Home Team"].ToString();
                    lblAwayTeam.Text = dr[0]["Away Team"].ToString();
                    if (File.Exists(dr[0]["HomeTeam_Logo"].ToString()))
                    {
                        pnlHomeFlag.BackgroundImage = Image.FromFile(dr[0]["HomeTeam_Logo"].ToString());
                    }
                    if (File.Exists(dr[0]["AwayTeam_Logo"].ToString()))
                    {
                        pnlAwayFlag.BackgroundImage = Image.FromFile(dr[0]["AwayTeam_Logo"].ToString());
                    }
                    InitializeScores(ActiveMatch);
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// Get the team scores and names and set it to the score bug wired variable 
        /// </summary>
        /// <param name="ActiveMatch"></param>
        private void InitializeScores(string ActiveMatch)
        {
            try
            {
                DataRow[] dr = m_objUDTMatch.CurrentDataSet.Tables[4].Select("Match= '" + ActiveMatch + "'");
                lblHomeScore.Text = dr[0]["HomeGoal"].ToString();
                lblAwayScore.Text = dr[0]["AwayGoal"].ToString();
                DataTable dtteams = m_objUDTMatch.CurrentDataSet.Tables[1];
                if (dtteams != null && dtteams.Rows.Count > 0)
                {
                    hometeamshortname = dtteams.Rows[0]["Short Name_HT"].ToString();
                    awayteamshortname = dtteams.Rows[0]["Short Name_AT"].ToString();
                }
                m_objsceneHandler.Hometeamscore = lblHomeScore.Text;
                m_objsceneHandler.Awayteamscore = lblAwayScore.Text;
                m_objsceneHandler.Hometeamshortname = hometeamshortname;
                m_objsceneHandler.Awayteamshortname = awayteamshortname;
                m_objsceneHandler.SetMatchUdt();
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// Update the Match Statistics table by checking the existance
        /// </summary>
        /// <param name="Column"></param>
        /// <param name="value"></param>
        /// <param name="uniqueColumn"></param>
        /// <param name="uniqueValue"></param>
        private void UpdateMatchStats(string Column, int value, string uniqueColumn, string uniqueValue)
        {
            try
            {
                if (isformloaded)
                {
                    m_objUDTMatch.RefreshUdt(MATCHUDTNAME); // It will refresh the UDT and Dataset and get latest values available in UDT
                }
                if (m_objUDTMatch.CurrentDataSet != null)
                {
                    DataRow[] dr = m_objUDTMatch.CurrentDataSet.Tables[4].Select(uniqueColumn + "= '" + uniqueValue + "'");
                    if (dr != null && dr.Length > 0 && (Convert.ToInt32(dr[0][Column]) > 0 || value == 1))
                    {
                        int val = Convert.ToInt32(dr[0][Column].ToString()) + value;
                        m_objUDTMatch.UpdateUdt(4, new string[] { Column }, new string[] { val.ToString() }, uniqueColumn, uniqueValue, isformloaded);
                    }
                    else if (value != -1)
                    {
                        DataRow drlast = m_objUDTMatch.CurrentDataSet.Tables[4].Rows[m_objUDTMatch.CurrentDataSet.Tables[4].Rows.Count - 1];
                        int id = 0;
                        if (drlast != null)
                        {
                            int.TryParse(Convert.ToString(drlast["ID"]), out id);
                        }
                        id++;
                        int val = value;
                        m_objUDTMatch.InsertUdt(4, new string[] { "ID", uniqueColumn, Column }, new string[] { id.ToString(), uniqueValue, val.ToString() }, isformloaded);
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }

        }

        /// <summary>
        /// Fills the Datagridview to select players.
        /// </summary>
        private void Fillgrid()
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Column2", typeof(bool));
                dt.Columns.Add("Column3", typeof(bool));
                foreach (var item in m_lstSceneCollection)
                {
                    if (item.ismanual)
                    {
                        dr = dt.NewRow();
                        dr["Name"] = item.Description;
                        dr["Column2"] = false;
                        dr["Column3"] = false;
                        dt.Rows.Add(dr);
                    }
                }
                dgvSelectPlayer.DataSource = dt;
                dgvSelectPlayer.CellContentClick += dataGridView2_CellContentClick;
                dgvSelectPlayer.CurrentCellDirtyStateChanged += dataGridView2_CurrentCellDirtyStateChanged;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }

        }

        /// <summary>
        /// Method to get template details and player
        /// </summary>
        /// <param name="str"></param>
        /// <param name="column"></param>
        private void GetTemplate(string str, string column)
        {
            try
            {
                string columnname = column;
                ScenInfo si = m_lstSceneCollection.Where(s => s.Description == str).FirstOrDefault();
                Beesys.Wasp.Workflow.TemplateInfo obj = objPlayergetter.GetPlayerInfo(si.Id);
                m_objPlayer = Activator.CreateInstance(obj.TemplatePlayerInfo) as IPlayer;
                Form objfrm = m_objPlayer as Form;
                SetMatchUdt(objfrm);
                if (m_objPlayer != null)
                {
                    m_objPlayer.Init("", "", si.Id, "");
                    m_objPlayer.SetLink(m_objsceneHandler.AppLink, obj.MetaDataXml);
                    //S. No 116: Added for AddIn
                    //S.No.	: -	128
                    if (m_objPlayer is IAddinInfo)
                        (m_objPlayer as IAddinInfo).Init(new InstanceInfo() { InstanceId = si.Id });

                    IDataEntry objDataentry = m_objPlayer as IDataEntry;
                    if (objDataentry != null)
                    {
                        objDataentry.PostData += objDataentry_PostData;
                    }

                    IChannelShotBox objChannelShotBox = m_objPlayer as IChannelShotBox;
                    if (objChannelShotBox != null)
                    {
                        objChannelShotBox.SetEngineUrl(m_serverurl);
                    }

                    m_objPlayer.OnShotBoxControllerStatus += objPlayer_OnShotBoxControllerStatus;
                    m_objPlayer.Prepare(m_serverurl, Convert.ToInt32(m_objPlayer.ZORDER), string.Empty, RENDERMODE.PROGRAM);
                }
                if (columnname != "-1")
                {
                    objfrm.TopLevel = false;
                    objfrm.Visible = false;
                    objfrm.Dock = DockStyle.Fill;
                    Control ctl = null;
                    Control ctl2 = null;
                    if (string.Compare(columnname, "Column2", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        ctl = tableLayoutPanel1.GetControlFromPosition(0, 0);
                        if (ctl != null) // Already control present there
                        {
                            //First play out the graphic.
                            //then delete the gfx                            
                            tableLayoutPanel1.Controls.Remove(ctl);
                            (ctl as IPlayer).DeleteSg();
                            tableLayoutPanel1.Controls.Add(objfrm, 0, 0);
                        }
                    }

                    if (string.Compare(columnname, "Column3", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        ctl2 = tableLayoutPanel1.GetControlFromPosition(1, 0);
                        if (ctl2 != null) // Already control present there
                        {
                            //First play out the graphic.
                            //then delete the gfx
                            tableLayoutPanel1.Controls.Remove(ctl2);
                            (ctl2 as IPlayer).DeleteSg();
                            tableLayoutPanel1.Controls.Add(objfrm, 1, 0);
                        }
                    }
                    if (ctl == null && (string.Compare(columnname, "Column2",StringComparison.OrdinalIgnoreCase) == 0))
                    {
                        tableLayoutPanel1.Controls.Add(objfrm, 0, 0);
                    }
                    else if (ctl2 == null && (string.Compare(columnname, "Column3", StringComparison.OrdinalIgnoreCase) == 0))
                    {
                        tableLayoutPanel1.Controls.Add(objfrm, 1, 0);
                    }
                    objfrm.Show();
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// Set match udt name in the udt sequecer object
        /// </summary>
        /// <param name="frmobj"></param>
        private void SetMatchUdt(Form frmobj)
        {

            Control frmctrl = frmobj as Control;

            foreach (Control ctrl in frmobj.Controls)
            {
                if (ctrl is BeeSys.Wasp3D.Controls2.TextBox)
                {
                    var matchudtnamectrl = ctrl as BeeSys.Wasp3D.Controls2.TextBox;
                    matchudtnamectrl.Text = MATCHUDTNAME;
                }
            }

            FieldInfo[] objfieldinfo = frmctrl.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var objfield in objfieldinfo)
            {
                object o = objfield.GetValue(frmctrl);
                if (o != null)
                {
                    Component objComponent = o as Component;
                    if (objComponent != null && objComponent is UDT && !string.IsNullOrEmpty(MATCHUDTID))
                    {
                        UDT objudt = (UDT)objComponent;
                        objudt.SelectedUdtId = MATCHUDTID;
                        objudt.UDTNames = MATCHUDTNAME;
                        objudt.ExecuteQuery(MATCHUDTID, objudt.Filter);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Loading match events player 
        /// </summary>
        /// <param name="eventid"></param>
        private void LoadMatchEventGrahicPlayer()
        {
            try
            {
                bool oldP1 = false;
                bool oldP2 = false;
                MatchGraphicPlayerSelector mgps = new MatchGraphicPlayerSelector();
                oldP1 = PlayerControlCheck(1, 0);
                oldP2 = PlayerControlCheck(2, 1);
                mgps.P1 = oldP1;
                mgps.P2 = oldP2;
                mgps.StartPosition = FormStartPosition.CenterParent;
                mgps.ShowDialog();
                if (mgps.P1)
                {
                    foreach (DataGridViewRow dr in dgvSelectPlayer.Rows)
                    {
                        dr.Cells[1].Value = false;
                    }
                    if (oldP1 != mgps.P1)
                    {
                        GetTemplate("matchevent", "Column2");
                    }
                }
                if (mgps.P2)
                {
                    foreach (DataGridViewRow dr in dgvSelectPlayer.Rows)
                    {
                        dr.Cells[2].Value = false;
                    }
                    if (oldP2 != mgps.P2)
                    {
                        GetTemplate("matchevent", "Column3");
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// check player controls
        /// </summary>
        /// <param name="columnindex"></param>
        /// <param name="columnpos"></param>
        /// <returns></returns>
        private bool PlayerControlCheck(int columnindex, int columnpos)
        {

            bool isAllfalse = true;

            foreach (DataGridViewRow dr in dgvSelectPlayer.Rows)
            {
                if ((bool)dr.Cells[columnindex].Value)
                {
                    isAllfalse = false;
                    break;
                }
            }
            if (isAllfalse && tableLayoutPanel1.GetControlFromPosition(columnpos, 0) != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Change the active match and fill the data according to the new match
        /// </summary>
        private void FillMatchDetails()
        {
            try
            {
                UdtFilter filter = new UdtFilter();
                filter.FilterColumn = "Name";
                filter.FilterValue = cmbMatch.Text;
                filter.TableIndex = 1;
                if (!m_objUDTMatchSchedule.UdtFilters.ContainsKey("Active Match"))
                    m_objUDTMatchSchedule.UdtFilters.Add("Active Match", filter);
                else
                    m_objUDTMatchSchedule.UdtFilters["Active Match"] = filter;

                foreach (string item in cmbMatch.Items)
                {
                    if (item == cmbMatch.Text)
                    {
                        m_objUDTMatchSchedule.UpdateUdt(1, new string[] { "Active" }, new string[] { "true" }, "Name", cmbMatch.Text, isformloaded);
                    }
                    else
                    {
                        m_objUDTMatchSchedule.UpdateUdt(1, new string[] { "Active" }, new string[] { "false" }, "Name", item, isformloaded);
                    }
                }
                m_objUDTMatchSchedule.Notify("Active Match");
                ActiveMatch = cmbMatch.Text;
                if (m_objUDTMatchSchedule != null)
                {
                    DataTable dt = m_objUDTMatchSchedule.CurrentDataSet.Tables[1];
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if ((bool)row["Active"])
                            {
                                NAME = row["NAME"].ToString();
                                SetMatchUdtName();
                            }
                        }
                    }
                }
                SelectTeams(ActiveMatch);
                dgvMatchevents.Rows.Clear();
                FillMatchEvents();
                ClearPlayerControlls();
                lblMatchname.Text = lblHomeTeam.Text + " Vs " + lblAwayTeam.Text;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// remove the player controls
        /// </summary>
        private void ClearPlayerControlls()
        {
            Control ctl1 = null;
            Control ctl2 = null;
            try
            {
                ctl1 = tableLayoutPanel1.GetControlFromPosition(0, 0);
                ctl2 = tableLayoutPanel1.GetControlFromPosition(1, 0);
                if (ctl1 != null)
                {
                    tableLayoutPanel1.Controls.Remove(ctl1);
                    (ctl1 as IPlayer).DeleteSg();
                }
                if (ctl2 != null)
                {
                    tableLayoutPanel1.Controls.Remove(ctl2);
                    (ctl2 as IPlayer).DeleteSg();
                }
                foreach (DataGridViewRow dr in dgvSelectPlayer.Rows)
                {
                    dr.Cells[1].Value = false;
                }
                foreach (DataGridViewRow dr in dgvSelectPlayer.Rows)
                {
                    dr.Cells[2].Value = false;
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// Enable buttons
        /// </summary>
        private void EnabaleActions()
        {
            btnhomeplus.Enabled = true;
            btnHomeminus.Enabled = true;
            btnawayplus.Enabled = true;
            btnawayminus.Enabled = true;
            btnFoul.Enabled = true;
            btnYellow.Enabled = true;
            btnRed.Enabled = true;
            btnCorner.Enabled = true;
            btnShots.Enabled = true;
            btnShotsOff.Enabled = true;
            btnSubstitute.Enabled = true;

        }

        /// <summary>
        /// Disable buttons
        /// </summary>
        private void DisableActions()
        {
            btnhomeplus.Enabled = false;
            btnHomeminus.Enabled = false;
            btnawayplus.Enabled = false;
            btnawayminus.Enabled = false;
            btnFoul.Enabled = false;
            btnYellow.Enabled = false;
            btnRed.Enabled = false;
            btnCorner.Enabled = false;
            btnShots.Enabled = false;
            btnShotsOff.Enabled = false;
            btnSubstitute.Enabled = false;
        }

        #endregion

        #region Events

        /// <summary>
        ///  On player post button click update the scenegraph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="objNodeXml"></param>
        void objDataentry_PostData(object sender, XmlNode objNodeXml)
        {
            IPlayer objPlayer = sender as IPlayer;
            if (objNodeXml != null && objPlayer != null)
            {
                objPlayer.UpdateSceneGraph(objNodeXml.InnerXml, true);
            }
        }

        /// <summary>
        /// On active match change event call the method to fill the new match details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbMatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillMatchDetails();
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// On change of match part set the counter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbMatchPart_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblCounter.Enabled = false;
                timer1.Stop();
                lblCounter.Text = "00:00";
                btnstartstop.Text = "Start";
                if (cmbMatchPart.Text == "Extra Time")
                {
                    lblCounter.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// Do Action on home click, insert values to UDT. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnhomeplus_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objUDTMatch != null)
                {
                    TimeSpan ts = DateTime.Now.Subtract(MatchPartStartTime);
                    lblHomeScore.Text = (Convert.ToInt32(lblHomeScore.Text) + 1).ToString();
                    Player p = new Player();
                    p.Team = lblHomeTeam.Text;
                    p.ObjUdtprovider = m_objUDTMatch;
                    p.FillTeam();
                    p.StartPosition = FormStartPosition.CenterParent;
                    p.ShowDialog();
                    if (p.IsPlayerSelected)
                    {
                        string selectedPlayer = p.SelectedPlayer;
                        DataGridViewRow dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
                        dgv.Cells[0].Value = SrNo;
                        dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
                        dgv.Cells[2].Value = cmbMatchPart.Text;
                        dgv.Cells[3].Value = "Goal";
                        dgv.Cells[4].Value = lblHomeTeam.Text;
                        dgv.Cells[5].Value = selectedPlayer;
                        dgvMatchevents.Rows.Add(dgv);
                        SrNo++;
                        m_objUDTMatch.InsertUdt(5, new string[] { "EventID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Goal", lblHomeTeam.Text, selectedPlayer }, isformloaded);
                        p = null;
                        UpdateMatchStats("HomeGoal", 1, "Match", cmbMatch.Text);
                        m_objUDTMatch.UpdateUdt(1, new string[] { "HomeScore" }, new string[] { lblHomeScore.Text }, "Name", cmbMatch.Text, isformloaded);
                        m_objsceneHandler.Hometeamscore = lblHomeScore.Text;
                        m_objsceneHandler.SetMatchUdt();
                    }
                    else
                    {
                        lblHomeScore.Text = (Convert.ToInt32(lblHomeScore.Text) - 1).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }//end(btnhomeplus_Click)

        /// <summary>
        /// On hometeam goals minus action update the values in udt as well as in UI to the new value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHomeminus_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(lblHomeScore.Text) != 0 && m_objUDTMatch != null)
                {
                    lblHomeScore.Text = (Convert.ToInt32(lblHomeScore.Text) - 1).ToString();
                    m_objUDTMatch.UpdateUdt(1, new string[] { "HomeScore" }, new string[] { lblHomeScore.Text }, "Name", cmbMatch.Text, isformloaded);
                    UpdateMatchStats("HomeGoal", -1, "Match", cmbMatch.Text);
                    m_objsceneHandler.Hometeamscore = lblHomeScore.Text;
                    m_objsceneHandler.SetMatchUdt();
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }//end(btnHomeminus_Click)

        /// <summary>
        /// On awayteam goals plus action update the values in udt as well as in UI to the new value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnawayplus_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objUDTMatch != null)
                {
                    lblAwayScore.Text = (Convert.ToInt32(lblAwayScore.Text) + 1).ToString();
                    Player p = new Player();
                    p.Team = lblAwayTeam.Text;
                    p.ObjUdtprovider = m_objUDTMatch;
                    p.FillTeam();
                    p.StartPosition = FormStartPosition.CenterParent;
                    p.ShowDialog();
                    if (p.IsPlayerSelected)
                    {
                        TimeSpan ts = DateTime.Now.Subtract(MatchPartStartTime);
                        string selectedPlayer = p.SelectedPlayer;
                        DataGridViewRow dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
                        dgv.Cells[0].Value = SrNo;
                        dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
                        dgv.Cells[2].Value = cmbMatchPart.Text;
                        dgv.Cells[3].Value = "Goal";
                        dgv.Cells[4].Value = lblAwayTeam.Text;
                        dgv.Cells[5].Value = selectedPlayer;
                        dgvMatchevents.Rows.Add(dgv);
                        SrNo++;
                        m_objUDTMatch.InsertUdt(5, new string[] { "EventID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Goal", lblAwayTeam.Text, selectedPlayer }, isformloaded);
                        p = null;
                        m_objUDTMatch.UpdateUdt(1, new string[] { "AwayScore" }, new string[] { lblAwayScore.Text }, "Name", cmbMatch.Text, isformloaded);
                        UpdateMatchStats("AwayGoal", 1, "Match", cmbMatch.Text);
                        m_objsceneHandler.Awayteamscore = lblAwayScore.Text;
                        m_objsceneHandler.SetMatchUdt();
                    }
                    else
                    {
                        lblAwayScore.Text = (Convert.ToInt32(lblAwayScore.Text) - 1).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// On awayteam goals minus action update the values in udt as well as in UI to the new value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnawayminus_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(lblAwayScore.Text) != 0 && m_objUDTMatch != null)
                {
                    lblAwayScore.Text = (Convert.ToInt32(lblAwayScore.Text) - 1).ToString();
                    m_objUDTMatch.UpdateUdt(1, new string[] { "AwayScore" }, new string[] { lblAwayScore.Text }, "Name", cmbMatch.Text, isformloaded);
                    UpdateMatchStats("AwayGoal", -1, "Match", cmbMatch.Text);
                    m_objsceneHandler.Awayteamscore = lblAwayScore.Text;
                    m_objsceneHandler.SetMatchUdt();
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblHomeTeam_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                TeamBuilderForm tf = new TeamBuilderForm();
                tf.Team = lblHomeTeam.Text;
                tf.ObjUdtprovider = m_objUDTMatch;
                tf.FillTeam();
                tf.StartPosition = FormStartPosition.CenterParent;
                tf.ShowDialog();
                tf = null;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblAwayTeam_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                TeamBuilderForm tf = new TeamBuilderForm();
                tf.Team = lblAwayTeam.Text;
                tf.ObjUdtprovider = m_objUDTMatch;
                tf.FillTeam();
                tf.StartPosition = FormStartPosition.CenterParent;
                tf.ShowDialog();
                tf = null;
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (CountDownTarget < DateTime.Now)
                    timer1.Stop();
                else
                {
                    TimeSpan timeLeft = CountDownTarget.Subtract(DateTime.Now);
                    lblCounter.Text = timeLeft.Minutes.ToString("00") + ":" + timeLeft.Seconds.ToString("00");
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnstartstop_Click(object sender, EventArgs e)
        {
            try
            {
                bool bneedStart = false;
                if (m_objsceneHandler != null)
                {
                    if (btnstartstop.Text == "Start")
                    {
                        if (lblCounter.Text.Trim() == "00:00")
                        {
                            if (cmbMatchPart.Text != "Extra Time")
                            {
                                CountDownTarget = DateTime.Now.AddMinutes(45.00);
                                m_objsceneHandler.TimerAction("update", "45,0,0,0");
                                m_objsceneHandler.TimerAction("start", "");
                                bneedStart = true;
                            }
                            else
                            {
                                ExtraTimeEditor ete = new ExtraTimeEditor();
                                ete.StartPosition = FormStartPosition.CenterParent;
                                ete.ShowDialog();
                                if (ete.Extratime > 0)
                                {
                                    m_objsceneHandler.TimerAction("updateextra", ete.Extratime + ",0,0,0");
                                    m_objsceneHandler.TimerAction("extrain", "");
                                    CountDownTarget = DateTime.Now.AddMinutes(ete.Extratime);
                                    m_objsceneHandler.TimerAction("extrastart", "");
                                    bneedStart = true;
                                }
                                ete = null;
                            }
                            MatchPartStartTime = DateTime.Now;
                        }
                        else
                        {
                            bneedStart = true;
                            m_objsceneHandler.TimerAction("start", "");
                        }
                        if (bneedStart)
                        {
                            timer1.Enabled = true;
                            timer1.Interval = 1000;
                            timer1.Start();
                            btnstartstop.Text = "Stop";
                            EnabaleActions();
                        }
                    }
                    else
                    {
                        m_objsceneHandler.TimerAction("update", "0,0,0,0");
                        if (cmbMatchPart.Text != "Extra Time")
                        {
                            m_objsceneHandler.TimerAction("stop", "");
                        }
                        else
                        {

                            m_objsceneHandler.TimerAction("stopextratime", "");
                        }
                        timer1.Stop();
                        btnstartstop.Text = "Start";
                        DisableActions();
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objUDTMatch != null)
                {
                    TimeSpan ts = new TimeSpan();
                    if (MatchPartStartTime.Year != 0001)
                    {
                        ts = DateTime.Now.Subtract(MatchPartStartTime);
                    }

                    Substitution sb = new Substitution();
                    sb.ObjUdtprovider = m_objUDTMatch;
                    sb.Hometeam = lblHomeTeam.Text;
                    sb.Awayteam = lblAwayTeam.Text;
                    sb.Team = lblHomeTeam.Text;
                    sb.Initialize();
                    sb.StartPosition = FormStartPosition.CenterParent;
                    sb.ShowDialog();
                    if (sb.IsTeamSelected)
                    {
                        DataGridViewRow dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
                        dgv.Cells[0].Value = SrNo;
                        if (MatchPartStartTime.Year != 0001)
                        {
                            dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
                        }
                        else
                        {
                            dgv.Cells[1].Value = "00:00";
                        }
                        dgv.Cells[2].Value = cmbMatchPart.Text;
                        dgv.Cells[3].Value = "Substitution Out";
                        dgv.Cells[4].Value = sb.Team;
                        dgv.Cells[5].Value = sb.SelectedOutPlayer;
                        dgvMatchevents.Rows.Add(dgv);
                        m_objUDTMatch.InsertUdt(5, new string[] { "EventID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Substitution Out", sb.Team, sb.SelectedOutPlayer }, isformloaded);
                        SrNo++;
                        dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
                        dgv.Cells[0].Value = SrNo;
                        dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
                        dgv.Cells[2].Value = cmbMatchPart.Text;
                        dgv.Cells[3].Value = "Substitution In";
                        dgv.Cells[4].Value = sb.Team;
                        dgv.Cells[5].Value = sb.SelectedInPlayer;
                        dgvMatchevents.Rows.Add(dgv);
                        m_objUDTMatch.InsertUdt(5, new string[] { "EventID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Substitution In", sb.Team, sb.SelectedInPlayer }, isformloaded);
                        SrNo++;
                        sb = null;
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFoul_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objUDTMatch != null)
                {
                    TimeSpan ts = new TimeSpan();
                    if (MatchPartStartTime.Year != 0001)
                    {
                        ts = DateTime.Now.Subtract(MatchPartStartTime);
                    }
                    PlayerDetails pd = new PlayerDetails();
                    pd.ObjUdtprovider = m_objUDTMatch;
                    pd.ParentName = "Foul";
                    pd.Team = lblHomeTeam.Text;
                    pd.cmbTeam.Items.Add(lblHomeTeam.Text);
                    pd.cmbTeam.Items.Add(lblAwayTeam.Text);
                    pd.Initialize();
                    pd.StartPosition = FormStartPosition.CenterParent;
                    pd.ShowDialog();
                    if (pd.IsTeamSelected)
                    {
                        DataGridViewRow dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
                        dgv.Cells[0].Value = SrNo;
                        if (MatchPartStartTime.Year != 0001)
                        {
                            dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
                        }
                        else
                        {
                            dgv.Cells[1].Value = "00:00";
                        }
                        dgv.Cells[2].Value = cmbMatchPart.Text;
                        dgv.Cells[3].Value = "Fouls";
                        dgv.Cells[4].Value = pd.Team;
                        dgv.Cells[5].Value = pd.SelectedPlayer;
                        dgvMatchevents.Rows.Add(dgv);
                        m_objUDTMatch.InsertUdt(5, new string[] { "EventID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Fouls", pd.Team, pd.SelectedPlayer }, isformloaded);
                        SrNo++;
                        string FoulTeam = "";
                        if (pd.Team == lblHomeTeam.Text)
                        {
                            FoulTeam = "HomeFoul";
                        }
                        else
                        {
                            FoulTeam = "AwayFoul";
                        }
                        UpdateMatchStats(FoulTeam, 1, "Match", cmbMatch.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCorner_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objUDTMatch != null)
                {
                    TimeSpan ts = new TimeSpan();
                    if (MatchPartStartTime.Year != 0001)
                    {
                        ts = DateTime.Now.Subtract(MatchPartStartTime);
                    }
                    TeamSelection tms = new TeamSelection();
                    tms.cmbTeam.Items.Add(lblHomeTeam.Text);
                    tms.cmbTeam.Items.Add(lblAwayTeam.Text);
                    tms.StartPosition = FormStartPosition.CenterParent;
                    tms.ShowDialog();
                    if (tms.IsTeamSelected)
                    {
                        DataGridViewRow dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
                        dgv.Cells[0].Value = SrNo;
                        if (MatchPartStartTime.Year != 0001)
                        {
                            dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
                        }
                        else
                        {
                            dgv.Cells[1].Value = "00:00";
                        }
                        dgv.Cells[2].Value = cmbMatchPart.Text;
                        dgv.Cells[3].Value = "Corner kicks";
                        dgv.Cells[4].Value = tms.SelectedTeam;
                        dgv.Cells[5].Value = "";
                        dgvMatchevents.Rows.Add(dgv);
                        m_objUDTMatch.InsertUdt(5, new string[] { "EventID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Corner kicks", tms.SelectedTeam, "" }, isformloaded);
                        SrNo++;

                        string FoulTeam = "";
                        if (tms.SelectedTeam == lblHomeTeam.Text)
                        {
                            FoulTeam = "HomeCorner";
                        }
                        else
                        {
                            FoulTeam = "AwayCorner";
                        }
                        UpdateMatchStats(FoulTeam, 1, "Match", cmbMatch.Text);
                        tms = null;
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShots_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objUDTMatch != null)
                {
                    TimeSpan ts = new TimeSpan();
                    if (MatchPartStartTime.Year != 0001)
                    {
                        ts = DateTime.Now.Subtract(MatchPartStartTime);
                    }
                    PlayerDetails pd = new PlayerDetails();
                    pd.ObjUdtprovider = m_objUDTMatch;
                    pd.ParentName = "Shots ON";
                    pd.Team = lblHomeTeam.Text;
                    pd.cmbTeam.Items.Add(lblHomeTeam.Text);
                    pd.cmbTeam.Items.Add(lblAwayTeam.Text);
                    pd.Initialize();
                    pd.StartPosition = FormStartPosition.CenterParent;
                    pd.ShowDialog();
                    if (pd.IsTeamSelected)
                    {
                        DataGridViewRow dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
                        dgv.Cells[0].Value = SrNo;
                        if (MatchPartStartTime.Year != 0001)
                        {
                            dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
                        }
                        else
                        {
                            dgv.Cells[1].Value = "00:00";
                        }
                        dgv.Cells[2].Value = cmbMatchPart.Text;
                        dgv.Cells[3].Value = "Shots On Goal";
                        dgv.Cells[4].Value = pd.Team;
                        dgv.Cells[5].Value = pd.SelectedPlayer;
                        dgvMatchevents.Rows.Add(dgv);
                        m_objUDTMatch.InsertUdt(5, new string[] { "EventID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Shots On Goal", pd.Team, pd.SelectedPlayer }, isformloaded);
                        SrNo++;
                        string FoulTeam = "";
                        if (pd.Team == lblHomeTeam.Text)
                        {
                            FoulTeam = "HomeShotsOnGoal";
                        }
                        else
                        {
                            FoulTeam = "AwayShotsOnGoal";
                        }
                        UpdateMatchStats(FoulTeam, 1, "Match", cmbMatch.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShotsOff_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objUDTMatch != null)
                {
                    TimeSpan ts = new TimeSpan();
                    if (MatchPartStartTime.Year != 0001)
                    {
                        ts = DateTime.Now.Subtract(MatchPartStartTime);
                    }
                    PlayerDetails pd = new PlayerDetails();
                    pd.ObjUdtprovider = m_objUDTMatch;
                    pd.ParentName = "Shots OFF";
                    pd.Team = lblHomeTeam.Text;
                    pd.cmbTeam.Items.Add(lblHomeTeam.Text);
                    pd.cmbTeam.Items.Add(lblAwayTeam.Text);
                    pd.Initialize();
                    pd.StartPosition = FormStartPosition.CenterParent;
                    pd.ShowDialog();
                    if (pd.IsTeamSelected)
                    {
                        DataGridViewRow dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
                        dgv.Cells[0].Value = SrNo;
                        if (MatchPartStartTime.Year != 0001)
                        {
                            dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
                        }
                        else
                        {
                            dgv.Cells[1].Value = "00:00";
                        }
                        dgv.Cells[2].Value = cmbMatchPart.Text;
                        dgv.Cells[3].Value = "Shots Off Goal";
                        dgv.Cells[4].Value = pd.Team;
                        dgv.Cells[5].Value = pd.SelectedPlayer;
                        dgvMatchevents.Rows.Add(dgv);
                        m_objUDTMatch.InsertUdt(5, new string[] { "EventID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Shots Off Goal", pd.Team, pd.SelectedPlayer }, isformloaded);
                        SrNo++;
                        string FoulTeam = "";
                        if (pd.Team == lblHomeTeam.Text)
                        {
                            FoulTeam = "HomeShotsMissed";
                        }
                        else
                        {
                            FoulTeam = "AwayShotsMissed";
                        }
                        UpdateMatchStats(FoulTeam, 1, "Match", cmbMatch.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYellow_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objUDTMatch != null)
                {
                    TimeSpan ts = new TimeSpan();
                    if (MatchPartStartTime.Year != 0001)
                    {
                        ts = DateTime.Now.Subtract(MatchPartStartTime);
                    }
                    PlayerDetails pd = new PlayerDetails();
                    pd.ObjUdtprovider = m_objUDTMatch;
                    pd.ParentName = "Yellow Card";
                    pd.Team = lblHomeTeam.Text;
                    pd.cmbTeam.Items.Add(lblHomeTeam.Text);
                    pd.cmbTeam.Items.Add(lblAwayTeam.Text);
                    pd.Initialize();
                    pd.StartPosition = FormStartPosition.CenterParent;
                    pd.ShowDialog();
                    if (pd.IsTeamSelected)
                    {
                        DataGridViewRow dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
                        dgv.Cells[0].Value = SrNo;
                        if (MatchPartStartTime.Year != 0001)
                        {
                            dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
                        }
                        else
                        {
                            dgv.Cells[1].Value = "00:00";
                        }
                        dgv.Cells[2].Value = cmbMatchPart.Text;
                        dgv.Cells[3].Value = "Yellow Cards";
                        dgv.Cells[4].Value = pd.Team;
                        dgv.Cells[5].Value = pd.SelectedPlayer;
                        dgvMatchevents.Rows.Add(dgv);
                        m_objUDTMatch.InsertUdt(5, new string[] { "EventID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Yellow Cards", pd.Team, pd.SelectedPlayer }, isformloaded);
                        SrNo++;
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRed_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_objUDTMatch != null)
                {
                    TimeSpan ts = new TimeSpan();
                    if (MatchPartStartTime.Year != 0001)
                    {
                        ts = DateTime.Now.Subtract(MatchPartStartTime);
                    }
                    PlayerDetails pd = new PlayerDetails();
                    pd.ObjUdtprovider = m_objUDTMatch;
                    pd.ParentName = "Red Card";
                    pd.Team = lblHomeTeam.Text;
                    pd.cmbTeam.Items.Add(lblHomeTeam.Text);
                    pd.cmbTeam.Items.Add(lblAwayTeam.Text);
                    pd.Initialize();
                    pd.StartPosition = FormStartPosition.CenterParent;
                    pd.ShowDialog();
                    if (pd.IsTeamSelected)
                    {
                        DataGridViewRow dgv = (DataGridViewRow)dgvMatchevents.Rows[0].Clone();
                        dgv.Cells[0].Value = SrNo;
                        if (MatchPartStartTime.Year != 0001)
                        {
                            dgv.Cells[1].Value = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
                        }
                        else
                        {
                            dgv.Cells[1].Value = "00:00";
                        }
                        dgv.Cells[2].Value = cmbMatchPart.Text;
                        dgv.Cells[3].Value = "Red Cards";
                        dgv.Cells[4].Value = pd.Team;
                        dgv.Cells[5].Value = pd.SelectedPlayer;
                        dgvMatchevents.Rows.Add(dgv);
                        m_objUDTMatch.InsertUdt(5, new string[] { "EventID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { SrNo.ToString(), cmbMatchPart.Text, dgv.Cells[1].Value.ToString(), "Red Cards", pd.Team, pd.SelectedPlayer }, isformloaded);
                        SrNo++;
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dataGridView2_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvSelectPlayer.IsCurrentCellDirty)
                {
                    dgvSelectPlayer.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }

            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// Event to handle cell value changed for loading corresponding templates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgvSelectPlayer.Update();
                if (dgvSelectPlayer.Columns.IndexOf(dgvSelectPlayer.Columns["Column2"]) == e.ColumnIndex)
                {
                    int currentcolumnclicked = e.ColumnIndex;
                    int currentrowclicked = e.RowIndex;
                    foreach (DataGridViewRow dr in dgvSelectPlayer.Rows)
                    {
                        dr.Cells[currentcolumnclicked].Value = false;
                    }
                    dgvSelectPlayer.CurrentRow.Cells[currentcolumnclicked].Value = true;
                    dgvSelectPlayer.CurrentRow.Cells[currentcolumnclicked].ReadOnly = true;
                }

                if (dgvSelectPlayer.Columns.IndexOf(dgvSelectPlayer.Columns["Column3"]) == e.ColumnIndex)
                {
                    int currentcolumnclicked = e.ColumnIndex;
                    int currentrowclicked = e.RowIndex;
                    foreach (DataGridViewRow dr in dgvSelectPlayer.Rows)
                    {
                        dr.Cells[currentcolumnclicked].Value = false;
                    }
                    dgvSelectPlayer.CurrentRow.Cells[currentcolumnclicked].Value = true;
                }

                if (e.ColumnIndex == dgvSelectPlayer.Columns["Column2"].Index)
                {
                    if ((bool)dgvSelectPlayer.Rows[e.RowIndex].Cells["Column2"].Value)
                    {
                        string sTemplate = dgvSelectPlayer.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                        string sCol = "Column2";
                        GetTemplate(sTemplate, sCol);
                    }
                }

                if (e.ColumnIndex == dgvSelectPlayer.Columns["Column3"].Index)
                {
                    if ((bool)dgvSelectPlayer.Rows[e.RowIndex].Cells["Column3"].Value)
                    {
                        string sTemplate = dgvSelectPlayer.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                        string sCol2 = "Column3";
                        GetTemplate(sTemplate, sCol2);
                    }
                }
            }

            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void objPlayer_OnShotBoxControllerStatus(object sender, SHOTBOXARGS e)
        {
            switch (e.SHOTBOXRESPONSE)
            {
                case SHOTBOXMSG.PLAYCOMPLETE:
                    m_objplayertodelete = sender as IPlayer;
                    if (m_objplayertodelete != null && BtnLoadBG.Text != "Unload BG")
                    {
                        m_objplayertodelete.DeleteSg();
                    }
                    break;
                case SHOTBOXMSG.SGDELETED:
                    if (m_objplayertodelete != null && m_objplayertodelete.Equals(sender as IPlayer))
                    {
                        TableLayoutPanelCellPosition position = tableLayoutPanel1.GetPositionFromControl(m_objplayertodelete as Form);
                        tableLayoutPanel1.Controls.Remove(m_objplayertodelete as Form);
                        switch (position.Column)
                        {
                            case 0:
                                foreach (DataGridViewRow dr in dgvSelectPlayer.Rows)
                                {
                                    dr.Cells[1].Value = false;
                                }
                                break;
                            case 1:
                                foreach (DataGridViewRow dr in dgvSelectPlayer.Rows)
                                {
                                    dr.Cells[2].Value = false;
                                }
                                break;
                        }
                        break;
                    }
                    break;
            }
        }

        /// <summary>
        /// Match events double click event to open the player selection 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMatchevents_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    int eventid = Convert.ToInt32(dgvMatchevents.Rows[e.RowIndex].Cells[0].Value);
                    if (eventid > 0)
                    {
                        LoadMatchEventGrahicPlayer();
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLoadBG_Click(object sender, EventArgs e)
        {
            try
            {
                switch (BtnLoadBG.Text)
                {
                    case "Load BG":
                        BtnLoadBG.Text = "Unload BG";
                        ScenInfo si = m_lstSceneCollection.Where(s => s.Description == "bg").FirstOrDefault();
                        Beesys.Wasp.Workflow.TemplateInfo tempinfo = objPlayergetter.GetPlayerInfo(si.Id);
                        m_objsceneHandler.LoadBackground(tempinfo, si.Id);
                        break;
                    case "Unload BG":
                        BtnLoadBG.Text = "Load BG";
                        m_objsceneHandler.UnloadBackground();
                        break;

                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }

        #endregion
    }
}
