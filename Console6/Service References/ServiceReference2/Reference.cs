﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Console6.ServiceReference2 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://tempuri.org", ConfigurationName="ServiceReference2.PUB0001Soap")]
    public interface PUB0001Soap {
        
        // CODEGEN: 命名空间 http://tempuri.org 的元素名称 input1 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DHC.Published.PUB0001.BS.PUB0001.HOSDocumentRetrieval", ReplyAction="*")]
        Console6.ServiceReference2.HOSDocumentRetrievalResponse HOSDocumentRetrieval(Console6.ServiceReference2.HOSDocumentRetrievalRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DHC.Published.PUB0001.BS.PUB0001.HOSDocumentRetrieval", ReplyAction="*")]
        System.Threading.Tasks.Task<Console6.ServiceReference2.HOSDocumentRetrievalResponse> HOSDocumentRetrievalAsync(Console6.ServiceReference2.HOSDocumentRetrievalRequest request);
        
        // CODEGEN: 命名空间 http://tempuri.org 的元素名称 input1 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DHC.Published.PUB0001.BS.PUB0001.HOSDocumentSearch", ReplyAction="*")]
        Console6.ServiceReference2.HOSDocumentSearchResponse HOSDocumentSearch(Console6.ServiceReference2.HOSDocumentSearchRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DHC.Published.PUB0001.BS.PUB0001.HOSDocumentSearch", ReplyAction="*")]
        System.Threading.Tasks.Task<Console6.ServiceReference2.HOSDocumentSearchResponse> HOSDocumentSearchAsync(Console6.ServiceReference2.HOSDocumentSearchRequest request);
        
        // CODEGEN: 命名空间 http://tempuri.org 的元素名称 input1 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DHC.Published.PUB0001.BS.PUB0001.SaveHOSDocumentInfo", ReplyAction="*")]
        Console6.ServiceReference2.SaveHOSDocumentInfoResponse SaveHOSDocumentInfo(Console6.ServiceReference2.SaveHOSDocumentInfoRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DHC.Published.PUB0001.BS.PUB0001.SaveHOSDocumentInfo", ReplyAction="*")]
        System.Threading.Tasks.Task<Console6.ServiceReference2.SaveHOSDocumentInfoResponse> SaveHOSDocumentInfoAsync(Console6.ServiceReference2.SaveHOSDocumentInfoRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HOSDocumentRetrievalRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HOSDocumentRetrieval", Namespace="http://tempuri.org", Order=0)]
        public Console6.ServiceReference2.HOSDocumentRetrievalRequestBody Body;
        
        public HOSDocumentRetrievalRequest() {
        }
        
        public HOSDocumentRetrievalRequest(Console6.ServiceReference2.HOSDocumentRetrievalRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org")]
    public partial class HOSDocumentRetrievalRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string input1;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string input2;
        
        public HOSDocumentRetrievalRequestBody() {
        }
        
        public HOSDocumentRetrievalRequestBody(string input1, string input2) {
            this.input1 = input1;
            this.input2 = input2;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HOSDocumentRetrievalResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HOSDocumentRetrievalResponse", Namespace="http://tempuri.org", Order=0)]
        public Console6.ServiceReference2.HOSDocumentRetrievalResponseBody Body;
        
        public HOSDocumentRetrievalResponse() {
        }
        
        public HOSDocumentRetrievalResponse(Console6.ServiceReference2.HOSDocumentRetrievalResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org")]
    public partial class HOSDocumentRetrievalResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string HOSDocumentRetrievalResult;
        
        public HOSDocumentRetrievalResponseBody() {
        }
        
        public HOSDocumentRetrievalResponseBody(string HOSDocumentRetrievalResult) {
            this.HOSDocumentRetrievalResult = HOSDocumentRetrievalResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HOSDocumentSearchRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HOSDocumentSearch", Namespace="http://tempuri.org", Order=0)]
        public Console6.ServiceReference2.HOSDocumentSearchRequestBody Body;
        
        public HOSDocumentSearchRequest() {
        }
        
        public HOSDocumentSearchRequest(Console6.ServiceReference2.HOSDocumentSearchRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org")]
    public partial class HOSDocumentSearchRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string input1;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string input2;
        
        public HOSDocumentSearchRequestBody() {
        }
        
        public HOSDocumentSearchRequestBody(string input1, string input2) {
            this.input1 = input1;
            this.input2 = input2;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HOSDocumentSearchResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HOSDocumentSearchResponse", Namespace="http://tempuri.org", Order=0)]
        public Console6.ServiceReference2.HOSDocumentSearchResponseBody Body;
        
        public HOSDocumentSearchResponse() {
        }
        
        public HOSDocumentSearchResponse(Console6.ServiceReference2.HOSDocumentSearchResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org")]
    public partial class HOSDocumentSearchResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string HOSDocumentSearchResult;
        
        public HOSDocumentSearchResponseBody() {
        }
        
        public HOSDocumentSearchResponseBody(string HOSDocumentSearchResult) {
            this.HOSDocumentSearchResult = HOSDocumentSearchResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SaveHOSDocumentInfoRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SaveHOSDocumentInfo", Namespace="http://tempuri.org", Order=0)]
        public Console6.ServiceReference2.SaveHOSDocumentInfoRequestBody Body;
        
        public SaveHOSDocumentInfoRequest() {
        }
        
        public SaveHOSDocumentInfoRequest(Console6.ServiceReference2.SaveHOSDocumentInfoRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org")]
    public partial class SaveHOSDocumentInfoRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string input1;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string input2;
        
        public SaveHOSDocumentInfoRequestBody() {
        }
        
        public SaveHOSDocumentInfoRequestBody(string input1, string input2) {
            this.input1 = input1;
            this.input2 = input2;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SaveHOSDocumentInfoResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SaveHOSDocumentInfoResponse", Namespace="http://tempuri.org", Order=0)]
        public Console6.ServiceReference2.SaveHOSDocumentInfoResponseBody Body;
        
        public SaveHOSDocumentInfoResponse() {
        }
        
        public SaveHOSDocumentInfoResponse(Console6.ServiceReference2.SaveHOSDocumentInfoResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org")]
    public partial class SaveHOSDocumentInfoResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string SaveHOSDocumentInfoResult;
        
        public SaveHOSDocumentInfoResponseBody() {
        }
        
        public SaveHOSDocumentInfoResponseBody(string SaveHOSDocumentInfoResult) {
            this.SaveHOSDocumentInfoResult = SaveHOSDocumentInfoResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface PUB0001SoapChannel : Console6.ServiceReference2.PUB0001Soap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PUB0001SoapClient : System.ServiceModel.ClientBase<Console6.ServiceReference2.PUB0001Soap>, Console6.ServiceReference2.PUB0001Soap {
        
        public PUB0001SoapClient() {
        }
        
        public PUB0001SoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PUB0001SoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PUB0001SoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PUB0001SoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Console6.ServiceReference2.HOSDocumentRetrievalResponse Console6.ServiceReference2.PUB0001Soap.HOSDocumentRetrieval(Console6.ServiceReference2.HOSDocumentRetrievalRequest request) {
            return base.Channel.HOSDocumentRetrieval(request);
        }
        
        public string HOSDocumentRetrieval(string input1, string input2) {
            Console6.ServiceReference2.HOSDocumentRetrievalRequest inValue = new Console6.ServiceReference2.HOSDocumentRetrievalRequest();
            inValue.Body = new Console6.ServiceReference2.HOSDocumentRetrievalRequestBody();
            inValue.Body.input1 = input1;
            inValue.Body.input2 = input2;
            Console6.ServiceReference2.HOSDocumentRetrievalResponse retVal = ((Console6.ServiceReference2.PUB0001Soap)(this)).HOSDocumentRetrieval(inValue);
            return retVal.Body.HOSDocumentRetrievalResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Console6.ServiceReference2.HOSDocumentRetrievalResponse> Console6.ServiceReference2.PUB0001Soap.HOSDocumentRetrievalAsync(Console6.ServiceReference2.HOSDocumentRetrievalRequest request) {
            return base.Channel.HOSDocumentRetrievalAsync(request);
        }
        
        public System.Threading.Tasks.Task<Console6.ServiceReference2.HOSDocumentRetrievalResponse> HOSDocumentRetrievalAsync(string input1, string input2) {
            Console6.ServiceReference2.HOSDocumentRetrievalRequest inValue = new Console6.ServiceReference2.HOSDocumentRetrievalRequest();
            inValue.Body = new Console6.ServiceReference2.HOSDocumentRetrievalRequestBody();
            inValue.Body.input1 = input1;
            inValue.Body.input2 = input2;
            return ((Console6.ServiceReference2.PUB0001Soap)(this)).HOSDocumentRetrievalAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Console6.ServiceReference2.HOSDocumentSearchResponse Console6.ServiceReference2.PUB0001Soap.HOSDocumentSearch(Console6.ServiceReference2.HOSDocumentSearchRequest request) {
            return base.Channel.HOSDocumentSearch(request);
        }
        
        public string HOSDocumentSearch(string input1, string input2) {
            Console6.ServiceReference2.HOSDocumentSearchRequest inValue = new Console6.ServiceReference2.HOSDocumentSearchRequest();
            inValue.Body = new Console6.ServiceReference2.HOSDocumentSearchRequestBody();
            inValue.Body.input1 = input1;
            inValue.Body.input2 = input2;
            Console6.ServiceReference2.HOSDocumentSearchResponse retVal = ((Console6.ServiceReference2.PUB0001Soap)(this)).HOSDocumentSearch(inValue);
            return retVal.Body.HOSDocumentSearchResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Console6.ServiceReference2.HOSDocumentSearchResponse> Console6.ServiceReference2.PUB0001Soap.HOSDocumentSearchAsync(Console6.ServiceReference2.HOSDocumentSearchRequest request) {
            return base.Channel.HOSDocumentSearchAsync(request);
        }
        
        public System.Threading.Tasks.Task<Console6.ServiceReference2.HOSDocumentSearchResponse> HOSDocumentSearchAsync(string input1, string input2) {
            Console6.ServiceReference2.HOSDocumentSearchRequest inValue = new Console6.ServiceReference2.HOSDocumentSearchRequest();
            inValue.Body = new Console6.ServiceReference2.HOSDocumentSearchRequestBody();
            inValue.Body.input1 = input1;
            inValue.Body.input2 = input2;
            return ((Console6.ServiceReference2.PUB0001Soap)(this)).HOSDocumentSearchAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Console6.ServiceReference2.SaveHOSDocumentInfoResponse Console6.ServiceReference2.PUB0001Soap.SaveHOSDocumentInfo(Console6.ServiceReference2.SaveHOSDocumentInfoRequest request) {
            return base.Channel.SaveHOSDocumentInfo(request);
        }
        
        public string SaveHOSDocumentInfo(string input1, string input2) {
            Console6.ServiceReference2.SaveHOSDocumentInfoRequest inValue = new Console6.ServiceReference2.SaveHOSDocumentInfoRequest();
            inValue.Body = new Console6.ServiceReference2.SaveHOSDocumentInfoRequestBody();
            inValue.Body.input1 = input1;
            inValue.Body.input2 = input2;
            Console6.ServiceReference2.SaveHOSDocumentInfoResponse retVal = ((Console6.ServiceReference2.PUB0001Soap)(this)).SaveHOSDocumentInfo(inValue);
            return retVal.Body.SaveHOSDocumentInfoResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Console6.ServiceReference2.SaveHOSDocumentInfoResponse> Console6.ServiceReference2.PUB0001Soap.SaveHOSDocumentInfoAsync(Console6.ServiceReference2.SaveHOSDocumentInfoRequest request) {
            return base.Channel.SaveHOSDocumentInfoAsync(request);
        }
        
        public System.Threading.Tasks.Task<Console6.ServiceReference2.SaveHOSDocumentInfoResponse> SaveHOSDocumentInfoAsync(string input1, string input2) {
            Console6.ServiceReference2.SaveHOSDocumentInfoRequest inValue = new Console6.ServiceReference2.SaveHOSDocumentInfoRequest();
            inValue.Body = new Console6.ServiceReference2.SaveHOSDocumentInfoRequestBody();
            inValue.Body.input1 = input1;
            inValue.Body.input2 = input2;
            return ((Console6.ServiceReference2.PUB0001Soap)(this)).SaveHOSDocumentInfoAsync(inValue);
        }
    }
}