using ServerManager.Models;
using ServerManager.Models.JsonObjects;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace ServerManager
{
    internal class Manager
    {
        private const string configurationFileName = "configuration.json";

        private static bool enableLogging = false;

        static void Main(string[] args)
        {
            string configurationPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "configuration.json");

            if (!File.Exists(configurationPath))
                throw new Exception("Configuration file for Server Manager could not be found at " + configurationPath);

            string json = null;

            try
            {
                json = File.ReadAllText(configurationPath);
            }
            catch
            {
                throw new Exception("Error when loading configuration file at " + Assembly.GetExecutingAssembly().Location);
            }

            ManagerConfiguration configuration = ManagerConfiguration.FromJson(json);
            Run(configuration).Wait();
        }

        private static async Task Run(ManagerConfiguration configuration)
        {
            try
            {
                enableLogging = configuration.EnableLogging;

                while (true)
                {
                    Netlify netlify = new Netlify(configuration.AccessKey);

                    string ipAddress = IpAddressHelper.GetIpAddress().ToString();
                    Log("Current ip: " + ipAddress);

                    foreach (string domainName in configuration.Domains)
                    {
                        Log("\nWill check: " + domainName);

                        NetlifyDnsRecord record = await netlify.GetDnsRecordAsync(domainName);

                        if (record == null || record.ToString() != ipAddress)
                        {
                            Log("Different ip found! Will update");

                            if (record != null)
                                await netlify.DeleteDnsRecordAsync(record);

                            record = new NetlifyDnsRecord()
                            {
                                Hostname = domainName,
                                Value = ipAddress,
                                Type = "A",
                                Ttl = 1800
                            };

                            await netlify.AddDnsRecordAsync(record);

                            Log("Did update");
                        }
                        else
                        {
                            Log("Same ip found: " + record.ToString());
                        }
                    }

                    await Task.Delay(TimeSpan.FromSeconds(configuration.Interval));
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        private static void Log(string text)
        {
            if (enableLogging)
                Console.WriteLine(text);
        }
    }
}
