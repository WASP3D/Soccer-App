using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UdtHelper;
using System.Configuration;

namespace TeamBuilder
{
    public partial class TeamBuilder : UserControl
    {
        private UdtController _udtController;
        public TeamBuilder()
        {
            InitializeComponent();
            /*var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, lblGK.Width, lblGK.Height);
            this.lblGK.Region = new Region(path);
            toolTip1.SetToolTip(lblGK, string.Format("Goal Keeper: Name:{0}SUB:{1}", "", ""));*/
        }
        public bool InitializeUDT()
        {
            _udtController = new UdtController();
            // TODO: Test: Missing Config/wrong entries/correct entries
            _udtController.CreateConnection(ConfigurationManager.AppSettings["kc"], ConfigurationManager.AppSettings["port"]);
            // TODO: Test: Wrong UDT Name/correct UDT Name
            _udtController.GetUdt("SoccerUDT");
            return false;

        }
        public bool InitializeUDT(string KCName,string port)
        {
            _udtController = new UdtController();
            // TODO: Test: empty entry/wrong entries/correct entries
            _udtController.CreateConnection(KCName, port);
            // TODO: Test: Wrong UDT Name/correct UDT Name
            _udtController.GetUdt("SoccerUDT");
            return false;
            //j
        }
    }
}
