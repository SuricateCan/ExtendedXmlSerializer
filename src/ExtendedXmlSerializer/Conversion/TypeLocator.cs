using System;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using ExtendedXmlSerialization.Conversion.Xml;

namespace ExtendedXmlSerialization.Conversion
{
	class TypeLocator : ITypeLocator
	{
		readonly ITypes _types;

		public TypeLocator(ITypes types)
		{
			_types = types;
		}

		public TypeInfo Get(XmlReader parameter)
		{
			switch (parameter.MoveToContent())
			{
				case XmlNodeType.Element:
					var name = XName.Get(parameter.LocalName, parameter.NamespaceURI);
					var result = _types.Get(name);
					return result;
			}

			throw new InvalidOperationException($"Could not locate the type from the current Xml reader '{parameter}.'");
		}
	}
}