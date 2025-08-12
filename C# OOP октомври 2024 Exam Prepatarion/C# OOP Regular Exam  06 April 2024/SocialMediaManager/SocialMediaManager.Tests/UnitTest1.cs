using System;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

namespace SocialMediaManager.Tests
{
    public class Testove
    {
        private InfluencerRepository repository;
        private Influencer influencer;

        [SetUp]
        public void Setup()
        {
            repository = new InfluencerRepository();
            influencer = new Influencer("Test potrebitel", 1000); 
        }

        [Test]
        public void asffg()
        {
            Assert.AreEqual(0, repository.Influencers.Count);
        }

        [Test]
        public void gjhkg()
        {
            string result = repository.RegisterInfluencer(influencer);
            Assert.AreEqual(1, repository.Influencers.Count);
            Assert.AreEqual($"Successfully added influencer {influencer.Username} with {influencer.Followers}", result);
        }

        [Test]
        public void dsfhgjl()
        {
            Assert.Throws<ArgumentNullException>(() => repository.RegisterInfluencer(null));
        }

        [Test]
        public void sdgfshds()
        {
            repository.RegisterInfluencer(influencer);
            Influencer duplicateInfluencer = new Influencer("Test potrebitel", 500); 
            Assert.Throws<InvalidOperationException>(() => repository.RegisterInfluencer(duplicateInfluencer));
        }

        [Test]
        public void sdfsfgaf()
        {
            repository.RegisterInfluencer(influencer);
            bool result = repository.RemoveInfluencer(influencer.Username);
            Assert.IsTrue(result);
            Assert.AreEqual(0, repository.Influencers.Count);
        }

        [Test]
        public void adfsfhdhfm()
        {
            bool result = repository.RemoveInfluencer("NeSashtestvuvashtPotrebitel"); 
            Assert.IsFalse(result);
        }

        [Test]
        public void sdgzdjfk()
        {
            Assert.Throws<ArgumentNullException>(() => repository.RemoveInfluencer(null));
            Assert.Throws<ArgumentNullException>(() => repository.RemoveInfluencer(" "));
        }

        [Test]
        public void sDGtdjdfjx()
        {
            Influencer prvi = new Influencer("Prvi", 100); 
            Influencer vtori = new Influencer("Vtori", 200); 
            repository.RegisterInfluencer(prvi);
            repository.RegisterInfluencer(vtori);

            Influencer result = repository.GetInfluencerWithMostFollowers();

            Assert.AreEqual(vtori, result);
        }

        [Test]
        public void SDgzrdhzthjnx()
        {
            Assert.Throws<IndexOutOfRangeException>(() => repository.GetInfluencerWithMostFollowers());
        }

        [Test]
        public void szrgtjdxy()
        {
            repository.RegisterInfluencer(influencer);
            Influencer result = repository.GetInfluencer("Test potrebitel"); 
            Assert.AreEqual(influencer, result);
        }

        [Test]
        public void ydgjhsr()
        {
            Influencer result = repository.GetInfluencer("NeSashtestvuvashtPotrebitel"); 
            Assert.IsNull(result);
        }
    }
}
