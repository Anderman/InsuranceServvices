# Vecozo 'outgoing services'

Vecozo will send info to us when there is news. They send 
* statusupdates
* returninfo. 

They want also check if we are alive.


They call us with a valid client certificate. This client certificate is on the httprequest. This certificate should be send through iis to kestrel without config on a https request.


The *ClientCertificateAuthorization* read the http request to test a for a valid certificate.

With this service a validation can be done on the implemented vecozo 'outgoing service'


## Soap service implementation
The middleware will scan all soap request and use a xmlserializer. All soap request has an *action*. 
The *actions* that must implemented are in the wsdl file. With a WCF tool this file can converted to C#.

This file can't  be used and should be converted to a less verbose version.
Also the 4 reference to *system.servicemodelxxx* should be removed when the VS tool is used.

The implemented soap service read only attributes of the *service contract* and *operation contracts*.
all other attributes should be removed and new xmlElement attributes should be placed on the right properties 
The xml attribute is only needed to set the correct namespace and can be used to overrule a elementname

The reason that the generated file can't be used is that the default xmlserialize and the soap xml serializer use different attributes to specify namespaces.

### incomming call
When the middleware see a soap request with an soapaction then the service that implement this '*action*' will be started by DI.
The soap request is parsed and converted to the request object parameter of the *operation*
When the xml is parsed all namespace are removed. **Be sure that your request object has no namespaces**. Just remove all soap attributes from the generated c# file should be fine.

### outgoing response
The action is called and the response object is returned to the middeleware.
The middeware serialize this object with an xmlserialize to a soap response. **Be sure that you have set the correct namespaces on the properies of the response object**. 
NB: If the namespace is not changed on a deeper level then the namespace attribute can be skipped.

### troubleshooting
Problems with soap(xml) is almost always namespaces. there should be no namespace placed on all request parameter objects.
Only on the response object there should be namespace on the right place. Soap set namespaces on classes you should set the namespace on properties that refer to the classes.

## Soap Client implementation
A soap client can be instantiated by the DI. The client is specified by a config type, response type and a request type.
The generated WCF file is only used for the response and request object. Attributes on service and operation are ignored.
There are 3 things that should be copied from the service/operation attributes to the config file
* the soapaction from *action* attribute on the operationcontract
* the ActionElementname. this the *methodename* without the async
* the namespace from the servicecontract.

The url and the clientcertificate are also in the config file. The url can depend on the enviroment.

