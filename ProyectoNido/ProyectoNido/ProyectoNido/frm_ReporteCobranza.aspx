<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true"
    CodeBehind="frm_ReporteCobranza.aspx.cs" Inherits="ProyectoNido.frm_ReporteCobranza" %>
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
                <a class="activo" href="frm_ReporteCobranza.aspx">Cobranzas</a>
                <a href="frm_ReporteAlumnos.aspx">Reporte de Alumnos</a>
                <a href="frm_ReporteDocentes.aspx">Reporte de docentes</a>
            </div>
        </div>
    </asp:Content>

    <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
        <div class="reporte-ingresos-container">
            <div class="filtros-panel">
                <div class="filtro-row">
                    <div class="filtro-item filtro-salon">
                        <label for="ddlSalon">Salón</label>
                        <asp:DropDownList ID="ddlSalon" runat="server" CssClass="filtro-select"></asp:DropDownList>
                    </div>

                    <div class="filtro-item filtro-distrito">
                        <label for="ddlDistrito">Distrito</label>
                        <asp:DropDownList ID="ddlDistrito" runat="server" CssClass="filtro-select"></asp:DropDownList>
                    </div>
                </div>

                <div class="filtro-row">
                    <div class="filtro-item filtro-fecha">
                        <label for="txtFechaInicio">Fecha inicio</label>
                        <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="filtro-input" TextMode="Date">
                        </asp:TextBox>
                    </div>

                    <div class="filtro-item filtro-fecha">
                        <label for="txtFechaFin">Fecha fin</label>
                        <asp:TextBox ID="txtFechaFin" runat="server" CssClass="filtro-input" TextMode="Date">
                        </asp:TextBox>
                    </div>

                    <div class="filtro-item filtro-buscar">
                        <asp:Button ID="btnBuscar" runat="server" CssClass="btn-buscar" Text="&#128269;"
                            OnClick="btnBuscar_Click" ToolTip="Buscar cobranzas" />
                    </div>
                </div>
            </div>

            <asp:Panel ID="pnlResultados" runat="server" Visible="false" CssClass="panel-resultados">
                <asp:GridView ID="gvIngresos" runat="server" AutoGenerateColumns="False" CssClass="tabla-ingresos"
                    HeaderStyle-CssClass="tabla-header" RowStyle-CssClass="tabla-row"
                    AlternatingRowStyle-CssClass="tabla-row-alt">
                    <Columns>
                        <asp:BoundField DataField="FechaPago" HeaderText="Fecha Venc."
                            DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Salon" HeaderText="Salón" />
                        <asp:BoundField DataField="Distrito" HeaderText="Distrito" />
                        <asp:BoundField DataField="Alumno" HeaderText="Alumno" />
                        <asp:BoundField DataField="Concepto" HeaderText="Concepto" />
                        <asp:BoundField DataField="EstadoDescripcion" HeaderText="Estado" />
                        <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="S/ {0:N2}"
                            ItemStyle-CssClass="text-right" />
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="mensaje-sin-datos">
                            No se encontraron cobranzas pendientes para los filtros seleccionados.
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </asp:Panel>

            <div class="total-container"
                style="text-align: right; margin-top: 20px; font-size: 1.2em; font-weight: bold;">
                <asp:Label ID="lblTotalEtiqueta" runat="server" Text="Total Pendiente: "></asp:Label>
                <asp:Label ID="lblTotal" runat="server" Text="S/ 0.00"></asp:Label>
            </div>

            <asp:Panel ID="pnlSinIngresos" runat="server" Visible="false" CssClass="mensaje-sin-datos">
                No se encontraron cobranzas pendientes para los filtros seleccionados.
            </asp:Panel>
        </div>
    </asp:Content>