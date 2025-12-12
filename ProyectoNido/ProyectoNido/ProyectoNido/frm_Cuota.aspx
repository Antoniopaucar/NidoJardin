<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_Cuota.aspx.cs" Inherits="ProyectoNido.frm_Cuota" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table id="tablaCuota" class="tablaForm">
        <tr class="titulo">
            <td colspan="4">
                <asp:Label ID="LabelTitulo" runat="server" Text="Cuotas"></asp:Label>
            </td>
        </tr>

        <!-- TARIFARIO -->
        <tr>
            <td>
                <asp:Label ID="LabelTarifario" runat="server" Text="Tarifario:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:DropDownList ID="ddl_Tarifario" runat="server"
                    AutoPostBack="true" OnSelectedIndexChanged="ddl_Tarifario_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>

        <!-- ID CUOTA -->
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Id Cuota:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_IdCuota" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>

        <!-- NRO CUOTA -->
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Nro Cuota:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txt_NroCuota" runat="server" onkeypress="return SoloNumeros(event);"
                    CssClass="full-width-textbox"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>

        <!-- FECHA PAGO SUGERIDO -->
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Fecha Pago Sugerido:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txt_FechaPagoSugerido" runat="server" TextMode="Date"
                    CssClass="full-width-textbox"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>

        <!-- MONTO -->
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Monto:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txt_Monto" runat="server" onkeypress="return SoloNumeros(event);"
                    CssClass="full-width-textbox"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>

        <!-- DESCUENTO -->
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Descuento:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txt_Descuento" runat="server" onkeypress="return SoloNumeros(event);"
                    CssClass="full-width-textbox"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>

        <!-- ADICIONAL -->
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Adicional:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txt_Adicional" runat="server" onkeypress="return SoloNumeros(event);"
                    CssClass="full-width-textbox"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>

        <!-- NOMBRE CUOTA -->
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Nombre Cuota:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txt_NombreCuota" runat="server"
                    CssClass="full-width-textbox"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>

        <!-- BOTONES -->
        <tr>
            <td>
                <asp:Button ID="btn_Agregar" runat="server" Text="AGREGAR"
                    OnClientClick="return confirm('¿Deseas agregar esta cuota?') 
                        && validarCamposTabla('tablaCuota','ddl_Tarifario,txt_NroCuota,txt_FechaPagoSugerido,txt_Monto,txt_NombreCuota');"
                    OnClick="btn_Agregar_Click"
                    CssClass="btn btn-exito" />
            </td>
            <td>
                <asp:Button ID="btn_Modificar" runat="server" Text="MODIFICAR"
                    OnClientClick="return confirm('¿Deseas modificar esta cuota?') 
                        && validarCamposTabla('tablaCuota','ddl_Tarifario,txt_NroCuota,txt_FechaPagoSugerido,txt_Monto,txt_NombreCuota');"
                    OnClick="btn_Modificar_Click"
                    CssClass="btn btn-primario" />
            </td>
            <td>
                <asp:Button ID="btn_Limpiar" runat="server" Text="LIMPIAR"
                    OnClick="btn_Limpiar_Click"
                    CssClass="btn btn-advertencia" />
            </td>
            <td>
                <asp:Button ID="btn_Eliminar" runat="server" Text="ELIMINAR"
                    OnClientClick="return confirm('¿Deseas eliminar esta cuota?');"
                    OnClick="btn_Eliminar_Click"
                    CssClass="btn btn-peligro" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div style="width: 40%; margin: 20px auto; text-align: center;">
        <div style="display: flex; justify-content: center; gap: 10px;">
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="full-width-textbox"
                placeholder="Buscar por nombre de cuota, nro o monto..." />
            <asp:Button ID="btnFiltrar" runat="server" Text="FILTRAR"
                CssClass="btn btn-info" OnClick="btnFiltrar_Click" />
        </div>
        <div style="margin-top: 10px;">
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Black" Font-Bold="true" />
        </div>
    </div>

    <asp:GridView ID="gvCuota" runat="server" AutoGenerateColumns="False"
        AllowPaging="True" PageSize="8"
        CssClass="gridview-style sticky-header"
        OnPageIndexChanging="gvCuota_PageIndexChanging"
        OnRowCommand="gvCuota_RowCommand">
        <Columns>
            <asp:BoundField DataField="Id_Cuota" HeaderText="ID" />
            <asp:BoundField DataField="NroCuota" HeaderText="Nro" />
            <asp:BoundField DataField="FechaPagoSugerido" HeaderText="Fecha Pago"
                DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="Descuento" HeaderText="Descuento" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="Adicional" HeaderText="Adicional" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="NombreCuota" HeaderText="Nombre Cuota" />

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:Button ID="btnConsultar" runat="server" Text="Consultar"
                        CommandName="Consultar"
                        CommandArgument='<%# Eval("Id_Cuota") %>'
                        CssClass="btn btn-info btn-sm" />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar"
                        CommandName="Eliminar"
                        CommandArgument='<%# Eval("Id_Cuota") %>'
                        CssClass="btn btn-peligro btn-sm"
                        OnClientClick="return confirm('¿Deseas eliminar esta cuota?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
