<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="DeviceTracker.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <style type="text/css">
        .cellColor   
        { 
           color: white;
        }
    </style>
    <link href="main.css" rel="stylesheet" />
</head>
<body>

    <div class=""><h2>Device Tracker</h2></div>  
    <hr />
   
     <div class="divBody">
        <br/>      
	    <div class="outer_box">   
           <div class="inner_box_border">     
	       <br/>
               <form id="form1" runat="server">
                    <asp:ScriptManager runat="server"></asp:ScriptManager>
                   <div class="inner_box_border">     
	                <br/>
		           <asp:GridView ID="gvDeviceTracker" runat="server" 
			         RowStyle-HorizontalAlign="Left"
			         AutoGenerateColumns="False"			
			         PageSize="10"
			         GridLines="None"
			         AllowPaging="true"
			         CssClass="mGrid"
			         PagerStyle-CssClass="pgr"
			         Width="98%"   
                     OnRowDataBound="gvDeviceTracker_RowDataBound"> 
			        <Columns>
                          <asp:TemplateField HeaderText="No" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Left">
                              <ItemTemplate><%# Container.DataItemIndex + 1 %> </ItemTemplate>
                         </asp:TemplateField>
                        <asp:BoundField ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" DataField="DEVICE" HeaderText ="DEVICE" HeaderStyle-HorizontalAlign="Left" />
                       <asp:BoundField ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Left" DataField="IMEI" HeaderText ="IMEI" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Left" DataField ="STATUS" HeaderText ="Within Terminal?" HeaderStyle-HorizontalAlign="Left" />
                       <asp:BoundField ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Left" DataField ="LASTCHECKIN" HeaderText ="LAST CHECKIN" HeaderStyle-HorizontalAlign="Left" />
                        <asp:TemplateField  ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Left"  HeaderText="Device Locations" HeaderStyle-HorizontalAlign="Left" >
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSelect" Text="view device location"  runat="server" CommandArgument='<%# Eval("DEVICEID") %>' OnClick="lnkSelect_Click"  OnClientClick="return NewWindow();"/>
                            </ItemTemplate>
                        </asp:TemplateField>
			        </Columns>              
   		        </asp:GridView>
	           </div>
                </form>
           </div>
        </div>
       </div>
</body>

         <script>
        function NewWindow() {
            document.forms[0].target = '_blank';
        }
         setTimeout(function () {
            location.reload();
        }, 1000 * 60);
    </script>
</html>