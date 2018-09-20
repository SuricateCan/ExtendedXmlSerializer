// MIT License
//
// Copyright (c) 2016-2018 Wojciech Nag�rski
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

using ExtendedXmlSerializer.ContentModel.Format;
using ExtendedXmlSerializer.ContentModel.Members;

namespace ExtendedXmlSerializer.ContentModel
{
	class Serializer : Serializer<object>, ISerializer
	{
		public Serializer(IReader reader, IWriter writer) : base(reader, writer) {}
	}

	class Serializer<T> : ISerializer<T>
	{
		readonly IReader<T> _reader;
		readonly IWriter<T> _writer;

		public Serializer(IReader<T> reader, IWriter<T> writer)
		{
			_reader = reader;
			_writer = writer;
		}

		public T Get(IFormatReader parameter) => _reader.GetIfAssigned(parameter);

		public void Write(IFormatWriter writer, T instance) => _writer.Write(writer, instance);
	}
}