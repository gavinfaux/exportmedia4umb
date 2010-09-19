using System.Collections.Generic;
using System.IO;
using System.Web;
using Ionic.Zip;
using umbraco.cms.businesslogic.media;

namespace TheOutfield.UmbExt.ExportMedia.Handlers
{
    /// <summary>
    /// Summary description for ExportMedia
    /// </summary>
    public class ExportMedia : IHttpHandler
    {
        private List<string> _addedFiles;

        public void ProcessRequest(HttpContext context)
        {
            int rootNodeId = -1;
            int.TryParse(context.Request.QueryString["id"], out rootNodeId);

            context.Response.Clear();
            context.Response.AddHeader("content-disposition", "attachment; filename=Export.zip");
            context.Response.ContentType = "application/zip";

            _addedFiles = new List<string>();

            using (ZipFile zip = new ZipFile())
            {
                if (rootNodeId > 0)
                {
                    string path = "";

                    var rootMedia = new Media(rootNodeId);
                    ParseMedia(context, rootMedia, zip, path);
                }
                zip.Save(context.Response.OutputStream);
            }
            
            context.Response.End(); 
        }

        protected void ParseMedia(HttpContext context, Media media, ZipFile zip, string path)
        {
            if (media.HasChildren)
            {
                foreach (var child in media.Children)
                {
                    if (child.ContentType.Alias == "Folder")
                        ParseMedia(context, child, zip, path + "/" + child.Text);
                    else
                    {
                        var filePath = context.Server.MapPath(child.getProperty("umbracoFile").Value.ToString());
                        var fileName = Path.GetFileNameWithoutExtension(filePath);
                        var fileExtension = Path.GetExtension(filePath);
                        var newFilePath = path +"/"+ fileName + fileExtension;
                        var existingFileCount = 0;

                        if(_addedFiles.Contains(newFilePath))
                        {
                            while (_addedFiles.Contains(newFilePath))
                            {
                                existingFileCount++;
                                newFilePath = path + "/" + fileName + "-" + existingFileCount + fileExtension;
                            }
                        }

                        zip.AddEntry(newFilePath, File.Open(filePath, FileMode.Open));
                        _addedFiles.Add(newFilePath);
                    }
                }
            }
        }

        public bool IsReusable
        {
            get { return false;  }
        }
    }
}