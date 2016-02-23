﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Beesys.Wasp.Workflow;
using System.Threading;
using System.Windows.Forms;


namespace SoccerApp
{
    public class SceneHandler
    {
        #region Class Variables

        public bool isInitialized { get; set; }
        public bool isSceneLoaded { get; set; }
        LinkManager _objLinkManager;
        const string m_surlformat = "net.tcp://{0}:{1}/TcpBinding/WcfTcpLink";
        string m_serverurl = string.Empty;
        string m_scorebugscenepath = string.Empty;
        IPlayer objBGPlayer = null;
        ShotBox objScorePlayer = null;
        public string Hometeamshortname = string.Empty;
        public string Awayteamshortname = string.Empty;
        public string Hometeamscore = string.Empty;
        public string Awayteamscore = string.Empty;

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
            isInitialized = false;
            m_serverurl = string.Format(m_surlformat, ConfigurationManager.AppSettings["stingserverip"], ConfigurationManager.AppSettings["stingserverport"]);
            if (File.Exists(ConfigurationManager.AppSettings["scorebugscenepath"]))
            {
                m_scorebugscenepath = ConfigurationManager.AppSettings["scorebugscenepath"];
            }
            else if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "ScoreBug.w3d")))
            {
                m_scorebugscenepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "ScoreBug.w3d");
            }

            if (!string.IsNullOrEmpty(m_scorebugscenepath))
            {
                if (ConfigurationManager.AppSettings["stingserverip"] != null)
                {
                    string sLinkID = string.Empty;
                    _objLinkManager = new LinkManager();
                    AppLink = _objLinkManager.GetLink(LINKTYPE.TCP, out sLinkID);
                    AppLink.OnEngineConnected += new EventHandler<EngineArgs>(_objLink_OnEngineConnected);
                    AppLink.Connect(m_serverurl);
                    _objLinkManager.OnEngineDisConnected += _objLinkManager_OnEngineDisConnected;
                }
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
                    objBGPlayer.OnShotBoxStatus += _objPlayer1_OnShotBoxStatus;
                    objBGPlayer.Prepare(m_serverurl, 0, string.Empty, RENDERMODE.PROGRAM);
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void UnloadBackground()
        {
            if (objBGPlayer != null)
            {
                objBGPlayer.PlayActionSet("Unload");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadScene()
        {
            string stemplateID = string.Empty;
            string sXml = string.Empty;
            string sShotBoxID = null;
            bool isTicker;
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

                    objScorePlayer.OnShotBoxStatus += _objPlayer1_OnShotBoxStatus;
                    objScorePlayer.Prepare(m_serverurl, 10, RENDERMODE.PROGRAM);
                }//end (if)
            }
        }

        /// <summary>
        /// Set match udt name in the udt sequecer object
        /// </summary>
        /// <param name="frmobj"></param>
        public void SetMatchUDT()
        {
            if (objScorePlayer != null)
            {
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
        void _objPlayer1_OnShotBoxStatus(object sender, SHOTBOXARGS e)
        {
            if (e.SHOTBOXRESPONSE == SHOTBOXMSG.PREPARED)
            {
                isInitialized = true;
                isSceneLoaded = true;

                if (sender != null)
                {
                    ShotBox shotboxobj = sender as ShotBox;
                    if (shotboxobj != null)
                    {
                        shotboxobj.Play(true, true);
                        if (shotboxobj.Equals(objScorePlayer))
                        {
                            SetMatchUDT();
                        }
                    }
                    else
                    {
                        if (sender is IPlayer)
                        {
                            IPlayer playerobj = sender as IPlayer;
                            if (playerobj != null)
                            {
                                if (playerobj.Equals(objScorePlayer))
                                {
                                    SetMatchUDT();
                                }
                                playerobj.Play(true, true);
                            }
                        }
                    }
                }
            }
            else if (e.SHOTBOXRESPONSE == SHOTBOXMSG.PLAYCOMPLETE)
            {
                if (sender is ShotBox)
                {
                    ShotBox shotboxobj = sender as ShotBox;
                    if (shotboxobj.Equals(objScorePlayer))
                    {
                        shotboxobj.DeleteSg();
                    }
                }
                else if (sender is IPlayer)
                {
                    IPlayer playerobj = sender as IPlayer;
                    if (playerobj.Equals(objBGPlayer))
                    {
                        playerobj.DeleteSg();
                    }
                }
            }
        }


        void _objLinkManager_OnEngineDisConnected(object sender, EngineArgs e)
        {
            if (e.ENGINEIP == m_serverurl)
            {
                isInitialized = false;
            }
        }

        void _objLink_OnEngineConnected(object sender, EngineArgs e)
        {
            if (e.ENGINEIP == m_serverurl)
            {
                isInitialized = true;
                LoadScene();
            }
        }

        public void TimerAction(string actiontype, string counter)
        {
            try
            {
                TagData tg = new TagData();
                switch (actiontype.ToLower())
                {
                    case "start":
                        objScorePlayer.PlayActionSet(ConfigurationManager.AppSettings["counterstartaction"]);
                        break;
                    case "stop":
                        objScorePlayer.PlayActionSet(ConfigurationManager.AppSettings["counterstopaction"]);
                        break;
                    case "update":
                        tg.UserTags = new string[] { ConfigurationManager.AppSettings["counterusertag"] };
                        tg.Values = new string[] { counter };
                        tg.IsOnAirUpdate = true;
                        tg.Indexes = new string[] { "0" };
                        objScorePlayer.UpdateSceneGraph(tg);
                        break;
                    case "updateextra":
                        tg.UserTags = new string[] { ConfigurationManager.AppSettings["extratimeusertag"] };
                        tg.Values = new string[] { counter };
                        objScorePlayer.UpdateSceneGraph(tg);
                        break;
                    case "extrain":
                        objScorePlayer.PlayActionSet(ConfigurationManager.AppSettings["extratimein"]);
                        break;
                    case "extraout":
                        objScorePlayer.PlayActionSet(ConfigurationManager.AppSettings["extratimeout"]);
                        break;
                    case "extrastart":
                        objScorePlayer.PlayActionSet(ConfigurationManager.AppSettings["startextratime"]);
                        break;
                    case "extrastop":
                        objScorePlayer.PlayActionSet(ConfigurationManager.AppSettings["stopextratime"]);
                        break;
                    default:
                        break;
                }


            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
        }
    }
}
