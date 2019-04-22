using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace MtomReader
{
    public class ControllerTest : IClassFixture<XUnitWebApplicationFactory<Startup>>
    {
        private readonly XUnitWebApplicationFactory<Startup> _factory;

        public ControllerTest(XUnitWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task When_testing2()
        {
            var request = new HttpRequestMessage();
            request.Headers.Add("MIME-Version", "1.0");

            var outboundPayload = new MultipartContent("related", "uuid:3f4da383-2009-4fbd-a202-2a678f4ec28f+id=9");
            outboundPayload.Headers.ContentType.Parameters.Add(new NameValueHeaderValue("type", "\"application/soap+xml\""));
            outboundPayload.Headers.ContentType.Parameters.Add(new NameValueHeaderValue("start", "\"<http://test.org/0>\""));

            var soapXml = @"<s:Envelope
                            xmlns:s=""http://www.w3.org/2003/05/soap-envelope""
                            xmlns:a=""http://www.w3.org/2005/08/addressing"">
                          <s:Header>
                            <a:Action s:mustUnderstand=""1"">test</a:Action>
                            <a:MessageID>urn:uuid:00000000-0000-0000-0000-000000000000</a:MessageID>
                            <ActivityId CorrelationId=""00000000-0000-0000-0000-000000000000""
                                xmlns=""http://schemas.microsoft.com/2004/09/ServiceModel/Diagnostics"">
                              00000000-0000-0000-0000-000000000000
                            </ActivityId>
                            <a:ReplyTo>
                              <a:Address>http://www.w3.org/2005/08/addressing/anonymous</a:Address>
                            </a:ReplyTo>
                            <a:To s:mustUnderstand=""1"">http://test</a:To>
                          </s:Header>
                          <s:Body>
                          </s:Body>
                        </s:Envelope>";

            ///Ensuring the XML can be loaded
            XDocument.Parse(soapXml);

            var payload = new ByteArrayContent(Encoding.UTF8.GetBytes(soapXml));

            payload.Headers.Add("Content-ID", "<http://test.org/0>");
            payload.Headers.Add("Content-Transfer-Encoding", "8bit");
            payload.Headers.Add("Content-Type", "application/soap+xml");

            outboundPayload.Add(payload);
            request.Content = outboundPayload;

            request.RequestUri = new Uri($"/test", UriKind.Relative);
            request.Method = HttpMethod.Post;

            var client = _factory.CreateClient();
            var response = await client.SendAsync(request);
        }
    }
}
