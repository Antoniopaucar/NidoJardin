<%@ Page Title="" Language="C#" MasterPageFile="~/TemplatePrincipal.Master" AutoEventWireup="true" CodeBehind="frm_Apoderado_Comunicado.aspx.cs" Inherits="ProyectoNido.frm_Apoderado_Comunicado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/apoderado_hijos.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
            <div class="docente-left">
    <div class="docente-avatar"></div>
    <div class="docente-nombre">
        <asp:Label ID="lblNombreDocente" runat="server" Text="nombre y apellido"></asp:Label>
    </div>
    <div class="docente-menu">
        <a href="frm_Apoderado_Datos.aspx">Datos</a>
        <a class="activo" href="frm_Apoderado_Comunicado.aspx">Ver Comunicado</a>  
        <a href="frm_Apoderado_OfertaGrupoServicio.aspx">Oferta Servicio</a>
    </div>

    <!-- Sección de Hijos -->
    <div class="hijos-seccion">
        <div class="hijos-titulo">Mis Hijos</div>
        <div class="hijos-container">
            <asp:Repeater ID="rptHijos" runat="server">
                <ItemTemplate>
                    <asp:Button ID="btnHijo" runat="server" 
                        Text='<%# Eval("Nombres") + " " + Eval("ApellidoPaterno") + " " + Eval("ApellidoMaterno") %>'
                        CommandArgument='<%# Eval("Id") %>'
                        OnClick="btnHijo_Click"
                        CssClass='<%# Convert.ToInt32(Eval("Id")) == (Session["IdAlumnoSeleccionado"] != null ? Convert.ToInt32(Session["IdAlumnoSeleccionado"]) : 0) ? "btn-hijo activo" : "btn-hijo" %>' />
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
 <link href="CSS/docente_comunicado.css" rel="stylesheet" />
 <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
 <div class="comunicado-container">
     <asp:Repeater ID="rptComunicados" runat="server">
         <ItemTemplate>
             <div class="comunicado-item" onclick="toggleDetalle(this)" data-id='<%# Eval("Id") %>'>
                 <div class="comunicado-titulo">
                     <%# Eval("Nombre") %>
                 </div>
                 <div class="comunicado-remitente">
                     <%# Eval("Usuario.Nombres") %>
                         <%# Eval("Usuario.ApellidoPaterno") %>
                 </div>
                 <div class="comunicado-fecha">
                     <%# Eval("FechaCreacion", "{0:dd/MM/yyyy}" ) %>
                 </div>
                 <div class="comunicado-estado <%# (bool)Eval("Visto") ? "estado-visto" : "estado-abrir" %>">
                     <%# (bool)Eval("Visto") ? "visto" : "abrir" %>
                 </div>
             </div>
             <div class="comunicado-detalle">
                 <p>
                     <%# Eval("Descripcion") %>
                 </p>
             </div>
         </ItemTemplate>
     </asp:Repeater>
 </div>

 <script src="Js/docente_comunicado.js" type="text/javascript"></script>

</asp:Content>
