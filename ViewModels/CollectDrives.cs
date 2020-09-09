using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WpfApp4.Models;

namespace WpfApp4.ViewModels
{
    public class CollectDrives
    {

        public void GetDrives(ref DriveViewModels drives)
        {
            DriveModel drive;
            List<string> uniqDrives = new List<string>();

            DriveInfo[] allDrives = DriveInfo.GetDrives();

            if (drives?.Collection?.Count > 0)
            {
                uniqDrives = drives.GetUniqDrives();
            }

            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady)
                {
                    drive = GetDriveInfo(d);

                    drives.Collection.Add(drive);
                }
            }
        }

        public void CheckListDrives(ref DriveViewModels drives)
        {
            DriveModel drive;
            List<string> uniqDrives = new List<string>();

            DriveInfo[] allDrives = DriveInfo.GetDrives();

            if (drives?.Collection?.Count > 0)
            {
                uniqDrives = drives.GetUniqDrives();
            }

            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady)
                {
                    drive = GetDriveInfo(d);
                    if (drives?.Collection?.Count > 0)
                    {

                        if (!drives.Collection.Any(p => p.GetId() == drive.GetId()))
                        {
                            drives.Collection.Add(drive);
                        }
                        else
                        {
                            drives.Collection.Add(drives.Collection.Where(i => i.GetId() == drive.GetId()).Single());
                        }
                    }
                }
            }
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
