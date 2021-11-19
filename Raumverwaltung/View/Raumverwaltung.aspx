<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Raumverwaltung.aspx.cs" Inherits="Raumverwaltung.View.Raumverwaltung" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            &nbsp;
            <asp:ImageButton 
                ID="Home" 
                runat="server" 
                OnClick="btnHome_Click" 
                ImageUrl="E:\Berufsschule\AWE_MEI\003 Gruppenprojekt - Krankenhausverwaltung\Raumverwaltung\Raumverwaltung\Images\home.png" 
                Height="25px"
                ToolTip = "zurück" 
                HorizontalContentAlignment = "Left"
            />
            &nbsp;
            <asp:LoginName ID="LoginName1" runat="server" />
            &nbsp;<asp:LoginStatus ID="LoginStatus1" runat="server" />
        </div>
    </form>
</body>
</html>
