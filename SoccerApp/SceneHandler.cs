using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Beesys.Wasp.Workflow;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;


namespace SoccerApp
{
    public class SceneHandler:IDisposable
    {
        #region Class Variables

        public bool IsInitialized { get; set; }
        public bool IsSceneLoaded { get; set; }
        public string Hometeamshortname = string.Empty;
        public string Awayteamshortname = string.Empty;
        public string Hometeamscore = string.Empty;
        public string Awayteamscore = string.Empty;

        //private const string m_surlformat = "net.tcp://{0}:{1}/TcpBinding/WcfTcpLink";
        private string m_serverurl = string.Empty;
        private string m_scorebugscenepath = string.Empty;
        private IPlayer objBGPlayer = null;
        private ShotBox objScorePlayer = null;

        public Link AppLink
        {
            get;
            set;
        }

        public CWaspFileHandler FileHandler
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
            WriteTrace("Initialize");
            IsInitialized = false;
            m_serverurl = ConfigHandler.Engine!=null? ConfigHandler.Engine.TcpUrl:string.Empty;
            WriteTrace("Initialize m_serverurl->"+ m_serverurl);
            if (File.Exists(ConfigHandler.AppSettings.Settings["scorebugscenepath"].Value))
            {
                m_scorebugscenepath = ConfigHandler.AppSettings.Settings["scorebugscenepath"].Value;
            }
            else if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "ScoreBug.w3d")))
            {
                m_scorebugscenepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "ScoreBug.w3d");
            }

            if (!string.IsNullOrEmpty(m_scorebugscenepath) && !string.IsNullOrEmpty(m_serverurl))
            {
                WriteTrace("!string.IsNullOrEmpty(m_scorebugscenepath) && !string.IsNullOrEmpty(m_serverurl)");
                string sLinkID = string.Empty;
                LinkManager objLinkManager = new LinkManager();
                AppLink = objLinkManager.GetLink(LINKTYPE.TCP, out sLinkID);
                AppLink.OnEngineConnected += new EventHandler<EngineArgs>(objLink_OnEngineConnected);
                AppLink.Connect(m_serverurl);
                objLinkManager.OnEngineDisConnected += objLinkManager_OnEngineDisConnected;
            }
        }

        /// <summary>
        /// Load background template
        /// </summary>
        /// <param name="templateid"></param>
        public void LoadBackground(TemplateInfo tempinfo, string id)
        {
            try
            {
                objBGPlayer = Activator.CreateInstance(tempinfo.TemplatePlayerInfo) as IPlayer;
                if (objBGPlayer != null)
                {
                    objBGPlayer.Init("", "", id, "");
                    objBGPlayer.SetLink(AppLink, tempinfo.MetaDataXml);
                    if (objBGPlayer is IAddinInfo)
                        (objBGPlayer as IAddinInfo).Init(new InstanceInfo() { InstanceId = id });

                    IChannelShotBox objChannelShotBox = objBGPlayer as IChannelShotBox;
                    if (objChannelShotBox != null)
                    {
                        objChannelShotBox.SetEngineUrl(m_serverurl);
                    }
                    objBGPlayer.OnShotBoxStatus += objPlayer1_OnShotBoxStatus;
                    objBGPlayer.OnShotBoxControllerStatus += objPlayer1_OnShotBoxStatus;
                    objBGPlayer.Prepare(m_serverurl, 0, string.Empty, RENDERMODE.PROGRAM);
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex); WriteTrace(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void UnloadBackground()
        {
            if (objBGPlayer != null)
            {
                string actionsetname = ConfigHandler.AppSettings.Settings["unloadactionsetname"].Value;
                WriteTrace("Calling action set "+ actionsetname);
                objBGPlayer.PlayActionSet(ConfigHandler.AppSettings.Settings["unloadactionsetname"].Value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadScene()
        {
            string sXml = string.Empty;
            string sShotBoxID = null;
            bool isTicker;
            WriteTrace("LoadScene");
            sXml = Util.getSGFromWSL(m_scorebugscenepath);
            string filetype = Path.GetExtension(m_scorebugscenepath).Split(new string[] { "." }, StringSplitOptions.None)[1];
            if (!string.IsNullOrEmpty(sXml))
            {

                objScorePlayer = AppLink.GetShotBox(sXml, out sShotBoxID, out isTicker) as ShotBox;
                if (!Equals(objScorePlayer, null))
                {
                    objScorePlayer.SetEngineUrl(m_serverurl);

                    InstanceInfo o = new InstanceInfo() { Type = filetype, InstanceId = m_scorebugscenepath, TemplateId = string.Empty, ThemeId = "default" };

                    if (objScorePlayer is IAddinInfo)
                        (objScorePlayer as IAddinInfo).Init(o);

                    objScorePlayer.OnShotBoxStatus += objPlayer1_OnShotBoxStatus;
                    objScorePlayer.OnShotBoxControllerStatus += objPlayer1_OnShotBoxStatus;
                    objScorePlayer.Prepare(m_serverurl, 0, RENDERMODE.PROGRAM);
                }//end (if)
            }
        }

        /// <summary>
        /// Set match udt name in the udt sequecer object
        /// </summary>
        /// <param name="frmobj"></param>
        public void SetMatchUdt()
        {
            if (objScorePlayer != null)
            {
                WriteTrace("SetMatchUDT " + Hometeamshortname + " VS " + Awayteamshortname);
                WriteTrace("SetMatchUDT " + Hometeamscore + " : " + Awayteamscore);
                TagData tg = new TagData();
                tg.UserTags = new string[] { "Hometeamscore", "Awayteamscore", "Hometeamname", "Awayteamname" };
                tg.Values = new string[] { Hometeamscore, Awayteamscore, Hometeamshortname, Awayteamshortname };
                tg.IsOnAirUpdate = true;
                tg.Indexes = new string[] { "0", "0", "0", "0" };
                objScorePlayer.UpdateSceneGraph(tg);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void objPlayer1_OnShotBoxStatus(object sender, SHOTBOXARGS e)
        {
            if (e.SHOTBOXRESPONSE == SHOTBOXMSG.PREPARED)
            {
                IsInitialized = true;
                IsSceneLoaded = true;

                if (sender != null)
                {
                    ShotBox shotboxobj = sender as ShotBox;
                    if (shotboxobj != null)
                    {
                        if (shotboxobj.Equals(objScorePlayer))
                        {
                            WriteTrace("objPlayer1_OnShotBoxStatus Scoreplayer Scene prepared,Calling SetMatchUdt...");
                            SetMatchUdt();
                        }
                        WriteTrace("objPlayer1_OnShotBoxStatus Scoreplayer Scene prepared,Calling Play..");
                        shotboxobj.Play(true, true);
                    }
                    else
                    {
                        if (sender is IPlayer)
                        {
                            IPlayer playerobj = sender as IPlayer;
                            if (playerobj != null)
                            {
                                WriteTrace("objPlayer1_OnShotBoxStatus BackGround Scene prepared,Calling Play...");
                                playerobj.Play(true, true);
                            }
                        }
                    }
                }
            }
            else if (e.SHOTBOXRESPONSE == SHOTBOXMSG.PLAYCOMPLETE)
            {
                WriteTrace("objPlayer1_OnShotBoxStatus SHOTBOXMSG.PLAYCOMPLETE event..");
                if (sender is ShotBox)
                {
                    ShotBox shotboxobj = sender as ShotBox;
                    if (shotboxobj.Equals(objScorePlayer))
                    {
                        WriteTrace("objPlayer1_OnShotBoxStatus Scoreplayer Scene playcomplete,Calling DeleteSg...");
                        shotboxobj.DeleteSg();
                    }
                }
                else if (sender is IPlayer)
                {
                    IPlayer playerobj = sender as IPlayer;
                    if (playerobj.Equals(objBGPlayer))
                    {
                        WriteTrace("objPlayer1_OnShotBoxStatus BackGround Scene playcomplete,Calling DeleteSg...");
                        playerobj.DeleteSg();
                    }
                }
            }
        }


        void objLinkManager_OnEngineDisConnected(object sender, EngineArgs e)
        {
            if (e.ENGINEIP == m_serverurl)
            {
                IsInitialized = false;
            }
        }

        void objLink_OnEngineConnected(object sender, EngineArgs e)
        {
            WriteTrace("objLink_OnEngineConnected");
            if (e.ENGINEIP == m_serverurl)
            {
                IsInitialized = true;
                LoadScene();
            }
        }

        public void TimerAction(string actiontype, string counter)
        {
            try
            {
                TagData tg = new TagData();
                switch (actiontype.ToLower(CultureInfo.InvariantCulture))
                {
                    case "start":
                        objScorePlayer.PlayActionSet(ConfigHandler.AppSettings.Settings["counterstartaction"].Value);
                        break;
                    case "stop":
                        objScorePlayer.PlayActionSet(ConfigHandler.AppSettings.Settings["counterstopaction"].Value);
                        break;
                    case "update":
                        tg.UserTags = new string[] { ConfigHandler.AppSettings.Settings["counterusertag"].Value };
                        tg.Values = new string[] { counter };
                        tg.IsOnAirUpdate = true;
                        tg.Indexes = new string[] { "0" };
                        objScorePlayer.UpdateSceneGraph(tg);
                        break;
                    case "updateextra":
                        tg.UserTags = new string[] { ConfigHandler.AppSettings.Settings["extratimeusertag"].Value };
                        tg.Values = new string[] { counter };
                        tg.IsOnAirUpdate = true;
                        tg.Indexes = new string[] { "0" };
                        objScorePlayer.UpdateSceneGraph(tg);
                        break;
                    case "extrain":
                        objScorePlayer.PlayActionSet(ConfigHandler.AppSettings.Settings["extratimein"].Value);
                        break;
                    case "extraout":
                        objScorePlayer.PlayActionSet(ConfigHandler.AppSettings.Settings["extratimeout"].Value);
                        break;
                    case "extrastart":
                        objScorePlayer.PlayActionSet(ConfigHandler.AppSettings.Settings["startextratime"].Value);
                        break;
                    case "extrastop":
                        objScorePlayer.PlayActionSet(ConfigHandler.AppSettings.Settings["stopextratime"].Value);
                        break;
                    default:
                        break;
                }


            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex); WriteTrace(ex.ToString());
            }
        }

        public void Dispose()
        {
            if(objBGPlayer!=null)
            {
                objBGPlayer.OnShotBoxStatus -= objPlayer1_OnShotBoxStatus;
                objBGPlayer.OnShotBoxControllerStatus -= objPlayer1_OnShotBoxStatus;
                objBGPlayer.Dispose();
                objBGPlayer = null;
            }
            if (objScorePlayer != null)
            {
                objScorePlayer.OnShotBoxStatus -= objPlayer1_OnShotBoxStatus;
                objScorePlayer.OnShotBoxControllerStatus -= objPlayer1_OnShotBoxStatus;
                objScorePlayer.Dispose();
                objScorePlayer = null;
            }
            if(AppLink!=null)
            {
                AppLink.Dispose();
                AppLink = null;
            }
            if (FileHandler != null)
            {
                FileHandler.Dispose();
                FileHandler = null;
            }
        }
        private void WriteTrace(string message)
        {
            System.Diagnostics.Debug.WriteLine("SOCCER APP-->" + message);
        }
    }
}
