#region Disclaimer / License
// Copyright (C) 2009, Kenneth Skovhede
// http://www.hexad.dk, opensource@hexad.dk
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// 
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
// 
#endregion
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OSGeo.MapGuide.Maestro.ResourceEditors.FeatureSourceEditors.SHP
{
    public partial class FeatureSourceEditorSHP : ResourceEditors.FeatureSourceEditors.Filebased.FilebasedFeatureSourceEditor
    {
        public FeatureSourceEditorSHP(EditorInterface editor, OSGeo.MapGuide.MaestroAPI.FeatureSource feature)
            : base(editor, feature, false, "DefaultFileLocation", GetFileTypes())
        {
        }

        private static System.Collections.Specialized.NameValueCollection GetFileTypes()
        {
			System.Collections.Specialized.NameValueCollection nv = new System.Collections.Specialized.NameValueCollection();
			nv.Add(".shp", Strings.Common.ShapeFiles);
			nv.Add("", Strings.Common.AllFiles);
            return nv;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FeatureSourceEditorSHP));
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // UnmanagedEditor
            // 
            resources.ApplyResources(this.UnmanagedEditor, "UnmanagedEditor");
            // 
            // ManagedEditor
            // 
            resources.ApplyResources(this.ManagedEditor, "ManagedEditor");
            // 
            // FeatureSourceEditorSHP
            // 
            this.Name = "FeatureSourceEditorSHP";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
