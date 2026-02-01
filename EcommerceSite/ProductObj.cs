using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EcommerceSite
{
	public class ProductObj
	{
		public int ProductID { get; set; }
		public string ProductName { get; set; }
		public string ShortDescription { get; set; }
		public string LongDescription { get; set; }
		public string AdditionalDescription { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public string Size { get; set; }
		public string Color { get; set; }
		public string CompanyName { get; set; }
		public int CategoryID { get; set; }
		public int SubCategoryID { get; set; }
		public bool IsCustomized { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreateDate { get; set; }
		public List<ProductImageObj> ProductImages { get; set; }=new List<ProductImageObj>();
		public int DefaultImagePosition { get; set; }
	}
	public class ProductImageObj
	{
		public int ImageID { get; set; }
		public string ImageURL { get; set; }
		public int ProductID { get; set; }
		public bool DefaultImage { get; set; }

    }
	public class ProductDAL
	{
		SqlConnection con;
		SqlCommand cmd;
		SqlDataReader reader;
		DataTable tbl;
		SqlTransaction transaction;
		SqlDataAdapter adapter;
		public int AddUpdateProduct(ProductObj productBo)
		{
			int result = 0;
			int productID = 0;
			string type = "Insert";
			using (con=new SqlConnection(clsUtils.GetConnection()))
			{
				try
				{
					var productImages = productBo.ProductImages;
                    #region insert product
					con.Open();
					transaction = con.BeginTransaction();
					productID=productBo.ProductID;
					cmd=new SqlCommand("sp_Product",con,transaction);
					cmd.Parameters.AddWithValue("@Action", productID==0?type="Insert":type="Update");
					cmd.Parameters.AddWithValue("@ProductName", productBo.ProductName);
					cmd.Parameters.AddWithValue("@ShortDescription", productBo.ShortDescription);
					cmd.Parameters.AddWithValue("@LongDescription", productBo.LongDescription);
					cmd.Parameters.AddWithValue("@AdditionalDescription", productBo.AdditionalDescription);
					cmd.Parameters.AddWithValue("@Price", productBo.Price);
					cmd.Parameters.AddWithValue("@Quantity", productBo.Quantity);
					cmd.Parameters.AddWithValue("@Size", productBo.Size);
					cmd.Parameters.AddWithValue("@Color", productBo.Color);
					cmd.Parameters.AddWithValue("@CompanyName", productBo.CompanyName);
					cmd.Parameters.AddWithValue("@CategoryID", productBo.CategoryID);
					cmd.Parameters.AddWithValue("@SubCategoryID", productBo.SubCategoryID);
					cmd.Parameters.AddWithValue("@Sold",0);
					cmd.Parameters.AddWithValue("@IsCustomized", productBo.IsCustomized);
					cmd.Parameters.AddWithValue("@IsActive", productBo.IsActive);
					if (productID > 0)
					{
						cmd.Parameters.AddWithValue("@ProductID",productBo.ProductID);
					}
					
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.ExecuteNonQuery();
					if(productID ==0)
					{
						cmd=new SqlCommand("sp_Product",con,transaction);
						cmd.Parameters.AddWithValue("@Action", "RecentProduct");
						cmd.CommandType=CommandType.StoredProcedure;
                        reader=cmd.ExecuteReader();
						while (reader.Read())
						{
							productID = (int)reader["ProductID"];
						}
						reader.Close();
					}
					#endregion
					#region insert Images
					if (productID > 0)
					{
						if (type == "Insert")
						{

						
						foreach(var image in productImages)
						{
							cmd = new SqlCommand("sp_Product", con, transaction);
							cmd.Parameters.AddWithValue("@Action", "InsertProductImages");
							cmd.Parameters.AddWithValue("@ImageURL", image.ImageURL);
							cmd.Parameters.AddWithValue("@ProductID", productID);
							cmd.Parameters.AddWithValue("@DefaultImage", image.DefaultImage);
							cmd.CommandType= CommandType.StoredProcedure;
							cmd.ExecuteNonQuery();
							result = 1;
						}
                        }
						else // update images
						{
							bool isTrue = false;
							if(productImages.Count != 0)
							{
                                cmd = new SqlCommand("sp_Product", con, transaction);
                                cmd.Parameters.AddWithValue("@Action", "DeleteImages");
                                cmd.Parameters.AddWithValue("@ProductID", productID);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.ExecuteNonQuery();
								isTrue= true;
                            }
							else
							{
								int defaultImagePos = productBo.DefaultImagePosition;
								if(defaultImagePos > 0)
								{
                                    cmd = new SqlCommand("sp_Product", con, transaction);
                                    cmd.Parameters.AddWithValue("@Action", "UpdateImages");
                                    cmd.Parameters.AddWithValue("@ProductID", productID);
                                    cmd.Parameters.AddWithValue("@DefaultImage", defaultImagePos);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.ExecuteNonQuery();
                                    result = 1;
                                }
								else
								{
									result = 1;
								}
							}
							if (isTrue)
							{
                                foreach (var image in productImages)
                                {
                                    cmd = new SqlCommand("sp_Product", con, transaction);
                                    cmd.Parameters.AddWithValue("@Action", "InsertProductImages");
                                    cmd.Parameters.AddWithValue("@ImageURL", image.ImageURL);
                                    cmd.Parameters.AddWithValue("@ProductID", productID);
                                    cmd.Parameters.AddWithValue("@DefaultImage", image.DefaultImage);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.ExecuteNonQuery();
                                    result = 1;
                                }
                            }
						}
                    }
                    #endregion
					transaction.Commit();
                }
                catch (Exception ex)
				{
					try
					{
						transaction.Rollback();
						result = 0;
					}
					catch (Exception e)
					{
						throw;
					}
				}
			}
			return result;
		}
		public DataTable ProductByIdWithImages(int productID)
		{
			try
			{
				DataTable dt=ProductById(productID);
				dt.Columns.Add("Image2");
                dt.Columns.Add("Image3");
                dt.Columns.Add("Image4");
                dt.Columns.Add("DefaultImage");
				DataRow dr = dt.NewRow();
				string images = dt.Rows[0]["Image1"].ToString();
				string[] imgArr=images.Split(';');
				string imag;
				int rb = 0;
				foreach(string img in imgArr)
				{
					imag = img.Substring(img.IndexOf(": ") + 1);
					if (imag.Trim() == "1")
					{
						break;
					}
					else
					{
						rb++;
					}
				}
				foreach (DataRow dataRow in dt.Rows)
				{
					for(int i = 0; i < 4; i++)
					{
						dataRow["image" + (i + 1)] = imgArr[i].Trim();
					}
					dataRow["DefaultImage"] = rb;
				}
				return dt;
			}
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
                return null;
            }
		}
		public DataTable ProductById(int pID)
		{
			try
			{
				using(con=new SqlConnection(clsUtils.GetConnection()))
				{
					con.Open();
					cmd = new SqlCommand("sp_Product", con);
					cmd.Parameters.AddWithValue("@Action", "GetAllByID");
					cmd.Parameters.AddWithValue("@ProductID", pID);
					cmd.CommandType = CommandType.StoredProcedure;
					adapter=new SqlDataAdapter(cmd);
					tbl= new DataTable();
					adapter.Fill(tbl);
					return tbl;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
			//return tbl;
		}
        public DataTable ProductWithDefaultImg()
        {
            try
            {
                using (con = new SqlConnection(clsUtils.GetConnection()))
                {
                    con.Open();
                    cmd = new SqlCommand("sp_Product", con);
                    cmd.Parameters.AddWithValue("@Action", "Select");
                    cmd.CommandType = CommandType.StoredProcedure;
                    adapter = new SqlDataAdapter(cmd);
                    tbl = new DataTable();
                    adapter.Fill(tbl);
                    return tbl;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            //return tbl;
        }
    }
}