<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_ServicioAlumno.aspx.cs" Inherits="ProyectoNido.frm_ServicioAlumno" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table id="tablaServicioAlumno" class="tablaForm">
    <tr class="titulo">
        <td colspan="4">
            <asp:Label ID="LabelTitulo" runat="server" Text="Servicio Alumno"></asp:Label>
        </td>
    </tr>

    <!-- ID -->
    <tr>
        <td><asp:Label ID="LabelId" runat="server" Text="Id Servicio Alumno:"></asp:Label></td>
        <td><asp:TextBox ID="txt_IdServicioAlumno" runat="server" Enabled="false"></asp:TextBox></td>
        <td>&nbsp;</td><td>&nbsp;</td>
    </tr>

    <!-- Grupo Servicio -->
    <tr>
        <td><asp:Label ID="LabelGrupo" runat="server" Text="Grupo Servicio:"></asp:Label></td>
        <td colspan="3">
            <asp:DropDownList ID="ddl_GrupoServicio" runat="server" CssClass="full-width-textbox" OnSelectedIndexChanged="ddl_GrupoServicio_SelectedIndexChanged"></asp:DropDownList>
        </td>
    </tr>

    <!-- Alumno -->
    <tr>
        <td><asp:Label ID="LabelAlumno" runat="server" Text="Alumno:"></asp:Label></td>
        <td colspan="3">
            <asp:DropDownList ID="ddl_Alumno" runat="server" CssClass="full-width-textbox"></asp:DropDownList>
        </td>
    </tr>

    <!-- Fechas -->
    <tr>
        <td><asp:Label ID="LabelFechaInicio" runat="server" Text="Fecha Inicio:"></asp:Label></td>
        <td><asp:TextBox ID="txt_FechaInicio" runat="server" CssClass="full-width-textbox" placeholder="dd/mm/yyyy"></asp:TextBox></td>

        <td><asp:Label ID="LabelFechaFinal" runat="server" Text="Fecha Final:"></asp:Label></td>
        <td><asp:TextBox ID="txt_FechaFinal" runat="server" CssClass="full-width-textbox" placeholder="dd/mm/yyyy"></asp:TextBox></td>
    </tr>

    <!-- Fecha Pago -->
    <tr>
        <td><asp:Label ID="LabelFechaPago" runat="server" Text="Fecha Pago:"></asp:Label></td>
        <td colspan="3">
            <asp:TextBox ID="txt_FechaPago" runat="server" CssClass="full-width-textbox" placeholder="dd/mm/yyyy"></asp:TextBox>
        </td>
    </tr>

    <!-- Horas -->
    <tr>
        <td><asp:Label ID="LabelHoraInicio" runat="server" Text="Hora Inicio:"></asp:Label></td>
        <td><asp:TextBox ID="txt_HoraInicio" runat="server" CssClass="full-width-textbox" placeholder="HH:mm"></asp:TextBox></td>

        <td><asp:Label ID="LabelHoraFinal" runat="server" Text="Hora Final:"></asp:Label></td>
        <td><asp:TextBox ID="txt_HoraFinal" runat="server" CssClass="full-width-textbox" placeholder="HH:mm"></asp:TextBox></td>
    </tr>

    <!-- Monto -->
    <tr>
        <td><asp:Label ID="LabelMonto" runat="server" Text="Monto:"></asp:Label></td>
        <td><asp:TextBox ID="txt_Monto" runat="server" CssClass="full-width-textbox"></asp:TextBox></td>
        <td>&nbsp;</td><td>&nbsp;</td>
    </tr>

    <!-- BOTONES -->
    <tr>
        <td>
            <asp:Button ID="btn_Agregar" runat="server" Text="AGREGAR"
                OnClientClick="return confirm('¿Deseas agregar este registro?');"
                OnClick="btn_Agregar_Click" CssClass="btn btn-exito" />
        </td>
        <td>
            <asp:Button ID="btn_Modificar" runat="server" Text="MODIFICAR"
                OnClientClick="return confirm('¿Deseas modificar este registro?');"
                OnClick="btn_Modificar_Click" CssClass="btn btn-primario" />
        </td>
        <td>
            <asp:Button ID="btn_Limpiar" runat="server" Text="LIMPIAR"
                OnClick="btn_Limpiar_Click" CssClass="btn btn-advertencia" />
        </td>
        <td>
            <asp:Button ID="btn_Eliminar" runat="server" Text="ELIMINAR"
                OnClientClick="return confirm('¿Deseas eliminar este registro?');"
                OnClick="btn_Eliminar_Click" CssClass="btn btn-peligro" />
        </td>
    </tr>
</table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <asp:GridView ID="gvServicioAlumno" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="5"
    OnPageIndexChanging="gvServicioAlumno_PageIndexChanging"
    OnRowCommand="gvServicioAlumno_RowCommand"
    CssClass="gridview-style sticky-header">

    <Columns>
        <asp:BoundField DataField="Id_ServicioAlumno" HeaderText="ID" />
        <asp:BoundField DataField="NombreGrupo" HeaderText="Grupo" />
        <asp:BoundField DataField="NombreAlumno" HeaderText="Alumno" />
        <asp:BoundField DataField="FechaInicio" HeaderText="Fecha Inicio" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField DataField="FechaPago" HeaderText="Fecha Pago" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:C}" />

        <asp:TemplateField HeaderText="Acciones">
            <ItemTemplate>
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CommandName="Consultar"
                    CommandArgument='<%# Eval("Id_ServicioAlumno") %>' CssClass="btn btn-info btn-sm" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar"
                    CommandArgument='<%# Eval("Id_ServicioAlumno") %>' CssClass="btn btn-peligro btn-sm"
                    OnClientClick="return confirm('¿Deseas eliminar este registro?');" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>

</asp:GridView>
</asp:Content>
