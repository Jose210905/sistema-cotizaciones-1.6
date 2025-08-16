<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Diagnostico.aspx.cs" Inherits="SistemaCotizaciones.Pages.Diagnostico" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Diagnóstico del Sistema</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 20px; background-color: #f5f5f5; }
        .container { max-width: 800px; margin: 0 auto; background: white; padding: 30px; border-radius: 10px; }
        .status-ok { color: #27ae60; font-weight: bold; }
        .status-error { color: #e74c3c; font-weight: bold; }
        .status-warning { color: #f39c12; font-weight: bold; }
        .test-section { margin: 20px 0; padding: 15px; border: 1px solid #ddd; border-radius: 5px; }
        .btn { background: #3498db; color: white; padding: 10px 20px; border: none; border-radius: 5px; cursor: pointer; }
        .btn:hover { background: #2980b9; }
    </style>
</head>
<body>
    <div class="container">
        <h1>🔧 Diagnóstico del Sistema de Cotizaciones</h1>
        
        <form id="form1" runat="server">
            <asp:Button ID="btnEjecutarDiagnostico" runat="server" Text="🔄 Ejecutar Diagnóstico" 
                CssClass="btn" OnClick="btnEjecutarDiagnostico_Click" />
            
            <div class="test-section">
                <h3>📊 Resultados del Diagnóstico</h3>
                <asp:Label ID="lblResultados" runat="server"></asp:Label>
            </div>
            
            <div class="test-section">
                <h3>🔗 Accesos Directos</h3>
                <a href="Login.aspx" class="btn">🔑 Ir al Login</a>
                <a href="Default.aspx" class="btn" style="margin-left: 10px;">🏠 Ir al Dashboard</a>
                <a href="Test.aspx" class="btn" style="margin-left: 10px;">🧪 Página de Prueba</a>
            </div>
        </form>
    </div>
</body>
</html>