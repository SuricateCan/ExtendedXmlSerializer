﻿// MIT License
// 
// Copyright (c) 2016 Wojciech Nagórski
//                    Michael DeMond
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.IO;
using System.Text;
using System.Xml.Serialization;
using BenchmarkDotNet.Attributes;
using ExtendedXmlSerialization.Performance.Tests.Model;

namespace ExtendedXmlSerialization.Performance.Tests
{
	public class ExtendedXmlSerializerTest
	{
		readonly TestClassOtherClass _obj = new TestClassOtherClass();
		readonly string _xml;
#pragma warning disable 618
		readonly Legacy.IExtendedXmlSerializer _serializer = new Legacy.ExtendedXmlSerializer();
#pragma warning restore 618

		public ExtendedXmlSerializerTest()
		{
			_obj.Init();
			_xml = SerializationClassWithPrimitive();
			DeserializationClassWithPrimitive();
		}

		[Benchmark]
		public string SerializationClassWithPrimitive() => _serializer.Serialize(_obj);

		[Benchmark]
		public TestClassOtherClass DeserializationClassWithPrimitive() => _serializer.Deserialize<TestClassOtherClass>(_xml);
	}


	public class ExtendedXmlSerializerV2Test
	{
		readonly IExtendedXmlSerializer _serializer = new ExtendedXmlSerializer();
		readonly TestClassOtherClass _obj = new TestClassOtherClass().Init();
		readonly byte[] _xml;

		public ExtendedXmlSerializerV2Test()
		{
			_xml = Encoding.UTF8.GetBytes(SerializationClassWithPrimitive());
			DeserializationClassWithPrimitive();
		}
		
		[Benchmark]
		public string SerializationClassWithPrimitive() => _serializer.Serialize(_obj);

		[Benchmark]
		public object DeserializationClassWithPrimitive() => _serializer.Deserialize(_xml);
	}

	public class XmlSerializerTest
	{
		readonly TestClassOtherClass _obj = new TestClassOtherClass();
		readonly byte[] _xml;
		readonly XmlSerializer _serializer = new XmlSerializer(typeof(TestClassOtherClass));

		public XmlSerializerTest()
		{
			_obj.Init();
			_xml = Encoding.UTF8.GetBytes(SerializationClassWithPrimitive());
			DeserializationClassWithPrimitive();
		}

		[Benchmark]
		public string SerializationClassWithPrimitive()
		{
			using (var stream = new MemoryStream())
			{
				_serializer.Serialize(stream, _obj);
				stream.Seek(0, SeekOrigin.Begin);
				var result = new StreamReader(stream).ReadToEnd();
				return result;
			}
		}

		[Benchmark]
		public object DeserializationClassWithPrimitive()
		{
			using (var stream = new MemoryStream(_xml))
			{
				var result = _serializer.Deserialize(stream);
				return result;
			}
		}
	}
}