using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using WpfApp4.Models;

namespace WpfApp4.ViewModels
{
public    class CollectData
    {
        ManagementObjectCollection moReturn;
        ManagementObjectSearcher moSearch;

        public void CollectData_()
        {
             moSearch = new ManagementObjectSearcher("Select * from Win32_DiskDrive");
            moReturn = moSearch.Get();
            foreach (ManagementObject mo in moReturn)
            {
                try
                {
                    ia = mo["Caption"].ToString().ToUpper();
                }
                catch { }
                try
                {
                    double ib = Convert.ToDouble(mo["Size"].ToString().Trim()) / 1024 / 1024 / 1024;
                    lOEM.Add("Size: " + ib.ToString("#.##") + " GB,  " + "InterfaceType: " + mo["InterfaceType"].ToString().ToUpper().Trim());
                }
                catch { }
                try
                {
                    ia = mo["Signature"].ToString().ToUpper().Trim();
                    //                lOEM.Add("Signature: " + ia);
                    ia = mo["Partitions"].ToString().ToUpper().Trim();
                    // lOEM.Add("Partitions: " + ia);
                }
                catch { }
                try
                {
                    ia = mo["SerialNumber"].ToString().Trim().ToUpper();
                    lOEM.Add("SerialNumber: " + ia);
                }
                catch { }
                try
                {
                    //   ia = mo["MediaType"].ToString().ToUpper();
                    //   lOEM.Add("MediaType: " + ia);
                }
                catch { }
            }
        }

        protected override void WndProc(ref Message msg)
        {
            if (msg.Msg == 537) //WM_DEVICECHANGE
                System.Windows.MessageBox.Show("Device Changed " + msg.WParam + " " + msg.LParam);
            base.WndProc(ref msg);
        }
        public void GetEvent()
        {
             case SERVICE_CONTROL_DEVICEEVENT:
            if (dwEventType == DBT_DEVICEARRIVAL)
            {
                PDEV_BROADCAST_HDR pHdr = (PDEV_BROADCAST_HDR)lpEventData;

                switch (pHdr->dbch_devicetype)
                {
                    case DBT_DEVTYP_VOLUME:
                        {
                            PDEV_BROADCAST_VOLUME lpdbv = (PDEV_BROADCAST_VOLUME)lpdb;
                            //...
                            break;
                        }
                    case DBT_DEVTYP_DEVICEINTERFACE:
                        {
                            //...
                            break;
                        }

                        //Other code ...

                }
                break;
            }
        }
    }
}
