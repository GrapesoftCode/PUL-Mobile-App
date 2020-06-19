using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Core
{
    public static class Config
    {
        public static string MainEndPoint =
            "https://pulazurefunctions.azurewebsites.net";
        public static string NegotiateEndPoint =
            $"{MainEndPoint}/api/negotiate";
        public static string MessagesEndPoint =
            $"{MainEndPoint}/api/Messages";
        public static string AddToGroupEndPoint =
            $"{MainEndPoint}/api/AddToGroup";
        public static string LeaveGroupEndPoint =
            $"{MainEndPoint}/api/RemoveFromGroup";
        public static string RoomEndPoint =
            $"{MainEndPoint}/api/Users";
        public static string UserEndPoint =
           $"{MainEndPoint}/api/User";
    }
}
