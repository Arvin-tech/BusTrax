<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="BusTrax.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     
    <main aria-labelledby="title">
         
        <h2 id="title">Manage Bus Companies</h2>
        <p>Search a bus company using its Company ID. To manage their records, you can update or delete them here anytime.</p>

         <section class="row">
            <div class ="col-md-3">
                <div class ="form-group">
                    <label for="txtCompanyId">Company ID:</label>
                    <asp:TextBox ID="txtCompanyId" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                </div>

                <div class="form=group">
                    <label for="txtUpdatedCompanyName">Company Name:</label>
                    <asp:TextBox ID="txtUpdatedCompanyName" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="txtUpdatedContactNum">Contact No:</label>
                    <asp:TextBox ID="txtUpdatedContactNum" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                </div>

                <div class="form-group">
                     <label for="txtUpdatedEmail">Email:</label>
                     <asp:TextBox ID="txtUpdatedEmail" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>  
                </div>

                <div class="form-group">
                     <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                </div>

            </div>

             <div class="col-md-9">
               <div class="form-group">
                   <label for="GridView1"><b>Information</b></label>
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" DataKeyNames="BusComp_ID">                            
                            <Columns>
                                <asp:BoundField HeaderText="Company ID" DataField="BusComp_ID" ReadOnly="true" />
                                <asp:TemplateField HeaderText="Company Name">
                                    <ItemTemplate>
                                        <%# Eval("BusComp_Name") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtUpdatedCompanyName" runat="server" Text='<%# Bind("BusComp_Name") %>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact No">
                                    <ItemTemplate>
                                        <%# Eval("BusComp_ContactNo") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtUpdatedContactNum" runat="server" Text='<%# Bind("BusComp_ContactNo") %>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email">
                                    <ItemTemplate>
                                        <%# Eval("Email") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtUpdatedEmail" runat="server" Text='<%# Bind("Email") %>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="true" />
                                <asp:CommandField ShowCancelButton="true" />
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete"
                                            CommandArgument='<%# Eval("BusComp_ID") %>' CssClass="btn btn-danger"
                                            OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>                        
                     </asp:GridView>  

               </div>
           </div>

             <div class="col-md-3">
                <div class="form-group">
                    <label for="txtSearchCompanyId">Search by Company ID:</label>
                    <asp:TextBox ID="txtSearchCompanyId" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="rfvSearchCompanyId" runat="server" ControlToValidate="txtSearchCompanyId" ErrorMessage="Field required!" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                </div>
             </div>

            <div class="col-md-9">
                <div class="form-group">
                    <label for="GridView2"><b>Search Results:</b></label>                    
                    <asp:GridView ID="GridView2" runat="server" CssClass="table table-striped" AutoGenerateColumns="false">
                        <EmptyDataTemplate>
                            <asp:Label ID="lblNoResults" runat="server" Text="No bus company found" CssClass="no-results-message" ForeColor="Red"></asp:Label>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField HeaderText="Company ID" DataField="BusComp_ID" ReadOnly="true" />
                            <asp:BoundField HeaderText="Company Name" DataField="BusComp_Name" />
                            <asp:BoundField HeaderText="Contact No" DataField="BusComp_ContactNo" />
                            <asp:BoundField HeaderText="Email" DataField="Email" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

         </section>                       
    </main>
</asp:Content>
