<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_Nivel.aspx.cs" Inherits="ProyectoNido.frm_Nivel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table id="tablaNivel" class="tablaForm" >
        <tr class="titulo">
            <td colspan="4">
                <asp:Label ID="Label2" runat="server" Text="Nivel"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Id:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_IdNivel" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Nombre:"></asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txt_Nombre" runat="server" CssClass="full-width-textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Descripcion:"></asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txt_Descripcion" runat="server" TextMode="MultiLine" Rows="5" CssClass="full-width-textbox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btn_Agregar" runat="server" Text="AGREGAR" 
                    OnClientClick="return confirm('¿Deseas agregar este Nivel?') && validarCamposTabla('tablaNivel','txt_Descripcion');" 
                    OnClick="btn_Agregar_Click" class="btn btn-exito" />
            </td>
            <td>
                <asp:Button ID="btn_Modificar" runat="server" Text="MODIFICAR" 
                    OnClientClick="return confirm('¿Deseas modificar este Nivel?') && validarCamposTabla('tablaNivel','txt_Descripcion');" 
                    OnClick="btn_Modificar_Click" class="btn btn-primario" />
            </td>
            <td>
                <asp:Button ID="btn_Limpiar" runat="server" Text="LIMPIAR" OnClick="btn_Limpiar_Click" class="btn btn-advertencia" />
            </td>
            <td>
                <asp:Button ID="btn_Eliminar" runat="server" Text="ELIMINAR" 
                    OnClientClick="return confirm('¿Deseas eliminar este Nivel?');"
                    OnClick="btn_Eliminar_Click" class="btn btn-peligro" />
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

<asp:GridView ID="gvNivel" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="4"
    OnPageIndexChanging="gvNivel_PageIndexChanging"
    OnRowCommand="gvNivel_RowCommand"
    CssClass="gridview-style sticky-header"> 
    <Columns>
        <asp:BoundField DataField="Id" HeaderText="ID" />
        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />

        <asp:TemplateField HeaderText="Acciones">
            <ItemTemplate>
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CommandName="Consultar"
                    CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-info btn-sm" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar"
                    CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-peligro btn-sm"
                    OnClientClick="return confirm('¿Deseas eliminar este Nivel?');" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

</asp:Content>
