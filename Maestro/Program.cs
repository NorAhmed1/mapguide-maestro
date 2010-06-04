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
using System.Windows.Forms;
using OSGeo.MapGuide.MaestroAPI;

namespace OSGeo.MapGuide.Maestro
{
	/// <summary>
	/// Summary description for Program.
	/// </summary>
	public class Program
	{
		[STAThread()]
		public static void Main(string[] args)
		{
            //MaestroAPI.HttpServerConnection con = new OSGeo.MapGuide.MaestroAPI.HttpServerConnection(new Uri("http://localhost/mapguide"), "Administrator", "admin", "da", true);

			try
			{
				Application.EnableVisualStyles();
				Application.DoEvents();

                try
                {
                    PreferedSiteList sites = PreferedSiteList.Load();
                    if (!string.IsNullOrEmpty(sites.GUILanguage))
                    {
                        System.Threading.Thread.CurrentThread.CurrentUICulture =
                        System.Threading.Thread.CurrentThread.CurrentCulture =
                            System.Globalization.CultureInfo.GetCultureInfo(sites.GUILanguage);
                    }
                }
                catch
                {
                }

				Application.Run(new FormMain());
			}
			catch(Exception ex)
			{
				MessageBox.Show(string.Format(Strings.Program.SeriousError, ex.ToString()), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private static string AppSettingFile
        {
            get
            {
                string path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), Application.ProductName);
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);
                path = System.IO.Path.Combine(path, "sitelist.xml");

                return path;
            }
        }

        private static PreferedSiteList _ApplicationSettings = null;

        public static PreferedSiteList ApplicationSettings
        {
            get 
            {
                if (_ApplicationSettings == null)
                    _ApplicationSettings = PreferedSiteList.Load();

                return _ApplicationSettings;
            }
            set
            {
                _ApplicationSettings = value;
                _ApplicationSettings.Save();
            }
        }

        /// <summary>
        /// Opens the given URL in a browser
        /// </summary>
        public static void OpenUrl(string url)
        {
            try
            {
                if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                    throw new Exception("Malformed URL");

                if (string.IsNullOrEmpty(ApplicationSettings.SystemBrowser))
                {
                    try
                    {
                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        process.StartInfo.FileName = url;
                        process.StartInfo.UseShellExecute = true;
                        process.Start();
                    }
                    catch
                    {
                        //The straightforward method gives an error: "The requested lookup key was not found in any active activation context"
                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        process.StartInfo.FileName = "rundll32.exe";
                        process.StartInfo.Arguments = "url.dll,FileProtocolHandler " + url;
                        process.StartInfo.UseShellExecute = true;
                        process.Start();
                    }
                }
                else
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = ApplicationSettings.SystemBrowser;
                    process.StartInfo.Arguments = url;
                    process.StartInfo.UseShellExecute = true;
                    process.Start();
                }

            }
            catch (Exception ex)
            {
                string s = ex.Message;
                MessageBox.Show(string.Format(Strings.Program.OpenLinkError, url), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
	}
}
