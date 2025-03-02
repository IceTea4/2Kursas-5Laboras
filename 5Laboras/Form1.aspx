<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form1.aspx.cs" Inherits="_5Laboras.Form1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Pradiniai duomenys:"></asp:Label>
            <br />
            <asp:PlaceHolder ID="tablePlaceholder1" runat="server" />
            <br />
            <asp:Table ID="Table1" runat="server"></asp:Table>
            <br />
            <asp:Button ID="Button1" runat="server" Text="Nuskaityti duomenis" OnClick="Button1_Click" />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Mėnesis:"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Pradinė data:"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label5" runat="server" Text="Galinė data:"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" Text="Apskaičiuoti" OnClick="Button2_Click" />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Rezultatai:"></asp:Label>
            <br />
            <asp:Table ID="Table2" runat="server"></asp:Table>
            <br />
            <asp:Table ID="Table3" runat="server"></asp:Table>
            <br />
            <asp:Table ID="Table4" runat="server"></asp:Table>
            <br />
        </div>
    </form>
</body>
</html>
