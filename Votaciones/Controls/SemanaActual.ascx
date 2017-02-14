<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SemanaActual.ascx.cs" Inherits="Votaciones.Controls.SemanaActual" %>
<div id="semanaDiv" class="row">

    <time datetime="2014-09-20" class="icon"> 
        <asp:Image runat="server" ID="imagenCerrao" ImageUrl="/img/closed.png" CssClass="containerSemanaClosed"/>                       
            <strong>Semana</strong><br />
            <asp:Label ID="lblIni" runat="server" CssClass="semanaRange"></asp:Label>
            -
            <asp:Label ID="lblFin" runat="server" CssClass="semanaRange"></asp:Label>
            <asp:Label ID="lblsemana" runat="server" CssClass="semanaNumber"></asp:Label>
        
    </time>

</div>
