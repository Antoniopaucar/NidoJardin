<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_Usuario_Rol.aspx.cs" Inherits="ProyectoNido.frm_Usuario_Rol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table id="tablaUsuarioRol" class="tablaForm" >
        <tr class="titulo">
            <td colspan="4">
                <asp:Label ID="Label2" runat="server" Text="Usuario Rol"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label14" runat="server" Text="Usuario:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="Ddl_Usuario" runat="server"></asp:DropDownList>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Rol:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="Ddl_Rol" runat="server"></asp:DropDownList>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btn_Agregar" runat="server" Text="AGREGAR" 
                    OnClientClick="return confirm('¿Deseas agregar este Usuario Rol?') && validarCamposTabla('tablaUsuarioRol','');" 
                    OnClick="btn_Agregar_Click" class="btn btn-exito" />
            </td>
            <td>

            </td>
            <td>
                <asp:Button ID="btn_Limpiar" runat="server" Text="LIMPIAR" OnClick="btn_Limpiar_Click" class="btn btn-advertencia" />
            </td>
            <td>

            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">

 <div style="width: 40%; margin: 20px auto; text-align: center;">
     <div style="display: flex; justify-content: center; gap: 10px;">
         <asp:TextBox ID="txtBuscar" runat="server" CssClass="full-width-textbox" placeholder="Buscar por Usuario o Rol..." />
         <asp:Button ID="btnFiltrar" runat="server" Text="FILTRAR" CssClass="btn btn-info" OnClick="btnFiltrar_Click" />
     </div>
     <div style="margin-top: 10px;">
         <asp:Label ID="lblMensaje" runat="server" ForeColor="Black" Font-Bold="true" />
     </div>
 </div>


 <asp:GridView ID="gvUsuarioRol" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="4"
     OnPageIndexChanging="gvUsuarioRol_PageIndexChanging"
     OnRowCommand="gvUsuarioRol_RowCommand"
     CssClass="gridview-style sticky-header"> 
    <Columns>
        <asp:BoundField DataField="Usuario.NombreUsuario" HeaderText="Usuario" />
        <asp:BoundField DataField="Rol.NombreRol" HeaderText="Rol" />
        <asp:TemplateField HeaderText="Acciones">
            <ItemTemplate>
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" 
                    CommandName="Eliminar"
                    CommandArgument='<%# Eval("Usuario.Id") + "-" + Eval("Rol.Id") %>'
                    CssClass="btn btn-peligro btn-sm"
                    OnClientClick="return confirm('¿Deseas eliminar este Usuario Rol?');" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
 </asp:GridView>

</asp:Content>
