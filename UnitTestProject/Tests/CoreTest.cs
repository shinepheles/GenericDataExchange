﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnitTestProject.Infrastructure;
using WCFService.Infrastructure;
using AdvancedDependencyContainer.Infrastructure;
using AdvancedDependencyContainer.Entities;

namespace UnitTestProject.Tests
{
    [Serializable]
    public class TestEntity
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public bool Visable { get; set; }
    }
    [TestClass]
    public class CoreTest
    {
        private string ConnectionStr { get; set; } = "Data Source=192.168.1.2\\SQL2008;uid=sa;pwd=123456a;database=xfzb;Connect Timeout=30";
        private string DesCode { get; set; } = "FF50E1525C650A58AEAB16969808322F8A05F4F054F803F39A9EB1CA3EABBC36E55E402825C50E5BD28A79D5A17761F232CBE1657DA924823180E0634289856C59B54F3B24BEE5D2A58CEE37309BA49BD2509C331F7CC98E";
        [TestMethod]
        public void DESCrypt()
        {
            var keycode = "jxkejixu".Encryptogram();
            var signal = new AutoResetEvent(false);
            signal.WaitOne();
            var encrypeStr = ConnectionStr.Encryptogram("jxkejixu");
            var decrypeStr = encrypeStr.Decryptogram("jxkejixu");
            Assert.AreEqual(decrypeStr, ConnectionStr);
        }
        private byte[] buffer = new byte[1024];
        [TestMethod]
        public void CompressionData()
        {
            var model = new TestEntity { Name = "entity", Id = 1, Visable = true };
            buffer = model.Compression();
            var entity = buffer.Decompress<TestEntity>();
            Assert.IsInstanceOfType(entity, typeof(TestEntity));
        }
        [TestMethod]
        public void IoCKernelTest()
        {
            
            //kernel.Bind<ITestInterface>().To<TestClass>();
            //var model = kernel.Resolve<IoCEntity>();
            //model.Ti.Name = "111";
            //Assert.IsInstanceOfType(model, typeof(IoCEntity));
        }
        [TestMethod]
        public void XmlSerializer()
        {
            //var result = XmlUtil.Deserialize<DependencyContainerConfiguration>(@"E:\GitHub\GenericDataExchange\AdvancedDependencyContainer\ContainerConfiguration.xml");
            //foreach(var item in result.Assmblies)
            //{
            //    foreach(var bind in item.Binds)
            //    {
            //        var s = bind.Key.Location;
            //        var n = bind.Key.Name;
            //        var e = bind.Val.Location;
            //        var c = bind.Val.Name;
            //        var str = s + "." + n + "." + "BindTo" + e + "." + c;
            //    }
            //}
            //XmlUtil.Serializer(new DependencyContainerConfiguration
            //{
            //    Assmblies = new List<AssemblyCollection>
            //    {
            //        new AssemblyCollection
            //        {
            //            Provider = "1",
            //            Binds = new List<BindCollection>
            //            {
            //                new BindCollection
            //                {
            //                    Key = new BindElement{ Location = "a", Name = "b"},
            //                    Val = new BindElement{ Location = "c", Name = "d"}
            //                }
            //            }
            //        }
            //    }
            //});
        }
    }
}
