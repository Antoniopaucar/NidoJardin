<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_Matricula.aspx.cs" Inherits="ProyectoNido.frm_Matricula" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Matrículas</title>
    <style>
        .modalFondo {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0,0,0,0.5);
            z-index: 9999;
            display: flex;
            justify-content: center;
            align-items: center;
        }
        .modalContenido {
            background-color: #fff;
            padding: 15px;
            border-radius: 4px;
            min-width: 400px;
            max-height: 70vh;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <table id="tablaMatricula" class="tablaForm">
        <tr class="titulo">
            <td colspan="4">
                <asp:Label ID="LabelTitulo" runat="server" Text="Matrículas"></asp:Label>
            </td>
        </tr>

        <!-- Id y Código -->
        <tr>
            <td>
                <asp:Label ID="LabelIdMatricula" runat="server" Text="Id Matrícula:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_IdMatricula" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="LabelCodigo" runat="server" Text="Código:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_Codigo" runat="server"></asp:TextBox>
            </td>
        </tr>

        <!-- Alumno -->
        <tr>
            <td>
                <asp:Label ID="LabelAlumno" runat="server" Text="Alumno:"></asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddl_Alumno" runat="server" CssClass="full-width-textbox">
                </asp:DropDownList>
            </td>
        </tr>

        <!-- Grupo Anual -->
        <tr>
            <td>
                <asp:Label ID="LabelGrupoAnual" runat="server" Text="Grupo Anual:"></asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddl_GrupoAnual" runat="server" CssClass="full-width-textbox">
                </asp:DropDownList>
            </td>
        </tr>

        <!-- Tarifario -->
        <tr>
            <td>
                <asp:Label ID="LabelTarifario" runat="server" Text="Tarifario:"></asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddl_Tarifario" runat="server" CssClass="full-width-textbox">
                </asp:DropDownList>
            </td>
        </tr>

        <!-- Fecha matrícula -->
        <tr>
            <td>
                <asp:Label ID="LabelFechaMatricula" runat="server" Text="Fecha Matrícula:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_FechaMatricula" runat="server" TextMode="Date"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="LabelEstado" runat="server" Text="Estado:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddl_Estado" runat="server">
                    <asp:ListItem Text="Activo" Value="A" />
                    <asp:ListItem Text="Inactivo" Value="I" />
                    <asp:ListItem Text="Anulado" Value="X" />
                </asp:DropDownList>
            </td>
        </tr>

        <!-- Totales (solo lectura, se recalculan por SP) -->
        <tr>
            <td>
                <asp:Label ID="LabelSubTotal" runat="server" Text="SubTotal:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_SubTotal" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="LabelDescuentoTotal" runat="server" Text="Desc. Total:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_DescuentoTotal" runat="server" Enabled="false"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="LabelTotal" runat="server" Text="Total:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt_Total" runat="server" Enabled="false"></asp:TextBox>
            </td>
            <td></td>
            <td></td>
        </tr>

        <!-- Observación -->
        <tr>
            <td>
                <asp:Label ID="LabelObservacion" runat="server" Text="Observación:"></asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txt_Observacion" runat="server" TextMode="MultiLine" Rows="2"
                    CssClass="full-width-textbox"></asp:TextBox>
            </td>
        </tr>

        <!-- Botones -->
        <tr>
            <td>
                <asp:Button ID="btn_Nuevo" runat="server" Text="NUEVO"
                    CssClass="btn btn-info" OnClick="btn_Nuevo_Click" />
            </td>
            <td>
                <asp:Button ID="btn_Guardar" runat="server" Text="GUARDAR"
                    CssClass="btn btn-exito" OnClick="btn_Guardar_Click" />
            </td>
            <td>
                <asp:Button ID="btn_Modificar" runat="server" Text="MODIFICAR"
                    CssClass="btn btn-primario" OnClick="btn_Modificar_Click" />
            </td>
            <td>
                <asp:Button ID="btn_Limpiar" runat="server" Text="LIMPIAR"
                    CssClass="btn btn-advertencia" OnClick="btn_Limpiar_Click" />
            </td>
        </tr>

        <tr>
            <td>
                <asp:Button ID="btn_Anular" runat="server" Text="ANULAR MATRÍCULA"
                    CssClass="btn btn-peligro"
                    OnClick="btn_Anular_Click"
                    OnClientClick="return confirm('¿Deseas cambiar el estado de esta matrícula?');" />
            </td>
            <td colspan="3"></td>
        </tr>
    </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">

    <div style="width: 60%; margin: 20px auto; text-align: center;">
        <div style="display: flex; justify-content: center; gap: 10px;">
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="full-width-textbox"
                placeholder="Buscar por código o nombre de alumno..." />

            <asp:DropDownList ID="ddlEstadoFiltro" runat="server">
                <asp:ListItem Text="-- Todos --" Value=""></asp:ListItem>
                <asp:ListItem Text="Activo" Value="A"></asp:ListItem>
                <asp:ListItem Text="Inactivo" Value="I"></asp:ListItem>
                <asp:ListItem Text="Anulado" Value="X"></asp:ListItem>
            </asp:DropDownList>

            <asp:Button ID="btnFiltrar" runat="server" Text="FILTRAR"
                CssClass="btn btn-info" OnClick="btnFiltrar_Click" />
        </div>
        <div style="margin-top: 10px;">
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Black" Font-Bold="true" />
        </div>
    </div>

    <!-- Grilla de matrículas -->
    <asp:GridView ID="gvMatriculas" runat="server"
        AutoGenerateColumns="False"
        AllowPaging="True"
        PageSize="5"
        CssClass="gridview-style sticky-header"
        OnPageIndexChanging="gvMatriculas_PageIndexChanging"
        OnRowCommand="gvMatriculas_RowCommand">

        <Columns>
            <asp:BoundField DataField="Id_Matricula" HeaderText="Id" />
            <asp:BoundField DataField="Codigo" HeaderText="Código" />
            <asp:BoundField DataField="AlumnoNombre" HeaderText="Alumno" />
            <asp:BoundField DataField="GrupoNombre" HeaderText="Grupo" />
            <asp:BoundField DataField="NombreTarifario" HeaderText="Tarifario" />
            <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="Estado" HeaderText="Estado" />

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:Button ID="btnConsultar" runat="server" Text="Consultar"
                        CommandName="Consultar"
                        CommandArgument='<%# Eval("Id_Matricula") %>'
                        CssClass="btn btn-info btn-sm" />

                    <asp:Button ID="btnCuotas" runat="server" Text="Cuotas"
                        CommandName="VerCuotas"
                        CommandArgument='<%# Eval("Id_Matricula") %>'
                        CssClass="btn btn-primario btn-sm" />

                    <asp:Button ID="btnAnular" runat="server" Text="Cambiar Estado"
                        CommandName="CambiarEstado"
                        CommandArgument='<%# Eval("Id_Matricula") %>'
                        CssClass="btn btn-peligro btn-sm"
                        OnClientClick="return confirm('¿Cambiar estado de esta matrícula?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>
