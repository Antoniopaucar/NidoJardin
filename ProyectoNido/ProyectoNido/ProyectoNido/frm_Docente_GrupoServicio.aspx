<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_Docente_GrupoServicio.aspx.cs" Inherits="ProyectoNido.frm_Docente_GrupoServicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/docente_grupo_anual.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script src="Js/docente_grupo_anual.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="docente-left">
        <div class="docente-avatar"></div>
        <div class="docente-nombre">
            <asp:Label ID="lblNombreDocente" runat="server" Text="nombre y apellido"></asp:Label>
        </div>
        <div class="docente-menu">
            <a href="frm_Docente_Datos.aspx">Datos</a>
            <a href="frm_Docente_Comunicado.aspx">Ver Comunicado</a>
            <a href="frm_Docente_GrupoAnual.aspx">Grupo Anual</a>
            <a href="frm_Docente_GrupoServicio.aspx" class="activo">Grupo Servicio</a>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div class="grupo-container">
        <h2 class="grupo-titulo-pagina">Mis Grupos de Servicio Asignados</h2>

        <asp:Repeater ID="rptGruposServicio" runat="server">
            <HeaderTemplate>
                <div class="grupo-grid">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="grupo-card">
                    <div class="grupo-header">
                        <div class="grupo-nivel">
                            <%# Eval("Servicio") %>
                        </div>
                        <div class="grupo-periodo">
                            <%# Eval("Periodo") %>
                        </div>
                    </div>
                    <div class="grupo-body">
                        <div class="grupo-info-item">
                            <i class="fas fa-tag"></i>
                            <span><strong>Tipo:</strong>
                                <%# Eval("Tipo") == "H" ? "Por Hora" : Eval("Tipo") == "D" ? "Por Día" : "Por Mes" %>
                            </span>
                        </div>
                        <div class="grupo-info-item">
                            <i class="fas fa-chalkboard-teacher"></i>
                            <span><strong>Salón:</strong>
                                <%# Eval("Salon") %>
                            </span>
                        </div>
                        <div class="grupo-info-item">
                            <i class="fas fa-users"></i>
                            <span><strong>Alumnos:</strong>
                                <%# Eval("TotalAlumnos") %> / <%# Eval("Aforo") %>
                            </span>
                        </div>
                    </div>
                    <div class="grupo-footer">
                        <button type="button" class="btn-ver-alumnos" 
                            data-id-grupo='<%# Eval("Id_GrupoServicio") %>'
                            data-servicio='<%# Eval("Servicio") %>'
                            data-salon='<%# Eval("Salon") %>'
                            onclick="abrirModalAlumnosServicioDesdeBoton(this)">
                            Ver Alumnos <i class="fas fa-arrow-right"></i>
                        </button>
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>

        <asp:Panel ID="pnlSinGrupos" runat="server" Visible="false" CssClass="sin-grupos">
            <i class="fas fa-folder-open" style="font-size: 48px; margin-bottom: 15px;"></i>
            <h3>No tienes grupos de servicio asignados para este periodo.</h3>
        </asp:Panel>
    </div>

    <!-- Modal para mostrar alumnos -->
    <div class="modal fade" id="modalAlumnosServicio" tabindex="-1" aria-labelledby="modalAlumnosServicioLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalAlumnosServicioLabel">
                        <i class="fas fa-users"></i> Lista de Alumnos
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div id="infoGrupoServicio" class="mb-3 p-3 bg-light rounded">
                        <strong>Servicio:</strong> <span id="lblServicioInfo"></span><br />
                        <strong>Salón:</strong> <span id="lblSalonServicioInfo"></span>
                    </div>
                    <div id="loadingAlumnosServicio" class="text-center" style="display: none;">
                        <div class="spinner-border text-primary" role="status">
                            <span class="visually-hidden">Cargando...</span>
                        </div>
                        <p>Cargando alumnos...</p>
                    </div>
                    <div id="contenidoAlumnosServicio">
                        <table class="table table-striped table-hover" id="tblAlumnosServicio">
                            <thead>
                                <tr>
                                    <th>Nombres</th>
                                    <th>Apellidos</th>
                                </tr>
                            </thead>
                            <tbody id="tbodyAlumnosServicio">
                                <!-- Los alumnos se cargarán aquí -->
                            </tbody>
                        </table>
                    </div>
                    <div id="sinAlumnosServicio" class="text-center p-4" style="display: none;">
                        <i class="fas fa-user-slash" style="font-size: 48px; color: #ccc; margin-bottom: 15px;"></i>
                        <p class="text-muted">No hay alumnos inscritos en este grupo de servicio.</p>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
