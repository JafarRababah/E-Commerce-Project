using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;

namespace EcommerceSite
{

    public class clsUtils
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        SqlDataReader reader;
        DataTable tbl;

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
    }

}