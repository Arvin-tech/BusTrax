using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BusTrax
{
    public partial class About : Page
    {
        string connectionString = "mongodb://localhost:27017";
        string databaseName = "bustrax";
        string collectionName = "buscompanies";

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None; //for field validators

            if (!IsPostBack)
            {
                BindGridView(); //auto display gridview 1
            }

            GridView1.RowUpdating += GridView1_RowUpdating;
            GridView1.RowDeleting += GridView1_RowDeleting;
            GridView1.RowEditing += GridView1_RowEditing;
            GridView1.RowCancelingEdit += GridView1_RowCancelingEdit;
        }
      
        //update button listener
        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            // Retrieve the updated values from the textboxes
            string updatedCompanyName = txtUpdatedCompanyName.Text; //exception
            string updatedContactNo = txtUpdatedContactNum.Text;
            string updatedEmail = txtUpdatedEmail.Text;

            // Update the MongoDB record with the new values
            var connStr = connectionString;
            var client = new MongoClient(connStr);
            var database = client.GetDatabase(databaseName);
            var collection = database.GetCollection<BusCompanies>(collectionName);

        }
        
        //data grid view update button listener
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string companyId = GridView1.DataKeys[e.RowIndex].Value.ToString();
            
            //text box objects assigned to gridview textboxes
            TextBox txtCompanyName = (TextBox)row.FindControl("txtUpdatedCompanyName"); 
            TextBox txtContactNo = (TextBox)row.FindControl("txtUpdatedContactNum");
            TextBox txtEmail = (TextBox)row.FindControl("txtUpdatedEmail");
      
            try
            {
                // Validate textboxes to avoid whitespace input to MongoDB
                if (txtCompanyName == null || txtContactNo == null || txtEmail == null ||
                    string.IsNullOrWhiteSpace(txtCompanyName.Text) ||
                    string.IsNullOrWhiteSpace(txtContactNo.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    Response.Write("<script>alert('Please fill up required fields!');</script>");
                    return;
                }

                // Retrieve the updated values from the textboxes
                string updatedCompanyName = txtCompanyName.Text; //exception
                string updatedContactNo = txtContactNo.Text;
                string updatedEmail = txtEmail.Text;

                // Update the MongoDB record with the new values
                var connStr = connectionString;
                var client = new MongoClient(connStr);
                var database = client.GetDatabase(databaseName);
                var collection = database.GetCollection<BusCompanies>(collectionName);

                //update record according to bus comp id using BusCompanies class
                var filter = Builders<BusCompanies>.Filter.Eq("BusComp_ID", companyId); //use filter based on the bus comp id
                var update = Builders<BusCompanies>.Update
                    .Set(c => c.BusComp_Name, updatedCompanyName)
                    .Set(c => c.BusComp_ContactNo, updatedContactNo)
                    .Set(c => c.Email, updatedEmail);

                collection.UpdateOne(filter, update); //filter bus comp then update record

                GridView1.EditIndex = -1; // Exit edit mode
                BindGridView(); // Rebind the GridView to reflect the updated data
            }
            catch (NullReferenceException ex)
            {
               Console.WriteLine(ex.Message); //log
            }

        }

        // Perform the delete operation
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string companyId = GridView1.DataKeys[e.RowIndex].Value.ToString();

            // Delete the record from the database
            var connStr = connectionString;
            var client = new MongoClient(connStr);
            var database = client.GetDatabase(databaseName);
            var collection = database.GetCollection<BusCompanies>(collectionName);

            var filter = Builders<BusCompanies>.Filter.Eq("BusComp_ID", companyId); // Use filter based on the bus comp id
            collection.DeleteOne(filter); // Delete the record matching the filter

            BindGridView(); // Rebind the GridView to reflect the updated data
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            BindGridView();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            BindGridView();
        }

        // Populate the GridView with data
        private void BindGridView()
        {
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(databaseName);
            IMongoCollection<BusCompanies> collection = database.GetCollection<BusCompanies>(collectionName);

            //read data from mongo
            List<BusCompanies> list = collection.AsQueryable().ToList();
            GridView1.DataSource = list;
            GridView1.DataBind();
      
            if (GridView1.Rows.Count > 0)
            {
                GridViewRow row = GridView1.Rows[0];
                txtCompanyId.Text = row.Cells[0].Text;
                txtUpdatedCompanyName.Text = row.Cells[1].Text;
                txtUpdatedContactNum.Text = row.Cells[2].Text;
                txtUpdatedEmail.Text = row.Cells[3].Text;

            }           
            ClearTextBoxes();
        }

        private void BindGridView2()
        {
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(databaseName);
            IMongoCollection<BusCompanies> collection = database.GetCollection<BusCompanies>(collectionName);

            //read data from mongo
            List<BusCompanies> list = collection.AsQueryable().ToList();
            GridView1.DataSource = list;
            GridView1.DataBind();

        }

        private void ClearTextBoxes()
        {
            txtCompanyId.Text = string.Empty;
            txtUpdatedCompanyName.Text = string.Empty;
            txtUpdatedContactNum.Text = string.Empty;
            txtUpdatedEmail.Text = string.Empty;
        }

        //perform search operation
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //assign the inputted value to companyId variable
            string companyId = txtSearchCompanyId.Text.Trim();

            //establish connection first to mongo db
            var connStr = connectionString;
            var client = new MongoClient(connStr);
            var database = client.GetDatabase(databaseName);
            var collection = database.GetCollection<BusCompanies>(collectionName);

            //filter by id
            var filter = Builders<BusCompanies>.Filter.Eq("BusComp_ID", companyId); //search by bus company id
            var searchResults = collection.Find(filter).ToList();
            if(searchResults.Count > 0)
            {
                //found
                GridView2.EmptyDataText = string.Empty;
                GridView2.DataSource = searchResults;
                GridView2.DataBind();
                GridView2.Visible = true;         
            }
            else
            {
                GridView2.EmptyDataText = "No bus company found.";
                GridView2.DataSource = new List<BusCompanies>(); // Empty collection
                GridView2.DataBind();         
            }           
        }
    }
}