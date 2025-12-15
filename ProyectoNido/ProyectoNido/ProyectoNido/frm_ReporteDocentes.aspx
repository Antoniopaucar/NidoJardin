<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true"
    CodeBehind="frm_ReporteDocentes.aspx.cs" Inherits="ProyectoNido.frm_ReporteDocentes" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

        <link href="CSS/reporte_ingresos.css" rel="stylesheet" />
        <script src="Js/docente_datos.js" type="text/javascript"></script>
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
        <div class="docente-left">
            <div class="docente-avatar"></div>
            <div class="docente-nombre">
                <asp:Label ID="lblNombreDocente" runat="server" Text="nombre y apellido"></asp:Label>
            </div>
            <div class="docente-menu">
                <a href="frm_ReporteIngresos.aspx">Ingresos</a>
                <a href="frm_ReporteCobranza.aspx">Cobranzas</a>
                <a href="frm_ReporteAlumnos.aspx">Reporte de Alumnos</a>
                <a class="activo" href="frm_ReporteDocentes.aspx">Reporte de docentes</a>
            </div>
        </div>
    </asp:Content>
    <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
        <div class="reporte-ingresos-container">
            <h2 style="text-align:center; color:#2c3e50; margin-bottom:20px;">Reporte de Docentes Activos</h2>

            <asp:Panel ID="pnlResultados" runat="server" CssClass="panel-resultados">
                <asp:GridView ID="gvDocentes" runat="server" AutoGenerateColumns="False" CssClass="tabla-ingresos"
                    HeaderStyle-CssClass="tabla-header" RowStyle-CssClass="tabla-row"
                    AlternatingRowStyle-CssClass="tabla-row-alt">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" />
                        <asp:BoundField DataField="Documento" HeaderText="DNI" />
                        <asp:BoundField DataField="NombreCompleto" HeaderText="Nombres y Apellidos" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="mensaje-sin-datos">
                            No se encontraron docentes activos.
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </asp:Panel>
            <div class="total-container" style="text-align:right; margin-top:20px; font-size:1.2em; font-weight:bold;">
                <asp:Label ID="lblTotalEtiqueta" runat="server" Text="Total Docentes Activos: "></asp:Label>
                <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>
            </div>
        </div>
    </asp:Content>