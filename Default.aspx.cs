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
        public static IMongoCollection<BusCompanies> buscompaniesCollection = database.GetCollection<BusCompanies>("buscompanies");
        public static IMongoCollection<BusInformation> businfoCollection = database.GetCollection<BusInformation>("businfo");

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
            // Generate a random company ID
            Random random = new Random();

            //get the value inputted @ txtboxes   
            string companyId = random.Next(1000, 9999).ToString();
            string busNum = txtBusNumber.Text.Trim();

            // Check if the bus company already exists
            var filter = Builders<BusCompanies>.Filter.Eq("BusComp_ID", companyId);
            var existingCompany = buscompaniesCollection.Find(filter).FirstOrDefault();
            if (existingCompany != null)
            {
                //already exist
                Response.Write("<script>alert('Bus company already exists!');</script>");
                ClearTextBoxes();
                return;
            }

            //check if bus number already exists in bus routes collection
            var businfoFilter = Builders<BusInformation>.Filter.Eq("Bus_ID", busNum);
            var existingBusNum = businfoCollection.Find(businfoFilter).FirstOrDefault();
            if(existingBusNum != null)
            {
                //already exist
                Response.Write("<script>alert('Bus already existed!'); </script>");
                ClearTextBoxes();
                return;
            }


            //insert value from txtbox to mongo collections buscompanies and businfo
            BusCompanies buscomp = new BusCompanies(companyId, txtCompanyName.Text, txtCompanyContact.Text, txtEmail.Text);
            buscompaniesCollection.InsertOne(buscomp);
            BusInformation businfo = new BusInformation(txtBusNumber.Text, companyId, txtBusDriver.Text, txtBusConductor.Text, txtBusRoute.Text, txtPlateNumber.Text);
            businfoCollection.InsertOne(businfo);

            Response.Write("<script>alert('Record successfully saved!');</script>");
            BindGridView(); //refresh grid
        }

        //display data from mongo to grid view
        private void BindGridView()
        {
            //read data from mongo
            List<BusCompanies> list = buscompaniesCollection.AsQueryable().ToList();
            List<BusInformation> list2 = businfoCollection.AsQueryable().ToList();

            // Combine the two lists into a single list to output in one gridview
            List<object> combinedList = new List<object>();
            combinedList.AddRange(list);
            combinedList.AddRange(list2);

            try
            {
                // code causing TargetInvocationException
                GridView1.DataSource = combinedList;
                GridView1.DataBind();
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    string err = e.InnerException.Message;
                }
            }
            
            /*
            if (GridView1.Rows.Count > 0)
            {
                GridViewRow row = GridView1.Rows[0];

                if(row.DataItem is BusCompanies)
                {
                    // Handle row with BusCompanies object
                    BusCompanies busCompanies = (BusCompanies)row.DataItem;
                    txtCompanyId.Text = busCompanies.BusComp_ID.ToString();
                    txtCompanyName.Text = busCompanies.BusComp_Name;
                    txtCompanyContact.Text = busCompanies.BusComp_ContactNo;
                    txtEmail.Text = busCompanies.Email;
                }
                else if (row.DataItem is BusInformation)
                {
                    // Handle row with BusInformation object
                    BusInformation busInformation = (BusInformation)row.DataItem;
                    txtBusNumber.Text = busInformation.Bus_ID;
                    txtCompanyId.Text = busInformation.BusComp_ID.ToString(); // Set the company ID                           
                    txtBusDriver.Text = busInformation.Bus_Driver;
                    txtBusConductor.Text = busInformation.Bus_Conductor;
                    txtBusRoute.Text = busInformation.Bus_Route;
                    txtPlateNumber.Text = busInformation.PlateNumber;
                }
              
            }*/

            ClearTextBoxes(); // Clear the text boxes
        }

        //for grid view headers
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (e.Row.Cells.Count >= 9) // Check if the row has at least 9 cells
                {
                    e.Row.Cells[0].Text = "Company ID";
                    e.Row.Cells[1].Text = "Company Name";
                    e.Row.Cells[2].Text = "Contact No";
                    e.Row.Cells[3].Text = "Email";
                    e.Row.Cells[4].Text = "Bus Number"; // Assuming this is the 4th cell
                    e.Row.Cells[5].Text = "Route";
                    e.Row.Cells[6].Text = "Driver";
                    e.Row.Cells[7].Text = "Conductor";
                    e.Row.Cells[8].Text = "Plate Number";
                }
                else
                {
                    // Handle the case when the row doesn't have enough cells
                }
            }
        }

        //clear text box inputs
        private void ClearTextBoxes()
        {
            //company
            txtCompanyName.Text = string.Empty;
            txtCompanyContact.Text = string.Empty;
            txtEmail.Text = string.Empty;

            //bus info txtboxes
            txtBusNumber.Text = string.Empty;
            txtBusRoute.Text = string.Empty;
            txtBusDriver.Text = string.Empty;
            txtBusConductor.Text = string.Empty;
            txtPlateNumber.Text = string.Empty;

        }

    }
}