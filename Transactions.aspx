﻿<%@ Page Title="Transactions" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Transactions.aspx.cs" Inherits="BusTrax.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-15">
                <section aria-labelledby="title">
                    <h2>Subscriptions</h2>
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped" AutoGenerateColumns="true" OnRowDataBound="GridView1_RowDataBound">                     
                     </asp:GridView> 
                               
                </section>
            </div>

        </div>
    </div>


    <div class="container">
        <div class="row">
            <div class="col-md-15">
                <section aria-labelledby="title">
                    <h2>Bus Routes</h2>
                        <div class="form-group">
                            <label for="txtSearchRoute">Search by Route</label>
                            <asp:TextBox ID="txtSearchRoute" runat="server" CssClass="form-control" OnTextChanged="txtSearchRoute_TextChanged" AutoPostBack="true" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSearchRoute" runat="server" ControlToValidate="txtSearchRoute" ErrorMessage="Field required!" CssClass="text-danger"></asp:RequiredFieldValidator>
                        </div>
                        <asp:GridView ID="GridView2" runat="server" CssClass="table table-striped" AutoGenerateColumns="true" OnRowDataBound="GridView2_RowDataBound">                     
                         </asp:GridView>   
                        <asp:LinkButton ID="btnExport" runat="server" Text="Download Data" OnClick="btnExport_Click" CssClass="btn btn-primary"></asp:LinkButton>
                    <address>
                                                              
                    </address>
                </section>
            </div>
        
        </div>
    </div>

    <div class="container">
        <address>                      
            <strong>Support:</strong> <a id="emailLink" href="mailto:bustrax@gmail.com">bustrax@gmail.com</a><br />                                                            
        </address>
    </div>

</asp:Content>

