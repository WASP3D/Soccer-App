using Beesys.Wasp.Workflow;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SoccerApp
{
    public class PlayerGetter
    {
        private TemplateInfo m_objTempDtls;
        private readonly CSceneReader m_objCSceneReader;

        private const string UNIFIED = "unified";
        private const string SOCCERFORM = "SoccerForm";
        private const string DGN = "dgn";
        private const string ACTION = "action";
        private const string FORMTYPE = "formtype";
        private const string NAME = "name";
        private readonly string GETATTACHMENTPATH = "//action/file[@filetype='{0}']";

        public PlayerGetter()
        {
            try
            {
                m_objTempDtls = new TemplateInfo();
                m_objCSceneReader = new CSceneReader();
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex); WriteTrace(ex.ToString());
            }
        }

        public TemplateInfo GetPlayerInfo(string Scenepath)
        {
            m_objTempDtls.TemplateDetails = m_objCSceneReader.GetPlayoutSceneInfo(Scenepath, new string[] { CCommonConstants.XML, CCommonConstants.FDDLL, CCommonConstants.CDLL, CCommonConstants.METADATAXML});
            if (m_objTempDtls.TemplateDetails != null)
            {
                Initialize(m_objTempDtls);
            }
            return m_objTempDtls;
        }

        private TemplateInfo Initialize(TemplateInfo info)
        {
            byte[] barrTypeData = null;
            byte[] barrType = null;
            XDocument xdHeaderXml = null;
            byte[] barrSceneDll = null;
            try
            {
                if (info.TemplateDetails != null && info.TemplateDetails.Keys.Count > 0)
                {
                    foreach (string skey in info.TemplateDetails.Keys)
                    {
                        switch (skey)
                        {
                            case CCommonConstants.FDDLL:
                                barrType = info.TemplateDetails[skey].Data;
                                break;
                            case CCommonConstants.CDLL:
                                barrTypeData = info.TemplateDetails[skey].Data;
                                break;
                            case CCommonConstants.XML:
                                byte[] barrData = info.TemplateDetails[skey].Data;
                                string sData = WaspEncoder.GetStringfromByte(barrData);
                                xdHeaderXml = XDocument.Parse(sData);
                                break;
                            case CCommonConstants.SCENEDLL:
                                barrSceneDll = info.TemplateDetails[skey].Data;
                                break;
                            case CCommonConstants.SGXML:
                                byte[] barrSGXml = info.TemplateDetails[skey].Data;
                                string sSGXml = WaspEncoder.GetStringfromByte(barrSGXml);
                                info.SgXml = sSGXml;
                                break;
                            case CCommonConstants.METADATAXML:
                                byte[] barrMetadataxml = info.TemplateDetails[skey].Data;
                                string sMetadataxml = WaspEncoder.GetStringfromByte(barrMetadataxml);
                                info.MetaDataXml = sMetadataxml;
                                break;
                            default:
                                break;
                        }
                    }
                    if (xdHeaderXml != null)
                    {
                        //checks whether given template is of unified type from header xml.
                        info.IsUnifiedForm = Unified(xdHeaderXml);
                        if (barrTypeData != null)
                        {
                            info.TemplatePlayerInfo = GetTypeFromAttachment(xdHeaderXml, barrTypeData, CCommonConstants.CDLL);
                        }
                        if (barrType != null)
                            info.TemplateDataInfo = GetTypeFromAttachment(xdHeaderXml, barrType, CCommonConstants.FDDLL);
                    }
                    if (string.Compare(info.Type, "tickersg", StringComparison.OrdinalIgnoreCase) == 0)
                        info.IsTicker = true;
                    else
                        info.IsTicker = false;

                    m_objTempDtls = info;
                }
            }
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex); WriteTrace(ex.ToString());
            }

            return m_objTempDtls;
        }
       
        private bool Unified(XDocument headerxml)
        {
            XElement xeActionNode = null;
            XAttribute xaFileType = null;
            bool bIsUnified = false;
            try
            {
                xeActionNode = headerxml.Element(DGN).Element(ACTION);

                if (xeActionNode != null)
                {
                    xaFileType = xeActionNode.Attribute(FORMTYPE);
                    if (
                            xaFileType != null &&
                            (
                                string.Compare(xaFileType.Value, UNIFIED, StringComparison.OrdinalIgnoreCase) == 0 ||
                                string.Compare(xaFileType.Value, SOCCERFORM, StringComparison.OrdinalIgnoreCase) == 0
                            )
                        )
                    {
                        bIsUnified = true;
                    }
                }//end if (xeActionNode != null)
                return bIsUnified;
            }//end try
            finally
            {
                xeActionNode = null;
                xaFileType = null;
            }//end finally
        }//end (IsUnified)

        private Type GetTypeFromAttachment(XDocument xdHdrXml, byte[] objarrBuffer, string filetype)
        {
            Type objType = null;
            string sXPath = null;
            string sTypeName = null;
            Assembly objAsmbly = null;
            XElement xnAtchmntInfo = null;
            try
            {
                sXPath = string.Format(CultureInfo.InvariantCulture, GETATTACHMENTPATH, filetype);
                //Gets attachment node from header xml on the basis of file type : CDLL/FDDLL.
                xnAtchmntInfo = xdHdrXml.XPathSelectElement(sXPath);
                if (xnAtchmntInfo != null)
                {
                    sTypeName = xnAtchmntInfo.Attribute(NAME).Value;
                    if (!string.IsNullOrEmpty(sTypeName))
                    {
                        sTypeName = sTypeName.Substring(0, (sTypeName.Length - 4));

                        //Loads assembly from CDLL/FDDLL byte array data.
                        objAsmbly = Assembly.Load(objarrBuffer);
                        //Gets type from loaded assembly.
                        objType = objAsmbly.GetType(sTypeName, true, true);
                    }//end (if (!string.IsNullOrEmpty (sTypeName)))
                }//end (if (xnAtchmntInfo != null))
                return objType;
            }//end try
            catch (Exception ex)
            {
                LogWriter.WriteLog(ex); WriteTrace(ex.ToString());
                return objType;
            }//end catch
            finally
            {
                objType = null;
                sXPath = null;
                sTypeName = null;
                objAsmbly = null;
                xnAtchmntInfo = null;
            }//end (finally)
        }

        private void WriteTrace(string message)
        {
            System.Diagnostics.Debug.WriteLine("SOCCER APP-->" + message);
        }

    }
   
}
