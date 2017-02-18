<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Votaciones._Default" EnableEventValidation="" %>

<%@ Register Src="~/Controls/SemanaActual.ascx" TagPrefix="uc1" TagName="SemanaActual" %>
<%@ Register Src="~/Controls/Vxtaciones.ascx" TagPrefix="uc1" TagName="Vxtaciones" %>
<%@ Register Src="~/Controls/Historico.ascx" TagPrefix="uc1" TagName="Historico" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron row">
        <div class="col-lg-3 text-center">
            <uc1:SemanaActual runat="server" ID="SemanaActual" />
        </div>
        <div class="col-lg-8">
            <h1>Seguimiento para una buena dieta</h1>
            <p class="lead">Wiiii</p>
            <p><a href="https://www.youtube.com/watch?v=Inj2Ch-oIX8" class="btn btn-primary btn-lg">Aprende a romper huevos</a></p>
        </div>
    </div>
    <%--  --%>
    <ul class="nav nav-tabs">
         <li class="active"><a data-toggle="tab" href="#pruebas">Pruebas</a></li>
        <!--<li ><a data-toggle="tab" href="#portada">Inicio</a></li>-->
        <li><a data-toggle="tab" href="#reglas">Reglas</a></li>
        <li><a data-toggle="tab" href="#votaciones">Votaciones</a></li>
        <li id="liVetaciones" runat="server" visible="false"><a data-toggle="tab" href="#vetaciones" >Vetaciones</a></li>
        <li><a data-toggle="tab" href="#resultados" runat="server">Resultados</a></li>
    </ul>
    <div class="tab-content">
        <div id="portada" class="tab-pane fade in active text-center">
            <br />
            <div class="row">
                <h2>Welcome bitches</h2>
                <div class="col-lg-6">
                    <img src="Img/ImgFront.PNG" style="border-radius: 50%" runat="server" id="imagenResultadoVotacion" width="420" height="315" />
                </div>
                <div class="col-lg-6">
                    <iframe width="420" height="315" style="border-radius: 50%" src="https://www.youtube.com/v/jPLJbSp6vKY"></iframe>
                </div>
            </div>
        </div>
        <div id="reglas" class="tab-pane fade">
            <br />
            <div class="jumbotron row">
                <div class="col-md-12 reglas">
                    <h2>Reglas</h2>
                    <ul>
                        <li>
                            <p class="alert alert-info">
                                <strong>El macho alfa se desgina semanalmente, en caso de empate de candidatos se mantendrá el de la semana anterior.
                                </strong>
                            </p>
                            <p>
                                <a class="btn btn-default" href="https://www.youtube.com/watch?v=niwLmH2cIaI">Learn more &raquo;</a>
                            </p>
                        </li>
                        <li>
                            <p class="alert alert-warning">
                                <strong>El macho alfa de la semana anterior puede Vetar 3 restaurantes.
                                </strong>
                            </p>
                            <p>
                                <a class="btn btn-default" href="https://www.youtube.com/watch?v=umDr0mPuyQc">Learn more &raquo;</a>
                            </p>
                        </li>
                        <li>
                            <p class="alert alert-info">
                                <strong>El macho omega se designa semanalmente, en caso de empate de candidatos se mantendrá el de la semana anterior.
                                </strong>
                            </p>
                            <p>
                                <a class="btn btn-default" href="https://www.youtube.com/watch?v=72yhjIRlu9k">Learn more &raquo;</a>
                            </p>
                        </li>
                        <li>
                            <p class="alert alert-info">
                                <strong>El macho omega de la semana anterior decidirá el restaurante ganador en caso de empate en la semana actual.
                                </strong>
                            </p>
                            <p>
                                <a class="btn btn-default" href="https://www.youtube.com/watch?v=ZGAcHP2y-pk">Learn more &raquo;</a>
                            </p>
                        </li>
                        <li>
                            <p class="alert alert-danger">
                                <strong>Cada semana, el macho omega, ofrecerá un desayuno en forma de sacrificio al macho alfa que le mostrará su satisfacción con una caricia en la cabeza.
                                </strong>
                            </p>
                            <p>
                                <a class="btn btn-default" href="http://ct.iscute.com/ol/ic/sw/i54/2/1/12/ic_1d06e7fece72fb355ed0857a1fd58742.jpg">Learn more &raquo;</a>
                            </p>
                        </li>
                        <li>
                            <p class="alert alert-info">
                                <strong>Se permitirán alianzas de carácter público, ejemplo: X e Y hemos acordado comprar un veto a cambio de un desayuno.
                                </strong>
                            </p>
                            <p>
                                <a class="btn btn-default" href="https://s-media-cache-ak0.pinimg.com/564x/86/5a/65/865a659c495feac5348efe4aa7b34341.jpg">Learn more &raquo;</a>
                            </p>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div id="pruebas" class="tab-pane fade">
            <uc1:Historico runat="server" ID="Historico" />
        </div>
        <div id="votaciones" class="tab-pane fade">
            <uc1:Vxtaciones runat="server" ID="ctrlVotaciones" tipo="Votacion" />
        </div>
        <div id="divVvetaciones" class="tab-pane fade" runat="server" visible="false">
            <uc1:Vxtaciones runat="server" ID="ctrlVetaciones" tipo="Vetacion" />
        </div>
        <asp:Panel ID="resultados" class="tab-pane fade" runat="server" ClientIDMode="static">
            <br />
            <div class="row alert alert-success text-center" id="contenedorBar" runat="server">
                <div class="row">
                    <h2>WINNEEEEEEEEEEEEEEEER!!!!!</h2>
                </div>
            </div>
            <div class="row text-center">
                <div id="bares" runat="server" style="background-color: transparent;" />
            </div>
            <div class="row alert alert-info text-center" runat="server" id="contenedorAlfa">
                <div class="row">
                    <h2>WINNEEEEEEEEEEEEEEEER!!!!!</h2>
                </div>
            </div>
            <div class="row text-center">
                <div id="comensales" runat="server" />
            </div>
            <div class="row alert alert-danger text-center" runat="server" id="contenedorOmega">
                <div class="row">
                    <h2>LOOSEEEEEEEEEEEEEEEEER!!!!!</h2>
                </div>
            </div>
        </asp:Panel>
    </div>
  
</asp:Content>
