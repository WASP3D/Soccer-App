using Beesys.Wasp.Workflow;
using SoccerApp.Properties;
using System;
using System.Diagnostics;
using System.Drawing;

namespace SoccerApp.Addin_Utils
{
    class OpenSoccerAppCmd : WCommand
    {
        #region Class Variables

        protected string m_sCaption = "Soccer APP";
        protected string m_sHelpText = "Soccer Data Entry App to UDT";
        protected string m_sFileCaption = "File";
        protected string m_sOpencaption = "Open";
        protected string m_shrtcut = string.Empty;
        protected string m_sLoadFrmCaption = null;
        public event EventHandler OnOpenClick;

        #endregion

        public OpenSoccerAppCmd()
        {
            Bitmap bmap = null;
            try
            {
                Debug.WriteLine("Soccer App-->OpenSoccerAppCmd");
                Sortnumber = 8;
                if (Resources.Soccer != null)
                {
                    Debug.WriteLine("Soccer App-->Resources.Soccer!=null");
                    bmap = new Bitmap(Resources.Soccer);
                    CommandImage = bmap;
                    Debug.WriteLine("Soccer App-->Resources.Soccer is set to CommandImage");
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex); Debug.WriteLine(ex.ToString());
            }
            finally
            {
                bmap = null;
            }
        }

        #region override properties

        public override string ShortCut
        {
            get
            {
                return m_shrtcut;
            }

            protected set
            {
                m_shrtcut = value;
            }
        }


        #endregion override properties

        #region Public Methods

        public override void Initialize(Guid addinid, string addinname)
        {
            Caption = m_sCaption;
            HelpText = m_sHelpText;
            ParentMenu = new CMenuInfo(m_sFileCaption, m_sFileCaption, null, new CMenuInfo(m_sOpencaption, m_sOpencaption, null, null));
            ToolItem = false;
            MenuItem = true;
            //base.Initialize(addinid, addinname);
        }

        public override void Execute()
        {
            try
            {
                if (OnOpenClick!=null)
                {
                    OnOpenClick(this,new EventArgs());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public override void FreeResources()
        {
            
        }


        #endregion Public Methods

    }
}
