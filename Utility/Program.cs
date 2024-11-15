using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BrethrenRepository;
using BrethrenModels;
using System.Drawing;

namespace Utility
{
    class Preface
    {
        public int sno { get; set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GeneratePrefaceJson();
            return;
            CropImage();
            return;
            var alllines = File.ReadAllLines(@"C:\Vincent\Projects\BookService-master\Utility\EvangelistsInfo.txt");
            foreach(var line in alllines)
            {
                try
                {
                    var evangelistInfo = line.Split(new char[] { '\t' });
                    if (evangelistInfo.Length != 5)
                    {

                    }

                    EvangelistFullDTO evangelistFullDTO = new EvangelistFullDTO();
                    evangelistFullDTO.Name = evangelistInfo[0].Trim();
                    evangelistFullDTO.City = evangelistInfo[1].Trim();
                    evangelistFullDTO.District = evangelistInfo[2].Trim();
                    evangelistFullDTO.State = evangelistInfo[3].Trim();
                    evangelistFullDTO.PermanantPhoneNo = evangelistInfo[4].Trim();
                    evangelistFullDTO.Country = "India";
                    BrethrenDB db = new BrethrenDB();
                    db.SaveEvangelist(evangelistFullDTO);
                }
                catch(Exception ex)
                {

                }
            }
        }

        static void CropImage() {
            try
            {
                foreach(var file in Directory.EnumerateFiles(@"C:\Users\Vincent\Desktop\Server Pics\"))
                {
                    Bitmap bmp = new Bitmap(file);
                    int size = 100;
                    Bitmap res = new Bitmap(size, size);
                    Graphics g = Graphics.FromImage(res);
                    g.FillRectangle(new SolidBrush(Color.White), 0, 0, size, size);
                    int t = 0, l = 0;
                    if (bmp.Height > bmp.Width)
                        t = (bmp.Height - bmp.Width) / 2;
                    else
                        l = (bmp.Width - bmp.Height) / 2;
                    g.DrawImage(bmp, new Rectangle(0, 0, size, size), new Rectangle(l, t, bmp.Width - l * 2, bmp.Height - t * 2), GraphicsUnit.Pixel);
                    string newfile = file.Replace(".jpg", "_small.jpg");
                    res.Save(newfile);
                }
                /*Bitmap bmp = new Bitmap(@"C:\Users\Vincent\Desktop\Pics\22ea7a05-4f31-4622-92a8-38a8fc1103ac.jpg");
                int size = 200;
                Bitmap res = new Bitmap(size, size);
                Graphics g = Graphics.FromImage(res);
                g.FillRectangle(new SolidBrush(Color.White), 0, 0, size, size);
                int t = 0, l = 0;
                if (bmp.Height > bmp.Width)
                    t = (bmp.Height - bmp.Width) / 2;
                else
                    l = (bmp.Width - bmp.Height) / 2;
                g.DrawImage(bmp, new Rectangle(0, 0, size, size), new Rectangle(l, t, bmp.Width - l * 2, bmp.Height - t * 2), GraphicsUnit.Pixel);
                res.Save(@"C:\Users\Vincent\Desktop\Pics\22ea7a05-4f31-4622-92a8-38a8fc1103ac_123.jpg") ;*/
            }
            catch { }

        }

        static void GeneratePrefaceJson()
        {
            List<Preface> prefacelist = new List<Preface>();
            Preface p1 = new Preface();
            p1.sno = 1;
            p1.Name = "M.C.Newton Bob";
            p1.ImageName = "newtonbob.jpg";
            p1.Content = File.ReadAllText("newtonbob.txt");
            prefacelist.Add(p1);

            Preface p2 = new Preface();
            p2.sno = 2;
            p2.Name = "Bro P.Wilson";
            p2.ImageName = "wilson.jpg";
            p2.Content = File.ReadAllText("wilson.txt");
            prefacelist.Add(p2);

            Preface p3 = new Preface();
            p3.sno = 3;
            p3.Name = "Bro S.Isaac Babu";
            p3.ImageName = "isaacbabu.jpg";
            p3.Content = File.ReadAllText("isaacbabu.txt");
            prefacelist.Add(p3);

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(prefacelist);
            File.WriteAllText("preface.json", json);

        }

    }
}















