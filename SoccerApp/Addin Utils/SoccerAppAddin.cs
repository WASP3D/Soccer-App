using Beesys.Wasp.Workflow;
using BeeSys.Shared.Library;
using BeeSys.Wasp.Communicator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerApp.Addin_Utils
{
    public class SoccerAppAddin : WAddin
    {
        #region Constants

        const string ADDINID = "{714ae5e3-007f-4b08-94d5-ba1e994f43c7}";
        internal const string STARTPATH = "Shared Resources";

        #endregion

        #region Class Variables

        IHostHelper m_objHelper = null;
        OpenSoccerAppCmd m_objopencommand = null;
        SoccerAppDocument m_objDocument = null;

        #endregion

        #region Constructor

        public SoccerAppAddin()
        {

            ID = Guid.Parse(ADDINID);
            AddinName = "SoccerApp";
        }

        static SoccerAppAddin()
        {
            AssemblyResolver.AddPath
                       (
                           Path.Combine(CCommonHostHelper.CommonPath, STARTPATH)
                       );
        }//end (GlobalVariableAddin)
        #endregion

        #region Public Methods

        public override void Load()
        {
            try
            {
                m_objopencommand = new OpenSoccerAppCmd();
                m_objopencommand.Initialize(ID, AddinName);
                m_objopencommand.OnOpenClick += M_objopencommand_OnCommandClick;

            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }
            finally
            {

            }
            base.Load();
        }

        private void M_objopencommand_OnCommandClick(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine("Soccer App-->M_objopencommand_OnCommandClick");
                if (m_objDocument != null && m_objDocument.IsOpened)
                {
                    Debug.WriteLine("Soccer App-->m_objDocument != null && m_objDocument.IsOpened");
                    return;
                }
                Debug.WriteLine("Soccer App-->M_objopencommand_OnCommandClick-->m_objDocument == null");
                m_objDocument = new SoccerAppDocument(ID, AddinName);
                m_objDocument.Initialize(ID, AddinName);
                m_objDocument.LoadApp();
                OnWDocumentCreation(this, new WDocumentArgs(m_objDocument));

            }
            catch (Exception ex)
            {

            }
        }

        public override void Initialize(IHostHelper host)
        {
            try
            {
                m_objHelper = host;

            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex);
            }

        }

        #endregion Public Methods



        public override void FreeResource()
        {
            Debug.WriteLine("Soccer App-->FreeResource Addin");
            if (m_objopencommand != null)
            {
                m_objopencommand.FreeResources();
                m_objopencommand = null;
            }
            if (m_objDocument != null)
            {
                m_objDocument.FreeResource();
                m_objDocument = null;
            }
        }

        public override WCommand[] GetCommands()
        {
            return new WCommand[] { m_objopencommand };
        }

        public override WDocument[] GetDocuments()
        {
            return null;
        }

        public override WPanel[] GetPanels()
        {
            return null;
        }

        public override IAddinService[] GetServices()
        {
            return null;
        }

    }
}
