using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls;

namespace EcommerceSite
{

    public class clsUtils
    {
       

        public static string GetConnection()
        {
            return ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }
        public static bool IsValidExtention(string FileName)
        {
            bool IsValid = false;
            string[] FileExtention = { ".jpg",".png",".jpeg" };
            foreach(string file in FileExtention)
            {
                if (FileName.Contains(file))
                {
                    IsValid = true;
                    break;
                }
            }
            return IsValid;
        }
        public static string GetUniqueID()
        {
            Guid guid=Guid.NewGuid();
            return guid.ToString();
        }
        public static string GetImageUrl(Object url)
        {
            string rul1 = string.Empty;
            if (string.IsNullOrEmpty(url.ToString()) || url == DBNull.Value)
            {
                rul1 = "../Images/No_image.png";
            }
            else
            {
                rul1 = string.Format("../{0}", url);
            }
            return rul1;
        }
        public static string[] getImagePath(string[] images)
        {
            List<string> list = new List<string>();
            string fileExtentiom = string.Empty;
            for (int i = 0; i < images.Length; i++)
            {
                fileExtentiom=Path.GetExtension(images[i]).Trim();
                list.Add("Images/Product/" + GetUniqueID().ToString() + fileExtentiom);
            }
            return list.ToArray();
        }
        public static string getItemWithCommaSeparater(ListBox listBox)
        {
            string selectedItem = string.Empty;
            foreach(var item in listBox.GetSelectedIndices())
            {
                selectedItem = listBox.Items[item].Text+",";
            }
            return selectedItem;
        }

    }

}