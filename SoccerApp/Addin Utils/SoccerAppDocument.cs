using Beesys.Wasp.Workflow;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoccerApp.Addin_Utils
{
    class SoccerAppDocument : WDocument
    {
        Guid DocumentId = Guid.NewGuid();
        public bool IsOpened = true;
        SoccerApp soccerApp = null;

        #region constructor
        public SoccerAppDocument(Guid addinid, string addinname)
        {
            base.Initialize(addinid, addinname);
            IsSetControlStyle = false;// S.No.			: -	19
        }
        #endregion

        public override bool ShowCloseButton
        {
            get
            {
                return true;
            }
            protected set
            {
                base.ShowCloseButton = value;
            }
        }
        public override bool IsSetControlStyle
        {
            get;
            set;
        }

        public override void Initialize(Guid addinid, string addinname)
        {
            base.Initialize(addinid, addinname);
        }

        internal void LoadApp()
        {
            try
            {
                soccerApp = new SoccerApp();
                soccerApp.Text = "SOCCER APP";
                soccerApp.BackColor = System.Drawing.Color.Gray;
                soccerApp.TopLevel = false;
                soccerApp.TopMost = false;
                soccerApp.Dock = DockStyle.Fill;
                soccerApp.FormBorderStyle = FormBorderStyle.None;

                DocumentID = DocumentId;

                Caption = "SOCCER APP";

                this.Controls.Add(soccerApp);
                soccerApp.Show();
            }
            catch(Exception ex)
            {

            }

        }

        public override void FreeResource()
        {
            try
            {
                IsOpened = false;
                Debug.WriteLine("Soccer App-->FreeResource Document");
                if(soccerApp!=null)
                {
                    Debug.WriteLine("Soccer App-->FreeResource Document soccerApp!=null");
                    soccerApp.ShutDown();
                    soccerApp = null;
                }
                else
                {
                    Debug.WriteLine("Soccer App-->FreeResource Document soccerApp==null");
                }
                if (this.Controls.Count > 0)
                    this.Controls[0].Dispose();
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Soccer App-->Document Error->"+ ex.Message);
            }
        }
    }
}
