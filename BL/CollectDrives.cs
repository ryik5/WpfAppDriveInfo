using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WpfApp4.Models;

namespace WpfApp4
{
    public class CollectDrives
    {
        IList<DriveModel> driveList;
        public IList<DriveModel> GetDrives( )
        {
            driveList = new List<DriveModel>();
            DriveModel drive;

            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady)
                {
                    drive = GetDriveInfo(d);

                    driveList.Add(drive);
                }
            }

            return driveList;
        }

        private DriveModel GetDriveInfo(DriveInfo d)
        {
            DriveModel drive = new DriveModel();

            string name = d.Name;
            double totalSpace = d.TotalSize;
            double freeSpace = d.TotalFreeSpace;
            string fileSystem = d.DriveFormat;
            string mediaType = d.DriveType.ToString();

            drive.Name = name;
            drive.Caption = name + fileSystem + totalSpace + mediaType;
            drive.FileSystem = GetFileSystem(fileSystem);
            drive.Type = GetDriveType(mediaType);
            drive.TotalSpace = totalSpace;
            drive.FreeSpace = freeSpace;

            return drive;
        }

        private FileSystem GetFileSystem(string type)
        {
            if (type?.Length == 0)
            {
                return FileSystem.Unknown;
            }
            else if (type.ToLower().Contains("fat32"))
            {
                return FileSystem.FAT32;
            }
            else if (type.ToLower().Contains("ntfs"))
            {
                return FileSystem.NTFS;
            }
            else
            {
                return FileSystem.Unknown;
            }
        }
        private DiskType GetDriveType(string type)
        {
            if (type?.Length == 0)
            {
                return DiskType.Unknown;
            }
            else if (type.ToLower().Contains("remov"))
            {
                return DiskType.Removable;
            }
            else if (type.ToLower().Contains("fix"))
            {
                return DiskType.Fixed;
            }
            else
            {
                return DiskType.Unknown;
            }
        }

    }
}
