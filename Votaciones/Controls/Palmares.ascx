<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Palmares.ascx.cs" Inherits="Votaciones.Controls.Palmares" %>
<asp:Panel ID="pnlPalmaresMultiple" runat="server" CssClass="col-xs-4" style="display:inline-block;float:none">
        <div id="contenedor" class="row alert" runat="server">
            <h4 class="text-center"><asp:Label ID="lblGanadorOPerdedor" runat="server" ></asp:Label></h4>
            <div class="row text-center">
                <img id="imgResGanador" runat="server" height="150" width="150" style="border-radius:25%"/>
            </div>
            <h1 class="text-center ">
                <asp:Label ID="lblGanador" runat="server" class="jumbo" /></h1>
        </div>
</asp:Panel>     