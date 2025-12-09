<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_ServicioAdicional.aspx.cs" Inherits="ProyectoNido.frm_ServicioAdicional" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table id="tablaServicioAdicional" class="tablaForm">
    <tr class="titulo">
        <td colspan="4">
            <asp:Label ID="Label2" runat="server" Text="Servicio Adicional"></asp:Label>
        </td>
    </tr>
    <!-- ID -->
    <tr>
        <td>
            <asp:Label ID="Label3" runat="server" Text="Id:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txt_IdServicioAdicional" runat="server" Enabled="false"></asp:TextBox>
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <!-- NOMBRE -->
    <tr>
        <td>
            <asp:Label ID="Label4" runat="server" Text="Nombre:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txt_Nombre" runat="server" CssClass="full-width-textbox"></asp:TextBox>
        </td>
    </tr>
    <!-- DESCRIPCIÓN -->
    <tr>
        <td>
            <asp:Label ID="Label5" runat="server" Text="Descripción:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txt_Descripcion" runat="server" TextMode="MultiLine" Rows="3" CssClass="full-width-textbox"></asp:TextBox>
        </td>
    </tr>
    <!-- TIPO -->
    <tr>
        <td>
            <asp:Label ID="Label6" runat="server" Text="Tipo:"></asp:Label>
        </td>
        <td>
            <!-- CHAR(1): por ejemplo A, B, C... -->
            <asp:TextBox ID="txt_Tipo" runat="server" MaxLength="1"></asp:TextBox>
        </td>
        <td colspan="2">
            <asp:Literal ID="lblTipoInfo" runat="server" Text="<ul style='margin:0; padding-left:16px; font-size:smaller; list-style-type:disc;'><li>H</li><li>D</li><li>M</li></ul>" />
        </td>
    </tr>
    <!-- COSTO -->
    <tr>
        <td>
            <asp:Label ID="Label7" runat="server" Text="Costo:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txt_Costo" runat="server" MaxLength="8" onkeypress="return SoloNumeros(event);"></asp:TextBox>
        </td>
        <td colspan="2">
            <asp:Label ID="lblCostoInfo" runat="server" Text="(Solo números, ej. 150 o 150.50)" Font-Size="Smaller"></asp:Label>
        </td>
    </tr>
    <!-- BOTONES -->
    <tr>
        <td>
            <asp:Button ID="btn_Agregar" runat="server" Text="AGREGAR"
                OnClientClick="return confirm('¿Deseas agregar este Servicio Adicional?') && validarCamposTabla('tablaServicioAdicional','txt_Descripcion');"
                OnClick="btn_Agregar_Click" CssClass="btn btn-exito" />
        </td>
        <td>
            <asp:Button ID="btn_Modificar" runat="server" Text="MODIFICAR"
                OnClientClick="return confirm('¿Deseas modificar este Servicio Adicional?') && validarCamposTabla('tablaServicioAdicional','txt_Descripcion');"
                OnClick="btn_Modificar_Click" CssClass="btn btn-primario" />
        </td>
        <td>
            <asp:Button ID="btn_Limpiar" runat="server" Text="LIMPIAR"
                OnClick="btn_Limpiar_Click" CssClass="btn btn-advertencia" />
        </td>
        <td>
            <asp:Button ID="btn_Eliminar" runat="server" Text="ELIMINAR"
                OnClientClick="return confirm('¿Deseas eliminar este Servicio Adicional?');"
                OnClick="btn_Eliminar_Click" CssClass="btn btn-peligro" />
        </td>
    </tr>
</table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div style="width: 40%; margin: 20px auto; text-align: center;">
    <div style="display: flex; justify-content: center; gap: 10px;">
        <asp:TextBox ID="txtBuscar" runat="server" CssClass="full-width-textbox" placeholder="Buscar por nombre..." />
        <asp:Button ID="btnFiltrar" runat="server" Text="FILTRAR" CssClass="btn btn-info" OnClick="btnFiltrar_Click" />
    </div>
    <div style="margin-top: 10px;">
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Black" Font-Bold="true" />
    </div>
</div>

<asp:GridView ID="gvServicioAdicional" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="4"
    OnPageIndexChanging="gvServicioAdicional_PageIndexChanging"
    OnRowCommand="gvServicioAdicional_RowCommand"
    CssClass="gridview-style sticky-header">
    <Columns>
        <asp:BoundField DataField="Id_ServicioAdicional" HeaderText="ID" />
        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
        <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
        <asp:BoundField DataField="Costo" HeaderText="Costo" DataFormatString="{0:N2}" />
        <asp:TemplateField HeaderText="Acciones">
            <ItemTemplate>
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CommandName="Consultar"
                    CommandArgument='<%# Eval("Id_ServicioAdicional") %>' CssClass="btn btn-info btn-sm" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar"
                    CommandArgument='<%# Eval("Id_ServicioAdicional") %>' CssClass="btn btn-peligro btn-sm"
                    OnClientClick="return confirm('¿Deseas eliminar este Servicio Adicional?');" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</asp:Content>
