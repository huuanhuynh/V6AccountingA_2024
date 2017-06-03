﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Samples.Models.User>>"%>

<asp:Content ContentPlaceHolderID="content" runat="server">

   <h1>Users</h1>

   <ul>
   <% foreach (var user in Model) {%>
      <li>
         <a href="<%: Url.Action("Profile", new { username = user.Name }) %>"><%: user.Name %></a>
      </li>      
   <% } %>
   </ul>

</asp:Content>
