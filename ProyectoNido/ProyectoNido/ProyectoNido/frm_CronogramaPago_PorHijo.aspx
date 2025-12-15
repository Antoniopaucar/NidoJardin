<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_CronogramaPago_PorHijo.aspx.cs" Inherits="ProyectoNido.frm_CronogramaPago_PorHijo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/apoderado_hijos.css" rel="stylesheet" />
    <link href="CSS/apoderado_hijos_expandible.css" rel="stylesheet" />
    <link href="CSS/cronograma_pago.css" rel="stylesheet" />
    <script src="Js/docente_datos.js" type="text/javascript"></script>
    <script src="Js/apoderado_hijos_expandible.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

                <div class="docente-left">
    <div class="docente-avatar"></div>
    <div class="docente-nombre">
        <asp:Label ID="lblNombreDocente" runat="server" Text="nombre y apellido"></asp:Label>
    </div>
    <div class="docente-menu">
        <a href="frm_Apoderado_Datos.aspx">Datos</a>
        <a href="frm_Apoderado_Comunicado.aspx">Ver Comunicado</a>  
        <a href="frm_Apoderado_OfertaGrupoServicio.aspx">Oferta Servicio</a>
    </div>

    <!-- Sección de Hijos Expandibles -->
    <div class="hijos-seccion">
        <div class="hijos-titulo">Mis Hijos</div>
        <div class="hijos-container">
            <asp:Repeater ID="rptHijos" runat="server">
                <ItemTemplate>
                    <div class="hijo-item" data-id-hijo='<%# Eval("Id") %>'>
                        <asp:Button ID="btnHijo" runat="server" 
                            Text='<%# Eval("Nombres") + " " + Eval("ApellidoPaterno") + " " + Eval("ApellidoMaterno") %>'
                            CommandArgument='<%# Eval("Id") %>'
                            OnClick="btnHijo_Click"
                            CssClass='<%# Convert.ToInt32(Eval("Id")) == (Session["IdAlumnoSeleccionado"] != null ? Convert.ToInt32(Session["IdAlumnoSeleccionado"]) : 0) ? "btn-hijo activo" : "btn-hijo" %>' />
                        <div class="hijo-opciones" style="display:none;">
                            <asp:LinkButton ID="lnkCronograma" runat="server" 
                                CssClass="hijo-opcion"
                                OnClick="lnkCronograma_Click"
                                CommandArgument='<%# Eval("Id") %>'>
                                <i class="fas fa-calendar-alt"></i> Cronograma de Pagos
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkHistorial" runat="server" 
                                CssClass="hijo-opcion"
                                OnClick="lnkHistorial_Click"
                                CommandArgument='<%# Eval("Id") %>'>
                                <i class="fas fa-history"></i> Ver Historial de Servicio
                            </asp:LinkButton>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div class="cronograma-container">
        <div class="cronograma-header">
            <h2>Cronograma de Pagos</h2>
            <asp:Label ID="lblNombreHijo" runat="server" CssClass="nombre-hijo"></asp:Label>
        </div>

        <!-- Resumen de Cuotas -->
        <div class="resumen-cuotas">
            <div class="resumen-item">
                <span class="resumen-label">Total:</span>
                <span class="resumen-valor total">
                    <asp:Label ID="lblTotal" runat="server" Text="S/ 0.00"></asp:Label>
                </span>
            </div>
            <div class="resumen-item">
                <span class="resumen-label">Pagado:</span>
                <span class="resumen-valor pagado">
                    <asp:Label ID="lblPagado" runat="server" Text="S/ 0.00"></asp:Label>
                </span>
            </div>
            <div class="resumen-item">
                <span class="resumen-label">Pendiente:</span>
                <span class="resumen-valor pendiente">
                    <asp:Label ID="lblPendiente" runat="server" Text="S/ 0.00"></asp:Label>
                </span>
            </div>
        </div>

        <!-- Mensaje cuando no hay matrícula -->
        <asp:Panel ID="pnlSinMatricula" runat="server" Visible="false" CssClass="mensaje-sin-datos">
            <p>El alumno seleccionado no tiene una matrícula activa.</p>
        </asp:Panel>

        <!-- Tabla de Cronograma -->
        <asp:Panel ID="pnlCronograma" runat="server" Visible="false">
            <div class="tabla-cronograma-wrapper">
                <table class="tabla-cronograma">
                    <thead>
                        <tr>
                            <th>N° Cuota</th>
                            <th>Nombre Cuota</th>
                            <th>Fecha Vencimiento</th>
                            <th>Monto</th>
                            <th>Descuento</th>
                            <th>Adicional</th>
                            <th>Total</th>
                            <th>Fecha Pago</th>
                            <th>Estado</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptCronograma" runat="server">
                            <ItemTemplate>
                                <tr class='<%# GetClaseFilaEstado(Eval("EstadoPago")?.ToString()) %>'>
                                    <td><%# Eval("NroCuota") %></td>
                                    <td><%# Eval("NombreCuota") %></td>
                                    <td><%# Eval("FechaVencimiento") != null ? Convert.ToDateTime(Eval("FechaVencimiento")).ToString("dd/MM/yyyy") : "-" %></td>
                                    <td class="text-right">S/ <%# Convert.ToDecimal(Eval("Monto")).ToString("N2") %></td>
                                    <td class="text-right">S/ <%# Convert.ToDecimal(Eval("Descuento")).ToString("N2") %></td>
                                    <td class="text-right">S/ <%# Convert.ToDecimal(Eval("Adicional")).ToString("N2") %></td>
                                    <td class="text-right total-fila">S/ <%# Convert.ToDecimal(Eval("TotalLinea")).ToString("N2") %></td>
                                    <td><%# Eval("FechaPago") != null ? Convert.ToDateTime(Eval("FechaPago")).ToString("dd/MM/yyyy") : "-" %></td>
                                    <td>
                                        <span class='badge-estado <%# GetClaseBadgeEstado(Eval("EstadoPago")?.ToString()) %>'>
                                            <%# GetTextoEstado(Eval("EstadoPago")?.ToString()) %>
                                        </span>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </asp:Panel>

        <!-- Mensaje cuando no hay cuotas -->
        <asp:Panel ID="pnlSinCuotas" runat="server" Visible="false" CssClass="mensaje-sin-datos">
            <p>No se encontraron cuotas registradas para esta matrícula.</p>
        </asp:Panel>
    </div>
</asp:Content>
