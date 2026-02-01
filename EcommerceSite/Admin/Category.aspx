<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="EcommerceSite.Admin.Category" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%#lblMsg.ClientID%>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
    <script>
        function ImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imagePreview.ClientID%>').prop('src', e.target.result)
                        .width(200)
                        .height(200);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mb-4">
        <asp:Label ID="lblMsg" runat="server" ></asp:Label>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-4">
            <div class="card">
                <div class="card-body">
                    <h4 class="card-title">
                        Category
                    </h4><hr>
                    <div class="form-body">
                        <label>Category Name</label>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <asp:TextBox ID="txtCategoryName" CssClass="form-control" placeholder="Enter Category Name" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCategoryName" ForeColor="Red" Font-Size="Small" Display="Dynamic" SetFocusOnError="true" runat="server" ErrorMessage="Required" ControlToValidate="txtCategoryName"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                         <label>Category Image</label>
                        <div class="row">
                          <div class="col-sm-12">
                               <div class="form-group">
                                   <asp:FileUpload ID="fuCategoryImage" CssClass="form-control" runat="server"
                                       onchange="ImagePreview(this);"/>
                                   <asp:HiddenField ID="hfCategoryID" runat="server" Value="0" />
                                </div>
                             </div>
                           </div>
                        <div class="=row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:CheckBox ID="cbIsActive" runat="server" Text="&nbsp; IsActive" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-actiona pb-5">
                        <div class="text-left">
                            <asp:Button ID="btnAddOrUpdate" runat="server" CssClass="btn btn-info" Text="Add" OnClick="btnAddOrUpdate_Click" CausesValidation="True" />
                            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-dark" Text="Reset" OnClick="btnClear_Click" CausesValidation="True" />
                            </div>
                    </div>
                    <div>
                        <asp:Image ID="imagePreview" CssClass="img-thumbnail" runat="server" />
                    
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-12 col-md-8">
    <div class="card">
        <div class="card-body">
            <h4 class="card-title">
                Category List
            </h4><hr/>
            <div class="table-responsive">
                <asp:Repeater ID="rCategory" runat="server" onItemCommand="rCategory_ItemCommand">
                <HeaderTemplate>
                    <table class="table datatable-export table-hover nowrap">
                        <thead>
                            <tr>
                                <th class="table-plus">Name</th>
                                <th >Image</th>
                                <th >IsActive</th>
                                <th >CreateDate</th>
                                <th class="datatable-nosort">Action</th>
                            </tr>
                        </thead>
                    <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                        <td class="table-plus"> <%# Eval("CategoryName") %> </td>
                        <td> 
                            <img width="40" src="<%# EcommerceSite.clsUtils.GetImageUrl(Eval("CategoryImage")) %>" alt="image" />
                        </td>
                            <td>
                                <asp:Label ID="lblIsActive" runat="server" Text='<%#(bool)Eval("IsActive")==true?"Active":"In-Active" %>' 
                                    CssClass='<%#(bool)Eval("IsActive")==true?"badge badge-success":"badge badge-danger" %>'></asp:Label>
                            </td>
                            <td> <%# Eval("CreateDate") %></td>
                              <td>  <asp:LinkButton ID="lbEdit" runat="server" CssClass="badge badge-primary"
                                  CommandArgument='<%# Eval("CategoryID") %>' CommandName="edit" CausesValidation="False"><i class="fas fa-edit"></i></asp:LinkButton>
                                   <asp:LinkButton ID="lbDelete" runat="server" CssClass="badge badge-danger" 
                                       CommandArgument='<%# Eval("CategoryID") %>' CommandName="delete" CausesValidation="False"
                                       onClientClick="return confirm ('Do you want delete this Category?');"><i class="fas fa-trash-alt"></i></asp:LinkButton>

                            
                           

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
    </div>
</asp:Content>
