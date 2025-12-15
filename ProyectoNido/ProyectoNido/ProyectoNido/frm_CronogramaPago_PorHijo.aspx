<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_CronogramaPago_PorHijo.aspx.cs" Inherits="ProyectoNido.frm_CronogramaPago_PorHijo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="CSS/apoderado_hijos.css" rel="stylesheet" />
    <link href="CSS/apoderado_hijos_expandible.css" rel="stylesheet" />
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

    <style>
        .cronograma-container {
            padding: 20px;
            max-width: 1400px;
            margin: 0 auto;
        }

        .cronograma-header {
            margin-bottom: 25px;
        }

        .cronograma-header h2 {
            color: #333;
            margin-bottom: 10px;
            font-size: 24px;
        }

        .nombre-hijo {
            font-size: 16px;
            color: #666;
            font-weight: 500;
        }

        .resumen-cuotas {
            display: flex;
            gap: 20px;
            margin-bottom: 30px;
            padding: 20px;
            background-color: #f8f9fa;
            border-radius: 8px;
            flex-wrap: wrap;
        }

        .resumen-item {
            flex: 1;
            min-width: 150px;
            text-align: center;
        }

        .resumen-label {
            display: block;
            font-size: 14px;
            color: #666;
            margin-bottom: 5px;
            font-weight: 500;
        }

        .resumen-valor {
            display: block;
            font-size: 24px;
            font-weight: bold;
        }

        .resumen-valor.total {
            color: #333;
        }

        .resumen-valor.pagado {
            color: #28a745;
        }

        .resumen-valor.pendiente {
            color: #dc3545;
        }

        .tabla-cronograma-wrapper {
            overflow-x: auto;
            border: 1px solid #ddd;
            border-radius: 8px;
            background-color: #fff;
        }

        .tabla-cronograma {
            width: 100%;
            border-collapse: collapse;
            min-width: 1000px;
        }

        .tabla-cronograma thead {
            background-color: #007bff;
            color: white;
        }

        .tabla-cronograma th {
            padding: 12px;
            text-align: left;
            font-weight: 600;
            font-size: 14px;
        }

        .tabla-cronograma td {
            padding: 12px;
            border-bottom: 1px solid #e0e0e0;
            font-size: 14px;
        }

        .tabla-cronograma tbody tr:hover {
            background-color: #f5f5f5;
        }

        .fila-pagada {
            background-color: #d4edda;
        }

        .fila-pendiente {
            background-color: #fff;
        }

        .fila-exonerada {
            background-color: #e7f3ff;
        }

        .text-right {
            text-align: right;
        }

        .total-fila {
            font-weight: bold;
            color: #007bff;
        }

        .badge-estado {
            display: inline-block;
            padding: 5px 12px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            text-transform: uppercase;
        }

        .badge-pagado {
            background-color: #28a745;
            color: white;
        }

        .badge-pendiente {
            background-color: #ffc107;
            color: #333;
        }

        .badge-exonerado {
            background-color: #17a2b8;
            color: white;
        }

        .mensaje-sin-datos {
            text-align: center;
            padding: 40px;
            background-color: #f8f9fa;
            border-radius: 8px;
            color: #666;
            font-size: 16px;
        }

        @media (max-width: 768px) {
            .resumen-cuotas {
                flex-direction: column;
            }

            .resumen-item {
                min-width: 100%;
            }
        }
    </style>






</asp:Content>
