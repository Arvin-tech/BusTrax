using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BusTrax
{
    public partial class _Default : Page
    {
        public static MongoClient client = new MongoClient("mongodb://localhost:27017");
        public static IMongoDatabase database = client.GetDatabase("bustrax");
        public static IMongoCollection<BusCompanies> collection = database.GetCollection<BusCompanies>("buscompanies");

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None; //for field validators
            if (!IsPostBack)
            {
                BindGridView(); //load
            }
        }

        protected void btnSave_Click1(object sender, EventArgs e)
        {
            string companyId = txtCompanyId.Text.Trim();

            // Check if the bus company already exists
            var filter = Builders<BusCompanies>.Filter.Eq("BusComp_ID", companyId);
            var existingCompany = collection.Find(filter).FirstOrDefault();
            if (existingCompany != null)
            {
                //error
                Response.Write("<script>alert('Bus company already exists!');</script>");
                ClearTextBoxes();
                return;
            }

            //insert value from txtbox to mongo collection
            BusCompanies buscomp = new BusCompanies(txtCompanyId.Text, txtCompanyName.Text, txtCompanyContact.Text, txtEmail.Text);
            collection.InsertOne(buscomp);
            Response.Write("<script>alert('Record successfully saved!');</script>");
            BindGridView(); //refresh grid
        }

        //display data from mongo to grid view
        private void BindGridView()
        {
            //read data from mongo
            List<BusCompanies> list = collection.AsQueryable().ToList();
            //List<BusCompanies> list = collection.Find(_ => true).ToList();
            GridView1.DataSource = list;
            GridView1.DataBind();

            if (GridView1.Rows.Count > 0)
            {
                GridViewRow row = GridView1.Rows[0];
                txtCompanyId.Text = row.Cells[0].Text;
                txtCompanyName.Text = row.Cells[1].Text;
                txtCompanyContact.Text = row.Cells[2].Text;
                txtEmail.Text = row.Cells[3].Text;        
                    
            }
            ClearTextBoxes(); // Clear the text boxes
        }

        //for grid view headers
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Company ID";
                e.Row.Cells[1].Text = "Company Name";
                e.Row.Cells[2].Text = "Contact No";
                e.Row.Cells[3].Text = "Email";         
            }
        }

        //clear text box inputs
        private void ClearTextBoxes()
        {
            txtCompanyId.Text = string.Empty;
            txtCompanyName.Text = string.Empty;
            txtCompanyContact.Text = string.Empty;
            txtEmail.Text = string.Empty;

        }

    }
}