﻿using Raumverwaltung.Controllers;
using Raumverwaltung.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Raumverwaltung.View
{
    public partial class Raumverwaltung : System.Web.UI.Page
    {
        #region Eigenschaften
        private string _GateWayAddress;
        private int _UserRechte;
        private int _WorkID;
        #endregion

        #region Modifier
        public string GateWayAddress { get => _GateWayAddress; set => _GateWayAddress = value; }
        public int UserRechte { get => _UserRechte; set => _UserRechte = value; }
        public int WorkID { get => _WorkID; set => _WorkID = value; }
        #endregion

        #region Konstruktoren
        public Raumverwaltung()
        {
            GateWayAddress = null;
            UserRechte = -1;
            WorkID = -1;
        }

        public Raumverwaltung(string gateWayAddress)
        {
            GateWayAddress = gateWayAddress;
            UserRechte = -1;
            WorkID = -1;
        }

        public Raumverwaltung(string gateWayAddress, int userRechte)
        {
            GateWayAddress = gateWayAddress;
            UserRechte = userRechte;
            WorkID = -1;
        }
        #endregion

        #region Worker
        private bool GetGateway()
        {
            bool lGet = false;

            GateWayAddress = "";
            return lGet;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int UserID = 0;

            //Get UserID noch imlpementieren
            CheckUserRole(UserID);

//            Grid1_Fuellen();
            //Grid2_Fuellen();
            
        }

        private void CheckUserRole(int UserID)
        {
            switch (UserID)
            {
                case 0:
                    SetAdminAuthority();
                    break;
                case 1:
                    SetVerwalterAuthority();
                    break;
                default:
                    SetNormalAuthority();
                    break;
            }
        }

        private void SetAdminAuthority()
        {
            Grid1.AutoGenerateDeleteButton = true;
            Grid1.AutoGenerateEditButton = true;
            Grid2.AutoGenerateDeleteButton = true;
            Grid2.AutoGenerateEditButton = true;
            btnAddPersZimmer.Visible = true;
            btnAddRaum.Visible = true;
        }

        private void SetVerwalterAuthority()
        {
            Grid1.AutoGenerateEditButton = true;
            Grid2.AutoGenerateDeleteButton = true;
            Grid2.AutoGenerateEditButton = true;
            btnAddPersZimmer.Visible = true;
            btnAddRaum.Visible = true;
        }

        private void SetNormalAuthority()
        {
            Grid1.AutoGenerateDeleteButton = false;
            Grid1.AutoGenerateEditButton = false;
            Grid2.AutoGenerateDeleteButton = false;
            Grid2.AutoGenerateEditButton = false;
            btnAddPersZimmer.Visible = false;
            btnAddRaum.Visible = false;
        }

        protected void btnHome_Click(object sender, ImageClickEventArgs e)
        {
            if (GetGateway())
            {
                Response.Redirect(_GateWayAddress);
            }
            else
            {
                //throw Message: "Gateway nicht erreichbar!"
            }
        }

        #region RaumWorker
        protected void btnAddRaum_Click(object sender, EventArgs e)
        {
            DataTable DataTab = (DataTable)Session["Grid1"];
            DataRow newDR = DataTab.NewRow();
            newDR[0] = "0";
            DataTab.Rows.Add(newDR);
            Grid1.EditIndex = DataTab.Rows.IndexOf(newDR);
            BindDataGrid1();
            Grid1.Rows[DataTab.Rows.IndexOf(newDR)].Cells[1].Enabled = false;
            btnAddRaum.Enabled = false;
        }

        private void BindDataGrid1()
        {
            Grid1.DataSource = Session["Grid1"];
            Grid1.DataBind();
        }

        protected void ddlZweck_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlZweck = (DropDownList)sender;
            GridViewRow gv = (GridViewRow)ddlZweck.NamingContainer;
            int index = gv.RowIndex;
            DropDownList DropDownList1 = (DropDownList)Grid1.Rows[index].FindControl("DropDownList1");
            Global.cMainController.Raeume[index].ZweckID = Int16.Parse(DropDownList1.SelectedValue);
        }

        private void Grid1_Fuellen()
        {
            DropDownList ddlZweck = (DropDownList)Grid1.Rows[0].FindControl("ddlZweck");

            DataTable DT = new DataTable();
            //DT.Col[0]
            DataColumn Col = new DataColumn("ID");
            Col.DataType = System.Type.GetType("System.Int32");
            DT.Columns.Add(Col);
            //DT.Col[1]
            Col = new DataColumn("Raumtyp");
            Col.DataType = System.Type.GetType("System.Int32");
            DT.Columns.Add(Col);
            //DT.Col[2]
            Col = new DataColumn("Betriebsstatus");
            Col.DataType = System.Type.GetType("System.Boolean");
            DT.Columns.Add(Col);

            foreach (Raum R in Global.cMainController.Raeume)
            {
                DataRow DR = DT.NewRow();
                DR[0] = R.rID;
                DR[1] = R.ZweckID;
                DR[2] = R.AußerBetrieb;
                DT.Rows.Add(DR);
            }
            Grid1.DataSource = DT;
            Grid1.DataBind();
            Session["Grid1"] = Grid1.DataSource;
        }

        protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlZweck = (DropDownList)e.Row.FindControl("ddlZweck");
                ddlZweck.Items.Clear();
                List<RaumZweck> _List = new List<RaumZweck>();
                _List.Add(new RaumZweck(1, "Test"));
                Global.cMainController.RaumZwecks = _List;
                foreach (RaumZweck Z in Global.cMainController.RaumZwecks)
                {
                    ListItem item = new ListItem();
                    item.Text = Z.Bezeichnung;
                    item.Value = Z.ID.ToString();
                    ddlZweck.Items.Add(item);
                }
            }
        }

        protected void Grid1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int RowIndex = e.RowIndex;

            DataTable DataTab = (DataTable)Session["GridView1"];

            string IDstr = Grid1.Rows[RowIndex].Cells[1].Text.ToString();
            try
            {
                int DatabaseIndex = Int16.Parse(IDstr);
                DeletePerson(DatabaseIndex);
            }
            catch
            {
                //IDstr nicht umformbar
            }
            finally
            {
                //nichts
            }
            DataTab.Rows[RowIndex].Delete();
            BindDataGrid1();
        }

        protected void Grid1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            //Set the edit index.
            int EditIndex = e.NewEditIndex;
            Grid1.EditIndex = EditIndex;
            //Bind data to the GridView control.
            BindDataGrid1();
            Grid1.Rows[EditIndex].Cells[1].Enabled = false;
            btnAddRaum.Enabled = false;
        }

        protected void Grid1_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            DataTable DT = (DataTable)Session["GridView1"];
            int editIndex = Grid1.EditIndex;
            int RowID = editIndex;
            //Reset the edit index.
            Grid1.EditIndex = -1;
            //Bind data to the GridView control.
            DataRow dtr = DT.Rows[RowID];
            if (dtr[0].ToString() == "")
            {
                DT.Rows[RowID].Delete();
            }
            BindDataGrid1();
            btnAddRaum.Enabled = true;
        }

        protected void Grid1_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            DataTable DT = (DataTable)Session["GridView1"];
            int editIndex = Grid1.EditIndex;
            Grid1.EditIndex = -1;

            DataRow DR = DT.Rows[editIndex];
            
            Global.cMainController.Raeume[editIndex].rID = Int16.Parse(DR[0].ToString());
            Global.cMainController.Raeume[editIndex].ZweckID = Int16.Parse(DR[1].ToString());
            Global.cMainController.Raeume[editIndex].AußerBetrieb = Boolean.Parse(DR[2].ToString());
            if (!Global.cMainController.Raeume[editIndex].Added)
            {
                Global.cMainController.Raeume[editIndex].Bearbeitet = true;
            }

            BindDataGrid1();

            

            btnAddRaum.Enabled = true;
        }
        #endregion

        #region PZWorker
        protected void btnAddPersZimmer_Click(object sender, EventArgs e)
        {
            DataTable DataTab = (DataTable)Session["Grid2"];
            DataRow newDR = DataTab.NewRow();
            newDR[0] = "0";
            DataTab.Rows.Add(newDR);
            Grid2.EditIndex = DataTab.Rows.IndexOf(newDR);
            BindDataGrid1();
            Grid2.Rows[DataTab.Rows.IndexOf(newDR)].Cells[1].Enabled = false;
            btnAddPersZimmer.Enabled = false;
        }

        private void BindDataGrid2()
        {
            Grid1.DataSource = Session["Grid2"];
            Grid1.DataBind();
        }

        private void Grid2_Fuellen()
        {
            DataTable DT = new DataTable();
            //DT.Col[0]
            DataColumn Col = new DataColumn("ID");
            Col.DataType = System.Type.GetType("System.Int32");
            DT.Columns.Add(Col);
            //DT.Col[1]
            Col = new DataColumn("Betten belegt");
            Col.DataType = System.Type.GetType("System.Int32");
            DT.Columns.Add(Col);
            //DT.Col[2]
            Col = new DataColumn("Betten maximal");
            Col.DataType = System.Type.GetType("System.Int32");
            DT.Columns.Add(Col);

            foreach (Patientenzimmer P in Global.cMainController.Patientenzimmers)
            {
                DataRow DR = DT.NewRow();
                int DTID = DT.Rows.Count;
                DR[0] = P.pzID;
                DR[1] = P.BettenBelegt;
                DR[2] = P.BettenMaxAnzahl;
                DT.Rows.Add(DR);
            }
            Grid2.DataSource = DT;
            Grid2.DataBind();
            Session["Grid2"] = Grid2.DataSource;
            ButtonField BF = new ButtonField();
            BF.Text = "Bearbeiten";
            BF.ButtonType = 0;
            Grid2.Columns.Add(BF);
            //< asp:ButtonField ButtonType = "Button" Text = "Schaltfläche" />
        }

        protected void Grid2_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            //Delete DS From Grid2
            int RowIndex = e.RowIndex;

            DataTable DataTab = (DataTable)Session["GridView1"];

            string IDstr = Grid2.Rows[RowIndex].Cells[1].Text.ToString();
            try
            {
                int DatabaseIndex = Int16.Parse(IDstr);
                DeletePerson(DatabaseIndex);
            }
            catch
            {
                //IDstr nicht umformbar
            }
            finally
            {
                //nichts
            }
            DataTab.Rows[RowIndex].Delete();
            BindDataGrid2();


            btnAddPersZimmer.Enabled = true;
        }

        protected void Grid2_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {



            btnAddPersZimmer.Enabled = true;
        }

        protected void Grid2_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {



            btnAddPersZimmer.Enabled = true;
        }

        protected void Grid2_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            

            btnAddPersZimmer.Enabled = true;
        }
        #endregion

        #endregion

        protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
        {

        }

        protected void Grid3_Load1(object sender, EventArgs e)
        {
            
        }

        protected void Grid3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlZweck = (DropDownList)e.Row.FindControl("ddlZweck");
                ddlZweck.Items.Clear();   
                List<RaumZweck> _List = new List<RaumZweck>();
                _List.Add(new RaumZweck(1, "Test"));
                Global.cMainController.RaumZwecks = _List;
                foreach (RaumZweck Z in Global.cMainController.RaumZwecks)
                {
                    ListItem item = new ListItem();
                    item.Text = Z.Bezeichnung;
                    item.Value = Z.ID.ToString();
                    ddlZweck.Items.Add(item);
                }
            }
        }
    }
}