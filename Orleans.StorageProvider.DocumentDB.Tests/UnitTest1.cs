using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;
using Orleans.StorageProvider.DocumentDB.TestGrains;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Orleans.StorageProvider.DocumentDB.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestGrains()
        {

            // insert your grain test code here
            var grain = GrainClient.GrainFactory.GetGrain<IGrain1>("1234");
            var now = DateTime.UtcNow;
            var guid = Guid.NewGuid();
            await grain.Set("string value", 12344, now, guid, grain);
            await grain.Set("string value", 12345, now, guid, grain);

            var result = await grain.Get();
            Assert.AreEqual("string value", result.Item1);
            Assert.AreEqual(12345, result.Item2);
            Assert.AreEqual(now, result.Item3);
            Assert.AreEqual(guid, result.Item4);
            Assert.AreEqual("1234", result.Item5.GetPrimaryKeyString());

            await grain.Clear();
        }

      


        // code to initialize and clean up an Orleans Silo

        private static SiloHost siloHost;
        private static AppDomain hostDomain;

        private static void InitSilo(string[] args)
        {
            var config = ClusterConfiguration.LocalhostPrimarySilo();
            config.Globals.RegisterDocumentDBProvider("DocDB", "https://ra-cosmos-test.documents.azure.com:443/", "XXX", "ra-cosmos-test", "Items");
            siloHost = new SiloHost("Primary", config );

            siloHost.InitializeOrleansSilo();
            var ok = siloHost.StartOrleansSilo();
            if (!ok) throw new SystemException($"Failed to start Orleans silo '{siloHost.Name}' as a {siloHost.Type} node.");
        }

        [ClassInitialize]
        public static void GrainTestsClassInitialize(TestContext testContext)
        {
            hostDomain = AppDomain.CreateDomain("OrleansHost", null, new AppDomainSetup
            {
                AppDomainInitializer = InitSilo,
                ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
            });

            GrainClient.Initialize(ClientConfiguration.LocalhostSilo());
        }

        [ClassCleanup]
        public static void GrainTestsClassCleanUp()
        {
            try
            {
                hostDomain.DoCallBack(() =>
                {
                    siloHost.Dispose();
                    siloHost = null;
                    AppDomain.Unload(hostDomain);
                });
            }
            catch (Exception ex)
            { }

            var startInfo = new ProcessStartInfo
            {
                FileName = "taskkill",
                Arguments = "/F /IM vstest.executionengine.x86.exe",
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden,
            };
            Process.Start(startInfo);
        }
    }
}
