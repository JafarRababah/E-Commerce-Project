using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace EcommerceSite.Admin
{
    public partial class Product : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable tbl;
        string[] imagePath;
        ProductObj productObj;
        ProductDAL productDAL;
        List<ProductImageObj> productImages = new List<ProductImageObj>();
        int defaultImgAfterEdit = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["breakCumbTitle"] = "Manage Product";
            Session["breakCumbPage"] = "Product";
            if (!IsPostBack)
            {
                GetCategories();
                if (Request.QueryString["id"] != null)
                {
                    GetProductDetails();
                }
            }
            lblMsg.Visible = false;
        }
        void GetCategories()
        {
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_Category", con);
            cmd.Parameters.AddWithValue("@Action", "GetAll");
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            tbl = new DataTable();
            adapter.Fill(tbl);
            ddlCategory.DataSource = tbl;
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryID";
            ddlCategory.DataBind();
        }
        void GetSubCategories(int CategoryId)
        {
            con = new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("sp_SubCategory", con);
            cmd.Parameters.AddWithValue("@Action", "SubCategoryByID");
            cmd.Parameters.AddWithValue("@CategoryID", CategoryId);
            cmd.CommandType = CommandType.StoredProcedure;
            adapter = new SqlDataAdapter(cmd);
            tbl = new DataTable();
            adapter.Fill(tbl);
            ddlSubCategory.Items.Clear();
            ddlSubCategory.DataSource = tbl;
            ddlSubCategory.DataTextField = "SubCategoryName";
            ddlSubCategory.DataValueField = "SubCategoryID";
            ddlSubCategory.DataBind();
            ddlSubCategory.Items.Insert(0, "Select SubCategory");
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSubCategories(Convert.ToInt32(ddlCategory.SelectedValue));
        }
        void GetProductDetails()
        {
            if (Request.QueryString["id"] != null)
            {
                int productID=Convert.ToInt32(Request.QueryString["id"]);
                productDAL = new ProductDAL();
                DataTable dt=productDAL.ProductByIdWithImages(productID);
                if(dt.Rows.Count > 0)
                {
                    txtProductName.Text = dt.Rows[0]["ProductName"].ToString();
                    txtProductPrice.Text = dt.Rows[0]["Price"].ToString();
                    txtQuantity.Text = dt.Rows[0]["Quantity"].ToString();
                    txtShortdescription.Text = dt.Rows[0]["ShortDescription"].ToString();
                    txtLongDescription.Text = dt.Rows[0]["LongDescription"].ToString();
                    txtAdditionalDescription.Text = dt.Rows[0]["AdditionalDescription"].ToString();
                    string[] color = dt.Rows[0]["Color"].ToString().Split('\u002c');
                    string[] size = dt.Rows[0]["Size"].ToString().Split('\u002c');
                    for(int i = 0; i < color.Length - 1; i++)
                    {
                        lboxColor.Items.FindByText(color[i]).Selected=true;
                    }
                    for(int i=0;i<size.Length - 1; i++)
                    {
                        lboxSize.Items.FindByText(size[i]).Selected=true;
                    }
                    txtCompanyName.Text = dt.Rows[0]["CompanyName"].ToString() ;
                    ddlCategory.SelectedValue = dt.Rows[0]["CategoryID"].ToString();
                    GetSubCategories(Convert.ToInt32(dt.Rows[0]["CategoryID"]));
                    ddlSubCategory.SelectedValue = dt.Rows[0]["SubCategoryID"].ToString();
                    cbIsCustomized.Checked = Convert.ToBoolean(dt.Rows[0]["IsCustomized"]);
                    cbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                    rblDefaultImage.SelectedIndex = Convert.ToInt32(dt.Rows[0]["DefaultImage"]);
                    hfDefImagePos.Value = (Convert.ToInt32(dt.Rows[0]["DefaultImage"]) + 1).ToString();
                    imageProduct1.ImageUrl = "../" + dt.Rows[0]["Image1"].ToString().Substring(0, dt.Rows[0]["Image1"].ToString().IndexOf(":"));
                    imageProduct2.ImageUrl = "../" + dt.Rows[0]["Image2"].ToString().Substring(0, dt.Rows[0]["Image2"].ToString().IndexOf(":"));
                    imageProduct3.ImageUrl = "../" + dt.Rows[0]["Image3"].ToString().Substring(0, dt.Rows[0]["Image3"].ToString().IndexOf(":"));
                    imageProduct4.ImageUrl = "../" + dt.Rows[0]["Image4"].ToString().Substring(0, dt.Rows[0]["Image4"].ToString().IndexOf(":"));
                    imageProduct1.Width = 200;
                    imageProduct2.Width = 200;
                    imageProduct3.Width = 200;
                    imageProduct4.Width = 200;
                    imageProduct1.Style.Remove("display");
                    imageProduct2.Style.Remove("display");
                    imageProduct3.Style.Remove("display");
                    imageProduct4.Style.Remove("display");
                    btnAddOrUpdate.Text = "Update";
                }
            }
        }
        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                string selectedColor = string.Empty;
                string selectedSize = string.Empty;
                bool isValid = false;
                bool isValidToExcute = false;
                List<string> list = new List<string>();
                bool isImageSaved = false;
                if (Request.QueryString["id"] == null)
                {
                    if (fuFirstImage.HasFile && fuSecondImage.HasFile && fuThirdImage.HasFile && fuFourthImage.HasFile)
                    {
                        list.Add(fuFirstImage.FileName);
                        list.Add(fuSecondImage.FileName);
                        list.Add(fuThirdImage.FileName);
                        list.Add(fuFourthImage.FileName);
                        string[] fu = list.ToArray();
                        #region validate images
                        for (int i = 0; i <= fu.Length - 1; i++)
                        {
                            if (clsUtils.IsValidExtention(fu[i]))
                            {
                                isValid = true;
                            }
                            else
                            {
                                isValid = false; break;
                            }
                        }
                        #endregion
                        #region after image validation proceeding to add product
                        if (isValid)
                        {
                            imagePath = clsUtils.getImagePath(fu);
                            for (int i = 0; i < imagePath.Length; i++)
                            {

                                for (int j = i; i < rblDefaultImage.Items.Count;)
                                {
                                    productImages.Add
                                        (
                                        new ProductImageObj()
                                        {
                                            ImageURL = imagePath[i],
                                            DefaultImage = Convert.ToBoolean(rblDefaultImage.Items[j].Selected)
                                        }
                                        );
                                    break;

                                }
                                #region saving all images
                                if (i == 0)
                                {
                                    fuFirstImage.PostedFile.SaveAs(Server.MapPath("~/Images/Product/") + imagePath[i].Replace("Images/Product/", ""));
                                    isImageSaved = true;
                                }
                                if (i == 1)
                                {
                                    fuSecondImage.PostedFile.SaveAs(Server.MapPath("~/Images/Product/") + imagePath[i].Replace("Images/Product/", ""));
                                    isImageSaved = true;
                                }
                                if (i == 2)
                                {
                                    fuThirdImage.PostedFile.SaveAs(Server.MapPath("~/Images/Product/") + imagePath[i].Replace("Images/Product/", ""));
                                    isImageSaved = true;
                                }
                                if (i == 3)
                                {
                                    fuFourthImage.PostedFile.SaveAs(Server.MapPath("~/Images/Product/") + imagePath[i].Replace("Images/Product/", ""));
                                    isImageSaved = true;
                                }
                                #endregion
                            }
                            #region saving new product
                            if (isImageSaved)
                            {
                                selectedColor = clsUtils.getItemWithCommaSeparater(lboxColor);
                                selectedSize = clsUtils.getItemWithCommaSeparater(lboxSize);
                                productDAL = new ProductDAL();
                                productObj = new ProductObj()
                                {
                                    ProductID =Request.QueryString["id"]==null?0: Convert.ToInt32(Request.QueryString["id"]),
                                    ProductName = txtProductName.Text.Trim(),
                                    ShortDescription = txtShortdescription.Text.Trim(),
                                    LongDescription = txtLongDescription.Text.Trim(),
                                    AdditionalDescription = txtAdditionalDescription.Text.Trim(),
                                    Price = Convert.ToDecimal(txtProductPrice.Text.Trim()),
                                    Quantity = Convert.ToInt32(txtQuantity.Text.Trim()),
                                    Size = selectedSize,
                                    Color = selectedColor,
                                    CompanyName = txtCompanyName.Text.Trim(),
                                    CategoryID = Convert.ToInt32(ddlCategory.SelectedValue),
                                    SubCategoryID = Convert.ToInt32(ddlSubCategory.SelectedValue),
                                    IsCustomized = cbIsCustomized.Checked,
                                    IsActive = cbIsActive.Checked,
                                    ProductImages = productImages
                                };
                                int r = productDAL.AddUpdateProduct(productObj);
                                if (r > 0)
                                {
                                    DisplayMessage("Product Saved Successful", "success");
                                    Response.AddHeader("REFRESH", "2;URL=ProductList.aspx");
                                }
                                else
                                {
                                    DeleteFile(imagePath);
                                    DisplayMessage("Product Cannot Save", "warning");
                                }
                            }
                            else
                            {
                                DeleteFile(imagePath);
                            }
                            #endregion
                        }

                        else
                        {
                            DisplayMessage("Please Select .jpg, jpeg, png file for images", "warning");
                        }

                        #endregion

                    }

                  
                    else
                    {
                        DisplayMessage("Please Select all product images", "warning");
                    }
                }
                else
                {
                    if (fuFirstImage.HasFile && fuSecondImage.HasFile && fuThirdImage.HasFile && fuFourthImage.HasFile)
                    {
                        list.Add(fuFirstImage.FileName);
                        list.Add(fuSecondImage.FileName);
                        list.Add(fuThirdImage.FileName);
                        list.Add(fuFourthImage.FileName);
                        string[] fu = list.ToArray();
                        #region validate images
                        for (int i = 0; i < fu.Length; i++)
                        {
                            if (clsUtils.IsValidExtention(fu[i]))
                            {
                                isValid = true;
                            }
                            else
                            {
                                isValid = false; break;
                            }
                        }
                        #endregion

                        #region after image validation proceeding to add product
                        if (isValid)
                        {
                            imagePath = clsUtils.getImagePath(fu);
                            for (int i = 0; i < imagePath.Length ; i++)
                            {

                                for (int j = i; i < rblDefaultImage.Items.Count;)
                                {
                                    productImages.Add
                                        (
                                        new ProductImageObj()
                                        {
                                            ImageURL = imagePath[i],
                                            DefaultImage = Convert.ToBoolean(rblDefaultImage.Items[j].Selected)
                                        }
                                        );
                                    break;

                                }
                                #region saving all images
                                if (i == 0)
                                {
                                    fuFirstImage.PostedFile.SaveAs(Server.MapPath("~/Images/Product/") + imagePath[i].Replace("Images/Product/", ""));
                                    isImageSaved = true;
                                }
                                if (i == 1)
                                {
                                    fuSecondImage.PostedFile.SaveAs(Server.MapPath("~/Images/Product/") + imagePath[i].Replace("Images/Product/", ""));
                                    isImageSaved = true;
                                }
                                if (i == 2)
                                {
                                    fuThirdImage.PostedFile.SaveAs(Server.MapPath("~/Images/Product/") + imagePath[i].Replace("Images/Product/", ""));
                                    isImageSaved = true;
                                }
                                if (i == 3)
                                {
                                    fuFourthImage.PostedFile.SaveAs(Server.MapPath("~/Images/Product/") + imagePath[i].Replace("Images/Product/", ""));
                                    isImageSaved = true;
                                }
                                #endregion
                            }
                            
                            if (isImageSaved)
                            {
                                isValidToExcute = true;
                            }
                            else
                            {
                                DeleteFile(imagePath);
                            }
                            
                        }

                        else
                        {
                            DisplayMessage("Please Select .jpg, jpeg, png file for images", "warning");
                        }

                        #endregion
                    }
                    else if (fuFirstImage.HasFile || fuSecondImage.HasFile || fuThirdImage.HasFile || fuFourthImage.HasFile)
                    {
                        DisplayMessage("Please add all 4 product images if yoou want to update it", "warning");
                    }
                    else
                    {
                        // update product without image
                        if (Convert.ToInt32(hfDefImagePos.Value) != Convert.ToInt32(rblDefaultImage.SelectedValue))
                        {
                            defaultImgAfterEdit=Convert.ToInt32(rblDefaultImage.SelectedValue);
                        }
                        isValidToExcute=true;
                    }

                    #region updating  product
                    if (isImageSaved)
                    {
                        selectedColor = clsUtils.getItemWithCommaSeparater(lboxColor);
                        selectedSize = clsUtils.getItemWithCommaSeparater(lboxSize);
                        productDAL = new ProductDAL();
                        productObj = new ProductObj()
                        {
                            ProductID = Convert.ToInt32(Request.QueryString["id"]),
                            ProductName = txtProductName.Text.Trim(),
                            ShortDescription = txtShortdescription.Text.Trim(),
                            LongDescription = txtLongDescription.Text.Trim(),
                            AdditionalDescription = txtAdditionalDescription.Text.Trim(),
                            Price = Convert.ToDecimal(txtProductPrice.Text.Trim()),
                            Quantity = Convert.ToInt32(txtQuantity.Text.Trim()),
                            Size = selectedSize,
                            Color = selectedColor,
                            CompanyName = txtCompanyName.Text.Trim(),
                            CategoryID = Convert.ToInt32(ddlCategory.SelectedValue),
                            SubCategoryID = Convert.ToInt32(ddlSubCategory.SelectedValue),
                            IsCustomized = cbIsCustomized.Checked,
                            IsActive = cbIsActive.Checked,
                            ProductImages = productImages,
                            DefaultImagePosition = defaultImgAfterEdit
                        };
                        int r = productDAL.AddUpdateProduct(productObj);
                        if (r > 0)
                        {
                            DisplayMessage("Product updated Successful", "success");
                            Response.AddHeader("REFRESH", "2;URL=ProductList.aspx");
                        }
                        else
                        {
                            DeleteFile(imagePath);
                            DisplayMessage("Product Cannot update at this moment", "warning");
                        }
                    }
                    else
                    {
                        DisplayMessage("Something wrong", "danger");
                    }
                    #endregion
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        void DeleteFile(string[] filePath)
        {
            for (int i = 0; filePath.Length > i; i++)
            {
                if (File.Exists(Server.MapPath("_/" + filePath[i])))
                {
                    File.Delete(Server.MapPath("-/" + filePath[i]));
                }

            }
        }
        void DisplayMessage(string message, string cssClass)
        {
            lblMsg.Visible = true;
            lblMsg.Text = message;
            lblMsg.CssClass = "alert alert" + cssClass;
        }
        private void Clear()
        {
            txtAdditionalDescription.Text= string.Empty;
            txtCompanyName.Text= string.Empty;
            txtLongDescription.Text= string.Empty;
            txtProductName.Text= string.Empty;
            txtProductPrice.Text= string.Empty;
            txtQuantity.Text= string.Empty;
            txtShortdescription.Text= string.Empty;
            txtTags.Text= string.Empty;
            ddlCategory.ClearSelection();
            ddlSubCategory.ClearSelection();
            lblMsg.Text= string.Empty;
            lboxColor.ClearSelection();
            lboxSize.ClearSelection();
            cbIsActive.Checked= false;
            cbIsCustomized.Checked= false;
            hfDefImagePos.Value = "0";
        }
    }
}
