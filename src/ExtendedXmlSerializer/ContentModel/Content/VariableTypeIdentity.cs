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

using System;
using System.Reflection;
using ExtendedXmlSerializer.ContentModel.Format;
using ExtendedXmlSerializer.ContentModel.Identification;
using ExtendedXmlSerializer.ContentModel.Reflection;
using ExtendedXmlSerializer.Core.Specifications;

namespace ExtendedXmlSerializer.ContentModel.Content
{
	sealed class VariableTypeIdentity : IWriter
	{
		readonly ISpecification<Type> _specification;
		readonly IWriter<object>      _start;
		readonly RuntimeElement       _runtime;

		public VariableTypeIdentity(Type definition, IIdentity identity, RuntimeElement runtime)
			: this(VariableTypeSpecification.Defaults.Get(definition), new Identity<object>(identity), runtime) {}

		public VariableTypeIdentity(ISpecification<Type> specification, IWriter<object> start, RuntimeElement runtime)
		{
			_specification = specification;
			_start         = start;
			_runtime       = runtime;
		}

		public void Write(IFormatWriter writer, object instance)
		{
			var type = instance.GetType();
			if (_specification.IsSatisfiedBy(type))
			{
				_runtime.Get(type.GetTypeInfo())
				        .Write(writer, instance);
			}
			else
			{
				_start.Write(writer, instance);
			}
		}
	}
}