<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BusTrax._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">Register Bus Companies</h1>
            <p>Register bus companies in this webpage.</p>
        </section>

       <section class="row">
        <div class="col-md-4">
            <div class="form-group">     
                <label for="txtCompanyId">Company ID:</label>
                <asp:TextBox ID="txtCompanyId" runat="server" CssClass="form-control" AutoCompleteType="Disabled" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCompanyId" runat="server" ControlToValidate="txtCompanyId" ErrorMessage="Company ID is required" CssClass="text-danger"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group">
                <label for="txtCompanyName">Company Name:</label>
                <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" AutoCompleteType="Disabled" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ControlToValidate="txtCompanyName" ErrorMessage="Company Name is required" CssClass="text-danger"></asp:RequiredFieldValidator>

            </div>

            <div class="form-group">
                <label for="txtContactNo">Contact No:</label>
                <asp:TextBox ID="txtCompanyContact" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCompanyContact" runat="server" ControlToValidate="txtCompanyContact" ErrorMessage="Contact No is required" CssClass="text-danger"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group">
                <label for="txtEmail">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required" CssClass="text-danger"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$" ErrorMessage="Invalid Email" CssClass="text-danger"></asp:RegularExpressionValidator>
            </div>
             <div class="form-group">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click1"  />
            </div>
        </div>

           <div class="col-md-6">
               <div class="form-group">
                   <label for="GridView1">Information:</label>
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped" AutoGenerateColumns="true" OnRowDataBound="GridView1_RowDataBound">                     
                     </asp:GridView>  
               </div>
           </div>
    </section>
    </main>


</asp:Content>
