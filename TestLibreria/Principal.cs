using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGrid;
using TestLibreria.NancyAnna;
namespace TestLibreria
{
    public class Principal:IAppGridApp
    {
        private Configuration conf;
        private static Anna.HttpServer server;
        private static Nancy.INancyEngine engine;
        private static Nancy.Bootstrapper.INancyBootstrapper bootstrapper;
        public void Init(Configuration configuration)
        {
            this.conf = configuration;
        }

        public void Start()
        {
            bootstrapper = Nancy.Bootstrapper.NancyBootstrapperLocator.Bootstrapper;
            bootstrapper.Initialise();
            engine = bootstrapper.GetEngine();
            server = new Anna.HttpServer("http://"+ conf.Host+":" + conf.HttpPort.ToString() + "/");
            server.RAW("/*").Subscribe(ctx =>
            {
                var request = ctx.Request.ToNancyRequest();
                var result = engine.HandleRequest(request);
                ctx.Respond(new NancyResponse(result.Response));
            });
        }

        public void Stop()
        {
            server.Dispose();
        }
    }
}
