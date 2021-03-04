﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServerWCF.ServerASMX {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://serverASMX.org/", ConfigurationName="ServerASMX.ServerSoap")]
    public interface ServerSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://serverASMX.org/GetUsers", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable GetUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://serverASMX.org/GetUsers", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetUsersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://serverASMX.org/GetRoles", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable GetRoles();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://serverASMX.org/GetRoles", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetRolesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://serverASMX.org/AddRoles", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void AddRoles(string[] rolesArray);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://serverASMX.org/AddRoles", ReplyAction="*")]
        System.Threading.Tasks.Task AddRolesAsync(string[] rolesArray);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://serverASMX.org/AddUsers", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void AddUsers(System.Data.DataTable dtUsers);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://serverASMX.org/AddUsers", ReplyAction="*")]
        System.Threading.Tasks.Task AddUsersAsync(System.Data.DataTable dtUsers);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://serverASMX.org/UpdateUsers", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void UpdateUsers(System.Data.DataTable dtUsers);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://serverASMX.org/UpdateUsers", ReplyAction="*")]
        System.Threading.Tasks.Task UpdateUsersAsync(System.Data.DataTable dtUsers);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ServerSoapChannel : ServerWCF.ServerASMX.ServerSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServerSoapClient : System.ServiceModel.ClientBase<ServerWCF.ServerASMX.ServerSoap>, ServerWCF.ServerASMX.ServerSoap {
        
        public ServerSoapClient() {
        }
        
        public ServerSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServerSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServerSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServerSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Data.DataTable GetUsers() {
            return base.Channel.GetUsers();
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GetUsersAsync() {
            return base.Channel.GetUsersAsync();
        }
        
        public System.Data.DataTable GetRoles() {
            return base.Channel.GetRoles();
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GetRolesAsync() {
            return base.Channel.GetRolesAsync();
        }
        
        public void AddRoles(string[] rolesArray) {
            base.Channel.AddRoles(rolesArray);
        }
        
        public System.Threading.Tasks.Task AddRolesAsync(string[] rolesArray) {
            return base.Channel.AddRolesAsync(rolesArray);
        }
        
        public void AddUsers(System.Data.DataTable dtUsers) {
            base.Channel.AddUsers(dtUsers);
        }
        
        public System.Threading.Tasks.Task AddUsersAsync(System.Data.DataTable dtUsers) {
            return base.Channel.AddUsersAsync(dtUsers);
        }
        
        public void UpdateUsers(System.Data.DataTable dtUsers) {
            base.Channel.UpdateUsers(dtUsers);
        }
        
        public System.Threading.Tasks.Task UpdateUsersAsync(System.Data.DataTable dtUsers) {
            return base.Channel.UpdateUsersAsync(dtUsers);
        }
    }
}
