using DevExpress.Utils;
using DevExpress.XtraBars;
using System;
using System.Drawing;
using System.IO;

namespace SF.Framework
{
    public partial class Test : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Test()
        {
            InitializeComponent();

        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barCheckItem1_CheckedChanged(object sender, ItemClickEventArgs e)
        {

        }

        private void barToggleSwitchItem1_CheckedChanged(object sender, ItemClickEventArgs e)
        {

        }

        private void skinRibbonGalleryBarItem1_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void skinRibbonGalleryBarItem1_GalleryItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
        {

        }

        private void barButtonItem1_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            var a = 0;
            string[] list = DevExpress.Images.ImageResourceCache.Default.GetAllResourceKeys();
            
            foreach (var item in list)
            {
                //if (item.EndsWith(".jpg") || item.EndsWith(".png") || item.EndsWith(".gif"))
                //{
                //    Image image = DevExpress.Images.ImageResourceCache.Default.GetImage(item);
                //    string fileName = item.Substring(item.LastIndexOf("/")+1);
                //    if (fileName.EndsWith("_16x16.png"))
                //    {
                //        image.Save(@"C:\images\16x16\" + fileName.Replace("_16x16", ""));
                //    }
                //    else if (fileName.EndsWith("_32x32.png"))
                //    {
                //        image.Save(@"C:\images\32x32\" + fileName.Replace("_32x32", ""));
                //    }

                //}
                //else 
                    if (item.EndsWith(".svg"))
                {
                    
                    Console.WriteLine(a++);
                    string fileName = item.Substring(item.LastIndexOf("/")+1);
                    Stream inStream = DevExpress.Images.ImageResourceCache.Default.GetResource(item);
                    FileStream outStream = new FileStream(@"C:\images\svg\" + fileName, FileMode.Create);
                    int i = 0;
                    while (true)
                    {
                        i = inStream.ReadByte();
                        if (i != -1)
                        {
                            outStream.WriteByte((byte)i);
                        }
                        else
                        {
                            break;
                        }
                    }

                    outStream.Close();
                    inStream.Close();
                }
               
            }
            
        }
    }
}