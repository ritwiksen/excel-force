using ExcelForce.Foundation.EntityManagement.Models.ExtractMap;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExcelForce.Forms.ExtractionMap.ExtractData
{
    public partial class ExtractMapViewer : Form
    {
        private ReadableMapExtract _treeViewSource;
        public ExtractMapViewer()
        {
            InitializeComponent();
        }

        public ExtractMapViewer(ReadableMapExtract mapExtract) : this()
        {
            _treeViewSource = mapExtract;

            LoadTreeView();
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {

        }

        private void LoadTreeView()
        {
            var parent = _treeViewSource.Parent;

            var parentNode = GetTreeNode(parent.Label);

            parentNode = GetNodes(parentNode, parent, true);

            parentNode = ConstructChildren(parentNode);

            treeViewObject.Nodes.Add(parentNode);

            treeViewObject.ExpandAll();
        }

        private TreeNode ConstructChildren(TreeNode parentNode)
        {
            if (_treeViewSource.Children?.Any() ?? false)
            {
                var childrenNode = parentNode.Nodes["Children"];

                foreach (var child in _treeViewSource.Children)
                {
                    var childNode = GetTreeNode(child.Label);

                    childNode = GetNodes(childNode, child);

                    childrenNode.Nodes.Add(childNode);
                }
            }

            return parentNode;
        }

        private TreeNode GetNodes(TreeNode node, ReadableObject dataObject, bool isParent = false)
        {
            var fieldNode = GetFieldsNode(dataObject.Fields);

            node.Nodes.Add(fieldNode);

            if (isParent)
            {
                var childNode = GetTreeNode("Children");

                node.Nodes.Add(childNode);
            }

            return node;
        }

        private TreeNode GetFieldsNode(IList<SfField> fields)
        {
            var fieldNode = GetTreeNode("Fields");

            foreach (var field in fields)
            {
                var node = GetTreeNode(field.Name, field.DisplayName());

                fieldNode.Nodes.Add(node);
            }

            return fieldNode;
        }

        private TreeNode GetTreeNode(string text, string name = null)
        {
            return new TreeNode
            {
                Name = string.IsNullOrWhiteSpace(name) ? text : name,
                Text = text
            };
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {

        }
    }
}
