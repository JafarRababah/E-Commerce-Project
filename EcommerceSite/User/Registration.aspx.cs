using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceSite.User
{
    public partial class Registration : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable tbl;
        ProductDAL productDAL;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnRegisterOrUpdate_Click(object sender,EventArgs e)
        {
            string actionName=string.Empty, imagePath=string.Empty, fileExtention=string.Empty;
            bool isValidToExcute = false;
            int userId=Convert.ToInt32(Request.QueryString["id"]);
            con=new SqlConnection(clsUtils.GetConnection());
            cmd = new SqlCommand("User", con);
            cmd.Parameters.AddWithValue("@Action", userId == 0 ? "Insert" : "Update");
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@UserId", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@UserId", txtUsername.Text.Trim());
            cmd.Parameters.AddWithValue("@UserId", txtMobile.Text.Trim());
            cmd.Parameters.AddWithValue("@UserId", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@UserId", txtAddress.Text.Trim());
            cmd.Parameters.AddWithValue("@UserId", txtPostCode.Text.Trim());
            cmd.Parameters.AddWithValue("@UserId", txtPassword.Text.Trim());
            if (fuUserImage.HasFile)
            {
                if(clsUtils.IsValidExtention(fuUserImage.FileName))
                {
                    string newImageName = clsUtils.GetUniqueID();
                    fileExtention=Path.GetExtension(fuUserImage.FileName).Trim();
                    imagePath = "Images/User/" + newImageName.ToString() + fileExtention;
                    fuUserImage.PostedFile.SaveAs(Server.MapPath("~/Images/User") + newImageName.ToString() + fileExtention);
                    cmd.Parameters.AddWithValue("@ImageUrl",imagePath);
                    isValidToExcute = true;
                }
                else
                {
                    lblMsg.Visible= false;
                    lblMsg.Text = "";
                    lblMsg.CssClass = "alert alert-danger";
                    isValidToExcute= false;
                }
                
            }
            else
            {
                isValidToExcute = true;
            }
            if (isValidToExcute)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    actionName = userId == 0 ? "registration is succefull <b><a href='Login.aspx'>Click'here</a></b> to do login" :
                        "detail updated succefull <b><a href='Profile.aspx'>Can check here</a></b>";
                    lblMsg.Visible = true;
                    lblMsg.Text = "<b>" + txtUsername.Text.Trim() + "</b>" + actionName;
                    lblMsg.CssClass = "alert alert-success";
                    if (userId != 0)
                    {
                        Response.AddHeader("REFRESH", "3;URL=Profile.aspx");
                    }
                }
                catch(Exception ex)
                {
                    throw;
                }
            }

        }
    }
}