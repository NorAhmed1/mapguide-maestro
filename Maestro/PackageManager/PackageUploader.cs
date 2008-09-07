#region Disclaimer / License
// Copyright (C) 2008, Kenneth Skovhede
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
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using OSGeo.MapGuide.MaestroAPI;

namespace OSGeo.MapGuide.Maestro.PackageManager
{
    public class PackageUploader
    {
        private class Runner
        {
            PackageProgress m_owner;
            string m_filename;
            ServerConnectionI m_con;
            System.Threading.Thread m_thread;

            public Runner(PackageProgress owner, string filename, ServerConnectionI connection)
            {
                m_owner = owner;
                m_filename = filename;
                m_con = connection;
                m_thread = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadEntry));
                m_thread.Start();
            }

            private void ThreadEntry()
            {
                try
                {
                    m_con.UploadPackage(m_filename, new Utility.StreamCopyProgressDelegate(ProgressCallback));
                }
                catch(Exception ex)
                {
                    if (ex.Message == "CANCEL" || ex as ObjectDisposedException != null)
                        return;
                    
                    MessageBox.Show(string.Format(Globalizator.Globalizator.Translate("OSGeo.MapGuide.Maestro.PackageManager.PackageProgress", System.Reflection.Assembly.GetExecutingAssembly(), "Failed to upload package: {0}"), ex.Message), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    m_owner.Cancel();
                    return;
                }

                m_owner.Close();
            }

            private void ProgressCallback(long copied, long remain, long total)
            {
                if (m_owner.InvokeRequired)
                    m_owner.Invoke(new Utility.StreamCopyProgressDelegate(ProgressCallback), copied, remain, total);
                else
                {
                    if (copied == 0)
                    {
                        if (total == -1)
                        {
                            m_owner.CurrentProgress.Style = ProgressBarStyle.Marquee;
                            m_owner.SetOperation(Globalizator.Globalizator.Translate("OSGeo.MapGuide.Maestro.PackageManager.PackageProgress", System.Reflection.Assembly.GetExecutingAssembly(), "Uploading file..."));
                        }
                        else
                            m_owner.CurrentProgress.Style = ProgressBarStyle.Blocks;

                        m_owner.HideTotal();
                    }

                    if (total != -1)
                    {
                        m_owner.SetOperation(string.Format(Globalizator.Globalizator.Translate("OSGeo.MapGuide.Maestro.PackageManager.PackageProgress", System.Reflection.Assembly.GetExecutingAssembly(), "Uploaded {0} of {1}"), Utility.FormatSizeString(copied), Utility.FormatSizeString(total)));
                        m_owner.SetCurrentProgress((int)((copied / (double)total) * 100), 100);
                    }

                    if (remain == 0)
                    {
                        m_owner.CurrentProgress.Style = ProgressBarStyle.Marquee;
                        m_owner.SetOperation(Globalizator.Globalizator.Translate("OSGeo.MapGuide.Maestro.PackageManager.PackageProgress", System.Reflection.Assembly.GetExecutingAssembly(), "Waiting for server to complete restore operation..."));
                    }
                    else
                    {
                        if (!m_owner.Visible && m_owner.DialogResult == DialogResult.Cancel)
                            throw new Exception("CANCEL");
                    }
                }
            }
        }

        public static DialogResult UploadPackage(Form owner, ServerConnectionI con)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            //Mono does NOT like this one
            //dlg.AutoUpgradeEnabled = true;
            dlg.CheckFileExists = true;
            dlg.CheckPathExists = true;
            dlg.DefaultExt = ".mgp";
            dlg.Filter = "MapGuide Packages (*.mgp)|*.mgp|Zip files (*.zip)|*.zip|All files (*.*)|*.*";
            dlg.FilterIndex = 0;
            dlg.Multiselect = false;
            dlg.ValidateNames = true;
            dlg.Title = Globalizator.Globalizator.Translate("OSGeo.MapGuide.Maestro.PackageManager.PackageProgress", System.Reflection.Assembly.GetExecutingAssembly(), "Select the package to upload");

            if (dlg.ShowDialog(owner) == DialogResult.OK)
            {
                PackageProgress pg = new PackageProgress();
                Runner r = new Runner(pg, dlg.FileName, con);
                return pg.ShowDialog(owner);
            }
            else
                return DialogResult.Cancel;
        }
    }
}
