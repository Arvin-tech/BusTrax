<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BusTrax._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">Bus Company Registration</h1>
            <h5>Company Information</h5>
        </section>

       <section class="row">
        <div class="col-md-2">         

            <div class="form-group">
                <label for="txtCompanyName">Company Name</label>
                <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" AutoCompleteType="Disabled" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ControlToValidate="txtCompanyName" ErrorMessage="Company Name is required" CssClass="text-danger"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group">
                <label for="txtContactNo">Contact No</label>
                <asp:TextBox ID="txtCompanyContact" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCompanyContact" runat="server" ControlToValidate="txtCompanyContact" ErrorMessage="Contact No is required" CssClass="text-danger"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group">
                <label for="txtEmail">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required" CssClass="text-danger"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$" ErrorMessage="Invalid Email" CssClass="text-danger"></asp:RegularExpressionValidator>
            </div>
          
            <section class="row">
            <h5>Bus Information</h5>
            </section>

            <div class="form-group">
                    <label for="txtBusNumber">Bus Number</label>
                    <asp:TextBox ID="txtBusNumber" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvBusNumber" runat="server" ControlToValidate="txtBusNumber" ErrorMessage="Bus Number is required!" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>

                
                <div class="form-group">
                    <label for="txtBusRoute">Route</label>
                    <asp:TextBox ID="txtBusRoute" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvBusRoute" runat="server" ControlToValidate="txtBusRoute" ErrorMessage="Bus Route is required!" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
                
                <div class="form-group">
                    <label for="txtBusDriver">Driver</label>
                    <asp:TextBox ID="txtBusDriver" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvBusDriver" runat="server" ControlToValidate="txtBusDriver" ErrorMessage="Bus Driver is required!" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>
                
                <div class="form-group">
                    <label for="txtBusConductor">Conductor</label>
                    <asp:TextBox ID="txtBusConductor" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvBusConductor" runat="server" ControlToValidate="txtBusConductor" ErrorMessage="Bus Conductor is required!" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>

                <div class="form-group">
                    <label for="txtPlateNumber">Plate Number</label>
                    <asp:TextBox ID="txtPlateNumber" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPlateNumber" runat="server" ControlToValidate="txtPlateNumber" ErrorMessage="Plate number required!" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>

                <div class="form-group">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click1"  />
                </div>  
            </div>

           <div class="col-md-10">
               <div class="form-group">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped" AutoGenerateColumns="true" OnRowDataBound="GridView1_RowDataBound">                               
                     </asp:GridView>  
               </div>
           </div>
    </section>
    </main>


</asp:Content>