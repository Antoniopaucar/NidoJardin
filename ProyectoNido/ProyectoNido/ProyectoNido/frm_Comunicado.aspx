<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_Comunicado.aspx.cs" Inherits="ProyectoNido.frm_Comunicado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table id="tablaComunicados" class="tablaForm" >
        <tr class="titulo">
            <td colspan="4">
                <asp:Label ID="Label2" runat="server" Text="Comunicados"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Id:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_IdComunicado" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label14" runat="server" Text="Dirigido A:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="Ddl_Rol" runat="server"></asp:DropDownList>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Nombre:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_Nombre" runat="server" CssClass="full-width-textbox"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
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
                <asp:Label ID="Label15" runat="server" Text="Fecha Creacion:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_Fecha_Creacion" runat="server" CssClass="full-width-textbox" TextMode="Date" ReadOnly="true"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
         <tr>
             <td>
                 <asp:Label ID="Label1" runat="server" Text="Fecha Final:"></asp:Label>
             </td>
             <td>
                 <asp:TextBox ID="txt_Fecha_Final" runat="server" CssClass="full-width-textbox" TextMode="Date"></asp:TextBox>
             </td>
             <td>&nbsp;</td>
             <td>&nbsp;</td>
         </tr>
        <tr>
            <td>
                <asp:Button ID="btn_Agregar" runat="server" Text="AGREGAR" 
                    OnClientClick="return confirm('¿Deseas agregar este Comunicado?') && validarCamposTabla('tablaComunicados','txt_Fecha_Creacion');" 
                    OnClick="btn_Agregar_Click" class="btn btn-exito" />
            </td>
            <td>
                <asp:Button ID="btn_Modificar" runat="server" Text="MODIFICAR" 
                    OnClientClick="return confirm('¿Deseas modificar este Comunicado?') && validarCamposTabla('tablaComunicados','txt_Fecha_Creacion');" 
                    OnClick="btn_Modificar_Click" class="btn btn-primario" />
            </td>
            <td>
                <asp:Button ID="btn_Limpiar" runat="server" Text="LIMPIAR" OnClick="btn_Limpiar_Click" class="btn btn-advertencia" />
            </td>
            <td>
                <asp:Button ID="btn_Eliminar" runat="server" Text="ELIMINAR" 
                    OnClientClick="return confirm('¿Deseas eliminar este Comunicado?');"
                    OnClick="btn_Eliminar_Click" class="btn btn-peligro" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div style="width: 40%; margin: 20px auto; text-align: center;">
        <div style="display: flex; justify-content: center; gap: 10px;">
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="full-width-textbox" placeholder="Buscar por nombre o Rol ..." />
            <asp:Button ID="btnFiltrar" runat="server" Text="FILTRAR" CssClass="btn btn-info" OnClick="btnFiltrar_Click" />
        </div>
        <div style="margin-top: 10px;">
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Black" Font-Bold="true" />
        </div>
    </div>


    <asp:GridView ID="gvComunicados" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="4"
        OnPageIndexChanging="gvComunicados_PageIndexChanging"
        OnRowCommand="gvComunicados_RowCommand"
        CssClass="gridview-style sticky-header"> 
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" />
            <asp:BoundField DataField="Rol.NombreRol" HeaderText="Dirigido A" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
            <asp:BoundField DataField="FechaCreacion" HeaderText="Fecha de Creacion" />
            <asp:BoundField DataField="FechaFinal" HeaderText="Fecha Final" />

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CommandName="Consultar"
                        CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-info btn-sm" />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar"
                        CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-peligro btn-sm"
                        OnClientClick="return confirm('¿Deseas eliminar este Comunicado?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
