<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="EcommerceSite.Admin.ProductList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script>
      window.onload = function () {
          var seconds = 5;
          setTimeout(function () {
              document.getElementById("<%#lblMsg.ClientID%>").style.display = "none";
          }, seconds * 1000);
      };
      </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mb-4">
    <asp:Label ID="lblMsg" runat="server" ></asp:Label>
</div>
            <div class="col-sm-12 col-md-12">
    <div class="card">
        <div class="card-body">
            <h4 class="card-title">
                Product List
            </h4><hr/>
            <div class="table-responsive">
                <asp:Repeater ID="rProductList" runat="server" OnItemCommand="rProductList_ItemCommand">
                <HeaderTemplate>
                    <table class="table datatable-export table-hover nowrap">
                        <thead>
                            <tr>
                                <th class="table-plus">Name</th>
                                <th >Image</th>
                                <th >Price</th>
                                <th >Qty</th>
                                <th >Sold</th>
                                <th >Category</th>
                                <th >SCategory</th>
                                <th >IsActive</th>
                                <th >CreateDate</th>
                                <th class="datatable-nosort">Action</th>
                            </tr>
                        </thead>
                    <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                        <td class="table-plus"> <%# Eval("ShortDescription") %> </td>
                        <td> 
                            <img width="40" src="<%# EcommerceSite.clsUtils.GetImageUrl(Eval("ImageURL")) %>" alt="image" />
                        </td>
                            <td> <%# Eval("Price") %></td>
                            <td>
                               <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                            </td>
                            <td> <%# Eval("Sold") %></td>
                            <td> <%# Eval("CategoryName") %></td>
                            <td> <%# Eval("SubCategoryName") %></td>
                            <td>
                                <asp:Label ID="lblIsActive" runat="server" Text='<%#(bool)Eval("IsActive")==true?"Active":"In-Active" %>' 
                                    CssClass='<%#(bool)Eval("IsActive")==true?"badge badge-success":"badge badge-danger" %>'></asp:Label>
                            </td>
                            <td> <%# Eval("CreateDate") %></td>
                              <td>  <asp:LinkButton ID="lbEdit" runat="server" CssClass="badge badge-primary"
                                  CommandArgument='<%# Eval("ProductID") %>' CommandName="edit" CausesValidation="False"><i class="fas fa-edit"></i></asp:LinkButton>
                                   <asp:LinkButton ID="lbDelete" runat="server" CssClass="badge badge-danger" 
                                       CommandArgument='<%# Eval("ProductID") %>' CommandName="delete" CausesValidation="False"
                                       onClientClick="return confirm ('Do you want delete this product?');"><i class="fas fa-trash-alt"></i></asp:LinkButton>

                            
                           

                            </td>
                         </tr>
                    </ItemTemplate>
                   <FooterTemplate>
                       </tbody>
                    </table>
                   </FooterTemplate>
                    </asp:Repeater>
            </div>
        </div>
    </div>
</div>
</asp:Content>
