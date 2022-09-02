namespace ApplicationCore.Services
{
    public class WebProxyService : System.Net.IWebProxy
    {
        protected System.Net.IWebProxy m_proxy;


        public System.Net.IWebProxy Proxy
        {
            get { return this.m_proxy ??= System.Net.WebRequest.DefaultWebProxy; }
            set { this.m_proxy = value; }
        }

        System.Net.ICredentials System.Net.IWebProxy.Credentials
        {
            get { return this.Proxy.Credentials; }
            set { this.Proxy.Credentials = value; }
        }


        public WebProxyService()
        { } // Constructor 

        public WebProxyService(System.Net.IWebProxy proxy)
        {
            this.Proxy = proxy;
        } // Constructor 


        System.Uri System.Net.IWebProxy.GetProxy(System.Uri destination)
        {
            return this.Proxy.GetProxy(destination);
        }

        bool System.Net.IWebProxy.IsBypassed(System.Uri host)
        {
            return this.Proxy.IsBypassed(host);
        }


    }
}