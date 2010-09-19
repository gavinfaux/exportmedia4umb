using System;
using TheOutfield.UmbExt.ExportMedia.Actions;
using umbraco.BusinessLogic;
using umbraco.BusinessLogic.Actions;
using umbraco.cms.presentation.Trees;

namespace TheOutfield.UmbExt.ExportMedia
{
    public class AppBase : ApplicationBase
    {
        public AppBase()
        {
            BaseTree.BeforeNodeRender += new BaseTree.BeforeNodeRenderEventHandler(BaseTree_BeforeNodeRender);
        }

        protected void BaseTree_BeforeNodeRender(ref XmlTree sender, ref XmlTreeNode node, EventArgs e)
        {
            if (node.Menu != null && node.NodeType == "media")
            {
                node.Menu.Insert(1, ContextMenuSeperator.Instance);
                node.Menu.Insert(2, ExportMediaAction.Instance);
                node.Menu.Insert(3, ContextMenuSeperator.Instance);
            }
        }
    }
}