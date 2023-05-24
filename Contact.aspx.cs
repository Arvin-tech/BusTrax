using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BusTrax
{
    public partial class Contact : Page
    {

        string busRoutesCollection = "busroutes";
        public static MongoClient client = new MongoClient("mongodb://localhost:27017");
        public static IMongoDatabase database = client.GetDatabase("bustrax");
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView(); //load
                BindGridView2();
            }
        }
        //subscriptions grid view
        private void BindGridView()
        {
            IMongoCollection<Subscriptions> collection = database.GetCollection<Subscriptions>("subscriptions");
            List<Subscriptions> list = collection.AsQueryable().ToList();
            GridView1.DataSource = list;
            GridView1.DataBind();

        }

        //bus routes grid view
        private void BindGridView2()
        {
            IMongoCollection<BusRoutes> collection = database.GetCollection<BusRoutes>("busroutes");
            List<BusRoutes> list = collection.AsQueryable().ToList();
            GridView2.DataSource = list;
            GridView2.DataBind();
        }
        
        //for headers
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Bus Company";
                e.Row.Cells[1].Text = "Date";
                e.Row.Cells[2].Text = "Status";
                e.Row.Cells[3].Text = "Type";
                e.Row.Cells[4].Text = "Payment";
            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Route ID";
                e.Row.Cells[1].Text = "Route";
                e.Row.Cells[2].Text = "Origin";
                e.Row.Cells[3].Text = "Destination";
                e.Row.Cells[4].Text = "Fare";
                e.Row.Cells[5].Text = "Estimated Time Arrival";
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            // Create a StringBuilder to hold the CSV data
            StringBuilder sb = new StringBuilder();

            // Add column headers
            foreach (TableCell headerCell in GridView2.HeaderRow.Cells)
            {
                sb.Append(headerCell.Text + ",");
            }
            sb.Append("\r\n");

            // Add data rows
            foreach (GridViewRow row in GridView2.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    sb.Append(cell.Text + ",");
                }
                sb.Append("\r\n");
            }

            // Set response headers to force download the file
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=BusRoutes.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";

            // Write the CSV data to the response
            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }


    }
}