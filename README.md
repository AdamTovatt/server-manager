# server-manager
Will run on a server to make sure the dns configuration on netlify is pointing to the right ip address

# Configuration
Needs a configuration.json in the same directory as the executable. It should look like this:

{"accessKey":"your-netlify-key", "domains":["first-domain.com", "second-domain.online"], "interval": 1800, "enableLogging": true}

accessKey: your key to the netlify api. Can be obtained here: https://app.netlify.com/user/applications under "Personal access tokens"
domains: a list of domains to check ip for
interval: the TTL value on netlify, defaults to 1800 if not specified
enableLogging: if the application should write to the console
