<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_Login.aspx.cs" Inherits="ProyectoNido.frm_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="CSS/EstiloLogin.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" class="login-container">
        <img src="Img/usuario2.png" style="width: 201px;height: 166px;"/>
        <table style="margin: auto; text-align: center;">
            <tr style="font-weight:bold;font-size:24px">
                <td colspan="2">Usuario</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr style="font-weight:bold;font-size:24px">
                <td colspan="2">Contraseña</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtClave" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" Text="Aceptar" CssClass="btn btn-primario"/>
                </td>
                <td>
                    <asp:Button ID="btnSalir" runat="server" Text="Salir" CssClass="btn btn-primario" OnClick="btnSalir_Click"/>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
