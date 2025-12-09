<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_MatriculaDetalle.aspx.cs" Inherits="ProyectoNido.frm_MatriculaDetalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table id="tablaMatDetalle" class="tablaForm">
        <tr class="titulo">
            <td colspan="4">
                <asp:Label ID="lblTitulo" runat="server" Text="Detalle de Matrícula (Cuotas)"></asp:Label>
            </td>
        </tr>

        <tr>
            <td>Id Matrícula:</td>
            <td>
                <asp:TextBox ID="txt_IdMatricula" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>Id Detalle:</td>
            <td>
                <asp:TextBox ID="txt_IdMatriculaDetalle" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>Nro Cuota:</td>
            <td>
                <asp:TextBox ID="txt_NroCuota" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>Nombre Cuota:</td>
            <td>
                <asp:TextBox ID="txt_NombreCuota" runat="server" CssClass="full-width-textbox" Enabled="false"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>Fecha Venc.:</td>
            <td>
                <asp:TextBox ID="txt_FechaVencimiento" runat="server" TextMode="Date" Enabled="false"></asp:TextBox>
            </td>
            <td>Cantidad:</td>
            <td>
                <asp:TextBox ID="txt_Cantidad" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>Monto:</td>
            <td>
                <asp:TextBox ID="txt_Monto" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>Descuento:</td>
            <td>
                <asp:TextBox ID="txt_Descuento" runat="server"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>Adicional:</td>
            <td>
                <asp:TextBox ID="txt_Adicional" runat="server"></asp:TextBox>
            </td>
            <td>Fecha Pago:</td>
            <td>
                <asp:TextBox ID="txt_FechaPago" runat="server" TextMode="Date"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>Estado Pago:</td>
            <td>
                <asp:DropDownList ID="ddl_EstadoPago" runat="server">
                    <asp:ListItem Text="Pendiente" Value="P" />
                    <asp:ListItem Text="Pagado" Value="C" />
                    <asp:ListItem Text="Anulado" Value="X" />
                </asp:DropDownList>
            </td>
            <td>Total Línea:</td>
            <td>
                <asp:TextBox ID="txt_TotalLinea" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>Observación:</td>
            <td colspan="3">
                <asp:TextBox ID="txt_Observacion" runat="server" TextMode="MultiLine" Rows="2"
                    CssClass="full-width-textbox"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>
                <!-- AGREGAR eliminado -->
            </td>
            <td>
                <asp:Button ID="btn_Modificar" runat="server" Text="GUARDAR CAMBIOS"
                    CssClass="btn btn-primario" OnClick="btn_Modificar_Click" />
            </td>
            <td>
                <asp:Button ID="btn_Limpiar" runat="server" Text="LIMPIAR"
                    CssClass="btn btn-advertencia" OnClick="btn_Limpiar_Click" />
            </td>
            <td>
                <!-- ELIMINAR eliminado (se anula desde la grilla) -->
            </td>
        </tr>
    </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
     <div style="width: 80%; margin: 10px auto; text-align:center;">
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Black" Font-Bold="true" />
    </div>

    <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False"
        CssClass="gridview-style sticky-header"
        OnRowCommand="gvDetalle_RowCommand"
        DataKeyNames="Id_MatriculaDetalle">

        <Columns>
            <asp:BoundField DataField="Id_MatriculaDetalle" HeaderText="Id Detalle" />
            <asp:BoundField DataField="NroCuota" HeaderText="Nro" />
            <asp:BoundField DataField="NombreCuota" HeaderText="Cuota" />
            <asp:BoundField DataField="FechaVencimiento" HeaderText="F. Venc." DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="Descuento" HeaderText="Desc." DataFormatString="{0:N2}" />
            <asp:BoundField DataField="Adicional" HeaderText="Adic." DataFormatString="{0:N2}" />
            <asp:BoundField DataField="TotalLinea" HeaderText="Total" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="EstadoPago" HeaderText="Estado" />

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:Button ID="btnConsultar" runat="server" Text="Editar"
                        CommandName="Consultar"
                        CommandArgument='<%# Eval("Id_MatriculaDetalle") %>'
                        CssClass="btn btn-info btn-sm" />

                    <asp:Button ID="btnAnular" runat="server" Text="Anular"
                        CommandName="Anular"
                        CommandArgument='<%# Eval("Id_MatriculaDetalle") %>'
                        CssClass="btn btn-peligro btn-sm"
                        OnClientClick="return confirm('¿Anular esta cuota?');" />

                    <asp:Button ID="btnReactivar" runat="server" Text="Reactivar"
                        CommandName="Reactivar"
                        CommandArgument='<%# Eval("Id_MatriculaDetalle") %>'
                        CssClass="btn btn-primario btn-sm" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>
</asp:Content>
