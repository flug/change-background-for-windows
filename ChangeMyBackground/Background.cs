using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ChangeMyBackground
{
    public class Background
    {
        private const uint SPI_SETDESKWALLPAPER = 0x14;
        private const uint SPIF_UPDATEINIFILE = 0x01;

        [DllImport("user32.dll")]
        private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, string pvParam, uint fWinIni);

        public void SetDWallpaper(string path)
        {
            //SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE);
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE);
        }

        private string path_file;
        private string link;
        private Bitmap bitmap;
        private string result;

        public void background()
        {
            WebClient wc = new WebClient();
            if (File.Exists(path_file))
            {
                File.Delete(path_file);
            }
            result = wc.DownloadString(new Uri("http://4walled.cc/search.php?tags=&board=&width_aspect=&searchstyle=larger&sfw=0&search=random"));

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(result);

            var src = doc.DocumentNode.SelectNodes(".//*/li[1]//img[@src]");
            foreach (var item in src)
            {
                link = item.Attributes["src"].Value;
            }
            string nlink = link.Replace("thumb", "src");
            string[] filename = nlink.Split('/');
            string nomimage = filename[filename.Length - 1];

            WebClient wc2 = new WebClient();
            try
            {
                Stream st = wc2.OpenRead(nlink);
                bitmap = new Bitmap(st);

                //Le serveur distant a retourné une erreur : (404) Introuvable.
                bitmap.Save(Directory.GetCurrentDirectory() + "/background/" + nomimage);
                st.Flush();
                st.Close();
                path_file = Directory.GetCurrentDirectory() + "/background/";
                SetDWallpaper(Directory.GetCurrentDirectory() + "/background/" + nomimage);

                foreach (var item in Directory.GetFiles(path_file))
                {
                    if (Directory.GetCurrentDirectory() + "/background/" + nomimage != item)
                    {
                        File.Delete(item);
                    }
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}