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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace OSGeo.MapGuide.Maestro.ResourceEditors.FeatureSourceEditors.Gdal
{
	/// <summary>
	/// Summary description for Simple.
	/// </summary>
	public class Simple : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        private System.Windows.Forms.Button BrowseFileButton;
        public TextBox Filepath;
		private System.Windows.Forms.Label label1;
		private bool m_isUpdating = false;
		private EditorInterface m_editor;
		private OSGeo.MapGuide.MaestroAPI.FeatureSource m_feature;

		public Simple()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		public void SetItem(EditorInterface editor, OSGeo.MapGuide.MaestroAPI.FeatureSource feature)
		{
			m_editor = editor;
			m_feature = feature;

			UpdateDisplay();
		}

		public void UpdateDisplay()
		{
			try
			{
				m_isUpdating = true;
				if (m_feature == null || m_feature.Parameter == null)
					return;

				Filepath.Text = m_feature.Parameter["DefaultRasterFileLocation"];			}
			finally
			{
				m_isUpdating = false;
			}
		}


		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Simple));
            this.BrowseFileButton = new System.Windows.Forms.Button();
            this.Filepath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BrowseFileButton
            // 
            resources.ApplyResources(this.BrowseFileButton, "BrowseFileButton");
            this.BrowseFileButton.Name = "BrowseFileButton";
            this.BrowseFileButton.Click += new System.EventHandler(this.BrowseFileButton_Click);
            // 
            // Filepath
            // 
            resources.ApplyResources(this.Filepath, "Filepath");
            this.Filepath.Name = "Filepath";
            this.Filepath.TextChanged += new System.EventHandler(this.Filepath_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // Simple
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.BrowseFileButton);
            this.Controls.Add(this.Filepath);
            this.Controls.Add(this.label1);
            this.Name = "Simple";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void Filepath_TextChanged(object sender, System.EventArgs e)
		{
			if (m_feature == null || m_isUpdating)
				return;

			if (m_feature.Parameter == null)
				m_feature.Parameter = new OSGeo.MapGuide.MaestroAPI.NameValuePairTypeCollection();

			if (m_feature.ConfigurationDocument != null && m_feature.ConfigurationDocument.Trim().Length != 0)
				if (MessageBox.Show(this, Strings.Simple.RemoveConfigurationWarning, Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3) != DialogResult.Yes)
					return;
				else
					m_feature.ConfigurationDocument = null;
 
			m_feature.Parameter["DefaultRasterFileLocation"] = Filepath.Text;
			m_editor.HasChanged();
		}

		private void BrowseFileButton_Click(object sender, System.EventArgs e)
		{
			NameValueCollection nv = new NameValueCollection();
			nv.Add("", OSGeo.MapGuide.Maestro.ResourceEditors.Strings.Common.AllFiles);
			string s = m_editor.BrowseUnmanagedData(Filepath.Text, nv);
			if (s != null)
				Filepath.Text = s;
		}
	}
}
