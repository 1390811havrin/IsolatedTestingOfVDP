using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace IsolatedTestingOfVDP
{
    public class VDPProgram
    {
  

        public static void Main(string[] args)
        {

            //for testing purposes only

            string[] test = args;
            /*
            test[0] = "1,Baracks,1,1,7,9";
            test[1] = "1,Baracks,1,1,7,10";
            test[2] = "1,BlackSmith,1,1,7,11";
            test[3] = "1,Farm,1,1,7,12";
            */
            VDP vdp = new VDP();

            //We will be receiving and processing the strings one at a time
            //[ int ownerID, string buildingName, int buildingType, int buildingLevel, int x, int y]
            int index;
            int ownerId;
            string bName;
            int bType;
            int bLevel;
            int x;
            int y;
            string scratch;
            string temp;

            for (int i = 0; i < test.Length; i++)
            {

               //does ownerId
                temp = test[i];
                index = temp.IndexOf(',');
                scratch = temp.Substring(0, index);
                ownerId = Convert.ToInt32(scratch);
                temp = temp.Remove(0, index+1);

                index = temp.IndexOf(',');
                bName = temp.Substring(0, index);
                temp = temp.Remove(0, index+1);

                index = temp.IndexOf(',');
                scratch = temp.Substring(0, index);
                bType = Convert.ToInt32(scratch);
                temp = temp.Remove(0, index+1);

                index = temp.IndexOf(',');
                scratch = temp.Substring(0, index);
                bLevel = Convert.ToInt32(scratch);
                temp = temp.Remove(0, index+1);

                index = temp.IndexOf(',');
                scratch = temp.Substring(0, index);
                x = Convert.ToInt32(scratch);
                temp = temp.Remove(0, index+1);

           
                scratch = temp.Substring(0);
                y = Convert.ToInt32(scratch);
                temp = string.Empty;
                VDP.Buildings buildings = new VDP.Buildings();
                buildings.x = x;
                buildings.y = y;
                buildings.level = bLevel;
                buildings.name = bName;
                buildings.type = bType;
                buildings.owner = ownerId;
                index = vdp.HashIt(x, y);
                vdp.CityMap.Add(index, buildings);
            }
            int[] arr = vdp.CityMap.Keys.ToArray();
            for(int i = 0; i < arr.Length; i++)
            {
                vdp.CreateBuilding(vdp.CityMap[arr[i]]);
            }
        }
    }
}
