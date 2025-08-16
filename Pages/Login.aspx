<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SistemaCotizaciones.Pages.Login" %>

<!DOCTYPE html>
<html>
<head>
    <title>Login - Sistema de Cotizaciones</title>
    <style>
        body { font-family: Arial; background: #f0f0f0; margin: 0; padding: 100px; }
        .login-box { background: white; padding: 30px; border-radius: 10px; max-width: 400px; margin: 0 auto; }
        .form-group { margin: 15px 0; }
        label { display: block; margin-bottom: 5px; font-weight: bold; }
        input[type="text"], input[type="password"] { width: 100%; padding: 10px; border: 1px solid #ddd; border-radius: 5px; box-sizing: border-box; }
        .btn { background: #007bff; color: white; padding: 10px 20px; border: none; border-radius: 5px; cursor: pointer; width: 100%; }
        .btn:hover { background: #0056b3; }
        .alert { padding: 10px; margin: 10px 0; border-radius: 5px; }
        .alert-error { background: #f8d7da; color: #721c24; border: 1px solid #f5c6cb; }
        .alert-success { background: #d4edda; color: #155724; border: 1px solid #c3e6cb; }
    </style>
</head>
<body>
    <div class="login-box">
        <h2>🏪 Sistema de Cotizaciones</h2>
        <h3>Pasty Custom Design</h3>
        
        <form id="form1" runat="server">
            <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </asp:Panel>
            
            <div class="form-group">
                <label>Usuario:</label>
                <asp:TextBox ID="txtUsuario" runat="server" placeholder="admin"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>Contraseña:</label>
                <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" placeholder="admin123"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn" OnClick="btnIngresar_Click" />
            </div>
            
            <p><strong>Credenciales de prueba:</strong><br/>
            Usuario: admin<br/>
            Contraseña: admin123</p>
        </form>
    </div>
</body>
</html>