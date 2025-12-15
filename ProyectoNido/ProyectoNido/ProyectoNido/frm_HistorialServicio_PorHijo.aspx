<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_HistorialServicio_PorHijo.aspx.cs" Inherits="ProyectoNido.frm_HistorialServicio_PorHijo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/apoderado_hijos.css" rel="stylesheet" />
    <link href="CSS/apoderado_hijos_expandible.css" rel="stylesheet" />
    <link href="CSS/historial_servicio.css" rel="stylesheet" />
    <script src="Js/docente_datos.js" type="text/javascript"></script>
    <script src="Js/apoderado_hijos_expandible.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="docente-left">
    <div class="docente-avatar"></div>
    <div class="docente-nombre">
        <asp:Label ID="lblNombreApoderado" runat="server" Text="nombre y apellido"></asp:Label>
    </div>
    <div class="docente-menu">
        <a href="frm_Apoderado_Datos.aspx">Datos</a>
        <a href="frm_Apoderado_Comunicado.aspx">Ver Comunicado</a>  
        <a class="activo" href="frm_Apoderado_OfertaGrupoServicio.aspx">Oferta Servicio</a>
    </div>

    <!-- Sección de Hijos -->
    <div class="hijos-seccion">
        <div class="hijos-titulo">Mis Hijos</div>
        <div class="hijos-container">
            <asp:Repeater ID="rptHijos" runat="server">
                <ItemTemplate>
                    <asp:Button ID="btnHijo" runat="server" 
                        Text='<%# Eval("Nombres") + " " + Eval("ApellidoPaterno") + " " + Eval("ApellidoMaterno") %>'
                        CommandArgument='<%# Eval("Id") %>'
                        OnClick="btnHijo_Click"
                        CssClass='<%# Convert.ToInt32(Eval("Id")) == (Session["IdAlumnoSeleccionado"] != null ? Convert.ToInt32(Session["IdAlumnoSeleccionado"]) : 0) ? "btn-hijo activo" : "btn-hijo" %>' />
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div class="historial-servicio-container">
        <div class="historial-header">
            <h2>Historial de Servicios Adicionales</h2>
            <asp:Label ID="lblNombreHijo" runat="server" CssClass="nombre-hijo"></asp:Label>
        </div>

        <!-- Mensaje cuando no hay servicios -->
        <asp:Panel ID="pnlSinServicios" runat="server" Visible="false" CssClass="mensaje-sin-datos">
            <p>El alumno seleccionado no tiene servicios adicionales asignados.</p>
        </asp:Panel>

        <!-- Tabla de Servicios -->
        <asp:Panel ID="pnlServicios" runat="server" Visible="false">
            <div class="tabla-servicios-wrapper">
                <table class="tabla-servicios">
                    <thead>
                        <tr>
                            <th>Servicio</th>
                            <th>Fecha Inicio</th>
                            <th>Fecha Final</th>
                            <th>Hora Inicio</th>
                            <th>Hora Final</th>
                            <th>Fecha Pago</th>
                            <th>Monto</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptServicios" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("NombreGrupo") %></td>
                                    <td><%# Eval("FechaInicio") != null ? Convert.ToDateTime(Eval("FechaInicio")).ToString("dd/MM/yyyy") : "-" %></td>
                                    <td><%# Eval("FechaFinal") != null ? Convert.ToDateTime(Eval("FechaFinal")).ToString("dd/MM/yyyy") : "-" %></td>
                                    <td><%# Eval("HoraInicio") != null ? ((TimeSpan)Eval("HoraInicio")).ToString(@"hh\:mm") : "-" %></td>
                                    <td><%# Eval("HoraFinal") != null ? ((TimeSpan)Eval("HoraFinal")).ToString(@"hh\:mm") : "-" %></td>
                                    <td><%# Eval("FechaPago") != null ? Convert.ToDateTime(Eval("FechaPago")).ToString("dd/MM/yyyy") : "-" %></td>
                                    <td class="text-right monto-fila">
                                        <%# Eval("Monto") != null ? $"S/ {Convert.ToDecimal(Eval("Monto")):N2}" : "-" %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
