﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tetris.TetrisConnectionLocal {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TetrisConnectionLocal.ITetrisConnection")]
    public interface ITetrisConnection {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITetrisConnection/GetPlayers", ReplyAction="http://tempuri.org/ITetrisConnection/GetPlayersResponse")]
        string[] GetPlayers(int matchID);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/ITetrisConnection/GetPlayers", ReplyAction="http://tempuri.org/ITetrisConnection/GetPlayersResponse")]
        System.IAsyncResult BeginGetPlayers(int matchID, System.AsyncCallback callback, object asyncState);
        
        string[] EndGetPlayers(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITetrisConnection/AddPlayer", ReplyAction="http://tempuri.org/ITetrisConnection/AddPlayerResponse")]
        void AddPlayer(int matchID);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/ITetrisConnection/AddPlayer", ReplyAction="http://tempuri.org/ITetrisConnection/AddPlayerResponse")]
        System.IAsyncResult BeginAddPlayer(int matchID, System.AsyncCallback callback, object asyncState);
        
        void EndAddPlayer(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITetrisConnectionChannel : Tetris.TetrisConnectionLocal.ITetrisConnection, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetPlayersCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetPlayersCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string[] Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TetrisConnectionClient : System.ServiceModel.ClientBase<Tetris.TetrisConnectionLocal.ITetrisConnection>, Tetris.TetrisConnectionLocal.ITetrisConnection {
        
        private BeginOperationDelegate onBeginGetPlayersDelegate;
        
        private EndOperationDelegate onEndGetPlayersDelegate;
        
        private System.Threading.SendOrPostCallback onGetPlayersCompletedDelegate;
        
        private BeginOperationDelegate onBeginAddPlayerDelegate;
        
        private EndOperationDelegate onEndAddPlayerDelegate;
        
        private System.Threading.SendOrPostCallback onAddPlayerCompletedDelegate;
        
        public TetrisConnectionClient() {
        }
        
        public TetrisConnectionClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TetrisConnectionClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TetrisConnectionClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TetrisConnectionClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public event System.EventHandler<GetPlayersCompletedEventArgs> GetPlayersCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> AddPlayerCompleted;
        
        public string[] GetPlayers(int matchID) {
            return base.Channel.GetPlayers(matchID);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginGetPlayers(int matchID, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetPlayers(matchID, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public string[] EndGetPlayers(System.IAsyncResult result) {
            return base.Channel.EndGetPlayers(result);
        }
        
        private System.IAsyncResult OnBeginGetPlayers(object[] inValues, System.AsyncCallback callback, object asyncState) {
            int matchID = ((int)(inValues[0]));
            return this.BeginGetPlayers(matchID, callback, asyncState);
        }
        
        private object[] OnEndGetPlayers(System.IAsyncResult result) {
            string[] retVal = this.EndGetPlayers(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetPlayersCompleted(object state) {
            if ((this.GetPlayersCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetPlayersCompleted(this, new GetPlayersCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetPlayersAsync(int matchID) {
            this.GetPlayersAsync(matchID, null);
        }
        
        public void GetPlayersAsync(int matchID, object userState) {
            if ((this.onBeginGetPlayersDelegate == null)) {
                this.onBeginGetPlayersDelegate = new BeginOperationDelegate(this.OnBeginGetPlayers);
            }
            if ((this.onEndGetPlayersDelegate == null)) {
                this.onEndGetPlayersDelegate = new EndOperationDelegate(this.OnEndGetPlayers);
            }
            if ((this.onGetPlayersCompletedDelegate == null)) {
                this.onGetPlayersCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetPlayersCompleted);
            }
            base.InvokeAsync(this.onBeginGetPlayersDelegate, new object[] {
                        matchID}, this.onEndGetPlayersDelegate, this.onGetPlayersCompletedDelegate, userState);
        }
        
        public void AddPlayer(int matchID) {
            base.Channel.AddPlayer(matchID);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginAddPlayer(int matchID, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginAddPlayer(matchID, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndAddPlayer(System.IAsyncResult result) {
            base.Channel.EndAddPlayer(result);
        }
        
        private System.IAsyncResult OnBeginAddPlayer(object[] inValues, System.AsyncCallback callback, object asyncState) {
            int matchID = ((int)(inValues[0]));
            return this.BeginAddPlayer(matchID, callback, asyncState);
        }
        
        private object[] OnEndAddPlayer(System.IAsyncResult result) {
            this.EndAddPlayer(result);
            return null;
        }
        
        private void OnAddPlayerCompleted(object state) {
            if ((this.AddPlayerCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.AddPlayerCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void AddPlayerAsync(int matchID) {
            this.AddPlayerAsync(matchID, null);
        }
        
        public void AddPlayerAsync(int matchID, object userState) {
            if ((this.onBeginAddPlayerDelegate == null)) {
                this.onBeginAddPlayerDelegate = new BeginOperationDelegate(this.OnBeginAddPlayer);
            }
            if ((this.onEndAddPlayerDelegate == null)) {
                this.onEndAddPlayerDelegate = new EndOperationDelegate(this.OnEndAddPlayer);
            }
            if ((this.onAddPlayerCompletedDelegate == null)) {
                this.onAddPlayerCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnAddPlayerCompleted);
            }
            base.InvokeAsync(this.onBeginAddPlayerDelegate, new object[] {
                        matchID}, this.onEndAddPlayerDelegate, this.onAddPlayerCompletedDelegate, userState);
        }
    }
}