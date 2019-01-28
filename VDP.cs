using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Configuration;

namespace IsolatedTestingOfVDP
{
    class VDP
    {
        public int buildingsConstructedSoFar = 0;
        public Bitmap defaultImage = (Bitmap)Image.FromFile("C: \\Users\\David H\\Documents\\Visual Studio 2017\\Projects\\IsolatedTestingOfVDP\\IsolatedTestingOfVDP\\DefaultImage\\editedfullmap.jpg");
        public Dictionary<int, Buildings> CityMap = new Dictionary<int, Buildings>();
        public struct Buildings
        {
            public int owner { get; set; }
            public int type { get; set; }
            public int level { get; set; }
            public string name { get; set; }
            public int x { get; set; }
            public int y { get; set; }
        };

       
        //returns a bitmap image for use
        public Bitmap GetImage(string name)
        {
            Bitmap aBuilding;

            if (name == "Baracks")
            {
                aBuilding = (Bitmap)Image.FromFile(ConfigurationManager.AppSettings["BaracksCell.jpg"]);
                return aBuilding;
            }
            else if (name == "BlackSmith")
            {
                aBuilding = (Bitmap)Image.FromFile(ConfigurationManager.AppSettings["BlackSmithCell.jpg"]);
                return aBuilding;

            }
            else if (name == "Farm")
            {
                aBuilding = (Bitmap)Image.FromFile(ConfigurationManager.AppSettings["FarmCell.png"]);
                return aBuilding;

            }
            else if (name == "Grass")
            {
                aBuilding = (Bitmap)Image.FromFile(ConfigurationManager.AppSettings["GrassCell.jpg"]);
                return aBuilding;

            }
            else if (name == "Magic")
            {
                aBuilding = (Bitmap)Image.FromFile(ConfigurationManager.AppSettings["MagicCell.jpg"]);
                return aBuilding;

            }
            else if (name == "Science")
            {
                aBuilding = (Bitmap)Image.FromFile(ConfigurationManager.AppSettings["ScienceCell.jpg"]);
                return aBuilding;

            }
            else if (name == "Storage")
            {
                aBuilding = (Bitmap)Image.FromFile(ConfigurationManager.AppSettings["StorageCell.jpg"]);
                return aBuilding;

            }
            else if (name == "Wall")
            {
                aBuilding = (Bitmap)Image.FromFile(ConfigurationManager.AppSettings["WallCell.jpg"]);
                return aBuilding;

            }
            else
            {
                Console.Out.WriteLine("Image Doesn't have a match");
                return null;
            }
        }
        //Clears the dictionairy
        public void WrapItUp()
        {
            CityMap.Clear();
            Console.Out.WriteLine("CityMap Cleared");
        }
        //prints out dictionairy
        public void PrintIt()
        {
            for(int i = 0; i < CityMap.Count; i++)
            {
                Console.Out.WriteLine(CityMap[i].name);
            }

        }
        //hashes two int vals into one hashed key
        public int HashIt(int valX, int valY)
        {
            //key gets hashed or an error gets thrown
            if (valX >= 0 && valY >= 0 && valX<=40 && valY<=40)
            {
                int hashedKey = 0;
                hashedKey = valX * 100;
                hashedKey = hashedKey + valY;
                return hashedKey;
            }
            else
            {
                //error occurred
                return int.MinValue;
            }
        }
        
        //creates a image at a certain point
        public void CreateBuilding(Buildings buildings)
        {
            if (buildingsConstructedSoFar == 0)
            {


                if (System.IO.File.Exists(ConfigurationManager.AppSettings["ForSaving"] + "User" + buildings.owner + ".jpg"))
                {
                    defaultImage.Dispose();
                    Bitmap defaultImage1 = (Bitmap)Image.FromFile(ConfigurationManager.AppSettings["ForSaving"] + "User" + buildings.owner + ".jpg");
                    defaultImage = new Bitmap(defaultImage1);
                    defaultImage1.Dispose();
                    System.IO.File.Delete(ConfigurationManager.AppSettings["ForSaving"] + "User" + buildings.owner + ".jpg");
                }
                int[] arr = InvertyXYBecauseWhyisTheOriginTopLeft(buildings.x, buildings.y);
                int x = arr[0] * 70;
                int y = arr[1] * 70;
                Bitmap overlayImage = GetImage(buildings.name);
                var graphics = Graphics.FromImage(defaultImage);
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.DrawImage(overlayImage, x, y);     


                defaultImage.Save(ConfigurationManager.AppSettings["ForSaving"] + "User" + buildings.owner + ".jpg", ImageFormat.Jpeg);
                graphics.Dispose();
                overlayImage.Dispose();
                defaultImage.Dispose();
                buildingsConstructedSoFar++;
            }
            else
            {
                Bitmap baseImage1 = (Bitmap)Image.FromFile(ConfigurationManager.AppSettings["ForSaving"] + "User" + buildings.owner + ".jpg");
                Bitmap baseImage = new Bitmap(baseImage1);
                baseImage1.Dispose();
                if(System.IO.File.Exists(ConfigurationManager.AppSettings["ForSaving"] + "User" + buildings.owner + ".jpg"))
                     System.IO.File.Delete(ConfigurationManager.AppSettings["ForSaving"] + "User" + buildings.owner + ".jpg");

                int[] arr = InvertyXYBecauseWhyisTheOriginTopLeft(buildings.x, buildings.y);
                int x = arr[0] * 70;
                int y = arr[1] * 70;
                Bitmap overlayImage = GetImage(buildings.name);
                var graphics = Graphics.FromImage(baseImage);
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.DrawImage(overlayImage, x, y);
                
                baseImage.Save(ConfigurationManager.AppSettings["ForSaving"] + "User" + buildings.owner + ".jpg", ImageFormat.Jpeg);
                graphics.Dispose();
                baseImage.Dispose();
                overlayImage.Dispose();
                buildingsConstructedSoFar++;
            }
            Console.Out.WriteLine("Buildings Constructed So Far: " + buildingsConstructedSoFar);
        }

        public int[] InvertyXYBecauseWhyisTheOriginTopLeft(int x, int y)
        {
            int[] arr = new int[2];
            y = 40 - y;
            arr[0] = x;
            arr[1] = y;
            return arr;
        }

    }
}
