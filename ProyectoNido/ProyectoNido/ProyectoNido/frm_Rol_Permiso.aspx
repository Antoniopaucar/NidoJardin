<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_Rol_Permiso.aspx.cs" Inherits="ProyectoNido.frm_Rol_Permiso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table id="tablaRolPermiso" class="tablaForm" >
        <tr class="titulo">
            <td colspan="4">
                <asp:Label ID="Label2" runat="server" Text="Rol Permiso"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label14" runat="server" Text="Rol:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="Ddl_Rol" runat="server"></asp:DropDownList>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Permiso:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="Ddl_Permiso" runat="server"></asp:DropDownList>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Tipo:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_Tipo" runat="server" CssClass="full-width-textbox" MaxLength="20"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr> 
        <tr>
            <td>
                <asp:Button ID="btn_Agregar" runat="server" Text="AGREGAR" 
                    OnClientClick="return confirm('¿Deseas agregar este Rol Permiso?') && validarCamposTabla('tablaRolPermiso','');" 
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
         <asp:TextBox ID="txtBuscar" runat="server" CssClass="full-width-textbox" placeholder="Buscar por Rol o Permiso ..." />
         <asp:Button ID="btnFiltrar" runat="server" Text="FILTRAR" CssClass="btn btn-info" OnClick="btnFiltrar_Click" />
     </div>
     <div style="margin-top: 10px;">
         <asp:Label ID="lblMensaje" runat="server" ForeColor="Black" Font-Bold="true" />
     </div>
 </div>


 <asp:GridView ID="gvRolPermiso" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="4"
     OnPageIndexChanging="gvRolPermiso_PageIndexChanging"
     OnRowCommand="gvRolPermiso_RowCommand"
     CssClass="gridview-style sticky-header"> 
    <Columns>
        <asp:BoundField DataField="Rol.NombreRol" HeaderText="Rol" />
        <asp:BoundField DataField="Permiso.NombrePermiso" HeaderText="Permiso" />
        <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
        <asp:TemplateField HeaderText="Acciones">
            <ItemTemplate>
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" 
                    CommandName="Eliminar"
                    CommandArgument='<%# Eval("Rol.Id") + "-" + Eval("Permiso.Id") %>'
                    CssClass="btn btn-peligro btn-sm"
                    OnClientClick="return confirm('¿Deseas eliminar este Rol Permiso?');" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
 </asp:GridView>
</asp:Content>
