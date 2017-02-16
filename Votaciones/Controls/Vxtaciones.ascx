<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Vxtaciones.ascx.cs" Inherits="Votaciones.Controls.Vxtaciones" %>
<br />
<asp:UpdatePanel ID="upVotacion" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="row">
            <asp:Repeater ID="gvVotaciones" runat="server" OnItemDataBound="gvVotaciones_ItemDataBound">
                <ItemTemplate>
                    <div class="col-lg-3 col-md-3">
                        <asp:Panel CssClass="thumbnail" Style="margin-top: 10px; cursor: pointer;" ID="divItem" runat="server">
                            <asp:Image runat="server" ImageUrl='<%# Eval("Descripcion") %>' CssClass="group list-group-image" Style="height: 200px; width: 200px; border-radius: 35%" />
                            <asp:HiddenField runat="server" ID="hidNombre" Value='<%# Eval("idVotacion")%>' />
                            <div class="caption">
                                <h4 class="text-center">
                                    <asp:Label
                                        runat="server"
                                        Text='<%# Eval("Nombre") %>'>
                                    </asp:Label>
                                </h4>
                                <asp:CheckBox
                                    OnCheckedChanged="CheckBox1_CheckedChanged"
                                    ID="chkVoto"
                                    runat="server"
                                    AutoPostBack="true"
                                    itemindex='<%# ((RepeaterItem)Container).ItemIndex %>'
                                    Style="display: none;" />
                            </div>
                        </asp:Panel>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <br />
        <div class="text-center row">
            <asp:Button ID="btnEnviarVotacion" runat="server" Text="VOTAR!!!" CssClass="btn-success btn-lg" OnClick="btnEnviarVotacion_Click" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="upVotacion" DisplayAfter="1">
    <ProgressTemplate>
        <div class="transparent">            
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/img/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="border-radius: 50%; position: fixed; bottom: 0; left: 0;" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
