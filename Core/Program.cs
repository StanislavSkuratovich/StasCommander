using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class Program
    {
        static void Main()
        {
            #region driveexample

            foreach (var drive in DriveInfo.GetDrives())
            {
                double freeSpace = drive.TotalFreeSpace;
                double totalSpace = drive.TotalSize;
                double percentFree = (freeSpace / totalSpace) * 100;
                float num = (float)percentFree;
                var temp = new FileInfo("H:/0505201211156.jpg").Length;

                Console.WriteLine(temp);

                Console.WriteLine("Drive:{0} With {1} % free", drive.Name, num);
                Console.WriteLine("Space Remaining:{0}", drive.AvailableFreeSpace);
                Console.WriteLine("Percent Free Space:{0}", percentFree);
                Console.WriteLine("Space used:{0}", drive.TotalSize);
                Console.WriteLine("Type: {0}", drive.DriveType);
            }
            #endregion

            Console.ReadKey();
        }
    }
}
