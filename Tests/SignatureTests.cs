﻿using System.Collections.Generic;
using NUnit.Framework;
using ShippingEasy;

namespace Tests
{
    [TestFixture]
    public class SignatureTests
    {
        private string _apiKey;
        private string _apiSecret;
        private int _apiTimestamp;
        private string _body;
        private string _httpMethod;
        private string _path;
        private Dictionary<string, string> _parameters;

        [SetUp]
        public void Setup()
        {
            _apiKey = "444888222";
            _apiSecret = "zxy98761234dcba";
            _apiTimestamp = 1421551594;
            _body = "";
            _httpMethod = "Get";
            _path = "/api/orders";
            _parameters = new Dictionary<string, string>{{"api_key", _apiKey},
                {"page", "1"}, {"count", "200"},
                {"api_timestamp", _apiTimestamp.ToString()}
            };
        }

        [Test]
        public void CombinesSignatureComponentsInDeterministicOrder()
        {
            _body = "{}";
            Assert.AreEqual(
                "GET&/api/orders&api_key=444888222&api_timestamp=1421551594&count=200&page=1&{}",
                BuildSignature().ToPlainTextString());
        }

        [Test]
        public void GeneratesSignatureWithEmptyBody()
        {
            _body = "";
            Assert.AreEqual(
                "0d3ff01aa8c2bd5b9d8a5148b9ac4c9acbd01c9cdd53e01d630bce6cb59a4d20",
                BuildSignature().ToString());
        }

        private Signature BuildSignature()
        {
            return new Signature(_apiSecret, _httpMethod, _path, _parameters, _body);
        }

        [Test]
        public void GeneratesSignatureWithBody()
        {
            _body = "{}";
            Assert.AreEqual(
                "67b39b771fd62a3c9ec2d385330b49cafe595010ba459a9c981384bfcdcf58af",
                BuildSignature().ToString());

        }
    }
}