<%@ Page Title="Sistema de Cotizaciones" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SistemaCotizaciones._Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Sistema de Cotizaciones - Cargando...</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            text-align: center;
            padding: 100px;
            background-color: #f5f5f5;
        }
        .loading-container {
            background-color: white;
            padding: 40px;
            border-radius: 10px;
            box-shadow: 0 4px 6px rgba(0,0,0,0.1);
            max-width: 500px;
            margin: 0 auto;
        }
        .spinner {
            border: 4px solid #f3f3f3;
            border-top: 4px solid #3498db;
            border-radius: 50%;
            width: 50px;
            height: 50px;
            animation: spin 1s linear infinite;
            margin: 20px auto;
        }
        @keyframes spin {
            0% { transform: rotate(0deg); }
            100% { transform: rotate(360deg); }
        }
        .btn {
            background-color: #3498db;
            color: white;
            padding: 12px 24px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            text-decoration: none;
            display: inline-block;
            margin-top: 20px;
        }
        .btn:hover {
            background-color: #2980b9;
        }
    </style>
</head>
<body>
    <div class="loading-container">
        <h2>🏪 Sistema de Cotizaciones</h2>
        <h3>Pasty Custom Design</h3>
        <div class="spinner"></div>
        <p>Redirigiendo al sistema de login...</p>
        <p>Si no es redirigido automáticamente:</p>
        <a href="Pages/Login.aspx" class="btn">🔑 Ir al Login</a>
    </div>

    <script type="text/javascript">
        // Redirección automática después de 2 segundos
        setTimeout(function () {
            window.location.href = 'Pages/Login.aspx';
        }, 2000);
    </script>
</body>
</html>