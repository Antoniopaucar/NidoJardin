<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_ServicioAlumno.aspx.cs" Inherits="ProyectoNido.frm_ServicioAlumno" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table class="tablaForm">

        <tr>
            <td>Id Servicio Alumno:</td>
            <td>
                <asp:TextBox ID="txt_IdServicioAlumno" runat="server" Width="120" />
            </td>
        </tr>

        <tr>
            <td>Grupo Servicio:</td>
            <td>
                <asp:HiddenField ID="hdnIdGrupoServicio" runat="server" />
                <asp:TextBox 
                    ID="txt_GrupoServicioSeleccionado"
                    runat="server"
                    ReadOnly="true"
                    EnableViewState="false"
                    Width="300" />
                &nbsp;
                <button type="button" class="btn btn-secondary btn-sm"
                        data-bs-toggle="modal" data-bs-target="#modalGrupoServicio">
                    Buscar
                </button>
            </td>
        </tr>

        <tr>
            <td>Alumno:</td>
            <td>
                <asp:HiddenField ID="hdnIdAlumno" runat="server" />
                <asp:TextBox 
                    ID="txt_AlumnoSeleccionado"
                    runat="server"
                    ReadOnly="true"
                    EnableViewState="false"
                    Width="300" />
                &nbsp;
                <button type="button" class="btn btn-secondary btn-sm"
                        data-bs-toggle="modal" data-bs-target="#modalAlumno">
                    Buscar
                </button>
            </td>
        </tr>

        <tr>
            <td>Fecha Inicio:</td>
            <td>
                <asp:TextBox ID="txt_FechaInicio" runat="server" Width="140" placeholder="yyyy-MM-dd" />
            </td>
        </tr>

        <tr>
            <td>Fecha Final:</td>
            <td>
                <asp:TextBox ID="txt_FechaFinal" runat="server" Width="140" placeholder="yyyy-MM-dd (opcional)" />
            </td>
        </tr>

        <tr>
            <td>Fecha Pago:</td>
            <td>
                <asp:TextBox ID="txt_FechaPago" runat="server" Width="140" placeholder="yyyy-MM-dd (opcional)" />
            </td>
        </tr>

        <tr>
            <td>Hora Inicio:</td>
            <td>
                <asp:TextBox ID="txt_HoraInicio" runat="server" Width="120" placeholder="HH:mm (opcional)" />
            </td>
        </tr>

        <tr>
            <td>Hora Final:</td>
            <td>
                <asp:TextBox ID="txt_HoraFinal" runat="server" Width="120" placeholder="HH:mm (opcional)" />
            </td>
        </tr>

        <tr>
            <td>Monto:</td>
            <td>
                <asp:TextBox ID="txt_Monto" runat="server" Width="140" />
            </td>
        </tr>

        <tr>
            <td>
                <asp:Button ID="btn_Agregar" runat="server" Text="AGREGAR"
                    OnClick="btn_Agregar_Click" CssClass="btn btn-exito" />
            </td>
            <td>
                <asp:Button ID="btn_Modificar" runat="server" Text="MODIFICAR"
                    OnClick="btn_Modificar_Click" CssClass="btn btn-primario" />
            </td>
            <td>
                <asp:Button ID="btn_Limpiar" runat="server" Text="LIMPIAR"
                    OnClick="btn_Limpiar_Click" CssClass="btn btn-advertencia" />
            </td>
            <td>
                <asp:Button ID="btn_Eliminar" runat="server" Text="ELIMINAR"
                    OnClick="btn_Eliminar_Click" CssClass="btn btn-peligro" />
            </td>
        </tr>
    </table>

    <!-- ================= MODAL GRUPO SERVICIO ================= -->
    <div class="modal fade" id="modalGrupoServicio" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title">Buscar Grupo Servicio</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>

                <div class="modal-body">
                    Buscar:
                    <asp:TextBox ID="txtBuscarGrupoServicio" runat="server" Width="250" />
                    <asp:LinkButton ID="btnBuscarGrupoServicioModal" runat="server"
                        CssClass="btn btn-primary btn-sm"
                        OnClick="btnBuscarGrupoServicioModal_Click">
                        Buscar
                    </asp:LinkButton>

                    <hr />

                    <asp:GridView ID="gvGrupoServicioModal" runat="server"
                        CssClass="table table-hover"
                        AutoGenerateColumns="False"
                        DataKeyNames="Id_GrupoServicio"
                        OnRowCommand="gvGrupoServicioModal_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="NombreSalon" HeaderText="Salón" />
                            <asp:BoundField DataField="NombreProfesor" HeaderText="Profesor" />
                            <asp:BoundField DataField="NombreServicio" HeaderText="Servicio" />
                            <asp:BoundField DataField="Periodo" HeaderText="Periodo" />
                            <asp:ButtonField Text="Seleccionar" CommandName="seleccionar" />
                        </Columns>
                    </asp:GridView>

                </div>
            </div>
        </div>
    </div>

    <!-- ================= MODAL ALUMNO ================= -->
    <div class="modal fade" id="modalAlumno" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title">Buscar Alumno</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>

                <div class="modal-body">
                    Buscar:
                    <asp:TextBox ID="txtBuscarAlumno" runat="server" Width="250" />
                    <asp:LinkButton ID="btnBuscarAlumnoModal" runat="server"
                        CssClass="btn btn-primary btn-sm"
                        OnClick="btnBuscarAlumnoModal_Click">
                        Buscar
                    </asp:LinkButton>

                    <hr />

                    <asp:GridView ID="gvAlumno" runat="server"
                        CssClass="table table-hover"
                        AutoGenerateColumns="False"
                        DataKeyNames="Id_Alumno"
                        OnRowCommand="gvAlumno_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="NombreCompleto" HeaderText="Alumno" />
                            <asp:BoundField DataField="Documento" HeaderText="Documento" />
                            <asp:ButtonField Text="Seleccionar" CommandName="seleccionar" />
                        </Columns>
                    </asp:GridView>

                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div style="width: 60%; margin: 20px auto; text-align: center;">
        <div style="display: flex; justify-content: center; gap: 10px;">
            <asp:TextBox ID="txtBuscar" runat="server" CssClass="full-width-textbox"
                placeholder="Buscar por alumno / salón / servicio / periodo..." />
            <asp:Button ID="btnFiltrar" runat="server" Text="FILTRAR"
                CssClass="btn btn-info" OnClick="btnFiltrar_Click" />
        </div>
        <div style="margin-top: 10px;">
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Black" Font-Bold="true" />
        </div>
    </div>

    <asp:GridView ID="gvServicioAlumno" runat="server" AutoGenerateColumns="False"
        AllowPaging="True" PageSize="7"
        OnPageIndexChanging="gvServicioAlumno_PageIndexChanging"
        OnRowCommand="gvServicioAlumno_RowCommand"
        CssClass="gridview-style sticky-header">

        <Columns>
            <asp:BoundField DataField="Id_ServicioAlumno" HeaderText="ID" />
            <asp:BoundField DataField="NombreAlumno" HeaderText="Alumno" />
            <asp:BoundField DataField="NombreSalon" HeaderText="Salón" />
            <asp:BoundField DataField="NombreServicio" HeaderText="Servicio" />
            <asp:BoundField DataField="Periodo" HeaderText="Periodo" />
            <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:N2}" />

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:Button ID="btnConsultar" runat="server" Text="Consultar"
                        CommandName="Consultar" CommandArgument='<%# Eval("Id_ServicioAlumno") %>'
                        CssClass="btn btn-info btn-sm" />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar"
                        CommandName="Eliminar" CommandArgument='<%# Eval("Id_ServicioAlumno") %>'
                        CssClass="btn btn-peligro btn-sm"
                        OnClientClick="return confirm('¿Deseas eliminar este registro?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>