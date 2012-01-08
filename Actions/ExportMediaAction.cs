using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using umbraco;
using umbraco.BusinessLogic.Utils;
using umbraco.interfaces;

namespace TheOutfield.UmbExt.ExportMedia.Actions
{
    public class ExportMediaAction : IAction
    {
        public static ExportMediaAction Instance
        {
            get { return Singleton<ExportMediaAction>.Instance; }
        }

        public string Alias
        {
            get { return "Export Media"; }
        }

        public bool CanBePermissionAssigned
        {
            get { return false; }
        }

        public string Icon
        {
            get { return ".sprExportDocumentType"; }
        }

        public string JsFunctionName
        {
            get
            {
                if (GlobalSettings.CurrentVersion.StartsWith("4.0"))
                {
                    return "top.location.href = '/umbraco/theoutfield/exportmedia/handlers/ExportMedia.ashx?id=' + nodeID";
                }

                return "actionExportMedia()";
            }
        }

        public string JsSource
        {
            get { return @"/umbraco/theoutfield/exportmedia/scripts/exportmedia.js"; }
        }

        public char Letter
        {
            get { return 'v'; }
        }

        public bool ShowInNotifier
        {
            get { return true; }
        }
    }
}